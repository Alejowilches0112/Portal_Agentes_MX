using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamPeriodos
    {
        public double? secuencia { get; set; }
        public string periodo { get; set; } 
    }
    public class OutParamPeriodos
    {
        public List<ParamPeriodos> ListPeriodos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
