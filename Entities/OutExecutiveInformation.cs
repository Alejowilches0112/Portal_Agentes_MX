using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutExecutiveInformation
    {
        public double CODIGO_ADMINISTRA { get; set; }
        public double branchCode { get; set; }
        public double executiveCode { get; set; }
        public string executiveName { get; set; }
        public string executivePhone { get; set; }
        public double executiveType { get; set; }
        public double PC_Code { get; set; }
        public double costCenter { get; set; }
        public string userCode { get; set; }
        public string documentID { get; set; }
        public string status { get; set; }
        public string executiveDate { get; set; }
        public string inactivationDate { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public double TIPO_ASESOR { get; set; }
        public double APLICA_INCENTIVOS { get; set; }
        public double zoneCode { get; set; }
        public double bossCode { get; set; }
        public double indZoneBoss { get; set; }
        public string email { get; set; }
        public double codCnlVta { get; set; }
        public double regionalCode { get; set; }
        public double channelType { get; set; }
        public string documentType{ get; set; }
        public string expeditionDate { get; set; }
        public string expeditionPlace { get; set; }
        public double gender { get; set; }
        public string birthDate { get; set; }
        public double placeBirth { get; set; }
        public double civilStatus { get; set; }
        public double peopleInCharge { get; set; }
        public double housingType { get; set; }
        public double appliedStudies { get; set; }
        public string notifyAddress { get; set; }
        public double neighborhood { get; set; }
        public double department { get; set; }
        public double housePhone { get; set; }
        public string notifyEmail { get; set; }
        public string bayportEmail { get; set; }
        public string spouseName { get; set; }
        public string spouseID { get; set; }
        public string spouseEmail { get; set; }
        public double spouseCellphone { get; set; }
        public double assets { get; set; }
        public double liabilities { get; set; }
        public double income { get; set; }
        public double expenses { get; set; }
        public double otherIncome { get; set; }
        public string otherIncomeDescription { get; set; }       
        public string bankAccount { get; set; }
        public double accountType { get; set; }
        public double entityBank { get; set; }
        public double TIPODEAGENTE { get; set; }
        public double CODIGOCOMISION { get; set; }
        public double TIENEOFICINA { get; set; }
        public string causeCancelation { get; set; }
        public string executiveCategory { get; set; }
        public string AQUIENLLAMARENCASODEEMERGENCIA { get; set; }
        public double emergencyPhone { get; set; }
        public double city { get; set; }
        public string blackLists { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string surname1 { get; set; }
        public string surname2 { get; set; }
        public string USUARIO_CREA { get; set; }
        public string creationDate { get; set; }
        public string USUARIO_MODIFICA { get; set; }
        public string CLAVE_PORTAL { get; set; }
        public string CLAVE_FECHA_ULTIMA_MODIF { get; set; }
        public double IND_EJECUTIVO { get; set; }
        public string afpNIT { get; set; }
        public string arpNIT { get; set; }
        public string epsNIT { get; set; }
        public double NUI { get; set; }

        public Response msg { get; set; } = new Response();

    }
}
