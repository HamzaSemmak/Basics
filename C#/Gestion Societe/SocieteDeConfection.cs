using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Societe
{
    internal class SocieteDeConfection : Societe
    {
        private int nomberDeMachine;
        private int quantiteArticleProduit;
        private double prixArticle;
        private string marqueOrigine;

        public int NomberDeMachine { get => nomberDeMachine; set => nomberDeMachine = value; }
        public int QuantiteArticleProduit { get => quantiteArticleProduit; set => quantiteArticleProduit = value; }
        public double PrixArticle { get => prixArticle; set => prixArticle = value; }
        public string MarqueOrigine { get => marqueOrigine; set => marqueOrigine = value; }

        public SocieteDeConfection()
        {
            //
        }
        public new string getNom(string nom)
        {
            return $"Société de {nom}, Type Confection";
        }

        public int chiffreAffaire()
        {
            return this.quantiteArticleProduit * Convert.ToInt32(this.prixArticle);
        }
    }
}
