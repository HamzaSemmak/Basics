using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Controls;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.ModuleCote_rapport.controls;
using sorec_gamma.modules.ModuleCote_rapport.model;
using sorec_gamma.modules.ModuleScanner;
using System.Globalization;
using System.Threading;
using sorec_gamma.modules.UTILS;
using calipso2020.Model;
using System.Linq;
using sorec_gamma.IHMs.ComposantsGraphique;
using System.ComponentModel;
using System.Timers;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.ModuleDetailClient.model;
using sorec_gamma.modules.ModuleBJournal.Models;
using System.Text.RegularExpressions;
using System.Diagnostics;
using sorec_gamma.modules.ModuleEvents.SignauxEvents.controls;
using System.Text;
using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleEvents.helpers;

namespace sorec_gamma.IHMs
{
    public partial class PrincipaleForm : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(PrincipaleForm));
        private Reunion SelectedReunion;
        private Course SelectedCourse;
        private List<Reunion> lsReunion;
        private List<object> listeChoix;
        private int chevalReplaceWith;
        private int numHippo;
        private int replaceFlag = -1;
        private int iTypePari;
        private int iBase;
        private int montant = 0;
        private System.Windows.Forms.Timer _datetimeTimer;

        private bool champsTotalFlag = false;
        private bool champsReduitFlag = false;
        private bool champsXFlag = false;
        private bool formuleComplete = false;
        private int numCourse;
        private int ImprimerFlag = 1;
        private int flagVerrouReunion = 0;
        private int flagVerrouCourse = 0;
        private int nouveauClientFlag = 0;
        private int numPartant = 0;
        private List<int> listeRChoisis;
        private List<int> listeMultiplicateurs;
        private int permutation = 0;
        private int nbrVariableX;
        private int variablesX;
        private int nbrChevalJoué;
        private int Ordre;
        private int indiceReunion;
        private int indiceNbrFormulation = 0;
        private int indiceHashReunion = 0;
        private Reunion r;
        private Course myCourse;
        private Ticket ticket;
        private List<Produit> produits;
        private List<Produit> formulationProduits = new List<Produit>();
        private List<Formulation> lsformulation;
        private DiffusionOffreControl offreControl;
        private EnregistrementPariControl pariService;
        private ReglesPariControles pariControles;
        private int indiceCourse;
        private SortedDictionary<DateTime, List<Reunion>> hashReunion = new SortedDictionary<DateTime, List<Reunion>>();
        private int flagPLA = 0;
        private int flagGAG = 0;
        private int flagJUG = 0;
        private int flagJUP = 0;
        private int flagJUO = 0;
        private int flagSimpleIsValid = 1;
        private int flagJumeleIsValid = 1;
        private int flagMultiIsValid = 1;
        private int flagImprimerIsValid = 0;
        private Ticket lastformulation = new Ticket();
        private ScannerHandler scannerHandler = new ScannerHandler();
        private int nombreParisExpress = 1;
        private bool IsExpress = false;
        private bool isExpressImprimed;

        public string _ecranClientPrice = "";
        private string _ecranClientParis = "";
        private BackgroundWorker UpdateEcranClientBackgroundWorker;
        private bool isSimpleGSelected = false;
        private bool isSimplePSelected = false;
        private bool isJumeleGSelected = false;
        private bool isJumelePSelected = false;
        private bool isChevalExpress = false;
        private bool showButtonZExpress = false;
        private bool expressBtnClicked = false;
        private int first = 0;
        private Button selectedCourseBtn;
        private System.Timers.Timer _blinkMessageTimer;
        private bool blinkFlag = false;
        private System.Timers.Timer _blinkChevalTimer;
        private bool blinkChevalFlag = false;
        private int chexpressCount = 0;
        private bool isCourseClicked;
        private SignalAlertMsg signalAlertMsg;
        private int showEcranClient = 0;
        private string SimpleMsg = "";
        private Font errorMsgInitFont;
        private int indexOperationDc;
        private Font errorMsgGreen;
        private EcranClientModel _ecranClientModel;

        public static int distributionCount;
        public static int paimentCount;
        public static decimal paiement;
        public static int annulationCount;
        public static decimal annulation;
        private static decimal lastAnnulation;
        private static int lastAnnulationCount;
        private static decimal lastDistribution;
        public static decimal lastPaiement;
        private static int lastDistributionCount;
        private static int lastPaiementCount;
        public static decimal distribution;
        public static decimal lastStotal;

        private int ctrChExp = 0;


        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (UpdateEcranClientBackgroundWorker != null)
                {
                    if (UpdateEcranClientBackgroundWorker.IsBusy)
                        UpdateEcranClientBackgroundWorker.CancelAsync();
                    UpdateEcranClientBackgroundWorker.Dispose();
                }
                if (_datetimeTimer != null)
                    _datetimeTimer.Dispose();

            }
            base.Dispose(disposing);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }

        public PrincipaleForm(string initMsg = "NOUVEAU CLIENT")
        {
            InitializeComponent();
            Initialize(initMsg, 0);
        }

        public void Initialize(string initialMsg, int showEcranClient)
        {
            if (!ApplicationContext.IsScannerInitialized())
            {
                ApplicationContext.InitScanner();
            }
            else if (!ApplicationContext.develop)
            {
                ApplicationContext.scanner.Connect();
            }
            errorMsgInitFont = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            errorMsgGreen = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);

            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
            _datetimeTimer = new System.Windows.Forms.Timer();
            _datetimeTimer.Enabled = true;
            _datetimeTimer.Interval = 1000;
            _datetimeTimer.Tick += new EventHandler(_dateTimeTick);

            _blinkMessageTimer = new System.Timers.Timer(1000)
            {
                Enabled = false
            };
            _blinkMessageTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            _blinkChevalTimer = new System.Timers.Timer(1000)
            {
                Enabled = false
            };
            _blinkChevalTimer.Elapsed += new ElapsedEventHandler(BlinkChevalBtn);

            flowLayoutPanel1.BackgroundImage = Properties.Resources.lblMessageInfo;

            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
           
            blinkFlag = true;
            _ecranClientModel = new EcranClientModel();
            SelectedReunion = null;
            SelectedCourse = null;
            chevalReplaceWith = 0;
            numHippo = 0;
            replaceFlag = -1;
            iTypePari = 0;
            iBase = 0;
            montant = 0;
            champsTotalFlag = false;
            champsReduitFlag = false;
            champsXFlag = false;
            formuleComplete = false;
            ImprimerFlag = 1;
            flagVerrouReunion = 0;
            flagVerrouCourse = 0;
            nouveauClientFlag = 0;
            numPartant = 0;
            permutation = 0;
            nbrVariableX = 0;
            variablesX = 0;
            nbrChevalJoué = 0;
            Ordre = 0;
            indiceReunion = 0;
            indiceNbrFormulation = 0;
            r = null;
            myCourse = null;
            ticket = null;
            produits = null;
            lsformulation = null;
            indiceCourse = -1;
            hashReunion = null;
            flagPLA = 0;
            flagGAG = 0;
            flagJUG = 0;
            flagJUP = 0;
            flagJUO = 0;
            flagSimpleIsValid = 1;
            flagJumeleIsValid = 1;
            flagMultiIsValid = 1;
            flagImprimerIsValid = 0;
            lastformulation = new Ticket();
            scannerHandler = new ScannerHandler();

            nombreParisExpress = 1;
            IsExpress = false;
            isExpressImprimed = false;

            isSimpleGSelected = false;
            isSimplePSelected = false;
            isJumeleGSelected = false;
            isJumelePSelected = false;
            _ecranClientPrice = "";
            _ecranClientParis = "";
            pariControles = new ReglesPariControles();
            buttonReunions.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            lsReunion = new List<Reunion>();
            lsformulation = new List<Formulation>();
            formulationProduits = new List<Produit>();
            listeChoix = new List<object>();
            listeRChoisis = new List<int>();
            listeMultiplicateurs = new List<int>();
            indiceHashReunion = 0;
            chevalReplaceWith = 0;
            iTypePari = 0;
            numHippo = 1;
            numCourse = 1;
            first = 0;
            indiceReunion = 0;
            indiceCourse = -1;
            hashReunion = new SortedDictionary<DateTime, List<Reunion>>();
            ticket = new Ticket();
            offreControl = new DiffusionOffreControl();
            pariService = new EnregistrementPariControl();
            lblCaisse.Text = "Guichet " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            textBoxMesssage.Text = " HELLO !!  "; textBoxMesssage.TextAlign = HorizontalAlignment.Center;
            panel18.Hide();
            panel19.Hide(); 
            ReLoadView(new DataTerminalIndicesHolder());
            ApplicationContext.sTotal -= ApplicationContext.TEMP_TOTAL_BEFORE_PRINT;
            ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
            showPrice(0);
            this.showEcranClient = showEcranClient;
            if (ApplicationContext.scanner != null)
            {
                ApplicationContext.scanner.ScanResultProcess -= scan_ProcessCompleted;
                ApplicationContext.scanner.ScanResultProcess += scan_ProcessCompleted;
            }

            if (!ApplicationContext.CommunicationTerminal.Equals(""))
            {
                textBoxMesssage.Text = ApplicationContext.CommunicationTerminal;
                textBoxMesssage.ForeColor = Color.OrangeRed;
            }
            richTextBox3.Clear();
            richTextBox3.Refresh();
            buttonReunions.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            buttonCourses.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            panelCheval.Hide();
            panelClient.Hide();

            UpdateValiderBtn("Valider", false);
            configUpdateEcranClientBackgroundWorker();
            if (Created) showErrorMessage(initialMsg, Color.Green);
            lastFormulationGender = "";
            count = 0;
            chexpressCount = 0;
            isCourseClicked = false;
            signalAlertMsg = null;
            SimpleMsg = "BIENVENUE";
            bisBtn.Hide();
            if (ApplicationContext.IsAutJeu)
            {
                buttonZGestClients.Enabled = true;
                buttonZGestClients.BackgroundImage = Properties.Resources.BtnEnjeu_Desel;
            }
            else
            {
                buttonZGestClients.Enabled = false;
                buttonZGestClients.BackgroundImage = Properties.Resources.BtnEnjeu_Inactif;
            }
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        private void configUpdateEcranClientBackgroundWorker()
        {
            UpdateEcranClientBackgroundWorker = new BackgroundWorker();
            UpdateEcranClientBackgroundWorker.DoWork += updateEcranClientBackgroundWorker_DoWork;
            UpdateEcranClientBackgroundWorker.WorkerSupportsCancellation = true;
            UpdateEcranClientBackgroundWorker.RunWorkerAsync();
        }
        private void unsubscribeUpdateEcranClientBackgroundWorker()
        {
            if (UpdateEcranClientBackgroundWorker != null)
            {
                if (UpdateEcranClientBackgroundWorker.IsBusy)
                    UpdateEcranClientBackgroundWorker.CancelAsync();
                UpdateEcranClientBackgroundWorker.DoWork -= updateEcranClientBackgroundWorker_DoWork;
            }
        }

        private void updateEcranClientBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (UpdateEcranClientBackgroundWorker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    EcranClientModel ecranClientModel = new EcranClientModel();
                    switch (showEcranClient)
                    {
                        case 0:
                            ecranClientModel.MessageType = MessageType.Simple;
                            ecranClientModel.SimpleMsg = SimpleMsg;
                            break;
                        case 1:
                            //Show Pari Detail
                            if(SelectedReunion != null)
                            {
                                ecranClientModel.MessageType = MessageType.TicketDetail;
                                ecranClientModel.Date = SelectedReunion.DateReunion.ToString("dddd, dd MMMM yyyy");
                                ecranClientModel.Hippo = SelectedReunion.LibReunion;
                                ecranClientModel.Reunion = SelectedReunion.NumReunion.ToString();
                                ecranClientModel.Course = SelectedCourse.NumCoursePmu.ToString();
                                ecranClientModel.Paris = _ecranClientParis;
                                ecranClientModel.Formulation = getPari();
                                ecranClientModel.Price = _ecranClientPrice;
                            }
                            break;
                        case 2:
                            //Show client detail et msg terminal
                            ecranClientModel.MessageType = MessageType.ClientDetail;
                            ecranClientModel.Distribution = lastDistribution;
                            ecranClientModel.DistributionCount = lastDistributionCount;
                            ecranClientModel.Paiement = lastPaiement;
                            ecranClientModel.PaiementCount = lastPaiementCount;
                            ecranClientModel.Annulation = lastAnnulation;
                            ecranClientModel.AnnulationCount = lastAnnulationCount;
                            break;
                        default:
                            ecranClientModel.MessageType = MessageType.Simple;
                            ecranClientModel.SimpleMsg = "BIENVENUE";
                            break;
                    }
                    if (!ecranClientModel.Equals(_ecranClientModel))
                    {
                        ApplicationContext.UpdateEcranClient(ecranClientModel);
                        _ecranClientModel = ecranClientModel;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(string.Format("Update Ecran client Exception: {0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
                }
                finally
                {
                    Thread.Sleep(450);
                }

            }
        }

        private void UpdateHashReunionFromSorec_Offre()
        {
            var offre = ApplicationContext.SOREC_DATA_OFFRE;
            hashReunion.Clear();
            if (offre != null && offre.ListeReunion.Count > 0)
            {
                offre.ListeReunion.Sort((x, y) => x.NumReunion.CompareTo(y.NumReunion));
                lsReunion = offre.ListeReunion;
                foreach (Reunion reunion1 in lsReunion)
                {
                    if(reunion1 != null && reunion1.ListeCourse.Count>1)
                        reunion1.ListeCourse.Sort((x, y) => x.NumCoursePmu.CompareTo(y.NumCoursePmu));
                    if (hashReunion.ContainsKey(reunion1.DateReunion))
                    {
                        List<Reunion> newListe = hashReunion[reunion1.DateReunion];
                        newListe.Add(reunion1);
                        hashReunion[reunion1.DateReunion] = newListe;
                    }
                    else if (!hashReunion.ContainsKey(reunion1.DateReunion))
                    {
                        List<Reunion> newList = new List<Reunion>();
                        newList.Add(reunion1);
                        hashReunion.Add(reunion1.DateReunion, newList);
                    }
                }
            }
        }
        private void ShowHashReunionButtonsChanger()
        {
            if (hashReunion.Count > 1)
            {
                pictureBox3.BackgroundImage = indiceHashReunion == 0 ? Properties.Resources.BtnReunionPrecPetit_Inactif : Properties.Resources.BtnReunionPrecPetit_Desel;
                pictureBox4.BackgroundImage = indiceHashReunion == hashReunion.Count - 1 ? Properties.Resources.BtnReunionSuivPetit_Inactif : Properties.Resources.BtnReunionSuivPetit_Desel;
                pictureBox3.Enabled = indiceHashReunion > 0;
                pictureBox4.Enabled = indiceHashReunion < hashReunion.Count - 1;
                pictureBox3.Show();
                pictureBox4.Show();
                labelCourseList.Hide();
            }
            else
            {
                pictureBox3.Hide();
                pictureBox4.Hide();
                labelCourseList.Show();
            }
        }

        private void UpdateAllReunionsButton(List<Reunion> reunions)
        {
            int i = 1;
            Button RunionButton;
            foreach (Reunion reunion in lsReunion)
            {
                string nameReunion = reunion.LibReunion;
                int nbrCourses = reunion.ListeCourse.Count;
                Course course = getFirstCourse(reunion);
                string s = getReunionInfos(reunion, course);
                RunionButton = GetReunionButton(i);
                if (RunionButton == null)
                {
                    Logger.Warn(string.Format("UpdateReunions: unrecognized Reunion button {0} ", i));
                }
                else
                {
                    RunionButton.Text = s;
                    RunionButton.Show();
                }
                i++;
            }
            for (int j = i; j <= 5; j++)
            {
                RunionButton = GetReunionButton(j);
                if (RunionButton != null)
                {
                    RunionButton.Hide();
                }
            }
        }

        public Button GetReunionButton(int numReunion)
        {
            switch (numReunion)
            {
                case 1:
                    return buttonZ1;
                case 2:
                    return buttonZ2;
                case 3:
                    return buttonZ3;
                case 4:
                    return buttonZ4;
                case 5:
                    return buttonZ5;
                default:
                    return null;
            }
        }

        private void UpdateButtonScrollHorsesPanel(Course course)
        {
            if (course == null)
                return;
            button10.Enabled = false;
            button10.BackgroundImage = Properties.Resources.BtnReunionPrec_Inactif;
            if (course.ListeHorses.Count > 8)
            {
                button14.Enabled = true;
                button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Desel;
            }
            else
            {
                button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Inactif;
                button14.Enabled = false;
            }

        }
        private void UpdateCourseButton(Course course)
        {
            if (selectedCourseBtn != null && selectedCourseBtn.BackColor.Equals(Color.Orange))
            {
                selectedCourseBtn.BackColor = Color.Navy;
                selectedCourseBtn.BackgroundImage = Properties.Resources.bouton_Course_Vente;
                selectedCourseBtn.ForeColor = Color.White;
            }
            Button sCourseBtn = GetSelectedCourseButton(numCourse);
            if (sCourseBtn != null)
                selectedCourseBtn = sCourseBtn;
            else
            {
                Logger.Error("Liste des courses dépasse 12: " + numCourse);
            }
            if (selectedCourseBtn.BackColor.Equals(Color.Navy) || selectedCourseBtn.BackColor.Equals(Color.Orange))
            {
                selectedCourseBtn.BackColor = Color.Orange;
                selectedCourseBtn.BackgroundImage = Properties.Resources.bouton_Course_Sel;
                selectedCourseBtn.FlatAppearance.BorderColor = Color.DarkRed;
            }
            if (selectedCourseBtn.BackColor.Equals(Color.Gray))
            {
                HideHorses();
                HideProduits();
            }
            if (course.IsRapportDisponible)
            {
                button15.Enabled = true;
                button15.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                button15.BackColor = Color.DarkCyan;
                button15.ForeColor = Color.White;

            }
            else
            {
                button15.Enabled = false;
                button15.BackgroundImage = Properties.Resources.BtnOption2_Inactif;
            }
        }
        private void HideAndResetExpress()
        {
            panel18.Hide();
            panel19.Hide();
            IsExpress = false;
            isChevalExpress = false;
            chexpressCount = 0;
            nombreParisExpress = 1;
            buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
            btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
        }
        private void ClearAndResetTicketInfo()
        {
            ticket.ListeFormulation.Clear();
            lastFormulationGender = "";
            count = 0;
            formulationProduits.Clear();
            lastformulation.ListeFormulation.Clear();
        }

        public void ReLoadView(DataTerminalIndicesHolder indices)
        {
            UpdateHashReunionFromSorec_Offre();
            if (hashReunion.Count > 0)
            {
                //int index = hashReunion.GetIndex(pair => pair.Equals(indices.DateLastHashReunionSelected));
                int index = SDictionnaryUtils<DateTime,List<Reunion>>.getIndexItem(hashReunion, indices.DateLastHashReunionSelected);
                indiceHashReunion = index == -1 ? 0 : index;
                lsReunion = SDictionnaryUtils < DateTime, List<Reunion>>.getValueByIndex(hashReunion,indiceHashReunion);
                if (lsReunion != null && lsReunion.Count > 0)
                {
                    Reunion toCompre = new Reunion(indices.LastDateReunionSelected, indices.LastNumReunionSelectd);
                    int indexReunion = lsReunion.FindIndex(r => r.isSame(toCompre));
                    r = lsReunion[indexReunion == -1 ? 0 : indexReunion];
                    numHippo = indexReunion == -1 ? 1 : indexReunion + 1;
                    UpdateAllReunionsButton(lsReunion);
                }
            }
            ShowHashReunionButtonsChanger();
            if (r != null)
            {
                SelectedReunion = r;
                UpdateReunionButton(r);
                Course toCompare = new Course(indices.LastNumReunionSelectd, indices.LastCourseSelected);
                int indexCourse = r.ListeCourse.FindIndex(c => c.isSame(toCompare));
                myCourse = indexCourse == -1 ? getFirstCourse(r) : r.ListeCourse[indexCourse];
                numCourse = indexCourse == -1 ? r.ListeCourse.FindIndex(c => c.Equals(myCourse)) + 1 : indexCourse + 1;
                produits = myCourse.ListeProduit;
                showCourses(r);
                if(myCourse != null)
                {
                    SelectedCourse = myCourse;
                    lblcourse.Text = "Réunion " + r.NumReunion + " -- Course " + myCourse.NumCoursePmu + "-- " + myCourse.HeureDepartCourse.ToString("HH:mm");
                    richTextBox1.Text = string.Format("{0}\n{1} {2}\nRéunion {3}- Course {4} \r\n", r.LibReunion, r.DateReunion.ToString("dddd, dd MMMM yyyy"), myCourse.HeureDepartCourse.ToString("HH:mm"), r.NumReunion, myCourse.NumCoursePmu);
                    UpdateCourseButton(myCourse);
                    switch (indices.TypeReload)
                    {
                        case TypeReload.INIT:
                            ResetView();
                            break;
                        case TypeReload.SIGNAL:
                            CourseData lastCourseInfoPushed = SignalEventControl.CourseInfoPushed;
                            if (myCourse.NumCoursePmu == lastCourseInfoPushed.NumeroCourse
                                && r.NumReunion == lastCourseInfoPushed.NumeroReunion
                                && r.DateReunion == lastCourseInfoPushed.DateReunion
                                && !lastCourseInfoPushed.StatusCourse.Equals("PDE")
                                && !lastCourseInfoPushed.StatusCourse.Equals("APR"))
                            {
                                if (ApplicationContext.TEMP_TOTAL_BEFORE_PRINT != 0)
                                {
                                    ApplicationContext.sTotal -= ApplicationContext.TEMP_TOTAL_BEFORE_PRINT;
                                    ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
                                }
                                HideAndResetExpress();
                                ClearAndResetTicketInfo();
                                ClearDesignationsMode();
                                showPrice(0);
                                UpdateValiderBtn("Valider", false);
                                bisBtn.Hide();
                                clearData();
                            }
                            else
                            {
                                getButtonsControls();
                                return;
                            }
                            break;
                        default:
                            ResetView();
                            break;
                    }
                    first = 0;
                    UpdateButtonScrollHorsesPanel(myCourse);
                    showButtonZExpress = false;
                    expressBtnClicked = false;
                    clearMessageZone();
                    ResetProductsFlags();
                    string s = getReunionInfos(r, myCourse);
                    switch (numHippo)
                    {
                        case 1: buttonZ1.Text = s; break;
                        case 2: buttonZ2.Text = s; break;
                        case 3: buttonZ3.Text = s; break;
                        case 4: buttonZ4.Text = s; break;
                        case 5: buttonZ5.Text = s; break;
                    }
                    EnableCourseButton(true);
                    lblRC.Text = "➤ " + r.LibReunion + "- Course" + myCourse.NumCoursePmu;
                    lblDépart.Text = "Départ prévu à " + myCourse.HeureDepartCourse.Hour + "h" + myCourse.HeureDepartCourse.Minute;
                    showHorses(myCourse);
                    showProduits(myCourse);
                    clearMultiplicators();
                }
            }
        }

        private void ResetProductsFlags()
        {
            flagPLA = 0; flagGAG = 0; flagJUG = 0; flagJUP = 0; flagJUO = 0;
        }
        private void ResetView()
        {
            // Reset All Button View to init Value
            buttonReunions.BackgroundImage = Properties.Resources.BtnDesignation_Sel;
            buttonClient.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            buttonCourses.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            panelCheval.Hide();
            panelClient.Hide();
            flagVerrouCourse = 0;
            flagVerrouReunion = 0;
            button22.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            button21.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            ClearAndResetTicketInfo();
            HideAndResetExpress();
            ClearDesignationsMode();
        }

        private void HideProduits()
        {
            buttonZTiercé.Hide();
            buttonZQuarté.Hide();
            buttonZQuartéPlus.Hide();
            buttonZQuintéPlus.Hide();
            buttonZExpress.Hide();
            btnCHExpress.Hide();
            buttonZJGP.Hide();
            buttonZJP.Hide();
            buttonZJG.Hide();
            buttonZSGP.Hide();
            buttonZSG.Hide();
            buttonZSP.Hide();
            buttonZTrio.Hide();
        }
        private void HideCourses()
        {
            for (int i = 1; i < 17; i++)
            {
                String name = "buttonZC" + i.ToString();
                foreach (Control control in this.panel1.Controls)
                {
                    if (control is Button && control.Name.Equals(name))
                    {
                        control.Hide();
                    }
                }

            }
        }
        private void HideHorses()
        {
            for (int i = 1; i < 21; i++)
            {
                String name = "btnZCH" + i.ToString();
                foreach (Control control in this.panel21.Controls)
                {
                    if (control is Button && control.Name.Equals(name))
                    {
                        control.BackColor = Color.Gray;
                        control.ForeColor = Color.White;
                        //control.Hide();
                    }
                }
            }
        }

        private void ControleExpresse(Course course)
        {
            if (course == null || course.ListeProduit == null)
                return;
            foreach (Produit produit in course.ListeProduit)
            {
                if (produit.ChevalExpress)
                {
                    btnCHExpress.Show();
                    buttonZExpress.Show();
                    break;
                }
                else
                {
                    btnCHExpress.Hide();
                    buttonZExpress.Hide();
                }
            }
        }

        private void showProduits(Course course)
        {
            if (course == null || course.ListeProduit == null)
                return;
            showButtonZExpress = false;
            string[] products = { "buttonZTiercé", "buttonZQuarté", "buttonZQuartéPlus", "buttonZQuintéPlus", "buttonZJG",
                "buttonZJP", "buttonZSP", "buttonZSG", "buttonZTrio", "buttonZSGP", "buttonZJGP","multi4PB","multi5PB","multi6PB","multi7PB"};
            foreach (Produit produit in course.ListeProduit)
            {
                if (produit == null)
                    continue;
                bool enabled = produit.Statut == StatutProduit.EnVenteProduit
                    && (course.Statut == StatutCourse.DepartDansXX ||
                        course.Statut == StatutCourse.EnVente ||
                        course.Statut == StatutCourse.PreDepart);

                if (produit.CodeProduit.Equals("TRC"))
                {
                    showButtonProduct("buttonZTiercé", enabled);
                    products = products.Where(e => e != "buttonZTiercé").ToArray();
                }
                else if (produit.CodeProduit.Equals("QUU"))
                {
                    showButtonProduct("buttonZQuarté", enabled);
                    products = products.Where(e => e != "buttonZQuarté").ToArray();
                }
                else if (produit.CodeProduit.Equals("QAP"))
                {
                    showButtonProduct("buttonZQuartéPlus", enabled);
                    products = products.Where(e => e != "buttonZQuartéPlus").ToArray();
                }
                else if (produit.CodeProduit.Equals("QIP"))
                {
                    products = products.Where(e => e != "buttonZQuintéPlus").ToArray();
                    showButtonProduct("buttonZQuintéPlus", enabled);
                    showButtonZExpress = enabled;
                }
                else if(produit.CodeProduit.Equals("ML4"))
                {
                    showButtonProduct("multi4PB", enabled);
                    products = products.Where(e => e != "multi4PB").ToArray();
                    showButtonZExpress = enabled;
                }
                else if (produit.CodeProduit.Equals("ML5"))
                {
                    showButtonProduct("multi5PB", enabled);
                    products = products.Where(e => e != "multi5PB").ToArray();
                    showButtonZExpress = enabled;
                }
                else if (produit.CodeProduit.Equals("ML6"))
                {
                    showButtonProduct("multi6PB", enabled);
                    products = products.Where(e => e != "multi6PB").ToArray();
                    showButtonZExpress = enabled;
                }
                else if (produit.CodeProduit.Equals("ML7"))
                {
                    showButtonProduct("multi7PB", enabled);
                    products = products.Where(e => e != "multi7PB").ToArray();
                    showButtonZExpress = enabled;
                }
                else if (produit.CodeProduit.Equals("JUO"))
                {
                    flagJUO = 1;
                    showButtonProduct("buttonZJG", enabled);
                    products = products.Where(e => e != "buttonZJG").ToArray();
                }
                else if (produit.CodeProduit.Equals("JUP"))
                {
                    showButtonProduct("buttonZJP", enabled);
                    products = products.Where(e => e != "buttonZJP").ToArray();
                    if (enabled)
                    {
                        flagJUP = 1;
                    }
                    else
                    {
                        flagJUP = 0;
                    }
                }
                else if (produit.CodeProduit.Equals("JUG"))
                {
                    if (enabled)
                    {
                        flagJUG = 1;
                    }
                    else
                    {
                        flagJUG = 0;
                    }
                    showButtonProduct("buttonZJG", enabled);
                    products = products.Where(e => e != "buttonZJG").ToArray();
                }

                else if (produit.CodeProduit.Equals("PLA"))
                {
                    showButtonProduct("buttonZSP", enabled);
                    products = products.Where(e => e != "buttonZSP").ToArray();
                    if (enabled)
                    {
                        flagPLA = 1;
                    }
                    else
                    {
                        flagPLA = 0;

                    }
                }
                else if (produit.CodeProduit.Equals("GAG"))
                {
                    showButtonProduct("buttonZSG", enabled);
                    products = products.Where(e => e != "buttonZSG").ToArray();
                    if (enabled)
                    {
                        flagGAG = 1;
                    }
                    else
                    {
                        flagGAG = 0;
                    }

                }
                else if (produit.CodeProduit.Equals("TRO"))
                {
                    showButtonProduct("buttonZTrio", enabled);
                    products = products.Where(e => e != "buttonZTrio").ToArray();
                }

            }
            if (flagGAG == 1 && flagPLA == 1)
            {
                showButtonProduct("buttonZSGP", true);
                products = products.Where(e => e != "buttonZSGP").ToArray();
            }
            if (flagJUG == 1 && flagJUP == 1)
            {
                showButtonProduct("buttonZJGP", true);
                products = products.Where(e => e != "buttonZJGP").ToArray();
            }
            ControleExpresse(course);
            for (int i = 0; i < products.Length; i++)
            {
                PictureBox productBtn = GetProductBtnByName(products[i]);
                if (productBtn != null)
                {
                    productBtn.Hide();
                }
            }
            bool isCourseEnabled = myCourse.Statut == StatutCourse.DepartDansXX ||
                                myCourse.Statut == StatutCourse.EnVente ||
                                myCourse.Statut == StatutCourse.PreDepart;

            if (isCourseEnabled)
            {
                if (isChevalExpress || expressBtnClicked)
                {
                    btnCHExpress.BackgroundImage = isChevalExpress ?
                        Properties.Resources.BtnDesignationChevalEclair_Sel : Properties.Resources.BtnDesignationChevalEclair_Desel;
                    buttonZExpress.Image = Properties.Resources.BtnParis_Eclair_Sel;
                    btnFComplete.Hide();
                    btnCTotal.Hide();
                    btnFCX.Hide();
                    bisBtn.Hide();
                }
                else
                {
                    buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
                    btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                    buttonZExpress.Enabled = true;
                    btnCHExpress.Enabled = true;
                }
            }
            else
            {
                buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Inactif;
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Inactif;
                buttonZExpress.Enabled = false;
                btnCHExpress.Enabled = false;
            }

        }

        private PictureBox GetProductBtnByName(string name)
        {
            if (name == null)
                return null;
            switch (name)
            {
                case "buttonZTiercé":
                    return buttonZTiercé;
                case "buttonZQuarté":
                    return buttonZQuarté;
                case "buttonZQuartéPlus":
                    return buttonZQuartéPlus;
                case "buttonZQuintéPlus":
                    return buttonZQuintéPlus;
                case "multi4PB":
                    return multi4PB;
                case "multi5PB":
                    return multi5PB;
                case "multi6PB":
                    return multi6pb;
                case "multi7PB":
                    return multi7pb;
                case "buttonZJP":
                    return buttonZJP;
                case "buttonZJG":
                    return buttonZJG;
                case "buttonZSP":
                    return buttonZSP;
                case "buttonZSG":
                    return buttonZSG;
                case "buttonZTrio":
                    return buttonZTrio;
                case "buttonZJGP":
                    return buttonZJGP;
                case "buttonZSGP":
                    return buttonZSGP;
                default:
                    return null;
            }
        }

        private void showButtonProduct(String name, bool enable)
        {
            PictureBox productBtn = null;
            switch (name)
            {
                case "buttonZTiercé":
                    if (!enable)
                    {
                        buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce_Inactif;
                    }
                    else
                    {
                        buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce;
                    }
                    productBtn = buttonZTiercé;
                    break;
                case "buttonZQuarté":
                    if (!enable)
                    {
                        buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte_Inactif;
                    }
                    else
                    {
                        buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte;
                    }
                    productBtn = buttonZQuarté;
                    break;
                case "buttonZQuartéPlus":
                    if (!enable)
                    {
                        buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus_Inactif;
                    }
                    else
                    {
                        buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus;
                    }
                    productBtn = buttonZQuartéPlus;
                    break;
                case "buttonZQuintéPlus":
                    if (!enable)
                    {
                        buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte_Inactif;
                    }
                    else
                    {
                        buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte;
                    }
                    productBtn = buttonZQuintéPlus;
                    break;
                case "multi4PB":
                    if(!enable)
                    {
                        multi4PB.Image = Properties.Resources.BtnParis_Multi4_Inactif;
                    }
                    else
                    {
                        multi4PB.Image = Properties.Resources.BtnParis_Multi4;
                    }
                    productBtn = multi4PB;
                    break;

                case "multi5PB":
                    if (!enable)
                    {
                        multi5PB.Image = Properties.Resources.BtnParis_Multi5_Inactif;
                    }
                    else
                    {
                        multi5PB.Image = Properties.Resources.BtnParis_Multi5;
                    }
                    productBtn = multi5PB;
                    break;

                case "multi6PB":
                    if (!enable)
                    {
                        multi6pb.Image = Properties.Resources.BtnParis_Multi6_Inactif;
                    }
                    else
                    {
                        multi6pb.Image = Properties.Resources.BtnParis_Multi6;
                    }
                    productBtn = multi6pb;
                    break;

                case "multi7PB":
                    if (!enable)
                    {
                        multi7pb.Image = Properties.Resources.BtnParis_Multi7_Inactif;
                    }
                    else
                    {
                        multi7pb.Image = Properties.Resources.BtnParis_Multi7;
                    }
                    productBtn = multi7pb;
                    break;
                case "buttonZJP":
                    if (!enable)
                    {
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace_Inactif;
                    }
                    else
                    {
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace;
                    }
                    productBtn = buttonZJP;
                    break;
                case "buttonZJG":
                    if (flagJUO == 1)
                    {
                        if (!enable)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre_Inactif;
                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre;
                            //flagJUO = 1;
                        }
                        productBtn = buttonZJG;
                    }
                    else
                    {
                        if (!enable)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant;
                        }
                        productBtn = buttonZJG;
                    }
                    break;
                case "buttonZSP":
                    if (!enable)
                    {
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace_Inactif;
                    }
                    else
                    {
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace;
                    }
                    productBtn = buttonZSP;
                    break;
                case "buttonZSG":
                    if (!enable)
                    {
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                    }
                    else
                    {
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant;
                    }
                    productBtn = buttonZSG;
                    break;
                case "buttonZTrio":
                    if (!enable)
                    {
                        buttonZTrio.Image = Properties.Resources.BtnParis_Trio_Inactif;
                    }
                    else
                    {
                        buttonZTrio.Image = Properties.Resources.BtnParis_Trio;
                    }
                    productBtn = buttonZTrio;
                    break;
                case "buttonZJGP":
                    if (!enable)
                    {
                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                    }
                    else
                    {
                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP;
                    }
                    productBtn = buttonZJGP;
                    break;
                case "buttonZSGP":
                    if (!enable)
                    {
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                    }
                    else
                    {
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP;
                    }
                    productBtn = buttonZSGP;
                    break;
            }
            if (productBtn != null)
            {
                productBtn.Enabled = enable;
                productBtn.Show();
            }
        }
        private void showCourses(Reunion reunion)
        {
            Button courseBtn;
            int i = 0;
            int btnState;
            foreach (Course c in reunion.ListeCourse)
            {
                i++;
                int rapportFlag = 0;
                switch (c.Statut)
                {
                    case StatutCourse.EnVente:
                    case StatutCourse.DepartDansXX:
                    case StatutCourse.PreDepart:
                        btnState = 0;
                        break;
                    case StatutCourse.CHA:
                    case StatutCourse.ArriveeDefinitive:
                        btnState = 3;
                        foreach (Produit p in c.ListeProduit)
                        {
                            if (p == null)
                                continue;
                            switch (p.Statut)
                            {
                                case StatutProduit.Paiement:
                                case StatutProduit.Remboursement:
                                    btnState = 2;
                                    rapportFlag = 1;
                                    break;
                            }
                        }
                        break;
                    case StatutCourse.ANN:
                        btnState = 4;
                        foreach (Produit p in c.ListeProduit)
                        {
                            if (p == null)
                                continue;
                            if (p.Statut == StatutProduit.Remboursement)
                            {
                                btnState = 2;
                                rapportFlag = 1;
                                break;
                            }
                        }
                        break;
                    case StatutCourse.ArriveeProvisoire:
                    case StatutCourse.Depart:
                        btnState = 3;
                        break;
                    default:
                        btnState = 1;
                        break;
                }
                if (rapportFlag == 1)
                {
                    c.IsRapportDisponible = true;
                }
                courseBtn = GetSelectedCourseButton(i);
                
                if (courseBtn != null)
                {
                    courseBtn.Text = c.NumCoursePmu.ToString();
                    switch (btnState)
                    {
                        case 0: //actif state
                            courseBtn.BackColor = Color.Navy;
                            courseBtn.ForeColor = Color.White;
                            courseBtn.BackgroundImage = Properties.Resources.bouton_Course_Vente;
                            break;
                        case 2: // rapport state
                            courseBtn.BackColor = Color.Gray;
                            courseBtn.ForeColor = Color.White;
                            courseBtn.BackgroundImage = Properties.Resources.bouton_Course_Inactif_Rapport;
                            break;
                        case 3: // depart state
                            courseBtn.BackColor = Color.Gray;
                            courseBtn.ForeColor = Color.White;
                            courseBtn.BackgroundImage = Properties.Resources.bouton_Course_Inactif_Depart;
                            break;
                        case 4: // Remboursée state
                            courseBtn.BackColor = Color.Gray;
                            courseBtn.ForeColor = Color.White;
                            courseBtn.BackgroundImage = Properties.Resources.bouton_Course_Inactif_Annulee;
                            break;
                        default:
                            courseBtn.BackColor = Color.Gray;
                            courseBtn.ForeColor = Color.White;
                            courseBtn.BackgroundImage = Properties.Resources.bouton_Course_Inactif;
                            break;
                    }
                    courseBtn.Show();
                }

            }
            for (int j = i + 1; j < 13; j++)
            {
                courseBtn = GetSelectedCourseButton(j);
                if (courseBtn != null)
                    courseBtn.Hide();
            }
        }

        private Button GetHorseButton(int numHorse)
        {
            switch (numHorse)
            {
                case 1:
                    return btnZCH1;
                case 2:
                    return btnZCH2;
                case 3:
                    return btnZCH3;
                case 4:
                    return btnZCH4;
                case 5:
                    return btnZCH5;
                case 6:
                    return btnZCH6;
                case 7:
                    return btnZCH7;
                case 8:
                    return btnZCH8;
                case 9:
                    return btnZCH9;
                case 10:
                    return btnZCH10;
                case 11:
                    return btnZCH11;
                case 12:
                    return btnZCH12;
                case 13:
                    return btnZCH13;
                case 14:
                    return btnZCH14;
                case 15:
                    return btnZCH15;
                case 16:
                    return btnZCH16;
                case 17:
                    return btnZCH17;
                case 18:
                    return btnZCH18;
                case 19:
                    return btnZCH19;
                case 20:
                    return btnZCH20;
                default:
                    return null;
            }
        }

        private void showHorses(Course course)
        {
            if (course == null)
                return;
            course.ListeHorses.Sort((x, y) => x.NumPartant.CompareTo(y.NumPartant));

            int i = 0;
            Button horseBtn;
            foreach (Horse p in course.ListeHorses)
            {
                i++;
                horseBtn = GetHorseButton(i);
                if (horseBtn == null)
                {
                    Logger.Warn(string.Format("ShowHorses: unrecognized horse button {0} - partant number {1}", i, p.NumPartant));
                    continue;
                }
                if (course.Statut != StatutCourse.DepartDansXX &&
                        course.Statut != StatutCourse.EnVente &&
                        course.Statut != StatutCourse.PreDepart)
                {
                    horseBtn.Enabled = false;
                    horseBtn.BackgroundImage = Properties.Resources.BtnChevaux_NonPartant;
                    horseBtn.BackColor = Color.Gray;
                    if (p.EstPartant == 0)
                    {
                        horseBtn.ForeColor = Color.White;
                    }
                    else
                    {
                        horseBtn.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (p.EstPartant == 0)
                    {
                        horseBtn.Enabled = false;
                        horseBtn.BackgroundImage = Properties.Resources.BtnChevaux_NonPartant;
                        horseBtn.BackColor = Color.Gray;
                        horseBtn.ForeColor = Color.Black;
                    }
                    else
                    {
                        horseBtn.Enabled = true;
                        horseBtn.BackgroundImage = Properties.Resources.BtnChevaux_Partant;
                        horseBtn.BackColor = Color.Navy;
                        horseBtn.ForeColor = Color.White;
                    }
                }
                if (string.IsNullOrEmpty(p.EcuriePart))
                {
                    horseBtn.Text = p.NumPartant.ToString();
                    horseBtn.Font = new Font(FontFamily.GenericSansSerif, 14.25F, FontStyle.Bold);
                }
                else
                {
                    horseBtn.Text = p.NumPartant.ToString() + "\n        " + p.EcuriePart;
                    horseBtn.Font = new Font(FontFamily.GenericSansSerif, 11.25F, FontStyle.Bold);
                }
                horseBtn.Show();
            }
            for (int j = i + 1; j < 21; j++)
            {
                horseBtn = GetHorseButton(j);
                if (horseBtn != null)
                {
                    horseBtn.Hide();
                }
            }
        }

        private void HippodromesClicked(Reunion reunion)
        {
            if (reunion == null || SelectedReunion == reunion)
                return;
            SelectedReunion = reunion;
            isCourseClicked = false;
            clearData();
            showCourses(reunion);
            myCourse = getFirstCourse(reunion);
            numCourse = reunion.ListeCourse.FindIndex(c => c.Equals(myCourse)) + 1;
            produits = myCourse.ListeProduit;
            CourseSelected(myCourse);
            clearMultiplicators();
        }


        private Course getFirstCourse(Reunion reunion)
        {
            Course firstCourseEnVente = reunion.ListeCourse
                .Where(c => c.Statut.Equals(StatutCourse.EnVente)
                    || c.Statut.Equals(StatutCourse.PreDepart)
                    || c.Statut.Equals(StatutCourse.DepartDansXX))
                .FirstOrDefault();
            if (firstCourseEnVente == null)
                return reunion.ListeCourse[0];
            return firstCourseEnVente;
        }

        private void btnSelectioned(int index)
        {
            Button SelectedBtn = GetHorseButton(index);
            if (SelectedBtn != null)
                SelectedBtn.BackgroundImage = Properties.Resources.BtnChevaux_Sel;
        }

        private void btnSelectionedWithR(int index)
        {
            Button selectedWithR = GetHorseButton(index);
            if (selectedWithR != null)
                selectedWithR.BackgroundImage = Properties.Resources.BtnChevaux_ChampX;
        }

        private void btnDeselectionned(int index)
        {
            Button DeselecteBtn = GetHorseButton(index);
            if (DeselecteBtn != null)
                DeselecteBtn.BackgroundImage = Properties.Resources.BtnChevaux_Partant;
        }

        private int getNbrPartantInCombinaison()
        {
            return pariControles.getNbrPartantInCombinaison(listeChoix);
        }

        

        public void getButtonsControls()
        {
            bool flagCHX = true, isJumeleOrdre = false;
            int flagFC = 0, flagX = 0, max = 0;
            if (formulationProduits.Count > 0)
            {
                if (formulationProduits[0] != null)
                {
                    max = formulationProduits[0].NombreBase;
                }
                foreach (Produit p in formulationProduits)
                {
                    if (p == null)
                    {
                        continue;
                    }
                    if (!p.Ordre)
                    {
                        flagFC = 1;
                    }
                    if (!p.ChevalExpress)
                    {
                        flagCHX = false;
                    }
                    if (p.CodeProduit == "JUO")
                    {
                        isJumeleOrdre = true;
                    }
                    if (p.NombreBase != formulationProduits[0].NombreBase || !p.ChampX)
                    {
                        flagX = 1;
                    }
                    if (p.NombreBase > max)
                    {
                        max = p.NombreBase;
                    }
                }
            }
            if (max > 0 && listeChoix.Count >= max)
            {
                if (listeChoix.Contains("R"))
                {
                    int nbrX = getNbrVariablesX();
                    if (listeRChoisis.Count >= nbrX)
                    {
                        UpdateValiderBtn("Valider", true);
                        flagImprimerIsValid = 1;
                    }
                    else
                    {
                        UpdateValiderBtn("Valider", false);
                        flagImprimerIsValid = 0;
                    }
                }
                else
                {
                    UpdateValiderBtn("Valider", true);
                    flagImprimerIsValid = 1;
                }

            }
            else if (max == 0 || listeChoix.Count < max)
            {
                flagImprimerIsValid = 0;
                UpdateValiderBtn("Valider", false);
            }

            if (!champsXFlag && !formuleComplete)
            {
                flagJumeleIsValid = 1;
                flagSimpleIsValid = 1;
                flagMultiIsValid = 1;
            }
            else if (!champsXFlag && formuleComplete)
            {
                flagJumeleIsValid = 0;
                flagSimpleIsValid = 0;
                flagMultiIsValid = 0;
            }
            else if (champsXFlag && !formuleComplete)
            {
                flagJumeleIsValid = 1;
                flagMultiIsValid = 1;
                flagSimpleIsValid = 0;
            }
            else if (formuleComplete && champsXFlag)
            {
                flagJumeleIsValid = 0;
                flagMultiIsValid = 0;
                flagSimpleIsValid = 0;
            }

            if (flagX == 1)
            {
                champsXFlag = false;
                champsReduitFlag = false;
                btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                btnFCX.Hide();
            }
            else
            {
                btnFCX.Show();
            }

            btnCHExpress.Visible = flagCHX;
            buttonZExpress.Visible = flagCHX;

            if (flagFC == 1)
            {
                formuleComplete = false;
                btnFComplete.Hide();
            }
            else
            {
                btnFComplete.Show();
                if (isJumeleOrdre)
                {
                    btnCHExpress.Hide();
                    buttonZExpress.Hide();
                }
                else if (showButtonZExpress && !champsXFlag)
                {
                    buttonZExpress.Show();
                    btnCHExpress.Show();
                }
                else
                {
                    buttonZExpress.Hide();
                    btnCHExpress.Hide();
                }
            }

            ControleExpresse(myCourse);
            controlExpresseGrs();

            if (getNbrPartantInCombinaison() == 0 )
            {

                champsTotalFlag = false;
                btnCTotal.Hide();
                btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }
            else if (getNbrPartantInCombinaison() > 0)
            {
                if (flagX == 1)
                {
                    champsTotalFlag = false;
                    btnCTotal.Hide();
                    btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                }
                else
                    btnCTotal.Show();
            }


            bool isCourseEnabled = myCourse.Statut == StatutCourse.DepartDansXX ||
                                  myCourse.Statut == StatutCourse.EnVente ||
                                  myCourse.Statut == StatutCourse.PreDepart;

            if (isCourseEnabled)
            {
                if (isChevalExpress || expressBtnClicked)
                {
                    btnCHExpress.BackgroundImage = isChevalExpress ?
                        Properties.Resources.BtnDesignationChevalEclair_Sel : Properties.Resources.BtnDesignationChevalEclair_Desel;
                    buttonZExpress.Image = Properties.Resources.BtnParis_Eclair_Sel;
                    btnFComplete.Hide();
                    btnCTotal.Hide();
                    btnFCX.Hide();
                    bisBtn.Hide();
                }
                else
                {
                    buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
                    btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                    buttonZExpress.Enabled = true;
                    btnCHExpress.Enabled = true;
                }
            }
            else
            {
                buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Inactif;
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Inactif;
                buttonZExpress.Enabled = false;
                btnCHExpress.Enabled = false;
            }
            if (listeChoix.Contains("R") || isChevalExpress) btnCTotal.Hide();
            if (Designation() != "" || formulationProduits.Count > 0)
            {
                button5.Show();
            }
            else
            {
                button5.Hide();
            }
            if (ticket.ListeFormulation.Count > 0 && formulationProduits.Count == 0)
            {
                UpdateValiderBtn("Imprimer", true);
                bisBtn.Visible = !ticket.ListeFormulation[ticket.ListeFormulation.Count - 1].Designation.Contains("S");
                bisBtn.Enabled = true;
                flagImprimerIsValid = 1;
                ImprimerFlag = 0;
            }
        }


        private Produit getProduitByName(String s)
        {
            foreach (Produit produit in produits)
            {
                if (produit != null && produit.CodeProduit != null && produit.CodeProduit.Equals(s))
                    return produit;

            }
            return null;
        }
        private int getNbrRChoisis()
        {
            return listeRChoisis.Count;
        }


        private int getNbrChevalJoue()
        {
            return pariControles.getNbrChevalJoue(listeChoix);
        }

        private int addMultiplicateur(int m)
        {
            return pariControles.addMultiplicateur(m, listeMultiplicateurs);
        }

        private int getNbrVariablesX()
        {
            return pariControles.getNbrVariablesX(listeChoix);
        }
        private int getVariablesX()
        {
            int n1 = getNbrPartantInCombinaison();
            int n2 = myCourse == null ? 0 : myCourse.ListePartant.Count;
            int n3 = getNbrRChoisis();

            return pariControles.getVariablesX(listeChoix, n1, n2, n3);
        }

        public void showPrice(decimal priceS)
        {
            List<Formulation> f = getFormulations();

            int miseBase = 0;
            long miseTotal = 0;
            foreach (Formulation fm in f)
            {
                if (fm != null)
                {
                    if (fm.Produit != null)
                        miseBase = fm.Produit.EnjeuMin;
                    miseTotal += fm.MiseTotal;
                }

            }
            UpdateTotalPrice(priceS);
            showPariPrice(miseBase, miseTotal);
        }

        private String Designation()
        {
            String str = "";
            int counter = 0;
            while (counter < listeChoix.Count)
            {

                if (counter == listeChoix.Count - 1)
                {
                    str = str + listeChoix[counter].ToString();
                }
                else
                {
                    str = str + listeChoix[counter].ToString() + " ";
                }
                counter++;
            }

            return str;
        }

        public List<Formulation> getFormulations()
        {

            lsformulation.Clear();
            nbrChevalJoué = getNbrChevalJoue();
            nbrVariableX = getNbrVariablesX();
            variablesX = getVariablesX();

            foreach (Produit p in formulationProduits)
            {
                if (p == null)
                    continue;

                long m = 0;
                iTypePari = p.NombreBase;
                int multip = 1;
                if (listeMultiplicateurs.Count >= 1)
                {
                    multip = 0;
                    foreach (int mu in listeMultiplicateurs)
                    {
                        multip = multip + mu;
                    }
                }
                int V = variablesX;
                long var2 = pariControles.Combinaison(nbrVariableX, V);
                long var1 = pariControles.Combinaison(p.NombreBase, nbrChevalJoué);
                Formulation f = new Formulation();
                f.Produit = p;
                f.FormComplete = formuleComplete;
                f.Designation = Designation();
                if (p.Ordre)
                    Ordre = 1;
                else
                    Ordre = 0;
                if (formuleComplete)
                    permutation = p.NombreBase;
                else
                    permutation = 1;
                //m =  multip * p.EnjeuMin * nombreParisExpress * var1 * var2 * pariControles.Max(
                // 1, Ordre * pariControles.Max(pariControles.factorial(nbrVariableX), pariControles.factorial(permutation)));
                m = multip * p.EnjeuMin * var1 * var2 * pariControles.Max(
                 1, Ordre * pariControles.Max(pariControles.factorial(nbrVariableX), pariControles.factorial(permutation)));
                // logger.addLog("mise"+m,1);
                f.MiseCombinaison = p.EnjeuMin * multip;
                f.MiseTotal = m;
                lsformulation.Add(f);
            }
            if (lsformulation.Count > 0)
            {
                for (int i = 1; i < nombreParisExpress; i++)
                {
                    Formulation f = new Formulation(lsformulation[0]);
                    lsformulation.Add(f);
                }

            }
        

            return lsformulation;
        }
        int itChoixMAke(int num, int n)
        {
            int res = 0;
            int index = -1; // index of n
            int index2 = -1; // index of X
            int index3 = -1; // index of R
                             // Recherche de champs X, Champs R, Et cheval saisi
            bool isProductOrder = false;
            for (int i = 0; i < listeChoix.Count; i++)
            {
                if (listeChoix[i] == null)
                    continue;
                if (listeChoix[i].Equals(n))
                {
                    index = i;
                }
                else if (listeChoix[i].Equals("X"))
                {
                    index2 = i;
                }
                else if (listeChoix[i].Equals("R"))
                {
                    index3 = i;
                }
            }

            foreach (Produit p in formulationProduits)
            {
                if (p.Ordre)
                {
                    isProductOrder = true;
                }
            }
            // cas normal si un nouveau cheval est saisi sans Champs X

            if (index == -1 && index2 == -1 && index3 == -1 && replaceFlag == -1 || index == -1 && listeChoix.Count < iTypePari && replaceFlag == -1)
            {
                if (this.isChevalExpress && this.listeChoix.Count >= 8)
                {
                    showErrorMessage("Vous ne pouvez pas designer plus de 8 chevaux", Color.Red);
                    return 0;
                }
                listeChoix.Add(n);
                StopBlinkChevalBtn();
                btnSelectioned(num);
            }
            // cas ou un cheval choisi et selectionné une nouvelle fois
            // on attend la saisie d'un nouveau cheval et on le remplace

            else if (replaceFlag != -1)
            {

                if (index2 == -1 && index3 == -1)
                {
                    if (listeChoix[replaceFlag] != null && !listeChoix[replaceFlag].Equals(n) && !listeChoix.Contains(n))
                    {
                        listeChoix.Insert(replaceFlag, n);
                        listeChoix.RemoveAt(replaceFlag + 1);
                        StopBlinkChevalBtn();
                        btnSelectioned(num);
                        btnDeselectionned(chevalReplaceWith);
                        replaceFlag = -1;
                    }
                    else
                    {
                        showErrorMessage("Sélectionnez un cheval pour \n remplacer le cheval " + chevalReplaceWith, Color.Red);
                        res = chevalReplaceWith;
                    }
                }

                else
                {
                    if (listeChoix[replaceFlag] != null && !listeChoix[replaceFlag].Equals(n) && !listeChoix.Contains(n))
                    {
                        listeChoix.Insert(replaceFlag, n);
                        listeChoix.RemoveAt(replaceFlag + 1);

                        if (champsReduitFlag)
                        {
                            if (replaceFlag > index3)
                            {
                                btnSelectionedWithR(num);
                            }
                            else if (replaceFlag < index3)
                            {
                                StopBlinkChevalBtn();
                                btnSelectioned(num);
                            }

                        }
                        else if (!champsReduitFlag)
                        {
                            StopBlinkChevalBtn();
                            btnSelectioned(num);

                        }

                        replaceFlag = -1;
                        btnDeselectionned(chevalReplaceWith);
                    }

                }

            }

            else if (index2 != -1 && listeChoix.Count >= iTypePari)
            {
                if (!champsTotalFlag)
                {

                    if (index3 == -1 && index == -1)
                    {
                        champsReduitFlag = true;
                        listeChoix.Add("R");
                        listeChoix.Add(n);
                        btnSelectionedWithR(num);
                        listeRChoisis.Add(n);
                        btnCTotal.Hide();

                    }
                    else if (index3 != -1 && index == -1)
                    {
                        listeChoix.Add(n);
                        btnSelectionedWithR(num);
                        listeRChoisis.Add(n);
                    }
                    else if (index == listeChoix.Count - 1 || index > index3)
                    {
                        listeChoix.Remove(n);
                        btnDeselectionned(num);
                        listeRChoisis.Remove(n);
                        if (listeRChoisis.Count == 0)
                        {
                            listeChoix.Remove("R");
                            champsReduitFlag = false;
                            champsXFlag = true;
                            btnCTotal.Show();

                        }
                    }

                    else if (index != listeChoix.Count - 1 && index < index3)
                    {
                        replaceFlag = index;
                        chevalReplaceWith = num;
                        showErrorMessage("Sélectionnez un cheval pour \n remplacer le cheval " + chevalReplaceWith, Color.Red);
                        res = chevalReplaceWith;
                    }
                }
                else
                {
                    showErrorMessage("Formulation incorrect", Color.Red);
                }
            }
            else
            {   // si le cheval à la derniere position est selectionné deux fois on le supprime
                // si le cheval n'est pas à la derniere position est selectionné deux fois on stcok sa valeur pour un remplacement prochain.
                if (index == listeChoix.Count - 1 || !isProductOrder)
                {
                    listeChoix.Remove(n);
                    btnDeselectionned(num);
                }
                else if (index != listeChoix.Count - 1)
                {
                    replaceFlag = index;
                    chevalReplaceWith = num;
                    blinkChevalButton(chevalReplaceWith);
                    showErrorMessage("Sélectionnez un cheval pour \n remplacer le cheval " + chevalReplaceWith, Color.Red);
                    res = chevalReplaceWith;
                }

            }
            getButtonsControls();
            return res;

        }
        private static Button chevalToBlink;
        public void blinkChevalButton(int num)
        {
            chevalToBlink = GetHorseButton(num);
            _blinkChevalTimer.Enabled = true;
        }
        public void BlinkChevalBtn(object source, ElapsedEventArgs e)
        {
            chevalToBlink.ForeColor = blinkChevalFlag ? Color.DarkRed : Color.Yellow;
            blinkChevalFlag = !blinkChevalFlag;
        }


        public void StopBlinkChevalBtn()
        {
            
            _blinkChevalTimer.Enabled = false;
            if(chevalToBlink != null)
            {
                chevalToBlink.ForeColor = Color.White;
                chevalToBlink.Refresh();
            }
            blinkChevalFlag = false;
            chevalToBlink = null;
        }

        private void CoefMulti(int coef)
        {
            montant = (montant * coef);
            richTextBox4.Text = "Vous recevez\n\n" + montant.ToString() + "DH";
            showPariPrice(iBase, montant);

        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _dateTimeTick(object sender, EventArgs e)
        {
            if (!Visible)
                return;
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public static string RemoveExtraSpaces(string str)
        {
            if (str.IndexOf("  ") == -1)
            {
                return str;
            }
            else
            {
                return RemoveExtraSpaces(str.Replace("  ", " "));
            }
        }
        private String getPari()
        {
            return pariControles.getPari(listeChoix);
        }

        private void clearData()
        {
            Color color = Color.Transparent;
            listeChoix.Clear();
            listeRChoisis.Clear();
            //txtTotal.Text = "";
            txtbMessages.Text = "";
            txtbMessages.Refresh();
            formulationProduits.Clear();
            txtbMessages.BackColor = color;
            champsXFlag = false;
            richTextBox3.Clear();
            richTextBox3.Refresh();
            iBase = 0;
            showPariPrice(iBase, iBase);
            foreach (Control control in this.panel21.Controls)
            {
                if (control is Button)
                {
                    if (control.Enabled == false)
                    {
                        control.BackColor = Color.Gray;
                        control.ForeColor = Color.Black;

                    }
                    else
                    {
                        control.BackColor = Color.Navy;
                        control.ForeColor = Color.White;
                    }
                }
            }
            btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            clearData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearData();
        }


        public void showParis(int i = 0, bool isAfterPrinted = false)
        {
            if(!isAfterPrinted)
                showEcranClient = 1;
            richTextBox3.Clear();
            richTextBox3.Refresh();
            String s = getPari();
            
            richTextBox3.Font = new Font("Arial", 14, FontStyle.Bold);
            if (i == 0)
                richTextBox3.Text = "  " + s;
            else
            {
                string[] splited = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string res = "";
                for (int j = 0; j < splited.Length; j++)
                {
                    if (j != 0)
                        res += "  ";
                    if (splited[j] == "" + i)
                        res += splited[j].Replace("" + i, " -" + i + "- ");
                    else
                        res += splited[j];
                }
                richTextBox3.Text = "  " + res;
            }
        }

        private string getNameProduit(string code)
        {
            string produit;
            switch (code)
            {
                case "GAG": produit = "GAGNANT"; break;
                case "PLA": produit = "PLACE"; break;
                case "JUG": produit = "JUMELE G"; break;
                case "JUO": produit = "JUMELE O"; break;
                case "JUP": produit = "JUMELE P"; break;
                case "TRO": produit = "TRIO"; break;
                case "TRC": produit = "TIERCE"; break;
                case "QUU": produit = "QUARTE"; break;
                case "QAP": produit = "QUARTE +"; break;
                case "QIP": produit = "QUINTE +"; break;
                case "ML4": produit = "MULTI EN 4"; break;
                case "ML5": produit = "MULTI EN 5"; break;
                case "ML6": produit = "MULTI EN 6"; break;
                case "ML7": produit = "MULTI EN 7"; break;
                default: produit = code; break;
            }
            return produit;
        }

        private void showPariPrice(int montant1, long montant2)
        {
            if (formulationProduits.Count == 1)
            {
                txtbPari.Text = getNameProduit(formulationProduits[0].CodeProduit) + "    " + montant1 + "DH" + "          " + montant2 + " DH";
            }
            else if (formulationProduits.Count > 1)
            {
                txtbPari.Text = formulationProduits.Count + "PARIS     " + "          " + montant2 + " DH";
            }
            else
            {
                txtbPari.Text = "PARIS      " + "              " + montant2 + " DH";
            }

            _ecranClientParis = txtbPari.Text;
        }

        private bool imprimerTicket(List<Formulation> produits)
        {
            ticket.NumberPartans = myCourse.ListePartant.Count;

            List<Formulation> temp = new List<Formulation>();
            int tempNumOffreVersion = ApplicationContext.CURRENT_NUMOFFRE_VERSION;
            for (int i = 0; i < produits.Count; i++)
            {
                if ((i + 1) % 4 == 0 || produits.Count == (i + 1))
                {
                    temp.Add(produits[i]);
                    ticket.CodeHippo = r.LibReunion;
                    ticket.NumPDV = ConfigUtils.ConfigData.Num_pdv;
                    ticket.PosTerminal = ConfigUtils.ConfigData.Pos_terminal;
                    ticket.ListeFormulation = temp;
                    long total = 0;
                    for (int j = 0; j < temp.Count; j++)
                    {
                        total += temp[j].MiseTotal;
                    }
                    ticket.PrixTotalTicket = total;
                    string invalidDesignation = null;
                    foreach (Formulation formulation in ticket.ListeFormulation)
                    {
                        if (!_validateFormComplete(formulation))
                        {
                            invalidDesignation = "Formulation incorrecte\nFormule complète non autorisée";
                            break;
                        }
                        if (!_validateDesignation(formulation))
                        {
                            invalidDesignation = "Formulation incorrecte\n" + formulation.Designation;
                            break;
                        }
                        if (!_validateMiseTotal(formulation))
                        {
                            invalidDesignation = "Montant incorrect\n" + formulation.MiseTotal;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(invalidDesignation))
                    {
                        temp.Clear();
                        chexpressCount = 0;
                        ApplicationContext.sTotal -= total;
                        JournalUtils.saveJournalInvalidDesignation(r.NumReunion, myCourse.NumCoursePmu, invalidDesignation);
                        showErrorMessage(invalidDesignation, Color.Red);
                    }
                    else
                    {
                        string response = pariService.EnregistrerPari(ticket);

                        string TLV = TLVHandler.getTLVChamps(response);
                        TLVHandler appTagsHandler = new TLVHandler(TLV);
                        int codeResponse = TLVHandler.Response(response);
                        try
                        {
                            TLVTags dataPariTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_PARI);
                            // TLVTags coteDataTag = null;
                            TLVTags communicationsTag = null;
                            TLVTags majLogicielle = appTagsHandler.getTLV(TLVTags.SOREC_MAJ_LOGICIELLE);
                            int countPartant = 0;
                            if (dataPariTag != null)
                            {
                                TLVHandler dataPariHandler = new TLVHandler(Utils.bytesToHex(dataPariTag.value));
                                TLVTags NumVersionTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_OFFRE);
                                if (NumVersionTag != null)
                                {
                                    tempNumOffreVersion = int.Parse(Utils.bytesToHex(NumVersionTag.value));
                                }
                                communicationsTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_COMMUNICATIONS);
                                string idTicket = Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_IDCONC).value);
                                ticket.IdTicket = int.Parse(idTicket).ToString();
                                ticket.IdServeur = (Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_IDSERVEUR).value));
                                ticket.CVNT = Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_CVNT).value));
                                ticket.DateEmission = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_DATEEMISSION).value)));
                                ticket.NumReunion = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_NUMREUNION).value)));
                                ticket.NumCourse = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_NUMCOURSE).value)));
                                ticket.DateReunion = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARI_DATEREUNION).value)));
                                countPartant = Int32.Parse(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_PARTANT).value));
                            }
                            switch (codeResponse)
                            {
                                case Constantes.RESPONSE_CODE_OK_UPDATE_OFFRE:
                                case Constantes.RESPONSE_CODE_OK:
                                    bool isPrinted = ImprimerTicket.imprimerTicket(ticket, ApplicationContext.CommunicationTicket, countPartant, listeMultiplicateurs);
                                    if (isPrinted)
                                    {
                                        DetailsClientOperation op = new DetailsClientOperation(ticket, TypeOperation.Pari);
                                        ApplicationContext.DetailClient.Operations.Add(op);
                                        
                                        distributionCount++;
                                        distribution += ticket.PrixTotalTicket;
                                        UpdateDetailClientData();
                                        showEcranClient = 2;
                                        JournalUtils.saveJournalDistribution(ticket, true);
                                    }
                                    else
                                    {
                                        TraitementTicketControle annulationControl = new TraitementTicketControle();
                                        ApplicationContext.sTotal -= total;
                                        string result = annulationControl.TraiterTicket(ticket, false, true);
                                        int annulationRpCode = TLVHandler.Response(result);
                                        switch (annulationRpCode)
                                        {
                                            case Constantes.RESPONSE_CODE_OK:
                                                TerminalUtils.updateLastTicketInfos(ticket);
                                                JournalUtils.saveJournalDistribution(ticket, false, "DÉDUIT MACHINE");
                                                break;
                                            default:
                                                Logger.Error(string.Format("Problème annulation machine du ticket N° {0}, ErrorCode {1}", ticket.IdTicket, annulationRpCode));
                                                JournalUtils.saveJournalDistribution(ticket, false, "DÉDUIT SYSTÈME");
                                                break;
                                        }
                                        using (PariAlert pariAlert = new PariAlert(ticket.IdTicket))
                                        {
                                            _ = pariAlert.ShowDialog();
                                        }
                                    }
                                    ApplicationContext.SOREC_PARI_MISE_TOTALE -= total;
                                    TlvUtlis.SetCorpsCommunication(communicationsTag);
                                    if (!ApplicationContext.isAuthenticated
                                        || (!ApplicationContext.develop && !isPrinted))
                                    {
                                        return false;
                                    }
                                    if (majLogicielle != null)
                                    {
                                        using (MAJForm alert = new MAJForm())
                                        {
                                            alert.StartPosition = FormStartPosition.CenterParent;
                                            _ = alert.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        ApplicationContext.isVersionAuthorized = true;
                                    }
                                    if (!ApplicationContext.isVersionAuthorized)
                                    {
                                        ApplicationContext.LaunchAuthenticationForm("");
                                        return false;
                                    }
                                    break;
                                case Constantes.RESPONSE_CODE_KO:
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_MAC_INVALIDE:
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_NB_FORMULATIONS:
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_STATUT_PDV:
                                    showErrorMessage("Ce PDV est suspendu de la vente.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_SOLDE_INSUFFISANT:
                                    showErrorMessage("Dépassement du plafond autorisé.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_COURSE_INTROUVABLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PRODUIT_INDISPONIBLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PARTICIPANT_INDISPONIBLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_MISSING_TAG:
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_TYPECANAL_INVALIDE:
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_STATUT_COURSE_INVALIDE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_STATUT_PARTICIPANT_INVALIDE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_NB_PARTANT_INVALIDE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_STATUT_PRODUIT_INVALIDE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PARTICIPANT_INTROUVABLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PRODUIT_INTROUVABLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_DESIGNATION_ABSENT:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_REUNION_INTROUVABLE:
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PLAFOND_POSTPAYE_DEPASSE:
                                    showErrorMessage(string.Format("Dépassement du plafond d'endettement autorisé."), Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                case Constantes.RESPONSE_CODE_KO_PLAFOND_PREPAYE_DEPASSE:
                                    showErrorMessage(string.Format("Dépassement du plafond prépayé autorisé."), Color.Red);
                                    ApplicationContext.sTotal -= total;
                                    break;
                                default:
                                    ApplicationContext.sTotal -= total;
                                    showErrorMessage("Transaction non aboutie: " + codeResponse + ".\nVeuillez réessayer.", Color.Red);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            ApplicationContext.sTotal -= total;
                            Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
                            showErrorMessage("Erreur technique.\nVeuillez contacter l'administration.", Color.Red);
                            var st = new StackTrace(ex, true);
                            var frame = st.GetFrame(0);
                            var line = frame.GetFileLineNumber();
                            JournalUtils.saveJournalMessage(string.Format("Une erreur technique s'est produite. {0}: [Form Principale, {1}]", ex.Message, line));
                        }
                        finally
                        {
                            temp.Clear();
                            chexpressCount = 0;
                        }
                    }
                }
                else
                {
                    temp.Add(produits[i]);
                }
                if (ApplicationContext.CURRENT_NUMOFFRE_VERSION < tempNumOffreVersion)
                {
                    break;
                }
            }
            if (ApplicationContext.CURRENT_NUMOFFRE_VERSION < tempNumOffreVersion)
            {
                Offre newOffre = offreControl.getDataOffreFromMT();
                if (newOffre != null && !newOffre.IsEmpty)
                {
                    ApplicationContext.SOREC_DATA_OFFRE = newOffre;
                    ReLoadView(new DataTerminalIndicesHolder(SelectedReunion.DateReunion, SDictionnaryUtils<DateTime,List<Reunion>>.getKeyByIndex(hashReunion,indiceHashReunion), SelectedReunion.NumReunion, myCourse.NumCoursePmu, TypeReload.INIT));
                    showErrorMessage("Attention.\nChangement d'offre.", Color.Red);
                }
                else
                {
                    showErrorMessage("Attention.\nla nouvelle offre n'est pas chargée.", Color.Red);
                }
                bisBtn.Hide();
            }
            return true;
        }

        public void DiffuserOffre()
        {
            Offre newOffre = offreControl.getDataOffreFromMT();
            if (newOffre != null && !newOffre.IsEmpty)
            {
                ApplicationContext.SOREC_DATA_OFFRE = newOffre;
                ApplicationContext.CURRENT_NUMOFFRE_VERSION = ApplicationContext.SOREC_DATA_OFFRE.NumVersion;
            }
            else
            {
                showErrorMessage("Attention.\nLa nouvelle offre n'est pas chargée", Color.Red);
            }
        }

        public void SignalLoaded(string message, bool showAlert = false)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { SignalLoaded(message, showAlert); }));
                return;
            }
            if (Visible)
            {
                if (showAlert)
                {
                    if (signalAlertMsg == null)
                    {
                        signalAlertMsg = new SignalAlertMsg(this);
                    }
                    signalAlertMsg.ShowForm();
                }
                else
                {
                    ReLoadView(new DataTerminalIndicesHolder(SelectedReunion.DateReunion, SDictionnaryUtils<DateTime, List<Reunion>>.getKeyByIndex(hashReunion, indiceHashReunion), SelectedReunion.NumReunion, myCourse.NumCoursePmu, TypeReload.SIGNAL));
                    showErrorMessage("Changement d'offre\n" + message, Color.Red);
                }
            }
            JournalUtils.saveJournalMessage("Changement d'offre\n" + message);
        }

        public void LoadDataSignal()
        {
            ReLoadView(new DataTerminalIndicesHolder(SelectedReunion.DateReunion, SDictionnaryUtils<DateTime, List<Reunion>>.getKeyByIndex(hashReunion,indiceHashReunion), SelectedReunion.NumReunion, myCourse.NumCoursePmu, TypeReload.SIGNAL));
        }

        private void nouvelleCourseControle()
        {
            ApplicationContext.sTotal -= ApplicationContext.TEMP_TOTAL_BEFORE_PRINT;
            ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
            chexpressCount = 0;
            UpdateTotalPrice(0);
            champsXFlag = false;
            champsTotalFlag = false;
            formuleComplete = false;
            champsReduitFlag = false;
            IsExpress = false;
            isChevalExpress = false;
            btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            panel18.Hide();
            panel19.Hide();
            nombreParisExpress = 1;
            listeChoix.Clear();
            replaceFlag = -1;
            listeRChoisis.Clear();
            clearMultiplicators();
            formulationProduits.Clear();
            ticket.ListeFormulation.Clear();
            getButtonsControls();
            StopBlinkChevalBtn();
            showParis();
            showPrice(0);
            expressBtnClicked = false;
          //  buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[0]) && !SelectedCourse.IsRapportDisponible)
                return;
            myCourse = r.ListeCourse[0];
            bisBtn.Hide();
            numCourse = 1;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;

        }

        private bool checkPartant(int numbutton, int numPartant)
        {
            if (myCourse == null)
                return false;
            Horse horse = myCourse.GetHorseByNum(numPartant);
            return  horse != null && horse.EstPartant == StatutPartant.EstPartant;
        }

        private void EnableCourseButton(bool enable)
        {
            buttonZC1.Enabled = enable;
            buttonZC2.Enabled = enable;
            buttonZC3.Enabled = enable;
            buttonZC4.Enabled = enable;
            buttonZC5.Enabled = enable;
            buttonZC6.Enabled = enable;
            buttonZC7.Enabled = enable;
            buttonZC8.Enabled = enable;
            buttonZC9.Enabled = enable;
            buttonZC10.Enabled = enable;
            buttonZC11.Enabled = enable;
            buttonZC12.Enabled = enable;
        }
        private void EnableHorsesButton(bool enable)
        {
            btnZCH1.Enabled = enable;
            btnZCH2.Enabled = enable;
            btnZCH3.Enabled = enable;
            btnZCH4.Enabled = enable;
            btnZCH5.Enabled = enable;
            btnZCH6.Enabled = enable;
            btnZCH7.Enabled = enable;
            btnZCH8.Enabled = enable;
            btnZCH9.Enabled = enable;
            btnZCH10.Enabled = enable;
            btnZCH11.Enabled = enable;
            btnZCH12.Enabled = enable;
            btnZCH13.Enabled = enable;
            btnZCH14.Enabled = enable;
            btnZCH15.Enabled = enable;
            btnZCH16.Enabled = enable;
            btnZCH17.Enabled = enable;
            btnZCH18.Enabled = enable;
            btnZCH19.Enabled = enable;
            btnZCH20.Enabled = enable;
        }

        private Button GetSelectedCourseButton(int num)
        {
            switch (num)
            {
                case 1:
                    return buttonZC1;
                case 2:
                    return buttonZC2;
                case 3:
                    return buttonZC3;
                case 4:
                    return buttonZC4;
                case 5:
                    return buttonZC5;
                case 6:
                    return buttonZC6;
                case 7:
                    return buttonZC7;
                case 8:
                    return buttonZC8;
                case 9:
                    return buttonZC9;
                case 10:
                    return buttonZC10;
                case 11:
                    return buttonZC11;
                case 12:
                    return buttonZC12;
                default:
                    return null;
            }
        }

        private string addNWhitespace(string sBase, int n)
        {
            if (sBase == null || sBase.Length > n)
                return sBase;
            return sBase + new string(' ', n - sBase.Length);
        }  
        
        private string getReunionInfos(Reunion reun, Course course)
        {
            return addNWhitespace(reun.LibReunion, 27) + "Course " + course.NumCoursePmu
                + "\n\nR" + addNWhitespace(reun.NumReunion.ToString() + "  " + reun.ListeCourse.Count + " Courses ", 25) + course.ListePartant.Count + " Partants";
        }

        private void CourseSelected(Course course)
        {
            if (course.IsRapportDisponible && isCourseClicked)
            {
                ShowDetailCourcePanel();
            }
            if (SelectedCourse == course)
            {
                EnableCourseButton(true);
                return;
            }
            first = 0;
            showButtonZExpress = false;
            expressBtnClicked = false;
            SelectedCourse = course;
            myCourse = course;
            UpdateButtonScrollHorsesPanel(course);
            clearMessageZone();
            ResetProductsFlags();
            lblcourse.Text = "Réunion " + r.NumReunion + " -- Course " + course.NumCoursePmu + "-- " + course.HeureDepartCourse.ToString("HH:mm");
            string s = getReunionInfos(r, course);
            switch (numHippo)
            {
                case 1: buttonZ1.Text = s; break;
                case 2: buttonZ2.Text = s; break;
                case 3: buttonZ3.Text = s; break;
                case 4: buttonZ4.Text = s; break;
                case 5: buttonZ5.Text = s; break;
            }
            richTextBox1.Text = string.Format("{0} | Réunion {3}- Course {4}\n{1} {2}", r.LibReunion, r.DateReunion.ToString("dddd, dd MMMM yyyy"), myCourse.HeureDepartCourse.ToString("HH:mm"), r.NumReunion, myCourse.NumCoursePmu);
            EnableCourseButton(false);
            UpdateCourseButton(course);
            EnableCourseButton(true);
            lblRC.Text = "➤ " + r.LibReunion + "- Course" + course.NumCoursePmu;
            lblDépart.Text = "Départ prévu à " + course.HeureDepartCourse.Hour + "h" + course.HeureDepartCourse.Minute;
            showHorses(course);
            showProduits(course);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[1]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[1];
            bisBtn.Hide();
            numCourse = 2;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[2]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[2];
            bisBtn.Hide();
            numCourse = 3;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 1;
            string[] chNbr = btnZCH1.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré Non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private delegate void ShowErrorMessageDelegate(string text, Color color);
        private void showErrorMessage(string text, Color color)
        {
            txtbMessages.Invoke(new ShowErrorMessageDelegate(updateErrorMessage), text, color);
        }

        private void updateErrorMessage(string text, Color color)
        {
            StopBlinkMessageTimer();
            txtbMessages.ForeColor = color;
            txtbMessages.Text = color == Color.Green ? text.ToUpper() : text;
            txtbMessages.Font = color == Color.Green ? errorMsgGreen : errorMsgInitFont;
            txtbMessages.Refresh();
            if (color == Color.Red && !string.IsNullOrEmpty(text))
            {
                _blinkMessageTimer.Enabled = true;
            }
        }

        private void StopBlinkMessageTimer()
        {
            blinkFlag = true;
            _blinkMessageTimer.Stop();
            flowLayoutPanel1.BackgroundImage = Properties.Resources.lblMessageInfo;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            txtbMessages.ForeColor = blinkFlag ? Color.Red : Color.White;
            flowLayoutPanel1.BackgroundImage = blinkFlag ? Properties.Resources.lblMessageInfo : Properties.Resources.lblMessageErreur;
            blinkFlag = !blinkFlag;
        }

        private void clearMessageZone()
        {
            StopBlinkMessageTimer();
            txtbMessages.Text = "";
            txtbMessages.Refresh();
        }
        private void btnZCH2_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 2;
            string[] chNbr = btnZCH2.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH3_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 3;
            string[] chNbr = btnZCH3.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est  \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH4_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 4;
            string[] chNbr = btnZCH4.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est  \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH5_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 5;
            string[] chNbr = btnZCH5.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH6_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 6;
            string[] chNbr = btnZCH6.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH7_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 7;
            string[] chNbr = btnZCH7.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH8_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 8;
            string[] chNbr = btnZCH8.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH9_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 9;
            string[] chNbr = btnZCH9.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH10_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 10;
            string[] chNbr = btnZCH10.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH11_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 11;
            string[] chNbr = btnZCH11.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH12_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 12;
            string[] chNbr = btnZCH12.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH13_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 13;
            string[] chNbr = btnZCH13.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH14_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 14;
            string[] chNbr = btnZCH14.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH15_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 15;
            string[] chNbr = btnZCH15.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
               remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH16_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 16;
            string[] chNbr = btnZCH16.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH17_Click(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 17;
            string[] chNbr = btnZCH17.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH18_Click_1(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 18;
            string[] chNbr = btnZCH18.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH19_Click_1(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 19;
            string[] chNbr = btnZCH19.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void btnZCH20_Click_1(object sender, EventArgs e)
        {
            if (IsExpress)
                return;
            clearMessageZone();
            numPartant = 20;
            string[] chNbr = btnZCH20.Text.Split('\n');
            int n = Int32.Parse(chNbr[0]);
            bool isPartant = checkPartant(numPartant, n);
            int remplacant = 0;
            if (isPartant)
            {
                remplacant = itChoixMAke(numPartant, n);
            }
            else
            {
                showErrorMessage("cheval " + n + "  Est \n déclaré non Partant ", Color.Red);
            }
            showParis(remplacant);
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZC4_Click_1(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[3]) & !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[3];
            bisBtn.Hide();
            numCourse = 4;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC5_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[4]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[4];
            bisBtn.Hide();
            numCourse = 5;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;

        }

        private void buttonZC6_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[5]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[5];
            bisBtn.Hide();
            numCourse = 6;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC7_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[6]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[6];


            numCourse = 7;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC8_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[7]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[7];
            bisBtn.Hide();
            numCourse = 8;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC9_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[8]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[8];
            bisBtn.Hide();
            numCourse = 9;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC10_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[9]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[9];
            bisBtn.Hide();
            numCourse = 10;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC11_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[10]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[10];
            bisBtn.Hide();
            numCourse = 11;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }

        private void buttonZC12_Click(object sender, EventArgs e)
        {
            isCourseClicked = true;
            if ((flagVerrouCourse == 1 || SelectedCourse == r.ListeCourse[11]) && !SelectedCourse.IsRapportDisponible)
                return;
            
            myCourse = r.ListeCourse[11];
            bisBtn.Hide();
            numCourse = 12;
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
        }


        private void btnChampsXClicked(object sender, EventArgs e)
        {
            if (formulationProduits.Count == 0)
            {
                iTypePari = 5;
            }
            else
            {
            }
            if (!champsXFlag)
            {
                champsXFlag = true;
                btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            }
            int nbrX = getNbrVariablesX();
            int index = listeChoix.IndexOf("TOTAL");
            int index2 = listeChoix.IndexOf("R");
            // 
            if (nbrX < iTypePari - 1 && index == -1 && index2 == -1 && listeChoix.Count < iTypePari)
            {
                listeChoix.Add("X");
            }

            else if (index != -1)
            {
                listeChoix.Remove("TOTAL");
                btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }

            else
            {
                showErrorMessage("Rang du X trop important ", Color.Red);
                txtbMessages.Refresh();

            }
            getButtonsControls();
            showParis();
            showPrice(0);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {

            int flag = addMultiplicateur(2);
            if (flag == 0)
            {
                btnX2.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX2.ForeColor = Color.Black;
            }
            else
            {

                btnX2.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX2.ForeColor = Color.White;
            }
            showPrice(0);
        }



        private void button11_Click(object sender, EventArgs e)
        {
            int flag = addMultiplicateur(3);
            if (flag == 0)
            {
                btnX3.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX3.ForeColor = Color.Black;
            }
            else
            {
                btnX3.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX3.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int flag = addMultiplicateur(4);
            if (flag == 0)
            {
                btnX4.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX4.ForeColor = Color.Black;
            }
            else
            {
                btnX4.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX4.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int flag = addMultiplicateur(5);
            if (flag == 0)
            {
                btnX5.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX5.ForeColor = Color.Black;
            }
            else
            {
                btnX5.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX5.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button14_Click(object sender, EventArgs e)
        {

            int flag = addMultiplicateur(10);
            if (flag == 0)
            {
                btnX10.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX10.ForeColor = Color.Black;
            }
            else
            {
                btnX10.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX10.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int flag = addMultiplicateur(15);
            if (flag == 0)
            {
                btnX15.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX15.ForeColor = Color.Black;
            }
            else
            {
                btnX15.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX15.ForeColor = Color.White;
            }
            showPrice(0);

        }

        private void button15_Click(object sender, EventArgs e)
        {

            int flag = addMultiplicateur(20);
            if (flag == 0)
            {
                btnX20.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX20.ForeColor = Color.Black;
            }
            else
            {
                btnX20.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX20.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button18_Click(object sender, EventArgs e)
        {

            int flag = addMultiplicateur(30);
            if (flag == 0)
            {
                btnX30.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX30.ForeColor = Color.Black;
            }
            else
            {
                btnX30.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX30.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button19_Click(object sender, EventArgs e)
        {

            int flag = addMultiplicateur(50);
            if (flag == 0)
            {
                btnX50.Image = Properties.Resources.BtnEnjeuMini_Sel;
                btnX50.ForeColor = Color.Black;
            }
            else
            {

                btnX50.Image = Properties.Resources.BtnEnjeuMini_Desel;
                btnX50.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int flag = addMultiplicateur(100);
            if (flag == 0)
            {
                btnX100.Image = Properties.Resources.BtnEnjeuMini_Sel;

                btnX100.ForeColor = Color.Black;
            }
            else
            {
                btnX100.Image = Properties.Resources.BtnEnjeuMini_Desel;

                btnX100.ForeColor = Color.White;
            }
            showPrice(0);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || SelectedReunion == lsReunion[0])
                return;
            r = lsReunion[0];
            bisBtn.Hide();
            numHippo = 1;
            ReunionSelected(r);
            nouvelleCourseControle();
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || SelectedReunion == lsReunion[1])
                return;
            r = lsReunion[1];
            bisBtn.Hide();
            numHippo = 2;
            ReunionSelected(r);
            nouvelleCourseControle();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            int nbrBase = 0; string nomProduit = "";
            if (formulationProduits.Count == 0)
            {
                nbrBase = 5;
            }
            else
            {
            }
            if (formulationProduits.Count >= 1)
            {

                nbrBase = formulationProduits[0].NombreBase;
                nomProduit = formulationProduits[0].NomProduit;
            }

            champsTotalFlag = true;
            champsXFlag = false;
            
            int nbrCheveuxChoisis = getNbrPartantInCombinaison();
            int index = listeChoix.LastIndexOf("X");
            int index2 = listeChoix.LastIndexOf("TOTAL");
            int index3 = listeChoix.LastIndexOf("R");
            if (index == nbrBase - 1 && index2 == -1) { listeChoix.Add("TOTAL"); }
            else if (index == -1 && nbrCheveuxChoisis < nbrBase)
            {
                for (int i = nbrCheveuxChoisis; i < nbrBase; i++)
                {
                    listeChoix.Add("X");
                }
                listeChoix.Add("TOTAL");

            }
            else if (index != -1 && getNbrChevalJoue() < nbrBase)
            {
                for (int i = getNbrChevalJoue(); i < nbrBase; i++)
                {
                    listeChoix.Add("X");
                }
                listeChoix.Add("TOTAL");

            }

            else if (index2 != -1)
            {
                int count = listeChoix.Count - 1;
                while (count != nbrCheveuxChoisis - 1)
                {
                    listeChoix.RemoveAt(count);
                    count--;
                }
                champsTotalFlag = false;
            }
            else
            {
                showErrorMessage("Formulation incorrect", Color.Red);
                //txtbMessages.Text = "Formulation incorrect";
                //txtbMessages.Refresh();
                champsTotalFlag = false;
            }
            if (champsTotalFlag)
            {
                btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            }
            else
            {
                btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }
            getButtonsControls();
            showParis();
            showPrice(0);
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            Effacer();
        }
        private void Effacer()
        {
            clearPanelProduit();
            clearMessageZone();
            clearFormulationPartant();
            formulationProduits.Clear();
            getButtonsControls();
            if (ticket.ListeFormulation.Count > 0)
            {
                UpdateValiderBtn("Imprimer", true);
            }
            flagImprimerIsValid = 1;
            ImprimerFlag = 0;
            chexpressCount = 0;
            showParis();
            showPrice(0);
            expressBtnClicked = false;
            this.isChevalExpress = false;
            buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
            btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
            bisBtn.Enabled = true;
        }
        public void clearFormulationPartant(bool hideExpress = true)
        {

            champsXFlag = false;
            champsTotalFlag = false;
            formuleComplete = false;
            champsReduitFlag = false;
            StopBlinkChevalBtn();
            //**************************  hide bis button if express type selected
            if (IsExpress)
            {
                nouveauClientFlag = 0;
                BISControl();
                bisBtn.Hide();
            }
            //**************************************************************
            IsExpress = false;
            if (hideExpress)
            {
                panel18.Hide();
                panel19.Hide();
            }
            if (isExpressImprimed)
            {
                nombreParisExpress = 1;
                IsExpress = false;
                isExpressImprimed = false;
                nouveauClientFlag = 0;
                BISControl();
            }

            replaceFlag = -1;
            showHorses(myCourse);
            listeRChoisis.Clear();
            listeChoix.Clear();
            clearMultiplicators();
            btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
        }

        private void clearLastFormulation()
        {
            if (indiceNbrFormulation == 1)
            {
                if (ticket.ListeFormulation.Count > 0)
                {
                    ticket.ListeFormulation.RemoveAt(ticket.ListeFormulation.Count - 1);
                }

            }
            else if (indiceNbrFormulation > 1)
            {
                ticket.ListeFormulation.Clear();
            }
        }
        private void clearLastFormulation2()
        {

            if (indiceNbrFormulation == 1)
            {
                if (ticket.ListeFormulation.Count > 0)
                {
                    ticket.ListeFormulation.RemoveAt(ticket.ListeFormulation.Count - 1);
                }

            }
            else if (indiceNbrFormulation > 1)
            {
                ticket.ListeFormulation.Clear();
            }

        }
        public void clearPanelProduit()
        {
            showProduits(myCourse);
        }
        public void clearAllFormulation()
        {
            formulationProduits.Clear();
            ticket.ListeFormulation.Clear();
            showPrice(0);
        }

        private int addProduitControl(Produit produit)
        {
            int flag;
            flag = addProduit(produit);
            getButtonsControls();
            return flag;
        }

        private int addProduit(Produit produit)
        {

            int flag = 1;
            int nbrVariablesX = getNbrVariablesX();
            int chevaux = getNbrPartantInCombinaison();
            if (formulationProduits.Count != 0 && this.isChevalExpress)
            {
                if (formulationProduits.Contains(produit))
                {
                    formulationProduits.Remove(produit);
                    flag = 0;
                }
                return 0;
            }
            if (formulationProduits.Contains(produit))
            {
                formulationProduits.Remove(produit);
                flag = 0;
            }

            else
            {
                if (formulationProduits.Count >= 1)
                {
                    foreach (Produit p in formulationProduits)
                    {
                        if (p == null)
                            continue;
                        if (champsXFlag || champsTotalFlag)
                        {

                            if (produit.NombreBase != p.NombreBase || !produit.ChampX)
                            {
                                showErrorMessage("Champ interdit pour \n une combinaison de paris", Color.Red);
                                flag = 0;
                            }
                        }
                    }

                }
                else
                {
                    if (champsXFlag || champsTotalFlag)
                    {
                        Logger.Error(nbrVariablesX + chevaux + "/" + produit.NombreBase);
                        if (nbrVariablesX > produit.NombreBase - 1 || chevaux > produit.NombreBase || (nbrVariablesX + chevaux) > produit.NombreBase || (nbrVariablesX + chevaux) < produit.NombreBase)
                        {
                            showErrorMessage("Champ interdit pour \n une combinaison de paris", Color.Red);
                            flag = 0;
                        }
                    }
                }
                if (flag == 1)
                {
                    formulationProduits.Add(produit);
                }
            }
            return flag;
        }

        private void ClearDesignationsMode()
        {
            formuleComplete = false;
            btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            champsTotalFlag = false;
            btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            champsXFlag = false;
            btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
        }

        private void btnFComplete_Click(object sender, EventArgs e)
        {
            if (!formuleComplete)
            {
                formuleComplete = true;
                btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            }
            else
            {
                formuleComplete = false;
                btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }
            getButtonsControls();
            showPrice(0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            showPrice(0);
        }

        public void scan_ProcessCompleted(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(scan_ProcessCompleted),
                     new object[] { sender, e });
            }
            else
            {
                Logger.Info("Scan_ProcessCompleted: START");
                showErrorMessage("", Color.Red);
                try
                {
                    TLVTags montantTag;
                    ScanResultEventArgs se = e as ScanResultEventArgs;
                    string tlvData = scannerHandler.getScanedData(se.BitmapData);
                    string type = "";
                    Logger.Info("Scan_ProcessCompleted TLVDATA : " + tlvData);
                    if (tlvData != null)
                    {
                        int tempNumOffreVersion = ApplicationContext.CURRENT_NUMOFFRE_VERSION;

                        TLVHandler appTagsHandler = new TLVHandler(tlvData);
                        int code_reponse = TLVHandler.Response(tlvData);
                        TLVTags NumVersionTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_OFFRE);
                        if (NumVersionTag != null)
                        {
                            tempNumOffreVersion = int.Parse(Utils.bytesToHex(NumVersionTag.value));
                        }
                        TLVTags dataTicketTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET);
                        TLVTags voucherDataTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER);

                        decimal montantMois = 0;

                        
                        if (dataTicketTag != null)
                        {
                            TLVHandler dataTicketHandler = new TLVHandler(Utils.bytesToHex(dataTicketTag.value));
                            montantTag = dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT);
                            TLVTags typePaiementTag = dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_TYPE_PAIEMENT);
                            ApplicationContext.SOREC_PARI_MISE_TOTALE += decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                            montantMois = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);

                            if (typePaiementTag != null)
                            {
                                string typePaiement = Utils.HexToASCII(Utils.bytesToHex(typePaiementTag.value));
                                switch (typePaiement)
                                {
                                    case "TOTAL":
                                        type = "P";
                                        break;
                                    case "AVANCE":
                                        string Hippodromme = Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_HIPPODROME).value));
                                        int numReunion = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_NUMEROREUNION).value)));
                                        int numCourse = Int32.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_NUMEROCOURSE).value)));
                                        string heureCourse = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_DATECOURSE).value))).ToString("HH:mm");
                                        decimal montant_Avance = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT).value)), CultureInfo.InvariantCulture);
                                        ApplicationContext.LaunchGrosGainForm(scannerHandler.Ticket.IdTicket, scannerHandler.Ticket.IdServeur, scannerHandler.Ticket.DateReunion, numReunion, numCourse, Hippodromme, heureCourse, Convert.ToInt32(montant_Avance), false);
                                        break;
                                }
                            }
                            else
                            {
                                type = "A";
                            }

                        }
                        else if (voucherDataTag != null)
                        {
                            TLVHandler dataVoucherHandler = new TLVHandler(Utils.bytesToHex(voucherDataTag.value));
                            montantTag = dataVoucherHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_MONTANT);
                            ApplicationContext.SOREC_PARI_MISE_TOTALE += decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                            montantMois = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                            type = "V";
                        }
                        string msg = "";
                        switch (code_reponse)
                        {
                            case Constantes.RESPONSE_CODE_OK:
                                showPrice(-montantMois);
                                DetailsClientOperation op = null;
                                if (type.Equals("A") || type.Equals("P"))
                                {
                                    if (type.Equals("A"))
                                    {
                                        op = new DetailsClientOperation(scannerHandler.Ticket, montantMois, TypeOperation.Paiement, TypePaiment.ANNULATION);
                                        annulation += montantMois;
                                        annulationCount++;
                                        showErrorMessage("Annulation effectuée.", Color.Green);
                                    }
                                    else if (type.Equals("P"))
                                    {
                                        op = new DetailsClientOperation(scannerHandler.Ticket, montantMois, TypeOperation.Paiement, TypePaiment.PAIEMENT);
                                        paiement += montantMois;
                                        paimentCount++;
                                        showErrorMessage("Paiement effectué.", Color.Green);
                                    }
                                    JournalUtils.saveJournalPaiement(type, scannerHandler.Ticket, montantMois);
                                }

                                else if (type.Equals("V"))
                                {
                                    showErrorMessage("Paiement effectué.", Color.Green);
                                    paiement += montantMois;
                                    paimentCount++;
                                    op = new DetailsClientOperation(scannerHandler.Voucher, montantMois, TypeVoucher.ANNULATION);
                                    JournalUtils.saveJournalPaiementVoucher(scannerHandler.Voucher, montantMois);
                                }
                                if (op != null)
                                    ApplicationContext.DetailClient.Operations.Add(op);
                                UpdateDetailClientData();
                                showEcranClient = 2;
                                ApplicationContext.scanner.Eject(true);

                                break;
                            case Constantes.RESPONSE_CODE_KO_STATUT_PDV:
                                showErrorMessage("PDV est suspendu.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_TICKET_PERDANT:
                                SimpleMsg = "Ticket Perdant.";
                                showEcranClient = 0;
                                showErrorMessage("Ticket Perdant.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Ticket Perdant.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_TICKET_DEJA_ANNULE:
                                TLVTags numPDVTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                                string numPDV = numPDVTlv == null ? "-" : Utils.bytesToHex(numPDVTlv.value);
                                TLVTags posTerTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TERMINAL_POSITION);
                                string posTer = posTerTlv == null ? "-" : Utils.HexToASCII(Utils.bytesToHex(posTerTlv.value));
                                TLVTags dateAnnulTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET_DATEANNUL);

                                string dateAnnul = dateAnnulTlv == null || dateAnnulTlv.value == null ? "-"
                                    : Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dateAnnulTlv.value))).ToString("dd/MM/yyyy à HH:mm");
                                TLVTags montantTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT);
                                string montant = montantTlv == null ? "-" : Utils.HexToASCII(Utils.bytesToHex(montantTlv.value));
                                msg = "Ticket déjà annulé caisse " + numPDV + "." + posTer + " le " + dateAnnul + " pour " + montant + " DH";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Ticket Déjà Annulé ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_TICKET_DEJA_PAYE:
                                TLVTags numPDVPayeTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                                string numPDVPaye = numPDVPayeTlv == null ? "-" : Utils.bytesToHex(numPDVPayeTlv.value);
                                TLVTags posTerPayeTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TERMINAL_POSITION);
                                string posTerPaye = posTerPayeTlv == null ? "-" : Utils.HexToASCII(Utils.bytesToHex(posTerPayeTlv.value));
                                TLVTags datePaiementTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET_DATEPAIEMENT);
                                string datePaiement = datePaiementTlv == null || datePaiementTlv.value == null ? "-"
                                    : Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(datePaiementTlv.value))).ToString("dd/MM/yyyy à HH:mm");
                                TLVTags montantPaiTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT);
                                string montantPai = montantPaiTlv == null ? "-" : Utils.HexToASCII(Utils.bytesToHex(montantPaiTlv.value));
                                msg = "Ticket déjà payé caisse " + numPDVPaye + "." + posTerPaye + " le " + datePaiement + " pour " + montantPai + " DH";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Ticket Déjà Payé ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_LIEU_ANNULATION_NOT_ALLOWED:
                                TLVTags pdvTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                                string pdv = pdvTlv == null ? "-" : Utils.bytesToHex(pdvTlv.value);
                                ApplicationContext.scanner.Eject(false);
                                showErrorMessage("Annulation non autorisée\n ticket enregistré dans le\n point de vente " + pdv, Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_LIEU_NOT_ALLOWED:
                            case Constantes.RESPONSE_CODE_KO_LIEU_PAIEMENT_NOT_ALLOWED:
                                TLVTags pdvTlv1 = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                                string pdv1 = pdvTlv1 == null ? "un autre PDV" : "le PDV " + Utils.bytesToHex(pdvTlv1.value);
                                showErrorMessage("Paiement non autorisé.\nTicket enregistré dans " + pdv1, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_MODEANNULATION_AUTO:
                                showErrorMessage("Annulation automatique\nnon autorisée.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_INFORMATIONGAGNANT_DEJA_APPELE:
                                showErrorMessage("Gagnant déjà appelé.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_TICKET_NOT_FOUND:
                                showErrorMessage("Ticket introuvable.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_TICKET_INVALIDE:
                                msg = "Ticket invalide.";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Ticket invalide.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_CVNV_INVALIDE:
                            case Constantes.RESPONSE_CODE_KO_CVNT:
                                ApplicationContext.scanner.Eject(false);
                                showErrorMessage("Code CVN est invalide.", Color.Red);
                                JournalUtils.saveJournalPaiementErreur("Code CVNT est invalide.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_STATUT_TICKET_INVALIDE:
                                showErrorMessage("Statut ticket est invalide.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Statut ticket est invalide.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_INTROUVABLE:
                                showErrorMessage("Voucher introuvable.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Voucher introuvable ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_SUP_PLAFOND:
                                showErrorMessage("Montant voucher supérieur au plafond.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Montant voucher\nsupérieur au plafond ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_INF_SEUIL:
                                showErrorMessage("Montant Voucher inférieur au seuil.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Montant voucher\ninférieur au seuil ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_PAYE:
                                string numPDVVoucher = Utils.bytesToHex(appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV).value);
                                string posTerVoucher = Utils.HexToASCII(Utils.bytesToHex(appTagsHandler.getTLV(TLVTags.SOREC_DATA_TERMINAL_POSITION).value));
                                string dateVoucher = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(appTagsHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_DATEPAIEMENT).value))).ToString("dd/MM/yyyy à HH:mm");
                                string montantVoucher = Utils.HexToASCII(Utils.bytesToHex(appTagsHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_MONTANT).value));
                                msg = "Voucher déjà payé caisse " + numPDVVoucher + ". " + posTerVoucher + " le " + dateVoucher + " pour " + montantVoucher + " DH.";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Voucher Déja Payé", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_BLOQUE:
                                showErrorMessage("Voucher bloqué.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Voucher bloqué.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_INVALIDE:
                                msg = "Voucher Invalide.";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Voucher invalide.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PAIEMENT_INACTIF:
                                showErrorMessage("Paiment de ticket n'est pas activé.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Paiment de ticket \nn'est pas activé", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PAIEMENT_BLOQUE:
                                showErrorMessage("Paiement de ticket est bloqué.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Paiement de ticket \nest bloqué.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PAIEMENT_COURSE_INACTIF:
                                showErrorMessage("Course est inactive.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_VOUCHER_EXPIRE:
                                msg = "Voucher expiré.";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Voucher expiré ", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_AUTORISATION_GROS_GAIN:
                                msg = "Félicitation vous avez gagné.\nVeuillez s'adressez à l'agence la plus proche.";
                                SimpleMsg = msg;
                                showEcranClient = 0;
                                showErrorMessage(msg, Color.Red);
                                txtbMessages.Refresh();
                                ApplicationContext.scanner.Eject(false);
                                JournalUtils.saveJournalPaiementErreur("Terminal non autorisé à payer les gros gains.", r.NumReunion, myCourse.NumCoursePmu);
                                break;
                            case Constantes.RESPONSE_CODE_KO_STATUT_COURSE_INVALIDE:
                                showErrorMessage("Paiement non autorisé.\nVeuillez réessayer plus tard.", Color.Red);
                                _ = ApplicationContext.scanner.Eject(false);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PRODUIT_INTROUVABLE:
                                showErrorMessage("Paiement suspendu.\nVeuillez réessayer plus tard.", Color.Red);
                                _ = ApplicationContext.scanner.Eject(false);
                                break;
                            case 500:
                                showErrorMessage("Opération non aboutie." + ".\nVeuillez réessayer.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                            default:
                                showErrorMessage("Erreur inconnue: " + code_reponse + ".\nVeuillez contacter l'administration.", Color.Red);
                                ApplicationContext.scanner.Eject(false);
                                break;
                        }
                        if (ApplicationContext.CURRENT_NUMOFFRE_VERSION < tempNumOffreVersion)
                        {
                            Offre newOffre = offreControl.getDataOffreFromMT();
                            if (newOffre != null && !newOffre.IsEmpty)
                            {
                                ApplicationContext.SOREC_DATA_OFFRE = newOffre;
                                ReLoadView(new DataTerminalIndicesHolder(SelectedReunion.DateReunion, SDictionnaryUtils<DateTime, List<Reunion>>.getKeyByIndex(hashReunion, indiceHashReunion), SelectedReunion.NumReunion, myCourse.NumCoursePmu, TypeReload.INIT));
                                showErrorMessage("Attention.\nChangement d'offre.", Color.Red);
                            }
                            else
                            {
                                showErrorMessage("Attention.\nla nouvelle offre n'est pas chargée.", Color.Red);
                            }
                        }
                    }
                    else
                    {
                        Logger.Warn("Scan_ProcessCompleted: TLV Data null after scan");
                        showErrorMessage("Défaut de lecture du support", Color.Red);
                        ApplicationContext.scanner.Eject(false);
                    }

                }
                catch (Exception ex)
                {
                    showErrorMessage("Erreur technique.\nVeuillez contacter l'administration.", Color.Red);
                    var st = new StackTrace(ex, true);
                    var frame = st.GetFrame(0);
                    var line = frame.GetFileLineNumber();
                    JournalUtils.saveJournalMessage(string.Format("Une erreur technique s'est produite. {0}: [Form Principale, {1}]", ex.Message, line));
                    ApplicationContext.scanner.Eject(false);
                    Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
                }
                finally
                {
                    Logger.Info("Scan_ProcessCompleted: END");
                }
            }
        }

        public static void UpdateDetailClientData()
        {
            lastPaiement = paiement;
            lastPaiementCount = paimentCount;
            lastDistribution = distribution;
            lastDistributionCount = distributionCount;
            lastAnnulation = annulation;
            lastAnnulationCount = annulationCount;
            lastStotal = ApplicationContext.sTotal;
        }

        private void buttonReunions_Click(object sender, EventArgs e)
        {
            ShowReunionPanel();
        }
        private void ShowReunionPanel()
        {
            buttonReunions.BackgroundImage = Properties.Resources.BtnDesignation_Sel;
            buttonClient.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            buttonCourses.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            panelCheval.Hide();
            panelClient.Hide();
        }

        private void buttonCourses_Click(object sender, EventArgs e)
        {
            ShowDetailCourcePanel();
        }

        private void ShowDetailCourcePanel()
        {
            if (myCourse == null)
                return;
            _updateCoursesHorses();

            buttonReunions.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            buttonClient.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            buttonCourses.BackgroundImage = Properties.Resources.BtnDesignation_Sel;
            panelClient.Hide();
            panelCheval.Show();

            button18.Enabled = numCourse > 1;
            button18.BackgroundImage = numCourse > 1 ? Properties.Resources.BtnCoursePrec_Desel
                : Properties.Resources.BtnCoursePrec_Inactif;

            button19.Enabled = numCourse < r.ListeCourse.Count;
            button19.BackgroundImage = numCourse < r.ListeCourse.Count ? Properties.Resources.BtnCourseSuiv_Desel
                : Properties.Resources.BtnCourseSuiv_Inactif;
        }

        private void buttonClient_Click(object sender, EventArgs e)
        {
            ShowDetailClientPanel();
        }
        private void ShowDetailClientPanel()
        {
            dataGridView1.Columns[0].Width = 361;
            indexOperationDc = ApplicationContext.DetailClient.Operations.Count - 1;
            button1.Enabled = indexOperationDc > 0;
            button1.BackgroundImage = indexOperationDc > 0 ? Properties.Resources.BtnReunionPrecPetit_Desel
                : Properties.Resources.BtnReunionPrecPetit_Inactif;

            button2.Enabled = indexOperationDc < ApplicationContext.DetailClient.Operations.Count - 1;
            button2.BackgroundImage = indexOperationDc < ApplicationContext.DetailClient.Operations.Count - 1 ? Properties.Resources.BtnReunionSuivPetit_Desel
                : Properties.Resources.BtnReunionSuivPetit_Inactif;
            DisplayOpeartion(12, true);
            panelCheval.Hide();
            panelClient.Show();
            buttonReunions.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
            buttonClient.BackgroundImage = Properties.Resources.BtnDesignation_Sel;
            buttonCourses.BackgroundImage = Properties.Resources.BtnDesignation_Desel;
        }
        private void DisplayOpeartion(int limit, bool fromButtom = false)
        {
            dataGridView1.Rows.Clear();
            List<DetailsClientOperation> operationClient = ApplicationContext.DetailClient.Operations;
            if (operationClient.Count <= 0)
                return;
            int count = 0;
            bool cancel = false;
            if (fromButtom)
            {
                List<OperationLigne> newLignes = new List<OperationLigne>();
                for (int i = operationClient.Count; i > 0;i --)
                {
                    List<OperationLigne> opLignes = operationClient[i - 1].OperationLignes();
                    for(int j=opLignes.Count; j > 0;j--)
                    {
                        if (count == limit)
                        {
                            cancel = true;
                            break;
                        }
                        newLignes.Add(opLignes[j-1]);
                        count++;
                    }
                    if (count == limit || cancel)
                    {
                        break;
                    }
                }
                newLignes.Reverse();
                int index = 0;
                foreach (var opNL in newLignes)
                {
                    _addDataGridView(opNL, index);
                    index++;
                }
            }
            else
            {
                for (int i = indexOperationDc; i < operationClient.Count; i++)
                {
                    foreach (OperationLigne opL in operationClient[i].OperationLignes())
                    {
                        if (count == limit)
                        {
                            cancel = true;
                            break;
                        }
                        _addDataGridView(opL, count);
                        count++;
                    }
                    if (count == limit || cancel)
                    {
                        break;
                    }
                }
            }
        }

        private void _addDataGridView(OperationLigne opL,int index)
        {
            dataGridView1.Rows.Add(opL.Ligne);
            this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = opL.BackColor;
            this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = opL.ForeColor;
        }
        private Label[] _getChevalLabels(int numCheval)
        {
            Label[] chevalLabels = new Label[2];
            switch (numCheval)
            {
                case 1:
                    chevalLabels[0] = numCH1;
                    chevalLabels[1] = nameCH1;
                    break;
                case 2:
                    chevalLabels[0] = numCH2;
                    chevalLabels[1] = nameCH2;
                    break;
                case 3:
                    chevalLabels[0] = numCH3;
                    chevalLabels[1] = nameCH3;
                    break;
                case 4:
                    chevalLabels[0] = numCH4;
                    chevalLabels[1] = nameCH4;
                    break;
                case 5:
                    chevalLabels[0] = numCH5;
                    chevalLabels[1] = nameCH5;
                    break;
                case 6:
                    chevalLabels[0] = numCH6;
                    chevalLabels[1] = nameCH6;
                    break;
                case 7:
                    chevalLabels[0] = numCH7;
                    chevalLabels[1] = nameCH7;
                    break;
                case 8:
                    chevalLabels[0] = numCH8;
                    chevalLabels[1] = nameCH8;
                    break;
            }
            return chevalLabels;
        }

        private void button14_Click_2(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled || myCourse == null || myCourse.ListeHorses == null || myCourse.ListeHorses.Count < 1)
            {
                return;
            }
            button10.Enabled = true;
            button10.BackgroundImage = Properties.Resources.BtnReunionPrec_Desel;
            first += 8;
            _updateCoursesHorses();
            button14.Enabled = first + 8 < myCourse.ListeHorses.Count;
        }

        private void button10_Click_2(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled || myCourse == null || myCourse.ListeHorses == null || myCourse.ListeHorses.Count < 1)
                return;
            button14.Enabled = true;
            button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Desel;

            first -= 8;
            _updateCoursesHorses();
            if (first < 7)
            {
                button10.Enabled = false;
                first = 0;
            }
        }
        public List<string> getListNonPartants(Course c)
        {
            List<string> listNonPartant = new List<string>();
            foreach (Horse p in c.ListeHorses)
            {
                if (p.EstPartant.ToString().Equals(StatutPartant.NonPartant.ToString()))
                {
                    listNonPartant.Add(p.NumPartant.ToString());
                }
            }
            return listNonPartant;
        }

        private void _updateCoursesHorses()
        {
            List<string> nonPartans = getListNonPartants(myCourse);
            for (int i = 0; i < 8; i++)
            {
                Label[] chevalLabels = _getChevalLabels(i + 1);
                if (chevalLabels[0] != null && chevalLabels[1] != null)
                {
                    if (i + first < myCourse.ListeHorses.Count)
                    {
                        string numPart = "" + myCourse.ListeHorses[i + first].NumPartant;
                        chevalLabels[0].Text = numPart;
                        chevalLabels[1].Text = myCourse.ListeHorses[i + first].NomPartant;
                        if (nonPartans.Contains(numPart))
                        {
                            chevalLabels[0].Enabled = false;
                            chevalLabels[0].BackColor = Color.LightGray;
                        }
                        else
                        {
                            chevalLabels[0].Enabled = true;
                            chevalLabels[0].BackColor = Color.PaleTurquoise;
                        }
                    }
                    else
                    {
                        chevalLabels[0].Text = "";
                        chevalLabels[1].Text = "";
                        chevalLabels[0].Enabled = false;
                        chevalLabels[0].BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void buttonZ3_Click_1(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || SelectedReunion == lsReunion[2])
                return;
            r = lsReunion[2];
            bisBtn.Hide();
            numHippo = 3;
            ReunionSelected(r);
            nouvelleCourseControle();
        }

        private void UpdateReunionButton(Reunion r)
        {
            lprogramme.Text = "Programme Du  " + r.DateReunion.ToString("dd/MM/yyyy");
            lblHipo.Text = "  " + r.LibReunion + " ";
            labelCourseList.Text = "Courses de " + r.LibReunion;
            Bitmap BtnRang_Sel_edited = Properties.Resources.BtnRang_Sel_edited;
            Bitmap BtnRang_Desel_edited = Properties.Resources.BtnRang_Desel_edited;

            switch (numHippo)
            {
                case 1:
                    buttonZ1.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 2:
                    buttonZ2.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 3:
                    buttonZ3.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 4:
                    buttonZ4.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;
                    break;
                case 5:
                    buttonZ5.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    break;
            }
        }

        private void ReunionSelected(Reunion reunion)
        {
            reunion.ListeCourse.Sort((x, y) => x.NumCoursePmu.CompareTo(y.NumCoursePmu));
            lprogramme.Text = "Programme Du  " + reunion.DateReunion.ToString("dd/MM/yyyy");
            lblHipo.Text = "  " + reunion.LibReunion + " ";
            labelCourseList.Text = "Courses de " + reunion.LibReunion;
            r = reunion;

            Bitmap BtnRang_Sel_edited = Properties.Resources.BtnRang_Sel_edited;
            Bitmap BtnRang_Desel_edited = Properties.Resources.BtnRang_Desel_edited;

            switch (numHippo)
            {
                case 1:
                    buttonZ1.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 2:
                    buttonZ2.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 3:
                    buttonZ3.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;

                    break;
                case 4:
                    buttonZ4.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ5.BackgroundImage = BtnRang_Desel_edited;
                    break;
                case 5:
                    buttonZ5.BackgroundImage = BtnRang_Sel_edited;
                    buttonZ1.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ3.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ4.BackgroundImage = BtnRang_Desel_edited;
                    buttonZ2.BackgroundImage = BtnRang_Desel_edited;

                    break;
            }

            HippodromesClicked(reunion);
            richTextBox1.Clear();
            richTextBox1.Text = string.Format("{0} | Réunion {3}- Course {4}\n{1} {2}", r.LibReunion, r.DateReunion.ToString("dddd, dd MMMM yyyy"), myCourse.HeureDepartCourse.ToString("HH:mm"), r.NumReunion, myCourse.NumCoursePmu);
        }

        private void buttonZ4_Click(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || SelectedReunion == lsReunion[3])
                return;
            r = lsReunion[3];
            bisBtn.Hide();
            numHippo = 4;
            ReunionSelected(r);
            nouvelleCourseControle();
        }

        private void buttonZ5_Click_2(object sender, EventArgs e)
        {

            if (flagVerrouReunion == 1 || SelectedReunion == lsReunion[4])
                return;
            r = lsReunion[4];
            bisBtn.Hide();
            numHippo = 5;
            ReunionSelected(r);
            nouvelleCourseControle();
        }

        private void button22_Click(object sender, EventArgs e)
        {

            if (flagVerrouReunion == 0)
            {
                indiceReunion = numHippo - 1;
                flagVerrouReunion = 1;
                button22.BackgroundImage = Properties.Resources.BtnOption2_Sel;
            }
            else
            {
                flagVerrouReunion = 0;
                indiceReunion = 0;
                button22.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }


        }

        private void button21_Click_2(object sender, EventArgs e)
        {
            if (flagVerrouCourse == 0)
            {
                indiceCourse = numCourse - 1;
                flagVerrouCourse = 1;
                button21.BackgroundImage = Properties.Resources.BtnOption2_Sel;

            }
            else
            {
                indiceCourse = -1;
                flagVerrouCourse = 0;
                button21.BackgroundImage = Properties.Resources.BtnOption2_Desel;
            }

        }
        private void clearDetailClientData()
        {
            paiement = 0;
            paimentCount = 0;
            distributionCount = 0;
            distribution = 0;
            annulation = 0;
            annulationCount = 0;
        }
        private void buttonZNclient_Click(object sender, EventArgs e)
        {
            SimpleMsg = "BIENVENUE";
            showEcranClient = 0;
            UpdateTotalPrice(0);
            clearLastFormulation();
            clearAllFormulation();
            clearFormulationPartant();
            if (panelClient.Visible)
            {
                ShowReunionPanel();
            }
            try
            {
                r = lsReunion[indiceReunion];
                if (indiceCourse != -1)
                {
                    myCourse = r.ListeCourse[indiceCourse];
                    numCourse = indiceCourse + 1;
                }
                else
                {
                    myCourse = getFirstCourse(r);
                    int indexCourse = r.ListeCourse.FindIndex(c => c.Equals(myCourse));
                    numCourse = indexCourse == -1 ? 1 : indexCourse + 1;
                }
                numHippo = indiceReunion + 1;
                ReunionSelected(r);
                if (this.SelectedReunion == r)
                {
                    isCourseClicked = false;
                    produits = myCourse.ListeProduit;
                    showProduits(myCourse);
                    CourseSelected(myCourse);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception Nouveau Client: " + ex.Message);
            }
            finally
            {
                champsXFlag = false;
                champsTotalFlag = false;
                formuleComplete = false;
                champsReduitFlag = false;
                IsExpress = false;
                expressBtnClicked = false;
                this.isChevalExpress = false;
                panel18.Hide();
                panel19.Hide();
                nombreParisExpress = 1;
                replaceFlag = -1;
                nouveauClientFlag = 0;
                ApplicationContext.SOREC_PARI_MISE_TOTALE = 0;
                iTypePari = 5;
                listeChoix.Clear();
                formulationProduits.Clear();
                ticket.ListeFormulation.Clear();
                lastformulation.ListeFormulation.Clear();
                listeRChoisis.Clear();
                clearMultiplicators();
                BISControl();
                showErrorMessage("NOUVEAU CLIENT ", Color.Green);
                btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Desel;
                getButtonsControls();
                if(ApplicationContext.DetailClient.Operations.Count > 0)
                {
                    JournalUtils.saveJournalFinClient(ApplicationContext.sTotal);
                    ApplicationContext.DetailClient.Operations.Clear();
                    UpdateDetailClientData();
                    clearDetailClientData();
                }
                lastFormulationGender = "";
                ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
                ApplicationContext.sTotal = 0;
                showParis(0,true);
                controlExpresseGrs();
                showPrice(0);
                buttonZNclient.BackgroundImage = Properties.Resources.BtnFonctionRecupSoldeClient_Desel;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            clearMultiplicators();
            showPrice(0);
        }

        private void clearMultiplicators()
        {
            listeMultiplicateurs.Clear();
            btnX2.Image = btnX3.Image = btnX4.Image = btnX5.Image = btnX10.Image = btnX15.Image = btnX20.Image = btnX30.Image = btnX50.Image = btnX100.Image = Properties.Resources.BtnEnjeuMini_Desel;
            btnX2.ForeColor = btnX3.ForeColor = btnX4.ForeColor = btnX5.ForeColor = btnX10.ForeColor = btnX15.ForeColor = btnX20.ForeColor = btnX30.ForeColor = btnX50.ForeColor = btnX100.ForeColor = Color.White;
        }

        public void BISControl()
        {
            if (nouveauClientFlag == 0)
            {
                bisBtn.Hide();
            }
            else
                bisBtn.Show();
        }

        private void bisproduit()
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            bisBtn.Enabled = false;
            lastformulation.NumReunion = r.NumReunion;
            lastformulation.NumCourse = myCourse.NumCoursePmu;

            //********************************************************************************//
            //lastformulation.NumReunion = ticket.NumReunion;
            //lastformulation.NumCourse = ticket.NumCourse;
            if (lastformulation != null)
            {
                try
                {
                    int numReunion = lastformulation.NumReunion;
                    int numCourse = lastformulation.NumCourse;
                    if (lastformulation.ListeFormulation.Count != 0)
                    {
                        // Formulation formulation = lastformulation.ListeFormulation[lastformulation.ListeFormulation.Count-1];

                        List<Formulation> formulations = lastformulation.ListeFormulation;

                        Reunion reu = pariControles.getReunionByNuM(lsReunion, numReunion);

                        if (reu != null)
                        {
                            Course c = pariControles.getCourseByNum(reu, numCourse);

                            if (c != null)
                            {
                                int indice = pariControles.getindiceReunion(lsReunion, numReunion);
                                if (indice != -1)
                                {
                                    /* if (indice > 4 )
                                    {
                                        indice = indice - 5;
                                    }*/

                                    this.numHippo = indice + 1;
                                    ReunionSelected(reu);
                                    int indice2 = pariControles.getindiceCourse(reu, numCourse);
                                    for (int j = 0; j < formulations.Count; j++)
                                    {
                                        listeRChoisis.Clear();
                                        if (indice2 != -1)
                                        {
                                            this.numCourse = indice2 + 1;
                                            CourseSelected(c);
                                            Produit p = formulations[j].Produit;
                                            int flag = addProduit(p);
                                            // getButtonsControls();
                                            if (flag == 1)
                                            {
                                                controlMultiBtns();
                                                controlNonMultiBtns();
                                                switch (p.CodeProduit)
                                                {
                                                    case "TRC":

                                                        buttonZTiercé.Image = Properties.Resources.BtnParis_TierceSel;
                                                        break;
                                                    case "ML4":
                                                        multi4PB.Image = Properties.Resources.BtnParis_Multi4Sel;
                                                        break;
                                                    case "ML5":
                                                        multi5PB.Image = Properties.Resources.BtnParis_Multi5Sel;
                                                        break;
                                                    case "ML6":
                                                        multi6pb.Image = Properties.Resources.BtnParis_Multi6Sel;
                                                        break;
                                                    case "ML7":
                                                        multi7pb.Image = Properties.Resources.BtnParis_Multi7Sel;
                                                        break;
                                                    case "QUU":

                                                        buttonZQuarté.Image = Properties.Resources.BtnParis_QuarteSel;
                                                        break;
                                                    case "QAP":

                                                        buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlusSel;
                                                        break;
                                                    case "QIP":

                                                        buttonZQuintéPlus.Image = Properties.Resources.BtnParis_QuinteSel;
                                                        break;
                                                    case "JUP":

                                                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlaceSel;
                                                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                                                        this.isJumelePSelected = true;
                                                        buttonZJGP.Enabled = false;
                                                        break;
                                                    case "JUG":

                                                        buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnantSel;
                                                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                                                        this.isJumeleGSelected = true;
                                                        buttonZJGP.Enabled = false;
                                                        break;
                                                    case "PLA":

                                                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlaceSel;
                                                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                                                        this.isSimplePSelected = true;
                                                        buttonZSGP.Enabled = false;
                                                        break;
                                                    case "GAG":

                                                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnantSel;
                                                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                                                        this.isSimpleGSelected = true;
                                                        buttonZSGP.Enabled = false;
                                                        break; 
                                                    case "TRO":

                                                        buttonZTrio.Image = Properties.Resources.BtnParis_TrioSel;
                                                        break;

                                                    case "JUO":

                                                        buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdreSel;
                                                        flagJUO = 1;
                                                        break;

                                                }
                                                char[] delimiters = { ' ' };
                                                String[] designation = formulations[j].Designation.Split(delimiters);
                                                listeChoix.Clear();
                                                for (int i = 0; i < designation.Length; i++)
                                                {
                                                    int isSuccess = 0;
                                                    if (!designation[i].Equals(""))
                                                    {
                                                        bool success = Int32.TryParse(designation[i], out isSuccess);
                                                        if (success)
                                                        {
                                                            listeChoix.Add(isSuccess);
                                                        }
                                                        else
                                                        {
                                                            listeChoix.Add(designation[i]);//nok
                                                        }
                                                    }
                                                }
                                                // int nbrX = getNbrVariablesX();
                                                /* if (listeChoix.Contains("TOTAL"))
                                                 {
                                                     btnCTotal.Image = Properties.Resources.BtnOption2_Sel;
                                                 }*/

                                                if (formulations[j].FormComplete)
                                                {
                                                    btnFComplete.BackgroundImage = Properties.Resources.BtnOption2_Sel;
                                                    formuleComplete = true;
                                                }
                                                if (listeChoix.Contains("X") && !listeChoix.Contains("TOTAL"))
                                                {
                                                    btnFCX.BackgroundImage = Properties.Resources.BtnOption2_Sel;
                                                    this.champsXFlag = true;
                                                }

                                                if (listeChoix.Contains("TOTAL"))
                                                {
                                                    btnCTotal.BackgroundImage = Properties.Resources.BtnOption2_Sel;
                                                    champsTotalFlag = true;
                                                    champsXFlag = false;
                                                }


                                                int indiceR = listeChoix.IndexOf("R");
                                                int count = 0;
                                                int indicePartant = 0;
                                                foreach (Object obj in listeChoix)
                                                {
                                                    if (obj != null && !obj.Equals("X") && !obj.Equals("R") && !obj.Equals("TOTAL") && !obj.Equals(""))
                                                    {
                                                        indicePartant = pariControles.getindicePartant(c.ListeHorses, Int32.Parse(obj.ToString())) + 1;
                                                        // logger.addLog("partant " + (indicePartant+1),1);
                                                        if (indiceR != -1 && count > indiceR)
                                                        {
                                                            listeRChoisis.Add(Int32.Parse(obj.ToString()));
                                                            btnSelectionedWithR(indicePartant);
                                                        }
                                                        else
                                                        {
                                                            StopBlinkChevalBtn();
                                                            btnSelectioned(indicePartant);
                                                        }
                                                    }
                                                    count++;
                                                }
                                                getButtonsControls();
                                                controlExpresseGrs();
                                                showParis();
                                                showPrice(0);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                    UpdateValiderBtn("Valider", true);
                    buttonZValiderActif.Enabled = true;
                    ImprimerFlag = 1;
                }
                catch (Exception ex) 
                {
                    Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
                    Effacer();
                    bisBtn.Hide();
                    showErrorMessage("Formulation incorrecte", Color.Red);
                }
            }
        }

        private void btnannulation_Click(object sender, EventArgs e)
        {
            ApplicationContext.sTotal -= ApplicationContext.TEMP_TOTAL_BEFORE_PRINT;
            ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
            this.isChevalExpress = false;
            expressBtnClicked = false;
            nombreParisExpress = 1;
            chexpressCount = 0;
            clearLastFormulation();
            clearAllFormulation();
            clearPanelProduit();
            clearFormulationPartant();
            clearMessageZone();
            showErrorMessage("", Color.Red);
            getButtonsControls();
            showParis();
            showPrice(0);
            lastFormulationGender = "";
            count = 0;
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            if (!myCourse.IsRapportDisponible || !(sender as Control).Enabled)
                return;
            try
            {
                button15.Enabled = false;
                button15.BackgroundImage = Properties.Resources.BtnOption_Sel;
                button15.Refresh();
                RapportsControle controle = new RapportsControle();
                List<RapportCoteModel> rapports = controle.GetRapports(r, myCourse);
                if (rapports != null && rapports.Count > 0)
                {
                    ImprimerRapportService rapportService = new ImprimerRapportService();
                    rapportService.imprimerRapport(r, myCourse, rapports);
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
                showErrorMessage("Erreur technique.\nVeuillez contacter l'administration.", Color.Red);
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                JournalUtils.saveJournalMessage(string.Format("Une erreur technique s'est produite. {0}: [Form Principale, {1}]", ex.Message, line));
            }
            finally
            {
                button15.Enabled = true;
                button15.BackgroundImage = Properties.Resources.BtnOption_Desel;
                button15.Refresh();
            }

        }

        private void formulationCountControle()
        {
            if (ticket.ListeFormulation.Count == 0)
            {
                UpdateValiderBtn("Valider", false);
                flagImprimerIsValid = 0;
            }
            else
            {
                UpdateValiderBtn("Imrprimer", true);
                flagImprimerIsValid = 1;
            }

        }

        private void buttonZTiercé1_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("TRC");
            if (!IsExpress)
            {
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        buttonZTiercé.Image = Properties.Resources.BtnParis_TierceSel;
                    }
                    else
                    {
                        buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }

        private void getNbrPartantExpress(int nombreBase)
        {
            foreach (Control control in panel19.Controls)
            {
                control.Visible = false;
            }
            label15.Visible = true;
            int partantNumber = myCourse.ListePartant.Count - nombreBase;
            if (partantNumber < 0)
            {
                label13.Visible = true;
            }
            else
            {
                int start = nombreBase;
                label13.Visible = false;
                for (int j = 0; j < nombreBase; j++)
                {
                    listeChoix.Add("S");
                }
                for (int i = 1; i < (10 - nombreBase); i++)
                {
                    Button expressBtn = GetExpressButton(i);
                    if (expressBtn == null)
                    {
                        Logger.Warn(string.Format("ShowExpress: unrecognized Express button {0} ", i));
                        continue;
                    }
                    if (expressBtn != null)
                    {
                        if (i == 1)
                        {
                            expressBtn.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                        }
                        else
                        {
                            expressBtn.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                        }
                        expressBtn.ForeColor = Color.White;
                        expressBtn.Text = start.ToString();
                        expressBtn.Visible = true;
                    }
                    start++;
                    if (i == partantNumber + 1) break;
                }
                getButtonsControls();
                showParis();
                showPrice(0);
            }
            
        }

        private Button GetExpressButton(int numExpress)
        {
            switch (numExpress)
            {
                case 1:
                    return express1;
                case 2:
                    return express2;
                case 3:
                    return express3; 
                case 4:
                    return express4;
                case 5:
                    return express5;
                case 6:
                    return express6;
                case 7:
                    return express7;
                case 8:
                    return express8;
                default:
                    return null;
            }
        }
        private void buttonZQuarté1_Click(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("QUU");

            if (!IsExpress)
            {
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        buttonZQuarté.Image = Properties.Resources.BtnParis_QuarteSel;
                    }
                    else
                    {
                        buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZQuartéPlus1_Click(object sender, EventArgs e)
        {


            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("QAP");
            if (!IsExpress)
            {
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlusSel;
                        if (isChevalExpress)
                        {
                            this.HideBaseProducts();
                        }
                    }
                    else
                    {
                        buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZQuintéPlus1_Click(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("QIP");
            if (!IsExpress)
            {
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZQuintéPlus.Image = Properties.Resources.BtnParis_QuinteSel;
                    }
                    else
                    {
                        buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }
        private void buttonZJGP1_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("JUP");
            Produit produit2 = getProduitByName("JUG");
            if (!IsExpress)
            {
                if (flagJumeleIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n non ordre ", Color.Red);
                    return;
                }

                if (produit != null && produit2 != null)
                {
                    int flag = addProduitControl(produit);
                    int flag2 = addProduitControl(produit2);
                    ImprimerFlag = 1;
                    if (flag == 1 && flag2 == 1)
                    {
                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGPSel;
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace_Inactif;
                        buttonZJP.Enabled = false;
                        buttonZJG.Image = (flagJUO == 1) ? Properties.Resources.BtnParis_JumeleOrdre_Inactif : Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                        buttonZJG.Enabled = false;
                    }
                    else
                    {
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace;
                        buttonZJP.Enabled = true;
                        buttonZJG.Image = (flagJUO == 1) ? Properties.Resources.BtnParis_JumeleOrdre : buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant;
                        buttonZJG.Enabled = true;
                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZJP1_Click(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("JUG");

            if (!IsExpress)
            {
                if (flagJumeleIsValid == 0 && flagJUO != 1)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n non ordre ", Color.Red);
                    return;
                }

                if (flagJUO == 1)
                {
                    produit = getProduitByName("JUO");
                }

                else
                {
                    produit = getProduitByName("JUG");

                }

                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        if (flagJUO == 1)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdreSel;
                            buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                            //Button for simple G/P
                            this.isJumeleGSelected = true;
                            buttonZJGP.Enabled = false;
                            //buttonZJGP.Enabled = true;
                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnantSel;
                            buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                            //Button for simple G/P
                            this.isJumeleGSelected = true;
                            buttonZJGP.Enabled = false;
                        }

                    }
                    else
                    {
                        if (flagJUO == 1)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre;
                            this.isJumeleGSelected = false;
                            if (!isJumelePSelected)
                            {
                                buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP;
                                buttonZJGP.Enabled = true;
                            }
                            formulationCountControle();
                            getButtonsControls();

                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant;
                            this.isJumeleGSelected = false;
                            if (!isJumelePSelected)
                            {
                                buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP;
                                buttonZJGP.Enabled = true;
                            }
                            formulationCountControle();
                            getButtonsControls();

                        }
                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }


            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void controlNonMultiBtns()
        {
            if (formulationProduits == null || formulationProduits.Count < 1)
            {
                showProduits(myCourse);
            }
            bool isCourseEnabled = myCourse != null && (myCourse.Statut == StatutCourse.DepartDansXX ||
                       myCourse.Statut == StatutCourse.EnVente ||
                       myCourse.Statut == StatutCourse.PreDepart);
            foreach (Produit pr in formulationProduits)
            {
               
                if (pr.IsMulti()   )
                {
                    buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce_Inactif;
                    buttonZTiercé.Enabled = false;
                    buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte_Inactif;
                    buttonZQuarté.Enabled = false;
                    buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus_Inactif;
                    buttonZQuartéPlus.Enabled = false;
                    buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte_Inactif;
                    buttonZQuintéPlus.Enabled = false;
                    buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace_Inactif;
                    buttonZJP.Enabled = false;
                    buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                    buttonZJG.Enabled = false;  
                    buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace_Inactif;
                    buttonZSP.Enabled = false;
                    buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                    buttonZSG.Enabled = false;
                    buttonZTrio.Image = Properties.Resources.BtnParis_Trio_Inactif;
                    buttonZTrio.Enabled = false;
                    bool juo = myCourse != null && myCourse.ListeProduit.Where(pp => pp.CodeProduit == "JUO").FirstOrDefault() != null;
                    buttonZJG.Image = juo ? Properties.Resources.BtnParis_JumeleOrdre_Inactif
                        : Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                    buttonZJG.Enabled = false; 
                    buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                    buttonZJGP.Enabled = false;
                    buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                    buttonZSGP.Enabled = false;
                    break;
                }
                
                else
                {
                    bool enabled = pr.Statut == StatutProduit.EnVenteProduit
                   && isCourseEnabled;
                    switch (pr.CodeProduit)
                    {
                        case "TRC":
                            buttonZTiercé.Image = enabled ?
                               Properties.Resources.BtnParis_TierceSel : Properties.Resources.BtnParis_Tierce_Inactif;
                            buttonZTiercé.Enabled = enabled;
                            break;
                        case "QUU":
                            buttonZQuarté.Image = enabled ?
                               Properties.Resources.BtnParis_QuarteSel : Properties.Resources.BtnParis_Quarte_Inactif;
                            buttonZQuarté.Enabled = enabled;
                            break;
                        case "QAP":
                            buttonZQuartéPlus.Image = enabled ?
                               Properties.Resources.BtnParis_QuartePlusSel : Properties.Resources.BtnParis_QuartePlus_Inactif;
                            buttonZQuartéPlus.Enabled = enabled;
                            break;
                        case "QIP":
                            buttonZQuintéPlus.Image = enabled ?
                               Properties.Resources.BtnParis_Quinte : Properties.Resources.BtnParis_Quinte_Inactif;
                            buttonZQuintéPlus.Enabled = enabled;
                            break;
                        case "JUP":
                            buttonZJP.Image = enabled ?
                               Properties.Resources.BtnParis_JumelePlaceSel : Properties.Resources.BtnParis_JumelePlace_Inactif;
                            buttonZJP.Enabled = enabled;
                            break;
                        case "JUG":
                            buttonZJG.Image = enabled ?
                               Properties.Resources.BtnParis_JumeleGagnantSel : Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                            buttonZJG.Enabled = enabled;
                            break; 
                        case "PLA":
                            buttonZSP.Image = enabled ?
                               Properties.Resources.BtnParis_SimplePlaceSel : Properties.Resources.BtnParis_SimplePlace_Inactif;

                            buttonZSP.Enabled = enabled;
                            break;
                        case "GAG":
                            buttonZSG.Image = enabled ?
                               Properties.Resources.BtnParis_SimpleGagnantSel : Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                            buttonZSG.Enabled = enabled;
                            break;
                        case "TRO":
                            buttonZTrio.Image = enabled ?
                               Properties.Resources.BtnParis_TrioSel : Properties.Resources.BtnParis_Trio_Inactif;
                            buttonZTrio.Enabled = enabled;
                            break;

                        case "JUO":
                            buttonZJG.Image = enabled ?
                               Properties.Resources.BtnParis_JumeleOrdreSel : Properties.Resources.BtnParis_JumeleOrdre_Inactif;
                            buttonZJG.Enabled = enabled;
                            break;

                       
                    }
                }

            }            
        }

        private void controlExpresseGrs()
        {
            if (formulationProduits == null || formulationProduits.Count < 1)
            {
                if(buttonZExpress.Enabled)
                {

                }
                else
                {
                    buttonZExpress.Enabled = true;
                    btnCHExpress.Enabled = true;
                }
                
            }
            foreach (Produit pr in formulationProduits)
            {
                if (!pr.ChevalExpress)
                {
                    buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Inactif;
                    btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Inactif;
                    buttonZExpress.Enabled = false;
                    btnCHExpress.Enabled = false;
                    break;
                }
            }
        }
        private void controlMultiBtns()
        {
            if (formulationProduits == null || formulationProduits.Count < 1)
            {
                showProduits(myCourse);
            }
            foreach (Produit pr in formulationProduits)
            {
                if (pr == null)
                    continue;

                if (!pr.IsMulti())
                {
                    multi4PB.Image = Properties.Resources.BtnParis_Multi4_Inactif;
                    multi5PB.Image = Properties.Resources.BtnParis_Multi5_Inactif;
                    multi6pb.Image = Properties.Resources.BtnParis_Multi6_Inactif;
                    multi7pb.Image = Properties.Resources.BtnParis_Multi7_Inactif;
                    multi4PB.Enabled = false;
                    multi5PB.Enabled = false;
                    multi6pb.Enabled = false;
                    multi7pb.Enabled = false;
                    break;
                }
                else
                {
                    bool enabled = pr.Statut == StatutProduit.EnVenteProduit
                   && (myCourse.Statut == StatutCourse.DepartDansXX ||
                       myCourse.Statut == StatutCourse.EnVente ||
                       myCourse.Statut == StatutCourse.PreDepart);
                    switch (pr.CodeProduit)
                    {
                        case "ML4":
                            multi4PB.Image = enabled ?
                                Properties.Resources.BtnParis_Multi4Sel : Properties.Resources.BtnParis_Multi4_Inactif;
                            multi4PB.Enabled = enabled;
                            break;
                        case "ML5":
                            multi5PB.Image = enabled ?
                                Properties.Resources.BtnParis_Multi5Sel : Properties.Resources.BtnParis_Multi5_Inactif;
                            multi5PB.Enabled = enabled;
                            break;
                        case "ML6":
                            multi6pb.Image = enabled ?
                                Properties.Resources.BtnParis_Multi6Sel : Properties.Resources.BtnParis_Multi6_Inactif;
                            multi6pb.Enabled = enabled;
                            break;
                        case "ML7":
                            multi7pb.Image = enabled ?
                                Properties.Resources.BtnParis_Multi7Sel : Properties.Resources.BtnParis_Multi7_Inactif;
                            multi7pb.Enabled = enabled;
                            break;
                    }
                    
                }
            }
        }

        private void buttonZJP1_Click_1(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("JUP");

            if (!IsExpress)
            {
                if (flagJumeleIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n non ordre ", Color.Red);
                    return;
                }

                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlaceSel;
                        buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                        this.isJumelePSelected = true;
                        buttonZJGP.Enabled = false;
                    }
                    else
                    {
                        buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace;
                        this.isJumelePSelected = false;
                        if (!this.isJumeleGSelected)
                        {
                            buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP;
                            buttonZJGP.Enabled = true;
                        }

                        formulationCountControle();
                        getButtonsControls();


                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZSGP1_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("PLA");
            Produit produit2 = getProduitByName("GAG");
            if (!IsExpress)
            {
                if (flagSimpleIsValid == 0)
                {
                    showErrorMessage("Les champs ne sont pas \n autorisés pour le pari \n Simple ", Color.Red);
                    return;
                }

                if (produit != null && produit2 != null)
                {
                    int flag = addProduitControl(produit);
                    int flag2 = addProduitControl(produit2);
                    ImprimerFlag = 1;
                    if (flag == 1 && flag2 == 1)
                    {
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGPSel;
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace_Inactif;
                        buttonZSP.Enabled = false;
                        buttonZSG.Enabled = false;
                    }
                    else
                    {
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP;
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant;
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace;
                        buttonZSP.Enabled = true;
                        buttonZSG.Enabled = true;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }

            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);

        }

        private void buttonZSP1_Click(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("PLA");

            if (!IsExpress)
            {
                if (flagSimpleIsValid == 0)
                {
                    showErrorMessage("Les champs ne sont pas \n autorisés pour le pari \n PLACE ", Color.Red);
                    return;
                }
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlaceSel;
                        this.isSimplePSelected = true;
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                        buttonZSGP.Enabled = false;
                    }
                    else
                    {
                        buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace;
                        this.isSimplePSelected = false;
                        if (!isSimpleGSelected)
                        {
                            buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP;
                            buttonZSGP.Enabled = true;
                        }
                        formulationCountControle();
                        getButtonsControls();

                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZSG1_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("GAG");

            if (!IsExpress)
            {
                if (flagSimpleIsValid == 0)
                {
                    showErrorMessage("Les champs ne sont pas \n autorisés pour le pari \n GAGNANT ", Color.Red);
                    return;
                }

                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnantSel;
                        this.isSimpleGSelected = true;
                        //Button for simple G/P
                        buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                        buttonZSGP.Enabled = false;

                    }
                    else
                    {
                        buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant;
                        this.isSimpleGSelected = false;
                        if (!isSimplePSelected)
                        {
                            buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP;
                            buttonZSGP.Enabled = true;
                        }
                        formulationCountControle();
                        getButtonsControls();

                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
                btnFComplete.Hide();
                formuleComplete = false;
            }
            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void buttonZTrio1_Click(object sender, EventArgs e)
        {

            bisBtn.Hide();
            clearMessageZone();
            Produit produit = getProduitByName("TRO");

            if (!IsExpress)
            {
                if (flagJumeleIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisées pour les produits \n non ordre ", Color.Red);
                    return;
                }
                int flag = addProduitControl(produit);

                if (produit != null)
                {
                    //?UpdateValiderBtn("Valider", true);
                    ImprimerFlag = 1;
                    if (flag == 1)
                    {
                        buttonZTrio.Image = Properties.Resources.BtnParis_TrioSel;
                    }
                    else
                    {
                        buttonZTrio.Image = Properties.Resources.BtnParis_Trio;
                        formulationCountControle();
                        getButtonsControls();

                    }
                }
            }
            else if (produit != null)
            {
                int flag = addProduitControl(produit);
                //?UpdateValiderBtn("Valider", true);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }

            }

            controlMultiBtns();
            controlExpresseGrs();
            showPrice(0);
        }

        private void UpdateValiderBtn(string msg, bool activate)
        {
            buttonZValiderInactif.Text = activate ? "" : msg;
            buttonZValiderActif.Text = activate ? msg : "";
            buttonZValiderInactif.Visible = !activate;
            buttonZValiderActif.Visible = activate;
            buttonZValiderActif.Refresh();
            buttonZValiderInactif.Refresh();
        }

        private static int count = 0;
        private static string lastFormulationGender = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            clearMessageZone();
            buttonZValiderActif.Enabled = false;
            bisBtn.Enabled = false;
            UpdateValiderBtn("En cours...", false);

            //showErrorMessage("", Color.Red);
            if (flagImprimerIsValid == 0)
            {
                showErrorMessage(" Formulation Incorrecte ", Color.Red);
                UpdateValiderBtn("Valider", false);
            }
            // cas de validaton 
            else if (ImprimerFlag == 1)
            {
                this.isSimpleGSelected = false;
                this.isSimplePSelected = false;
                this.isJumeleGSelected = false;
                this.isJumelePSelected = false;

                long total = 0;
                List<Formulation> formulations = getFormulations();
                if (formulations.Count == 0 && lastformulation.ListeFormulation.Count > 0)
                {
                    for (int i = 0; i < lastformulation.ListeFormulation.Count; i++)
                    {
                        Formulation temp = new Formulation(lastformulation.ListeFormulation[i]);
                        formulations.Add(new Formulation(temp));

                    }
                }
                lastformulation.ListeFormulation.Clear();
                for (int i = 0; i < formulations.Count; i++)
                {
                    if (isChevalExpress)
                    {
                        string[] selectedPrtant = formulations[i].Designation.Split(' ');
                        if (selectedPrtant.Length > myCourse.ListePartant.Count)
                        {
                            showErrorMessage("Insufisant Prtants pour cette Operation", Color.Red);
                            Application.DoEvents();
                            buttonZValiderActif.Enabled = true;
                            bisBtn.Enabled = true;
                            UpdateValiderBtn("Valider", true);
                            chexpressCount = 0;
                            btnCHExpress.Hide();
                            return;
                        }
                    }
                    total += formulations[i].MiseTotal;
                    if (isChevalExpress)
                        formulations[i].ChevalExpress = true;

                    if (formulations[i].MiseTotal >= formulations[i].Produit.EnjeuMax)
                    {
                        showErrorMessage("Attention : la transaction \n dépasse l'Enjeu Max \n autorisé pour " + formulations[i].Produit.NomProduit, Color.Red);
                        Application.DoEvents();
                        buttonZValiderActif.Enabled = true;
                        bisBtn.Enabled = true;
                        UpdateValiderBtn("Valider", true);
                        return;
                    }
                    lastformulation.ListeFormulation.Add(formulations[i]);
                }
                if (total > ConfigUtils.ConfigData.SeuilAlerte)
                {
                    using (SeuilConfirmation seuilConfirmation = new SeuilConfirmation(total))
                    {
                        seuilConfirmation.StartPosition = FormStartPosition.CenterScreen;
                        seuilConfirmation.ShowDialog();
                        if (!(Boolean)seuilConfirmation.Tag)
                        {
                            Effacer();
                        }
                        else
                        {
                            ApplicationContext.TEMP_TOTAL_BEFORE_PRINT += total;
                            readyToPrint(formulations);
                        }
                    }
                }
                else
                {
                    ApplicationContext.TEMP_TOTAL_BEFORE_PRINT += total;
                    readyToPrint(formulations);
                }
            }
            else
            {
                imprimer();
                count = 0;
                lastFormulationGender = "";
            }
            Application.DoEvents();
            bisBtn.Enabled = true;
            controlExpresseGrs();
            buttonZValiderActif.Enabled = true;
        }

        private void imprimer()
        {
            
            ApplicationContext.TEMP_TOTAL_BEFORE_PRINT = 0;
            flagImprimerIsValid = 0;
            //.buttonZValiderActif.Image = Properties.Resources.BtnValider_Inactif;
            List<Formulation> produitBase = new List<Formulation>();
            List<Formulation> produitEvent = new List<Formulation>();
            List<Formulation> produitAutre = new List<Formulation>();  
            ticket.NumReunion = r.NumReunion;
            ticket.DateReunion = r.DateReunion;
            ticket.NumCourse = myCourse.NumCoursePmu;
            for (int i = 0; i < ticket.ListeFormulation.Count; i++)
            {
                if (ticket.ListeFormulation[i].Designation.Contains("S"))
                {
                    if (ticket.ListeFormulation[i].ChevalExpress)
                    {
                        ticket.ListeFormulation[i].Designation = TlvUtlis.getChevalExpressDesignation(ticket.ListeFormulation[i], ticket.NumReunion, ticket.NumCourse, ticket.DateReunion);
                        ticket.ListeFormulation[i].ListSindexes = new List<int>(TlvUtlis.listSindexes);
                        TlvUtlis.listSindexes.Clear();
                    }
                    else
                    {
                        ticket.ListeFormulation[i].Designation = TlvUtlis.getExpressDesignation(ticket.ListeFormulation[i], ticket.NumReunion, ticket.NumCourse,ticket.DateReunion);
                        ticket.ListeFormulation[i].FormuleExpress = true;
                    }
                }
                if (ticket.ListeFormulation[i].Produit.Genre.Equals("EVENEMENT"))
                {
                    produitEvent.Add(ticket.ListeFormulation[i]);
                }
                else if (ticket.ListeFormulation[i].Produit.Genre.Equals("AUTRE"))
                {
                    produitAutre.Add(ticket.ListeFormulation[i]);
                }
                else if (ticket.ListeFormulation[i].Produit.Genre.Equals("BASE"))
                {
                    produitBase.Add(ticket.ListeFormulation[i]);
                }
         
            }
            bool isPrinted = true;
            if (produitEvent.Count > 0)
            {
                isPrinted = imprimerTicket(produitEvent);
            }
            if (isPrinted && produitBase.Count > 0)
            {
                isPrinted = imprimerTicket(produitBase);
            }
            if (isPrinted && produitAutre.Count > 0)
            {
                imprimerTicket(produitAutre);
            }
            this.isChevalExpress = false;
            this.isChevalExpress = false;
            ImprimerFlag = 1;
            clearAllFormulation();
            clearFormulationPartant();
            getButtonsControls();
            showParis(0,true);
            showPrice(0);
        }


        private void UpdateTotalPrice(decimal unitPrice)
        {
            ApplicationContext.sTotal += unitPrice;
            if (ApplicationContext.sTotal == 0)
            {
                panel4.BackgroundImage = Properties.Resources.SoldeClientJaune;
                richTextBox4.Text = "Vous recevez\n\n" + ApplicationContext.sTotal.ToString() + "DH";
                _ecranClientPrice = "Vous devez: " + ApplicationContext.sTotal.ToString() + "DH";
                buttonZMPayment.BackgroundImage = Properties.Resources.BtnFonctionGrand_Inactif;
                buttonZMPayment.ForeColor = Color.Black;
                buttonZMPayment.Enabled = false;
            }
            else if (ApplicationContext.sTotal > 0)
            {
                panel4.BackgroundImage = Properties.Resources.SoldeClientJaune;
                richTextBox4.Text = "Vous recevez\n\n" + Math.Abs(ApplicationContext.sTotal).ToString() + "DH";
                _ecranClientPrice = "Vous devez: " + Math.Abs(ApplicationContext.sTotal).ToString() + "DH";
                ApplicationContext.SOREC_PARI_MISE_TOTALE = 0;
                buttonZMPayment.BackgroundImage = Properties.Resources.BtnFonctionGrand_Inactif;
                buttonZMPayment.ForeColor = Color.Black;
                buttonZMPayment.Enabled = false;
            }
            else
            {
                buttonZMPayment.Enabled = true;
                buttonZMPayment.BackgroundImage = Properties.Resources.BtnFonctionGrand_Desel;
                buttonZMPayment.ForeColor = Color.White;
                panel4.BackgroundImage = Properties.Resources.SoldeClientVert;
                richTextBox4.Text = "Vous devez\n\n" + Math.Abs(ApplicationContext.sTotal).ToString() + "DH";
                _ecranClientPrice = "Vous recevez: " + Math.Abs(ApplicationContext.sTotal).ToString() + "DH";
            }
            richTextBox4.Refresh();
            panel4.Refresh();
            buttonZMPayment.Refresh();
        }

        public void readyToPrint(List<Formulation> formulations)
        {
            string temp = lastFormulationGender;
            bool isContainBase = false;
            bool isContainEvent = false;
            for (int i = 0; i < formulations.Count; i++)
            {
                ticket.ListeFormulation.Add(formulations[i]);
                lastFormulationGender = formulations[i].Produit.Genre;
                if (formulations[i].Produit.Genre.Equals("BASE"))
                    isContainBase = true;
                if (formulations[i].Produit.Genre.Equals("EVENEMENT"))
                    isContainEvent = true;
                //  lastformulation.ListeFormulation.Add(ticket.ListeFormulation[i]);
                UpdateTotalPrice(formulations[i].MiseTotal);
            }
            if (formulations.Count > 0)
            {
                if (formulations[formulations.Count - 1].Designation.Contains("S"))
                    bisBtn.Hide();
                else
                    bisBtn.Show();
            }
            indiceNbrFormulation = formulations.Count;
            UpdateValiderBtn("Imprimer", true);
            ImprimerFlag = 0;
            nouveauClientFlag = 1;
            if (temp.Equals(""))
            {
                count++;
            }
            else
            {
                if (!temp.Equals(lastFormulationGender)) count = 4;
                else count++;
            }
            if (isContainEvent && isContainBase)
            {
                count = 4;
            }
            if (ticket.ListeFormulation.Count >= 4) count = 4;
            clearPanelProduit();
            if (IsExpress || isChevalExpress)
            {
                if (ticket.ListeFormulation.Count < 4)
                {
                    clearFormulationPartant();
                    count = ticket.ListeFormulation.Count;
                }
                else
                {
                    clearFormulationPartant(false);
                    count = 4;
                }
            }
            else
            {
                clearFormulationPartant(false);
            }
            clearMessageZone();
            showParis();
            showPrice(0);
            if (expressBtnClicked)
            {
                buttonZExpress.Enabled = true;
                expressBtnClicked = false;
                buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
            }
            buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
            btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
            this.isChevalExpress = false;
            button5.Hide();
            formulationProduits.Clear();
            if (count == 4)
            {
                UpdateValiderBtn("En cours...", false);
                imprimer();
                count = 0;
                lastFormulationGender = "";
            }
            nombreParisExpress = 1;
            if (panelClient.Visible)
            {
                ShowReunionPanel();
            }
            chexpressCount = 0;
        }

        private void buttonZJournal_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchJournalForm();
        }

        private void buttonZGestClients_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            if (ApplicationContext.IsAutJeu)
                ApplicationContext.LaunchCréationClientForm();
            else
                showErrorMessage("Gestion des clients\nnon autorisée.", Color.Red);
        }

        private void buttonZMenu_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaissePreposeForm();
        }

        private void buttonZMPayment_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchClientForm();
        }

        private void buttonZDClient_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            showEcranClient = 2;
            if (lastStotal >= 0)
            {
                richTextBox4.Text = "Rappel: Vous recevez\n\n" + lastStotal.ToString() + "DH";
            }
            else 
            {
                richTextBox4.Text = "Rappel: Vous devez\n\n" + Math.Abs(lastStotal).ToString() + "DH";
            }
            richTextBox4.Refresh();
            // detail client
            if (ApplicationContext.DetailClient.Operations.Count > 0)
                ShowDetailClientPanel();
        }

        private void showReunion()
        {
            lsReunion = SDictionnaryUtils<DateTime, List<Reunion>>.getValueByIndex(hashReunion, indiceHashReunion);
            r = lsReunion[0];
            if (r.ListeCourse.Count > 0)
            {
                r.ListeCourse.Sort((x, y) => x.NumCoursePmu.CompareTo(y.NumCoursePmu));
                UpdateAllReunionsButton(lsReunion);
                ReunionSelected(r);
                BISControl();
            }
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || flagVerrouCourse == 1)
                return;
            if (indiceHashReunion > 0 && indiceHashReunion <= hashReunion.Count)
            {
                indiceHashReunion--;
                numHippo = 1;
                showReunion();
                lprogramme.Text = "Programme Du  " + r.DateReunion.ToString("dd/MM/yyyy");
            }
            pictureBox4.Enabled = true;
            pictureBox4.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Desel;
            pictureBox3.Enabled = indiceHashReunion > 0;
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            if (flagVerrouReunion == 1 || flagVerrouCourse == 1)
                return;
            if (indiceHashReunion >= 0 && indiceHashReunion < hashReunion.Count - 1)
            {
                indiceHashReunion++;
                numHippo = 1;
                showReunion();
                lprogramme.Text = "Programme Du  " + r.DateReunion.ToString("dd/MM/yyyy");

            }
            pictureBox3.Enabled = true;
            pictureBox3.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Desel;
            pictureBox4.Enabled = indiceHashReunion < hashReunion.Count - 1;
            // down
        }

        private void btnCHExpress_Click(object sender, EventArgs e)
        {
            foreach (Produit p in formulationProduits)
            {
                if (!p.ChevalExpress)
                {
                    showErrorMessage("Produit " + p.CodeProduit + " n'a pas de formule express", Color.Red);
                    return;
                }
            }

            IsExpress = false;
            this.isChevalExpress = true;
            this.HideBaseProducts();
            controlChExpBtns();
            if (listeChoix.Count >= 8)
            {
                showErrorMessage("Vous ne pouvez pas designer plus de 8 chevaux", Color.Red);
                return;
            }
            if (chexpressCount < myCourse.ListePartant.Count)
            {
                listeChoix.Add("S");
                chexpressCount++;
                showParis();
                showPrice(0);
                getButtonsControls();
            }
            else
            {
                showErrorMessage("Nombre Partants insuffisant ", Color.Red);
                return;
            }
        }
        private void HideBaseProducts()
        {
        /*  paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel; */
            btnFCX.Hide();
            btnFComplete.Hide();
            if (myCourse == null)
                return;
            foreach (Produit p in myCourse.ListeProduit)
            {
                if (p == null)
                    continue;

                switch (p.CodeProduit)
                {
                    case "TRC":
                        if (!p.ChevalExpress )
                        {
                            buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce_Inactif;
                            buttonZTiercé.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            buttonZTiercé.Image = Properties.Resources.BtnParis_TierceSel;
                            buttonZTiercé.Enabled = true;
                        }
                    /*    else
                        {
                            buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce;
                            buttonZTiercé.Enabled = true;
                        } */
                        break;

                    case "QUU":
                        if (!p.ChevalExpress)
                        {
                            buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte_Inactif;
                            buttonZQuarté.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            buttonZQuarté.Image = Properties.Resources.BtnParis_QuarteSel;
                            buttonZQuarté.Enabled = true;
                        }
                        /*    else
                            {
                                buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte;
                                buttonZQuarté.Enabled = true;
                            } */

                        break;
                    case "QAP":
                        if (!p.ChevalExpress)
                        {
                            buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus_Inactif;
                            buttonZQuartéPlus.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlusSel;
                            buttonZQuartéPlus.Enabled = true;
                        }
                     /*   else
                        {
                            buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus;
                            buttonZQuartéPlus.Enabled = true;

                        } */
                        break;
                    case "QIP":
                        if (!p.ChevalExpress)
                        {
                            buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte_Inactif;
                            buttonZQuintéPlus.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            buttonZQuintéPlus.Image = Properties.Resources.BtnParis_QuinteSel;
                            buttonZQuintéPlus.Enabled = true;
                        }
                   /*     else
                        {
                            buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte;
                            buttonZQuintéPlus.Enabled = true;

                        } */
                        break;
                    case "JUP":
                        if (p.ChevalExpress)
                        {
                            buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace;
                            buttonZJP.Enabled = true;
                        }
                        else
                        {
                            buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace_Inactif;
                            buttonZJP.Enabled = false;
                        }
                        break;
                    case "JUG":
                        if (p.ChevalExpress)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant;
                            buttonZJG.Enabled = true;
                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                            buttonZJG.Enabled = false;
                        }
                        break; 
                    case "PLA":
                        if (p.ChevalExpress)
                        {
                            buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace;
                            buttonZSP.Enabled = true;
                        }
                        else
                        {
                            buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace_Inactif;
                            buttonZSP.Enabled = false;
                        }
                        break;
                    case "GAG":
                        if (p.ChevalExpress)
                        {
                            buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant;
                            buttonZSG.Enabled = true;
                        }
                        else
                        {
                            buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                            buttonZSG.Enabled = false;
                        }
                        break;
                    case "TRO":
                        if (p.ChevalExpress)
                        {
                            buttonZTrio.Image = Properties.Resources.BtnParis_Trio;
                            buttonZTrio.Enabled = true;
                        }
                        else
                        {
                            buttonZTrio.Image = Properties.Resources.BtnParis_Trio_Inactif;
                            buttonZTrio.Enabled = false;
                        }
                        break;

                    case "JUO":
                        if (p.ChevalExpress)
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre;
                            buttonZJG.Enabled = true;
                        }
                        else
                        {
                            buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre_Inactif;
                            buttonZJG.Enabled = false;
                        }
                        break; 

                    case "ML4":
                        if (!p.ChevalExpress)
                        {
                            multi4PB.Image = Properties.Resources.BtnParis_Multi4_Inactif;
                            multi4PB.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            multi4PB.Image = Properties.Resources.BtnParis_Multi4Sel;
                            multi4PB.Enabled = true;
                        }
                        break;
                    case "ML5":
                        if (!p.ChevalExpress)
                        {
                            multi5PB.Image = Properties.Resources.BtnParis_Multi5_Inactif;
                            multi5PB.Enabled = false;
                        }
                        else if(formulationProduits.Contains(p))
                        {
                            multi5PB.Image = Properties.Resources.BtnParis_Multi5Sel;
                            multi5PB.Enabled = true;
                        }
                        break;
                    case "ML6":
                        if (!p.ChevalExpress)
                        {
                            multi6pb.Image = Properties.Resources.BtnParis_Multi6_Inactif;
                            multi6pb.Enabled = false;
                        }
                        else if(formulationProduits.Contains(p))
                        {
                            multi6pb.Image = Properties.Resources.BtnParis_Multi6Sel;
                            multi6pb.Enabled = true;
                        }
                        break;
                    case "ML7":
                        if (!p.ChevalExpress)
                        {
                            multi7pb.Image = Properties.Resources.BtnParis_Multi7_Inactif;
                            multi7pb.Enabled = false;
                        }
                        else if (formulationProduits.Contains(p))
                        {
                            multi7pb.Image = Properties.Resources.BtnParis_Multi7Sel;
                            multi7pb.Enabled = true;
                        }
                        break;
                }
            }
            buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
            buttonZSGP.Enabled = false;
            buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
            buttonZJGP.Enabled = false;

        }
        private void paris1_Click(object sender, EventArgs e)
        {
            nombreParisExpress = 1;
            paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            showParis();
            showPrice(0);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 2;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 2;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 3;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 3;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 4;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 4;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 5;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 5;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 6;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 6;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 7;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 7;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                showParis();
                showPrice(0);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!IsExpress)
            {
                btnCHExpress.BackgroundImage = Properties.Resources.BtnDesignationChevalEclair_Desel;
                IsExpress = true;
                nombreParisExpress = 8;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                showParis();
                showPrice(0);
            }
            else
            {
                nombreParisExpress = 8;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                showParis();
                showPrice(0);
            }
        }

        public void controlChExpBtns()
        {
            if (!IsExpress && !expressBtnClicked)
            {
                return;
            }
            foreach (Produit pro in formulationProduits)
            {
                if (pro == null || !pro.ChevalExpress)
                {
                    continue;
                }
                if (ctrChExp == 0)
                {
                    express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                    express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                    ctrChExp++;
                }
                else if (ctrChExp == 1 && pro.NombreBase < 7)
                {
                    express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                    express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                    ctrChExp++;
                }
                else if (ctrChExp == 2 && pro.NombreBase < 6)
                {
                    express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                    express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                    ctrChExp++;
                }
                else if (ctrChExp == 3 && pro.NombreBase < 5)
                {
                    express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                    express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                    ctrChExp++;
                }
                else if (ctrChExp == 4 && pro.NombreBase < 4)
                {
                    express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                    express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                    ctrChExp++;
                }
            }
        }


        private void buttonZExpress_Click(object sender, EventArgs e)
        {
            ctrChExp = 0;
            if (myCourse == null || myCourse.ListeProduit == null || myCourse.ListeProduit.Count < 1 || isChevalExpress || (listeChoix.Count > 0 && formulationProduits.Count > 0))
                return;
            if (expressBtnClicked && !isChevalExpress)
            {
                expressBtnClicked = false;
                buttonZExpress.Image = Properties.Resources.BtnDesignationChevalSpOt_Desel;
                IsExpress = false;
                getButtonsControls();
                showProduits(myCourse);
            }
            else
            {
                expressBtnClicked = true;
                buttonZExpress.Image = Properties.Resources.BtnParis_Eclair_Sel;
                paris1.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                paris2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                paris8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                bisBtn.Hide();
                btnCTotal.Hide();
                btnFCX.Hide();
                btnFComplete.Hide();
                listeChoix.Clear();
                IsExpress = true;

                bool isCourseEnabled = myCourse.Statut == StatutCourse.DepartDansXX ||
                       myCourse.Statut == StatutCourse.EnVente ||
                       myCourse.Statut == StatutCourse.PreDepart;
                foreach (Produit p in myCourse.ListeProduit)
                {
                    if (p == null)
                        continue;
                    bool enabled = p.Statut == StatutProduit.EnVenteProduit
                        && isCourseEnabled;

                    switch (p.CodeProduit)
                    {
                        case "TRC":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce;
                                buttonZTiercé.Enabled = true;
                            }
                            else
                            {
                                buttonZTiercé.Image = Properties.Resources.BtnParis_Tierce_Inactif;
                                buttonZTiercé.Enabled = false;
                            }
                            break; 

                        case "QUU":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte;
                                buttonZQuarté.Enabled = true;

                            }
                            else
                            {
                                buttonZQuarté.Image = Properties.Resources.BtnParis_Quarte_Inactif;
                                buttonZQuarté.Enabled = false;

                            }
                            break;
                        case "QAP":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus;
                                buttonZQuartéPlus.Enabled = true;

                            }
                            else
                            {
                                buttonZQuartéPlus.Image = Properties.Resources.BtnParis_QuartePlus_Inactif;
                                buttonZQuartéPlus.Enabled = false;

                            }
                            break;
                        case "QIP":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte;
                                buttonZQuintéPlus.Enabled = true;

                            }
                            else
                            {
                                buttonZQuintéPlus.Image = Properties.Resources.BtnParis_Quinte_Inactif;
                                buttonZQuintéPlus.Enabled = false;

                            }
                            break;
                        case "JUP":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace;
                                buttonZJP.Enabled = true;

                            }
                            else
                            {
                                buttonZJP.Image = Properties.Resources.BtnParis_JumelePlace_Inactif;
                                buttonZJP.Enabled = false;

                            }
                            break;
                        case "JUG":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant;
                                buttonZJG.Enabled = true;

                            }
                            else
                            {
                                buttonZJG.Image = Properties.Resources.BtnParis_JumeleGagnant_Inactif;
                                buttonZJG.Enabled = false;

                            }
                            break;
                        case "PLA":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace;
                                buttonZSP.Enabled = true;

                            }
                            else
                            {
                                buttonZSP.Image = Properties.Resources.BtnParis_SimplePlace_Inactif;
                                buttonZSP.Enabled = false;

                            }
                            break;
                        case "GAG":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant;
                                buttonZSG.Enabled = true;

                            }
                            else
                            {
                                buttonZSG.Image = Properties.Resources.BtnParis_SimpleGagnant_Inactif;
                                buttonZSG.Enabled = false;

                            }
                            break;
                        case "TRO":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZTrio.Image = Properties.Resources.BtnParis_Trio;
                                buttonZTrio.Enabled = true;

                            }
                            else
                            {
                                buttonZTrio.Image = Properties.Resources.BtnParis_Trio_Inactif;
                                buttonZTrio.Enabled = false;

                            }
                            break;

                        case "JUO":
                            if (p.ChevalExpress && enabled)
                            {
                                buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre;
                                buttonZJG.Enabled = true;

                            }
                            else
                            {
                                buttonZJG.Image = Properties.Resources.BtnParis_JumeleOrdre_Inactif;
                                buttonZJG.Enabled = false;
                            }
                            break;
                        case "ML4":
                            if (p.ChevalExpress && enabled)
                            {
                                multi4PB.Image = Properties.Resources.BtnParis_Multi4;
                                multi4PB.Enabled = true;
                            }
                            else
                            {
                                multi4PB.Image = Properties.Resources.BtnParis_Multi4_Inactif;
                                multi4PB.Enabled = false;
                            }
                            break; 
                        case "ML5":
                            if (p.ChevalExpress && enabled)
                            {
                                multi5PB.Image = Properties.Resources.BtnParis_Multi5;
                                multi5PB.Enabled = true;
                            }
                            else
                            {
                                multi5PB.Image = Properties.Resources.BtnParis_Multi5_Inactif;
                                multi5PB.Enabled = false;
                            }
                            break;
                        case "ML6":
                            if (p.ChevalExpress && enabled)
                            {
                                multi6pb.Image = Properties.Resources.BtnParis_Multi6;
                                multi6pb.Enabled = true;
                            }
                            else
                            {
                                multi6pb.Image = Properties.Resources.BtnParis_Multi6_Inactif;
                                multi6pb.Enabled = false;
                            }
                            break;
                        case "ML7":
                            if (p.ChevalExpress && enabled)
                            {
                                multi7pb.Image = Properties.Resources.BtnParis_Multi7;
                                multi7pb.Enabled = true;
                            }
                            else
                            {
                                multi7pb.Image = Properties.Resources.BtnParis_Multi7_Inactif;
                                multi7pb.Enabled = false;
                            }
                            break;
                    }
                }

                buttonZSGP.Image = Properties.Resources.BtnParis_SimpleGP_Inactif;
                buttonZSGP.Enabled = false;
                buttonZJGP.Image = Properties.Resources.BtnParis_JumeleGP_Inactif;
                buttonZJGP.Enabled = false;
                formulationProduits.Clear();
                button5.Show();
            }
        }

        private void express1_Click(object sender, EventArgs e)
        {
                listeChoix.Clear();
                int end = Int32.Parse(express1.Text);
                for (int i = 0; i < end; i++)
                {
                    listeChoix.Add("S");
                }
                express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
                express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
                getButtonsControls();

                showParis();
                showPrice(0);

                ctrChExp = 0;
             
        }

        private void express2_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express2.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);
            ctrChExp = 1;
        }

        private void express3_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express3.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);
            ctrChExp = 2;
        }

        private void express4_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express4.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);

            ctrChExp = 3;
        }

        private void express5_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express5.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);

            ctrChExp = 4;
        }

        private void express6_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express6.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);

            ctrChExp = 5;
        }

        private void express7_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express7.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            getButtonsControls();

            showParis();
            showPrice(0);

            ctrChExp = 6;
        }

        private void express8_Click(object sender, EventArgs e)
        {
            listeChoix.Clear();
            int end = Int32.Parse(express8.Text);

            for (int i = 0; i < end; i++)
            {
                listeChoix.Add("S");
            }
            express1.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express2.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express3.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express4.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express5.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express6.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express7.BackgroundImage = Properties.Resources.bouton_NbSpot_Desel;
            express8.BackgroundImage = Properties.Resources.bouton_NbSpot_Sel;
            getButtonsControls();

            showParis();
            showPrice(0);

            ctrChExp = 7;

        }

        private void buttonZFermGuichet_Click(object sender, EventArgs e)
        {
            if (ApplicationContext.sTotal != 0)
            {
                showErrorMessage("Veuillez finir la transaction avec votre client avant de fermer le guichet.", Color.Red);
                return;
            }
            ApplicationContext.sTotal = 0;
            ApplicationContext.LaunchAuthenticationForm("");
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            produits.Clear();
            formulationProduits.Clear();
            lsformulation.Clear();
            listeChoix.Clear();
            listeMultiplicateurs.Clear();
            lsReunion.Clear();
            showPrice(0);
            getButtonsControls();
            formulationCountControle();
        }

        private void buttonZNclient_MouseEnter(object sender, EventArgs e)
        {
            buttonZNclient.BackgroundImage = Properties.Resources.BtnFonctionRecupSoldeClient_Sel;
        }

        private void buttonZNclient_MouseLeave(object sender, EventArgs e)
        {
            buttonZNclient.BackgroundImage = Properties.Resources.BtnFonctionRecupSoldeClient_Desel;
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            button10.BackgroundImage = Properties.Resources.BtnReunionPrec_Sel;
        }

        private void button10_MouseClick(object sender, MouseEventArgs e)
        {
            if (button10.Enabled)
            {
                button10.BackgroundImage = Properties.Resources.BtnReunionPrec_Desel;
            }
            else
            {
                button10.BackgroundImage = Properties.Resources.BtnReunionPrec_Inactif;
            }
        }

        private void button14_MouseDown(object sender, MouseEventArgs e)
        {
            button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Sel;
        }

        private void button14_MouseClick(object sender, MouseEventArgs e)
        {
            if (button14.Enabled)
            {
                button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Desel;
            }
            else
            {
                button14.BackgroundImage = Properties.Resources.BtnReunionSuiv_Inactif;
            }
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled || numCourse >= r.ListeCourse.Count)
            {
                return;
            }
            numCourse++;
            myCourse = r.ListeCourse[numCourse-1];
            bisBtn.Hide();
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
            _updateCoursesHorses();
            button19.Enabled = numCourse != r.ListeCourse.Count;
            button18.Enabled = true;
            button18.BackgroundImage = Properties.Resources.BtnCoursePrec_Desel;
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled || numCourse <= 1)
            {
                return;
            }
            numCourse--;
            myCourse = r.ListeCourse[numCourse - 1];
            bisBtn.Hide();
            nouvelleCourseControle();
            CourseSelected(myCourse);
            produits = myCourse.ListeProduit;
            _updateCoursesHorses();

            button18.Enabled = numCourse != 1;
            button19.Enabled = true;
            button19.BackgroundImage = Properties.Resources.BtnCourseSuiv_Desel;
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            if (myCourse == null || !(sender as Control).Enabled)
                return;
            button17.Enabled = false;
            try
            {
                button17.BackgroundImage = Properties.Resources.BtnOption_Sel;
                button17.Refresh();
                Criteres c = new Criteres(myCourse, r.LibReunion, r.DateReunion, r.NumReunion);
                ImprimerCriteresService.ImprimerCriteres(c);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                Application.DoEvents();
                button17.BackgroundImage = Properties.Resources.BtnOption_Desel;
                button17.Enabled = true;
            }
        }

        private void button19_MouseClick(object sender, MouseEventArgs e)
        {
            if (button19.Enabled)
            {
                button19.BackgroundImage = Properties.Resources.BtnCourseSuiv_Desel;
            }
            else
            {
                button19.BackgroundImage = Properties.Resources.BtnCourseSuiv_Inactif;
            }
        }

        private void button19_MouseDown(object sender, MouseEventArgs e)
        {
            button19.BackgroundImage = Properties.Resources.BtnCourseSuiv_Sel;
        }

        private void button18_MouseClick(object sender, MouseEventArgs e)
        {
            if (button18.Enabled)
            {
                button18.BackgroundImage = Properties.Resources.BtnCoursePrec_Desel;
            }
            else
            {
                button18.BackgroundImage = Properties.Resources.BtnCoursePrec_Inactif;
            }
        }

        private void button18_MouseDown(object sender, MouseEventArgs e)
        {
            button18.BackgroundImage = Properties.Resources.BtnCoursePrec_Sel;
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            if (indexOperationDc < 0)
                return;
            indexOperationDc--;
            DisplayOpeartion(12);
            button1.Enabled = indexOperationDc != 0;
            button2.Enabled = true;
            button2.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Desel;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (indexOperationDc >= (ApplicationContext.DetailClient.Operations.Count - 1))
                return;
            indexOperationDc++;
            DisplayOpeartion(12);
            button2.Enabled = indexOperationDc != ApplicationContext.DetailClient.Operations.Count -1;
            button1.Enabled = true;
            button1.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Desel;
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (button1.Enabled)
            {
                button1.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Desel;
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Inactif;
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Sel;
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            if (button2.Enabled)
            {
                button2.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Desel;
            }
            else
            {
                button2.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Inactif;
            }
        }
        
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Sel;
        }

        private bool _validateDesignation(Formulation formulation)
        {
            string[] designation = formulation.Designation.Split('R');
            if (designation[0].Contains("X"))
            {
                string designation0 = Regex.Replace(designation[0], "TOTAL", "");
                int countX = designation0.Count(f => f == 'X');
                string trimDesignation0 = Regex.Replace(designation0, " ", "");
                string[] partsBR = designation0.Trim().Split(' ');
                bool valid = !trimDesignation0.All(char.IsLetter) && partsBR.Length == formulation.Produit.NombreBase;
                if (valid && designation.Length > 1)
                {
                    string trimDesignation1 = Regex.Replace(designation[1], " ", "");
                    string[] partsAR = designation[1].Trim().Split(' ');
                    valid = partsAR.Length >= countX && trimDesignation1.All(char.IsDigit) && !partsBR.Intersect(partsAR).Any();
                }
                return valid;
            }
            return true;
        }
        private bool _validateMiseTotal(Formulation formulation)
        {
            string designation = formulation.Designation;
            if (designation.Contains("X"))
            {
                designation = designation.Trim();
                designation = Regex.Replace(designation, "TOTAL", "");
                int nbr_ch_J = myCourse == null ? 0 : myCourse.ListePartant.Count;
                int nbr_X = designation.Count(f => f == 'X');
                int nbr_c_avant_T_R = 0;
                int nbr_c_apres_T_R = 0;
                
                string[] twoParts = designation.Split('R');

                if (twoParts.Length > 1)
                {
                    string[] avant = twoParts[0].Trim().Split(' ');
                    string[] apres = twoParts[1].Trim().Split(' ');
                    nbr_c_avant_T_R = avant.Length;
                    nbr_c_apres_T_R = apres.Length;
                }
                else
                {
                    nbr_c_avant_T_R = twoParts[0].Trim().Split(' ').Length;
                    int nbr_ch_S = nbr_c_avant_T_R - nbr_X;
                    nbr_c_apres_T_R = nbr_ch_J - nbr_ch_S;
                }
                int multiplicateur = 1;
                int ordre = formulation.Produit.Ordre ? 1 : 0;
                int permutation = formulation.FormComplete ? formulation.Produit.NombreBase : 1;
                long misetotal =
                    multiplicateur
                    * formulation.Produit.EnjeuMin
                    * pariControles.Combinaison(formulation.Produit.NombreBase, nbr_c_avant_T_R)
                    * pariControles.Combinaison(nbr_X, nbr_c_apres_T_R)
                    * pariControles.Max(1, ordre * pariControles.Max(pariControles.factorial(nbr_X), permutation));

                return misetotal == formulation.MiseTotal;
            }
            return true;
        }

        private bool _validateFormComplete(Formulation formulation)
        {
            return !formulation.FormComplete || formulation.Produit.Ordre;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
           pictureBox3.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Sel;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            if (pictureBox3.Enabled)
            {
                pictureBox3.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Desel;
            }
            else
            {
                pictureBox3.BackgroundImage = Properties.Resources.BtnReunionPrecPetit_Inactif;
            }
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (pictureBox4.Enabled)
            {
                pictureBox4.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Desel;
            }
            else
            {
                pictureBox4.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Inactif;
            }
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.BtnReunionSuivPetit_Sel;
        }
        public override void HideForm()
        {
            _blinkChevalTimer.Dispose();
            _datetimeTimer.Dispose();
            _blinkMessageTimer.Dispose();
            if (errorMsgInitFont != null)
                errorMsgInitFont.Dispose();
            if (errorMsgGreen != null)
                errorMsgGreen.Dispose();
            unsubscribeUpdateEcranClientBackgroundWorker();
            if (ApplicationContext.scanner != null)
            {
                ApplicationContext.scanner.ScanResultProcess -= scan_ProcessCompleted;
                ApplicationContext.scanner.Disconnect();
            }
            Hide();
        }

        public void BackToFront()
        {
            ApplicationContext.UpdateEcranClient(_ecranClientModel);
            if (ApplicationContext.scanner != null)
            {
                ApplicationContext.scanner.Connect();
            }
        }

        private void multi7pb_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("ML7");
            if (!IsExpress)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n Multi ", Color.Red);
                    return;
                }
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        multi7pb.Image = Properties.Resources.BtnParis_Multi7Sel;
                    }
                    else
                    {
                        multi7pb.Image = Properties.Resources.BtnParis_Multi;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisée pour les produits \n Multi ", Color.Red);
                    return;
                }
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlNonMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);


        }

        private void multi6pb_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("ML6");
            if (!IsExpress)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n Multi ", Color.Red);
                    return;
                }
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        multi6pb.Image = Properties.Resources.BtnParis_Multi6Sel;
                    }
                    else
                    {
                        multi6pb.Image = Properties.Resources.BtnParis_Multi6;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisée pour les produits \n Multi ", Color.Red);
                    return;
                }
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }
            }
            controlNonMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);

        }

        private void multi5PB_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("ML5");
            if (!IsExpress)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisés pour les produits \n Multi ", Color.Red);
                    return;
                }
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        multi5PB.Image = Properties.Resources.BtnParis_Multi5Sel;
                    }
                    else
                    {
                        multi5PB.Image = Properties.Resources.BtnParis_Multi5;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisée pour les produits \n Multi ", Color.Red);
                    return;
                }
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                    panel18.Show();
                    HideProduits();
                    getNbrPartantExpress(produit.NombreBase);
                    panel19.Show();
                    HideHorses();
                }


            }
            controlNonMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }

        private void multi4PB_Click(object sender, EventArgs e)
        {
            bisBtn.Hide();
            clearMessageZone();

            Produit produit = getProduitByName("ML4");
            if (!IsExpress)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisée pour les produits \n Multi ", Color.Red);
                    return;
                }
                if (produit != null)
                {
                    int flag = addProduitControl(produit);
                    ImprimerFlag = 1;

                    if (flag == 1)
                    {
                        multi4PB.Image = Properties.Resources.BtnParis_Multi4Sel;
                    }
                    else
                    {
                        multi4PB.Image = Properties.Resources.BtnParis_Multi4;
                        formulationCountControle();
                        getButtonsControls();
                    }
                }
            }
            else if (produit != null)
            {
                if (flagMultiIsValid == 0)
                {
                    showErrorMessage("Formule complète n'est pas \n autorisée pour les produits \n Multi ", Color.Red);
                    return;
                }
                int flag = addProduitControl(produit);
                ImprimerFlag = 1;
                if (flag == 1 && !isChevalExpress)
                {
                   panel18.Show();
                   HideProduits();
                   getNbrPartantExpress(produit.NombreBase);
                   panel19.Show();
                   HideHorses();
                }
            }
            controlNonMultiBtns();
            if (isChevalExpress)
            {
                this.HideBaseProducts();
            }
            controlExpresseGrs();
            showPrice(0);
        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonZValiderInactif_Click(object sender, EventArgs e)
        {
            
        }
    }
}
