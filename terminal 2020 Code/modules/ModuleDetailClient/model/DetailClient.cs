using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModuleDetailClient.model
{
    class DetailClient
    {
        public List<DetailsClientOperation> Operations { set; get; }
        public DetailClient()
        {
            Operations = new List<DetailsClientOperation>();
        }
    }
}
