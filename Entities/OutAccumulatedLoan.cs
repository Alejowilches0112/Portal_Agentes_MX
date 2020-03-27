using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutAccumulatedLoan
    {
        public List<AccumulatedLoan> lstAccumulatedLoan { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class AccumulatedLoan
    {
        public string executiveID { get; set; }
        public string month { get; set; }
        public double amount { get; set; }
        public double loanCount { get; set; }

    }
}
