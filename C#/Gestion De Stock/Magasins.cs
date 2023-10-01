using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class Magasins
    {
        public double depences;
        public double revenus;
        public ArticlesElectromenagers articlesElectromenagers;
        public ArticlesPrimeurs articlesPrimeurs;

        public Magasins(double depences, double revenus, ArticlesElectromenagers articlesElectromenagers, ArticlesPrimeurs articlesPrimeurs)
        {
            this.depences = depences;
            this.revenus = revenus;
            this.articlesElectromenagers = articlesElectromenagers;
            this.articlesPrimeurs = articlesPrimeurs;
        }

        public void Info()
        {
            Console.WriteLine($"Depences : {this.depences} ");
            Console.WriteLine($"Revenus : {this.revenus} ");
            Console.WriteLine("     Articles Electromenagers : ");
            Console.WriteLine($"Nom : {this.articlesElectromenagers.Nom} ");
            Console.WriteLine($"Prix D'Achats : {this.articlesElectromenagers.PrixAchat} ");
            Console.WriteLine($"Prix De vente : {this.articlesElectromenagers.PrixDeVente} ");
            Console.WriteLine($"Fournisseur : {this.articlesElectromenagers.Fournisseur} ");
            Console.WriteLine($"Nomber de piece en stock Articles Electromenagers : {this.articlesElectromenagers.NomberDePiecesEnStock} ");
            Console.WriteLine($"Taux de Rendement : {this.articlesElectromenagers.tauxRendement()} ");
            Console.WriteLine("     Articles Primeurs : ");
            Console.WriteLine($"Nom : {this.articlesPrimeurs.Nom} ");
            Console.WriteLine($"Prix D'Achats : {this.articlesPrimeurs.PrixAchat} ");
            Console.WriteLine($"Prix De vente : {this.articlesPrimeurs.PrixDeVente} ");
            Console.WriteLine($"Fournisseur : {this.articlesPrimeurs.Fournisseur} ");
            Console.WriteLine($"Nomber de piece en stock : {this.articlesPrimeurs.NomberDePiecesEnStock} ");
            Console.WriteLine($"Taux de Rendement : {this.articlesPrimeurs.tauxRendement()} ");
        }

        public double tauxRendement()
        {
            return (this.revenus - this.depences) / this.depences;
        }


    }
}
