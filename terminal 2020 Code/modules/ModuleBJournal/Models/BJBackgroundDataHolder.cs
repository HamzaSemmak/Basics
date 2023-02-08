using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class BJBackgroundDataHolder
    {
        public DateTime DateTime { get; set; }
        public OperationType Type { get; set; }
        public List<ConcurrentLigne> Lignes { get; set; }

        public BJBackgroundDataHolder(OperationType type, List<ConcurrentLigne> lignes, DateTime dateTime)
        {
            Type = type;
            Lignes = lignes;
            DateTime = dateTime;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BJBackgroundDataHolder: {");
            sb.Append("Date: ");
            sb.Append(DateTime);
            sb.Append("Type: ");
            sb.Append(Type);
            sb.Append(", Lines: [");
            foreach (ConcurrentLigne ligne in Lignes)
            {
                sb.Append("Line: {");
                sb.Append(ligne.ToString());
                sb.Append("}");
            }
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
