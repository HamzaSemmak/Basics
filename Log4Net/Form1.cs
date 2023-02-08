using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log4Net
{
    public partial class Form1 : Form
    {
        ILog Logger = LoggerHelper.GetLogger(typeof(Form1));
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger.Debug($"Clicked button1_Click {sender} - {e} ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logger.Debug($"Clicked button2_Click {sender} - {e} ");
        }
    }
}
