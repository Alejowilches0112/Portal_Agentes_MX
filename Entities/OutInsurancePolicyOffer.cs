using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutInsurancePolicyOffer
    {
        public List<InsurancePolicyOffer> lstInsurancePolicy { get; set; }
        public List<InsurancePolicyOfferRel> lstInsurancePolicyRel { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class InsurancePolicyOffer
    {
        public double PlanCode { get; set; }
        public string PlanName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double PolicyValue { get; set; }
        public int Principal { get; set; }
        public int AllowRelatives { get; set; }
        public int YearsValidity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double CovertureValue { get; set; }

        [DisplayFormat(DataFormatString = "{0:N}")]
        public double MonthlyValue { get; set; }
    }
    public class InsurancePolicyOfferRel
    {
        public double PlanCode { get; set; }
        public string RelationshipName { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public double InsuredValue { get; set; }
    }
}
