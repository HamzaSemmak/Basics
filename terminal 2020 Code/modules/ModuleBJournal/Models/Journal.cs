using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    [Serializable]
    [XmlRoot]
    public class Journal
    {
        [XmlAttribute]
        public DateTime Session { get; set; }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlAttribute]
        public int NumLastClient { get; set; }

        public List<Operation> Operations { get; }
        
        public Journal()
        {
            Operations = new List<Operation>();
        }
       
        public Journal(ConcurrentJournal journal)
        {
            Session = journal.Session;
            Version = journal.Version;
            NumLastClient = journal.NumLastClient;
            Operations = new List<Operation>();
            List<ConcurrentOperation> sortedOperations = journal.Operations.OrderByDescending(op => op.Heure).ToList();
            foreach (ConcurrentOperation operation in sortedOperations)
            {
                Operations.Add(new Operation(operation));
            }
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{Session: " + Session);
            sb.Append(", Version: " + Version);
            sb.Append(", NumLastClient: " + NumLastClient);
            sb.Append(", Operations: [");
            foreach (Operation op in Operations)
            {
                sb.Append("Op: {");
                sb.Append(op.ToString());
                sb.Append("}");
            }
            sb.AppendLine("]}");
            return sb.ToString();
        }
    }
}
