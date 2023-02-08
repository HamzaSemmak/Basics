using sorec_gamma.modules.UTILS;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class UnhandledExceptionForm : Form
    {
        public UnhandledExceptionForm(Exception exception)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            int line = 0;
            var filename = "";
            var exMsg = "";
            if (exception != null)
            {
                var st = new StackTrace(exception, true);
                var frame = st.GetFrame(0);
                if (frame != null)
                {
                    line = frame.GetFileLineNumber();
                    filename = frame.GetFileName();
                    if (filename == null) filename = "";
                }
                if (exception.Message != null)
                    exMsg = exception.Message.Replace("\n", "");
            }
            detailsMsg.Text = StringUtils.Truncate(exMsg, 200)
                + "...\nÀ [" + StringUtils.GetLastWord(filename, Path.DirectorySeparatorChar)
                + ", "
                + line + "]";
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            TransparencyKey = BackColor;
            panel1.Hide();
            panel1.Refresh();
            panel2.Hide();
            panel2.Refresh();
            gradientPanel1.Hide();
            gradientPanel1.Refresh();
            Refresh();
            base.OnClosing(e);
        }
    }
}
