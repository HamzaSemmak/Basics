using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class ArticlesElectromenagers : Articles, VendableParPiece, Susceptible
    {
        private int nomberDePiecesEnStock = 0;

        public int NomberDePiecesEnStock { get => nomberDePiecesEnStock; set => nomberDePiecesEnStock = value; }

        public ArticlesElectromenagers(int nomberDePiecesEnStock, double prixAchat, double prixDeVente, string nom, string fournisseur) : base(prixAchat, prixDeVente, nom,fournisseur)
        {
            this.nomberDePiecesEnStock = nomberDePiecesEnStock;
        }

        public override void description()
        {
            Console.WriteLine("     Articles Electromenagers : ");
            base.description();
            Console.WriteLine($"Nomber de piece en stock : {this.nomberDePiecesEnStock} ");
        }

        public double VenderParPiece(int Quantite)
        {
            this.nomberDePiecesEnStock -= Quantite;
            return this.PrixDeVente - this.PrixAchat * Convert.ToDouble(Quantite);
        }

        public void LancerLeSolde(double Pourcentage)
        {
            Console.WriteLine($"Lancer Le Solde : -{Pourcentage}%");
            Console.WriteLine($"Prix avant Solde : {this.PrixDeVente} ");
            this.PrixDeVente = (Pourcentage * this.PrixDeVente) / 100;
            Console.WriteLine($"Prix Apres Solde : {this.PrixDeVente} ");
        }

        public void TerminerLeSolde(double Pourcentage)
        {
            Console.WriteLine($"Terminer Le Solde : +{Pourcentage}%");
            double AugmenterLePrix = (Pourcentage * this.PrixDeVente) / 100;
            this.PrixDeVente += AugmenterLePrix;
            Console.WriteLine($"Prix Apres Solde est Terminer : {this.PrixDeVente} ");
        }
    }
}
