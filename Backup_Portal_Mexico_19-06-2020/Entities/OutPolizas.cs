using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities
{
    public class OutPolizas
    {
        public string oferta { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class OutOfferPolizas
    {
        public List<InsurancePolicyOffer> ofertas { get; set; }
        public Response msg { get; set; } = new Response();
    }

}
