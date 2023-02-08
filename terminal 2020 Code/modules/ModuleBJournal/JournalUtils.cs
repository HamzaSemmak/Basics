using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleMAJ;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class JournalUtils
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(JournalUtils));
        public const string dirName = "journaux";
        public const int PAGE_SIZE = 23;
        public const int MAX_MOUNTED_BJ = 30;
        public static Thread DeserializerThread;

        private static volatile object _obj = new object();
        private static bool Starting = true;

        public static void initJournalXmlFile(DateTime dateTime)
        {
            if (!Directory.Exists(dirName))
            {
                _ = Directory.CreateDirectory(dirName);
            }
            string currentJournal = dirName + "\\BJ." + dateTime.ToString("yyyyMMdd") + ".xml";

            if (!File.Exists(currentJournal))
            {
                createJournalFile(dateTime);
            }
            else
            {
                ApplicationContext.JOURNAL = DeserializeJournalFile(currentJournal, dateTime);
                if (ApplicationContext.JOURNAL == null
                    || ApplicationContext.JOURNAL.Session == null
                    || ApplicationContext.JOURNAL.Session == default
                    || ApplicationContext.JOURNAL.Operations == null)
                {
                    createJournalFile(dateTime);
                }
                else if (!ApplicationContext.JOURNAL.Session
                    .ToString("yyyyMMdd")
                    .Equals(dateTime.ToString("yyyyMMdd")))
                {
                    int counter = 0;
                    string backUp;
                    do
                    {
                        counter++;
                        backUp = dirName + "\\BJ." + ApplicationContext.JOURNAL.Session.ToString("yyyyMMdd") + "." + counter.ToString("00") + ".xml";
                    }
                    while (File.Exists(backUp));
                    File.Move(currentJournal, backUp);
                    createJournalFile(dateTime);
                }
            }
            ApplicationContext.BJS = GetJournalList(dateTime);
            if (Starting)
            {
                saveJournalDemarrage(dateTime);
            }
            Starting = false;
        }

        public static void createJournalFile(DateTime dateTime)
        {
            lock (_obj)
            {
                string location = dirName + "\\BJ." + dateTime.ToString("yyyyMMdd") + ".xml";
                ApplicationContext.JOURNAL = new ConcurrentJournal(dateTime, "1.0.0", 1);
                using (var writter = new FileStream(location, FileMode.Create, FileAccess.Write))
                {
                    var xmlSerializer = new XmlSerializer(typeof(Journal));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(writter, new Journal(ApplicationContext.JOURNAL), ns);
                }
            }
        }

        public static ConcurrentJournal DeserializeJournalFile(string name, DateTime dateTime)
        {
            ConcurrentJournal journal = null;
            DeserializerThread = new Thread(() =>
            {
                try
                {
                    using (FileStream reader = File.OpenRead(name))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Journal));
                        Journal _journal = (Journal)xmlSerializer.Deserialize(reader);
                        if (_journal != null
                            && _journal.Session != null
                            && _journal.Session != default
                            && _journal.Operations != null)
                        {
                            journal = new ConcurrentJournal(_journal);
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    if (journal == null)
                    {
                        _ = FileUtils.RenameOrMoveFile(name, dirName + "\\BJ." + dateTime.ToString("yyyyMMddHHmmss") + ".xml.ERR");
                    }
                    Logger.Error("Deserialize journal file error: " + ex.Message + ex.StackTrace);
                }
            });
            DeserializerThread.Priority = ThreadPriority.Lowest;
            DeserializerThread.Start();
            DeserializerThread.Join();
            DeserializerThread = null;
            return journal;
        }

        public static List<ConcurrentLigne> GetNextPage(int from, ConcurrentJournal journal)
        {
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            if (journal != null)
            {
                int linesCount = journal.GetTotalLines();
                if (linesCount > from)
                {
                    List<ConcurrentOperation> sortedOperations = journal.Operations.OrderByDescending(op => op.Heure).ToList();
                    List<ConcurrentLigne> lines = GetListLines(sortedOperations);
                    int number = PAGE_SIZE;
                    if (linesCount - from < number)
                    {
                        number = linesCount - from;
                    }

                    lignes = lines.GetRange(from, number);
                }
            }
            return lignes;
        }

        public static List<ConcurrentLigne> GetListLines(List<ConcurrentOperation> operations)
        {
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            foreach (ConcurrentOperation operation in operations)
            {
                List<ConcurrentLigne> sortedLignes = operation.Lignes.OrderBy(line => line.Order).ToList();
                lignes.AddRange(sortedLignes);
            }
            return lignes;
        }

        public static List<JournalLite> GetJournalList(DateTime dateTime)
        {
            List<JournalLite> journals = new List<JournalLite>();
            string[] files = Directory.GetFiles(dirName, "BJ.*.xml");
            files = files.OrderByDescending(file => file).ToArray();
            for (int i = 0; i < files.Length; i++)
            {
                if (journals.Count == MAX_MOUNTED_BJ)
                {
                    break;
                }
                try
                {
                    string session = files[i].Split('.')[1];
                    DateTime sessionDt = DateTime.ParseExact(session, "yyyyMMdd", null);
                    bool isLast = i == files.Length - 1 || journals.Count == MAX_MOUNTED_BJ - 1;
                    bool isFirst = sessionDt.Date.Equals(dateTime.Date);
                    JournalLite jl = new JournalLite(files[i], sessionDt, isLast, isFirst);
                    journals.Add(jl);
                }
                catch { }
            }
            return journals;
        }

        private static void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ApplicationContext.JOURNAL == null)
            {
                Logger.Warn("Journal not initialized");
                return;
            }
            BJBackgroundDataHolder dataHolder = (BJBackgroundDataHolder)e.Argument;
            try
            {
                ConcurrentOperation operation = new ConcurrentOperation(dataHolder.DateTime)
                {
                    Type = dataHolder.Type
                };
                List<ConcurrentLigne> sortedConcurrentLignes = dataHolder.Lignes.OrderBy(cl => cl.Order).ToList();
                int cmp = 0;
                foreach (ConcurrentLigne lg in sortedConcurrentLignes)
                {
                    MatchCollection mc = StringUtils.SplitToLines(lg.Value2, 38);
                    for (int i = 0; i < mc.Count; i++)
                    {
                        cmp += i;
                        operation.Lignes
                                    .Add(new ConcurrentLigne(i == 0 ? lg.Value1 : "", mc[i].Value.Replace('\n', ' '), i == 0 ? lg.Value3 : "", lg.Order + cmp, lg.BackColor, lg.ForColor));
                    }
                }
                lock (_obj)
                {
                    if (!operation.Heure.Date.Equals(ApplicationContext.JOURNAL.Session.Date))
                    {
                        initJournalXmlFile(operation.Heure);
                    }
                    ApplicationContext.JOURNAL.AddOperation(operation);
                    string location = dirName + "\\BJ." + ApplicationContext.JOURNAL.Session.ToString("yyyyMMdd") + ".xml";
                    using (FileStream writter = new FileStream(location, FileMode.Create, FileAccess.ReadWrite))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Journal));
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        Journal journal = new Journal(ApplicationContext.JOURNAL);
                        xmlSerializer.Serialize(writter, journal, ns);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception: {0} - StackTrace: {1}\n Data Journal: {2}", ex.Message, ex.StackTrace, dataHolder.ToString()));
            }
        }

        private static void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.Error("JournalUtils backgroundWorker_RunWorkerCompleted: " + e.Error.Message);
            }
        }

        private static void addOperation(OperationType type, List<ConcurrentLigne> lignes, BJBackgroundDataHolder bJBackgroundDataHolder = null)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            if (bJBackgroundDataHolder == null)
            {
                bJBackgroundDataHolder = new BJBackgroundDataHolder(type, lignes, DateTime.Now);
            }
            backgroundWorker.RunWorkerAsync(bJBackgroundDataHolder);
        }

        // ************************
        public static void saveJournalAuth(string preposé, string Message, DateTime dateTime)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = dateTime.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, Message, "", order++, "LightGray", "Black"));
            lignes.Add(new ConcurrentLigne("", "PRÉPOSÉ:    " + preposé, "", order++, "LightGray", "Black"));
            lignes.Add(new ConcurrentLigne("", "IP:         " + ApplicationContext.IP, "", order++, "White", "Black"));
            lignes.Add(new ConcurrentLigne("", "PDV:        " + ConfigUtils.ConfigData.Num_pdv, "", order++, "White", "Black"));
            lignes.Add(new ConcurrentLigne("", "POS:        " + ConfigUtils.ConfigData.Pos_terminal, "", order++, "White", "Black"));
            lignes.Add(new ConcurrentLigne("", "VERSION:        " + ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV, "", order++, "White", "Black"));
            lignes.Add(new ConcurrentLigne("", "SERVEUR:    " + ConfigUtils.ConfigData.DnsServer, "", order++, "Gray", "Black"));
            BJBackgroundDataHolder bJBackgroundDataHolder = new BJBackgroundDataHolder(OperationType.Login, lignes, dateTime);
            addOperation(OperationType.Login, lignes, bJBackgroundDataHolder);
        }

        public static void saveJournalMessage(string message)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, message, "", order++, "LightGray", "Red"));
            addOperation(OperationType.Message, lignes);
        }
        public static void saveJournalPaiementErreur(string msgErreur, int numreunion, int numCourse)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, "PAIEMENT REFUSÉ", "", order++, "Gray", "Red"));
            lignes.Add(new ConcurrentLigne("", msgErreur, "", order++, "LightGray", "Red"));
            addOperation(OperationType.Message, lignes);
        }
        public static void saveJournalPaiement(string type, Ticket ticket, decimal montant)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, ticket.DateReunion.ToString("dd-MM-yyyy"), "N°" + Int64.Parse(ticket.IdTicket), order++, "Gray", "Blue"));
            if (montant != 0)
            {
                string messageType = type == "A" ? "ANNULATION CLIENT" : "PAIEMENT GAIN CLIENT";
                string date = DateTime.Now.ToString("dd-MM-yyyy");
                switch (type)
                {
                    case "A":
                        lignes.Add(new ConcurrentLigne(date, messageType, (-montant) + " DH", order++, "LightGray", "Blue"));
                        break;
                    case "P":
                        lignes.Add(new ConcurrentLigne(date, messageType, montant + " DH", order++, "LightGray", "Blue"));
                        break;
                }
                addOperation(OperationType.Paiement, lignes);
            }
        }

        public static void saveJournalPaiementVoucher(Voucher voucher, decimal montant)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            lignes.Add(new ConcurrentLigne(DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.ToString("dd-MM-yyyy"), "N°" + Int64.Parse(voucher.IdVoucher), order++, "Gray", "Blue"));
            if (montant != 0)
            {
                lignes.Add(new ConcurrentLigne(DateTime.Now.ToString("dd-MM-yyyy"), "PAIEMENT CHEQUE JEUX", (-montant) + " DH", order++, "LightGray", "Blue"));
                addOperation(OperationType.Paiement, lignes);
            }
        }

        public static void saveJournalVoucher(Voucher voucher, bool isPrinted, string msg = "DISTRIBUTION CHÈQUE JEUX")
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            lignes.Add(new ConcurrentLigne(voucher.DateEmission.ToString("HH:mm:ss"), voucher.DateSession.ToString("dd-MM-yyyy"), "N°" + Int64.Parse(voucher.IdVoucher), order++, "Gray", isPrinted ? "Blue" : "Red"));
            lignes.Add(new ConcurrentLigne(voucher.DateEmission.ToString("dd-MM-yyyy"), msg, voucher.Montant + " DH", order++, "LightGray", isPrinted ? "Blue" : "Red"));
            addOperation(OperationType.Voucher, lignes);
        }
        public static void saveJournalGL(string idTicket, decimal montant, bool isPrinted)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            ConcurrentLigne firstLigne = new ConcurrentLigne(DateTime.Now.ToString("HH:mm:ss"),
                "TRANSACTION",
                "N° " + Int32.Parse(idTicket).ToString(),
                order++,
                "Gray", isPrinted ? "Blue" : "Red");
            lignes.Add(firstLigne);
            if (!isPrinted)
                lignes.Add(new ConcurrentLigne("", "TICKET NON IMPRIMÉ", "", order++, "Gray", "Red"));
            lignes.Add(new ConcurrentLigne(DateTime.Now.ToString("dd-MM-yyyy"), "Félicitation Vous avez remporté ", montant + "DH", order++, "LightGray", isPrinted ? "Blue" : "Red"));
            addOperation(OperationType.Paiement, lignes);
        }

        public static void saveJournalDepot(string cin, string numeroTransaction, string montant, bool isPrinted)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            StringBuilder depotInfo = new StringBuilder();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            depotInfo.Append("N° " + numeroTransaction);
            lignes.Add(new ConcurrentLigne(value1, "DEPÔT SUR COMPTE", montant + "DH", order++, "Gray", isPrinted ? "Blue" : "Red"));
            lignes.Add(new ConcurrentLigne("", "N° " + numeroTransaction, cin, order++, "Gray", isPrinted ? "Blue" : "Red"));
            if (!isPrinted)
                lignes.Add(new ConcurrentLigne("", "TICKET NON IMPRIMÉ", "", order++, "Gray", "Red"));
            addOperation(OperationType.DepotSurCompte, lignes);
        }

        public static void SaveNewAccount(string cin, DateTime time, bool isPrinted)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = time.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, "CRÉATION COMPTE", cin, order++, "Gray", isPrinted ? "Blue" : "Red"));
            if (!isPrinted)
                lignes.Add(new ConcurrentLigne("", "TICKET NON IMPRIMÉ", "", order++, "Gray", "Red"));
            addOperation(OperationType.DepotSurCompte, lignes);
        }
        
        public static void saveJournalFinClient(decimal montant)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            StringBuilder infoClient = new StringBuilder();
            string operationType = montant > 0 ? "VOUS RECEVEZ" : "VOUS DEVEZ";
            infoClient.Append("FIN CLIENT " + ApplicationContext.JOURNAL.NumLastClient + "  ");
            infoClient.Append(operationType);
            string value3 = Math.Abs(montant) + "DH";
            lignes.Add(new ConcurrentLigne(value1, infoClient.ToString(), value3, order++, "Black", "White"));
            ApplicationContext.JOURNAL.NumLastClient++;
            addOperation(OperationType.FinClient, lignes);
        }
        public static void saveJournalDemarrage(DateTime dateTime)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            StringBuilder value2 = new StringBuilder();
            lignes.Add(new ConcurrentLigne("+--------------------------", "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", "----------------+", order++, "Green", "White"));
            value2.Append("DÉMARRAGE  LE  ");
            value2.Append(ApplicationContext.JOURNAL.Session.ToString("dd/MM/yyyy"));
            value2.Append("  À  " + DateTime.Now.ToString("HH:mm:ss"));
            lignes.Add(new ConcurrentLigne("|", value2.ToString(), "|", order++, "Green", "White"));

            string terminalInfo = "TERMINAL  GAMMA 2020";
            lignes.Add(new ConcurrentLigne("|", terminalInfo, "|", order++, "Green", "White"));

            string applicationInfo = "APPLICATION  GAMMA 2020  V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                 + ApplicationContext.SOREC_DATA_ENV;
            lignes.Add(new ConcurrentLigne("|", applicationInfo, "|", order++, "Green", "White"));
            lignes.Add(new ConcurrentLigne("|", "SERVEUR  " + ConfigUtils.ConfigData.DnsServer, "|", order++, "Green", "White"));
            lignes.Add(new ConcurrentLigne("+--------------------------", "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", "----------------+", order++, "Green", "White"));
            BJBackgroundDataHolder bJBackgroundDataHolder = new BJBackgroundDataHolder(OperationType.Login, lignes, dateTime);
            addOperation(OperationType.Demarrage, lignes, bJBackgroundDataHolder);
        }
        public static void saveJournalDistribution(Ticket ticket, bool isPrinted, string msg = "DISTRIBUTION")
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            lignes.Add(new ConcurrentLigne(ticket.DateEmission.ToString("dd-MM-yyyy"), msg, ticket.PrixTotalTicket + " DH", order++, "Gray", isPrinted ? "Blue" : "Red"));

            /*string details = ">>  " + "1."
                                       + ticket.DateReunion.ToString("ddMMyy")
                                       + "." + ticket.IdServeur + "."
                                       + int.Parse(ticket.IdTicket) + "." + ticket.CVNT + "  <<";
            lignes.Add(new ConcurrentLigne("", details, "", "LightGray", isPrinted ? "Blue" : "Red"));
            */
            StringBuilder ticketInfo = new StringBuilder();
            ticketInfo.Append(ticket.CodeHippo);
            ticketInfo.Append("   R" + ticket.NumReunion);
            ticketInfo.Append("C" + ticket.NumCourse);
            ticketInfo.Append("   " + ticket.DateReunion.ToString("dd-MM-yyyy"));
            lignes.Add(new ConcurrentLigne(ticket.DateEmission.ToString("HH:mm:ss"),
                ticketInfo.ToString(),
                "N°" + int.Parse(ticket.IdTicket),
                order++,
                "LightGray",
                isPrinted ? "Blue" : "Red"));
            foreach (Formulation fm in ticket.ListeFormulation)
            {
                lignes.Add(new ConcurrentLigne("", fm.Designation, "", order++, "LightGray", isPrinted ? "Blue" : "Red"));
                lignes.Add(new ConcurrentLigne("C" + ticket.NumCourse + " " + getNameProduit(fm.Produit.NomProduit), "Enjeu   " + fm.Produit.EnjeuMin + " DH", fm.MiseTotal + " DH", order++, "LightGray", isPrinted ? "Blue" : "Red"));
            }

            addOperation(OperationType.Distribution, lignes);
        }
        public static void saveJournalCriteres(Course myCourse, Reunion r)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string dateNow = DateTime.Now.ToString("HH:mm:ss") + "                        Critères Course  R  " + myCourse.NumReunion + " C " + myCourse.NumCoursePmu;
            string hippodrome = dateNow + "\n" + "                                          " + r.CodeHippo + "     " + r.DateReunion.ToString("dd/MM/yyyy");
            string listPartants = hippodrome + "\n" + "                                          " + myCourse.ListeHorses.Count + "DECLARES PARTANTS";
            if (myCourse.ListeNonPartant.Count == 0)
                listPartants += "\n" + "                                          TOUS DECLARES PARTANTS" + "  ";
            else
                lignes.Add(new ConcurrentLigne(listPartants += "\n" + myCourse.ListeNonPartant.Count + "                                          DECLARES NON PARTANTS" + "\n" + "  ", "", "", order++));
            string parisEnJeux = listPartants + "\n" + "                                       PARIS         " + "                    ENJEUX ";
            foreach (Produit p in myCourse.ListeProduit)
            {
                string str = "";
                if (p.Ordre) str = "ORDRE ";
                parisEnJeux += "\n" + "                                          " + p.NomProduit + "       " + str + "     " + p.EnjeuMin;
            }
            lignes.Add(new ConcurrentLigne(parisEnJeux, "", "", order++));
            addOperation(OperationType.Critéres, lignes);
        }

        public static void saveJournalInvalidDesignation(int numR, int numC, string invalidDesignation)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            string value2 = "R" + numR + " C" + numC + " FORMULATION INCORRECTE";
            lignes.Add(new ConcurrentLigne(value1, value2, "", order++, "Gray", "Red"));
            lignes.Add(new ConcurrentLigne("", invalidDesignation, "", order++, "Gray", "Red"));
            addOperation(OperationType.Distribution, lignes);
        }

        public static void SaveSimpleMsg(string v, Color red)
        {
            int order = 0;
            List<ConcurrentLigne> lignes = new List<ConcurrentLigne>();
            string value1 = DateTime.Now.ToString("HH:mm:ss");
            lignes.Add(new ConcurrentLigne(value1, v, "", order++, "Gray", red.Name));
            addOperation(OperationType.Message, lignes);
        }


        // **************************
        private static string getNameProduit(string nomProduit)
        {
            string produit;
            switch (nomProduit)
            {
                case "QUARTE PLUS": produit = "QUARTE +"; break;
                case "QUINTE PLUS": produit = "QUINTE +"; break;
                default: produit = nomProduit; break;
            }
            return produit;
        }

    }
}
