namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public class DataworkerJournal
    {
        public ConcurrentJournal Journal { get; set; }
        public JournalLite JournalLite { get; set; }

        public DataworkerJournal(ConcurrentJournal journal, JournalLite journalLite)
        {
            Journal = journal;
            JournalLite = journalLite;
        }
    }
}
