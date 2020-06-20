using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InAcceptOffer
    {
        public int folder { get; set; }
        public string rate { get; set; }
        public int term { get; set; }
        public int amount { get; set; }
        public int amountClient { get; set; }
        public int termClient { get; set; }
        public int policyValue { get; set; }
        public int monthlyPayment { get; set; }       

    }
}
