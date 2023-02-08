using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using sorec_gamma.modules.ModuleCote_rapport.controls;
using sorec_gamma.modules.TLV;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.Config;
using log4net;

namespace sorec_gamma.IHMs
{
    public partial class CréationClientForm : BaseForm
    {
        private Timer _datetimeTimer;
        public string textCIN, textNom, textPrenom, textDateNaissance, textNumTel;
        int indice;
        private readonly ILog Logger = LogManager.GetLogger(typeof(CréationClientForm));

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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

        public CréationClientForm()
        {
            InitializeComponent();
            
            Initialize();
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }


        public void Initialize()
        {
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            _datetimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _datetimeTimer.Tick += new EventHandler(_datetimeTimerTick);

            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textCIN = "";
            textNom = "";
            textPrenom = "";
            textDateNaissance = "";
            textNumTel = "";
            indice = 1;
            label3.Text = "";

            btnPrepose.Enabled = false;
            button46.Enabled = false;
            btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            button46.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;

            label3.BackColor = Color.Transparent;

            lblIp.Text = "IP " + ApplicationContext.IP;
            lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV;

        }
        private void _datetimeTimerTick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void write_caractere(String c)
        {
            label3.Text = "";
            label3.ForeColor = Color.White;
            label3.BackColor = Color.Transparent;
            switch (indice)
            {
                case 1:
                    this.textCIN += c;
                    this.textBox1.Text = textCIN;
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.textCIN.Length;
                    break;
                case 2:
                    this.textNom += c;
                    this.textBox2.Text = textNom;
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.textNom.Length;
                    break;
                case 3:
                    this.textPrenom += c;
                    this.textBox3.Text = textPrenom;
                    this.textBox3.Focus();
                    this.textBox3.SelectionStart = this.textPrenom.Length;
                    break;
                case 4:
                    if (textDateNaissance.Length >= 8)
                        return;
                    this.textDateNaissance += c;
                    this.textBox4.Text = textDateNaissance;
                    this.textBox4.Focus();
                    this.textBox4.SelectionStart = this.textBox4.SelectionStart;
                    break;
                case 5:
                    if (textNumTel.Length >= 10)
                        return;
                    this.textNumTel += c;
                    this.textBox5.Text = textNumTel;
                    this.textBox5.Focus();
                    this.textBox5.SelectionStart = this.textNumTel.Length;
                    break;
            }
            int flag = fieldsCreationControl();
            int flag2 = fieldsAccesControl();
            if (flag == 1)
            {
                btnPrepose.Enabled = true;
                btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                btnPrepose.Enabled = false;
                btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }
            if (flag2 == 1)
            {
                button46.Enabled = true;
                button46.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                button46.Enabled = false;
                button46.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            indice = 1;
            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
        }

        private void delete_caractere()
        {

            switch (indice)
            {
                case 1:
                    if (this.textCIN.Length > 0)
                    {
                        this.textCIN = textCIN.Substring(0, textCIN.Length - 1);
                        this.textBox1.Text = textCIN;
                        this.textBox1.Focus();
                        this.textBox1.SelectionStart = this.textCIN.Length;
                    }
                    break;
                case 2:
                    if (this.textNom.Length > 0)
                    {
                        this.textNom = textNom.Substring(0, textNom.Length - 1);
                        this.textBox2.Text = textNom;
                        this.textBox2.Focus();
                        this.textBox2.SelectionStart = this.textNom.Length;
                    }
                    break;
                case 3:
                    if (this.textPrenom.Length > 0)
                    {
                        this.textPrenom = textPrenom.Substring(0, textPrenom.Length - 1);
                        this.textBox3.Text = textPrenom;
                        this.textBox3.Focus();
                        this.textBox3.SelectionStart = this.textPrenom.Length;
                    }
                    break;
                case 4:
                    if (this.textDateNaissance.Length > 0)
                    {
                        this.textDateNaissance = textDateNaissance.Substring(0, textDateNaissance.Length - 1);
                        this.textBox4.Text = textDateNaissance;
                        this.textBox4.Focus();
                        this.textBox4.SelectionStart = this.textBox4.SelectionStart;
                    }
                    break;
                case 5:
                    if (this.textNumTel.Length > 0)
                    {
                        this.textNumTel = textNumTel.Substring(0, textNumTel.Length - 1);
                        this.textBox5.Text = textNumTel;
                        this.textBox5.Focus();
                        this.textBox5.SelectionStart = this.textNumTel.Length;
                    }
                    break;
            }
            int flag = fieldsCreationControl();
            int flag2 = fieldsAccesControl();
            if (flag == 1)
            {
                btnPrepose.Enabled = true;
                btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                btnPrepose.Enabled = false;
                btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;

            }
            if (flag2 == 1)
            {
                button46.Enabled = true;
                button46.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                button46.Enabled = false;
                button46.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }

        }
        private int fieldsCreationControl()
        {
            int flag = 0;
            if (Regex.IsMatch(textNom, @"^[a-zA-Z][a-zA-Z \t _-]*$"))
            {
                if (Regex.IsMatch(textPrenom, @"^[a-zA-Z][a-zA-Z \t _-]*$"))
                {
                    if (Regex.IsMatch(textCIN, @"^[a-zA-Z]+[0-9]+$") && textCIN.Length >= 4)
                    {
                        if (Regex.IsMatch(textNumTel, @"^[0-9]+$") && textNumTel.Length == 10)
                        {
                            flag = 1;
                        }
                    }
                }
            }

            return flag;
        }
        private int fieldsAccesControl()
        {
            int flag = 0;

            if (Regex.IsMatch(textCIN, @"^[a-zA-Z]+[0-9]+$") && textCIN.Length >= 4)
            {
                flag = 1;
            }

            return flag;
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            indice = 2;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.Navy;
            textBox2.ForeColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            indice = 3;
        }

        private void buttonZ82_Click(object sender, EventArgs e)
        {
            write_caractere("A");
        }

        private void buttonZ84_Click(object sender, EventArgs e)
        {
            write_caractere("Z");

        }

        private void buttonZ86_Click(object sender, EventArgs e)
        {
            write_caractere("E");

        }

        private void buttonZ73_Click(object sender, EventArgs e)
        {
            write_caractere("R");

        }

        private void buttonZ75_Click(object sender, EventArgs e)
        {
            write_caractere("T");

        }

        private void buttonZ77_Click(object sender, EventArgs e)
        {
            write_caractere("Y");

        }

        private void buttonZ64_Click(object sender, EventArgs e)
        {
            write_caractere("U");

        }

        private void buttonZ66_Click(object sender, EventArgs e)
        {
            write_caractere("I");

        }

        private void buttonZ68_Click(object sender, EventArgs e)
        {
            write_caractere("O");

        }

        private void buttonZ61_Click(object sender, EventArgs e)
        {
            write_caractere("P");

        }

        private void buttonZ85_Click(object sender, EventArgs e)
        {
            write_caractere("Q");

        }

        private void buttonZ83_Click(object sender, EventArgs e)
        {
            write_caractere("S");

        }

        private void buttonZ81_Click(object sender, EventArgs e)
        {
            write_caractere("D");

        }

        private void buttonZ76_Click(object sender, EventArgs e)
        {
            write_caractere("F");

        }

        private void buttonZ74_Click(object sender, EventArgs e)
        {
            write_caractere("G");

        }

        private void buttonZ72_Click(object sender, EventArgs e)
        {
            write_caractere("H");

        }

        private void buttonZ67_Click(object sender, EventArgs e)
        {
            write_caractere("J");

        }

        private void buttonZ65_Click(object sender, EventArgs e)
        {
            write_caractere("K");

        }

        private void buttonZ63_Click(object sender, EventArgs e)
        {
            write_caractere("L");

        }

        private void buttonZ62_Click(object sender, EventArgs e)
        {
            write_caractere("M");

        }

        private void buttonZ80_Click(object sender, EventArgs e)
        {
            write_caractere("W");

        }

        private void buttonZ79_Click(object sender, EventArgs e)
        {
            write_caractere("X");

        }

        private void buttonZ78_Click(object sender, EventArgs e)
        {
            write_caractere("C");

        }

        private void buttonZ71_Click(object sender, EventArgs e)
        {
            write_caractere("V");

        }

        private void buttonZ70_Click(object sender, EventArgs e)
        {
            write_caractere("B");

        }

        private void buttonZ69_Click(object sender, EventArgs e)
        {
            write_caractere("N");

        }

        private void buttonZ92_Click(object sender, EventArgs e)
        {
            write_caractere(":");

        }

        private void buttonZ91_Click(object sender, EventArgs e)
        {
            write_caractere("_");

        }

        private void buttonZ90_Click(object sender, EventArgs e)
        {
            write_caractere("-");

        }

        private void buttonZ89_Click(object sender, EventArgs e)
        {
            write_caractere(".");

        }

        private void buttonZ88_Click(object sender, EventArgs e)
        {
            write_caractere(" ");

        }

        private void buttonZ87_Click(object sender, EventArgs e)
        {
            write_caractere("  ");

        }

        private void buttonZ60_Click(object sender, EventArgs e)
        {
            write_caractere("@");

        }

        private void buttonZ59_Click(object sender, EventArgs e)
        {
            write_caractere("/");

        }

        private void buttonZ58_Click(object sender, EventArgs e)
        {
            write_caractere("1");

        }

        private void buttonZ56_Click(object sender, EventArgs e)
        {
            write_caractere("2");

        }

        private void buttonZ54_Click(object sender, EventArgs e)
        {
            write_caractere("3");

        }

        private void buttonZ52_Click(object sender, EventArgs e)
        {
            write_caractere("4");

        }

        private void buttonZ51_Click(object sender, EventArgs e)
        {
            write_caractere("5");

        }

        private void buttonZ6_Click(object sender, EventArgs e)
        {
            write_caractere("6");

        }

        private void buttonZ57_Click(object sender, EventArgs e)
        {
            write_caractere("7");

        }

        private void buttonZ55_Click(object sender, EventArgs e)
        {
            write_caractere("8");

        }

        private void buttonZ53_Click(object sender, EventArgs e)
        {
            write_caractere("9");

        }

        private void buttonZ50_Click(object sender, EventArgs e)
        {
            write_caractere("0");

        }

        private void buttonZ4_Click(object sender, EventArgs e)
        {
            delete_caractere();
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.Navy;
            textBox3.ForeColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.Navy;
            textBox4.ForeColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.Navy;
            textBox5.ForeColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            indice = 4;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox4.BackColor = Color.Navy;
            textBox4.ForeColor = Color.White;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void button46_MouseEnter(object sender, EventArgs e)
        {
        }

        private void btnPrepose_MouseEnter(object sender, EventArgs e)
        {
            btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            indice = 5;
        }

        private void btnPrepose_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            try
            {
                label3.Text = "Traitement en cours. Veuillez patienter.";
                label3.ForeColor = Color.Black;
                label3.BackColor = Color.FromArgb(192, 255, 255);
                label3.Refresh();
                btnPrepose.Enabled = false;
                CreationCompteControle creationCompteControle = new CreationCompteControle();
                if (!textCIN.Equals("") && !textNom.Equals("") && !textPrenom.Equals("") && !textNumTel.Equals("") &&
                    !textDateNaissance.Equals(""))
                {
                    string year = textDateNaissance.Substring(4);
                    string month = textDateNaissance.Substring(2, 2);
                    string day = textDateNaissance.Substring(0, 2);
                    String dateNaissance = year + "-" + month + "-" + day;
                    String reponse = creationCompteControle.createCompte(textCIN, textNom, textPrenom, textNumTel, dateNaissance);
                    int codeResponse = TLVHandler.Response(reponse);
                    String TLV = TLVHandler.getTLVChamps(reponse);

                    TLVHandler compteHandler = new TLVHandler(TLV);
                    TLVTags compteTag = compteHandler.getTLV(TLVTags.SOREC_DATA_COMPTE);

                    switch (codeResponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            if (compteTag != null)
                            {
                                DateTime now = DateTime.Now;
                                bool isPrinted = ImpressionService.CreationCompte(
                                        ConfigUtils.ConfigData.Num_pdv, textNom, textPrenom, textCIN, dateNaissance, now.ToString("dd/MM/yyyy H:mm:ss"),
                                        ConfigUtils.ConfigData.Num_pdv + "." +
                                        ConfigUtils.ConfigData.Pos_terminal + " - " +
                                        ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV);
                                JournalUtils.SaveNewAccount(textCIN, now, isPrinted);
                                ApplicationContext.LaunchDepotCompteClientForm(textCIN, textNom, textPrenom);
                            }
                            else
                            {
                                string msgError = "";
                                TLVTags errorListTag = compteHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_ERROR_LIST);
                                if (errorListTag != null)
                                {
                                    TLVHandler errorListTagHandler = new TLVHandler(Utils.bytesToHex(errorListTag.value));
                                    List<TLVTags> errorTags = errorListTagHandler.getTLVList(TLVTags.SOREC_DATA_COMPTE_ERROR);
                                    if (errorTags != null && errorTags.Count > 0)
                                    {
                                        TLVHandler firstErrorTagHandler = new TLVHandler(Utils.bytesToHex(errorTags[0].value));
                                        TLVTags errorMsgTag = firstErrorTagHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_ERROR_MESSAGE);
                                        msgError = errorMsgTag != null ? Utils.HexToASCII(Utils.bytesToHex(errorMsgTag.value)) : "";
                                    }
                                }
                                label3.BackColor = Color.Red;
                                label3.ForeColor = Color.White;
                                label3.Text = "Le compte ne peut pas être créé." + (string.IsNullOrEmpty(msgError) ? "" : "\n" + msgError);
                            }
                            break;
                        case Constantes.RESPONSE_CODE_KO_AUTORISATION_ALLOJEU:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            label3.Text = "L'autorisation de créer un compte est refusée.";
                            break;
                        default:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            label3.Text = "Opération non aboutie: " + codeResponse + ".\nVeuillez réessayer ou contacter l'administration.";
                            break;
                    }
                }
                else
                {
                    label3.BackColor = Color.Red;
                    label3.ForeColor = Color.White;
                    label3.Text = "Veuillez saisir tous les champs obligatoires.";
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Logger.Error("Creation client Form Exception: " + ex.StackTrace);
                label3.BackColor = Color.Red;
                label3.ForeColor = Color.White;
                label3.Text = "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.";
            }
            finally
            {
                btnPrepose.Enabled = true;
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            try
            {
                label3.Text = "Traitement en cours. Veuillez patienter.";
                label3.BackColor = Color.FromArgb(192, 255, 255);
                label3.ForeColor = Color.Black;
                label3.Refresh();
                button46.Enabled = false;

                if (!textCIN.Equals(""))
                {
                    VerificationCompteControle accesCompte = new VerificationCompteControle();

                    string reponse = accesCompte.VerifierCompte(textCIN);
                    int codeResponse = TLVHandler.Response(reponse);
                    switch (codeResponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:

                            string TLV = TLVHandler.getTLVChamps(reponse);
                            TLVHandler compteHandler = new TLVHandler(TLV);
                            TLVTags compteTag = compteHandler.getTLV(TLVTags.SOREC_DATA_COMPTE);
                            if (compteTag != null)
                            {
                                TLVHandler compteTagHandler = new TLVHandler(Utils.bytesToHex(compteTag.value));

                                TLVTags nomTag = compteTagHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_NOM);
                                TLVTags prenomTag = compteTagHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_PRENOM);
                                string nom = Utils.HexToASCII(Utils.bytesToHex(nomTag.value));
                                string prenom = Utils.HexToASCII(Utils.bytesToHex(prenomTag.value));
                                ApplicationContext.LaunchDepotCompteClientForm(textCIN, nom, prenom);
                            }
                            else
                            {
                                string msgError = "";
                                TLVTags errorListTag = compteHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_ERROR_LIST);
                                if (errorListTag != null)
                                {
                                    TLVHandler errorListTagHandler = new TLVHandler(Utils.bytesToHex(errorListTag.value));
                                    List<TLVTags> errorTags = errorListTagHandler.getTLVList(TLVTags.SOREC_DATA_COMPTE_ERROR);
                                    if (errorTags != null && errorTags.Count > 0)
                                    {
                                        TLVHandler firstErrorTagHandler = new TLVHandler(Utils.bytesToHex(errorTags[0].value));
                                        TLVTags errorMsgTag = firstErrorTagHandler.getTLV(TLVTags.SOREC_DATA_COMPTE_ERROR_MESSAGE);
                                        msgError = errorMsgTag != null ? Utils.HexToASCII(Utils.bytesToHex(errorMsgTag.value)) : "";
                                    }
                                }
                                label3.BackColor = Color.Red;
                                label3.ForeColor = Color.White;
                                label3.Text = "L'accès au compte est refusé." + (string.IsNullOrEmpty(msgError) ? "" : "\n" + msgError);
                            }
                            break;
                        case Constantes.RESPONSE_CODE_KO_AUTORISATION_ALLOJEU:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            label3.Text = "L'autorisation d'accès au compte est refusée.";
                            break;
                        default:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            label3.Text = "Opération non aboutie: " + codeResponse  + ".\nVeuillez réessayer ou contacter l'administration.";
                            break;
                    }
                }
                else
                {
                    label3.BackColor = Color.Red;
                    label3.ForeColor = Color.White;
                    label3.Text = "Le champs CIN est obligatoire.";
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Vérification de compte par CIN Exception: " + ex.StackTrace);
                label3.BackColor = Color.Red;
                label3.ForeColor = Color.White;
                label3.Text = "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.";
            }
            finally
            {
                button46.Enabled = true;
            }
        }

        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            if (_datetimeTimer != null)
                _datetimeTimer.Dispose();
            Hide();
        }
    }
}
