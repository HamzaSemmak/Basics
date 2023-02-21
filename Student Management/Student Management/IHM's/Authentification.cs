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
            try
            {
                string msg;
                if (guna2TextBox1.Text == "" && guna2TextBox2.Text == "")
                {
                    msg = "Please There is an empty field";
                    Program.LaunchAlertForm(msg);
                }
                else
                {
                    string status = User.Authentification(guna2Button1.Text, guna2Button2.Text);
                    Program.LaunchAlertForm(status);
                    if (status == "201")
                    {
                        msg = "User Name is invalid please try again";
                        Program.LaunchAlertForm(msg);
                    }
                    if (status == "200")
                    {
                        Program.LaunchPrincipalForm();
                    }
                    if (status == "202")
                    {
                        msg = "Password is invalid please try again";
                        Program.LaunchAlertForm(msg);
                    }
                }
            }
            catch(Exception ex)
            {
                Program.LaunchAlertForm(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }
    }
}