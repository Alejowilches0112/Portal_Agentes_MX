using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InBeneficiarioPoliza
    {
        public double tipoDoc { get; set; }
        public string rfc { get; set; }
        public string pNombre { get; set; }
        public string sNombre { get; set; }
        public string pApellido { get; set; }
        public string sApellido { get; set; }
        public string entidadF { get; set; }
        public string municipioF { get; set; }
        public string tefFijoBeneficiario { get; set; }
        public string celularBeneficiario { get; set; }
        public double vlrAseguradoBeneficiario { get; set; }
        public string fechaNacimiento { get; set; }
        public string gender { get; set; }
        public string parentezco { get; set; }
        public double tipo { get; set; }
        public string calleBeneficiario { get; set; }
        public double edad { get; set; }
    }
}
