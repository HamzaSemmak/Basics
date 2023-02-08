using sorec_gamma.modules.Config;
﻿using log4net;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.ModuleDetailClient.model;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.ModulePari.Controls;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.TLV;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    public partial class GrosGainForm : BaseForm
    {
        private string textCIN;
        private string textNom;
        private string textPrenom;
        private string textcommentaire;
        private string textNTel;
        private int indice;
        private string idConc;
        private string idServeur;
        private int numReunion;
        private string reunion;
        private string heure;
        private int numCourse;
        private DateTime dateReunion;
        private int montant_Avance;
        private Timer _dateTimeTimer;
        private bool IsManuel;
        private readonly ILog Logger = LogManager.GetLogger(typeof(GrosGainForm));

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (_dateTimeTimer != null)
                    _dateTimeTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }
        public GrosGainForm(string idConc, string idServeur, DateTime dateReunion, int numReunion, int numCourse, string hippo, string heure,int montant_Avance,bool isManuel)
        {
            InitializeComponent();
            Initialize(idConc, idServeur, dateReunion, numReunion, numCourse, hippo, heure,montant_Avance,isManuel);
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }


        public void Initialize(string idConc, string idServeur, DateTime dateReunion, int numReunion, int numCourse, string hippo, string heure,int montant_Avance,bool isManuel)
        {
            Invalidate();

            _dateTimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _dateTimeTimer.Tick += new EventHandler(_dateTimeTimerTick);

            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;

            resultErr.BackColor = Color.Transparent;
            resultErr.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textCIN = "";
            textNom = "";
            textPrenom = "";
            textNTel = "";
            textcommentaire = "";


            textBox1_Click(null, null);

            this.idConc = idConc;
            this.idServeur = idServeur;
            this.dateReunion = dateReunion;
            this.numCourse = numCourse;
            this.numReunion = numReunion;
            this.reunion = hippo;
            this.heure = heure;
            this.montant_Avance = montant_Avance;
            this.IsManuel = isManuel;
            lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - "
                + ConfigUtils.ConfigData.Pos_terminal;
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblHipo.Text = this.reunion.ToUpper();
            lblcourse.Text = "Reunion " + this.numReunion + "Course " + this.numCourse + "  " + this.heure;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                                    + ApplicationContext.SOREC_DATA_ENV;
            lblIp.Text = "IP " + ApplicationContext.IP;
        }

        private void delete_caractere()
        {
            resultErr.BackColor = Color.Transparent;
            resultErr.Text = "";
            switch (indice)
            {
                case 1:
                    if (this.textCIN.Length > 0)
                    {
                        this.textCIN = textCIN.Substring(0, textCIN.Length - 1); 
                        this.textBox3.Text = textCIN;
                        this.textBox3.Focus();
                        this.textBox3.SelectionStart = this.textCIN.Length;
                    }
                    break;
                case 2:
                    if (this.textNom.Length > 0)
                    {
                        this.textNom = textNom.Substring(0, textNom.Length - 1); 
                        this.textBox1.Text = textNom;
                        this.textBox1.Focus();
                        this.textBox1.SelectionStart = this.textNom.Length;
                    }
                    break;
                case 3:
                    if (this.textPrenom.Length > 0)
                    {
                        this.textPrenom = textPrenom.Substring(0, textPrenom.Length - 1); 
                        this.textBox2.Text = textPrenom;
                        this.textBox2.Focus();
                        this.textBox2.SelectionStart = this.textPrenom.Length;
                    }
                    break;
                case 4:
                    if (this.textNTel.Length > 0)
                    {
                        this.textNTel = textNTel.Substring(0, textNTel.Length - 1); 
                        this.textBox4.Text = textNTel;
                        this.textBox4.Focus();
                        this.textBox4.SelectionStart = this.textNTel.Length;
                    }
                    break;
                case 5:
                    if (this.textcommentaire.Length > 0)
                    {
                        this.textcommentaire = textcommentaire.Substring(0, textcommentaire.Length - 1); 
                        this.textBox5.Text = textcommentaire;
                        this.textBox5.Focus();
                        this.textBox5.SelectionStart = this.textcommentaire.Length;
                    }
                    break;
            }

            int flag = fieldsControl();
            if (flag == 1)
            {
                button5.Enabled = true;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                button5.Enabled = false;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }

        }
        private void _dateTimeTimerTick(object sender, EventArgs e)
        {

            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");

        }
        private void write_caractere(string c)
        {
            resultErr.BackColor = Color.Transparent;
            resultErr.Text = "";
            switch (indice)
            {
                case 1: 
                    textCIN += c; 
                    this.textBox3.Text = textCIN;
                    this.textBox3.Focus();
                    this.textBox3.SelectionStart = this.textCIN.Length;
                    break;
                case 2: 
                    this.textNom += c; 
                    this.textBox1.Text = textNom;
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.textNom.Length;
                    break;
                case 3: 
                    this.textPrenom += c; 
                    this.textBox2.Text = textPrenom;
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.textPrenom.Length;
                    break;
                case 4:
                    if (textNTel.Length >= 10)
                        return;
                    this.textNTel += c; 
                    this.textBox4.Text = textNTel;
                    this.textBox4.Focus();
                    this.textBox4.SelectionStart = this.textNTel.Length;
                    break;
                case 5: 
                    this.textcommentaire += c;
                    this.textBox5.Text = textcommentaire;
                    this.textBox5.Focus();
                    this.textBox5.SelectionStart = this.textcommentaire.Length;
                    break;
            }
            int flag = fieldsControl();
            if (flag == 1)
            {
                button5.Enabled = true;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                button5.Enabled = false;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }
        }

        private int fieldsControl()
        {
            int flag = 0;
            if (Regex.IsMatch(textNom, @"^[a-zA-Z][a-zA-Z \t _-]*$"))
            {
                if (Regex.IsMatch(textPrenom, @"^[a-zA-Z][a-zA-Z \t _-]*$"))
                {
                    if (Regex.IsMatch(textCIN, @"^[a-zA-Z0-9]+$"))
                    {
                        if (Regex.IsMatch(textNTel, @"^[0-9]+$") && textNTel.Length == 10)
                        {
                            flag = 1;
                        }
                    }
                }
            }
            return flag;
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

        private void buttonZ87_Click(object sender, EventArgs e)
        {
            write_caractere("  ");

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

        private void SetResult(bool error, string msg)
        {
            if (error)
            {
                resultErr.BackColor = Color.Red;
            }
            else
            {
                resultErr.BackColor = Color.Green;
            }
            resultErr.Text = msg;
            resultErr.Refresh();
        }

        private bool isEmpty(string str)
        {
            return str == null || str.Equals("");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;

            try
            {
                button5.Enabled = false;
                button5.Refresh();
                if (isEmpty(textCIN) || isEmpty(textNom) || isEmpty(textPrenom))
                {
                    SetResult(true, "Merci de saisir tous les champs obligatoires.");
                }
                else
                {
                    resultErr.Text = "Traitement en cours. Veuillez patienter.";
                    resultErr.ForeColor = Color.Black;
                    resultErr.BackColor = Color.FromArgb(192, 255, 255);
                    resultErr.Refresh();
                    GagnantContole gagnantcontrole = new GagnantContole();
                    Gagnant gagnant = new Gagnant();

                    gagnant.CIN = textCIN;
                    gagnant.Nom = textNom;
                    gagnant.Prenom = textPrenom;
                    gagnant.NumTele = textNTel;
                    gagnant.Commentaires = textcommentaire;
                    gagnant.IdConc = this.idConc;
                    gagnant.IdServeur = this.idServeur;
                    gagnant.DateReunion = dateReunion;

                    string reponse = gagnantcontrole.PayerGrosGain(gagnant, "AVANCE",IsManuel);
                    int codeResponse = TLVHandler.Response(reponse);
                    string TLV = TLVHandler.getTLVChamps(reponse);

                    TLVHandler compteHandler = new TLVHandler(TLV);
                    TLVTags dataGagnant = compteHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT);
                    switch (codeResponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            TLVHandler dataGagnantHandler = new TLVHandler(Utils.bytesToHex(dataGagnant.value));
                            string nom = Utils.HexToASCII(Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_NOM).value));
                            string prenom = Utils.HexToASCII(Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_PRENOM).value));
                            string idConc = Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_IDENTIFIANTCONC).value);
                            string idServeur = Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_IDSERVEUR).value);
                            string cin = Utils.HexToASCII(Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_CIN).value));
                            string montant_Gain = Utils.HexToASCII(Utils.bytesToHex(dataGagnantHandler.getTLV(TLVTags.SOREC_DATA_GAGNANT_MONTANTGAIN).value));
                            string dateImpression = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            decimal rest = decimal.Parse(montant_Gain, CultureInfo.InvariantCulture) - montant_Avance;
                            try
                            {
                                string pied = "1." + DateTime.Now.ToString("ddMMyy") + "." + idServeur + "." + idConc + ".xxx";
                                bool isPrinted = ImpressionService.AvisGain(nom, prenom, cin, montant_Gain.ToString(),montant_Avance.ToString()
                                    , rest.ToString(), dateImpression,
                                    ConfigUtils.ConfigData.Num_pdv + "." +
                                    ConfigUtils.ConfigData.Pos_terminal + " - " +
                                    ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV,
                                    pied);

                                string resMsg = !isPrinted ? "Traitement terminé. Problème imprimante." : "Traitement terminé avec succès.";
                                resMsg += "\n\nMontant total du gain:  " + montant_Gain + "DH"
                                    + "\nAvance déjà payée:  " + montant_Avance + "DH"
                                    + "\nRestant dû:  " + rest + "DH";

                                SetResult(!isPrinted, resMsg);
                                JournalUtils.saveJournalGL(this.idConc, decimal.Parse(montant_Gain, CultureInfo.InvariantCulture), isPrinted);
                                PrincipaleForm.paimentCount++;
                                PrincipaleForm.paiement += montant_Avance;
                                DetailsClientOperation op = new DetailsClientOperation(decimal.Parse(montant_Gain, CultureInfo.InvariantCulture), TypeOperation.Paiement, TypePaiment.GROS_GAIN);
                                ApplicationContext.DetailClient.Operations.Add(op);
                                PrincipaleForm.UpdateDetailClientData();
                                if (isPrinted)
                                {
                                    ApplicationContext.LaunchPrincipaleForm(false, "", 2);
                                }
                            }
                            catch (Exception ex)
                            {
                                SetResult(true, "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.");
                                Logger.Error("Exception Trace: " + ex.StackTrace + ex.Message);
                            }
                            break;
                        case Constantes.RESPONSE_CODE_KO_PAIEMENT_INACTIF:
                            SetResult(true, "Paiement des tickets désactivé.");
                            break;
                        case Constantes.RESPONSE_CODE_KO_PAIEMENT_BLOQUE:
                            SetResult(true, "Paiement des tickets bloqué.");
                            break;
                        default:
                            SetResult(true, "L'opération a échoué. Code serveur: " + codeResponse + ".\nVeuillez contacter l'administration.");
                            break;
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                SetResult(true, "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.");
                Logger.Error("Gros Gain Exception Trace" + ex.StackTrace + ex.Message);
            }
            finally
            {
                button5.Enabled = true;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            indice = 2;
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
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            indice = 3;
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
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            indice = 1;
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
            textBox4.BackColor = Color.Navy;
            textBox4.ForeColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            indice = 5;
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
        }

        private void buttonZ88_Click(object sender, EventArgs e)
        {
            ApplicationContext.SOREC_PARI_MISE_TOTALE -= montant_Avance;
            ApplicationContext.sTotal = 0;
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void buttonZ88_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void buttonZ88_MouseLeave(object sender, EventArgs e)
        {
            buttonZ88.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

        }

        private void buttonZ1_Click(object sender, EventArgs e)
        {
            write_caractere("-");
        }

        private void buttonZ2_Click(object sender, EventArgs e)
        {
            write_caractere("_");
        }

        private void buttonZ31_Click(object sender, EventArgs e)
        {
            write_caractere(":");
        }

        private void buttonZ36_Click(object sender, EventArgs e)
        {
            write_caractere("/");
        }

        private void buttonZ35_Click(object sender, EventArgs e)
        {
            write_caractere("@");
        }

        private void buttonZ34_Click(object sender, EventArgs e)
        {
            write_caractere(".");
        }

        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            _dateTimeTimer.Dispose();
            Hide();
        }

    }
}

