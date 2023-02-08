using System;

namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class JournalLite
    {
        public string Filename { get; set; }
        public DateTime Session { get; set; }
        public bool IsLast { get; set; }
        public bool IsFirst { get; set; }

        public JournalLite(string filename, DateTime session, bool isLast, bool isFirst)
        {
            Filename = filename;
            Session = session;
            IsLast = isLast;
            IsFirst = isFirst;
        }
    }
}
