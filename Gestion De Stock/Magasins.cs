using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class Magasins
    {
        private string depences;
        private int revenus;
        private Object[,] produits;

        public string Depences { get => depences; set => depences = value; }
        public int Revenus { get => revenus; set => revenus = value; }
        public object[,] Produits { get => produits; set => produits = value; }

        public Magasins()
        {
            //
        }

    }
}
