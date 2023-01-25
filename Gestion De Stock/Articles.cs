using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class Articles
    {
        private double prixAchat;
        private double prixDeVente;
        private string nom;
        private string fournisseur;

        public double PrixAchat { get => prixAchat; set => prixAchat = value; }
        public double PrixDeVente { get => prixDeVente; set => prixDeVente = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Fournisseur { get => fournisseur; set => fournisseur = value; }

        public Articles(double prixAchat, double prixDeVente, string nom, string fournisseur)
        {
            this.prixAchat = prixAchat;
            this.prixDeVente = prixDeVente;
            this.nom = nom;
            this.fournisseur = fournisseur;
        }

        public double tauxRendement()
        {
            return this.prixAchat - this.prixDeVente / this.prixAchat;
        }

        public virtual void description()
        {
            Console.WriteLine($"Nom : {this.nom} ");
            Console.WriteLine($"Prix D'Achats : {this.prixAchat} ");
            Console.WriteLine($"Prix De vente : {this.prixDeVente} ");
            Console.WriteLine($"Fournisseur : {this.Fournisseur} ");
            Console.WriteLine($"Taux de Rendement : {this.tauxRendement()} ");
        }
    }
}
