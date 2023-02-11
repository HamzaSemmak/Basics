using log4net;
using log4net.Repository.Hierarchy;
using Log4Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Point_Of_Sale.IHM_s.ComposantsGraphique
{
    public partial class LoadingForm : Form
    {
        private static readonly BackgroundWorker backgroundWorkerworker = new BackgroundWorker();
        private static readonly ILog Logger = LoggerHelper.GetLogger(typeof(Program));
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.CloseApp();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2CircleProgressBar1.Increment(1);
            if(guna2CircleProgressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                Program.LaunchAuthentification();
                Logger.Info("Authentification Form...");
            }
        }
    }
}
