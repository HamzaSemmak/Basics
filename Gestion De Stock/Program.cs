using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_De_Stock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Articles 
            //Articles articles = new Articles(30, 50, "Women", "Hamza Semmak");
            //Console.WriteLine(articles.tauxRendement());
            //articles.description();
            //Console.WriteLine();

            //Articles Primeures
            ArticlesPrimeurs articleprimeurs = new ArticlesPrimeurs(50, 10, 15, "Cuiere", "Tarik Oulkhabou");
            articleprimeurs.description();
            Console.WriteLine($"Vender Par Kilogramme : {articleprimeurs.VenderParKilogramme(20)}");
            Console.WriteLine($"Vender Par Piece : {articleprimeurs.VenderParPiece(10)}");
            Console.WriteLine($"nomber De Pieces En Stock apres le vente : {articleprimeurs.NomberDePiecesEnStock}");
            Console.WriteLine();

            ArticlesElectromenagers articleseletromenages = new ArticlesElectromenagers(10, 30, 50, "Women", "Hamza Semmak");
        }
    }
}
