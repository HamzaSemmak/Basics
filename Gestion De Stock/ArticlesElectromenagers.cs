using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class ArticlesElectromenagers : Articles, VendableParPiece
    {
        private int nomberDePiecesEnStock;

        public int NomberDePiecesEnStock { get => nomberDePiecesEnStock; set => nomberDePiecesEnStock = value; }

        public ArticlesElectromenagers(int nomberDePiecesEnStock, double prixAchat, double prixDeVente, string nom, string fournisseur) : base(prixAchat, prixDeVente, nom,fournisseur)
        {
            this.nomberDePiecesEnStock = nomberDePiecesEnStock;
        }

        public override void description()
        {
            base.description();
            Console.WriteLine($"Nomber de piece en stock : {this.nomberDePiecesEnStock} ");
        }

        public int VenderParPiece(int Quantite)
        {
            return 1;
        }
    }
}
