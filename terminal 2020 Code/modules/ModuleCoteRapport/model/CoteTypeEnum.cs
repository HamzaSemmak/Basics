using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * Created by yelkarkouri@pcard
 */
namespace sorec_gamma.modules.ModuleCote_rapport
{
    public class CoteType
    {
        private CoteType(string code, string label)
        {
            this.code = code;
            this.label = label;
        }
        public string code { get; set; }
        public string label { get; set; }

        public static CoteType SIMPLE { get { return new CoteType("SIMPLE", "Simple"); } }
        public static CoteType JUMELE { get { return new CoteType("JUMELE", "Jumelé"); } }
        public static CoteType RAPPORTS { get { return new CoteType("RAPPORTS", "RAPPORTS"); } }
    }
}
