using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CasaFinanciera
    {
        public string rfc_casa { get; set; }
        public string casa_financera { get; set; } 
        public string estado { get; set; }
    }
    public class OutParamCasaFinanciera
    {
        public List<CasaFinanciera> ListCasas { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
