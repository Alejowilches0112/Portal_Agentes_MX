using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutMovesCommission
    {
        public List<MovementCommission> lstMovesCommission { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class MovementCommission
    {
        public double conceptCode { get; set; }
        public string conceptName { get; set; }
        public double ammountDB { get; set; }
        public double ammountCR { get; set; }
        public DateTime paymentDate { get; set; }
        public double creditNumber { get; set; }
        public DateTime disbursementDate { get; set; }
        public string newRenovated { get; set; }
        public double disbursementAmount { get; set; }
        public double investmentAmount { get; set; }
        public double commissionPercentage { get; set; }
        public double commissionAmount { get; set; }
        public string schemePayment { get; set; }
        public string observations { get; set; }
        public string nature { get; set; }
        public double bonnusPercentage { get; set; }
        public double bonnusAmount { get; set; }
        public double paymentTotal { get; set; }

    }
}
