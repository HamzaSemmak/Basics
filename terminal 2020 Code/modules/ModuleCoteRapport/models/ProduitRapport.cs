using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class ProduitRapport : IComparable<ProduitRapport>
    {
        public string Produit { get; set; }
        public string Mises { get; set; }
        public string Statut { get; set; }
        public ConcurrentBag<CombinaisonRapport> Rapports { get; set; }
        public ProduitRapport()
        {
            Rapports = new ConcurrentBag<CombinaisonRapport>();
        }

        public ProduitRapport(string code, string mises, string status)
        {
            Produit = code;
            Mises = mises;
            Statut = status;
            Rapports = new ConcurrentBag<CombinaisonRapport>();
        }

        public int CompareTo(ProduitRapport produitRapport)
        {
            if (produitRapport == null)
                return -1;
            return string.Compare(Produit, produitRapport.Produit, StringComparison.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            ProduitRapport obj1 = obj as ProduitRapport;
            return obj1 != null && Produit == obj1.Produit;
        }
        public override int GetHashCode()
        {
            int hash = 13;
            hash = Produit == null ? 0 : (hash * 7) + Produit.GetHashCode();
            hash = Mises == null ? 0 : (hash * 7) + Mises.GetHashCode();
            hash = Statut == null ? 0 : (hash * 7) + Statut.GetHashCode();
            hash = Rapports == null ? 0 : (hash * 7) + Rapports.GetHashCode();
            return hash;
        }
        public bool AddOrUpdateCombinaisonRapports(ConcurrentBag<CombinaisonRapport> rapports)
        {
            if (rapports == null || rapports.Count < 1)
                return false;
            foreach (CombinaisonRapport cr in rapports)
            {
                AddOrUpdateCombinaisonRapport(cr);
            }
            return true;
        }

        public bool AddOrUpdateCombinaisonRapport(CombinaisonRapport rapport)
        {
            if (rapport == null) return false;
            CombinaisonRapport tobeUpdatedCombinaisonRapport = Rapports.Where(r => { return r.Equals(rapport); }).FirstOrDefault();
            if (tobeUpdatedCombinaisonRapport != null)
            {
                tobeUpdatedCombinaisonRapport.Combinaison = rapport.Combinaison;
                tobeUpdatedCombinaisonRapport.Rapport = rapport.Rapport;
                tobeUpdatedCombinaisonRapport.Mises = rapport.Mises;
                tobeUpdatedCombinaisonRapport.Type = rapport.Type;
            }
            else
            {
                Rapports.Add(rapport);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ProduitRapport] CodeP: {0}, Mises: {1}, Status: {2}",
                Produit, Mises, Statut));
            if (IsRapport())
            {
                foreach (CombinaisonRapport cr in Rapports)
                {
                    sb.Append("\n\t" + cr.ToString());
                }
            }
            else if (IsRemboursement())
            {
                sb.Append(", Remboursé");
            }
            return sb.ToString();
        }

        public bool CanBeDisplayed()
        {
            return Statut == "RAPPORT" || Statut == "REMBOURSEMENT";
        }
        
        public bool IsRapport()
        {
            return Statut == "RAPPORT";
        }
        
        public bool IsRemboursement()
        {
            return Statut == "REMBOURSEMENT";
        }
        
        public int GetOrder()
        {
            int order = 0;
            switch (Produit)
            {
                case "GAG":
                    order = 1;
                    break;
                case "PLA":
                    order = 2;
                    break;
                case "JUG":
                    order = 3;
                    break;
                case "JUO":
                    order = 4;
                    break;
                case "JUP":
                    order = 5;
                    break;
                case "TRO":
                    order = 6;
                    break;
                case "TRC":
                    order = 7;
                    break;
                case "QUU":
                    order = 8;
                    break;
                case "QAP":
                    order = 9;
                    break;
                case "QIP":
                    order = 10;
                    break;
            }
            return order;
        }
    }
}
