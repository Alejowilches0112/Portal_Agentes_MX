using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamCampoParametrizado
    {
        public double codigo_campo { get; set; }
        public string solicitud { get; set; }
        public string dependencia { get; set; }
        public string producto { get; set; }
        public string periodo { get; set; }
        public string campo { get; set; }
        public string tipo_dato { get; set; }
        public string opciones { get; set; }
        public string obligatorio { get; set; }
    }
    public class OutParamCampos
    {
        public List<ParamCampoParametrizado> ListCampos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
