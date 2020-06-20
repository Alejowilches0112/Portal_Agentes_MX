using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamDependecia
    {
        public double secuencia { get; set; }
        public string dependencia { get; set; } 
        public string estado { get; set; }
    }
    public class OutParamDependencia
    {
        public List<ParamDependecia> ListDependencias { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
