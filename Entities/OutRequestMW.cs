
namespace Entities
{
    public class OutRequestMW
    {
        public string codigo_validador { get; set; }
        public string detalle { get; set; }
        public string identificador { get; set; }
        public decimal tasa { get; set; }
        public decimal tasa_seguro { get; set; }
        public decimal tasa_admin_fee { get; set; }
        public decimal tasa_aval { get; set; }
        public double monto_maximo { get; set; }
        public double plazo_maximo { get; set; }
        public double cuota_sugerida { get; set; }
        public double cuota { get; set; }
        public double capacidad { get; set; }
        public string continua { get; set; }
        public string detalle_continua { get; set; }
        public string carpeta { get; set; }
        public string ofertaSeguro { get; set; }
        public string cedula { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
