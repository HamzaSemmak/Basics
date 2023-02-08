using System.Collections.Generic;

namespace sorec_gamma.modules.ModuleCote_rapport.model
{
   public  class RapportCoteModel : CoteModel
    {
        public RapportCoteItemModel RapportCoteItem { get; set; }
        public List<RapportCoteItemModel> RapportCoteItem_Dupliateds { get; set; }

    }
}
