using System;
using System.Windows.Forms;
using sorec_gamma.IHMs;
using sorec_gamma.modules.ModulePari;
using System.Threading;
using sorec_gamma.modules.UTILS;
using System.Drawing;
using System.Collections.Generic;
using System.Media;
using calipso2020;
using sorec_gamma.modules.ModuleAdministration;
using sorec_gamma.modules.ModuleAdministration.Controls;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.TLV;
using log4net;
using log4net.Config;
using System.Collections.Concurrent;
using sorec_gamma.IHMs.ComposantsGraphique;
using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.ModuleDetailClient.model;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.ModuleSocket;
using static sorec_gamma.modules.UTILS.MonitorUtils;
using CyUSB;
using System.IO;
using calipso2020.Devices;
using sorec_gamma.modules.ModulePari.Model;

namespace sorec_gamma
{
    static class ApplicationContext
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// 
        // ****************************************************
        public static string SOREC_DATA_VERSION_LOG = "2222";
        public static string SOREC_DATA_ENV = "";
        public static readonly bool develop = true;
        // ****************************************************

        public static ConcurrentJournal JOURNAL;
        public static List<JournalLite> BJS = new List<JournalLite>();

        public static string IP = "";
        public static bool IsAutGain { get; internal set; }
        public static bool IsAutJeu { get; internal set; }
        public static readonly ILog Logger = LogManager.GetLogger(typeof(ApplicationContext));

        public static bool isVersionAuthorized = true;
        public static bool isAuthenticated = false;

        public static Thread majThread = null;
        public static volatile bool IsNetworkOnline;

        public static USBDeviceList UsbDeviceList;

        // SOREC DATA 
        public static Offre SOREC_DATA_OFFRE;
        public static volatile int CURRENT_NUMOFFRE_VERSION = 0;
        public static int COTE_CURRENT_VERSION = 0;
        public static ConcurrentBag<TLVTags> SOREC_DATA_COTES = new ConcurrentBag<TLVTags>();
        public static Cotes COTES = new Cotes();
        public static Rapports RAPPORTS = new Rapports();
        public static Signals SIGNALS = new Signals();

        public static Attributaire SOREC_DATA_ATTRIBUTAIRE;
        public static decimal SOREC_PARI_MISE_TOTALE = 0;
        public static decimal sTotal = 0;
        public static decimal TEMP_TOTAL_BEFORE_PRINT = 0;
        public static Imprimer imprimante;
        public static Scanner scanner;
        public static string CommunicationTerminal = "";
        public static string CommunicationTicket = "";
        public static DetailClient DetailClient = new DetailClient();

        public static EcranClient EcranClient;
        public static bool exit = false;
        public static bool _sendEcranClientPanne = true;

        private static Thread _uiCotesThread;
        public static EventHandler HealthConnEventHandler;

        public static Ticket LAST_TICKET_INFO = null;
        public static Voucher LAST_VOUCHER_INFO = null;

        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.Name = "Principal";
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            XmlConfigurator.Configure();
            Logger.Info("Start Gamma Application ");

            Application.ThreadException +=
                new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadingForm splash = new LoadingForm();
            _forms.Add(splash);
            splash.StartPosition = FormStartPosition.CenterScreen;

            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            Application.Run(splash);

