using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutJustification
    {
        public List<Justification> lstJustification { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Justification
    {
        public int justificationType { get; set; }
        public string justificationName { get; set; }        

    }
}
