using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class ConcurrentJournal
    {
        public DateTime Session { get; }
        public string Version { get; }
        public int NumLastClient { get; set; }
        public ConcurrentBag<ConcurrentOperation> Operations { get; }
        
        public ConcurrentJournal(DateTime session)
        {
            Session = session;
            Version = "DAMAGED";
            NumLastClient = 0;
            Operations = new ConcurrentBag<ConcurrentOperation>();
        }
                
        public ConcurrentJournal(DateTime session, string version, int nombreClt)
        {
            Session = session;
            Version = version;
            NumLastClient = nombreClt;
            Operations = new ConcurrentBag<ConcurrentOperation>();
        }

        public ConcurrentJournal(Journal journal)
        {
            Session = journal.Session;
            Version = journal.Version;
            NumLastClient = journal.NumLastClient;
            Operations = new ConcurrentBag<ConcurrentOperation>();
            List<Operation> sortedOperations = journal.Operations.OrderByDescending(op => op.Heure).ToList();
            foreach (Operation operation in sortedOperations)
            {
                Operations.Add(new ConcurrentOperation(operation));
            }
        }

        public ConcurrentJournal(ConcurrentJournal journal)
        {
            Session = journal.Session;
            Version = journal.Version;
            NumLastClient = journal.NumLastClient;
            Operations = new ConcurrentBag<ConcurrentOperation>();
            List<ConcurrentOperation> sortedOperations = journal.Operations.OrderByDescending(op => op.Heure).ToList();
            foreach (ConcurrentOperation operation in sortedOperations)
            {
                Operations.Add(new ConcurrentOperation(operation));
            }
        }

        public int GetTotalLines()
        {
            int total = 0;
            foreach (ConcurrentOperation operation in Operations)
            {
                if (operation.Lignes != null)
                {
                    total += operation.Lignes.Count;
                }
            }
            return total;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("ConcurrentJournal: {");
            _ = sb.Append("Session: " + Session);
            _ = sb.Append(", Version: " + Version);
            _ = sb.Append(", NumLastClient: " + NumLastClient);
            _ = sb.Append(", Operations: [");
            foreach (ConcurrentOperation op in Operations)
            {
                _ = sb.Append(op.ToString());
            }
            _ = sb.AppendLine("]}");
            return sb.ToString();
        }

        public void AddOperation(ConcurrentOperation operation)
        {
            if (operation == null)
            {
                return;
            }
            Operations.Add(operation);
        }
    }
}
