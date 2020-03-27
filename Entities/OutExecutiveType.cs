using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutExecutiveType
    {
        public List<ExecutiveType> lstExecutiveType { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ExecutiveType
    {
        public double Code { get; set; }
        public string Name { get; set; }
    }
}
