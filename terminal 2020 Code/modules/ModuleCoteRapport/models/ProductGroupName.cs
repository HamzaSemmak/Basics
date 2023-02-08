using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    public class ProductGroupName : IComparable<ProductGroupName>
    {
        public string Code { get; set; }
        public string Label { get; set; }

        private ProductGroupName(string code, string label)
        {
            Code = code;
            Label = label;
        }

        public int CompareTo(ProductGroupName productGroupName)
        {
            if (productGroupName == null)
                return -1;
            return string.Compare(Code, productGroupName.Code, StringComparison.InvariantCulture);
        }
        public override bool Equals(object obj)
        {
            ProductGroupName obj1 = obj as ProductGroupName;
            return obj1 != null && Code == obj1.Code;
        }
        public override string ToString()
        {
            return string.Format("[ProductGroupName] Code: {0}, Label: {1}", Code, Label);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Code.GetHashCode();
            hash = (hash * 7) + Label.GetHashCode();
            return hash;
        }

        public static ProductGroupName SIMPLE = new ProductGroupName("SIMPLE", "Simplé");
        public static ProductGroupName JUMELE = new ProductGroupName("JUMELE", "Jumelé");
        public static ProductGroupName QUARTE = new ProductGroupName("QUARTE", "Quarté");
        public static ProductGroupName QUINTE = new ProductGroupName("QUINTE", "Quinté");
        public static ProductGroupName TRIO = new ProductGroupName("TRIO", "Trio");
        // ...


    }
}
