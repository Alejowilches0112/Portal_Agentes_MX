using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamDiasExp
    {
        public double dias { get; set; }
    }
    public class OutParamDiasExp
    {
        public List<ParamDiasExp> ListDias { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
