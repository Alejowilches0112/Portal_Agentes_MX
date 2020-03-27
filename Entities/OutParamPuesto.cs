using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamPuesto
    {
        public double secuencia { get; set; }
        public string sector { get; set; }
        public string puestoSector { get; set; } 
    }
    public class OutParamPuesto
    {
        public List<ParamPuesto> ListPuestos { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ParamPuestoSector
    {
        public double secuencia { get; set; }
        public string puestoSector { get; set; }
    }
    public class OutParamPuestoSector
    {
        public List<ParamPuestoSector> ListPuestoSector { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
