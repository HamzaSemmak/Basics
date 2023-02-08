using System.Text;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class ConcurrentLigne
    {
        public string Value1 { get; }
        public string Value2 { get; }
        public string Value3 { get; }
        public string BackColor { get; }
        public string ForColor { get; }
        public int Order { get; }

        public ConcurrentLigne(Ligne ligne)
        {
            Value1 = ligne.Value1;
            Value2 = ligne.Value2;
            Value3 = ligne.Value3;
            BackColor = ligne.BackColor;
            ForColor = ligne.ForColor;
            Order = ligne.Order;
        }
        
        public ConcurrentLigne(ConcurrentLigne ligne)
        {
            Value1 = ligne.Value1;
            Value2 = ligne.Value2;
            Value3 = ligne.Value3;
            BackColor = ligne.BackColor;
            ForColor = ligne.ForColor;
            Order = ligne.Order;
        }

        public ConcurrentLigne(string value1, string value2, string value3, string backColor = "Gray", string forColor = "Black")
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            BackColor = backColor;
            ForColor = forColor;
        }
        public ConcurrentLigne(string value1, string value2, string value3, int order, string backColor = "Gray", string forColor = "Black")
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            BackColor = backColor;
            ForColor = forColor;
            Order = order;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("ConcurrentLigne: {");
            sb.Append("Value1: " + Value1);
            sb.Append(", Value2: " + Value2);
            sb.Append(", Value3: " + Value3);
            sb.Append(", BackColor: " + BackColor);
            sb.Append(", ForColor: " + ForColor);
            sb.Append("}");
            return sb.ToString();
        }
    }
}
