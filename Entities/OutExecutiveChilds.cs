using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutExecutiveChilds
    {
        public List<ExecutiveChilds> lstExecutiveChilds { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ExecutiveChilds
    {
        public double Code { get; set; }
        public string Name { get; set; }
        public string DocumentID { get; set; }
    }
}
