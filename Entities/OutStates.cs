using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutStates
    {
        public List<State> lstStates { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class State
    {
        public int flowType { get; set; }
        public string flowName { get; set; }
        public int codeState { get; set; }
        public string stateName { get; set; }
    }
}
