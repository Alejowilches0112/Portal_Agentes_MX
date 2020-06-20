using Entities;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ExecutiveDAO
    {
        public Response UpdateExecutive(InUpdateExecutive input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            try
            {
                var pi_documentID = new OracleParameter("fa_cedula", OracleDbType.Varchar2, input.documentID, ParameterDirection.Input);
                ora.AddParameter(pi_documentID);

                var pi_name1 = new OracleParameter("fa_nombre1", OracleDbType.Varchar2, input.name1, ParameterDirection.Input);
                ora.AddParameter(pi_name1);

                var pi_name2 = new OracleParameter("fa_nombre2", OracleDbType.Varchar2, input.name2, ParameterDirection.Input);
                ora.AddParameter(pi_name2);

                var pi_surname1 = new OracleParameter("fa_apellido1", OracleDbType.Varchar2, input.surname1, ParameterDirection.Input);
                ora.AddParameter(pi_surname1);

                var pi_surname2 = new OracleParameter("fa_apellido2", OracleDbType.Varchar2, input.surname2, ParameterDirection.Input);
                ora.AddParameter(pi_surname2);

                var pi_birthDate = new OracleParameter("fa_fecha_nacto", OracleDbType.Date, input.birthDate, ParameterDirection.Input);
                ora.AddParameter(pi_birthDate);

                var pi_placeBirth = new OracleParameter("fa_lugar_nacto", OracleDbType.Double, input.placeBirth, ParameterDirection.Input);
                ora.AddParameter(pi_placeBirth);

                var pi_gender = new OracleParameter("fa_genero_persona", OracleDbType.Double, input.gender, ParameterDirection.Input);
                ora.AddParameter(pi_gender);

                var pi_civilStatus = new OracleParameter("fa_estado_civil", OracleDbType.Double, input.civilStatus, ParameterDirection.Input);
                ora.AddParameter(pi_civilStatus);

                var pi_notifyAddress = new OracleParameter("fa_direccion_notificacion", OracleDbType.Varchar2, input.notifyAddress, ParameterDirection.Input);
                ora.AddParameter(pi_notifyAddress);

                var pi_department = new OracleParameter("fa_departamento", OracleDbType.Double, input.department, ParameterDirection.Input);
                ora.AddParameter(pi_department);

                var pi_city = new OracleParameter("fa_ciudad_ase", OracleDbType.Double, input.city, ParameterDirection.Input);
                ora.AddParameter(pi_city);

                var pi_neighborhood = new OracleParameter("fa_barrio", OracleDbType.Double, input.neighborhood, ParameterDirection.Input);
                ora.AddParameter(pi_neighborhood);

                var pi_executivePhone = new OracleParameter("fa_telefono_ejecutivo", OracleDbType.Varchar2, input.executivePhone, ParameterDirection.Input);
                ora.AddParameter(pi_executivePhone);

                var pi_housePhone = new OracleParameter("fa_telefono_fijo", OracleDbType.Varchar2, input.housePhone, ParameterDirection.Input);
                ora.AddParameter(pi_housePhone);

                var pi_housingType = new OracleParameter("fa_tipo_vivienda", OracleDbType.Double, input.housingType, ParameterDirection.Input);
                ora.AddParameter(pi_housingType);

                var pi_email = new OracleParameter("fa_correo_electronico", OracleDbType.Varchar2, input.email, ParameterDirection.Input);
                ora.AddParameter(pi_email);

                var pi_appliedStudies = new OracleParameter("fa_estudios_realizados", OracleDbType.Double, input.appliedStudies, ParameterDirection.Input);
                ora.AddParameter(pi_appliedStudies);

                var pi_notifyEmail = new OracleParameter("fa_correo_ele_notificacion", OracleDbType.Varchar2, input.notifyEmail, ParameterDirection.Input);
                ora.AddParameter(pi_notifyEmail);

                var pi_bayportEmail = new OracleParameter("fa_correo_ele_bayport", OracleDbType.Varchar2, input.bayportEmail, ParameterDirection.Input);
                ora.AddParameter(pi_bayportEmail);

                var pi_emergencyPhone = new OracleParameter("fa_tel_caso_de_emergencia", OracleDbType.Varchar2, input.emergencyPhone, ParameterDirection.Input);
                ora.AddParameter(pi_emergencyPhone);

                var pi_bankAccount = new OracleParameter("fa_cuentabancaria", OracleDbType.Varchar2, input.bankAccount, ParameterDirection.Input);
                ora.AddParameter(pi_bankAccount);

                var pi_accountType = new OracleParameter("fa_tipodecuenta", OracleDbType.Double, input.accountType, ParameterDirection.Input);
                ora.AddParameter(pi_accountType);

                var pi_entityBank = new OracleParameter("fa_bancooentidad", OracleDbType.Double, input.entityBank, ParameterDirection.Input);
                ora.AddParameter(pi_entityBank);

                var pi_spouseName = new OracleParameter("fa_nombre_conyuge", OracleDbType.Varchar2, input.spouseName, ParameterDirection.Input);
                ora.AddParameter(pi_spouseName);

                var pi_spouseID = new OracleParameter("fa_cedula_conyuge", OracleDbType.Varchar2, input.spouseID, ParameterDirection.Input);
                ora.AddParameter(pi_spouseID);

                var pi_spouseCellphone = new OracleParameter("fa_celular_conyuge", OracleDbType.Varchar2, input.spouseCellphone, ParameterDirection.Input);
                ora.AddParameter(pi_spouseCellphone);

                var pi_spouseEmail = new OracleParameter("fa_correo_ele_conyuge", OracleDbType.Varchar2, input.spouseEmail, ParameterDirection.Input);
                ora.AddParameter(pi_spouseEmail);

                var pi_assets = new OracleParameter("fa_total_activos", OracleDbType.Double, input.assets, ParameterDirection.Input);
                ora.AddParameter(pi_assets);

                var pi_liabilities = new OracleParameter("fa_total_pasivos", OracleDbType.Double, input.liabilities, ParameterDirection.Input);
                ora.AddParameter(pi_liabilities);

                var pi_income = new OracleParameter("fa_ingresos", OracleDbType.Double, input.income, ParameterDirection.Input);
                ora.AddParameter(pi_income);

                var pi_expenses = new OracleParameter("fa_gastos", OracleDbType.Double, input.expenses, ParameterDirection.Input);
                ora.AddParameter(pi_expenses);

                var pi_otherIncome = new OracleParameter("fa_otros_ingresos", OracleDbType.Double, input.otherIncome, ParameterDirection.Input);
                ora.AddParameter(pi_otherIncome);

                var pi_afpNIT = new OracleParameter("fa_nit_afp", OracleDbType.Double, input.afpNIT, ParameterDirection.Input);
                ora.AddParameter(pi_afpNIT);

                var pi_arpNIT = new OracleParameter("fa_nit_arp", OracleDbType.Double, input.arpNIT, ParameterDirection.Input);
                ora.AddParameter(pi_arpNIT);

                var pi_epsNIT = new OracleParameter("fa_nit_eps", OracleDbType.Double, input.epsNIT, ParameterDirection.Input);
                ora.AddParameter(pi_epsNIT);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);


                ora.ExecuteProcedureNonQuery("DLGWFC_F_UPDATE_ASESOR_PORTAL");
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

                ora.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ExecutiveDAO.UpdateExecutive", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutExecutiveInformation GetExecutiveInformation(string executiveID, double asesor)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutExecutiveInformation response = new OutExecutiveInformation();
            var ora = new OracleServer(connectionString);
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_ADMINISTRA, CODIGO_SUCURSAL, CODIGO_EJECUTIVO, NOMBRE_EJECUTIVO, TELEFONO_EJECUTIVO, TIPO_EJECUTIVO, ";
                command = command + "CODIGO_PC, CENTRO_COSTO, CODIGO_USUARIO, CEDULA, ESTADO, FECHA_ASESOR, FECHA_INACTIVACION, NOMBRE, APELLIDOS, ";
                command = command + "TIPO_ASESOR, APLICA_INCENTIVOS, CODIGO_ZONA, CODIGO_JEFE_CCIAL, IND_JEFE_ZONA, CORREO_ELECTRONICO, COD_CNL_VTA, ";
                command = command + "CODIGO_REGIONAL, TIPO_CANAL,  TIPO_DOCUMENTO, FECHA_EXP_CEDULA,LUGAR_EXPEDICION, GENERO_PERSONA, FECHA_NACTO, ";
                command = command + "LUGAR_NACTO, ESTADO_CIVIL, PERSONAS_CARGO, TIPO_VIVIENDA, ESTUDIOS_REALIZADOS, DIRECCION_NOTIFICACION, BARRIO, ";
                command = command + "DEPARTAMENTO, TELEFONO_FIJO, CORREO_ELE_NOTIFICACION, CORREO_ELE_BAYPORT, NOMBRE_CONYUGE, CEDULA_CONYUGE, ";
                command = command + "CORREO_ELE_CONYUGE, CELULAR_CONYUGE, TOTAL_ACTIVOS, TOTAL_PASIVOS, INGRESOS, GASTOS, OTROS_INGRESOS, DESCRIPCION_OTROS_ING, ";
                command = command + "CUENTABANCARIA, TIPODECUENTA, BANCOOENTIDAD, TIPODEAGENTE, CODIGOCOMISION, TIENEOFICINA, CAUSALDECANCELACION, ";
                command = command + "CATEGORIAEJECUTIVO, AQUIENLLAMARENCASODEEMERGENCIA, TELEFONO_EN_CASO_DE_EMERGENCIA, CIUDAD_ASE, IND_EN_LISTAS_NEGRAS, ";
                command = command + "NOMBRE1, NOMBRE2, APELLIDO1, APELLIDO2, USUARIO_CREA, FECHA_CREA, USUARIO_MODIFICA, CLAVE_PORTAL, CLAVE_FECHA_ULTIMA_MODIF, ";
                command = command + string.Format("IND_EJECUTIVO, NIT_AFP, NIT_ARP, NIT_EPS/*, NUI*/ from DFPEJC40 where CEDULA = '{0}' and codigo_ejecutivo = {1}", executiveID, asesor.ToString());
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    response.name1 = DBNull.Value.Equals(rdr["NOMBRE1"]) ? string.Empty : rdr["NOMBRE1"].ToString();
                    response.name2 = DBNull.Value.Equals(rdr["NOMBRE2"]) ? string.Empty : rdr["NOMBRE2"].ToString();
                    response.surname1 = DBNull.Value.Equals(rdr["APELLIDO1"]) ? string.Empty : rdr["APELLIDO1"].ToString();
                    response.surname2 = DBNull.Value.Equals(rdr["APELLIDO2"]) ? string.Empty : rdr["APELLIDO2"].ToString();
                    response.birthDate = DBNull.Value.Equals(rdr["FECHA_NACTO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_NACTO"].ToString()).ToString("dd/MM/yyyy");
                    response.placeBirth = DBNull.Value.Equals(rdr["LUGAR_NACTO"]) ? 0 : double.Parse(rdr["LUGAR_NACTO"].ToString());
                    response.gender = DBNull.Value.Equals(rdr["GENERO_PERSONA"]) ? 0 : double.Parse(rdr["GENERO_PERSONA"].ToString());
                    response.civilStatus = DBNull.Value.Equals(rdr["ESTADO_CIVIL"]) ? 0 : double.Parse(rdr["ESTADO_CIVIL"].ToString());
                    response.notifyAddress = DBNull.Value.Equals(rdr["DIRECCION_NOTIFICACION"]) ? string.Empty : rdr["DIRECCION_NOTIFICACION"].ToString();
                    response.department = DBNull.Value.Equals(rdr["DEPARTAMENTO"]) ? 0 : double.Parse(rdr["DEPARTAMENTO"].ToString());
                    response.city = DBNull.Value.Equals(rdr["CIUDAD_ASE"]) ? 0 : double.Parse(rdr["CIUDAD_ASE"].ToString());
                    response.neighborhood = DBNull.Value.Equals(rdr["BARRIO"]) ? 0 : double.Parse(rdr["BARRIO"].ToString());
                    response.executivePhone = DBNull.Value.Equals(rdr["TELEFONO_EJECUTIVO"]) ? string.Empty : rdr["TELEFONO_EJECUTIVO"].ToString();
                    response.housePhone = DBNull.Value.Equals(rdr["TELEFONO_FIJO"]) ? 0 : double.Parse(rdr["TELEFONO_FIJO"].ToString());
                    response.housingType = DBNull.Value.Equals(rdr["TIPO_VIVIENDA"]) ? 0 : double.Parse(rdr["TIPO_VIVIENDA"].ToString());
                    response.email = DBNull.Value.Equals(rdr["CORREO_ELECTRONICO"]) ? string.Empty : rdr["CORREO_ELECTRONICO"].ToString();
                    response.appliedStudies = DBNull.Value.Equals(rdr["ESTUDIOS_REALIZADOS"]) ? 0 : double.Parse(rdr["ESTUDIOS_REALIZADOS"].ToString());
                    response.notifyEmail = DBNull.Value.Equals(rdr["CORREO_ELE_NOTIFICACION"]) ? string.Empty : rdr["CORREO_ELE_NOTIFICACION"].ToString();
                    response.bayportEmail = DBNull.Value.Equals(rdr["CORREO_ELE_BAYPORT"]) ? string.Empty : rdr["CORREO_ELE_BAYPORT"].ToString();
                    response.emergencyPhone = DBNull.Value.Equals(rdr["TELEFONO_EN_CASO_DE_EMERGENCIA"]) ? 0 : double.Parse(rdr["TELEFONO_EN_CASO_DE_EMERGENCIA"].ToString());
                    response.bankAccount = DBNull.Value.Equals(rdr["CUENTABANCARIA"]) ? string.Empty : rdr["CUENTABANCARIA"].ToString();
                    response.accountType = DBNull.Value.Equals(rdr["TIPODECUENTA"]) ? 0 : double.Parse(rdr["TIPODECUENTA"].ToString());
                    response.entityBank = DBNull.Value.Equals(rdr["BANCOOENTIDAD"]) ? 0 : double.Parse(rdr["BANCOOENTIDAD"].ToString());
                    response.spouseName = DBNull.Value.Equals(rdr["NOMBRE_CONYUGE"]) ? string.Empty : rdr["NOMBRE_CONYUGE"].ToString();
                    response.spouseID = DBNull.Value.Equals(rdr["CEDULA_CONYUGE"]) ? string.Empty : rdr["CEDULA_CONYUGE"].ToString();
                    response.spouseCellphone = DBNull.Value.Equals(rdr["CELULAR_CONYUGE"]) ? 0 : double.Parse(rdr["CELULAR_CONYUGE"].ToString());
                    response.spouseEmail = DBNull.Value.Equals(rdr["CORREO_ELE_CONYUGE"]) ? string.Empty : rdr["CORREO_ELE_CONYUGE"].ToString();
                    response.assets = DBNull.Value.Equals(rdr["TOTAL_ACTIVOS"]) ? 0 : double.Parse(rdr["TOTAL_ACTIVOS"].ToString());
                    response.liabilities = DBNull.Value.Equals(rdr["TOTAL_PASIVOS"]) ? 0 : double.Parse(rdr["TOTAL_PASIVOS"].ToString());
                    response.income = DBNull.Value.Equals(rdr["INGRESOS"]) ? 0 : double.Parse(rdr["INGRESOS"].ToString());
                    response.expenses = DBNull.Value.Equals(rdr["GASTOS"]) ? 0 : double.Parse(rdr["GASTOS"].ToString());
                    response.otherIncome = DBNull.Value.Equals(rdr["OTROS_INGRESOS"]) ? 0 : double.Parse(rdr["OTROS_INGRESOS"].ToString());
                    response.afpNIT = DBNull.Value.Equals(rdr["NIT_AFP"]) ? string.Empty : rdr["NIT_AFP"].ToString();
                    response.arpNIT = DBNull.Value.Equals(rdr["NIT_ARP"]) ? string.Empty : rdr["NIT_ARP"].ToString();
                    response.epsNIT = DBNull.Value.Equals(rdr["NIT_EPS"]) ? string.Empty : rdr["NIT_EPS"].ToString();
                    response.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    response.executiveCode = DBNull.Value.Equals(rdr["CODIGO_EJECUTIVO"]) ? 0 : double.Parse(rdr["CODIGO_EJECUTIVO"].ToString());
                    response.executiveName = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();
                    response.executiveType = DBNull.Value.Equals(rdr["TIPO_EJECUTIVO"]) ? 0 : double.Parse(rdr["TIPO_EJECUTIVO"].ToString());
                    response.otherIncomeDescription = DBNull.Value.Equals(rdr["DESCRIPCION_OTROS_ING"]) ? string.Empty : rdr["DESCRIPCION_OTROS_ING"].ToString();
                    response.userCode = DBNull.Value.Equals(rdr["CODIGO_USUARIO"]) ? string.Empty : rdr["CODIGO_USUARIO"].ToString();
                    response.zoneCode = DBNull.Value.Equals(rdr["CODIGO_ZONA"]) ? 0 : double.Parse(rdr["CODIGO_ZONA"].ToString());
                    response.expeditionDate = DBNull.Value.Equals(rdr["FECHA_EXP_CEDULA"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_EXP_CEDULA"].ToString()).ToString("dd/MM/yyyy");
                    response.documentType = DBNull.Value.Equals(rdr["TIPO_DOCUMENTO"]) ? "0" : rdr["TIPO_DOCUMENTO"].ToString();
                    response.regionalCode = DBNull.Value.Equals(rdr["CODIGO_REGIONAL"]) ? 0 : double.Parse(rdr["CODIGO_REGIONAL"].ToString());
                    response.channelType = DBNull.Value.Equals(rdr["TIPO_CANAL"]) ? 0 : double.Parse(rdr["TIPO_CANAL"].ToString());
                    response.expeditionPlace = DBNull.Value.Equals(rdr["LUGAR_EXPEDICION"]) ? string.Empty : rdr["LUGAR_EXPEDICION"].ToString();
                    response.indZoneBoss = DBNull.Value.Equals(rdr["IND_JEFE_ZONA"]) ? 0 : double.Parse(rdr["IND_JEFE_ZONA"].ToString());
                    response.PC_Code = DBNull.Value.Equals(rdr["CODIGO_PC"]) ? 0 : double.Parse(rdr["CODIGO_PC"].ToString());
                    response.costCenter = DBNull.Value.Equals(rdr["CENTRO_COSTO"]) ? 0 : double.Parse(rdr["CENTRO_COSTO"].ToString());
                    response.documentID = DBNull.Value.Equals(rdr["CEDULA"]) ? string.Empty : rdr["CEDULA"].ToString();
                    response.status = DBNull.Value.Equals(rdr["ESTADO"]) ? "1" : rdr["ESTADO"].ToString();
                    response.executiveDate = DBNull.Value.Equals(rdr["FECHA_ASESOR"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_ASESOR"].ToString()).ToString("dd/MM/yyyy");
                    response.inactivationDate = DBNull.Value.Equals(rdr["FECHA_INACTIVACION"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INACTIVACION"].ToString()).ToString("dd/MM/yyyy");
                    response.bossCode = DBNull.Value.Equals(rdr["CODIGO_JEFE_CCIAL"]) ? 0 : double.Parse(rdr["CODIGO_JEFE_CCIAL"].ToString());
                    response.blackLists = DBNull.Value.Equals(rdr["IND_EN_LISTAS_NEGRAS"]) ? "0" : rdr["IND_EN_LISTAS_NEGRAS"].ToString();
                    response.peopleInCharge = DBNull.Value.Equals(rdr["PERSONAS_CARGO"]) ? 0 : double.Parse(rdr["PERSONAS_CARGO"].ToString());
                    response.creationDate = DBNull.Value.Equals(rdr["FECHA_CREA"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_CREA"].ToString()).ToString("dd/MM/yyyy");
                    response.codCnlVta = DBNull.Value.Equals(rdr["COD_CNL_VTA"]) ? 0 : double.Parse(rdr["COD_CNL_VTA"].ToString());
                    response.executiveCategory = DBNull.Value.Equals(rdr["CATEGORIAEJECUTIVO"]) ? string.Empty : rdr["CATEGORIAEJECUTIVO"].ToString();
                    response.causeCancelation = DBNull.Value.Equals(rdr["CAUSALDECANCELACION"]) ? string.Empty : rdr["CAUSALDECANCELACION"].ToString();
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ExecutiveDAO.GetExecutiveInformation", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
    }

        public OutExecutiveChilds GetExecutiveChilds(string executiveID, int level)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutExecutiveChilds response = new OutExecutiveChilds();
            var ora = new OracleServer(connectionString);

            ExecutiveChilds executiveChilds;
            List<ExecutiveChilds> list = new List<ExecutiveChilds>();
            string command = string.Empty;

            try
            {
                command = "SELECT BBS_LIQCOM_V_ASECARGO.codigo_asesor_hijo, BBS_LIQCOM_V_ASECARGO.cedula_asesor_hijo, BBS_LIQCOM_V_ASECARGO.nombre_asesor_hijo FROM   BBS_LIQCOM_V_ASECARGO ";
                command = command + string.Format(" WHERE BBS_LIQCOM_V_ASECARGO.cedula_asesor_superior =  '{0}' and BBS_LIQCOM_V_ASECARGO.nivel = {1} ", executiveID, level);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    executiveChilds = new ExecutiveChilds();
                    executiveChilds.Code = DBNull.Value.Equals(rdr["codigo_asesor_hijo"]) ? 0 : double.Parse(rdr["codigo_asesor_hijo"].ToString());
                    executiveChilds.Name = DBNull.Value.Equals(rdr["nombre_asesor_hijo"]) ? string.Empty : rdr["nombre_asesor_hijo"].ToString();
                    executiveChilds.DocumentID = DBNull.Value.Equals(rdr["cedula_asesor_hijo"]) ? string.Empty : rdr["cedula_asesor_hijo"].ToString();
                    list.Add(executiveChilds);
                }
                rdr.Close();
                response.lstExecutiveChilds = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ExecutiveDAO.GetExecutiveChilds", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
