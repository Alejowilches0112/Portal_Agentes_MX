using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutPurchaseEntity
    {
        public List<PurchaseEntity> purchaseEntityList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class PurchaseEntity
    {
        public double entityCode { get; set; }
        public string entityName { get; set; }
    }

}
