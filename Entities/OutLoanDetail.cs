using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanDetail
    {
        public List<LoanDetail> loanDetailList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class LoanDetail
    {
        public double folderNumber { get; set; }
        public double processType { get; set; }
        public string processTypeDescription { get; set; }
        public string processDate { get; set; }
        public string processUser { get; set; }
        public double previousStateCode { get; set; }
        public string previousStateName { get; set; }
        public double NewStateCode { get; set; }
        public string NewStateName { get; set; }
        public string observations { get; set; }
        public double LogType { get; set; }

    }
}
