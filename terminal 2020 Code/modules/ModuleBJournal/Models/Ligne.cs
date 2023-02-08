using System;
using System.Text;
using System.Xml.Serialization;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    [Serializable]
    public class Ligne
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }

        [XmlAttribute]
        public string BackColor { get; set; }

        [XmlAttribute]
        public string ForColor { get; set; }
        [XmlAttribute]
        public int Order { get; set; }

        public Ligne()
        {
        }

        public Ligne(ConcurrentLigne line)
        {
            Value1 = line.Value1;
            Value2 = line.Value2;
            Value3 = line.Value3;
            BackColor = line.BackColor;
            ForColor = line.ForColor;
            Order = line.Order;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Value1: " + Value1);
            sb.Append(", Value2: " + Value2);
            sb.Append(", Value3: " + Value3);
            sb.Append(", BackColor: " + BackColor);
            sb.Append(", ForColor: " + ForColor);
            return sb.ToString();
        }
    }
}
