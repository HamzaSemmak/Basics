using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.models
{
    public class ProduitEventModel
    {
        public string Statut {get;set;}

        public List<ProduitModel> Produits { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Statut: " + Statut);
            sb.Append(", Produits: " + Produits);
            return sb.ToString();
        }
    }
}
