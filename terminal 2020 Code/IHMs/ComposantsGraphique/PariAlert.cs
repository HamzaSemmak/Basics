using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class PariAlert : Form
    {
        public PariAlert(string numTicket)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            msgAlert.Text = "La transaction N° " + numTicket
                + " a été annulée au serveur."
                + "Vous ne devez pas remettre le ticket de cette transaction au parieur.";
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
