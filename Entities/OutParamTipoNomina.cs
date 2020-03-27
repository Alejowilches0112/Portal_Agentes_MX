using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamTipoNomina
    {
        public double secuencia { get; set; }
        public string tipoNomina { get; set; } 
    }
    public class OutParamTipoNomina
    {
        public List<ParamTipoNomina> ListTipoNomina { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
