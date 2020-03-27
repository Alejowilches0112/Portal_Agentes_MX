using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutHeaderPQR
    {
        public List<HeaderPQR> lstHeaderPQR { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class HeaderPQR
    {
        public int processNumber { get; set; }
        public string processName { get; set; }
        public int flowType { get; set; }
        public string flowName { get; set; }
        public int state { get; set; }
        public int processKey { get; set; }
        public double nui { get; set; }
        public int folderNumber { get; set; }
        public int loanNumber { get; set; }
        public string dateInsertBusiness { get; set; }
        public string userInsert { get; set; }
        public string dateInsertSYS { get; set; }
        public int keyType { get; set; }
        public string customerName { get; set; }
        public string stateName { get; set; }
        public int priorityManual { get; set; }
        public string priorityName { get; set; }
        public string documentID { get; set; }
        public string AssignedAnalyst { get; set; }
        public string closeDate { get; set; }        
        public int justificationCode { get; set; }
        public string reason { get; set; }
        public string executiveName { get; set; }
    }
}
