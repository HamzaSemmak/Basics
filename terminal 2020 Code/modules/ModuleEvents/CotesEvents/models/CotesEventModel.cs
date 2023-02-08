using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleEvents.CotesEvents.models
{
    public class CotesEventModel
    {
        public DateTime Instant { get; set; }

        public string ReferenceCalc { get; set; }

        public ConcurrentBag<CoteEventModel> Cotes { get; set; }

        //public int CurrentCoteVersion { get; set; }
    }
}
