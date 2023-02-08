using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class ContentRapport
    {
        public string NumCourse;
        public string NumReunion;
        public string CodeHippo;
        public DateTime DateReunion;
        public DateTime DateTimeRapport;
        
        public ConcurrentBag<LineRapport> LineRapports { get; }

        public ContentRapport()
        {
            LineRapports = new ConcurrentBag<LineRapport>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[ContentRapport]: "));
            foreach(LineRapport lineRapport in LineRapports) {
                sb.Append("\n\t" + lineRapport.ToString());
            }
            return sb.ToString();
        }

        public bool AddOrUpdateLineRapports(LineRapport lineRapport, object thirdColumn)
        {
            if (lineRapport == null)
                return false;
            LineRapport toBeUpdated = LineRapports.Where(lc => lineRapport.Equals(lc)).FirstOrDefault();
            if (toBeUpdated != null)
            {
                toBeUpdated.Code = lineRapport.Code;
                toBeUpdated.Parent = lineRapport.Parent;
                toBeUpdated.Column1 = lineRapport.Column1;

                if (thirdColumn == null)
                {
                    toBeUpdated.Column2 = lineRapport.Column2;
                    toBeUpdated.Column3 = lineRapport.Column3;
                }
                else if ((bool)thirdColumn == true)
                {
                    toBeUpdated.Column3 = lineRapport.Column3;
                }
                else if ((bool)thirdColumn == false)
                {
                    toBeUpdated.Column2 = lineRapport.Column2;
                }
            }
            else
            {
                LineRapports.Add(lineRapport);
            }
            return true;
        }

        public bool AddOrUpdateLineRapports(List<LineRapport> lineRapports)
        {
            if (lineRapports == null || lineRapports.Count < 1)
                return false;
            for (int i = lineRapports.Count - 1; i >= 0; i--)
            {
                AddOrUpdateLineRapports(lineRapports[i], null);
            }
            return true;
        }

        public bool IsEmpty()
        {
            return LineRapports == null || LineRapports.IsEmpty;
        }
    }
}
