using System;
using Helper;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Entities;


namespace DAO
{
    public class AuthorizationDAO
    {
        /*public Response ValidateAccess(InValidateAccess input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            

            try
            {
                var ora = new OracleServer(connectionString);

                var pi_Company = new OracleParameter("fa_empresa", OracleDbType.Double, input.company, ParameterDirection.Input);
                var pi_User = new OracleParameter("fa_usuario", OracleDbType.Varchar2, input.userID, ParameterDirection.Input);
                var pi_Funcionalidad = new OracleParameter("fa_funcionalidad_nombre", OracleDbType.Varchar2, input.controlName, ParameterDirection.Input);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(pi_Company);
                ora.AddParameter(pi_User);
                ora.AddParameter(pi_Funcionalidad);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_PORTAL_F_VALIDA_PERMISO");

                input.OutValidateUser.errorCode = ora.GetParameter("fa_Error").ToString();
                input.OutValidateUser.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

                ora.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("",ex);
            }

            return input;
        }*/
    }
}
