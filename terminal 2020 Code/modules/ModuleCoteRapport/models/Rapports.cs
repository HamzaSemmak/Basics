using System;
using System.Collections.Concurrent;
using System.Linq;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class Rapports
    {
        public Rapports()
        {
            CourseRapports = new ConcurrentBag<CourseRapport>();
        }
        public ConcurrentBag<CourseRapport> CourseRapports { get; set; }

        public bool IsEmpty()
        {
            if (CourseRapports == null || CourseRapports.IsEmpty)
                return true;
            return false;
        }

        public CourseRapport GetLastRapport()
        {
            if (IsEmpty())
                return null;
            DateTime maxDateTime = CourseRapports.Max(cr => { return cr.Timestamp; });
            return CourseRapports.Where(cr => { return cr.Timestamp == maxDateTime;}).FirstOrDefault();
        }

        public bool IsContainsNotDisplayedRapports()
        {
            if (IsEmpty())
                return false;
            return CourseRapports.Where(cr => { return !cr.Displayed; }).FirstOrDefault() != null;
        }

        public bool AddOrUpdateCourseRapports(CourseRapport courseRapport)
        {
            if (courseRapport == null) return false;
            CourseRapport tobeUpdated = CourseRapports.Where(cr => { return cr.Equals(courseRapport); }).FirstOrDefault();
            if (tobeUpdated != null)
            {
                tobeUpdated.Displayed = courseRapport.Displayed;
                tobeUpdated.Date = courseRapport.Date;
                tobeUpdated.Course = courseRapport.Course;
                tobeUpdated.Reunion = courseRapport.Reunion;
                tobeUpdated.Produits = courseRapport.Produits;
                // tobeUpdated.AddOrUpdateProduitRapport(courseRapport.Produits);
            }
            else
            {
                CourseRapports.Add(courseRapport);
            }
            return true;
        }

        public bool AddOrUpdateCourseRapports(ConcurrentBag<CourseRapport> courseRapports)
        {
            if (courseRapports == null || courseRapports.IsEmpty)
                return false;
            foreach (CourseRapport cr in courseRapports)
            {
                _ = AddOrUpdateCourseRapports(cr);
            }
            return true;
        }

        public void ClearAll()
        {
            if (CourseRapports == null || CourseRapports.IsEmpty)
                return;
            while (!CourseRapports.IsEmpty)
            {
                _ = CourseRapports.TryTake(out _);
            }
        }
    }
}
