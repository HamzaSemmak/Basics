using System.Text;

namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.models
{
    public class PartantEventModel
    {
		public string Statut { get; set; }

		public int Numero { get; set; }

		public string Nom { get; set; }

		public string Ecurie { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Statut: " + Statut);
            sb.Append(", Numero: " + Numero);
            sb.Append(", Nom: " + Nom);
            sb.Append(", Ecurie: " + Ecurie);
            return sb.ToString();
        }
    }
}
