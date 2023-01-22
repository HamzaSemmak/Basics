using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmploye_Examen
{
    internal class Formateur : Employe
    {
        private int HeureSupplementaires;
        private int RemunerationHeureSupplementaires;

        public int HeureSupplementaires1 { get => HeureSupplementaires; set => HeureSupplementaires = value; }

        public Formateur(string name, int anneeDeNaissance, int anneeEmbauche, int salaire, int heureSupplementaires, int remunerationHeureSupplementaires) : base(name, anneeDeNaissance, anneeEmbauche, salaire)
        {
            this.HeureSupplementaires = heureSupplementaires;
            this.RemunerationHeureSupplementaires = remunerationHeureSupplementaires * heureSupplementaires;
        }

        public Formateur()
        {
            //
        }

        public override void ToStrings()
        {
            base.ToStrings();
            Console.WriteLine($"Heure Supplementaires : {this.HeureSupplementaires} ");
            Console.WriteLine($"Remuneration Heure Supplementaires : {this.RemunerationHeureSupplementaires} ");
        }

        public int SalaireAPayerFormateur()
        {
            return (this.GetSalaireDebase() + this.HeureSupplementaires * this.RemunerationHeureSupplementaires) * (1 - this.GetIR());
        }
    }
}
