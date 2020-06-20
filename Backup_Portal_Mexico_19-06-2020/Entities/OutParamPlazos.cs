using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamPlazos
    {
        public double secuencia { get; set; }
        public string periodo { get; set; }
        public string plazos { get; set; } 
    }
    public class OutParamPlazos
    {
        public List<ParamPlazos> ListPlazos { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ParamPlazosPeriodos
    {
        public double secuencia { get; set; }
        public string plazos { get; set; }
    }
    public class OutParamPlazosPeriodos
    {
        public List<ParamPlazosPeriodos> ListPlazoPeriodo { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
