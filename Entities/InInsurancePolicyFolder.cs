using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InInsurancePolicyFolder
    {
        public int Company { get; set; } = 1;
        public double Folder { get; set; }
        public double PolicyPlanCode { get; set; }
        public double PolicyAmountPlan { get; set; }
        public string User { get; set; }      
    }
}
