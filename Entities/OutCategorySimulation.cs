using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCategorySimulation
    {
        public double categoryCode { get; set; }
        public string categoryName { get; set; }
        public string feeNew { get; set; }
        public string feeRenovated { get; set; }
        public Response msg { get; set; } = new Response();

    }
}
