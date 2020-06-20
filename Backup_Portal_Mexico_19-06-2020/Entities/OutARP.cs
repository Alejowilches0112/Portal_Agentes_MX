using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutARP
    {
        public List<ARP> lstARP { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ARP
    {
        public string arpNIT { get; set; }
        public string arpName { get; set; }
        public string arpCodeMinistry { get; set; }
    }
}
