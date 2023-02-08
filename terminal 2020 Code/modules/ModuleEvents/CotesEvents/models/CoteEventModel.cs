using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace sorec_gamma.modules.ModuleEvents.CotesEvents.models
{
    public class CoteEventModel
    {

		public DateTime Instant { get; set; }

		public string Version { get; set; }

		public DateTime DateReunion { get; set; }

		public string NumReunion { get; set; }

		public string NumCourse { get; set; }

		public string LibReunion { get; set; }

		public string CodeProd { get; set; }

		public DateTime DateReceptionCote { get; set; }

		public string Mises { get; set; } 

		public ConcurrentBag<CombinaisonEventModel> Combinaisons { get; set; }
	}
}
