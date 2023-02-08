using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.UTILS
{
    public class DataTerminalIndicesHolder
    {
        public DateTime LastDateReunionSelected { get; set; }
        public int LastNumReunionSelectd { get; set; }
        public int LastCourseSelected { get; set; }
        public DateTime DateLastHashReunionSelected { get; set; }
        public TypeReload TypeReload { get; set; }

        public DataTerminalIndicesHolder()
        {
            LastDateReunionSelected = DateTime.Today;
            DateLastHashReunionSelected = DateTime.Today;
            TypeReload = TypeReload.INIT;
        }

        public DataTerminalIndicesHolder(DateTime dateReunion, DateTime dateIndexHashReunions)
        {
            LastDateReunionSelected = dateReunion;
            DateLastHashReunionSelected = dateIndexHashReunions;
            TypeReload = TypeReload.INIT;
        }

        public DataTerminalIndicesHolder(DateTime dateReunion, DateTime dateIndexHashReunions, int numReunion, int numCourse, TypeReload typeReload)
        {
            LastDateReunionSelected = dateReunion;
            DateLastHashReunionSelected = dateIndexHashReunions;
            LastNumReunionSelectd = numReunion;
            LastCourseSelected = numCourse;
            TypeReload=typeReload;
        }
    }
}
