using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamDestinoCred
    {
        public double secuencia { get; set; }
        public string destinoCredito { get; set; } 
    }
    public class OutParamDestinoCred
    {
        public List<ParamDestinoCred> ListDestinoCred { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
