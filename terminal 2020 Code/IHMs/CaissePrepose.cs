using sorec_gamma.modules.Config;
using log4net;
using sorec_gamma.modules.ModuleBackOffice.Controle;
using sorec_gamma.modules.ModuleImpression;
using sorec_gamma.modules.TLV;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    public partial class CaissePrepose : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(CaissePrepose));

        private System.Windows.Forms.Timer _datetimeTimer;
        private EtatDeCaisseControle etatdeCaisseControle;
        private string dateCaisse = "";
        private string typeCaisse = "";
        private int caisse_Paris_Nombre = 0;
        private decimal caisse_Paris_Cummul = 0;
        private string formatted_caisse_Paris_Cummul ;
        private int caisse_Annulation_Nombre = 0;
        private decimal caisse_Annulation_Cummul = 0;
        private string formatted_caisse_Annulation_Cummul;
        private int caisse_Paiement_Nombre = 0;
        private string formatted_caisse_Paiement_Cummul;
        private decimal caisse_Paiement_Cummul = 0;
        private int caisse_Voucher_Nombre = 0;
        private decimal caisse_Voucher_Cummul = 0;
        private string formatted_caisse_Voucher_Cummul;
        private decimal total = 0;
        private string formatted_total;
        private int nombre_guichet = 0;
        private int caisse_annuSys_Nombre = 0;
        private decimal caisse_annuSys_Cummul = 0;
        private int caisse_annuMachine_Nombre = 0;
        private decimal caisse_annuMachine_Cummul = 0;
        private string formatted_caisse_annuSys_Cummul;
        private string formatted_annuMachine_Cummul;
        private int caisse_depot_Nombre = 0;
        private decimal caisse_depot_Cummul = 0;
        private string formatted_caisse_depot_Cummul;
        private int caisse_paiements_Nombre = 0;
        private decimal caisse_paiements_Cummul = 0;
        private string formatted_caisse_paiements_Cummul;
        private string year = "";
        private string month = "";
        private string day = "";
        private NumberFormatInfo nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }

        public CaissePrepose()
        {
           
            InitializeComponent();
            etatdeCaisseControle = new EtatDeCaisseControle();

            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;

            label1.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV;
            dataGridView1.RowCount = 9;
            dataGridView1.Rows[0].Cells[0].Value = "Distributions";
            dataGridView1.Rows[1].Cells[0].Value = "Annulation Client";
            dataGridView1.Rows[2].Cells[0].Value = "Paiements ";
            dataGridView1.Rows[3].Cells[0].Value = "Distribution Chèques Paris ";
            dataGridView1.Rows[4].Cells[0].Value = "Paiement Chèques Paris ";
            dataGridView1.Rows[5].Cells[0].Value = "Dépôt Sur Compte ";
            dataGridView1.Rows[6].Cells[0].Value = "Annulation Système ";
            dataGridView1.Rows[7].Cells[0].Value = "Annulation Machine ";


            for (int j = 0; j < 8; j++)
            {
                string cell0 = "" + dataGridView1.Rows[j].Cells[0].Value;

                if (cell0 == "Distributions"
                    || cell0 == "Distribution Chèques Paris "
                    || cell0 == "Dépôt Sur Compte ")
                {
                    dataGridView1.Rows[j].Cells[2].Style.ForeColor = Color.Black;
                }
                else
                {
                    dataGridView1.Rows[j].Cells[2].Style.ForeColor = Color.Red;
                    dataGridView1.Rows[j].Cells[2].Style.SelectionForeColor = Color.Red;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                    dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
            }
            dataGridView1.Rows[8].DefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.Rows[8].DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.Rows[8].DefaultCellStyle.SelectionBackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.Rows[8].DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.Rows[8].Cells[0].Value = "";
            dataGridView1.Rows[8].Cells[1].Value = " Solde    ";

            Initilize();
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }


        public void Initilize()
        {
            _datetimeTimer = new System.Windows.Forms.Timer
            {
                Enabled = true,
                Interval = 1
            };
            _datetimeTimer.Tick += new EventHandler(_TimeTick);
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            
            lblGuichet.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblIp.Text = "IP " + ApplicationContext.IP;
            if (Created)
            {
                HideDataUpdateLabelMsg(false, "Les données sont en cours de mise à jour. Veuillez patienter.", Color.White);
            }

            Thread updaterThread = new Thread(() =>
            {
                string result = etatdeCaisseControle.requestEtatCaisse("PDD", DateTime.Now.ToString("yyyy-MM-dd"));
                string tlv = TLVHandler.getTLVChamps(result);
                int coderesponse = TLVHandler.Response(result);
                switch (coderesponse)
                {
                    case Constantes.RESPONSE_CODE_OK:
                        etatdeCaisseControle.getEtatDeCaisse(tlv);
                        nfi.NumberGroupSeparator = " ";

                        this.dateCaisse = etatdeCaisseControle.dateCaisse;
                        this.typeCaisse = etatdeCaisseControle.typeCaisse;
                        this.caisse_Paris_Nombre = etatdeCaisseControle.caisse_Paris_Nombre;
                        this.caisse_Paris_Cummul = etatdeCaisseControle.caisse_Paris_Cummul;
                        formatted_caisse_Paris_Cummul = this.caisse_Paris_Cummul.ToString("#,0.00", nfi);

                        this.caisse_Annulation_Nombre = etatdeCaisseControle.caisse_Annulation_Nombre;
                        this.caisse_Annulation_Cummul = etatdeCaisseControle.caisse_Annulation_Cummul;
                        formatted_caisse_Annulation_Cummul = this.caisse_Annulation_Cummul.ToString("#,0.00", nfi);

                        this.caisse_Voucher_Nombre = etatdeCaisseControle.caisse_Voucher_Nombre;
                        this.caisse_Voucher_Cummul = etatdeCaisseControle.caisse_Voucher_Cummul;
                        formatted_caisse_Voucher_Cummul = this.caisse_Voucher_Cummul.ToString("#,0.00", nfi);

                        this.caisse_Paiement_Nombre = etatdeCaisseControle.caisse_Paiement_Nombre;
                        this.caisse_Paiement_Cummul = etatdeCaisseControle.caisse_Paiement_Cummul;
                        formatted_caisse_Paiement_Cummul = this.caisse_Paiement_Cummul.ToString("#,0.00", nfi);

                        this.caisse_annuSys_Nombre = etatdeCaisseControle.caisse_AnnulSys_Nombre;
                        this.caisse_annuSys_Cummul = etatdeCaisseControle.caisse_AnnulSys_Cummul;
                        formatted_caisse_annuSys_Cummul = this.caisse_annuSys_Cummul.ToString("#,0.00", nfi);
                        
                        this.caisse_annuMachine_Nombre = etatdeCaisseControle.caisse_AnnulMachine_Nombre;
                        this.caisse_annuMachine_Cummul = etatdeCaisseControle.caisse_AnnulMachine_Cummul;
                        formatted_annuMachine_Cummul = this.caisse_annuMachine_Cummul.ToString("#,0.00", nfi);

                        this.caisse_depot_Nombre = etatdeCaisseControle.caisse_depot_Nombre;
                        this.caisse_depot_Cummul = etatdeCaisseControle.caisse_depot_Cummul;
                        formatted_caisse_depot_Cummul = this.caisse_depot_Cummul.ToString("#,0.00", nfi);

                        this.caisse_paiements_Nombre = etatdeCaisseControle.caisse_paiements_Nombre;
                        this.caisse_paiements_Cummul = etatdeCaisseControle.caisse_paiements_Cummul;
                        formatted_caisse_paiements_Cummul = this.caisse_paiements_Cummul.ToString("#,0.00", nfi);

                        this.total = etatdeCaisseControle.caisse_Paris_Cummul - etatdeCaisseControle.caisse_Annulation_Cummul
                            - etatdeCaisseControle.caisse_paiements_Cummul + etatdeCaisseControle.caisse_Voucher_Cummul - etatdeCaisseControle.caisse_Paiement_Cummul
                            + this.caisse_depot_Cummul - this.caisse_annuSys_Cummul - caisse_annuMachine_Cummul;
                        formatted_total = this.total.ToString("#,0.00", nfi);
                        this.nombre_guichet = etatdeCaisseControle.nombre_guichet;
                        HideDataUpdateLabelMsg(true, "", Color.White);

                        break;

                    default:
                        this.caisse_Paris_Nombre = 0;
                        this.caisse_Annulation_Nombre = 0;
                        this.caisse_paiements_Nombre = 0;
                        this.caisse_Voucher_Nombre = 0;
                        this.caisse_Paiement_Nombre = 0;
                        this.caisse_depot_Nombre = 0;
                        this.caisse_annuSys_Nombre = 0;
                        this.caisse_annuMachine_Cummul = 0;
                        this.caisse_annuMachine_Nombre = 0;

                        formatted_caisse_Paris_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_Annulation_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_paiements_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_Voucher_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_Paiement_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_depot_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_caisse_annuSys_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_annuMachine_Cummul = 0.ToString("#,0.00", nfi);
                        formatted_total = 0.ToString("#,0.00", nfi);
                        HideDataUpdateLabelMsg(false, "Une erreur s'est produite lors de la mise à jour. Veuillez réessayer ou contacter l'administration.", Color.Red);
                        break;
                }
                _updateGrid();
            });
            updaterThread.Priority = ThreadPriority.BelowNormal;
            updaterThread.Start();
        }
        private void HideDataUpdateLabelMsg(bool hide, string msg, Color color)
        {
            buttonZ4.Invoke(new MethodInvoker(delegate { buttonZ4.Enabled = hide; }));
            majDataLbl.Invoke(new MethodInvoker(delegate
            {
                majDataLbl.Visible = !hide;
                majDataLbl.Text = msg;
                majDataLbl.ForeColor = color;
            }));
        }
        
        private void _updateGrid()
        {
            dataGridView1.Invoke(new MethodInvoker(delegate
            {
                dataGridView1.Rows[0].Cells[1].Value = this.caisse_Paris_Nombre;
                dataGridView1.Rows[1].Cells[1].Value = this.caisse_Annulation_Nombre;
                dataGridView1.Rows[2].Cells[1].Value = this.caisse_paiements_Nombre;
                dataGridView1.Rows[3].Cells[1].Value = this.caisse_Voucher_Nombre;
                dataGridView1.Rows[4].Cells[1].Value = this.caisse_Paiement_Nombre;
                dataGridView1.Rows[5].Cells[1].Value = this.caisse_depot_Nombre;
                dataGridView1.Rows[6].Cells[1].Value = this.caisse_annuSys_Nombre;
                dataGridView1.Rows[7].Cells[1].Value = this.caisse_annuMachine_Nombre;

                dataGridView1.Rows[0].Cells[2].Value = formatted_caisse_Paris_Cummul;
                dataGridView1.Rows[1].Cells[2].Value = formatted_caisse_Annulation_Cummul;
                dataGridView1.Rows[2].Cells[2].Value = formatted_caisse_paiements_Cummul;
                dataGridView1.Rows[3].Cells[2].Value = formatted_caisse_Voucher_Cummul;
                dataGridView1.Rows[4].Cells[2].Value = formatted_caisse_Paiement_Cummul;
                dataGridView1.Rows[5].Cells[2].Value = formatted_caisse_depot_Cummul;
                dataGridView1.Rows[6].Cells[2].Value = formatted_caisse_annuSys_Cummul;
                dataGridView1.Rows[7].Cells[2].Value = formatted_annuMachine_Cummul;
                dataGridView1.Rows[8].Cells[2].Value = formatted_total;
            }));
        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchJournalForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaisseForm(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaisseForm(1);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaisseForm(3);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaisseForm(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaisseForm(4);
        }


        private void btnClient_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchClientForm();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }

        private void buttonZ5_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            try
            {
                buttonZ4.Enabled = false;
                ImprimerEtatDeCaisse serviceImp = new ImprimerEtatDeCaisse();
                serviceImp.imprimerEtatCaisse(caisse_Paris_Nombre, formatted_caisse_Paris_Cummul, caisse_Annulation_Nombre, formatted_caisse_Annulation_Cummul,
                            caisse_Voucher_Nombre, formatted_caisse_Voucher_Cummul, caisse_Paiement_Nombre,
                            formatted_caisse_Paiement_Cummul, formatted_total, nombre_guichet, caisse_depot_Nombre, formatted_caisse_depot_Cummul,
                            caisse_annuSys_Nombre, formatted_caisse_annuSys_Cummul, caisse_paiements_Nombre, formatted_caisse_paiements_Cummul, year, month, day,
                            caisse_annuMachine_Nombre, formatted_annuMachine_Cummul);
                Application.DoEvents();
            }
            catch(Exception ex)
            {
                Logger.Error("Exception CaissePrepose: " + ex.StackTrace);
            }
            finally
            {
                buttonZ4.Enabled = true;
            }
        }

        private void _TimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public override void HideForm()
        {
            if (_datetimeTimer != null)
                _datetimeTimer.Dispose();
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;

            Hide();
        }

        private void btnPrepose_Click(object sender, EventArgs e)
        {

        }
    }
}
