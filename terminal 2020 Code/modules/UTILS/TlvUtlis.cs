using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleCoteRapport;
using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.ModuleMAJ;
using sorec_gamma.modules.ModuleMAJ.Controls;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.TLV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace sorec_gamma.modules.UTILS
{
    class TlvUtlis
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TlvUtlis));

        public static List<int> listSindexes = new List<int>();
        public static void SetCorpsCommunication(TLVTags communicationsTag)
        {
            if (communicationsTag != null && communicationsTag.length > 0)
            {
                TLVHandler communicationsTagHandler = new TLVHandler(Utils.bytesToHex(communicationsTag.value));
                List<TLVTags> communicationTag = communicationsTagHandler.getTLVList(TLVTags.SOREC_DATA_COMMUNICATION);
                foreach (var com in communicationTag)
                {
                    TLVHandler communicationTagHandler = new TLVHandler(Utils.bytesToHex(com.value));
                    string typeCommunication = Utils.HexToASCII(Utils.bytesToHex(communicationTagHandler.getTLV(TLVTags.SOREC_DATA_COMMUNICATION_TYPE).value));
                    string corpsCom = Utils.HexToASCII(Utils.bytesToHex(communicationTagHandler.getTLV(TLVTags.SOREC_DATA_COMMUNICATION_CORPS).value));
                    switch (typeCommunication)
                    {
                        case "SURTERMINAL":
                            ApplicationContext.CommunicationTerminal = corpsCom;
                            break;
                        case "SURTICKET":
                            ApplicationContext.CommunicationTicket = corpsCom;
                            break;
                    }
                }
            }
        }
       
        public static void NumPosPdvControle(TLVTags NumPdvTag, TLVTags PosTeminalTag)
        {
            if (NumPdvTag != null)
            {
                ConfigUtils.ConfigData.Num_pdv = Utils.bytesToHex(NumPdvTag.value);
            }
            if (PosTeminalTag != null)
            {
                ConfigUtils.ConfigData.Pos_terminal = Utils.HexToASCII(Utils.bytesToHex(PosTeminalTag.value));
            }
            ConfigUtils.createOrUpdateConfigFile(ConfigUtils.ConfigData);
        }
        public static void AttributaireControle(TLVTags attributaireTag)
        {
            if (attributaireTag != null)
            {
                TLVHandler attributaireTagHandler = new TLVHandler(Utils.bytesToHex(attributaireTag.value));
                ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.IdAttributaire = Int32.Parse(Utils.bytesToHex(attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_ID).value));
                ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.Code = Utils.HexToASCII(Utils.bytesToHex(attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_CODE).value));
                ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.Libelle = Utils.HexToASCII(Utils.bytesToHex(attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_LIBELLE).value));
                ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.Version = Utils.HexToASCII(Utils.bytesToHex(attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_VERSION).value));

                TLVTags listeProduitMARTAG = attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUITS_MA);
                TLVTags listeProduitFRTAG = attributaireTagHandler.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUITS_FR);
                if (listeProduitMARTAG != null)
                {
                    TLVHandler listeProduitMARTAGHandler = new TLVHandler(Utils.bytesToHex(listeProduitMARTAG.value));
                    List<TLVTags> produitsMA = listeProduitMARTAGHandler.getTLVList();
                    List<Produit> listePrMA = new List<Produit>();

                    foreach (var pr in produitsMA)
                    {
                        Produit p = new Produit();
                        TLVHandler tLVHandler1 = new TLVHandler(Utils.bytesToHex(pr.value));
                        p.IdProduit = Int32.Parse(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ID).value));
                        p.NomProduit = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_DESIGNATION).value));
                        p.CodeProduit = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_CODE).value));
                        p.EnjeuMin = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ENJEU_MIN).value)));
                        p.EnjeuMax = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ENJEU_MAX).value)));
                        string ordre = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ORDRE).value));
                        string ChampX = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_CHAMP_X).value));
                        string express = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_EXPRESS).value));
                        p.NombreBase = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_NBR_CHVEAUX_BASE).value)));
                        p.Genre = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_APPARTENANCE).value));
                        string statut = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_STATUT).value));

                        switch (ordre)
                        {
                            case "O":
                                p.Ordre = true;
                                break;
                            case "N":
                                p.Ordre = false;
                                break;
                        }
                        switch (ChampX)
                        {
                            case "O":
                                p.ChampX = true;
                                break;
                            case "N":
                                p.ChampX = false;
                                break;
                        }
                        switch (express)
                        {
                            case "O":
                                p.ChevalExpress = true;
                                break;
                            case "N":
                                p.ChevalExpress = false;
                                break;
                        }
                        if (statut.Equals("ACTIVE"))
                        {
                            listePrMA.Add(p);
                        }

                    }
                    ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.ProditMA = listePrMA;

                }
                if (listeProduitFRTAG != null)
                {
                    TLVHandler listeProduitFRTAGHandler = new TLVHandler(Utils.bytesToHex(listeProduitFRTAG.value));
                    List<TLVTags> produitsFR = listeProduitFRTAGHandler.getTLVList();
                    List<Produit> listePrFR = new List<Produit>();
                    foreach (var pr in produitsFR)
                    {
                        Produit p = new Produit();
                        TLVHandler tLVHandler1 = new TLVHandler(Utils.bytesToHex(pr.value));
                        p.IdProduit = Int32.Parse(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ID).value));
                        p.NomProduit = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_DESIGNATION).value));
                        p.CodeProduit = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_CODE).value));
                        p.EnjeuMin = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ENJEU_MIN).value)));
                        p.EnjeuMax = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ENJEU_MAX).value)));
                        string ordre = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_ORDRE).value));
                        string ChampX = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_CHAMP_X).value));
                        string express = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_EXPRESS).value));
                        p.NombreBase = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_NBR_CHVEAUX_BASE).value)));
                        p.Genre = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_APPARTENANCE).value));
                        string statut = Utils.HexToASCII(Utils.bytesToHex(tLVHandler1.getTLV(TLVTags.SOREC_DATA_ATTRIBUTAIRE_PRODUIT_STATUT).value));

                        switch (ordre)
                        {
                            case "O":
                                p.Ordre = true;
                                break;
                            case "N":
                                p.Ordre = false;
                                break;
                        }
                        switch (ChampX)
                        {
                            case "O":
                                p.ChampX = true;
                                break;
                            case "N":
                                p.ChampX = false;
                                break;
                        }
                        switch (express)
                        {
                            case "O":
                                p.ChevalExpress = true;
                                break;
                            case "N":
                                p.ChevalExpress = false;
                                break;
                        }
                        if (statut.Equals("ACTIVE"))
                        {
                            listePrFR.Add(p);
                        }
                    }
                    ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.ProditFR = listePrFR;

                }
            }
        }

        public static void MAJLogicielleControle(string tLV)
        {
            TLVHandler appTagsHandlerResponse = new TLVHandler(tLV);
            TLVTags majLogicielle = appTagsHandlerResponse.getTLV(TLVTags.SOREC_MAJ_LOGICIELLE);

            if (majLogicielle != null)
            {

                MAJControle mAJLogicielleControle = new MAJControle();
                string result = mAJLogicielleControle.sendRequest("EN_COURS_MAJ");
                int code_reponse = TLVHandler.Response(result);
                string tlv = TLVHandler.getTLVChamps(result);
                TLVHandler appTagHandler = new TLVHandler(tlv);
                bool isDownloaded = false;

                if (code_reponse == Constantes.RESPONSE_CODE_OK)
                {
                    TLVTags majLog = appTagHandler.getTLV(TLVTags.SOREC_MAJ_LOGICIELLE);
                    TLVHandler majHandler = new TLVHandler(Utils.bytesToHex(majLog.value));
                    string new_version = Utils.bytesToHex(majHandler.getTLV(TLVTags.SOREC_DATA_LOGICIEL_VERSION).value);
                    DateTime dateVersion = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(majHandler.getTLV(TLVTags.SOREC_DATA_LOGICIEL_DATE_TIME).value)));
                    
                    string url = ConfigUtils.ConfigData.Host_ftp;
                    string port = ConfigUtils.ConfigData.Port_ftp;
                    string login = ConfigUtils.ConfigData.Login;
                    string mdp = ConfigUtils.ConfigData.Mdp;
                    string destRep = "D:\\GAMMA\\MAJ";
                    string source_dir = ConfigUtils.ConfigData.Source_rep;

                    Logger.Info(string.Format("url: {0}, port: {1}, source: {2}, version: {3}, date maj: {4}, destination: {5} ",
                            url, port, source_dir, new_version, dateVersion, destRep));

                    Thread ftpThread = new Thread(() =>
                    {
                        FtpClient ftpClient = FtpClient.GetInstance(url, login, mdp, port);
                        try
                        {
                            string datePrevu = dateVersion.ToString("yyyyMMdd");
                            isDownloaded = ftpClient.DownloadFile("T2020_" + new_version + ".zip", source_dir, destRep + "\\" + datePrevu + "\\");
                            if (isDownloaded)
                            {
                                Logger.Info("isDownloaded");

                                // 3. calculate checksum
                                //string checksum = ChecksumUtils.GetChecksum(HashingAlgoTypes.MD5, destDir + newVersionFilename);
                                //   Console.WriteLine("File checksum : " + checksum);
                                // TODO: compare checksums

                                mAJLogicielleControle.sendRequest("CHARGE");
                                // 4. extract the file
                                //  FileUtils.Extract(destRep + newVersionFilename, destDir + newVersion);
                            }
                            else
                            {
                                Logger.Error("failed to load from ftp ");

                                // TODO: Ask the product owner
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error("Exception download ftp " + e.Message + e.StackTrace);
                        }

                    });
                    ftpThread.Name = "FTP";
                    ftpThread.Start();
                    if (dateVersion.Date <= DateTime.Now.Date)
                    {
                        ftpThread.Join();
                        if (!ApplicationContext.develop) TerminalMonitor.Reboot();
                    }
                }
                else
                {
                    // asking for new version failed
                }

            }
        }

        public static bool MAJLogicielleControle(DateTime dateVersion, string new_version, MAJControle mAJLogicielleControle)
        {
            bool updated = false;

            string url = ConfigUtils.ConfigData.Host_ftp;
            string port = ConfigUtils.ConfigData.Port_ftp;
            string login = ConfigUtils.ConfigData.Login;
            string mdp = ConfigUtils.ConfigData.Mdp;
            string destRep = "D:\\GAMMA\\MAJ";
            string source_dir = ConfigUtils.ConfigData.Source_rep;
            Logger.Info(string.Format("MAJLogicielleControle: url: {0}, port: {1}, source: {2}, version: {3}, date maj: {4}, destination: {5} ",
                url, port, source_dir, new_version, dateVersion, destRep));

            FtpClient ftpClient = FtpClient.GetInstance(url, login, mdp, port);
            try
            {
                string datePrevu = dateVersion.ToString("yyyyMMdd");
                bool isDownloaded = ftpClient.DownloadFile("T2020_" + new_version + ".zip", source_dir, destRep + "\\" + datePrevu + "\\");
                if (isDownloaded)
                {
                    Logger.Info("MAJLogicielleControle: Downloaded");

                    // 3. calculate checksum
                    //string checksum = ChecksumUtils.GetChecksum(HashingAlgoTypes.MD5, destDir + newVersionFilename);
                    //   Console.WriteLine("File checksum : " + checksum);
                    // TODO: compare checksums

                    mAJLogicielleControle.sendRequest("CHARGE");
                    updated = true;
                    // 4. extract the file
                    //  FileUtils.Extract(destRep + newVersionFilename, destDir + newVersion);
                }
                else
                {
                    Logger.Error("MAJLogicielleControle: Failed to load from ftp ");

                    // TODO: Ask the product owner
                }
            }
            catch (Exception e)
            {
                Logger.Error("MAJLogicielleControle: Exception download ftp " + e.Message + e.StackTrace);
            }
            return updated;
        }

        private static CourseCote containsCourseCote(int numReunion,int numCourse, DateTime dateReunion)
        {
            CourseCote courseCote = null;
            foreach (ReunionCote cote in ApplicationContext.COTES.ReunionCotes)
            {
                if (cote.NumReunion.Equals(numReunion.ToString()) && cote.DateReunion.Equals(dateReunion))
                {
                    foreach(CourseCote cC in cote.CourseCotes)
                    {
                        if (cC.NumCourse.Equals(numCourse.ToString()))
                        {
                            courseCote = cC;
                            break;
                        }
                    }
                    break;
                }
            }
            return courseCote;
        }
        private static ProduitCote ProduitCote(string codeProduit, CourseCote courseCote)
        {
            ProduitCote produitCote = null;
            if(courseCote != null)
            {
                foreach(ProduitCote pC in courseCote.ProduitCotes)
                {
                    if (pC.Code.Equals(codeProduit))
                    {
                        produitCote = pC;
                        break;
                    }
                }
            }
            return produitCote;
        }

        public static Course getCourse(int numReunion, int numCourse,DateTime dateReunion)
        {
            return ApplicationContext.SOREC_DATA_OFFRE.GetCourseByNumReunionAndNumCourse(dateReunion, numReunion, numCourse);
        }
        
        public static string getCotesFromGAG(string str, int start, int end, ProduitCote coteprGAG, Course course)
        {
            //Course course = getCourse(numReunion, numCourse);
            List<String> listeNonPartantsCourse = getNonPartants(course);
            List<String> listePartantsCourse = getPratants(course);
            int counter = 0;
            while (start < end)
            {
                if (counter < 3 && coteprGAG != null && coteprGAG.CombinaisonCotes != null && coteprGAG.CombinaisonCotes.Count > 0)
                {
                    int index = rand.Next(coteprGAG.CombinaisonCotes.Count);
                    string cheval = coteprGAG.CombinaisonCotes.ToArray()[index].CombPartants;
                    if (!str.Contains(cheval) && !listeNonPartantsCourse.Contains(cheval) && listePartantsCourse.Contains(cheval))
                    {
                        if (start == end)
                        {
                            str = str + cheval;
                        }
                        else
                        {
                            str = str + cheval + " ";
                        }
                        start++;
                    }
                    counter++;
                }
                else
                {
                    int index = rand.Next(course.ListeHorses.Count);
                    if (index < course.ListePartant.Count)
                    {
                        int cheval = course.ListePartant[index].NumPartant;
                        if (!str.Contains(cheval.ToString()))
                        {
                            if (start == end)
                            {
                                str = str + cheval;
                            }
                            else
                            {
                                str = str + cheval + " ";
                            }
                            start++;
                        }
                    }
                }
            }
            return str;
        }

        private static List<string> getNonPartants(Course course)
        {
            return course.ListeNonPartant.Select(nPart => nPart.NumPartant.ToString()).ToList();
        }
        private static List<String> getPratants(Course course)
        {
            return course.ListePartant.Select(nPart => nPart.NumPartant.ToString()).ToList();
        }


        public static string getCotesFromListePartant(string str, int start, int end, int numReunion, int numCourse,Course course)
        {
            ApplicationContext.Logger.Info("Get cheval From ListPartant C:" + numReunion + " ,R:" + numCourse);
            while (start < end)
            {
                int index = rand.Next(course.ListePartant.Count);
                int cheval = course.ListePartant[index].NumPartant;
                if (!str.Contains(cheval.ToString()))
                {
                    if (start == end)
                    {
                        str = str + cheval;

                    }
                    else
                    {
                        str = str + cheval + " ";
                    }
                    start++;
                }
            }
            return str;
        }


       static Random rand = new Random();
        public static string getExpressDesignation(Formulation formulation, int numReunion, int numCourse,DateTime dateReunion)
        {
            ApplicationContext.Logger.Info("Get Express Designation");
            string str = "";
            char[] delimiters = { ' ' };
            CourseCote cote = containsCourseCote(numReunion, numCourse, dateReunion);
            string[] partant = formulation.Designation.Split(delimiters);
            Course course = getCourse(numReunion, numCourse, dateReunion);
            List<String> listeNonPartantsCourse = getNonPartants(course);
            List<String> listePartantsCourse = getPratants(course);
            // cote Dispo
            if (cote != null)
            {
                ApplicationContext.Logger.Info(" cote Course Dispo  ");
                ProduitCote cotepr = ProduitCote(formulation.Produit.CodeProduit, cote);
                // Produit disponible dans Cote
                if (cotepr != null)
                {
                    ApplicationContext.Logger.Info("cotes pour " + cotepr.Code + " est dispo");
                    // Nombre Chevaux Express == Nombre Base Produit
                    if (partant.Length == formulation.Produit.NombreBase)
                    {
                        int partantRestCount = formulation.Produit.NombreBase;
                        int mIndex = rand.Next(cotepr.CombinaisonCotes.Count);

                        string[] partantsCombinaison = cotepr.CombinaisonCotes.ToArray()[mIndex].CombPartants.Split(delimiters);

                        //Get the cote combinaison if contains NonPartants after offer updated we remove the NonParatns from the combinaison geted and generate new Random Partant from listePartant of the course
                        foreach (string partantCombinaison in partantsCombinaison)
                        {
                            if (!listeNonPartantsCourse.Contains(partantCombinaison) && listePartantsCourse.Contains(partantCombinaison))
                            {
                                str += partantCombinaison + " ";
                                partantRestCount--;
                            }
                        }
                        
                        if ( partantRestCount != 0 )
                        {
                            int start = formulation.Produit.NombreBase - partantRestCount;
                            int end = formulation.Produit.NombreBase;
                            str = getCotesFromListePartant(str, start, end, numReunion, numCourse,course);
                        }
                        ApplicationContext.Logger.Info(" Express Final Designation : "+str);
                        return str;
                              
                    }
                    else if (partant.Length > formulation.Produit.NombreBase) { }
                    {

                        int mIndex = rand.Next(cotepr.CombinaisonCotes.Count);
                        str = "";
                        int partantRestCount = formulation.Produit.NombreBase;
                        string[] partantsCombinaison = cotepr.CombinaisonCotes.ToArray()[mIndex].CombPartants.Split(delimiters);

                        foreach (string partantCombinaison in partantsCombinaison)
                        {
                            if (!listeNonPartantsCourse.Contains(partantCombinaison) && listePartantsCourse.Contains(partantCombinaison))
                            {
                                str += partantCombinaison + " ";
                                partantRestCount--;
                            }
                        }

                        // On Complete par GAG 
                        int start = formulation.Produit.NombreBase - partantRestCount;
                        int end = partant.Length;

                        ProduitCote coteprGAG = ProduitCote("GAG", cote);
                        if (coteprGAG != null)
                        {
                            if(partantRestCount == formulation.Produit.NombreBase) str += " ";
                            str = getCotesFromGAG(str, start, end, coteprGAG,course);
                        }
                        else
                        {
                            if (partantRestCount == formulation.Produit.NombreBase) str += " ";
                            str = getCotesFromListePartant(str, start, end, numReunion, numCourse,course);
                        }
                    }
                }
                // cote course Existe ms Non encore Cote Produit 
                else
                {

                    int start = 0;
                    int end = partant.Length;
                    ProduitCote coteprGAG = ProduitCote("GAG", cote);
                    if (coteprGAG != null)
                    {
                        str = getCotesFromGAG(str, start, end, coteprGAG,course);
                    }
                    else
                    {
                        ApplicationContext.Logger.Info(" cote GAG Non dispo ");
                        str = getCotesFromListePartant(str, start, end, numReunion, numCourse,course);
                    }
                }
            }
            // Pas dispo ... Lance Random
            else
            {
                ApplicationContext.Logger.Info(" Pas de cote Course    ");
                int start = 0;
                int end = partant.Length;
                //CoteProduitItemModel coteprGAG = ProduitCote("GAG", cote);
                ProduitCote coteprGAG = ProduitCote("GAG", cote);
                if (coteprGAG != null)
                {
                    str = getCotesFromGAG(str, start, end, coteprGAG, course);
                }
                else
                {
                    str = getCotesFromListePartant(str, start, end, numReunion, numCourse, course);
                }

            }
            ApplicationContext.Logger.Info(" Express Final Designation : " + str);
            return str;
        }


        public static string getChevalExpressDesignation(Formulation formulation, int numReunion, int numCourse, DateTime dateReunion)
        {
            ApplicationContext.Logger.Info("Get cheval express designation");
            CourseCote cote = containsCourseCote(numReunion, numCourse, dateReunion);
            listSindexes.Clear();

            if (cote != null)
            {
                ProduitCote coteprGAG = ProduitCote("GAG", cote);
                if(coteprGAG != null)
                {
                    //Cote product exist. get cheval from cote product
                    for (int i = 0; i < formulation.Designation.Length; i++)
                    {
                        if (formulation.Designation[i] == 'S')
                        {
                            string newCheval = getCoteFromGagChevalExpress(formulation.Designation, numReunion, numCourse, coteprGAG, dateReunion);
                            formulation.Designation = formulation.Designation.Remove(i, 1).Insert(i, newCheval);
                            listSindexes.Add(newCheval.Length >= 2 ? i + 1 : i);
                        }
                    }
                }
                else
                {
                    //Cote exist but cote product not exist. get cheval from List partant
                    for (int i = 0; i < formulation.Designation.Length; i++)
                    {
                        if (formulation.Designation[i] == 'S')
                        {
                            string newCheval = getChevalExpressFromCoursePartant(formulation.Designation, numReunion, numCourse, dateReunion);
                            formulation.Designation = formulation.Designation.Remove(i, 1).Insert(i, newCheval);
                            listSindexes.Add(newCheval.Length >= 2 ? i + 1 : i);
                        }
                    }
                }
            }
            else
            {
                //Cote not exist. get cheval from List partant
                for (int i = 0; i < formulation.Designation.Length; i++)
                {
                    if (formulation.Designation[i] == 'S')
                    {
                        string newCheval = getChevalExpressFromCoursePartant(formulation.Designation, numReunion, numCourse, dateReunion);
                        formulation.Designation = formulation.Designation.Remove(i, 1).Insert(i, newCheval);
                        listSindexes.Add(newCheval.Length >= 2 ? i + 1 : i);
                    }
                }
            }
            ApplicationContext.Logger.Info("cheval express final designation"+formulation.Designation);
            
            return formulation.Designation;
        }

        public static string getCoteFromGagChevalExpress(string str, int numReunion, int numCourse, ProduitCote coteprGAG,DateTime dateReunion)
        {
            int counter = 0;
            Course course = getCourse(numReunion, numCourse, dateReunion);
            List<String> listeNonPartantsCourse = getNonPartants(course);
            List<String> listePartantsCourse = getPratants(course);

            ApplicationContext.Logger.Info("Getting cheval express from GAG...");
            while (counter < 3)
            {
                int index = rand.Next(coteprGAG.CombinaisonCotes.Count);
                string cheval = coteprGAG.CombinaisonCotes.ToArray()[index].CombPartants;
                if (!str.Contains(cheval) && !listeNonPartantsCourse.Contains(cheval) && listePartantsCourse.Contains(cheval))
                    return cheval;
                counter++;
            }
            return getChevalExpressFromCoursePartant(str, numReunion, numCourse, dateReunion);
        }

        public static string getChevalExpressFromCoursePartant(string str, int numReunion, int numCourse, DateTime dateReunion)
        {
            ApplicationContext.Logger.Info("Getting cheval express from Course...");
            Course course = getCourse(numReunion, numCourse, dateReunion);
            while (true)
            {
                int index = rand.Next(course.ListeHorses.Count);
                if (index < course.ListePartant.Count)
                {
                    int cheval = course.ListePartant[index].NumPartant;
                    if (!str.Contains(cheval.ToString()))
                    {
                        return cheval.ToString();
                    }
                }
            }
        }
        public static TLVHandler Annul_paiement_sys_controle()
        {
            TLVHandler pari_annul_ticketHandler = new TLVHandler();
            if (ApplicationContext.LAST_TICKET_INFO != null)
            {
                TLVHandler pari_annul_content_ticketHandler = new TLVHandler();
                pari_annul_content_ticketHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_PARI_DATE_REUNION, ApplicationContext.LAST_TICKET_INFO.DateReunion.ToString("yyyy-MM-dd"));
                pari_annul_content_ticketHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_PARI_NUMERO_TICKET, ApplicationContext.LAST_TICKET_INFO.IdTicket);
                pari_annul_content_ticketHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_PARI_NUMERO_REUNION, ApplicationContext.LAST_TICKET_INFO.NumReunion + "");
                pari_annul_content_ticketHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_PARI_NUMERO_COURSE, ApplicationContext.LAST_TICKET_INFO.NumCourse + "");
                pari_annul_ticketHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_PARI, pari_annul_content_ticketHandler);
            }
            if (ApplicationContext.LAST_VOUCHER_INFO != null)
            {
                TLVHandler pari_annul_content_voucherHandler = new TLVHandler();
                pari_annul_content_voucherHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_VOUCHER_DATE_SESSION, ApplicationContext.LAST_VOUCHER_INFO.DateSession.ToString("yyyy-MM-dd"));
                pari_annul_content_voucherHandler.addASCII(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_VOUCHER_NUMERO_TICKET, ApplicationContext.LAST_VOUCHER_INFO.IdVoucher);
                pari_annul_ticketHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME_VOUCHER, pari_annul_content_voucherHandler);
            }
            return pari_annul_ticketHandler;
        }
    }
}
