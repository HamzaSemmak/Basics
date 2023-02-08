using System;
using System.Collections.Generic;

using System.Text;


namespace sorec_gamma.modules.ModulePari
{
    public class Reunion
    {
        private int numReunion;
        private String codeHippo;
        private String libReunion;
        private DateTime dateReunion;
        private String diffusion;
        private List<Course> listeCourse = new List<Course>();
        private DateTime heureDemarrage;

        public Reunion()
        {

        }
         public Reunion(DateTime dateR, int numR)
        {
            dateReunion = dateR;
            numReunion = numR;
        }

        public bool IsEmpty
        {
            get
            {
                return listeCourse == null || listeCourse.Count < 1;
            }
        }
        public String CodeHippo
        {
            get
            {
                return this.codeHippo;
            }
            set
            {
                this.codeHippo = value;
            }
        }
         public DateTime HeureDemmarage
        {
            get
            {
                return this.heureDemarrage;
            }
            set
            {
                this.heureDemarrage = value;
            }
        }

        public String LibReunion  
        {
            get
            {
                return this.libReunion;
            }
            set
            {
                this.libReunion = value;
            }
        }

        public DateTime DateReunion
        {
            get
            {
                return this.dateReunion;
            }
            set
            {
                this.dateReunion = value;
            }
        }

        public int NumReunion
        {
            get
            {
                return this.numReunion;
            }
            set
            {
                this.numReunion = value;
            }
        }

        public String Diffusion
        {
            get
            {
                return this.diffusion;
            }
            set
            {
                this.diffusion = value;
            }
        }

        public List<Course> ListeCourse
        {
            get
            {
                return this.listeCourse;
            }
            set
            {
                this.listeCourse = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Réunion: ");
            sb.AppendLine("Numéro: " + numReunion);
            sb.AppendLine("Heure Démarrage: " + heureDemarrage);
            sb.AppendLine("Diffusion" + diffusion);
            sb.AppendLine("Date: " + dateReunion);
            sb.AppendLine("Libellé: " + libReunion);
            sb.AppendLine("Code Hippo: " + codeHippo);
            sb.AppendLine("Nombre de course: " + ListeCourse.Count);
            return sb.ToString();
        }

        public bool isSame(Reunion r)
        {
            return this.dateReunion.Equals(r.DateReunion) && numReunion == r.NumReunion;
        }
    }
}