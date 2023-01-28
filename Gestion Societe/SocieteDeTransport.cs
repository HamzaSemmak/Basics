using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Societe
{
    internal class SocieteDeTransport : Societe
    {
        private int nombreDeVehicules;
        private int nombreDeKilometrageParAns;
        private double prixParKilometre;
        private string ligneRoutiere;


        public int NombreDeVehicules { get => nombreDeVehicules; set => nombreDeVehicules = value; }
        public int NombreDeKilometrageParAns { get => nombreDeKilometrageParAns; set => nombreDeKilometrageParAns = value; }
        public double PrixParKilometre { get => prixParKilometre; set => prixParKilometre = value; }
        public string LigneRoutiere { get => ligneRoutiere; set => ligneRoutiere = value; }

        public SocieteDeTransport()
        {
            //
        }
        public new string getNom(string nom)
        {
            return $"Société de {nom}, Type Transport";
        }

        public int chiffreAffaire()
        {
            return this.nombreDeKilometrageParAns  * Convert.ToInt32(this.prixParKilometre);
        }
    }

}
