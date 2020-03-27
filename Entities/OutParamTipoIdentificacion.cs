using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamTipoidentificacion
    {
        public double secuencia { get; set; }
        public string tipoIdentificacion { get; set; } 
    }
    public class OutParamTipoIdentificacion
    {
        public List<ParamTipoidentificacion> ListTipoIdentificacion { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
