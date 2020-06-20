using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamMedios
    {
        public double secuencia { get; set; }
        public string medioDisposicion { get; set; } 
    }
    public class OutParamMedios
    {
        public List<ParamMedios> ListMedios { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
