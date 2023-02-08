using System;
using System.Drawing;
using System.Windows.Forms;
using sorec_gamma.modules.ModulePari.Controls;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.TLV;
using System.Globalization;
using sorec_gamma.modules.ModuleBackOffice.Controle;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.UTILS;
using System.ComponentModel;
using sorec_gamma.modules.ModuleDetailClient.model;
using sorec_gamma.modules.ModuleBJournal.Models;
using System.Text;
using sorec_gamma.modules.Config;
using log4net;

namespace sorec_gamma.IHMs
{
    public partial class ClientForm : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(ClientForm));
        
        private Timer _datetimeTimer;
        private string textA;
        private string textB;
        private string textC;
        private string textD;
        private string textD1;
        private int flag;
        private TraitementTicketControle annulationControl;
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }

        public ClientForm()
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
            _datetimeTimer.Tick += new EventHandler(_TimeTick);

            label1.Text = "";
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;

            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
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

            annulationControl = new TraitementTicketControle();
            flag = 1; textA = ""; textB = ""; textC = ""; textD = ""; textD1 = "";
            lblGuichet.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblIp.Text = "IP " + ApplicationContext.IP;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV;
            textBox2.Text = DateTime.Now.ToString("ddMMyy");
            textB = this.textBox2.Text;
            ChequeJeuControle();
        }

        private void btnPrepose_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaissePreposeForm();
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchJournalForm();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void write_caractere(String c)
        {

            switch (flag)
            {
                case 1:
                    this.textA += c;
                    this.textBox1.Text = textA;
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.textA.Length;
                    break;
                case 2:
                    this.textB += c;
                    this.textBox2.Text = textB;
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.textB.Length;
                    break;
                case 3:
                    this.textC += c;
                    this.textBox3.Text = textC;
                    this.textBox3.Focus();
                    this.textBox3.SelectionStart = this.textC.Length;
                    break;
                case 4:
                    this.textD += c;
                    this.textBox4.Text = textD;
                    this.textBox4.Focus();
                    this.textBox4.SelectionStart = this.textD.Length;
                    break;
                case 5:
                    this.textD1 += c;
                    this.textBox5.Text = textD1;
                    this.textBox5.Focus();
                    this.textBox5.SelectionStart = this.textD1.Length;
                    break;
                case 6:
                    this.textBox9.Text += c;
                    this.textBox9.Text = textBox9.Text;
                    this.textBox9.Focus();
                    this.textBox9.SelectionStart = this.textBox9.Text.Length;
                    break;
            }

        }
        private void ChequeJeuControle()
        {
            if (Decimal.ToInt32(ApplicationContext.sTotal) < 0)
            {
                buttonZ13.Hide();
                chequeJeuBtn.Enabled = true;
                chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
                messageHeader("Chèque jeux", true);
                panelVoucher.Show();
                buttonZ1.Show();
                textBox9.Text = Math.Abs(ApplicationContext.sTotal).ToString();
                flag = 6;
            }
            else
            {
                chequeJeuBtn.Enabled = false;
                messageHeader("Paiement Manuel", false);
                panelVoucher.Hide();
                buttonZ1.Hide();
                buttonZ13.Show();
                flag = 1;
            }
        }
        private void delete_caractere()
        {
            try
            {
                switch (flag)
                {

                    case 1:
                        if (this.textA.Length > 0)
                        {
                            this.textA = textA.Substring(0, textA.Length - 1);
                            this.textBox1.Text = textA;
                            this.textBox1.Focus();
                            this.textBox1.SelectionStart = this.textA.Length;
                        }
                        break;
                    case 2:
                        if (this.textB.Length > 0)
                        {
                            this.textB = textB.Substring(0, textB.Length - 1);
                            this.textBox2.Text = textB;
                            this.textBox2.Focus();
                            this.textBox2.SelectionStart = this.textB.Length;
                        }
                        break;
                    case 3:
                        if (this.textC.Length > 0)
                        {
                            this.textC = textC.Substring(0, textC.Length - 1);
                            this.textBox3.Text = textC;
                            this.textBox3.Focus();
                            this.textBox3.SelectionStart = this.textC.Length;
                        }
                        break;
                    case 4:
                        if (this.textD.Length > 0)
                        {
                            this.textD = textD.Substring(0, textD.Length - 1);
                            this.textBox4.Text = textD;
                            this.textBox4.Focus();
                            this.textBox4.SelectionStart = this.textD.Length;
                        }
                        break;
                    case 5:
                        if (this.textD1.Length > 0)
                        {
                            this.textD1 = textD1.Substring(0, textD1.Length - 1);
                            this.textBox5.Text = textD1;
                            this.textBox5.Focus();
                            this.textBox5.SelectionStart = this.textD1.Length;
                        }
                        break;
                    case 6:
                        if (this.textBox9.Text.Length > 0)
                        {
                            this.textBox9.Text = textBox9.Text.Substring(0, textBox9.Text.Length - 1);
                            this.textBox9.Text = textBox9.Text;
                            this.textBox9.Focus();
                            this.textBox9.SelectionStart = this.textBox9.Text.Length;
                        }
                        break;
                }
            }
            catch
            {
            }

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            controlTextBox1();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            controlTextBox2();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            controlTextBox3();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            controlTextBox4();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            flag = 6;
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

        private void buttonZ47_Click(object sender, EventArgs e)
        {
            delete_caractere();
        }

        private void buttonZ48_Click(object sender, EventArgs e)
        {
            write_caractere("0");
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            controlTextBox5();
        }

        private void buttonZ13_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.BackColor = Color.FromArgb(0, 192, 192);
            label1.Refresh();
            if (!textA.Equals("") && !textB.Equals("") && !textC.Equals("") && !textD.Equals("") && !textD1.Equals(""))
            {
                int idTypeTicket = int.Parse(textA);
                string result = null;
                Ticket ticket = new Ticket();
                Voucher voucher = new Voucher();
                switch (idTypeTicket)
                {
                    case 1:
                        //Ticket jeu
                        try
                        {
                            ticket.DateReunion = DateTime.ParseExact(textB, "ddMMyy", CultureInfo.InvariantCulture);
                            ticket.IdServeur = textC;
                            ticket.IdTicket = textD;
                            ticket.CVNT = textD1;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Client Form Exception: " + ex.Message);
                        }
                        result = annulationControl.TraiterTicket(ticket);
                        break;
                    case 2:
                        // Voucher 
                        PaiementVoucherControle paiementControle = new PaiementVoucherControle();
                        try
                        {
                            voucher.DateSession = DateTime.ParseExact(textB, "ddMMyy", CultureInfo.InvariantCulture);
                            voucher.IdServeur = textC;
                            voucher.IdVoucher = textD;
                            voucher.CVNT = textD1;
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("Client form paiement voucher Exception: " + ex.Message);
                        }
                        result = paiementControle.PayerVoucher(voucher);
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(result))
                {
                    string tlvData = TLVHandler.getTLVChamps(result);
                    TLVHandler appTagsHandler = new TLVHandler(tlvData);
                    int code_reponse = TLVHandler.Response(tlvData);
                    switch (code_reponse)
                    {
                        case Constantes.RESPONSE_CODE_OK:
                            TLVTags voucherDataTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER);
                            TLVTags dataTicketTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_TICKET);
                            TLVTags montantTag;
                            DetailsClientOperation op;
                            if (dataTicketTag != null)
                            {
                                TLVHandler dataTicketHandler = new TLVHandler(Utils.bytesToHex(dataTicketTag.value));
                                montantTag = dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT);
                                ApplicationContext.SOREC_PARI_MISE_TOTALE += decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                ApplicationContext.sTotal += -decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                decimal montantMoins = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);

                                TLVTags typePaiementTag = dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_TYPE_PAIEMENT);
                                try
                                {

                                    if (typePaiementTag != null)
                                    {
                                        string typePaiement = Utils.HexToASCII(Utils.bytesToHex(typePaiementTag.value));
                                        switch (typePaiement)
                                        {
                                            case "TOTAL":
                                                op = new DetailsClientOperation(ticket, montantMoins, TypeOperation.Paiement, TypePaiment.PAIEMENT);
                                                PrincipaleForm.paimentCount++;
                                                PrincipaleForm.paiement += montantMoins;
                                                PrincipaleForm.UpdateDetailClientData();
                                                JournalUtils.saveJournalPaiement("P", ticket, montantMoins);
                                                ApplicationContext.DetailClient.Operations.Add(op);
                                                ApplicationContext.LaunchPrincipaleForm(false, "Paiement effectué", 2);
                                                break;
                                            case "AVANCE":
                                                decimal montant_Avance = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_MONTANT).value)), CultureInfo.InvariantCulture);
                                                string Hippodromme = Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_HIPPODROME).value));
                                                int numReunion = int.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_NUMEROREUNION).value)));
                                                int numCourse = int.Parse(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_NUMEROCOURSE).value)));
                                                string heureCourse = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataTicketHandler.getTLV(TLVTags.SOREC_DATA_TICKET_DATECOURSE).value))).ToString("HH:mm");
                                                ApplicationContext.LaunchGrosGainForm(ticket.IdTicket, ticket.IdServeur, ticket.DateReunion, numReunion, numCourse, Hippodromme, heureCourse, Convert.ToInt32(montant_Avance), true);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ApplicationContext.SOREC_PARI_MISE_TOTALE = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                        op = new DetailsClientOperation(ticket, montantMoins, TypeOperation.Paiement, TypePaiment.ANNULATION);
                                        PrincipaleForm.annulationCount++;
                                        PrincipaleForm.annulation += montantMoins;
                                        PrincipaleForm.UpdateDetailClientData();
                                        ApplicationContext.DetailClient.Operations.Add(op);
                                        JournalUtils.saveJournalPaiement("A", ticket, montantMoins);
                                        ApplicationContext.LaunchPrincipaleForm(false, "Annulation effectuée", 2);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error("Exception load form gros gain: " + ex.Message);
                                }
                            }
                            else if (voucherDataTag != null)
                            {
                                TLVHandler dataVoucherHandler = new TLVHandler(Utils.bytesToHex(voucherDataTag.value));
                                montantTag = dataVoucherHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_MONTANT);
                                ApplicationContext.SOREC_PARI_MISE_TOTALE += decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                ApplicationContext.sTotal += -decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                PrincipaleForm.paimentCount++;
                                PrincipaleForm.paiement += decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture);
                                PrincipaleForm.UpdateDetailClientData();
                                op = new DetailsClientOperation(voucher, decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture), TypeVoucher.PAEIMENT);
                                ApplicationContext.DetailClient.Operations.Add(op);
                                JournalUtils.saveJournalPaiementVoucher(voucher, decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(montantTag.value)), CultureInfo.InvariantCulture));
                                ApplicationContext.LaunchPrincipaleForm(false, "Paiement effectué", 2);
                            }
                            break;
                        case Constantes.RESPONSE_CODE_KO_MODEANNULATION_AUTO:
                            label1.Text = "Annulation automatique non autorisée.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_TICKET_DEJA_ANNULE:
                            label1.Text = "Ticket déjà annulé.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_TICKET_PERDANT:
                            label1.Text = "Ticket perdant.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_TICKET_INVALIDE:
                            label1.Text = "Ticket invalide.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_STATUT_TICKET_INVALIDE:
                            label1.Text = "Statut du ticket invalide.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_AUTORISATION_GROS_GAIN:
                            label1.Text = "Félicitation vous avez gagné.\nVeuillez s'adressez à l'agence la plus proche.";
                            label1.BackColor = Color.Green;
                            label1.ForeColor = Color.White;
                            JournalUtils.saveJournalPaiementErreur("Terminal non autorisé à payer les gros gains.", 0, 0);
                            break;
                        case Constantes.RESPONSE_CODE_KO_LIEU_ANNULATION_NOT_ALLOWED:
                            TLVTags pdvTlv = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                            string pdv = pdvTlv == null ? "un autre PDV" : "le PDV " + Utils.bytesToHex(pdvTlv.value);
                            label1.Text = "Annulation non autorisée.\nTicket enregistré dans " + pdv;
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_LIEU_NOT_ALLOWED:
                        case Constantes.RESPONSE_CODE_KO_LIEU_PAIEMENT_NOT_ALLOWED:
                            TLVTags pdvTlv1 = appTagsHandler.getTLV(TLVTags.SOREC_DATA_NUMERO_PDV);
                            string pdv1 = pdvTlv1 == null ? "un autre PDV" : "le PDV " + Utils.bytesToHex(pdvTlv1.value);
                            label1.Text = "Paiement non autorisé.\nTicket enregistré dans " + pdv1;
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_INVALIDE:
                            label1.Text = "Voucher invalide.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_PAIEMENT_INACTIF:
                            label1.Text = "Paiement des tickets désactivé.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_PAIEMENT_BLOQUE:
                            label1.Text = "Paiement des tickets bloqué.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_STATUT_PDV:
                            label1.Text = "PDV est suspendu.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_TICKET_DEJA_PAYE:
                            label1.Text = "Ticket déjà payé.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_INFORMATIONGAGNANT_DEJA_APPELE:
                            label1.Text = "Gagnant déjà appelé.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_TICKET_NOT_FOUND:
                            label1.Text = "";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_CVNT:
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_CVNV_INVALIDE:
                            label1.Text = "Code CVN est invalide.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_INTROUVABLE:
                            label1.Text = "Voucher est introuvable.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_SUP_PLAFOND:
                            label1.Text = "Montant voucher supérieur au plafond.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_INF_SEUIL:
                            label1.Text = "Montant Voucher inférieur au seuil.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_PAYE:
                            label1.Text = "Voucher déjà payé.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_BLOQUE:
                            label1.Text = "Voucher bloqué.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_PAIEMENT_COURSE_INACTIF:
                            label1.Text = "Course est inactive.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_VOUCHER_EXPIRE:
                            label1.Text = "Voucher expiré.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_STATUT_COURSE_INVALIDE:
                            label1.Text = "Paiement non autorisé.\nVeuillez réessayer plus tard.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case Constantes.RESPONSE_CODE_KO_PRODUIT_INTROUVABLE:
                            label1.Text = "Paiement suspendu.\nVeuillez réessayer plus tard.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        case 500:
                            label1.Text = "Opération non aboutie.\nVeuillez réessayer.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                        default:
                            label1.Text = "Erreur inconnue: " + code_reponse + ".\nVeuillez contacter l'administration.";
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            break;
                    }
                }
                else
                {
                    label1.Text = "Opération non aboutie.\nVeuillez réessayer.";
                    label1.BackColor = Color.Red;
                    label1.ForeColor = Color.White;
                }
            }
            else
            {
                label1.Text = "Veuillez renseigner Les champs manuqants.";
                label1.BackColor = Color.Red;
                label1.ForeColor = Color.White;
            }
        }

        private void chequeJeuBtn_Click(object sender, EventArgs e)
        {
            this.chequeJeuBtn.Enabled = true;
            chequeJeuBtn.Show();
            flag = 6;
            buttonZ13.Hide();
            buttonZ1.Show();
            panelVoucher.Show();
            ChequeJeuControle();
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            flag = 6;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            label1.Text = "";
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            flag = 1;
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
            try
            {
                button5.Enabled = false;
                panelVoucher.Hide();
                buttonZ1.Hide();
                buttonZ13.Show();
                messageHeader("Paiement Manuel", false);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception Client Form: " + ex.StackTrace);
            }
            finally
            {
                button5.Enabled = true;
            }

        }
        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            messageHeader("Chèque jeux", true);
        }

        private void messageHeader(string message, bool chequeJeu)
        {
            headerLbl.Text = message;
            if (chequeJeu)
            {
                chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
            }
            else
            {
                chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
                button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
            }
            if (decimal.ToInt32(ApplicationContext.sTotal) >= 0)
            {
                chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Inactif;
            }
        }

        private void buttonZ1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Refresh();
            decimal montant;
            try
            {
                montant = decimal.Parse(textBox9.Text);
            }
            catch (Exception ex)
            {
                label1.Text = "Veuillez saisir un montant valide";
                label1.BackColor = Color.Red;
                label1.ForeColor = Color.White;
                Logger.Error("Cheque Jeu Exception: " + ex.Message);
                return;
            }
            if (montant == 0)
            {
                label1.Text = "Veuillez saisir un montant différent de zéro";
                label1.BackColor = Color.Red;
                label1.ForeColor = Color.White;
                return;
            }
            if (montant <= Math.Abs(ApplicationContext.sTotal))
            {
                label1.Text = "Traitement en cours. Veuillez patienter.";
                label1.ForeColor = Color.Black;
                label1.BackColor = Color.FromArgb(192, 255, 255);
                label1.Refresh();
                DemandeVoucherControle voucherControle = new DemandeVoucherControle();
                string response = voucherControle.RequestVoucher(montant);
                String TLV = TLVHandler.getTLVChamps(response);
                TLVHandler appTagsHandler = new TLVHandler(TLV);
                int codeResponse = TLVHandler.Response(response);
                switch (codeResponse)
                {
                    case Constantes.RESPONSE_CODE_OK:
                        try
                        {
                            TLVTags voucherDataPariTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER);
                            TLVHandler dataPariHandler = new TLVHandler(Utils.bytesToHex(voucherDataPariTag.value));
                            if (voucherDataPariTag != null)
                            {
                                Voucher voucher = new Voucher();
                                voucher.IdVoucher = Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_IDENTIFIANTCONC).value);
                                voucher.IdServeur = Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_IDSERVEUR).value);
                                voucher.CVNT = Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_CVNV).value));
                                voucher.DateEmission = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_DATEEMISSION).value)));
                                voucher.DateSession = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_DATESESSION).value)));
                                voucher.DateExpiration = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_DATEEXP).value)));
                                voucher.Montant = decimal.Parse(Utils.HexToASCII(Utils.bytesToHex(dataPariHandler.getTLV(TLVTags.SOREC_DATA_VOUCHER_MONTANT).value)), CultureInfo.InvariantCulture);
                                ImprimerChequeJeuService imprimerVoucher = new ImprimerChequeJeuService();
                                bool isPrinted = imprimerVoucher.ChequeJeux(voucher);
                                if (isPrinted)
                                {
                                    ApplicationContext.sTotal += montant;
                                    DetailsClientOperation op = new DetailsClientOperation(voucher, montant, TypeVoucher.DISTRIBUTION);
                                    ApplicationContext.DetailClient.Operations.Add(op);
                                    PrincipaleForm.distributionCount++;
                                    PrincipaleForm.distribution += montant;
                                    PrincipaleForm.UpdateDetailClientData();
                                    ChequeJeuControle();
                                    JournalUtils.saveJournalVoucher(voucher, true);
                                    ApplicationContext.LaunchPrincipaleForm(false, "", 2);
                                }
                                else
                                {
                                    PaiementVoucherControle paiementControle = new PaiementVoucherControle();
                                    string result = paiementControle.PayerVoucher(voucher, false, true);
                                    int annulationRpCode = TLVHandler.Response(result);
                                    switch (annulationRpCode)
                                    {
                                        case Constantes.RESPONSE_CODE_OK:
                                            TerminalUtils.updateLastVoucherInfos(voucher);
                                            JournalUtils.saveJournalVoucher(voucher, false, "CHÈQUE JEUX DÉDUIT MACHINE");
                                            break;
                                        default:
                                            Logger.Error(string.Format("Problème déduit machine VOUCHER N° {0}, ErrorCode {1}", voucher.IdVoucher, annulationRpCode));
                                            JournalUtils.saveJournalVoucher(voucher, false, "CHÈQUE JEUX DÉDUIT SYSTÈME");
                                            break;
                                    }
                                    label1.BackColor = Color.Red;
                                    label1.ForeColor = Color.White;
                                    label1.Text = "ATTENTION !!!" +
                                        "\nLe voucher N° " + voucher.IdVoucher + " a été annulé au serveur.\n" +
                                        "Vous ne devez pas remettre le ticket de ce voucher au parieur.";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            label1.BackColor = Color.Red;
                            label1.ForeColor = Color.White;
                            label1.Text = "Une erreur technique s'est produite.\nVeuillez réessayer ou contacter l'administration.";
                            Logger.Error("Client form exception: " + ex.StackTrace + ex.Message);
                        }
                        break;
                    case Constantes.RESPONSE_CODE_KO:
                        label1.Text = "Problème au niveau serveur.\nVeuillez réessayer ou contacter l'administration.";
                        label1.BackColor = Color.Red;
                        label1.ForeColor = Color.White;
                        break;
                    case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_SUP_PLAFOND:
                        label1.Text = "Le montant saisi dépasse le plafond autorisé.";
                        label1.BackColor = Color.Red;
                        label1.ForeColor = Color.White;
                        break;
                    case Constantes.RESPONSE_CODE_KO_VOUCHER_MONTANT_INF_SEUIL:
                        label1.Text = "Le montant saisi sous le seuil.";
                        label1.BackColor = Color.Red;
                        label1.ForeColor = Color.White;
                        break;
                    default:
                        label1.Text = "Problème au niveau serveur.\nVeuillez réessayer ou contacter l'administration.";
                        label1.BackColor = Color.Red;
                        label1.ForeColor = Color.White;
                        break;
                }
            }
            else
            {
                label1.Text = "Vous avez depassé le montant disponible\n" + Math.Abs(ApplicationContext.sTotal) + "DH";
                label1.BackColor = Color.Red;
                label1.ForeColor = Color.White;
            }
        }

        private void buttonZ1_MouseEnter(object sender, EventArgs e)
        {
            buttonZ1.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }

        private void chequeJeuBtn_MouseEnter(object sender, EventArgs e)
        {
            chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void buttonZ1_MouseLeave(object sender, EventArgs e)
        {
            buttonZ1.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }

        private void chequeJeuBtn_MouseLeave(object sender, EventArgs e)
        {
            chequeJeuBtn.BackgroundImage = Properties.Resources.btnChiffre134_Desel;

        }

        private void _TimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void buttonZ49_Click(object sender, EventArgs e)
        {
            switch (flag)
            {
                case 1:
                    textBox2.Focus();
                    controlTextBox2();
                    break;
                case 2:
                    textBox3.Focus();
                    controlTextBox3();
                    break;
                case 3:
                    textBox4.Focus();
                    controlTextBox4();
                    break;
                case 4:
                    textBox5.Focus();
                    controlTextBox5();
                    break;
            }
        }
        private void controlTextBox2()
        {
            flag = 2;
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
        private void controlTextBox3()
        {
            flag = 3;
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
        private void controlTextBox4()
        {
            flag = 4;
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
        private void controlTextBox5()
        {
            flag = 5;
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
        private void controlTextBox1()
        {
            flag = 1;
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

        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            if (_datetimeTimer != null)
                _datetimeTimer.Dispose();
            Hide();
        }
    }
}
