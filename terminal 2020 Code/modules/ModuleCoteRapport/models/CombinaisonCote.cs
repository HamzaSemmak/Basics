using System;
using System.Text;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    /**
     * Created by yelkarkouri@PCARD
     */
    [Serializable]
    public class CombinaisonCote : IComparable<CombinaisonCote>
    {
        public string CombPartants { get; set; }
        public string Cote { get; set; }
        public string TotalMise { get; set; }
        public string CodeProduit { get; set; }

        public CombinaisonCote(string CombParts, string Cote, string TotMise, string CodeProd)
        {
            CombPartants = CombParts;
            this.Cote = Cote;
            TotalMise = TotMise;
            CodeProduit = CodeProd;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(CombPartants)
                || string.IsNullOrEmpty(CodeProduit);
        }

        public int CompareTo(CombinaisonCote combinaisonCote)
        {
            if (combinaisonCote == null)
                return -1;
            return string.Compare(CombPartants, combinaisonCote.CombPartants, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            CombinaisonCote obj1 = obj as CombinaisonCote;
            return obj1 != null && CombPartants == obj1.CombPartants;
        }
        public override string ToString()
        {
            return string.Format("[CombinaisonCote] CombPartants: {0}, Cote: {1}, TotalMise: {2}", CombPartants, Cote, TotalMise);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + CombPartants.GetHashCode();
            hash = (hash * 7) + Cote.GetHashCode();
            hash = (hash * 7) + TotalMise.GetHashCode();
            return hash;
        }
    }
}
