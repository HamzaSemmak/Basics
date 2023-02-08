using System.Drawing;

namespace sorec_gamma.modules.ModuleDetailClient.model
{
    class OperationLigne
    {
        public string Ligne { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public OperationLigne()
        {
            Ligne = "";
            ForeColor = Color.Empty;
            BackColor = Color.Empty;
        }
        public OperationLigne(string ligne,Color foreColor, Color backColor)
        {
            this.Ligne = ligne;
            this.ForeColor = foreColor;
            this.BackColor = backColor;    
        }
    }
}
