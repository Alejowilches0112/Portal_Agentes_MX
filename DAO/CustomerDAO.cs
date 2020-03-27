using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CustomerDAO
    {
        public OutCustomer GetCustomerInformation(string customerID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCustomer response = new OutCustomer();
            var ora = new OracleServer(connectionString);
            string command = string.Empty;

            try
            {
                command = " SELECT T.TIPO_DOCUMENTO, T.CEDULA, T.NOMBRE1, T.NOMBRE2, T.APELLIDO1, T.APELLIDO2, T.GENERO_PERSONA,T.FECHA_NACTO, T.CELULAR,  ";
                command = command + " T.CONVENIO, T.PAGADURIA, T.CARGO, T.FECHA_VINCULACION_LABORAL, T.CONTRATO, T.SALARIO, T.VLR_SALUD, T.VLR_PENSION, ";
                command = command + " T.VLR_RETEFUENTE,T.OTROS_DESCUENTOS, T.VLR_FONDOSOLIDARIDAD, T.LINEA_CREDITO, T.TIPO_CREDITO, T.PLAZO, T.MONTO_SOLICITADO, T.OTROS_INGRESOS  ";
                command = command + string.Format("FROM  V_DLG_SOLICITUDES T WHERE CEDULA = '{0}'", customerID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    response.documentType = DBNull.Value.Equals(rdr["CEDULA"]) ? 0 : int.Parse(rdr["CEDULA"].ToString());
                    response.documentID = DBNull.Value.Equals(rdr["TIPO_DOCUMENTO"]) ? string.Empty : rdr["TIPO_DOCUMENTO"].ToString();
                    response.name1 = DBNull.Value.Equals(rdr["NOMBRE1"]) ? string.Empty : rdr["NOMBRE1"].ToString();
                    response.name2 = DBNull.Value.Equals(rdr["NOMBRE2"]) ? string.Empty : rdr["NOMBRE2"].ToString();
                    response.surname1 = DBNull.Value.Equals(rdr["APELLIDO1"]) ? string.Empty : rdr["APELLIDO1"].ToString();
                    response.surname2 = DBNull.Value.Equals(rdr["APELLIDO2"]) ? string.Empty : rdr["APELLIDO2"].ToString();
                    response.gender = DBNull.Value.Equals(rdr["GENERO_PERSONA"]) ? "1" : rdr["GENERO_PERSONA"].ToString();
                    response.birthdate = DBNull.Value.Equals(rdr["FECHA_NACTO"]) ? string.Empty : DateTime.Parse( rdr["FECHA_NACTO"].ToString()).ToString("dd'/'MM'/'yyyy");
                    response.cellphone = DBNull.Value.Equals(rdr["CELULAR"]) ? 0 : double.Parse(rdr["CELULAR"].ToString());
                    response.agreement = DBNull.Value.Equals(rdr["CONVENIO"]) ? 0 : double.Parse(rdr["CONVENIO"].ToString());
                    response.payable = DBNull.Value.Equals(rdr["PAGADURIA"]) ? 0 : double.Parse(rdr["PAGADURIA"].ToString());
                    response.position = DBNull.Value.Equals(rdr["CARGO"]) ? 0 : double.Parse(rdr["CARGO"].ToString());
                    response.vinculationDate = DBNull.Value.Equals(rdr["FECHA_VINCULACION_LABORAL"]) ? string.Empty : DateTime.Parse(rdr["FECHA_VINCULACION_LABORAL"].ToString()).ToString("dd'/'MM'/'yyyy");
                    response.contractType = DBNull.Value.Equals(rdr["CONTRATO"]) ? 0 : int.Parse(rdr["CONTRATO"].ToString());
                    response.salary = DBNull.Value.Equals(rdr["SALARIO"]) ? 0 : double.Parse(rdr["SALARIO"].ToString());
                    response.health = DBNull.Value.Equals(rdr["VLR_SALUD"]) ? 0 : double.Parse(rdr["VLR_SALUD"].ToString());
                    response.pension = DBNull.Value.Equals(rdr["VLR_PENSION"]) ? 0 : double.Parse(rdr["VLR_PENSION"].ToString());
                    response.retefuente = DBNull.Value.Equals(rdr["VLR_RETEFUENTE"]) ? 0 : double.Parse(rdr["VLR_RETEFUENTE"].ToString());
                    response.otherDiscounts = DBNull.Value.Equals(rdr["OTROS_DESCUENTOS"]) ? 0 : double.Parse(rdr["OTROS_DESCUENTOS"].ToString());
                    response.solidarityFunds = DBNull.Value.Equals(rdr["VLR_FONDOSOLIDARIDAD"]) ? 0 : double.Parse(rdr["VLR_FONDOSOLIDARIDAD"].ToString());
                    response.LoanLine = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : int.Parse(rdr["LINEA_CREDITO"].ToString());
                    response.LoanType = DBNull.Value.Equals(rdr["TIPO_CREDITO"]) ? 0 : int.Parse(rdr["TIPO_CREDITO"].ToString());
                    response.term = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : int.Parse(rdr["PLAZO"].ToString());
                    response.amount = DBNull.Value.Equals(rdr["MONTO_SOLICITADO"]) ? 0 : double.Parse(rdr["MONTO_SOLICITADO"].ToString());
                    response.otherIncome = DBNull.Value.Equals(rdr["OTROS_INGRESOS"]) ? 0 : double.Parse(rdr["OTROS_INGRESOS"].ToString());
                    
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerDAO.GetCustomerInformation", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutFolder GetFolderInformation(string customerID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutFolder response = new OutFolder();
            var ora = new OracleServer(connectionString);
            Folder folder;
            List<Folder> list = new List<Folder>();
            string command = string.Empty;
            try
            {
                command = "select numero_carpeta, fecha_creacion, monto_solicitado, plazo_solicitado from v_dlg_carpetas ";
                command += "where not codigo_estado_carpeta in (0, 100, 1300, 1500, 1600, 1700, 1800, 900000) ";
                command += string.Format("and numero_identificacion = '{0}'", customerID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    folder = new Folder();
                    folder.folder = DBNull.Value.Equals(rdr["numero_carpeta"]) ? 0 : double.Parse(rdr["numero_carpeta"].ToString());
                    folder.monto = DBNull.Value.Equals(rdr["monto_solicitado"]) ? 0 : double.Parse(rdr["monto_solicitado"].ToString());
                    folder.plazo = DBNull.Value.Equals(rdr["plazo_solicitado"]) ? 0 : double.Parse(rdr["plazo_solicitado"].ToString());
                    folder.create_date = DBNull.Value.Equals(rdr["fecha_creacion"]) ? string.Empty : rdr["fecha_creacion"].ToString();
                    list.Add(folder);
                }
                rdr.Close();
                response.lstFolder = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerDAO.GetFolderInformation", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
