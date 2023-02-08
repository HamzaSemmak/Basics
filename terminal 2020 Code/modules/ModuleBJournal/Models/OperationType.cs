using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace sorec_gamma.modules.ModuleBJournal.Models
{
    public enum OperationType
    {
        Demarrage,
        Connexion,
        Login,
        Message,
        Critéres,
        Distribution,
        FinClient,
        Paiement,
        Voucher,
        DepotSurCompte
    }
}
