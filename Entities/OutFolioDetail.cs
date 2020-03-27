using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutFolioDetail
    {
        public List<FolioDetail> loanFolioDetail { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class FolioDetail
    {
        public string folioNumber { get; set; }
        public string branch { get; set; }
        public string region { get; set; }
        public string division { get; set; }
        public string loanExecutive { get; set; }
        public double amount { get; set; }
        public double term { get; set; }
        public double disbursementAmount { get; set; }
        public string product { get; set; }
        public string requestDate { get; set; }
        public string captureDate { get; set; }
        public string disbursementDate { get; set; }
        public string loanStatus { get; set; }
        public double monthlyAmount { get; set; }
    }
}