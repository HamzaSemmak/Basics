using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    [Serializable]
    public class Operation
    {
        [XmlAttribute]
        public DateTime Heure { get; set; }

        [XmlAttribute]
        public OperationType Type { get; set; }

        public List<Ligne> Lignes { get; }

        public Operation()
        {
            Lignes = new List<Ligne>();
        }

        public Operation(ConcurrentOperation operation)
        {
            Heure = operation.Heure;
            Type = operation.Type;
            Lignes = new List<Ligne>();
            List<ConcurrentLigne> concurrentLignes = operation.Lignes.OrderBy(cl => cl.Order).ToList();
            foreach (ConcurrentLigne ligne in concurrentLignes)
            {
                Lignes.Add(new Ligne(ligne));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Heure: " + Heure);
            sb.Append(", Type: " + Type);
            sb.Append(", Lines: [");
            foreach (Ligne ligne in Lignes)
            {
                sb.Append("Line: {");
                sb.Append(ligne.ToString());
                sb.Append("}");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
