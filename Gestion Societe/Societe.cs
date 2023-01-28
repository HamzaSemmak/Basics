using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Societe
{
    internal class Societe
    {
        static int newRegisterCommerceDeSociete = 1;
        private string nomDeSociete;
        private string adresseDeSociete;
        private int chiffreAffaireDeSociete;
        private int nombereSalariesDeSociete;
        private int registerCommerceDeSociete;

        public string NomDeSociete { get => nomDeSociete; set => nomDeSociete = value; }
        public string AdresseDeSociete { get => adresseDeSociete; set => adresseDeSociete = value; }
        public int ChiffreAffaireDeSociete { get => chiffreAffaireDeSociete; set => chiffreAffaireDeSociete = value; }
        public int NombereSalariesDeSociete { get => nombereSalariesDeSociete; set => nombereSalariesDeSociete = value; }
        public int RegisterCommerceDeSociete { get => registerCommerceDeSociete; set => registerCommerceDeSociete = value; }

        public Societe()
        {
            this.registerCommerceDeSociete = newRegisterCommerceDeSociete++;
        }

        public string getNom(string nom)
        {
            return $"Société de {nom}";
        }
    }
}
