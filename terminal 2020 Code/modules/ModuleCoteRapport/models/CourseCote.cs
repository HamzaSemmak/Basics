using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.ModulePari;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Created by yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport
{
    [Serializable]
    public class CourseCote : IComparable<CourseCote>
    {
        public StatutCourse StatutCourse {
            get { return _statutCourse; }
            set { _statutCourse = value; } 
        }

        private StatutCourse _statutCourse;
        public string codeHippo { get; set; }
        public string NumReunion { get; set; }
        public string NumCourse { get; set; }
        public ConcurrentBag<ProduitCote> ProduitCotes { get; }

        public CourseCote(string numR, string numC)
        {
            NumCourse = numC;
            NumReunion = numR;
            _statutCourse = StatutCourse.EnVente;
            ProduitCotes = new ConcurrentBag<ProduitCote>();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(NumCourse)
                || string.IsNullOrEmpty(NumReunion)
                || ProduitCotes == null
                || ProduitCotes.IsEmpty;
        }

        public CourseCote(string NumC)
        {
            NumCourse = NumC;
            _statutCourse = StatutCourse.EnVente;
            ProduitCotes = new ConcurrentBag<ProduitCote>();
        }
        public int CompareTo(CourseCote courseCote)
        {
            if (courseCote == null)
                return -1;
            return string.Compare(NumCourse, courseCote.NumCourse, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            CourseCote obj1 = obj as CourseCote;
            return obj1 != null && NumCourse == obj1.NumCourse;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ProduitCotes] NumC: {0}, Liste de produit cotes: ", NumCourse));

            foreach (ProduitCote p in ProduitCotes)
            {
                sb.Append("\n\t" + p.ToString());
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + NumReunion.GetHashCode();
            hash = (hash * 7) + NumCourse.GetHashCode();
            hash = (hash * 7) + ProduitCotes.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateProduitCotes(ProduitCote produitCote)
        {
            if (produitCote == null || produitCote.IsEmpty()) return false;
            ProduitCote tobeUpdatedProduitCote = ProduitCotes.Where(p => { return p.Code == produitCote.Code; }).FirstOrDefault();
            if (tobeUpdatedProduitCote != null)
            {
                tobeUpdatedProduitCote.AddOrUpdateCombCotes(produitCote.CombinaisonCotes);
                tobeUpdatedProduitCote.TotalMises = produitCote.TotalMises;
                tobeUpdatedProduitCote.DateCote = produitCote.DateCote;
            }
            else
            {
                ProduitCotes.Add(produitCote);
            }
            return true;
        }

        public bool AddOrUpdateProduitCotes(ConcurrentBag<ProduitCote> produitCotes)
        {
            if (produitCotes == null || produitCotes.Count < 1)
                return false;
            foreach (ProduitCote pc in produitCotes)
            {
                AddOrUpdateProduitCotes(pc);
            }
            return true;
        }
    }
}
