using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmploye_Examen
{
    internal interface PEmploye
    {
        int Age();

        int Anciennete();

        int DateRetrate(int ageRetraite);

        void ToStrings();
    }
}
