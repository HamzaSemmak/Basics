using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class GroupedProductCote : IComparable<GroupedProductCote>
    {
        public ProductGroupName GroupeName { get; set; }
        public SortedSet<ProduitCote> ProduitCotes { get; }

        public GroupedProductCote()
        {
            ProduitCotes = new SortedSet<ProduitCote>();
        }

        public GroupedProductCote(ProductGroupName group)
        {
            GroupeName = group;
            ProduitCotes = new SortedSet<ProduitCote>();
        }
        public int CompareTo(GroupedProductCote groupedProductCote)
        {
            if (groupedProductCote == null)
                return -1;
            return GroupeName.CompareTo(groupedProductCote.GroupeName);
        }
        public override bool Equals(object obj)
        {
            GroupedProductCote obj1 = obj as GroupedProductCote;
            return obj1 != null && GroupeName == obj1.GroupeName;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[GroupedProductCote] GroupName: {0}, Liste de Product cotes: ", GroupeName));

            foreach (ProduitCote productCote in ProduitCotes)
            {
                sb.Append("\n\t" + productCote.ToString());
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + GroupeName.GetHashCode();
            hash = (hash * 7) + ProduitCotes.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateProductCotes(ProduitCote produitCote)
        {
            if (produitCote == null)  return false;
            ProduitCote tobeUpdatedProdCotes = ProduitCotes.Where(pc => { return pc.Code == produitCote.Code; }).FirstOrDefault();
            if (tobeUpdatedProdCotes != null)
            {
                tobeUpdatedProdCotes.AddOrUpdateCombCotes(produitCote.CombinaisonCotes);
            }
            else
            {
                ProduitCotes.Add(produitCote);
            }
            return true;
        }

        public bool AddOrUpdateProductCotes(SortedSet<ProduitCote> produitCotes)
        {
            if (produitCotes == null || produitCotes.Count < 1)
                return false;
            foreach(ProduitCote pc in produitCotes)
            {
                AddOrUpdateProductCotes(pc);
            }
            return true;
        }
    }
}
