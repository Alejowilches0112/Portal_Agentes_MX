using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamTipoSucursal
    {
        public double codigo_tipo { get; set; }
        public string nombre_tipo { get; set; } 
    }
    public class OutParamTipoSucursal
    {
        public List<ParamTipoSucursal> TipoSucursalList { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
