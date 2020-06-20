using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamBanco
    {
        public string codigo_banco { get; set; }
        public string nombre_banco { get; set; }
    }
    public class OutParamBanco
    {
        public List<ParamBanco> ListBancos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
