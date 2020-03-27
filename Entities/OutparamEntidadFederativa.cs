using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamEntidadfederativa
    {
        public double codigo_entidad { get; set; }
        public string codigo_pais { get; set; }
        public string nombre_entidad { get; set; }
        public string abreviatura { get; set; }
    }
    public class OutparamEntidadFederativa
    {
        public List<ParamEntidadfederativa> ListEntidades { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
