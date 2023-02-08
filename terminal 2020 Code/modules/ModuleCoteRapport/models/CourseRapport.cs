using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class CourseRapport : IComparable<CourseRapport>
    {
        public DateTime Timestamp { get; set; }
        public string ReferenceCalc { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Reunion { get; set; }
        public string Course { get; set; }
        public string LibReunion { get; set; }
        public ConcurrentBag<ProduitRapport> Produits { get; set; }
        public bool Displayed { get; set; }

        public CourseRapport()
        {
            Produits = new ConcurrentBag<ProduitRapport>();
        }
        public CourseRapport(DateTime date, string numReunion, string numCourse)
        {
            Date = date;
            Reunion = numReunion;
            Course = numCourse;
            Produits = new ConcurrentBag<ProduitRapport>();
        }

        public int CompareTo(CourseRapport courseRapport)
        {
            if (courseRapport == null || courseRapport.Reunion != Reunion || courseRapport.Course != Course) 
                return -1;
            else
                return Date.CompareTo(courseRapport.Date);
        }

        public override bool Equals(object obj)
        {
            CourseRapport obj1 = obj as CourseRapport;
            return obj1 != null 
                && Date.CompareTo(obj1.Date) == 0
                && Course == obj1.Course
                && Reunion == obj1.Reunion;
        }

        public bool EqualsHigthLevel(CourseRapport courseRapport)
        {
            return Equals(courseRapport)
                && Timestamp.CompareTo(courseRapport.Timestamp) == 0;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = Date == null ? 0 : (hash * 7) + Date.GetHashCode();
            hash = Reunion == null ? 0 : (hash * 7) + Reunion.GetHashCode();
            hash = Course == null ? 0 : (hash * 7) + Course.GetHashCode();
            hash = Produits == null ? 0 : (hash * 7) + Produits.GetHashCode();
            hash = LibReunion == null ? 0 : (hash * 7) + LibReunion.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateProduitRapport(ProduitRapport produitRapport)
        {
            if (produitRapport == null) return false;
            ProduitRapport tobeUpdatedProduitRaport = Produits.Where(pr => { return pr.Equals(produitRapport); }).FirstOrDefault();
            if (tobeUpdatedProduitRaport != null)
            {
                tobeUpdatedProduitRaport.Produit = produitRapport.Produit;
                tobeUpdatedProduitRaport.Statut = produitRapport.Statut;
                tobeUpdatedProduitRaport.Mises = produitRapport.Mises;
                tobeUpdatedProduitRaport.AddOrUpdateCombinaisonRapports(produitRapport.Rapports);
            }
            else
            {
                Produits.Add(produitRapport);
            }
            return true;
        }

        public bool AddOrUpdateProduitRapport(ConcurrentBag<ProduitRapport> produitRapports)
        {
            if (produitRapports == null || produitRapports.Count < 1)
                return false;
            foreach (ProduitRapport pr in produitRapports)
            {
                AddOrUpdateProduitRapport(pr);
            }
            return true;
        }

        public override string ToString()
        {
            if (!IsContainsProducts())
                return "";
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("     {0}  R{1}.C{2}  RAPPORTS ", LibReunion, Reunion, Course));
            IOrderedEnumerable<ProduitRapport> ordredProds = Produits.OrderBy(p => p.GetOrder());
            foreach (ProduitRapport pr in ordredProds)
            {
                if (!pr.CanBeDisplayed())
                    continue;
                if (pr.Statut == "RAPPORT")
                {
                    foreach (CombinaisonRapport cr in pr.Rapports)
                    {
                        string combs = string.Format("{0} : {1} {2}", cr.Combinaison.Replace(" ", " - "), cr.GetCombinaisonType(), cr.Rapport);
                        sb.Append(string.Format("  {0}  {1}   ", pr.Produit, combs));
                    }
                }
                else if (pr.Statut == "REMBOURSEMENT")
                {
                    sb.Append(string.Format("  {0}  REMBOURSEMENT   ", pr.Produit));
                }
            }
            return sb.ToString();
        }

        public bool IsContainsProducts()
        {
            return Produits != null && !Produits.IsEmpty && Produits.Any(pr => pr.CanBeDisplayed());
        }
    }
}

