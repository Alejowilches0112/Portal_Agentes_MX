using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanResume
    {

        public double loanNumber { get; set; }
        public string startDate{ get; set; }
        public double amount { get; set; }
        public double disbursement  { get; set; }
        public double executiveCode { get; set; }
        public double branchCode { get; set; }
        public string branchName { get; set; }
        public string documentID { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
