using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamEstadoCivil
    {
        public double secuencia { get; set; }
        public string estadoCivil { get; set; } 
    }
    public class OutParamEstadoCivil
    {
        public List<ParamEstadoCivil> ListEstados { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
