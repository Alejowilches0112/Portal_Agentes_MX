using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCalculoServicioCalculo
    {
        public string descuento { get; set; }
        public string tasa_anual { get; set; }
        public string cat { get; set; }
        public double qna_descuento { get; set; }
        public string fecha_1pago { get; set; }
        public string fecha_upago { get; set; }
        public string capacidad_pago { get; set; }
        public string monto_maxp { get; set; }
        public string estatus { get; set; }
        public string descripcionMovimiento { get; set; }
    }
}
