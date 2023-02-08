using System.Collections.Generic;

namespace sorec_gamma.modules.ModuleCote_rapport.model
{
   public class CoteProduitItemModel
    {
        public string CodeProduit { get; set; }
        public string TotalMises { get; set; }
        public List<CombinaisonCoteProduitModel> CombinaisonCotes { get; set; }
    }
}
