using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamQuincenaDscto
    {
        public double secuencia { get; set; }
        public string QuincenaDscto { get; set; } 
    }
    public class OutParamQuincenaDscto
    {
        public List<ParamQuincenaDscto> ListQuincenaDscto { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
