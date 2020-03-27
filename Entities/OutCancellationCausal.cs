using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCancellationCausal
    {
        public List<CancellationCausal> lstCancellationCausal { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class CancellationCausal
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
