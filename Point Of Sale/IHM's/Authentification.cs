using log4net;
using Log4Net;
using System;
using System.Windows.Forms;

namespace Point_Of_Sale.IHM_s
{
    public partial class Authentification : Form
    {
        private static ILog logger = LoggerHelper.GetLogger(typeof(Authentification));

        public Authentification()
        {
            InitializeComponent();
            guna2Button1.Focus();
            label4.Text = $"Copyright {DateTime.Now.Year}-{DateTime.Now.Year + 1} All right Reserved";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApp();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled)
                return;
            Authentifications();

        }

        private void Authentifications()
        {
            if(guna2TextBox1 == null || guna2TextBox2 == null)
            {
                //Here
            }
        }
    }
}
