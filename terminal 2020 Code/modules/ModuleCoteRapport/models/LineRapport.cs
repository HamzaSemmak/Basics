using System;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class LineRapport : IComparable<LineRapport>
    {
        public string Code { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public bool IsHeader { get; set; }
        public bool IsComposite { get { return Code == "SIMPLE" || Code == "JUMELE"; } }
        public LineRapport Parent { get; set; }

        public LineRapport()
        {
        }
        
        public LineRapport(string code, string column1, string column2, string column3, LineRapport parent, bool isHeader = false)
        {
            Code = code;
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            IsHeader = isHeader;
            Parent = parent;
        }

        public int CompareTo(LineRapport lineRapport)
        {
            if (lineRapport == null)
                return -1;
            if (Column1 != lineRapport.Column1)
                return string.Compare(Column1, lineRapport.Column1, StringComparison.InvariantCulture);
            if (Column2 != lineRapport.Column2)
                return string.Compare(Column2, lineRapport.Column2, StringComparison.InvariantCulture);
            return string.Compare(Code, lineRapport.Code, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            LineRapport obj1 = obj as LineRapport;
            return obj1 != null && Code == obj1.Code && Column1 == obj1.Column1
                && (IsComposite || Column2 == obj1.Column2);
        }

        public override string ToString()
        {
            return string.Format("[LineRapport] Column1: {0}, Column2: {1}, Column3: {2}, IsHeader: {3}", Column1, Column2, Column3, IsHeader);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            if (Column1 != null) hash = (hash * 7) + Column1.GetHashCode();
            if (Column2 != null) hash = (hash * 7) + Column2.GetHashCode();
            if (Column3 != null) hash = (hash * 7) + Column3.GetHashCode();
            hash = (hash * 7) + IsHeader.GetHashCode();
            return hash;
        }

        public string GetOrder()
        {
            string order = "";
            switch (Code)
            {
                case "SIMPLE":
                    order = IsHeader ? "1" : Column1;
                    break;
                case "JUMELE":
                    order = IsHeader ? "2" : (Column2 != "" ? Column2 : Column3);
                    break;
                case "TRO":
                    order = IsHeader ? "3" : Column3;
                    break;
                case "TRC":
                    order = IsHeader ? "4" : Column1 + Column1;
                    break;
                case "QUU":
                    order = IsHeader ? "5" : Column1 + Column1;
                    break;
                case "QAP": 
                    order = IsHeader ? "6" : Column1 + Column1;
                    break;
                case "QIP": 
                    order = IsHeader ? "7" : Column1 + Column1;
                    break;
            }
            return order;
        }
    }
}
