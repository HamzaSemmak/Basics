using sorec_gamma.modules.ModulePari;
using System;
using System.Collections.Concurrent;
using System.Linq;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class Cotes
    {
        public Cotes()
        {
            ReunionCotes = new ConcurrentBag<ReunionCote>();
        }
        public ConcurrentBag<ReunionCote> ReunionCotes { get; }

        public bool IsEmpty()
        {
            if (ReunionCotes == null || ReunionCotes.Count < 1)
                return true;
            ReunionCote rCote = ReunionCotes
                .Where(rc => rc.DateReunion.Day == DateTime.Today.Day && rc.GetFirstCourseCoteEnVente() != null)
                .FirstOrDefault();
            return rCote == null;
        }

        public void UpdateCotes(Offre offre)
        {
            foreach(ReunionCote reunionCote in ReunionCotes)
            {
                foreach(CourseCote courseCote in reunionCote.CourseCotes)
                {
                    int numR = int.Parse(reunionCote.NumReunion);
                    int numC = int.Parse(courseCote.NumCourse);
                    Course course = offre.GetCourseByNumReunionAndNumCourse(reunionCote.DateReunion, numR, numC);
                    if (course == null)
                    {
                        // ApplicationContext.Logger.Warn(string.Format("Update Cotes : Date réunion {0}, Num réunion {1}, Num course {2}", reunionCote.DateReunion, numR, numC));
                        courseCote.StatutCourse = StatutCourse.UNDIFINED;
                    }
                    else
                    {
                        courseCote.StatutCourse = course.Statut;
                        reunionCote.CodeHippo = course.CodeHippo;
                        courseCote.NumReunion = course.NumReunion.ToString();
                    }
                }
            }
        }

        public bool AddOrUpdateReunionCotes(ReunionCote reunionCote)
        {
            if (reunionCote == null || reunionCote.IsEmpty()) return false;
            ReunionCote tobeUpdatedReunionCote = ReunionCotes.Where(rc => { return rc.Equals(reunionCote); }).FirstOrDefault();
            if (tobeUpdatedReunionCote != null)
            {
                tobeUpdatedReunionCote.DateReunion = reunionCote.DateReunion;
                tobeUpdatedReunionCote.CodeHippo = reunionCote.CodeHippo;
                tobeUpdatedReunionCote.AddOrUpdateCourseCotes(reunionCote.CourseCotes);
            }
            else
            {
                ReunionCotes.Add(reunionCote);
            }
            return true;
        }

        public bool AddOrUpdateReunionCotes(ConcurrentBag<ReunionCote> reunionCotes)
        {
            if (reunionCotes == null || reunionCotes.Count < 1)
                return false;
            foreach (ReunionCote rc in reunionCotes)
            {
                AddOrUpdateReunionCotes(rc);
            }
            return true;
        }

        public void ClearAll()
        {
            ReunionCote rc;
            while (!ReunionCotes.IsEmpty)
            {
                ReunionCotes.TryTake(out rc);
            }
        }
    }
}
