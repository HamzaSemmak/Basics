using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class ProduitCote : IComparable<ProduitCote>
    {
        public DateTime DateCote { get; set; }
        public string Code { get; set; }
        public string TotalMises { get; set; }
        public ConcurrentBag<CombinaisonCote> CombinaisonCotes { get; }

        public ProduitCote(string CodeProd, DateTime DateCote, string TotMises)
        {
            Code = CodeProd;
            this.DateCote = DateCote;
            TotalMises = TotMises;
            CombinaisonCotes = new ConcurrentBag<CombinaisonCote>();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Code)
                || CombinaisonCotes == null
                || CombinaisonCotes.IsEmpty;
        }

        public int CompareTo(ProduitCote produitCote)
        {
            if (produitCote == null)
                return -1;
            return string.Compare(Code, produitCote.Code, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            ProduitCote obj1 = obj as ProduitCote;
            return obj1 != null && Code == obj1.Code;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ProduitCote] CodeP: {0}, DateCote: {1}, TotMises: {2}, Liste de combinaison cotes: ", Code, DateCote, TotalMises));

            foreach (CombinaisonCote cc in CombinaisonCotes)
            {
                sb.Append("\n\t" + cc.ToString());
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Code.GetHashCode();
            hash = (hash * 7) + DateCote.GetHashCode();
            hash = (hash * 7) + TotalMises.GetHashCode();
            hash = (hash * 7) + CombinaisonCotes.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateCombCotes(CombinaisonCote combCote)
        {
            if (combCote == null || combCote.IsEmpty())  return false;
            CombinaisonCote tobeUpdatedCombCote = CombinaisonCotes.Where(cc => { return cc.CombPartants == combCote.CombPartants; }).FirstOrDefault();
            if (tobeUpdatedCombCote != null)
            {
                tobeUpdatedCombCote.Cote = combCote.Cote;
                tobeUpdatedCombCote.TotalMise = combCote.TotalMise;
            }
            else
            {
                CombinaisonCotes.Add(combCote);
            }
            return true;
        }

        public bool AddOrUpdateCombCotes(ConcurrentBag<CombinaisonCote> combCotes)
        {
            if (combCotes == null || combCotes.Count < 1)
                return false;
            foreach(CombinaisonCote cc in combCotes)
            {
                AddOrUpdateCombCotes(cc);
            }
            return true;
        }
    }
}
