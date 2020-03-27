using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamPais
    {
        public double codigo_pais { get; set; }
        public string codigo { get; set; }
        public string nombre_pais { get; set; }
    }
    public class OutParamPais
    {
        public List<ParamPais> ListPais { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
