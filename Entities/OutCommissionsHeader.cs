using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCommissionsHeader
    {
        public List<CommissionHeader> lstCommissionHeader { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class CommissionHeader
    {
        public double accountCode { get; set; }
        public double branchCode { get; set; }
        public string branchName { get; set; }
        public double executiveType { get; set; }
        public string executiveTypeName { get; set; }
        public double positionCode { get; set; }
        public string positionCodeName { get; set; }
        public double categoryCode { get; set; }
        public string categoryName { get; set; }
        public double previousCategoryCode { get; set; }
        public string previousCategoryName { get; set; }
        public double previousPointsAccum { get; set; }
        public double previousCommissionBalance { get; set; }
        public double executiveCode { get; set; }
        public string executiveName { get; set; }
        public string notifyAddress{ get; set; }
        public string documentID { get; set; }
        public string executivePhone { get; set; }
        public double state { get; set; }
        public string stateDescription { get; set; }
        public string email { get; set; }
        public string notificationEmail { get; set; }
        public string bayportEmail { get; set; }
        public string birthday { get; set; }
        public double commercialBossCode { get; set; }
        public string commercialBossName { get; set; }
        public string commercialBossPhone { get; set; }
        public string commercialBossMail { get; set; }
        public double commissionBalance { get; set; }
        public string divisionName { get; set; }
        public string regionName { get; set; }
        public string rfc { get; set; }

    }
}
