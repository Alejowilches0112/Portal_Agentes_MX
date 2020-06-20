using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamConfiguracionDocumentos
    {
        public double codigo_config { get; set; }
        public double cod_documento { get; set; }
        public string nombre_documento { get; set; } 
        public string tipo_optencion { get; set; }
        public double posicion_x { get; set; }
        public double posicion_y { get; set; }
        public string valor { get; set; }
        public double pagina { get; set; }
        public double dependencia { get; set; }
        public double producto { get; set; }
        public string tvalidacion { get; set; }
        public string campoValidar { get; set; }
        public string valor_validacion { get; set; }
        public double fuente { get; set; }
        public double aumentoy { get; set; }
    }
    public class OutParamConfiguracionDoc
    {
        public List<ParamConfiguracionDocumentos> ListConfiguracion { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
