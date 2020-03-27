using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamTipoSolicitud
    {
        public double? secuencia { get; set; }
        public string tipoSolicitud { get; set; } 
    }
    public class OutParamTipo
    {
        public List<ParamTipoSolicitud> ListTipoSolicitud { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
