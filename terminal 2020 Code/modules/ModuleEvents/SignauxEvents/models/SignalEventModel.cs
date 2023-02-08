using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.models
{
    public class SignalEventModel
    {
        public string Identifiant { get; }

        public DateTime Timestamp { get; set; }

        public DateTime DateReunion { get; set; }
        public string LibReunion { get; set; }

        public int NumeroReunion { get; set; }

        public int NumeroCourse { get; set; }

        public string Type { get; set; }

        public CourseEventModel Course { get; set; }

        public List<PartantEventModel> Partants { get; set; }

        public ProduitEventModel Produit { get; set; }

        public VersionMTModel Version { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Identifiant: " + Identifiant);
            sb.Append(", Timestamp: " + Timestamp);
            sb.Append(", DateReunion: " + DateReunion);
            sb.Append(", LibReunion: " + LibReunion);
            sb.Append(", NumeroReunion: " + NumeroReunion);
            sb.Append(", NumeroCourse: " + NumeroCourse);
            sb.Append(", Type: " + Type);
            sb.Append(", Course: " + (Course != null ? Course.ToString() : null));
            if (Partants != null)
            {
                sb.Append(", Partants count: " + Partants.Count);
            }
            else
            {
                sb.Append(", Partants: Null");
            }
            sb.Append(", Produit: " + (Produit != null ? Produit.ToString() : null));
            sb.Append(", Version: " + (Version != null ? Version.ToString() : null));
            return sb.ToString();
        }
    }
}
