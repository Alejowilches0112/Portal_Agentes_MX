using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamSector
    {
        public double secuencia { get; set; }
        public string sector { get; set; } 
    }
    public class OutParamSector
    {
        public List<ParamSector> ListSector { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
