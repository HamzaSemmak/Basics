using System;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class CombinaisonRapport : IComparable<CombinaisonRapport>
    {
        public string Combinaison { get; set; }
        public string Mises { get; set; }
        public string Rapport { get; set; }
        public string Type { get; set; }

        public CombinaisonRapport(string com, string mises, string rapport, string type)
        {
            Combinaison = com;
            Mises = mises;
            Rapport = rapport;
            Type = type;
        }

        public int CompareTo(CombinaisonRapport combinaisonRapport)
        {
            if (combinaisonRapport == null)
                return -1;
            if (Type != combinaisonRapport.Type)
                return string.Compare(Type, combinaisonRapport.Type, StringComparison.InvariantCulture);
            return string.Compare(Combinaison, combinaisonRapport.Combinaison, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            CombinaisonRapport obj1 = obj as CombinaisonRapport;
            return obj1 != null && Combinaison == obj1.Combinaison
                && ((Type == null && obj1.Type == null) || Type == obj1.Type);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Combinaison != null ? Combinaison.GetHashCode() : 0;
            hash = (hash * 7) + Mises != null ? Mises.GetHashCode() : 0;
            hash = (hash * 7) + Rapport.GetHashCode();
            hash = (hash * 7) + Type != null ? Type.GetHashCode() : 0;
            return hash;
        }

        public override string ToString()
        {
            return string.Format("[CombinaisonRapport] Type: {0}, CombPartants: {1}, Mises: {2}, Rapport: {3}",
                Type, Combinaison, Mises, Rapport);
        }

        public string GetCombinaisonType()
        {
            string type;
            switch (Type)
            {
                case "N":
                    type = "";
                    break;
                case "O":
                    type = "Ordre";
                    break;
                case "DO":
                    type = "Désordre";
                    break;
                case "B":
                    type = "Bonus";
                    break;
                case "B3":
                    type = "Bonus 3";
                    break;
                case "B4":
                    type = "Bonus 4";
                    break;
                default:
                    type = Type;
                    break;
            }
            return type;
        }

    }

}
