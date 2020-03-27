using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamIngresos
    {
        public double secuencia { get; set; }
        public string ingresoNeto { get; set; } 
    }
    public class OutParamIngresos
    {
        public List<ParamIngresos> ListIngresos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
