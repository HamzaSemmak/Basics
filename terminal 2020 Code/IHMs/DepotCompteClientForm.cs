using sorec_gamma.modules.Config;
﻿using log4net;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.ModuleCote_rapport.controls;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.TLV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    public partial class DepotCompteClientForm : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(DepotCompteClientForm));

        private string textCIN;
        private string textNom;
        private string textPrenom;
        private string textMontant;
        private int indice;
        private Timer _dateTimeTimer;

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
        public DepotCompteClientForm(string cin,string nom,string prenom)
        {
            InitializeComponent();
            Initialize(cin, nom, prenom);
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        public void Initialize(string cin = "", string nom = "", string prenom = "")
        {
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;

            _dateTimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _dateTimeTimer.Tick += new EventHandler(_dateTimeTimerTick);


            indice = 4;

            textNom = nom;
            textPrenom = prenom;
            textCIN = cin;
            textBox3.Text = textPrenom;
            textBox2.Text = textNom;
            textBox1.Text = textCIN;
            textBox5.Text = textMontant = "";
            btnPrepose.Enabled = false;
            btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;

            label3.Text = "";
            label3.BackColor = Color.Transparent;

            lblIp.Text = "IP " + ApplicationContext.IP;
            lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                                    + ApplicationContext.SOREC_DATA_ENV;
        }

        private void _dateTimeTimerTick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }


        private void textBox2_Click(object sender, EventArgs e)
        {
            indice = 2;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            indice = 3;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            indice = 4;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            indice = 1;
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

        private void btnPrepose_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            try
            {
                btnPrepose.Enabled = false;
                label3.Text = "Traitement en cours. Veuillez patienter.";
                label3.ForeColor = Color.Black;
                label3.BackColor = Color.FromArgb(192, 255, 255);
                label3.Refresh();
                AlimentationCompteControle depotCompte = new AlimentationCompteControle();
                this.textCIN = this.textBox1.Text;
                this.textNom = this.textBox2.Text;
                this.textPrenom = this.textBox3.Text;

                if (!string.IsNullOrEmpty(textMontant))
                {
                    string reponse = depotCompte.alimenterCompte(this.textCIN, this.textNom, this.textPrenom, this.textMontant);

                    int codeResponse = TLVHandler.Response(reponse);
                    switch (codeResponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            string TLV = TLVHandler.getTLVChamps(reponse);

                            TLVHandler compteHandler = new TLVHandler(TLV);
                            TLVTags compteTag = compteHandler.getTLV(TLVTags.SOREC_DATA_COMPTE);
                            string transactionNum = "12345";
                            if (compteTag != null)
                            {
                                bool isPrinted = ImpressionService.AvisDepot(ConfigUtils.ConfigData.Num_pdv, textNom, textPrenom, textCIN, textMontant,
                                            transactionNum, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), ConfigUtils.ConfigData.Num_pdv + "." +
                                            ConfigUtils.ConfigData.Pos_terminal + " - " +
                                            ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV);
                                if (isPrinted)
                                {
                                    label3.BackColor = Color.LimeGreen;
                                    label3.ForeColor = Color.Black; 
                                    label3.Text = "Compte a été alimenté avec succès.";
                                }
                                else
                                {
                                    label3.BackColor = Color.Red;
                                    label3.ForeColor = Color.White;
                                    label3.Text = "Compte a été alimenté.\nProblème d'imprimante.";
                                }
                                JournalUtils.saveJournalDepot(textCIN, transactionNum, textMontant, isPrinted);
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
                                label3.Text = "Le compte ne peut pas être alimenté." + (string.IsNullOrEmpty(msgError) ? "" : "\n" + msgError);
                            }
                            break;
                        case Constantes.RESPONSE_CODE_KO_AUTORISATION_ALLOJEU:
                            label3.BackColor = Color.Red;
                            label3.ForeColor = Color.White;
                            label3.Text = "L'autorisation de dépôt sur le compte est refusée.";
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
                    label3.Text = "Veuillez saisir un montant.";
                }

                Application.DoEvents();
            }
            catch (Exception ex)
            {                
                Logger.Error("Depot compte Exception: " + ex.StackTrace);

                label3.BackColor = Color.Red;
                label3.ForeColor = Color.White;
                label3.Text = "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.";
            }
            finally
            {
                btnPrepose.Enabled = true;
            }
        }

        private void write_caractere(String c)
        {
            label3.Text = "";
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            switch (indice)
            {
                case 4: 
                    this.textMontant += c; 
                    this.textBox5.Text = textMontant;
                    this.textBox5.Focus();
                    this.textBox5.SelectionStart = this.textMontant.Length;
                    break;
            }
            int flag = fieldsControl();
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
        }
        private int fieldsControl()
        {
            int flag = 0;
            
            if (Regex.IsMatch(this.textMontant, @"^[0-9]+$"))
            {
                flag = 1;
            }

            return flag;
        }
        private void delete_caractere()
        {
            label3.Text = "";
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.White;
            switch (indice)
            {
                case 1:
                    if (textCIN.Length > 0)
                    {
                        this.textCIN = textCIN.Substring(0, textCIN.Length - 1); this.textBox1.Text = textCIN;
                    }
                    break;
                case 2: if (textNom.Length > 0) { this.textNom = textNom.Substring(0, textNom.Length - 1); this.textBox2.Text = textNom; } break;
                case 3:
                    if (textPrenom.Length > 0)
                    {
                        this.textPrenom = textPrenom.Substring(0, textPrenom.Length - 1); this.textBox3.Text = textPrenom;
                    }
                    break;
                case 4:
                    if (textMontant.Length > 0)
                    {
                        this.textMontant = textMontant.Substring(0, textMontant.Length - 1);
                        this.textBox5.Text = textMontant;
                        this.textBox5.Focus();
                        this.textBox5.SelectionStart = this.textMontant.Length;
                    } 
                    break;


            }
            int flag = fieldsControl();
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

        }
        private void buttonZ82_Click(object sender, EventArgs e)
        {
            write_caractere("A");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCréationClientForm();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.Navy;
            textBox2.ForeColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.Navy;
            textBox3.ForeColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox5.BackColor = Color.Navy;
            textBox5.ForeColor = Color.White;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }

        private void button46_MouseEnter(object sender, EventArgs e)
        {
            button46.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }

        private void btnPrepose_MouseEnter(object sender, EventArgs e)
        {
            btnPrepose.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void button46_MouseLeave(object sender, EventArgs e)
        {
            button46.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            if (_dateTimeTimer != null)
                _dateTimeTimer.Dispose();
            Hide();
        }
    }
}
