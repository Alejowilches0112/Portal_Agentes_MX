using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutParamParentesco
    {
        public List<ParamParentesco> listParentesco { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class ParamParentesco
    {
        public double codigo { get; set; }
        public string nombre_parentesco { get; set; }
    }
}
