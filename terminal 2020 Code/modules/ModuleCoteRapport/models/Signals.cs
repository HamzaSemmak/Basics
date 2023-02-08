using System;
using System.Collections.Concurrent;
using System.Linq;

/**
 * Created yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.ModuleCoteRapport.models
{
    [Serializable]
    public class Signals
    {
        public Signals()
        {
            CourseSignals = new ConcurrentBag<CourseSignal>();
        }
        public ConcurrentBag<CourseSignal> CourseSignals { get; set; }

        public bool IsEmpty()
        {
            return CourseSignals == null || CourseSignals.Count < 1;
        }


        public bool AddOrUpdateSignals(CourseSignal signal)
        {
            if (signal == null) return false;
            CourseSignal tobeUpdated = CourseSignals.Where(sig => { return sig.Equals(signal); }).FirstOrDefault();
            if (tobeUpdated != null)
            {
                tobeUpdated.DateReunion = signal.DateReunion;
                tobeUpdated.CodeHippo = signal.CodeHippo;
                tobeUpdated.NumCourse = signal.NumCourse;
                tobeUpdated.NumReunion = signal.NumReunion;
                tobeUpdated.Message = signal.Message;
            }
            else
            {
                CourseSignals.Add(signal);
            }
            return true;
        }

        public bool AddOrUpdateSignals(ConcurrentBag<CourseSignal> signals)
        {
            if (signals == null || signals.Count < 1)
                return false;
            foreach (CourseSignal signal in signals)
            {
                AddOrUpdateSignals(signal);
            }
            return true;
        }

        public void ClearAll()
        {
            CourseSignal signal;
            while (!CourseSignals.IsEmpty)
            {
                CourseSignals.TryTake(out signal);
            }
        }

        public CourseSignal GetCourseSignal()
        {
            CourseSignal signal;
            CourseSignals.TryTake(out signal);
            return signal;
        }
    }
}
