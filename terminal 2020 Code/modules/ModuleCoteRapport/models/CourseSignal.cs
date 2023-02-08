using System;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class CourseSignal : IComparable<CourseSignal>
    {
        public string NumReunion { get; set; }
        public string NumCourse { get; set; }
        public string CodeHippo { get; set; }
        public DateTime DateReunion { get; set; }
        public string Message { get; set; }

        public int CompareTo(CourseSignal courseSignal)
        {
            if (courseSignal == null) return -1;
            else if (courseSignal.NumReunion != NumReunion)
                return string.Compare(NumReunion, courseSignal.NumReunion, StringComparison.InvariantCulture);
            else if (courseSignal.NumCourse != NumCourse)
                return string.Compare(NumCourse, courseSignal.NumCourse, StringComparison.InvariantCulture);
            else return DateReunion.CompareTo(courseSignal.DateReunion);
        }

        public override bool Equals(object obj)
        {
            CourseSignal obj1 = obj as CourseSignal;
            return obj1 != null
                && NumReunion == obj1.NumReunion
                && NumCourse == obj1.NumCourse
                && CodeHippo == obj1.CodeHippo
                && DateReunion.CompareTo(obj1.DateReunion) == 0;
        }
        public override string ToString()
        {
            return string.Format("[CourseSignal] NumReunion: {0}, NumCourse: {1}, DateReunion: {2}, CodeHippo: {3}, Message: {4}",
                NumReunion, NumCourse, DateReunion, CodeHippo, Message);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = NumCourse == null ? 0 : (hash * 7) + NumCourse.GetHashCode();
            hash = NumReunion == null ? 0 : (hash * 7) + NumReunion.GetHashCode();
            hash = CodeHippo == null ? 0 : (hash * 7) + CodeHippo.GetHashCode();
            hash = (hash * 7) + DateReunion.GetHashCode();
            hash = Message == null ? 0 : (hash * 7) + Message.GetHashCode();
            return hash;
        }
    }
}
