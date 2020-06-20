using System.Collections.Generic;

namespace Entities
{
    public class OutPayable
    {
        public List<Payable> payableList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Payable
    {
        public double payableCode { get; set; }
        public double agreementCode { get; set; }
        public string payableName { get; set; }        
        public int sector { get; set; }
        public string sectorName { get; set; }
        public int payableGroup { get; set; }
        public string groupName { get; set; }
        public string segmentation { get; set; }
        public string applyPension { get; set; }
        public string payableType { get; set; }

    }
}
