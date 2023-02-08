using System;

namespace sorec_gamma.modules.ModuleCote_rapport.model
{
   public class CoteModel
    {
        public CoteType CoteType { get; set; }
        public string SlidingFooter { get; set; }
        public DateTime DateCote { get; set; }
        public string Hippodromme { get;  set; }
        public string StatutCote { get; set; }
        public int NumReunion { get; set; }
        public int NumCourse { get; set; }
        public DateTime DateReunion { get; set; }
    }
}
