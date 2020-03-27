using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ClaveDelg
    {
        public string clave { get; set; }
        public string delegacion { get; set; }
    }
    public class OutParamClaveDelg
    {
        public List<ClaveDelg> ListClavesDelg { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
