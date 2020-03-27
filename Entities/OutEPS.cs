using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutEPS
    {
        public List<EPS> lstEPS { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class EPS
    {
        public string epsNIT { get; set; }
        public string epsName { get; set; }
        public string epsCodeMinistry { get; set; }        
    }
}
