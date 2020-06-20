using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamGuias
    {
        public double? codigo_guia { get; set; }
        public string nombre { get; set; }
        public double sector { get; set; }
        public string nsector { get; set; }
        public string path { get; set; }
        public string fecha { get; set; }
        public string file {get;set;}
        public double ind_estado { get; set; }
    }
    public class OutParamGuias
    {
        public List<ParamGuias> ListDoc { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
