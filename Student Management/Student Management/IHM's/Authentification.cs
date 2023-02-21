using log4net;
using log4net.Repository.Hierarchy;
using Student_Management.IHM_s.ComposentsGraphique;
using Student_Management.Modules.LoggerManager;
using Student_Management.Modules.UserModel.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management.IHM_s
{
    public partial class Authentification : Form
    {
        public static readonly ILog logger = Log4NetManager.GetLogger(typeof(Authentification));
        UsersController User = new UsersController();
        public Authentification()
        {
            InitializeComponent();
            label2.Text = $"CopyRight © {DateTime.Now.Year} All Right Reserved";
            guna2TextBox1.Select();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            try
            {
                if(guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
                {
                    message = "There is an Empty field";
                    Program.LaunchAlertForm(message);
                }
                else
                {
                    int status = User.Authentification(guna2TextBox1.Text, guna2TextBox2.Text);
                    if (status == 201)
                    {
                        message = "User Name is incorrect, Please try again";
                        Program.LaunchAlertForm(message);
                    }
                    else if(status == 202)
                    {
                        message = "Password is is incorrect, Please try again";
                        Program.LaunchAlertForm(message);
                    }
                    else
                    {
                        this.Hide();
                        Program.LaunchPrincipalForm();
                    }
                }

            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                Program.LaunchAlertForm(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }
    }
}