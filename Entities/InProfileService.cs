using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class InProfileService
    {
        //Datos Solicitante
        public string numDoc { get; set; }
        public string pNombre { get; set; }
        public string pApellido { get; set; }
        public string sApellido { get; set; }
        public int paisN { get; set; }
        public int entidadN { get; set; }
        public int paisR { get; set; }
        public string fMigratoria { get; set; }
        public int gender { get; set; }

        [Required]
        [DataType("YYYY-MM-DD 00:00:00")]
        public string fecNac { get; set; }
        //Solicitud
        public int tipoCredito { get; set; }
        public int lineCredito { get; set; }
        public string monto { get; set; }
        public int periodo { get; set; }
        public string plazo { get; set; }
        public string convenio { get; set; }
        public int destino { get; set; }
        public int tNomina { get; set; }
        public int nPlazas { get; set; }
        public int dscto { get; set; }
        public int tAnual { get; set; }
        public string cat { get; set; }
        public int mOpcion { get; set; }
        public int quincenaDscto { get; set; }
        [DataType("YYYY-MM-DD 00:00:00")]
        public string fchPrPago { get; set; }
        [DataType("YYYY-MM-DD 00:00:00")]
        public string fchUltPago { get; set; }
        public int LBase { get; set; }
        public int cPago { get; set; }
        public int mMaxPlaz { get; set; }
        //Ocupacion
        public int sector { get; set; }
        public int puesto { get; set; }
        public int antiguedad { get; set; }
        public int salario { get; set; }
        public string Celular { get; set; }
        public string cPresupuestal { get; set; }
        public int pagaduria { get; set; }
        public string delegacion { get; set; }
        public string zonEscolar { get; set; }
        public string clave { get; set; }
        public string lugTrabajo { get; set; }
        public string calle { get; set; }
        public string nExterior { get; set; }
        public string colonia { get; set; }
        public int telFijo { get; set; }
        public int extension { get; set; }
        public int entidadT { get; set; }
        public int municipio { get; set; }
        public string CodigoPost { get; set; }
        public int tCargoPu { get; set; }
        public string pEjecucion { get; set; }
        public int tCargoPuF { get; set; }
        public string nombFamiliar { get; set; }
        public string puestoFam { get; set; }
        public int perEjecucionFam { get; set; }
        public int tCargoPuBen { get; set; }
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
        public int yearResidencia { get; set; }
        public int entidadDom { get; set; }
        public int municipioDom { get; set; }
        public int coloniaDom { get; set; }
        public string domicilioCalle { get; set; }
        public string noExteriorDom { get; set; }
        public string noInteriorDom { get; set; }
        public string entreCalleDom { get; set; }
        public string emailContacto { get; set; }
        public string CelularContacto { get; set; }
        public string CompanyPhone { get; set; }
        public int telefonoPropio { get; set; }
        //Referencias Personales
        public string nombreRef1 { get; set; }
        public string pApellidoRef1 { get; set; }
        public string sApellidoRef1 { get; set; }
        public int TelefonoRef1 { get; set; }
        public int CelularRef1 { get; set; }
        public string HoraContactoRef1 { get; set; }
        public string ParentescoRef1 { get; set; }
        public string nombreRef2 { get; set; }
        public string pApellidoRef2 { get; set; }
        public string sApellidoRef2 { get; set; }
        public int TelefonoRef2 { get; set; }
        public int CelularRef2 { get; set; }
        public string HoraContactoRef2 { get; set; }
        public string ParentescoRef2 { get; set; }
        //Medio Disposicion
        public string ClabeDisp{ get; set; }
        public string NombreBanco { get; set; }
        public string NumCuentaBanc { get; set; }
        public int medioDispAlt { get; set; }
        public string ClabeDispAlt { get; set; }
        public string NombreBancoAlt { get; set; }
        public string NumCuentaBancAlt { get; set; }
        //Compra Cartera
        public string depositoCliente { get; set; }
        public int DiasPagar { get; set; }
        public List<cartera> cartera { get; set; }
        public string regional { get; set; }
        public int executiveCode { get; set; }
        public int brachCode { get; set; }
        public string folderNumber { get; set; }
    }
}
