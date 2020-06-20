using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Beneficiarios_Poliza
    {
        public double id_poliza { get; set; }
        public string nombre { get; set; }
        public string fecha_ncto { get; set; }
        public string parentesco { get; set; }
    }
    public class OutBeneficiariosPoliza
    {
        public List<Beneficiarios_Poliza> listBeneficiario { get; set; }
        public Response msg { get; set; }
    }
}