            Logger.Info("Exit Gamma Application");
        }

        public static void InitPrinter()
        {
            imprimante = new Imprimer(UsbDeviceList);
        }

        public static void InitScanner()
        {
            scanner = new Scanner(UsbDeviceList);
        }

        public static bool IsScannerInitialized()
        {
            bool isSet = develop || (scanner != null && scanner.IsInitialized());
            Logger.Info("IsScannerInitialized: " + isSet);
            return isSet;
        }

        public static bool IsPrinterInitialized()
        {
            bool isSet = develop || (imprimante != null && imprimante.myDevice != null && imprimante.myConvoyeur != null);
            Logger.Info("IsPrinterInitialized: " + isSet);
            return isSet;
        }

        public static bool IsEcranClientInitialized()
        {
            return EcranClient != null && EcranClient.MyDevice != null;
        }

        private static bool initEcranClient()
        {
            if (!IsEcranClientInitialized())
            {
                EcranClient = new EcranClient(UsbDeviceList);
            }
            return IsEcranClientInitialized();
        }

        public static void LaunchCaissePreposeForm()
        {
            if (CaissePrepose == null)
            {
                CaissePrepose = new CaissePrepose();
                _forms.Add(CaissePrepose);
            }
            else
            {
                CaissePrepose.Initilize();
            }
            _navigateForms(CaissePrepose);
        }

        public static void LaunchReglageForm()
        {
            if (ReglagesForm == null)
            {
                ReglagesForm = new ReglagesForm();
                _forms.Add(ReglagesForm);
            }
            else
            {
                ReglagesForm.Initilize();
            }
            _navigateForms(ReglagesForm);
        }
        public static void LaunchAuthenticationForm(string msg, bool sound = true)
        {
            if (AuthentificationForm == null)
            {
                AuthentificationForm = new AuthentificationForm();
                _forms.Add(AuthentificationForm);
            }
            else
            {
                AuthentificationForm.Initialize(msg);
            }
            isAuthenticated = false;
            _navigateForms(AuthentificationForm);
            if (sound && File.Exists(@"Sons/Logout.wav"))
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Sons/Logout.wav");
                simpleSound.Play();
            }
        }

        public static void LaunchPrincipaleForm(bool sound, string initialMsg, int showEcranClient)
        {
            if (PrincipaleForm == null)
            {
                PrincipaleForm = new PrincipaleForm(initialMsg);
                _forms.Add(PrincipaleForm);
            }
            else
            {
                PrincipaleForm.Initialize(initialMsg, showEcranClient);
            }
            isAuthenticated = true;
            _navigateForms(PrincipaleForm);
            if (sound && File.Exists(@"Sons/Login.wav"))
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Sons/Login.wav");
                simpleSound.Play();
            }
        }

        public static void LaunchCaisseForm(int flagPdv)
        {
            if (CaisseForm == null)
            {
                CaisseForm = new CaisseForm(flagPdv);
                _forms.Add(CaisseForm);
            }
            else
            {
                CaisseForm.Initialize(flagPdv);
            }
            _navigateForms(CaisseForm);
        }
        public static void LaunchClientForm()
        {
            if (ClientForm == null)
            {
                ClientForm = new ClientForm();
                _forms.Add(ClientForm);
            }
            else
            {
                ClientForm.Initialize();
            }
            _navigateForms(ClientForm);
        }
        public static void LaunchCréationClientForm()
        {
            if (CréationClientForm == null)
            {
                CréationClientForm = new CréationClientForm();
                _forms.Add(CréationClientForm);
            }
            else
            {
                CréationClientForm.Initialize();
            }
            _navigateForms(CréationClientForm);
        }

        public static void LaunchDepotCompteClientForm(string textCIN, string textNom, string textPrenom)
        {
            if (DepotCompteClientForm == null)
            {
                DepotCompteClientForm = new DepotCompteClientForm(textCIN, textNom, textPrenom);
                _forms.Add(DepotCompteClientForm);
            }
            else
            {
                DepotCompteClientForm.Initialize(textCIN, textNom, textPrenom);
            }
            _navigateForms(DepotCompteClientForm);
        }
        public static void LaunchGrosGainForm(string idConc, string idServeur, DateTime dateReunion, int numReunion, int numCourse, string hippo, string heure, int montant_Avance, bool isManuel)
        {
            if (GrosGainForm == null)
            {
                GrosGainForm = new GrosGainForm(idConc, idServeur, dateReunion, numReunion, numCourse, hippo, heure, montant_Avance, isManuel);
                _forms.Add(GrosGainForm);
            }
            else
            {
                GrosGainForm.Initialize(idConc, idServeur, dateReunion, numReunion, numCourse, hippo, heure, montant_Avance, isManuel);
            }
            _navigateForms(GrosGainForm);
        }

        public static void LaunchJournalForm()
        {
            if (JournalForm == null)
            {
                JournalForm = new JournalForm();
                _forms.Add(JournalForm);
            }
            else
            {
                JournalForm.Initialize();
            }
            _navigateForms(JournalForm);
        }

        public static void CloseSession(string msg)
        {
            if (isAuthenticated)
            {
                sTotal = 0;
                JournalUtils.saveJournalMessage("Suspension Guichet.");
                LaunchAuthenticationForm(msg);
            }
        }

        // ------------------------- Ecran client ----------------------
        public static void UpdateEcranClient(EcranClientModel ecranClientModel)
        {
            try
            {
                if (initEcranClient())
                {
                    using (Bitmap bmp = GraphicUtils.GetEcranClientBitmap(ecranClientModel))
                    {
                        _sendEcranClientPanne = EcranClient.afficherEcranClient(bmp);
                    }
                }
                else if (_sendEcranClientPanne)
                {
                    PanneControle.sendRequest(Panne.ProblemeEcranClient);
                    _sendEcranClientPanne = false;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Update Ecran client Exception: {0}, StackTrace: {1}", ex.Message, ex.StackTrace));
            }
        }

        // -------------------- Ecran cotes ----------------------
        public static MonitorInfo GetEcranCotesScreen()
        {
            List<MonitorInfo> actualScreens = GetActualScreens();
            MonitorInfo screen = null;
            foreach (MonitorInfo sc in actualScreens)
            {
                if (!sc.IsPrimary)
                {
                    screen = sc;
                    break;
                }
            }
            return screen;
        }
        public static void LaunchCotesUIThread()
        {
            _uiCotesThread = new Thread(() =>
            {
                bool sendEcranCotePanne = true;
                while (true)
                {
                    MonitorInfo secondScreen = GetEcranCotesScreen();
                    if (secondScreen == null && sendEcranCotePanne)
                    {
                        PanneControle.sendRequest(Panne.ProblemeEcranCote);
                        sendEcranCotePanne = false;
                    }
                    else if (secondScreen != null)
                    {
                        sendEcranCotePanne = true;
                        Thread coteApp = new Thread(() =>
                        {
                            CoteForm CoteForm = new CoteForm(secondScreen);
                            sendEcranCotePanne = true;
                            Application.EnableVisualStyles();
                            Application.Run(CoteForm);
                        });
                        coteApp.Priority = ThreadPriority.BelowNormal;
                        coteApp.Name = "AFFICHEUR_COTES";
                        coteApp.SetApartmentState(ApartmentState.STA);
                        coteApp.Start();
                        coteApp.Join();
                    }
                    Thread.Sleep(1 * 1000);
                    SIGNALS.ClearAll();
                    RAPPORTS.ClearAll();
                }
            });
            _uiCotesThread.Priority = ThreadPriority.BelowNormal;
            _uiCotesThread.Name = "UI_COTES";
            _uiCotesThread.Start();
        }

        //-----------------------------------------------------------------------------
        public static void InitWebSocketClient()
        {
            WebSocketClient.InitWSC();
        }

        //-----------------------------------------------------------------------------
        private static void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                if (_uiCotesThread != null)
                    _uiCotesThread.Abort();
                if (imprimante != null)
                    imprimante.Discounnect();
                if (scanner != null)
                    scanner.Disconnect();
            }
            catch { }
        }

        public static void QuitApplication()
        {
            EcranClientModel ecranClientModel = new EcranClientModel();
            ecranClientModel.SimpleMsg = "GUICHET FERMÉ";
            UpdateEcranClient(ecranClientModel);
            Application.Exit();
        }

        private static void _navigateForms(BaseForm first)
        {
            try
            {
                first.Show();
                foreach (BaseForm f in _forms)
                {
                    if (first != f && !f.IsDisposed && f.Visible)
                        f.HideForm();
                }
            }
            catch (Exception e)
            {
                Logger.Error(string.Format("_navigateForms: ", e.Message, e.StackTrace));
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex;
            if (e != null && e.Exception != null)
            {
                ex = e.Exception;
                Logger.Error(string.Format("Application_ThreadException: ", e.Exception.Message, e.Exception.StackTrace));
            }
            else
            {
                ex = new Exception("");
                Logger.Error("Application_ThreadException: null");
            }
            UnhandledExceptionForm unhandledExceptionForm = new UnhandledExceptionForm(ex);
            unhandledExceptionForm.ShowDialog();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = null;
            if (e != null && e.ExceptionObject != null)
            {
                ex = e.ExceptionObject as Exception;
            }
            if (ex != null)
            {
                Logger.Error(string.Format("CurrentDomain_UnhandledException: ", ex.Message, ex.StackTrace));
            }
            else
            {
                ex = new Exception("");
                Logger.Error("CurrentDomain_UnhandledException: null");
            }
            UnhandledExceptionForm unhandledExceptionForm = new UnhandledExceptionForm(ex);
            unhandledExceptionForm.ShowDialog();
        }

        public static CaissePrepose CaissePrepose;
        public static ReglagesForm ReglagesForm;
        public static AuthentificationForm AuthentificationForm;
        public static PrincipaleForm PrincipaleForm;
        public static CaisseForm CaisseForm;
        public static ClientForm ClientForm;
        public static CréationClientForm CréationClientForm;
        public static DepotCompteClientForm DepotCompteClientForm;
        public static GrosGainForm GrosGainForm;
        public static JournalForm JournalForm;
        private static HashSet<Form> _forms = new HashSet<Form>();

    }
}
