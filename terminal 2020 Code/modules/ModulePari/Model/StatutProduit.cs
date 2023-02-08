using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModulePari
{
    public enum StatutProduit
    {
        EnVenteProduit = 1,
        ArretVenteProduit = 2,
        Remboursement = 3,
        RemboursementArrete = 4,
        Paiement = 5,
        Undefined = -1
    }

}
