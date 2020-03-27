using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamReca
    {
        public double secuencia { get; set; }
        public string reca { get; set; } 
    }
    public class OutParamReca
    {
        public List<ParamReca> ListRecas { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
