using sorec_gamma.modules.Config;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class SeuilConfirmation : Form
    {
        public SeuilConfirmation()
        {
            InitializeComponent();
        }
        public SeuilConfirmation(long total)
        {
            InitializeComponent();
            msgLabel.Text = "Attention : la transaction d'une \n valeur  de " + total + " DH dépasse\n  le seuil  fixé à " +
                    ConfigUtils.ConfigData.SeuilAlerte + " DH.";
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Tag = false;
            Close();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            Tag = true;
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
