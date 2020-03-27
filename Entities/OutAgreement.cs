using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutAgreement
    {
        public List<Agreement> agreementsList { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Agreement
    {
        public double agreementCode { get; set; }
        public string agreementName { get; set; }
    }
}
