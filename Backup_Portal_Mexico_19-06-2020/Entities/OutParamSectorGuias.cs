using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamSectorGuias
    {
        public double secuencia { get; set; }
        public string sector { get; set; }
        public double ind_estado { get; set; }
    }
    public class OutParamSectorGuias
    {
        public List<ParamSectorGuias> ListSectorGuias { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
