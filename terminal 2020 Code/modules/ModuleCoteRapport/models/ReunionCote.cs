using System;
using System.Text;
using System.Linq;
using sorec_gamma.modules.ModulePari;
using System.Collections.Concurrent;

/**
 * Created by yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport
{
    [Serializable]
    public class ReunionCote : IComparable<ReunionCote>
    {
        public string NumReunion { get; set; }
        public string CodeHippo { get; set; }
        public DateTime DateReunion { get; set; }
        public ConcurrentBag<CourseCote> CourseCotes { get; }

        public ReunionCote(string NumR, string CodeH, DateTime DateR)
        {
            NumReunion = NumR;
            CodeHippo = CodeH;
            DateReunion = DateR;
            CourseCotes = new ConcurrentBag<CourseCote>();
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(NumReunion)
                || DateReunion == null
                || CourseCotes == null
                || CourseCotes.IsEmpty;
        }

        public CourseCote GetFirstCourseCoteEnVente()
        {
            string numC = CourseCotes
                .Where(cc => cc.StatutCourse.Equals(StatutCourse.EnVente) 
                    || cc.StatutCourse.Equals(StatutCourse.DepartDansXX) 
                    || cc.StatutCourse.Equals(StatutCourse.PreDepart))
                .Min(ccEnvente => ccEnvente.NumCourse);
            return CourseCotes.Where(cc => cc.NumCourse.Equals(numC)).FirstOrDefault();
        }

        public int CompareTo(ReunionCote reunionCote)
        {
            if (reunionCote == null) return -1;
            else if (reunionCote.NumReunion != NumReunion)
                return string.Compare(NumReunion, reunionCote.NumReunion, StringComparison.InvariantCulture);
            else return DateReunion.CompareTo(reunionCote.DateReunion);
        }

        public override bool Equals(object obj)
        {
            ReunionCote obj1 = obj as ReunionCote;
            return obj1 != null
                && NumReunion == obj1.NumReunion
                && DateReunion.CompareTo(obj1.DateReunion) == 0;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ReunionCote] NumReunion: {0}, CodeHippo: {1}, DateReunion: {2}, Liste de course cotes: ",
                NumReunion, CodeHippo, DateReunion));

            foreach (CourseCote c in CourseCotes) {
                sb.Append("\n\t" + c.ToString());
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + NumReunion.GetHashCode();
            hash = (hash * 7) + CodeHippo.GetHashCode();
            hash = (hash * 7) + DateReunion.GetHashCode();
            hash = (hash * 7) + CourseCotes.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateCourseCotes(CourseCote courseCote)
        {
            if (courseCote == null || courseCote.IsEmpty()) return false;
            CourseCote tobeUpdatedCourseCote = CourseCotes.Where(c => { return c.Equals(courseCote); }).FirstOrDefault();
            if (tobeUpdatedCourseCote != null)
            {
                tobeUpdatedCourseCote.AddOrUpdateProduitCotes(courseCote.ProduitCotes);
            }
            else
            {
                CourseCotes.Add(courseCote);
            }
            return true;
        }
        public bool AddOrUpdateCourseCotes(ConcurrentBag<CourseCote> courseCotes)
        {
            if (courseCotes == null || courseCotes.Count < 1)
                return false;
            foreach (CourseCote cc in courseCotes)
            {
                AddOrUpdateCourseCotes(cc);
            }
            return true;
        }
    }
}
