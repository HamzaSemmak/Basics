using System;
using System.Collections.Generic;
using System.Text;
using  System.Linq;

namespace sorec_gamma.modules.ModulePari
{
    public class Course
    {
        private int numCoursePmu;
        private int numReunion;
        private String libCourse;
        private DateTime heureDepartCourse;
        private List<Horse> listeHorses = new List<Horse>();
        private List<Produit> listeProduits = new List<Produit>();
        private StatutCourse statut;
        private bool isRapportDisponible;
        private String resultat;

        public Course()
        {

        }
        public Course(int numR, int numC)
        {
            numReunion = numR;
            numCoursePmu = numC;
        }
        public Horse GetHorseByNum(int numPartant)
        {
            return listeHorses.Where(p => p.NumPartant == numPartant).FirstOrDefault();
        }
        public string CodeHippo
        {
            get;
            set;
        }        
        public int NumReunion
        {
            get { return this.numReunion; }
            set { this.numReunion = value; }
        }
        public int NumCoursePmu {
            get { return this.numCoursePmu; }
            set { this.numCoursePmu = value; }
        }
        public bool IsRapportDisponible
        {
            get { return this.isRapportDisponible; }
            set { this.isRapportDisponible = value; }
        }
        public String LibCourse
        {
            get { return this.libCourse; }
            set { this.libCourse = value; }
        }
        public StatutCourse Statut {
            get { return this.statut; }
            set { this.statut = value; }
        }
        public String Resultat {
            get { return this.resultat; }
            set { this.resultat = value; }
        }
        public DateTime  HeureDepartCourse
        {
            get { return this.heureDepartCourse; }
            set { this.heureDepartCourse = value; }

        }
        public List<Horse> ListeHorses
        {
            get { return this.listeHorses; }
            set { this.listeHorses = value; }
        }
        
        public List<Horse> ListePartant
        {
            get
            {
                return listeHorses
                          .Where(part => part.EstPartant == StatutPartant.EstPartant)
                          .ToList();
            }
        }
        public List<Horse> ListeNonPartant
        {
            get { return listeHorses
                            .Where(part => part.EstPartant == StatutPartant.NonPartant)
                            .ToList();
            }
        }

        public List<Produit> ListeProduit
        {
            get { return this.listeProduits; }
            set { this.listeProduits = value; }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Course: ");
            sb.AppendLine("Numéro: " + numCoursePmu);
            sb.AppendLine("Numéro Réunion: " + numReunion);
            sb.AppendLine("Libellé: " + libCourse);
            sb.AppendLine("Heure Départ: " + heureDepartCourse);
            sb.AppendLine("statut: " + Statut);
            sb.AppendLine("Rapport disponible: " + isRapportDisponible);

            sb.AppendLine("Nombre de chevaux: " + listeHorses.Count);
            sb.AppendLine("Nombre de non partants: " + ListeNonPartant.Count);
            sb.AppendLine("Nombre de produits: " + listeProduits.Count);

            sb.AppendLine("Resultat: " + resultat);

            return sb.ToString();
        }

        public bool isSame(Course c)
        {
            return c.NumCoursePmu == numCoursePmu;
        }

    }
}
