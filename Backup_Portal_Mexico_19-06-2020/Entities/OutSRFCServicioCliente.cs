using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutSRFCServicioCliente
    {
        public List<SRFCServicioCliente> ListRfcs { get; set; }
        public string estatus { get; set; }
        public string descripcionMovimiento { get; set; }
    }
    public class SRFCServicioCliente
    {
        public string numero { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string genero { get; set; }
        public string email { get; set; }
        public double celular { get; set; }
        public string fecha_nacimiento { get; set; }
        public string domicilio_Calle { get; set; }
        public string entre_calles_domicilio { get; set; }
        public double codigo_postal { get; set; }
        public double tiempo_residencia { get; set; }
        public string nacionalidad { get; set; }
        public string pais { get; set; }
        public string muncipio_domicilio { get; set; }
        public string colonia_Domicilio { get; set; }
        public string estatus { get; set; }
        public string descripcionMovimiento { get; set; }
    }
}
