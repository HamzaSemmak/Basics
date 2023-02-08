using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class ConcurrentOperation
    {
        public DateTime Heure { get; }
        public OperationType Type { get; set; }
        public ConcurrentBag<ConcurrentLigne> Lignes { get; }
        
        public ConcurrentOperation(Operation operation)
        {
            Heure = operation.Heure;
            Type = operation.Type;
            Lignes = new ConcurrentBag<ConcurrentLigne>();
            List<Ligne> sortedLines = operation.Lignes.OrderBy(cl => cl.Order).ToList();
            foreach (Ligne ligne in sortedLines)
            {
                Lignes.Add(new ConcurrentLigne(ligne));
            }
        }

        public ConcurrentOperation(ConcurrentOperation operation)
        {
            Heure = operation.Heure;
            Type = operation.Type;
            Lignes = new ConcurrentBag<ConcurrentLigne>();
            List<ConcurrentLigne> sortedLines = operation.Lignes.OrderBy(cl => cl.Order).ToList();
            foreach (ConcurrentLigne ligne in sortedLines)
            {
                Lignes.Add(new ConcurrentLigne(ligne));
            }
        }

        public ConcurrentOperation(DateTime dateTime)
        {
            Heure = dateTime;
            Lignes = new ConcurrentBag<ConcurrentLigne>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("CocncurrentOperation: {");
            sb.Append("Heure: " + Heure);
            sb.Append(", Type: " + Type);
            sb.Append(", Lignes: [");
            foreach (ConcurrentLigne ligne in Lignes)
            {
                sb.Append(ligne.ToString());
            }
            sb.Append("]}");
            return sb.ToString();
        }
    }
}
