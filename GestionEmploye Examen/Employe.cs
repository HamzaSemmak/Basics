using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmploye_Examen
{
    internal class Employe 
    {
        static int newMatricule = 1;
        static int CurrentYear = DateTime.Now.Year;
        private string name;
        private int matricule;
        private int anneDeNaissance;
        private int anneEmbauche;
        private int salaireDeBase;

        public string Name { get => name; set => name = value; }
        public int Matricule { get => matricule; set => matricule = value; }
        public int AnneDeNaissance { get => anneDeNaissance; set => anneDeNaissance = value; }
        public int AnneEmbauche { get => anneEmbauche; set => anneEmbauche = value; }
        public int SalaireDeBase { get => salaireDeBase; set => salaireDeBase = value; }

        public Employe()
        {
            //Constructeur d'initialisation.
        }

        public Employe(string name, int anneeDeNaissance, int anneeEmbauche, int salaire)
        {
            this.Matricule = newMatricule++;
            this.Name = name;
            this.AnneDeNaissance = anneeDeNaissance;
            this.AnneEmbauche = anneeEmbauche;
            this.SalaireDeBase = salaire;
        }

        public int GetIR()
        {
            int TauxIR = 0;
            if (this.SalaireDeBase >= 0 && this.SalaireDeBase <= 28000) { TauxIR = 0; }
            else if (this.SalaireDeBase >= 28001 && this.SalaireDeBase <= 40000) { TauxIR = 12; }
            else if (this.SalaireDeBase >= 40001 && this.SalaireDeBase <= 50000) { TauxIR = 24; }
            else if (this.SalaireDeBase >= 50001 && this.SalaireDeBase <= 60000) { TauxIR = 34; }
            else if (this.SalaireDeBase >= 60001 && this.SalaireDeBase <= 150000) { TauxIR = 38; }
            else if (this.SalaireDeBase >= 150000) { TauxIR = 40; }

            return TauxIR;
        }

        public virtual int SalaireAPayer()
        {
            return this.SalaireDeBase *  1 - GetIR();
        }

        public static void CompareEmployeSalaire(Employe a, Employe b)
        {
            if (a.salaireDeBase > b.salaireDeBase)
            {
                Console.WriteLine($"The salary of {a.name} is bigger then {b.name} ");
            }
            else if (a.salaireDeBase < b.salaireDeBase)
            {
                Console.WriteLine($"The salary of {b.name} is bigger then {a.name} ");
            }
            else if (a.salaireDeBase == b.salaireDeBase)
            {
                Console.WriteLine("There are Equals");
            }
        }

        public int GetSalaireDebase()
        {
            return this.salaireDeBase;
        }

        public virtual void ToStrings()
        {
            Console.WriteLine($"Name : {this.name} ");
            Console.WriteLine($"Matricule : {this.matricule} ");
            Console.WriteLine($"Anneé de aissance : {this.anneDeNaissance} ");
            Console.WriteLine($"Anneé d'embauche : {this.anneEmbauche} ");
            Console.WriteLine($"Salaire : {this.SalaireDeBase} ");
        }

        public int Age()
        {
            return CurrentYear - this.anneDeNaissance;
        }

        public int Anciennete()
        {
            return CurrentYear - this.anneEmbauche;
        }

        public int DateRetraite(int ageRetraite)
        {
            int anneDeTravailleRestant = ageRetraite - this.Age();
            return anneDeTravailleRestant + CurrentYear;
        }
    }
}
