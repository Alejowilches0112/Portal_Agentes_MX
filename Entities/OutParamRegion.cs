using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamRegion
    {
        public string codigo_region { get; set; }
        public string nombre_region { get; set; } 
    }
    public class OutParamRegion
    {
        public List<ParamRegion> regionList { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
