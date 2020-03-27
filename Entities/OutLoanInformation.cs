using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLoanInformation
    {
        public List<LoanInformation> loanInformationList { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class LoanInformation
    {
        public double folderNumber { get; set; }
        public string numberID { get; set; }
        public string personName { get; set; }
        public double folderStateCode { get; set; }
        public string folderStateName { get; set; }
        public double loanLine { get; set; }
        public double requisitionAmount { get; set; }
        public double amountApproved { get; set; }
        public int termApproved { get; set; }
        public decimal rateApproved { get; set; }
        public double loanOfficerCode { get; set; }
        public string creationDate { get; set; }
        public double zoneCode { get; set; }
        public double branchCode { get; set; }
        public string branchName { get; set; }
        public double agreementCode { get; set; }
        public string agreementName { get; set; }
        public double payamentCode { get; set; }
        public string payamentName { get; set; }
        public string analystsApproved { get; set; }
        public double noveltyCode { get; set; }
        public string noveltyName { get; set; }
        public double commercialBossCode { get; set; }
        public string commercialBossName { get; set; }
        public string executiveID { get; set; }
        public string executiveName { get; set; }

    }
}
