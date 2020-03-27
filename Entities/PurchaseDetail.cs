using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PurchaseDetail
    {
        public int monthlyPayment { get; set; } = 0;
        public int balance { get; set; } = 0;
        public string entity { get; set; } = "0|0";
        public int desprendible { get; set; } = 0;
    }
}
