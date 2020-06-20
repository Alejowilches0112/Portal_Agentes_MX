using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutRegional
    {
        public List<Regional> lstRegionals { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class Regional
    {
        public double Code { get; set; }
        public string Name { get; set; }
    }
}
