using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleAdministration
{
    public class Panne
    {
        public Panne(string Code, string Desc, bool IsBlocking) 
        {
            this.Code = Code; 
            this.Desc = Desc; 
            this.Category = IsBlocking;
        }

        public string Code { get; private set; }
        public string Desc { get; private set; }
        public bool Category { get; private set; }

        public static Panne AbsencePapier { get { return new Panne("AP", "Absence papier", true); } }
        public static Panne CouvercleOuvert { get { return new Panne("CO", "Couvercle Ouvert", true); } }
        public static Panne ProblemePrinter { get { return new Panne("PP", "Problème Imprimante", true); } }
        public static Panne ProblemeEcranClient { get { return new Panne("PECL", "Problème Ecran Client", false); } }
        public static Panne ProblemeEcranCote { get { return new Panne("PECO", "Problème Ecran Côte", false); } }
        public static Panne FinBobinePresque { get { return new Panne("FBP", "Fin de bobine presque", false); } }
        public static Panne ProblemeScanner { get { return new Panne("PS", "Problème Scanner", false); } }
    }
}
