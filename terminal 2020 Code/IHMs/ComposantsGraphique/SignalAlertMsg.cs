using System;
using System.Windows.Forms;

namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class SignalAlertMsg : Form
    {
        private Form _parent;
        public SignalAlertMsg(Form parent)
        {
            InitializeComponent();
            Owner = new SignalAlert();
            StartPosition = FormStartPosition.CenterScreen;
            _parent = parent;
            // TopMost = true;
        }
       
        private void validateButton_Click(object sender, EventArgs e)
        {
            Owner.Hide();
            Hide();
            if (_parent is PrincipaleForm principaleForm)
                principaleForm.LoadDataSignal();
        }

        public void ShowForm()
        {
            /*msgSignalAlert.Text = Visible ? msg + "\n" + msgSignalAlert.Text : msg;
            msgSignalAlert.Refresh();*/
            if (!Visible)
            {
                Owner.Show(_parent);
                Show();
            }
        }
    }
}
