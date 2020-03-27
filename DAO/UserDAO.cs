using Entities;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAO
{
    public class UserDAO
    {
        public OutUserOptions GetUserOptions(string executiveID, string ind_menu, string svrpath)
        {
            if (executiveID == "" || executiveID == null) { return null; }
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutUserOptions response = new OutUserOptions();
            var ora = new OracleServer(connectionString);
            UserOptions status;
            List<UserOptions> list = new List<UserOptions>();
            string command = string.Empty;

            try
            {
                command = "select cedula, ind_menu_inicio, funcionalidad_nombre, ventana_objeto from dlg_portal_get_user_options";
                command = command + string.Format(" where ind_menu_inicio = {0} and cedula = '{1}' ", ind_menu, executiveID);

                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    status = new UserOptions();
                    status.funcionalidad_nombre = DBNull.Value.Equals(rdr["funcionalidad_nombre"]) ? string.Empty : rdr["funcionalidad_nombre"].ToString();
                    if (ind_menu == "5")
                    {
                        status.ventana_objeto = DBNull.Value.Equals(rdr["ventana_objeto"]) ? string.Empty : rdr["ventana_objeto"].ToString();
                    }
                    else
                    {
                        status.ventana_objeto = DBNull.Value.Equals(rdr["ventana_objeto"]) ? string.Empty : /*svrpath +*/ rdr["ventana_objeto"].ToString();
                    }
                    list.Add(status);
                }
                rdr.Close();
                response.lstUserOptions = list;
                response.msg = new Response
                {
                    errorCode = "200",
                    errorMessage = "OK"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("UserDAO.GetUserOptions", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response ChangeEstadoSoli(string asesor)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_asesor = new OracleParameter("fa_asesor", OracleDbType.Varchar2, asesor, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 2000;
                ora.AddParameter(pi_cod_asesor);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_dias_expiracion_form");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.ChangeEstadoSoli", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
