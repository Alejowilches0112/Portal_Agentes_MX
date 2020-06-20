using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InOriginacionServicioSolicitud
    {
        //solicitud
        public string tipo { get; set; }
        public string dependenciaTrabajo { get; set; }
        public string producto { get; set; }
        public double montoSolicitado { get; set; }
        public double periodo { get; set; }
        public int plazo { get; set; }
        public string destinoCredito { get; set; }
        public string tipoNomina { get; set; }
        public int plazas { get; set; }
        public double descuento { get; set; }
        public double tasaAnual { get; set; }
        public double cat { get; set; }
        public string sucursal { get; set; }
        public string quincenaDescuento { get; set; }
        public string fechaPrimerPago { get; set; }
        public string fechaUltimoPago { get; set; }
        public double liquidoBase { get; set; }
        public double capacidadPago { get; set; }
        public double montoMaximo { get; set; }
        public string fechaSolicitud { get; set; }
        public string asesor { get; set; }
        public string folio { get; set; }
        public double montoDeudor { get; set; }
        public string matricula { get; set; }
        public double nss { get; set; }
        public double grupo { get; set; }
        public string claveTrabajador { get; set; }
        public string especificar { get; set; }
        public string reca { get; set; }
        //cliente
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string genero { get; set; }
        public string estadoCivil { get; set; }
        public string fechaNacimiento { get; set; }
        public string paisNacimiento { get; set; }
        public string entidadNacimiento { get; set; }
        public string nacionalidad { get; set; }
        public string paisResidenciaCliente { get; set; }
        public string formaMigratoria { get; set; }
        public string identificacionOficial { get; set; }
        public string otraIdentificacion { get; set; }
        //ocupacion
        public string sector { get; set; }
        public string puesto { get; set; }
        public string tiempoAntiguedad { get; set; }
        public string ingresoNetoMensual { get; set; }
        public string numeroEmpleado { get; set; }
        public string clavePresupuestal { get; set; }
        public string pagaduria { get; set; }
        public string fechaIngreso { get; set; }
        public string clave { get; set; }
        public string lugarTrabajo { get; set; }
        public string codigoPostalOcupacion { get; set; }
        public string entidadOcupacion { get; set; }
        public string municipioDelegacionOcupacion { get; set; }
        public string coloniaOcupacion { get; set; }
        public string otraOcupacion { get; set; }
        public string domicilioCalleOcupacion { get; set; }
        public string numeroExteriorOcupacion { get; set; }
        public string numeroInteriorOcupacion { get; set; }
        public string entreCallesOcupacion { get; set; }
        public string telefono { get; set; }
        public string extension { get; set; }
        public string tieneCargoPublicoP { get; set; }
        public string nombreCargoP { get; set; }
        public string puestoCargoP { get; set; }
        public string periodoEjercicioP { get; set; }
        public string tieneCargoPublicoF { get; set; }
        public string nombreCargoF { get; set; }
        public string puestoCargoF { get; set; }
        public string periodoEjercicioF { get; set; }
        public string beneficiario { get; set; }
        public string nombreBeneficiario { get; set; }
        public string categoriaTipoPension { get; set; }
        public string adscripcionUbicacionPago { get; set; }
        public string delegacion { get; set; }
        public string nombreT1 { get; set; }
        public string matriculaT1 { get; set; }
        public string gafeteT1 { get; set; }
        public string nombreT2 { get; set; }
        public string matriculaT2 { get; set; }
        public string gafeteT2 { get; set; }
        //Domicilio
        public string codigoPostalDomicilio { get; set; }
        public string entidadDomicilio { get; set; }
        public string municipioDelegacionDomicilio { get; set; }
        public string coloniaDomicilio { get; set; }
        public string otraDomicilio { get; set; }
        public string domicilioCalleDomicilio { get; set; }
        public string numeroExteriorDomicilio { get; set; }
        public string numeroInteriorDomicilio { get; set; }
        public string entreCallesDomicilio { get; set; }
        public string paisResidencia { get; set; }
        public string tiempoResidencia { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string proveedor { get; set; }
        public string propioRecados { get; set; }
        //Referencias Personales
        //Referencia1
        public string nombresRef1 { get; set; }
        public string primerApellidoRef1 { get; set; }
        public string segundoApellidoRef1 { get; set; }
        public double telefonoRef1 { get; set; }
        public double celularRef1 { get; set; }
        public string horarioContactoRef1 { get; set; }
        public string parentescoRef1 { get; set; }
        //Referencia2
        public string nombresRef2 { get; set; }
        public string primerApellidoRef2 { get; set; }
        public string segundoApellidoRef2 { get; set; }
        public double telefonoRef2 { get; set; }
        public double celularRef2 { get; set; }
        public string horarioContactoRef2 { get; set; }
        public string parentescoRef2 { get; set; }
        //Medios de Disposicion
        //Cuenta
        public string medio { get; set; }
        public double clabe { get; set; }
        public string institucionBancaria { get; set; }
        public double cuenta { get; set; }
        //Cuenta Alternativa
        public string medioA { get; set; }
        public double clabeA { get; set; }
        public string institucionBancariaA { get; set; }
        public double cuentaA { get; set; }
        //Compra Cartera
        public double depositoCliente { get; set; }
        public int diasPagar { get; set; }
        public string fechaContrato { get; set; }
        public string casaFinanciera { get; set; }
        public double montoCapital { get; set; }
        public double montoTotalPagar { get; set; }
        public double descuentoCC { get; set; }
        public int plazoCC { get; set; }
        public double montoSaldosInsolutos { get; set; }
        public double tasa { get; set; }
        public string fechaVencimientoCarta { get; set; }
        //Beneficiarios del seguro
        public string primerNombreSeguro { get; set; }
        public string segundoNombreSeguro { get; set; }
        public string primerApellidoSeguro { get; set; }
        public string segundoApellidoSeguro { get; set; }
        public string parentescoSeguro { get; set; }
        public double porcentaje { get; set; }
        public string curpSeguro { get; set; }
        //Seguro voluntario
        public string tieneSeguro { get; set; }
        public string tienePlan { get; set; }
        public double montoPrima { get; set; }
        public string estatus { get; set; }
    }
}
