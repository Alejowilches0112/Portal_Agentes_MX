using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanHeader
    {
        public double folderNumber { get; set; }
        public string numberID { get; set; }
        public string personName { get; set; }
        public DateTime creationDate { get; set; }
        public double folderStateCode { get; set; }
        public string folserStateName{ get; set; }
        public decimal amountRequested { get; set; }
        public decimal termRequested { get; set; }
        public decimal rateRequested { get; set; }
        public decimal amountApproved { get; set; }
        public double termApproved { get; set; }
        public decimal rateApproved { get; set; }
        public decimal monthlyPaymentApproved { get; set; }
        public double agreementCode { get; set; }
        public string agreementName { get; set; }
        public double payableCode { get; set; }
        public string payableName { get; set; }
        public double branchCode { get; set; }
        public double branchHomologous { get; set; }
        public Response msg { get; set; } = new Response();

    }
}
