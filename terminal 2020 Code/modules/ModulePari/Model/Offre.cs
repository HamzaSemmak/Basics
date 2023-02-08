using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModulePari
{
    public class Offre
    {

        private int id_offre;
        private List<Reunion> listeReunion = new List<Reunion>();
       // private Dictionary<DateTime,List<Reunion>> listeReunion = new Dictionary<DateTime, List<Reunion>>();
        private DateTime date_offre;
        private Attributaire attributaire;
        private int numVersion;

        public Offre()
        {

        }

        public bool IsEmpty
        {
            get
            {
                return listeReunion == null || listeReunion.Count < 1
                    || listeReunion.Where(r => !r.IsEmpty).FirstOrDefault() == null;
            }
        }
        public Course GetCourseByNumReunionAndNumCourse(DateTime dateReunion, int numR, int numC)
        {
            Reunion reunion = listeReunion.Where(r => r.DateReunion.CompareTo(dateReunion) == 0 && r.NumReunion == numR).FirstOrDefault();
            Course course = null;
            if (reunion != null)
            {
                course = reunion.ListeCourse.Where(c => c.NumCoursePmu == numC).FirstOrDefault();
                if (course != null)
                {
                    course.CodeHippo = reunion.CodeHippo;
                }
            }
            return course;
        }

        public int NumVersion {
            get { return this.numVersion; }
            set { this.numVersion = value; }
        }
        public int IdOffre {
             get { return this.id_offre; }
             set { this.id_offre = value; }
        }
         
        public List<Reunion> ListeReunion {
             get { return this.listeReunion; }
            set { this.listeReunion = value; }
        }
        public DateTime Date_Offre
        {
             get { return this.date_offre; }
             set { this.date_offre = value; }
        }
        public Attributaire Attributaire
        {
            get { return this.attributaire; }
            set { this.attributaire = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Offre : " + id_offre);
            sb.AppendLine("Liste Réunions : " + (listeReunion != null ? listeReunion.Count : 0));
            sb.AppendLine("Date offre : " + date_offre);
            sb.AppendLine("Attributaire : " + (attributaire != null));
            sb.AppendLine("Num version : " + numVersion);
            return sb.ToString();
        }
    }
    
}
