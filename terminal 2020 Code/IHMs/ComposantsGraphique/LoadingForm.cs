using CyUSB;
using log4net;
using sorec_gamma.modules.UTILS;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class LoadingForm : BaseForm
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LoadingForm));
        private static readonly BackgroundWorker backgroundWorker = new BackgroundWorker();
        public LoadingForm()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
            copyrigthLbl.Text = string.Format("© Copyright {0}, SOREC", DateTime.Today.Year);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged +=
                backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        private static void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                backgroundWorker.ReportProgress(2);
                TerminalUtils.initFiles();
                TerminalUtils.InitAddressIP();
                backgroundWorker.ReportProgress(25);

                Thread usbDevicesThread = new Thread(() =>
                {
                    ApplicationContext.UsbDeviceList = new USBDeviceList(CyConst.DEVICES_CYUSB);
                })
                {
                    Name = "DEVICES_CYUSB FINDER"
                };
                usbDevicesThread.Start();
                usbDevicesThread.Join();
                backgroundWorker.ReportProgress(50);

                Thread.Sleep(500);
                ApplicationContext.InitPrinter();
                backgroundWorker.ReportProgress(75);
                Thread.Sleep(2 * 1000);
                ApplicationContext.LaunchCotesUIThread();
                ApplicationContext.InitWebSocketClient();
                backgroundWorker.ReportProgress(85);
                Thread.Sleep(1 * 1000);
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("Exception Entry Point Exception : init usb devices " + ex.StackTrace);
            }
            finally
            {
                backgroundWorker.ReportProgress(99);
                Thread.Sleep(500);
                backgroundWorker.ReportProgress(100, new object());
            }
        }

        private static void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.Error("LoadingForm backgroundWorker_RunWorkerCompleted: " + e.Error.Message);
            }
        }
        
        public override void HideForm()
        {
            if (backgroundWorker != null)
                backgroundWorker.Dispose();
            Hide();
        }
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
                ApplicationContext.LaunchAuthenticationForm("", false);
        }
    }
}
