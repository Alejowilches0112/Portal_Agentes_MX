using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InInsurancePolicyOffer
    {
        public int Company { get; set; } = 1;
        public double Folder { get; set; }
        public double RequestAmount { get; set; }
        public double MaximumAmount { get; set; }
        public string User { get; set; }
    }
}
