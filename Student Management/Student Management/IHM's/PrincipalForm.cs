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
    public partial class PrincipalForm : Form
    {
        public static DateTime dateTime = DateTime.Now;
        public PrincipalForm()
        {
            InitializeComponent();
            label1.Text = $"{dateTime.DayOfWeek}, {dateTime.Day} {dateTime.ToString("MMMM")} {dateTime.Year}";

            //Test 
            //String[] arr1 = new string[] { "1", "Hamza Semmak", "21", "21" };
            //String[] arr2 = new string[] { "1", "Hamza Semmak", "21", "21" };
            //String[] arr3 = new string[] { "1", "Hamza Semmak", "21", "21" };
            //String[] arr4 = new string[] { "1", "Hamza Semmak", "21", "21" };
            //Object[] rows = new object[] { arr1, arr2, arr3, arr4 };
            //foreach (string[] rowArr in rows)
            //{
            //    this.guna2DataGridView1.Rows.Add(rowArr);
            //}
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Program.CloseApplication();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LaunchPrincipalForm();
        }
    }
}