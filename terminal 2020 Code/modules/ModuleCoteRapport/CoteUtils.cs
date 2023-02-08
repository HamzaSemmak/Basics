using CS_CLIB;
using sorec_gamma.modules.ModuleCote_rapport.model;
using System.Collections.Generic;

/**
 * Created by yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleEcranClient
{
    class CoteUtils
    {
        public static Tracing logger = new Tracing("cotes");

        public static string GetRapportBande(List<RapportCoteModel> rapports)
        {
            string data = "";
            foreach (RapportCoteModel rr in rapports)
            {
                if (rr == null)
                    continue;
                if (data == "")
                    data = "R" + rr.NumReunion + "    C" + rr.NumCourse + "   RAPPORTS   ";
                RapportCoteItemModel pR = rr.RapportCoteItem;
                if (pR != null)
                {
                    data += GetProductCode(pR.CodeProduit);
                    switch (pR.StatutRapport)
                    {
                        case "RAPPORT":
                            List<CombinaisonCoteRapportModel> combinaisonRapport = pR.CombinaisonRapports;
                            foreach (CombinaisonCoteRapportModel cR in combinaisonRapport)
                            {
                                string combinaison = cR.RapportCombinaison.Replace(" ", "-");
                                string rapport1DH = cR.Rapport1DH;
                                data += "   " + combinaison + " : " + rapport1DH;
                            }
                            break;
                    }
                }
            }
            return data;
        }

        public static string GetProductCode(string code)
        {
            string produit;
            switch (code)
            {
                case "GAG": produit = "GA"; break;
                case "PLA": produit = "PL"; break;
                case "JUG": produit = "JG"; break;
                case "JUP": produit = "JP"; break;
                case "TRO": produit = "TRIO"; break;
                case "TRC": produit = "TIERCE"; break;
                case "QUU": produit = "QUARTE"; break;
                case "QAP": produit = "QUARTE+"; break;
                case "QIP": produit = "QUINTE+"; break;
                default: produit = code; break;
            }
            return produit;
        }
    }
}
