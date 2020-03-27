using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamSucursales
    {
        public double secuencia { get; set; }
        public string sucursal { get; set; }
        public string codigo_sucursal { get; set; }
        public string division { get; set; }
        public string region { get; set; }
        public string tipo_sucursal { get; set; }
        public string emailCoordinador { get; set; }
        public string emailAuxiliar { get; set; }
    }
    public class OutParamSucursales
    {
        public List<ParamSucursales> ListSucursales { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
