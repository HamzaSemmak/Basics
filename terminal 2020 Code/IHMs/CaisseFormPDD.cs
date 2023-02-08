using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using sorec_gamma.modules.Config;
using log4net;
using sorec_gamma.modules.ModuleBackOffice.Controle;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.TLV;

namespace sorec_gamma.IHMs
{
    public partial class CaisseForm : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(CaisseForm));

        private Timer _datetimeTimer;
        private EtatDeCaisseControle etatdeCaisseControle;
        private SoldePrePayeControle soldePrepayeControle;
        private FSoldControl fSoldControl;
        private string textdd = "";
        private string textMM = "";
        private string textyyyy = "";
        private string textSAlerte;
        private int flag = 0;
        private int flagPDV = 0;
        private string dateCaisse = "";
        private string typeCaisse = "";
        private string formatted_caisse_Paris_Cummul;
        private string formatted_caisse_Annulation_Cummul;
        private string formatted_caisse_Paiement_Cummul;
        private string formatted_caisse_Voucher_Cummul;
        private string formatted_caisse_annuSys_Cummul;
        private string formatted_caisse_annuMachine_Cummul;
        private string formatted_caisse_depot_Cummul;
        private string formatted_caisse_paiements_Cummul;
        private string formatted_total;
        private int caisse_Paris_Nombre = 0;
        private decimal caisse_Paris_Cummul = 0;
        private int caisse_Annulation_Nombre = 0;
        private decimal caisse_Annulation_Cummul = 0;
        private int caisse_Paiement_Nombre = 0;
        private decimal caisse_Paiement_Cummul = 0;
        private int caisse_Voucher_Nombre = 0;
        private decimal caisse_Voucher_Cummul = 0;
        private int caisse_depot_Nombre = 0;
        private decimal caisse_depot_Cummul = 0;
        private int caisse_annuSys_Nombre = 0;
        private decimal caisse_annuSys_Cummul = 0; 
        private int caisse_annuMachine_Nombre = 0;
        private decimal caisse_annuMachine_Cummul = 0;         
        private int caisse_paiements_Nombre = 0;
        private decimal caisse_paiements_Cummul = 0;
        private decimal total = 0;
        private int nombre_guichet = 0;
        private decimal soldePrePaye = 0;
        
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }

        public CaisseForm(int flagpdv)
        {
            InitializeComponent();
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            _datetimeTimer = new Timer();
            _datetimeTimer.Enabled = true;
            _datetimeTimer.Interval = 1;
            _datetimeTimer.Tick += new EventHandler(_TimeTick);
            Initialize(flagpdv);

            etatdeCaisseControle = new EtatDeCaisseControle();
            fSoldControl = new FSoldControl();
            soldePrepayeControle = new SoldePrePayeControle();
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        public void Initialize(int flagpdv)
        {
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            _datetimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _datetimeTimer.Tick += new EventHandler(_TimeTick);

            labelMsg.Text = "";
            textSAlerte = seuilAlertTextBox.Text = ConfigUtils.ConfigData.SeuilAlerte.ToString();
            lblGuichet.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblIp.Text = "IP " + ApplicationContext.IP;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV;
            flagPDV = flagpdv;

            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

            flag = 1;
            textyyyy = textMM = textdd = "";
            etatJJTextBox.Text = etatMMTextBox.Text = etatYYYYTextBox.Text = "";
            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;

            switch (flagPDV)
            {
                //PDD
                case 0:
                    button2.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                    this.panelSAlerte.Hide();
                    this.panelSPrepaye.Hide();
                    this.panelCaisse2.Show();
                    this.panelCaisse1.Show();
                    break;
                // PDV
                case 1:
                    button3.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                    this.panelSAlerte.Hide();
                    this.panelSPrepaye.Hide();
                    this.panelCaisse2.Show();
                    this.panelCaisse1.Show();
                    break;
                // Solde PrePayé
                case 3:
                    button4.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                    this.panelSAlerte.Hide();
                    this.panelSPrepaye.Show();
                    this.panelCaisse2.Hide();
                    this.panelCaisse1.Hide();
                    string result = soldePrepayeControle.requestSoldePrepaye();
                    string tlv = TLVHandler.getTLVChamps(result);
                    int codeReponse = TLVHandler.Response(result);
                    TLVHandler tLVHandler = new TLVHandler(tlv);
                    switch (codeReponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            try
                            {
                                this.soldePrePaye = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler.getTLV(TLVTags.SOREC_DATA_SOLDE_PREPAYE).value)), CultureInfo.InvariantCulture);

                            }
                            catch (Exception ex)
                            {
                                Logger.Error("Caisse Form PDD: " + ex.StackTrace);
                            }

                            break;
                        default:
                            this.soldePrePaye = 0;
                            labelMsg.Text = "Ce PDV n'est pas PrePayé";
                            labelMsg.ForeColor = Color.Red;
                            break;
                    }
                    this.soldPrepRestTextBox.Text = this.soldePrePaye.ToString();
                    break;
                // Seuil Alerte
                case 4:
                    button6.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                    this.panelSAlerte.Show();
                    this.panelSPrepaye.Hide();
                    this.panelCaisse2.Show();
                    this.panelCaisse1.Hide();
                    flag = 4;
                    break;
                // FSOLD
                case 5:
                    button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                    this.panelSAlerte.Hide();
                    this.panelSPrepaye.Hide();
                    this.panelCaisse2.Show();
                    this.panelCaisse1.Show();
                    break;
            }
        }
        private void write_caractere(String c)
        {
            switch (flag)
            {
                case 1: 
                    this.textdd += c; 
                    this.etatJJTextBox.Text = textdd;
                    this.etatJJTextBox.Focus();
                    this.etatJJTextBox.SelectionStart = this.textdd.Length;
                    break;
                case 2: 
                    this.textMM += c; 
                    this.etatMMTextBox.Text = textMM;
                    this.etatMMTextBox.Focus();
                    this.etatMMTextBox.SelectionStart = this.textMM.Length;
                    break;
                case 3: 
                    this.textyyyy += c; 
                    this.etatYYYYTextBox.Text = textyyyy;
                    this.etatYYYYTextBox.Focus();
                    this.etatYYYYTextBox.SelectionStart = this.textyyyy.Length;
                    break;
                case 4:
                    if (textSAlerte.Length >= 8)
                        return;
                    this.textSAlerte += c; 
                    this.seuilAlertTextBox.Text = textSAlerte;
                    this.seuilAlertTextBox.Focus();
                    this.seuilAlertTextBox.SelectionStart = this.textSAlerte.Length;
                    break;
            }
        }
        private void delete_caractere()
        {
            try
            {
                switch (flag)
                {

                    case 1: 
                        if (this.textdd.Length > 0) 
                        { 
                            this.textdd = textdd.Substring(0, textdd.Length - 1); 
                            this.etatJJTextBox.Text = textdd;
                            this.etatJJTextBox.Focus();
                            this.etatJJTextBox.SelectionStart = this.textdd.Length;
                        } 
                        break;
                    case 2: 
                        if (this.textMM.Length > 0) 
                        { 
                            this.textMM = textMM.Substring(0, textMM.Length - 1);
                            this.etatMMTextBox.Text = textMM;
                            this.etatMMTextBox.Focus();
                            this.etatMMTextBox.SelectionStart = this.textMM.Length;
                        } 
                        break;
                    case 3: 
                        if (this.textyyyy.Length > 0) 
                        { 
                            this.textyyyy = textyyyy.Substring(0, textyyyy.Length - 1);
                            this.etatYYYYTextBox.Text = textyyyy;
                            this.etatYYYYTextBox.Focus();
                            this.etatYYYYTextBox.SelectionStart = this.textyyyy.Length;
                        } 
                        break;
                    case 4: 
                        if (this.textSAlerte.Length > 0)
                        {
                            this.textSAlerte = textSAlerte.Substring(0, textSAlerte.Length - 1);
                            this.seuilAlertTextBox.Text = textSAlerte;
                            this.seuilAlertTextBox.Focus();
                            this.seuilAlertTextBox.SelectionStart = this.textSAlerte.Length;
                        }  
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("CaissePDDForm Exception : " + ex.Message + "\n" + ex.StackTrace);
            }

        }
       
        private void btnPrepose_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaissePreposeForm();
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchJournalForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaissePreposeForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

            textdd = etatJJTextBox.Text = "";
            textMM = etatMMTextBox.Text = "";
            textyyyy = etatYYYYTextBox.Text = "";
            labelMsg.Text = "";

            this.panelSAlerte.Hide();
            this.panelSPrepaye.Show();
            this.panelCaisse2.Hide();
            this.panelCaisse1.Hide();
            string result = soldePrepayeControle.requestSoldePrepaye();
            string tlv = TLVHandler.getTLVChamps(result);
            int codeReponse = TLVHandler.Response(result);
            TLVHandler tLVHandler = new TLVHandler(tlv);

            try
            {
                switch (codeReponse)
                {
                    case Constantes.RESPONSE_CODE_OK:
                        this.soldePrePaye = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(tLVHandler.getTLV(TLVTags.SOREC_DATA_SOLDE_PREPAYE).value)),
                                CultureInfo.InvariantCulture);
                        break;
                    default:
                        this.soldePrePaye = 0;
                        labelMsg.Text = "Ce PDV n'est pas PrePayé";
                        labelMsg.ForeColor = Color.Red;
                        break;
                }
                this.soldPrepRestTextBox.Text = this.soldePrePaye.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("CaisseFormPDD Exception: " + ex.StackTrace);
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textdd = etatJJTextBox.Text = "";
            textMM = etatMMTextBox.Text = "";
            textyyyy = etatYYYYTextBox.Text = "";
            labelMsg.Text = "";
            this.panelSAlerte.Hide();
            this.panelSPrepaye.Hide();
            this.panelCaisse2.Show();
            this.panelCaisse1.Show();
            this.flagPDV = 3;
            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

            flag = 1;

            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            flag = 4;
            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

            labelMsg.Text = "";

            this.panelSPrepaye.Hide();
            this.panelSAlerte.Show();
            this.panelCaisse2.Show();
            this.panelCaisse1.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textdd = etatJJTextBox.Text = "";
            textMM = etatMMTextBox.Text = "";
            textyyyy = etatYYYYTextBox.Text = "";
            labelMsg.Text = "";
            this.panelSAlerte.Hide();
            this.panelSPrepaye.Hide();
            this.panelCaisse2.Show();
            this.panelCaisse1.Show();
            this.flagPDV = 1;
            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

            flag = 1;

            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button2.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
            button3.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            button6.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

            textdd = etatJJTextBox.Text = "";
            textMM = etatMMTextBox.Text = "";
            textyyyy = etatYYYYTextBox.Text = "";
            labelMsg.Text = "";
            this.panelSAlerte.Hide();
            this.panelSPrepaye.Hide();
            this.panelCaisse1.Show();
            this.panelCaisse2.Show();
            this.flagPDV = 0;
            flag = 1;

            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;

        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchClientForm();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void buttonZ7_Click(object sender, EventArgs e)
        {
            DateTime dt;
            if (textyyyy.Equals("") || textMM.Equals("") || textdd.Equals(""))
            {
                labelMsg.Text = "Veuillez saisir tous les champs.";
                labelMsg.ForeColor = Color.Red;
                return;
            }
            string date = textyyyy + "-" + textMM + "-" + textdd;
            string result = "";
            if (DateTime.TryParseExact(date.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                //code if parsing is successful
                if (flagPDV == 0 || flagPDV == 1)
                {
                    switch (flagPDV)
                    {
                        case 0:
                            result = etatdeCaisseControle.requestEtatCaisse("PDD", date);
                            break;
                        case 1:
                            result = etatdeCaisseControle.requestEtatCaisse("PDV", date);
                            break;
                    }
                    string tlv = TLVHandler.getTLVChamps(result);
                    int coderesponse = TLVHandler.Response(result);
                    switch (coderesponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:

                            etatdeCaisseControle.getEtatDeCaisse(tlv);

                            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                            nfi.NumberGroupSeparator = " ";
                            this.dateCaisse = etatdeCaisseControle.dateCaisse;
                            this.typeCaisse = etatdeCaisseControle.typeCaisse;
                            this.caisse_Paris_Nombre = etatdeCaisseControle.caisse_Paris_Nombre;
                            this.caisse_Paris_Cummul = etatdeCaisseControle.caisse_Paris_Cummul;
                            formatted_caisse_Paris_Cummul = caisse_Paris_Cummul.ToString("#,0.00", nfi);

                            this.caisse_Annulation_Nombre = etatdeCaisseControle.caisse_Annulation_Nombre;
                            this.caisse_Annulation_Cummul = etatdeCaisseControle.caisse_Annulation_Cummul;
                            formatted_caisse_Annulation_Cummul = caisse_Annulation_Cummul.ToString("#,0.00", nfi);

                            this.caisse_Voucher_Nombre = etatdeCaisseControle.caisse_Voucher_Nombre;
                            this.caisse_Voucher_Cummul = etatdeCaisseControle.caisse_Voucher_Cummul;
                            formatted_caisse_Voucher_Cummul = caisse_Voucher_Cummul.ToString("#,0.00", nfi);

                            this.caisse_Paiement_Nombre = etatdeCaisseControle.caisse_Paiement_Nombre;
                            this.caisse_Paiement_Cummul = etatdeCaisseControle.caisse_Paiement_Cummul;
                            formatted_caisse_Paiement_Cummul = caisse_Paiement_Cummul.ToString("#,0.00", nfi);

                            this.caisse_annuSys_Nombre = etatdeCaisseControle.caisse_AnnulSys_Nombre;
                            this.caisse_annuSys_Cummul = etatdeCaisseControle.caisse_AnnulSys_Cummul;
                            formatted_caisse_annuSys_Cummul = this.caisse_annuSys_Cummul.ToString("#,0.00", nfi);

                            this.caisse_annuMachine_Nombre = etatdeCaisseControle.caisse_AnnulMachine_Nombre;
                            this.caisse_annuMachine_Cummul = etatdeCaisseControle.caisse_AnnulMachine_Cummul;
                            formatted_caisse_annuMachine_Cummul = this.caisse_annuMachine_Cummul.ToString("#,0.00", nfi);

                            this.caisse_depot_Nombre = etatdeCaisseControle.caisse_depot_Nombre;
                            this.caisse_depot_Cummul = etatdeCaisseControle.caisse_depot_Cummul;
                            formatted_caisse_depot_Cummul = caisse_depot_Cummul.ToString("#,0.00", nfi);

                            this.nombre_guichet = etatdeCaisseControle.nombre_guichet;
                            this.caisse_paiements_Cummul = etatdeCaisseControle.caisse_paiements_Cummul;
                            formatted_caisse_paiements_Cummul = caisse_paiements_Cummul.ToString("#,0.00", nfi);

                            this.caisse_paiements_Nombre = etatdeCaisseControle.caisse_paiements_Nombre;
                            this.total = etatdeCaisseControle.caisse_Paris_Cummul - etatdeCaisseControle.caisse_Annulation_Cummul
                                + etatdeCaisseControle.caisse_Voucher_Cummul - etatdeCaisseControle.caisse_Paiement_Cummul
                                + etatdeCaisseControle.caisse_depot_Cummul - etatdeCaisseControle.caisse_AnnulSys_Cummul - etatdeCaisseControle.caisse_paiements_Cummul
                                -etatdeCaisseControle.caisse_AnnulMachine_Cummul;
                            formatted_total = total.ToString("#,0.00", nfi);

                            ImprimerEtatDeCaisse serviceImp = new ImprimerEtatDeCaisse();
                            serviceImp.imprimerEtatCaisse(caisse_Paris_Nombre, formatted_caisse_Paris_Cummul, caisse_Annulation_Nombre, formatted_caisse_Annulation_Cummul,
                                caisse_Voucher_Nombre, formatted_caisse_Voucher_Cummul, caisse_Paiement_Nombre,
                                formatted_caisse_Paiement_Cummul, formatted_total, nombre_guichet, caisse_depot_Nombre, formatted_caisse_depot_Cummul,
                                caisse_annuSys_Nombre, formatted_caisse_annuSys_Cummul, caisse_paiements_Nombre, formatted_caisse_paiements_Cummul, textyyyy, textMM, textdd,
                                caisse_annuMachine_Nombre, formatted_caisse_annuMachine_Cummul);

                            break;

                    }
                }
                else if (flagPDV == 3 || flagPDV == 5)
                {
                    string response = fSoldControl.requestFSold(date);
                    String myTlv = TLVHandler.getTLVChamps(response);
                    TLVHandler appTagsHandler = new TLVHandler(myTlv);

                    int codeReponse = TLVHandler.Response(response);
                    switch (codeReponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            TLVTags fsoldDataTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_FSOLD);
                            TLVHandler fSoldDataTagHandler = new TLVHandler(Utils.bytesToHex(fsoldDataTag.value));
                            TLVTags contentTag = fSoldDataTagHandler.getTLV(TLVTags.SOREC_DATA_FSOLD_CONTENT);
                            string content = Utils.HexToASCII(Utils.bytesToHex(contentTag.value));
                            ImprimerFSoldService imprimerService = new ImprimerFSoldService();
                            imprimerService.ImprimerFsold(content);
                            break;
                        default:
                            labelMsg.Text = "Cette date ne correspond à aucune situation PDV";
                            labelMsg.ForeColor = Color.Red;
                            break;
                    }
                }
            }
            else
            {
                labelMsg.Text = "Le format de la date est incorrect.";
                labelMsg.ForeColor = Color.Red;
                return;
            }

        }

        private void buttonZ29_Click(object sender, EventArgs e)
        {
            write_caractere("1");
        }

        private void buttonZ30_Click(object sender, EventArgs e)
        {
            write_caractere("2");

        }

        private void buttonZ40_Click(object sender, EventArgs e)
        {
            write_caractere("3");

        }

        private void buttonZ41_Click(object sender, EventArgs e)
        {
            write_caractere("4");

        }

        private void buttonZ42_Click(object sender, EventArgs e)
        {
            write_caractere("5");

        }

        private void buttonZ43_Click(object sender, EventArgs e)
        {
            write_caractere("6");

        }

        private void buttonZ44_Click(object sender, EventArgs e)
        {
            write_caractere("7");

        }

        private void buttonZ45_Click(object sender, EventArgs e)
        {
            write_caractere("8");

        }

        private void buttonZ46_Click(object sender, EventArgs e)
        {
            write_caractere("9");

        }

        private void buttonZ48_Click(object sender, EventArgs e)
        {
            write_caractere("0");

        }

        private void buttonZ47_Click(object sender, EventArgs e)
        {
            delete_caractere();

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            controlEtatJJTextBox();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            controlEtatMMTextBox();
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            controlEtatYYYYTextBox();
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            flag = 4;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            labelMsg.Text = "";
            labelMsg.BackColor = Color.Transparent;
            string value1;

            if (!textSAlerte.Equals(""))
            {
                value1 = textSAlerte;
                _ = int.TryParse(value1, out int seuil);
                ConfigUtils.ConfigData.SeuilAlerte = seuil;

                bool v1 = ConfigUtils.createOrUpdateConfigFile(ConfigUtils.ConfigData);
                if (v1)
                {
                    labelMsg.Text = "Le seuil a été défini avec succès.";
                    labelMsg.ForeColor = Color.Green;
                    ConfigUtils.ConfigData.SeuilAlerte = Int64.Parse(value1);
                }
                else
                {
                    labelMsg.Text = "Seuil non défini.\nVeuillez réessayer ou contacter l'administration.";
                    labelMsg.ForeColor = Color.Red;
                }
            }
            else
            {
                labelMsg.Text = "Veuillez saisir un seuil.";
                labelMsg.ForeColor = Color.Red;
            }
          
        }

        private void buttonZ8_Click(object sender, EventArgs e)
        {
            labelMsg.Text = "";
            textdd = etatJJTextBox.Text = "";
            textMM = etatMMTextBox.Text = "";
            textyyyy = etatYYYYTextBox.Text = "";

            flag = 1;
            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;
        }

        private void _TimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void controlEtatJJTextBox()
        {
            flag = 1;
            labelMsg.Text = "";
            etatJJTextBox.BackColor = Color.Navy;
            etatJJTextBox.ForeColor = Color.White;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;
        }
        private void controlEtatMMTextBox()
        {
            flag = 2;
            labelMsg.Text = "";
            etatJJTextBox.BackColor = Color.White;
            etatJJTextBox.ForeColor = Color.Black;
            etatMMTextBox.BackColor = Color.Navy;
            etatMMTextBox.ForeColor = Color.White;
            etatYYYYTextBox.BackColor = Color.White;
            etatYYYYTextBox.ForeColor = Color.Black;
        }
        private void controlEtatYYYYTextBox()
        {
            flag = 3;
            labelMsg.Text = "";
            etatJJTextBox.BackColor = Color.White;
            etatJJTextBox.ForeColor = Color.Black;
            etatMMTextBox.BackColor = Color.White;
            etatMMTextBox.ForeColor = Color.Black;
            etatYYYYTextBox.BackColor = Color.Navy;
            etatYYYYTextBox.ForeColor = Color.White;
        }

        private void buttonZ49_Click(object sender, EventArgs e)
        {
            switch (flag)
            {
                case 1:
                    etatMMTextBox.Focus();
                    controlEtatMMTextBox();
                    break;
                case 2:
                    etatYYYYTextBox.Focus();
                    controlEtatYYYYTextBox();
                    break;
            }
        }
        public override void HideForm()
        {
            if (_datetimeTimer != null)
                _datetimeTimer.Dispose();
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;

            Hide();
        }
    }
}
