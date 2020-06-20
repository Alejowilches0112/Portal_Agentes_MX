using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Entities
{
    [XmlRoot("notificacion")]
    public class OutOfferList
    {
        [XmlElement("notificacion")]
        public OutOffer Steps { get; set; }
        public Response msg { get; set; } = new Response();
    }
    
    public class OutOffer
    {
        public OutOffer()
        {
            defaultPolicy = new InsurancePolicyOffer();
        }

        //[XmlElement("codigo_validador")]
        public string validatorCode { get; set; }
        //[XmlElement("detalle")]
        public string detail { get; set; }
        //[XmlElement("identificador")]
        public string identifier { get; set; }
        //[XmlElement("tasa")]
        public decimal rate { get; set; }
        //[XmlElement("tasa_seguro")]
        public decimal insuranceRate { get; set; }
        //[XmlElement("tasa_admin_fee")]
        public decimal rate_admin_fee { get; set; }
        //[XmlElement("tasa_aval")]
        public decimal rateGuarantee { get; set; }
        //[XmlElement("monto_maximo")]
        public double maximumAmount { get; set; }
        //[XmlElement("plazo_maximo")]
        public double maximunTerm { get; set; }
        //[XmlElement("cuota_sugerida")]
        public double suggestedMonthlyPayment { get; set; }
        //[XmlElement("cuota")]
        public double monthlyPayment { get; set; }
        //[XmlElement("capacidad")]
        public double capacity { get; set; }
        //[XmlElement("continua")]
        public string pass { get; set; }
        //[XmlElement("detalle_continua")]
        public string passDetail { get; set; }
        //[XmlElement("carpeta")]
        public string folder { get; set; }

        public string observaciones { get; set; }

        public double amountClient { get; set; }
       
        public double termClient { get; set; }
        public double isurancePlanCode { get; set; }
        public double isurancePlanValue { get; set; }
        public bool applyInsurance { get; set; }

        public OutInsurancePolicyOffer insurancePolicyOffer { get; set; }
        public InsurancePolicyOffer defaultPolicy { get; set; }
        public Response msg { get; set; } = new Response();
    }


}
