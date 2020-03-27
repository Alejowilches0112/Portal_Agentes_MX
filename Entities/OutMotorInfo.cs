using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutMotorInfo
    {
        public List<MotorInfo> MotorList { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class MotorInfo
    {
        public double numero_carpeta { get; set; }
        public double secuencia { get; set; }
        public double codigo_politica { get; set; }
        public string descripcion_politica { get; set; }
        public string descricion_error { get; set; }
        public string fecha_registro { get; set; }
    }
}
