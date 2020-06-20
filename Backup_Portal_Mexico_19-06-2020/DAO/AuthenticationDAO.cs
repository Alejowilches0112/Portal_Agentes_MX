using System;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Entities;
using System.Globalization;

namespace DAO
{
    public class AuthenticationDAO
    {
        public OutValidateUser ValidateUser(InValidateUser user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutValidateUser response = new OutValidateUser();
            var ora = new OracleServer(connectionString);
            try
            {
                System.Diagnostics.Debug.WriteLine("empresa --> ", user.company);
                var pi_Company = new OracleParameter("fa_empresa", OracleDbType.Double, user.company, ParameterDirection.Input);
                var pi_User = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user.userID, ParameterDirection.Input);
                var pi_Password = new OracleParameter("fa_clave", OracleDbType.Varchar2, user.password, ParameterDirection.Input);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_ChangePassword = new OracleParameter("fa_ind_obliga_cambio", OracleDbType.Double, ParameterDirection.Output);
                var po_UserName = new OracleParameter("fa_nombre_usuario", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_sucursal = new OracleParameter("fa_sucursal", OracleDbType.Double, ParameterDirection.Output);
                var po_asesor= new OracleParameter("fa_codigo_asesor", OracleDbType.Double, ParameterDirection.Output);

                po_UserName.Size = 300;
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_Company);
                ora.AddParameter(pi_User);
                ora.AddParameter(pi_Password);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.AddParameter(po_ChangePassword);
                ora.AddParameter(po_UserName);
                ora.AddParameter(po_sucursal);
                ora.AddParameter(po_asesor);

                ora.ExecuteProcedureNonQuery("BBS_PORTAL_F_VALIDA_INGRESO");

                response.msg.errorCode = ora.GetParameter("fa_Error").ToString();
                response.msg.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
                response.userName = ora.GetParameter("fa_nombre_usuario").ToString();
                response.changePassword = int.Parse(ora.GetParameter("fa_ind_obliga_cambio").ToString());
                response.sucursal = double.Parse(ora.GetParameter("fa_sucursal").ToString());
                response.asesor = double.Parse(ora.GetParameter("fa_codigo_asesor").ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw new Exception("AuthenticationDAO.ValidateUser", ex);
            }
            finally {
                ora.Dispose();
            }

            return response;
        }

        public OutValidateUser ValidateUser(string asesor, ref InValidateUser user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutValidateUser response = new OutValidateUser();
            var ora = new OracleServer(connectionString);
            try
            {
                var pi_Company = new OracleParameter("fa_empresa", OracleDbType.Double, user.company, ParameterDirection.Input);
                var pi_asesor = new OracleParameter("fa_asesor", OracleDbType.Varchar2, asesor, ParameterDirection.Input);

                var po_User = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user.userID, ParameterDirection.Output)
                {
                    Size = 100
                };
                var po_Password = new OracleParameter("fa_clave", OracleDbType.Varchar2, user.password, ParameterDirection.Output)
                {
                    Size = 100
                };
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_ChangePassword = new OracleParameter("fa_ind_obliga_cambio", OracleDbType.Double, ParameterDirection.Output);
                var po_UserName = new OracleParameter("fa_nombre_usuario", OracleDbType.Varchar2, ParameterDirection.Output)
                {
                    Size = 100
                };

                po_ErrorMessage.Size = 100;

                ora.AddParameter(pi_Company);
                ora.AddParameter(pi_asesor);
                ora.AddParameter(po_User);
                ora.AddParameter(po_Password);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.AddParameter(po_ChangePassword);
                ora.AddParameter(po_UserName);

                ora.ExecuteProcedureNonQuery("dlg_portal_val_ingreso_asesor");

                response.msg.errorCode = ora.GetParameter("fa_Error").ToString();
                response.msg.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
                response.userName = ora.GetParameter("fa_nombre_usuario").ToString();
                response.userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(response.userName.ToLower());
                response.changePassword = int.Parse(ora.GetParameter("fa_ind_obliga_cambio").ToString());
                response.userID = ora.GetParameter("fa_usuario").ToString();
                response.password = ora.GetParameter("fa_clave").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("AuthenticationDAO.ValidateUser", ex);
            }
            finally
            {
                ora.Dispose();
            }

            return response;
        }
        public Response ChangePassword( InValidateUser input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            Response response = new Response();

            try
            {
                var ora = new OracleServer(connectionString);

                var pi_User = new OracleParameter("fa_cedula", OracleDbType.Double, input.userID, ParameterDirection.Input);
                var pi_Password = new OracleParameter("fa_clave", OracleDbType.Varchar2, input.password, ParameterDirection.Input);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(pi_User);
                ora.AddParameter(pi_Password);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_PORTAL_F_CAMBIO_CLAVE");

                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

                ora.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ChangePassword",ex);
            }

            return response;

        }

        public OutMenuType GetMenuType( string user )
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");           
            var ora = new OracleServer(connectionString);
            OutMenuType response = new OutMenuType();
            string command = string.Empty;

            try
            {
                command = string.Format( "SELECT nvl(BBS_WFG_V_TIPO_MENU.TIPO_MENU,0) as menu FROM BBS_WFG_V_TIPO_MENU WHERE BBS_WFG_V_TIPO_MENU.cedula = '{0}' ", user);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    response.menuType = DBNull.Value.Equals(rdr["menu"]) ? 0 : int.Parse(rdr["menu"].ToString());           
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("AuthenticationDAO.GetMenuType", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
