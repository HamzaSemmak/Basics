using calipso2020.Model;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleAdministration;
using sorec_gamma.modules.ModuleAdministration.Controls;
using sorec_gamma.modules.UTILS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    public partial class HorsServiceForm : BaseForm
    {
        private Timer _dateTimeTimer;
        private PrinterStatusModel _previousPrinterStatus;
        
        protected override void OnClosing(CancelEventArgs e)
        {
            if (ApplicationContext.imprimante != null)
            {
                ApplicationContext.imprimante.PrinterStatusEventHandler -= printer_HealthCheck;
            }
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            base.OnClosing(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (_dateTimeTimer != null)
                    _dateTimeTimer.Dispose();
            }

            base.Dispose(disposing);
        }

        public HorsServiceForm(PrinterStatusModel printerStatusModel)
        {
            InitializeComponent();
            lblIp.Text = "IP " + ApplicationContext.IP;
            lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv + " - " + ConfigUtils.ConfigData.Pos_terminal;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV;
            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.imprimante.PrinterStatusEventHandler += printer_HealthCheck;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;

            lblErrorMessage.Text = printerStatusModel.Desc;
            lblErrorMessage.ForeColor = Color.White;
            lblErrorMessage.BackColor = Color.Red;
            lblErrorMessage.Refresh();

            EcranClientModel ecranClientModel = new EcranClientModel();
            ecranClientModel.MessageType = MessageType.Simple;
            ecranClientModel.SimpleMsg = "GUICHET HORS SERVICE";
            ApplicationContext.UpdateEcranClient(ecranClientModel);

            _dateTimeTimer = new Timer();
            _dateTimeTimer.Enabled = true;
            _dateTimeTimer.Interval = 1;
            _dateTimeTimer.Tick += new EventHandler(DateTimeTick);

            Panne panne = new Panne(printerStatusModel.Code,
                            printerStatusModel.Desc,
                            printerStatusModel.isBlocking);
            PanneControle.sendRequest(panne);

        }

        private void DateTimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public void SetLblErrorMessage(string txt, Color color)
        {
            lblErrorMessage.Invoke(new MethodInvoker(delegate
            {
                lblErrorMessage.Text = txt;
                lblErrorMessage.ForeColor = Color.White;
                lblErrorMessage.BackColor = color;
                lblErrorMessage.Refresh();
            }));
        }


        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        private void printer_HealthCheck(object sender, EventArgs e)
        {
            try
            {
                if (InvokeRequired && !IsDisposed)
                {
                    Invoke(new EventHandler(printer_HealthCheck),
                         new object[] { sender, e });
                }
                else if (!IsDisposed)
                {
                    PrinterStatusEventArgs printerStatusEventArgs = e as PrinterStatusEventArgs;
                    if (_previousPrinterStatus == null
                        || !_previousPrinterStatus.Equals(printerStatusEventArgs.status))
                    {
                        if (printerStatusEventArgs.status.Equals(PrinterStatusModel.PrinterValid))
                        {
                            ApplicationContext.imprimante.PrinterStatusEventHandler -= printer_HealthCheck;
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            Panne panne = new Panne(printerStatusEventArgs.status.Code,
                                   printerStatusEventArgs.status.Desc,
                                   printerStatusEventArgs.status.isBlocking);
                            SetLblErrorMessage(printerStatusEventArgs.status.Desc, Color.Red);
                            // PanneControle.sendRequest(panne);
                        }
                    }
                    _previousPrinterStatus = printerStatusEventArgs.status;
                }
            }
            catch { }
        }

        private void abandonBtn_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchAuthenticationForm("Imprimante non opérationnelle");
            ApplicationContext.imprimante.PrinterStatusEventHandler -= printer_HealthCheck;
            DialogResult = DialogResult.No;
            Close();
        }

        public override void HideForm()
        {
            Hide();
        }
    }
}
