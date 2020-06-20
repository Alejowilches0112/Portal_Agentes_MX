using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutProductivity
    {
        public List<Productivity> lstProductivity { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class Productivity
    {
        public string Figure { get; set; }
        public double LoanCount { get; set; }
        public double LoanAmount { get; set; }
        public double Average { get; set; }
    }
}
