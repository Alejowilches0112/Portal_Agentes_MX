using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamDivision
    {
        public string codigo_division { get; set; }
        public string nombre_division { get; set; } 
    }
    public class OutParamDivision
    {
        public List<ParamDivision> divisionList { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
