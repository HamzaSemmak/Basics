using System.Collections.Generic;

namespace sorec_gamma.modules.ModuleCote_rapport.model
{
    public class RapportCoteItemModel
    {
        public string CodeProduit { get; set; }
        public string TotalMise { get; set; }
        public string StatutRapport { get; set; }
        public List<CombinaisonCoteRapportModel> CombinaisonRapports { get; set; }

    }
}
