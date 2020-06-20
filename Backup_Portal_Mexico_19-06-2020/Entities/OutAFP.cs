using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutAFP
    {
        public List<AFP> lstAFP { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class AFP
    {
        public string afpNIT { get; set; }
        public string afpName { get; set; }
        public string afpCodeMinistry { get; set; }
    }
}
