using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanLine
    {
        public List<LoanLine> loanLineList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class LoanLine
    {
        public string lineDescription { get; set; }
        public double loanLine { get; set; }
        public double freeInvestment { get; set; } 
        public double purchasePortfolio { get; set; }
    }

}
