using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutBranches
    {
        public List<Branch> lstBranches { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class Branch
    {
        public double Code { get; set; }
        public string Name { get; set; }
    }
}
