using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FormularioSolicitud
    {
        //Solicitud
        public double? id_poliza { get; set; }
        public string folderNumber { get; set; }
        public string estado { get; set; }
        public string subestado { get; set; }
        public string fchsolicitud { get; set; }
        public double tipoSolicitud { get; set; }
        public double monto { get; set; }
        public double periodo { get; set; }
        public double plazo { get; set; }
        public double LBase { get; set; }
        public double nPlazas { get; set; }
        public double Dependencia { get; set; }
        public double producto { get; set; }
        public double destino { get; set; }
        public double tNomina { get; set; }
        public double dscto { get; set; }
        public double tAnual { get; set; }
        public double sucursal { get; set; }
        public double cat { get; set; }
        public double quincenaDscto { get; set; }
        public string fchPrPago { get; set; }
        public string fchUltPago { get; set; }
        public double cPago { get; set; }
        public double mMaxPlaz { get; set; }
        public string especificar { get; set; }
        public string grupo { get; set; }
        public string matricula { get; set; }
        public double? monto_deudor { get; set; }
        public string clave_trabajdor { get; set; }
        public string nss { get; set; }
        public double? reca { get; set; }
        //Datos
        public string RFC { get; set; }
        public string pNombre { get; set; }
        public string sNombre { get; set; }
        public string pApellido { get; set; }
        public string sApellido { get; set; }
        public double? tipoDoc { get; set; }
        public string CURP { get; set; }
        public string fecNac { get; set; }
        public double? paisN { get; set; }
        public double? entidadN { get; set; }
        public double? paisR { get; set; }
        public string fMigratoria { get; set; }
        public string gender { get; set; }
        public string otraIdentificacion { get; set; }
        public double estadoCivil { get; set; }
        public string nacionalidad { get; set; }
        //ocupacion
        public double? sector { get; set; }
        public string otroSector { get; set; }
        public double? puesto { get; set; }
        public double? antiguedad { get; set; }
        public double? ingresos { get; set; }
        public string Celular { get; set; }
        public string cPresupuestal { get; set; }
        public string Pagaduria { get; set; }
        public string fchIngreso { get; set; }
        public string clave { get; set; }
        public string lugTrabajo { get; set; }
        public string calle { get; set; }
        public string nExterior { get; set; }
        public string colonia { get; set; }
        public string otraColonia { get; set; }
        public string telFijo { get; set; }
        public string extension { get; set; }
        public double? entidadT { get; set; }
        public string municipio { get; set; }
        public string CodigoPost { get; set; }
        public string tCargoPu { get; set; }
        public string pEjecucion { get; set; }
        public string tCargoPuF { get; set; }
        public string nombFamiliar { get; set; }
        public string puestoFam { get; set; }
        public string perEjecucionFam { get; set; }
        public string tBeneneficiario { get; set; }
        public string nombBene { get; set; }
        public string tipPension { get; set; }
        public string ubiPago { get; set; }
        public string delegacionImss { get; set; }
        public string nombTest1 { get; set; }
        public string matricula1 { get; set; }
        public string gafete1 { get; set; }
        public string nombTest2 { get; set; }
        public string matricula2 { get; set; }
        public string gafete2 { get; set; }
        //Domicilio
        public string codPostDom { get; set; }
        public string yearResidencia { get; set; }
        public double? entidadDom { get; set; }
        public string municipioDom { get; set; }
        public string coloniaDom { get; set; }
        public string otraColoniaDom { get; set; }
        public string domicilioCalle { get; set; }
        public string noExteriorDom { get; set; }
        public string noInteriorDom { get; set; }
        public string entreCalleDom { get; set; }
        public string emailContacto { get; set; }
        public string CelularContacto { get; set; }
        public string CompanyPhone { get; set; }
        public string telefonoPropio { get; set; }
        //Referencias
        public string nombreRef1 { get; set; }
        public string pApellidoRef1 { get; set; }
        public string sApellidoRef1 { get; set; }
        public string TelefonoRef1 { get; set; }
        public string CelularRef1 { get; set; }
        public string Hora1Ref1 { get; set; }
        public string Hora2Ref1 { get; set; }
        public string dia1Ref1 { get; set; }
        public string dia2Ref1 { get; set; }
        public string ParentescoRef1 { get; set; }
        public string nombreRef2 { get; set; }
        public string pApellidoRef2 { get; set; }
        public string sApellidoRef2 { get; set; }
        public string TelefonoRef2 { get; set; }
        public string CelularRef2 { get; set; }
        public string Hora1Ref2 { get; set; }
        public string Hora2Ref2 { get; set; }
        public string dia1Ref2 { get; set; }
        public string dia2Ref2 { get; set; }
        public string ParentescoRef2 { get; set; }
        //Dispocision
        public double? medioDisp { get; set; }
        public string ClabeDisp { get; set; }
        public string NombreBanco { get; set; }
        public string NumCuentaBanc { get; set; }
        public double? medioDispAlt { get; set; }
        public string ClabeDispAlt { get; set; }
        public string NombreBancoAlt { get; set; }
        public string NumCuentaBancAlt { get; set; }
        public double? medioDispAlt2 { get; set; }
        public string ClabeDispAlt2 { get; set; }
        public string NombreBancoAlt2 { get; set; }
        public string NumCuentaBancAlt2 { get; set; }
        public double? expediente_completo { get; set; }
        //Cartera
        public double depositoCliente { get; set; }
        public string DiasPagar { get; set; }
        public List<compra_cartera> cartera { get; set; }
        public double cliente_siebel { get; set; }
        //Polizas
        public double tiene_seguro { get; set; }
        public double? codePlan { get; set; }
        public double? planValue { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class OutFormularioSolicitud
    {
        public List<FormularioSolicitud> ListFormularios { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class solicitudesProgreso
    {
        public string folderNumber { get; set; }
        public string asesor { get; set; }
        public string fecha_soliciud { get; set; }
        public string rfc { get; set; }
        public string estado { get; set; }
        public string cliente { get; set; }
        public string tipo { get; set; }
        public double monto { get; set; }
        public double plazo { get; set; }
        public double cuota { get; set; }
        public string asesor_superior { get; set; }
        public string dependencia { get; set; }
        public string sucursal { get; set; }
        public string producto { get; set; }
        public double monto_deudor { get; set; }
        public double monto_remanente { get; set; }
        public double consecutivo { get; set; }
        public string fecha_desembolso { get; set; }

        public double dependenciaCodigo { get; set; }
        public double productoCodigo { get; set; }
        public List<compra_cartera> cartera { get; set; }
    }
    public class OutSolicitudProgreso
    {
        public List<solicitudesProgreso> listSolicitudes { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class compra_cartera
    {
        public double? item { get; set; }
        public string fecha { get; set; }
        public string entidad { get; set; }
        public double capital { get; set; }
        public double totPagar { get; set; }
        public double descuento { get; set; }
        public double plazo { get; set; }
        public double saldoInsoluto { get; set; }
        public double tasa { get; set; }
        public string rfc_casa { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class FormularioSolicitudDocs
    {
        //Solicitud
        public string id_sibel { get; set; }
        public string folderNumber { get; set; }
        public string estado { get; set; }
        public string fchsolicitud { get; set; }
        public string tipoSolicitud { get; set; }
        public string monto { get; set; }
        public string periodo { get; set; }
        public string plazo { get; set; }
        public string LBase { get; set; }
        public string nPlazas { get; set; }
        public string Dependencia { get; set; }
        public string producto { get; set; }
        public string destino { get; set; }
        public string tNomina { get; set; }
        public string dscto { get; set; }
        public string tAnual { get; set; }
        public string sucursal { get; set; }
        public string cat { get; set; }
        public string quincenaDscto { get; set; }
        public string fchPrPago { get; set; }
        public string fchUltPago { get; set; }
        public string cPago { get; set; }
        public string mMaxPlaz { get; set; }
        public string especificar { get; set; }
        public string grupo { get; set; }
        public string matricula { get; set; }
        public string monto_deudor { get; set; }
        public string clave_trabajdor { get; set; }
        public string nss { get; set; }
        public string reca { get; set; }
        //Datos
        public string RFC { get; set; }
        public string pNombre { get; set; }
        public string sNombre { get; set; }
        public string pApellido { get; set; }
        public string sApellido { get; set; }
        public string tipoDoc { get; set; }
        public string CURP { get; set; }
        public string fecNac { get; set; }
        public string paisN { get; set; }
        public string entidadN { get; set; }
        public string paisR { get; set; }
        public string fMigratoria { get; set; }
        public string gender { get; set; }
        public string otraIdentificacion { get; set; }
        public string estadoCivil { get; set; }
        public string nacionalidad { get; set; }
        //ocupacion
        public string sector { get; set; }
        public string otroSector { get; set; }
        public string puesto { get; set; }
        public string antiguedad { get; set; }
        public string ingresos { get; set; }
        public string Celular { get; set; }
        public string cPresupuestal { get; set; }
        public string Pagaduria { get; set; }
        public string fchIngreso { get; set; }
        public string clave { get; set; }
        public string lugTrabajo { get; set; }
        public string calle { get; set; }
        public string nExterior { get; set; }
        public string colonia { get; set; }
        public string otraColonia { get; set; }
        public string telFijo { get; set; }
        public string extension { get; set; }
        public string entidadT { get; set; }
        public string municipio { get; set; }
        public string CodigoPost { get; set; }
        public string tCargoPu { get; set; }
        public string pEjecucion { get; set; }
        public string tCargoPuF { get; set; }
        public string nombFamiliar { get; set; }
        public string puestoFam { get; set; }
        public string perEjecucionFam { get; set; }
        public string tBeneneficiario { get; set; }
        public string nombBene { get; set; }
        public string tipPension { get; set; }
        public string ubiPago { get; set; }
        public string delegacionImss { get; set; }
        public string nombTest1 { get; set; }
        public string matricula1 { get; set; }
        public string gafete1 { get; set; }
        public string nombTest2 { get; set; }
        public string matricula2 { get; set; }
        public string gafete2 { get; set; }
        public string codPostDom { get; set; }
        public string yearResidencia { get; set; }
        public string entidadDom { get; set; }
        public string municipioDom { get; set; }
        public string coloniaDom { get; set; }
        public string otraColoniaDom { get; set; }
        public string domicilioCalle { get; set; }
        public string noExteriorDom { get; set; }
        public string noInteriorDom { get; set; }
        public string entreCalleDom { get; set; }
        public string emailContacto { get; set; }
        public string CelularContacto { get; set; }
        public string CompanyPhone { get; set; }
        public string telefonoPropio { get; set; }
        public string nombreRef1 { get; set; }
        public string pApellidoRef1 { get; set; }
        public string sApellidoRef1 { get; set; }
        public string TelefonoRef1 { get; set; }
        public string CelularRef1 { get; set; }
        public string Hora1Ref1 { get; set; }
        public string Hora2Ref1 { get; set; }
        public string dia1Ref1 { get; set; }
        public string dia2Ref1 { get; set; }
        public string ParentescoRef1 { get; set; }
        public string nombreRef2 { get; set; }
        public string pApellidoRef2 { get; set; }
        public string sApellidoRef2 { get; set; }
        public string TelefonoRef2 { get; set; }
        public string CelularRef2 { get; set; }
        public string Hora1Ref2 { get; set; }
        public string Hora2Ref2 { get; set; }
        public string dia1Ref2 { get; set; }
        public string dia2Ref2 { get; set; }
        public string ParentescoRef2 { get; set; }
        public string medioDisp { get; set; }
        public string ClabeDisp { get; set; }
        public string NombreBanco { get; set; }
        public string NumCuentaBanc { get; set; }
        public string medioDispAlt { get; set; }
        public string ClabeDispAlt { get; set; }
        public string NombreBancoAlt { get; set; }
        public string NumCuentaBancAlt { get; set; }
        public string medioDispAlt2 { get; set; }
        public string ClabeDispAlt2 { get; set; }
        public string NombreBancoAlt2 { get; set; }
        public string NumCuentaBancAlt2 { get; set; }
        public double? expediente_completo { get; set; }
        //Cartera
        public string depositoCliente { get; set; }
        public string DiasPagar { get; set; }
        public double cliente_siebel { get; set; }
        public List<compra_cartera> cartera { get; set; }
        //Polizas
        public double codePlan { get; set; }
        public double planValue { get; set; }
        public double idPoliza { get; set; }
        //Asesor
        public string asesor { get; set; }
        //seguros
        public double tiene_seguro { get; set; }
        public string tieneSeguro { get; set; }
        public string tipoPlan { get; set; }
        public string estatus { get; set; }
        public string fechaVigencia { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
