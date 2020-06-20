using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InSegurosServicioPoliza
    {
        public string rfc { get; set; }
        public string estatus { get; set; }
        public double creditoAsociado { get; set; }
        public string polizaPagada { get; set; }
        public string motivoCancelacion { get; set; }
    }
}
