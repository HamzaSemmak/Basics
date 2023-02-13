using log4net;
using Log4Net;
using Point_Of_Sale.Modules.Config;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Point_Of_Sale.IHM_s
{
    public partial class Authentification : Form
    {
        private static ILog logger = LoggerHelper.GetLogger(typeof(Authentification));

        public Authentification()
        {
            InitializeComponent();
            guna2TextBox3.Focus();
            label4.Text = $"Copyright {DateTime.Now.Year}-{DateTime.Now.Year + 1} All right Reserved";
            label5.Hide();
        }
        private void Authentifications()
        {
            string error = string.Empty;
            if(guna2TextBox2.Text == "" || guna2TextBox3.Text == "")
            {
                label5.Show();
                ThrowError("Error : There is an empty field");
            }
            else
            {
                label5.Show();
                if (guna2TextBox3.Text.Equals("Hamza Semmak") && guna2TextBox2.Text.Equals("AA102374h"))
                {
                    this.Hide();
                    Program.LanchPrincipalForm(guna2TextBox3.Text);
                }
                else
                {
                    ThrowError("Error : User Name Or Password is Incorrect Please Try Again");
                }
            }
        }

        private void ThrowError(string error)
        {
            label5.Text = error; logger.Error(error);
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
    }
}
