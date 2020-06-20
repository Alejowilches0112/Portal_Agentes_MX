using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamMunicipio
    {
        public double codigo_municipio { get; set; }
        public string entidad_federativa { get; set; }
        public string pais { get; set; }
        public string nombre_municipio { get; set; }
        public double cod_entidad { get; set; }
        public double cod_pais { get; set; }

    }
    public class OutParamMunicipios
    {
        public List<ParamMunicipio> ListMunicipios { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
