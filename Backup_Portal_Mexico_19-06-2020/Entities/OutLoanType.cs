using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanType
    {
        public List<LoanType> loanTypeList { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class LoanType
    {
        public string productName { get; set; }
        public double productCode { get; set; }       
    }
}
