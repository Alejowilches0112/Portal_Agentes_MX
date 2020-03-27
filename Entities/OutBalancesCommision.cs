using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutBalancesCommision
    {
        public List<BalancesCommision> lstBalancesCommision { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class BalancesCommision
    { 
        public double conceptCode { get; set; }
        public string conceptName { get; set; }
        public double conceptBalance { get; set; }
        public double accountCode { get; set; }
        public double executiveCode { get; set; }
        public string documentID { get; set; }
        public string executiveName { get; set; }

    }
}
