using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class ContentCote : IComparable<ContentCote>
    {
        public string HeureCote { get; set; }
        public string NumReunion { get; set; }
        public string Hippo { get; set; }
        public string NumCourse { get; set; }
        public bool IsTwoColumns { get; set; }
        public string Column1Name { get; set; }
        public string Column2Name { get; set; }
        public string MisesTotal1 { get; set; }
        public string MisesTotal2 { get; set; }
        public CoteOrderBy OrderBy { get; set; }
        public ConcurrentBag<LineCote> LineCotes { get; }

        public ContentCote()
        {
            LineCotes = new ConcurrentBag<LineCote>();
        }

        public int CompareTo(ContentCote lineCote)
        {
            if (lineCote == null)
                return -1;
            return string.Compare(NumReunion, lineCote.NumReunion, StringComparison.InvariantCulture)
                   + string.Compare(NumCourse, lineCote.NumCourse, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            ContentCote obj1 = obj as ContentCote;
            return obj1 != null && NumCourse == obj1.NumCourse && NumReunion == obj1.NumReunion;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ContentCote] HeureCote: {0}, NumReunion: {1}, NumCourse: {2}, Column1Name: {3}, Column2Name: {4}, IsTwoColumns: {5}",
               HeureCote, NumReunion, NumCourse, Column1Name, Column2Name, IsTwoColumns));
            foreach(LineCote lineCote in LineCotes) {
                sb.Append("\n\t" + lineCote.ToString());
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int hash = 13;
            if (HeureCote != null) hash = (hash * 7) + HeureCote.GetHashCode();
            if (NumReunion != null) hash = (hash * 7) + NumReunion.GetHashCode();
            if (NumCourse != null) hash = (hash * 7) + NumCourse.GetHashCode();
            hash = (hash * 7) + IsTwoColumns.GetHashCode();
            if (Column1Name != null) hash = (hash * 7) + Column1Name.GetHashCode();
            if (Column2Name != null) hash = (hash * 7) + Column2Name.GetHashCode();
            return hash;
        }

        public bool AddOrUpdateLineCotes(LineCote lineCote, bool secondColumn)
        {
            if (lineCote == null)
                return false;
            LineCote toBeUpdatedLineCote = LineCotes.Where(lc => lc.CombPartants == lineCote.CombPartants).FirstOrDefault();
            if (toBeUpdatedLineCote != null)
            {
                if (secondColumn)
                    toBeUpdatedLineCote.CoteProduit2 = lineCote.CoteProduit2;
                else
                {
                    toBeUpdatedLineCote.CoteProduit1 = lineCote.CoteProduit1;
                }
            }
            else
            {
                LineCotes.Add(lineCote);
            }
            return true;
        }

        public bool IsEmpty()
        {
            return LineCotes == null || LineCotes.Count < 1;
        }
    }
}
