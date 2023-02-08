using System;
using System.Text;

namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.models
{
    public class CourseEventModel
    {
        public string Statut { get; set; }

        public string Resultats { get; set; }

        public DateTime? HeureDepartEff { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Statut: " + Statut);
            sb.Append(", Resultats: " + Resultats);
            sb.Append(", HeureDepartEff: " + HeureDepartEff);
            return sb.ToString();
        }
    }
}
