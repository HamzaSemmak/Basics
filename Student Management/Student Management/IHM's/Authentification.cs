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
        public Authentification()
        {
            InitializeComponent();
            label2.Text = $"CopyRight © {DateTime.Now.Year} All Right Reserved";
            label3.Text = String.Empty; //Error Field
            guna2TextBox1.Select();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchPrincipalForm();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }
    }
}