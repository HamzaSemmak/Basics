using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmploye_Examen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The Entry Point Of Program
            Formateur f1 = new Formateur("Hamza", 2001, 2022, 4500, 2, 70);
            Employe p = new Employe("Hamza", 2001, 2022, 90000);

            Console.Write(p.DateRetraite(60));



            Console.WriteLine();
        }
    }
}
