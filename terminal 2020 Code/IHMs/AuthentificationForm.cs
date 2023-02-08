using System;
using System.Drawing;
using System.Windows.Forms;
using sorec_gamma.modules.ModuleAuthentification;
using sorec_gamma.modules.ModulePari.Controls;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using sorec_gamma.IHMs.ComposantsGraphique;
using System.ComponentModel;
using System.Globalization;
using calipso2020.Model;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.Config;

namespace sorec_gamma.IHMs
{
    public partial class AuthentificationForm : BaseForm
    {
        private int indice = 1;
        private string TextPreposé, TextMdp;
        private DiffusionOffreControl service;
        private Timer timer1;
        private PrinterStatusModel _previousPrinterStatus;

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ApplicationContext.imprimante != null)
            {
                ApplicationContext.imprimante.PrinterStatusEventHandler -= printer_HealthCheck;
            }
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }
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

            }
            base.Dispose(disposing);
        }

        public AuthentificationForm()
        {
            InitializeComponent();
            if (!ApplicationContext.develop && ApplicationContext.imprimante != null)
                ApplicationContext.imprimante.PrinterStatusEventHandler += printer_HealthCheck;

            Initialize("");
        }

        public void Initialize(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { Initialize(msg); }));
            }
            else
            {
                ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
                etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
                etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
                lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
                timer1 = new Timer(components)
                {
                    Enabled = true,
                    Interval = 1000
                };
                timer1.Tick += new EventHandler(timer1_Tick);

                if (!string.IsNullOrEmpty(msg))
                    SetLblErrorMessage(msg, Color.Red);
                else
                    SetLblErrorMessage("", Color.Transparent);
                indice = 1;
                service = new DiffusionOffreControl();
                ApplicationContext.isAuthenticated = false;
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                textBox1.BackColor = Color.Navy;
                textBox1.ForeColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox2.ForeColor = Color.Black;
                TextPreposé = "";
                TextMdp = "";
                lblIp.Text = "IP " + ApplicationContext.IP;
                lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
                lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                    + ApplicationContext.SOREC_DATA_ENV;

                EcranClientModel ecranClientModel = new EcranClientModel();
                ecranClientModel.MessageType = MessageType.Simple;
                ecranClientModel.SimpleMsg = "GUICHET FERMÉ";
                ApplicationContext.UpdateEcranClient(ecranClientModel);
                ActiveControl = textBox1;
            }
        }

        private void printer_HealthCheck(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(printer_HealthCheck),
                     new object[] { sender, e });
                return;
            }
            else
            {
                PrinterStatusEventArgs printerStatusEventArgs = e as PrinterStatusEventArgs;
                if (printerStatusEventArgs.status != null
                   && !printerStatusEventArgs.status.Equals(_previousPrinterStatus)
                   && !printerStatusEventArgs.status.Equals(PrinterStatusModel.PrinterValid))
                {
                    _previousPrinterStatus = printerStatusEventArgs.status;
                    printerNok.Visible = true;
                    printerOk.Visible = false;
                    ApplicationContext.imprimante.PrinterStatusEventHandler -= printer_HealthCheck;
                    HorsServiceForm HorsServiceForm = new HorsServiceForm(printerStatusEventArgs.status);
                    if (ApplicationContext.scanner != null)
                    {
                        ApplicationContext.scanner.Disconnect();
                    }
                    DialogResult result = HorsServiceForm.ShowDialog();
                    if (result == DialogResult.OK)
                        printerStatusEventArgs.status = PrinterStatusModel.PrinterValid;
                    if (!ApplicationContext.isAuthenticated)
                    {
                        EcranClientModel ecranClientModel = new EcranClientModel
                        {
                            MessageType = MessageType.Simple,
                            SimpleMsg = "GUICHET FERMÉ"
                        };
                        ApplicationContext.UpdateEcranClient(ecranClientModel);
                    }
                    else if (ApplicationContext.isAuthenticated && ApplicationContext.PrincipaleForm != null && ApplicationContext.PrincipaleForm.Visible)
                    {
                        ApplicationContext.PrincipaleForm.BackToFront();
                    }
                    bool validPrinter = ApplicationContext.imprimante != null
                           && ApplicationContext.imprimante.IsValid;
                    _ = printerNok.Invoke(new MethodInvoker(delegate { printerNok.Visible = !validPrinter; }));
                    _ = printerOk.Invoke(new MethodInvoker(delegate { printerOk.Visible = validPrinter; }));
                    ApplicationContext.imprimante.PrinterStatusEventHandler += printer_HealthCheck;
                }
                else if (printerStatusEventArgs.status != null
                   && printerStatusEventArgs.status.Equals(PrinterStatusModel.PrinterValid))
                {
                    printerNok.Visible = false;
                    printerOk.Visible = true;
                    SetLblErrorMessage("", Color.FromArgb(0, 5, 104));
                }
                _previousPrinterStatus = printerStatusEventArgs.status;
            }
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        private void write_caractere(string c)
        {
            switch (indice)
            {
                case 1:
                    this.TextPreposé += c;
                    SetLblErrorMessage("Saisir le numéro du compte préposé", Color.FromArgb(76, 127, 228));
                    this.textBox1.Text = this.TextPreposé;
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.TextPreposé.Length;
                    break;
                case 2:
                    this.TextMdp += c;
                    SetLblErrorMessage("Saisir le code confidentiel", Color.FromArgb(76, 127, 228));
                    this.textBox2.Text += "*";
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.TextMdp.Length;
                    break;

            }
        }

        private void changeTextBox(int indice, Color color)
        {
            this.indice = indice;
            if (indice == 1)
            {
                SetLblErrorMessage("Saisir le numéro du compte préposé", color);
                textBox1.BackColor = Color.Navy;
                textBox1.ForeColor = Color.White;
                textBox2.BackColor = Color.White;
                textBox2.ForeColor = Color.Black;
            }
            else
            {
                SetLblErrorMessage("Saisir le code confidentiel", color);
                textBox2.BackColor = Color.Navy;
                textBox2.ForeColor = Color.White;
                textBox1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            changeTextBox(1, Color.FromArgb(76, 127, 228));
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            changeTextBox(2, Color.FromArgb(76, 127, 228));
        }


        private void delete_caractere()
        {
            switch (indice)
            {
                case 1:
                    if (TextPreposé.Length > 0)
                    {
                        this.TextPreposé = TextPreposé.Substring(0, TextPreposé.Length - 1);
                        this.textBox1.Text = TextPreposé;
                        this.textBox1.Focus();
                        this.textBox1.SelectionStart = this.TextPreposé.Length;
                    }
                    break;
                case 2:
                    if (TextMdp.Length > 0)
                    {
                        this.TextMdp = TextMdp.Substring(0, TextMdp.Length - 1);
                        String mdp = this.textBox2.Text;
                        this.textBox2.Text = mdp.Substring(0, mdp.Length - 1);
                        this.textBox2.Focus();
                        this.textBox2.SelectionStart = this.TextMdp.Length;
                    }
                    break;
            }
        }

        private void SetLblErrorMessage(string txt, Color color)
        {
            lblErrorMessage.Text = txt;
            lblErrorMessage.ForeColor = Color.White;
            lblErrorMessage.BackColor = color;
            lblErrorMessage.Refresh();
        }

        private void buttonZ13_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            authentication();
        }

        private void authentication()
        {
            try
            {
                buttonZ13.Enabled = false;
                buttonZ13.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
                buttonZ13.Refresh();

                if (TextPreposé == null || TextPreposé == "")
                {
                    changeTextBox(1, Color.Red);
                }
                else if (TextMdp == null || TextMdp == "")
                {
                    changeTextBox(2, Color.Red);
                }
                else
                {
                    SetLblErrorMessage("Authentification en cours, merci de patienter.", Color.FromArgb(76, 127, 228));
                    if (TextPreposé.Equals("1234")
                       && TextMdp.Equals("1515"))
                    {
                        ApplicationContext.LaunchReglageForm();
                    }
                    else
                    {
                        DateTime dateTime = DateTime.Now;
                        string response = AuthentificationControl.Login(TextPreposé, TextMdp);
                        string TLV = TLVHandler.getTLVChamps(response);
                        int coderesponse = TLVHandler.Response(response);
                        TLVHandler appTagsHandlerResponse = new TLVHandler(TLV);

                        TLVTags dataResponseTag = appTagsHandlerResponse.getTLV(TLVTags.SOREC_DATA_RESPONSE);

                        if (dataResponseTag != null)
                        {
                            TLVHandler responseTagHandler = new TLVHandler(Utils.bytesToHex(dataResponseTag.value));
                            if (responseTagHandler != null)
                            {
                                TLVTags dateTimeTag = responseTagHandler.getTLV(TLVTags.SOREC_DATA_SYSTEME_DATE_TIME);
                                if (dateTimeTag != null)
                                {
                                    string serverDateTime = Utils.HexToASCII(Utils.bytesToHex(dateTimeTag.value));
                                    try
                                    {
                                        dateTime = DateTime.Parse(serverDateTime);

                                        string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                                        string sysTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                                        string date = dateTime.ToString(sysFormat);
                                        string time = dateTime.ToString(sysTimeFormat);
                                        NetworkInterfaceUtils.SetDate(date);
                                        NetworkInterfaceUtils.SetTime(time);
                                    }
                                    catch (Exception ex)
                                    {
                                        ApplicationContext.Logger.Error("Authentication Form Exception: " + ex.StackTrace);
                                    }
                                }
                            }
                        }
                        switch (coderesponse)
                        {
                            case Constantes.RESPONSE_CODE_KO_UNOTHORIZED_VERSION:
                                TLVTags versionAutTag = appTagsHandlerResponse.getTLV(TLVTags.SOREC_DATA_LOGICIEL_VERSION);
                                if (versionAutTag != null)
                                {
                                    string versionAut = Utils.HexToASCII(Utils.bytesToHex(versionAutTag.value));
                                    SetLblErrorMessage("Version non autorisée. La version autorisée est  " + versionAut, Color.Red);
                                    using (MAJForm alert = new MAJForm(true, versionAut))
                                    {
                                        alert.StartPosition = FormStartPosition.CenterScreen;
                                        alert.ShowDialog();
                                    }
                                }
                                else
                                {
                                    SetLblErrorMessage("Version non autorisée. La version autorisée non spécifiée", Color.Red);
                                }
                                break;
                            case Constantes.RESPONSE_CODE_OK:
                            case Constantes.RESPONSE_CODE_OK_UPDATE_OFFRE:
                                JournalUtils.initJournalXmlFile(dateTime);
                                AuthentificationControl.responseAuthentificationControle(TLV);
                                JournalUtils.saveJournalAuth(TextPreposé, "Connexion Etablie", dateTime.AddMilliseconds(100));
                                ApplicationContext.SOREC_DATA_OFFRE = service.getDataOffreFromMT();
                                TLVTags majLogicielle = appTagsHandlerResponse.getTLV(TLVTags.SOREC_MAJ_LOGICIELLE);
                                SetLblErrorMessage("", Color.FromArgb(0, 5, 104));
                                if (majLogicielle != null)
                                {
                                    using (MAJForm alert = new MAJForm())
                                    {
                                        alert.StartPosition = FormStartPosition.CenterScreen;
                                        alert.ShowDialog();
                                    }
                                }
                                else
                                {
                                    ApplicationContext.isVersionAuthorized = true;
                                }
                                if (ApplicationContext.isVersionAuthorized)
                                {
                                    if (!ApplicationContext.develop && (ApplicationContext.imprimante == null || !ApplicationContext.imprimante.IsValid))
                                    {
                                        SetLblErrorMessage("Ouverture refusée.\nL'imprimante n'est pas operationnelle.", Color.Red);
                                        JournalUtils.SaveSimpleMsg("Ouverture refusée. Imprimante n'est pas operationnelle.", Color.Red);
                                        return;
                                    }
                                    if (ApplicationContext.SOREC_DATA_OFFRE == null || ApplicationContext.SOREC_DATA_OFFRE.IsEmpty)
                                    {
                                        SetLblErrorMessage("Aucune offre chargée.\nVeuillez réessayer ultérieurement.", Color.Red);
                                        JournalUtils.SaveSimpleMsg("Ouverture refusée. Aucune offre chargée.", Color.Red);
                                    }
                                    else
                                    {
                                        ApplicationContext.isAuthenticated = true;
                                        ApplicationContext.LaunchPrincipaleForm(true, "NOUVEAU CLIENT", 0);
                                    }
                                }
                                break;
                            case Constantes.RESPONSE_CODE_KO_MAC_INVALIDE:
                                SetLblErrorMessage("Mac invalide. Veuillez réessayer.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_DATA_TERMINAL_INVALIDE:
                                SetLblErrorMessage("Données invalides. Veuillez réessayer.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_MS_PARAMETRAGE_INJOIGNABLE:
                                SetLblErrorMessage("Erreur de connexion. Paramétrage injoignable.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_MS_PDV_INJOIGNABLE:
                                SetLblErrorMessage("Erreur de connexion. PDV injoignable.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PDV_INTROUVABLE:
                                SetLblErrorMessage("Erreur de connexion. PDV introuvable.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_PREPOSE_MDP_INVALIDE:
                                SetLblErrorMessage("Préposé/Mot de passe invalide.", Color.Red);
                                break;
                            case Constantes.RESPONSE_CODE_KO_STATUT_PDV:
                                SetLblErrorMessage("Connexion refusée. PDV Suspendu.", Color.Red);
                                break;
                            default:
                                SetLblErrorMessage("Erreur de connexion" + (coderesponse == 0 ? "." : ": " + coderesponse), Color.Red);
                                break;
                        }
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("Exception Authentication: " + ex.StackTrace);
            }
            finally
            {
                buttonZ49.Enabled = true;
                buttonZ13.Enabled = true;
                buttonZ13.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
        }

        private void buttonZ29_Click_1(object sender, EventArgs e)
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

        private void buttonZ47_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals("") || !textBox2.Text.Equals(""))
            {
                delete_caractere();
                if (textBox2.Text.Equals(""))
                {
                    ActiveControl = textBox1;
                    changeTextBox(1, Color.FromArgb(76, 127, 228));
                }
                else if (textBox1.Text.Equals(""))
                {
                    ActiveControl = textBox2;
                    changeTextBox(2, Color.FromArgb(76, 127, 228));
                }
            }
            else
            {
                changefocusToTextBox();
            }
        }
        private void changefocusToTextBox()
        {
            if (indice == 1)
            {
                ActiveControl = textBox2;
                changeTextBox(2, Color.FromArgb(76, 127, 228));
            }
            else
            {
                ActiveControl = textBox1;
                changeTextBox(1, Color.FromArgb(76, 127, 228));
            }
        }

        private void buttonZ48_Click(object sender, EventArgs e)
        {
            write_caractere("0");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Visible)
                return;
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
            lblwatch.Refresh();
        }

        private void buttonZ29_MouseLeave(object sender, EventArgs e)
        {
            buttonZ29.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ30_MouseEnter(object sender, EventArgs e)
        {
            buttonZ30.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }


        private void buttonZ30_MouseLeave(object sender, EventArgs e)
        {
            buttonZ30.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ40_MouseEnter(object sender, EventArgs e)
        {
            buttonZ40.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ40_MouseLeave(object sender, EventArgs e)
        {
            buttonZ40.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ41_MouseEnter(object sender, EventArgs e)
        {
            buttonZ41.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ41_MouseLeave(object sender, EventArgs e)
        {
            buttonZ41.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ42_MouseEnter(object sender, EventArgs e)
        {
            buttonZ42.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ42_MouseLeave(object sender, EventArgs e)
        {
            buttonZ42.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }
        private void buttonZ43_MouseEnter(object sender, EventArgs e)
        {
            buttonZ43.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ43_MouseLeave(object sender, EventArgs e)
        {
            buttonZ43.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }
        private void buttonZ44_MouseEnter(object sender, EventArgs e)
        {
            buttonZ44.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ44_MouseLeave(object sender, EventArgs e)
        {
            buttonZ44.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }
        private void buttonZ45_MouseEnter(object sender, EventArgs e)
        {
            buttonZ45.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ45_MouseLeave(object sender, EventArgs e)
        {
            buttonZ45.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }
        private void buttonZ46_MouseEnter(object sender, EventArgs e)
        {
            buttonZ46.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ46_MouseLeave(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ46, Properties.Resources.btnChiffre64_Desel);
        }
        private void buttonZ47_MouseEnter(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ47, Properties.Resources.btnChiffre64_Sel);
        }

        private void buttonZ47_MouseLeave(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ47, Properties.Resources.btnChiffre64_Desel);
        }
        private void buttonZ48_MouseEnter(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ48, Properties.Resources.btnChiffre64_Sel);
        }

        private void buttonZ48_MouseLeave(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ48, Properties.Resources.btnChiffre64_Desel);
        }
        private void buttonZ49_MouseEnter(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ49, Properties.Resources.btnChiffre64_Sel);
        }

        private void buttonZ49_MouseLeave(object sender, EventArgs e)
        {
            GraphicUtils.UpdateBackgroundImage(buttonZ49, Properties.Resources.btnChiffre64_Desel);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            buttonZ13.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonZ29_MouseEnter(object sender, EventArgs e)
        {
            buttonZ29.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ13_MouseEnter(object sender, EventArgs e)
        {
            buttonZ13.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void buttonZ49_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                indice = 2;
                textBox2.Focus();
                changeTextBox(2, Color.FromArgb(76, 127, 228));
                if (indice == 2 && !textBox2.Text.Equals(""))
                {
                    buttonZ49.Enabled = false;
                    authentication();
                }
            }
            else
            {
                SetLblErrorMessage("Saisir le numéro du compte préposé", Color.FromArgb(76, 127, 228));
            }
        }

        private void buttonZ13_MouseLeave(object sender, EventArgs e)
        {
            buttonZ13.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }


        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            timer1.Dispose();
            Hide();
        }
    }
}
