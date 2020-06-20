using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutServiceCodigoPostal
    {
        public string municipio { get; set; }
        public double estado { get; set; }
        public List<string> colonias { get; set; }
        public string estatus { get; set; }
        public string descripcionMovimiento { get; set; }
    }
}
