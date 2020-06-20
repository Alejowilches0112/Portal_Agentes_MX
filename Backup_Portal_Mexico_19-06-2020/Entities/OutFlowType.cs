using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutFlowType
    {
        public List<FlowType> lstFlowType { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class FlowType
    {
        public int flowType { get; set; }
        public string flowName { get; set; }
    }
}
