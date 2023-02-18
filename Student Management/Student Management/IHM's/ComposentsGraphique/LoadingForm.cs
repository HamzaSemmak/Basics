using log4net;
using log4net.Repository.Hierarchy;
using Student_Management.Modules.LoggerManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management.IHM_s.ComposentsGraphique
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2CircleProgressBar1.Increment(1);
            if (guna2CircleProgressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                Program.LaunchAuthentification();
            }
        }
    }
}
