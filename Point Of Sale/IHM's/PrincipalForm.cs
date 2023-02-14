using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Point_Of_Sale.IHM_s
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm(string UserName)
        {
            InitializeComponent();
            label2.Text = UserName.ToString();
            label3.Text = $"{DateTime.Now.Day.ToString()}/{DateTime.Now.Month.ToString()}/{DateTime.Now.Year.ToString()}";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Program.CloseApp();
        }
    }
}
