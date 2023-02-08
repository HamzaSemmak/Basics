using System;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class LineCote : IComparable<LineCote>
    {
        public string CombPartants { get; set; }
        public string CoteProduit1 { get; set; }
        public string CoteProduit2 { get; set; }

        public LineCote()
        {
        }

        public LineCote(string combParts, string coteProd1, string coteProd2 = null)
        {
            CombPartants = combParts;
            CoteProduit1 = coteProd1;
            CoteProduit2 = coteProd2;
        }
        public int CompareTo(LineCote lineCote)
        {
            if (lineCote == null)
                return -1;
            return string.Compare(CombPartants, lineCote.CombPartants, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            LineCote obj1 = obj as LineCote;
            return obj1 != null && CombPartants == obj1.CombPartants;
        }
        public override string ToString()
        {
            return string.Format("[LineCote] CombPartans: {0}, CoteProduit1: {1}, CoteProduit2: {2}", CombPartants, CoteProduit1, CoteProduit2);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            if (CombPartants != null) hash = (hash * 7) + CombPartants.GetHashCode();
            if (CoteProduit1 != null) hash = (hash * 7) + CoteProduit1.GetHashCode();
            if (CoteProduit2 != null) hash = (hash * 7) + CoteProduit2.GetHashCode();
            return hash;
        }
    }
}
