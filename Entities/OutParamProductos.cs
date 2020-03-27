using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamProducto
    {
        public double? secuencia { get; set; }
        public string dependencia { get; set; }
        public string producto { get; set; } 
        public string estado { get; set; }
    }
    public class OutParamProductos
    {
        public List<ParamProducto> ListProductos { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ParamProductoDependencia
    {
        public double secuencia { get; set; }
        public string producto { get; set; }
    }
    public class OutParamProductoDependencia
    {
        public List<ParamProductoDependencia> ListProductosDependencia { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
