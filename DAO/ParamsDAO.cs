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
    public class ParamsDAO
    {
        /* Dependencias*/
        public OutParamDependencia getDependencias()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDependencia response = new OutParamDependencia();
            var ora = new OracleServer(connectionString);

            ParamDependecia dependencias;
            List<ParamDependecia> list = new List<ParamDependecia>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM BB_LIB_CONVENIO_PAGADURIA ORDER BY CODIGO_PAGADURIA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dependencias = new ParamDependecia();
                    dependencias.secuencia = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA"].ToString());
                    dependencias.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    dependencias.estado = DBNull.Value.Equals(rdr["ESTADO"]) ? "" : (rdr["ESTADO"].ToString() == "A" ? "ACTIVO" : "INACTIVO");
                    list.Add(dependencias);
                }
                rdr.Close();
                response.ListDependencias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDependencias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDependencia getDependenciasActivas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDependencia response = new OutParamDependencia();
            var ora = new OracleServer(connectionString);

            ParamDependecia dependencias;
            List<ParamDependecia> list = new List<ParamDependecia>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM BB_LIB_CONVENIO_PAGADURIA WHERE ESTADO = 'A' ORDER BY CODIGO_PAGADURIA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dependencias = new ParamDependecia();
                    dependencias.secuencia = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA"].ToString());
                    dependencias.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    list.Add(dependencias);
                }
                rdr.Close();
                response.ListDependencias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDependencias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        //dias Expiracion
        public Response saveDiasExp(double dias)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            Response response = new Response();
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "update dlg_param_dias_exp ";
                command += string.Format("set dias = {0}", dias);
                ora.ExecuteCommand(command);
                response.errorCode = "200";
                response.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveDiasExp", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDiasExp getIdDiasExp(double dias)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDiasExp response = new OutParamDiasExp();
            var ora = new OracleServer(connectionString);

            ParamDiasExp dia;
            List<ParamDiasExp> list = new List<ParamDiasExp>();
            string command = string.Empty;
            try
            {
                command = "SELECT * from dlg_param_dias_exp ";
                command += string.Format("WHERE Dias = {0}", dias);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dia = new ParamDiasExp();
                    dia.dias = DBNull.Value.Equals(rdr["DIAS"]) ? 0 : double.Parse(rdr["DIAS"].ToString());
                    list.Add(dia);
                }
                rdr.Close();
                response.ListDias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdDiasExp", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDiasExp getDiasExp()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDiasExp response = new OutParamDiasExp();
            var ora = new OracleServer(connectionString);

            ParamDiasExp dia;
            List<ParamDiasExp> list = new List<ParamDiasExp>();
            string command = string.Empty;
            try
            {
                command = "SELECT * from dlg_param_dias_exp";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dia = new ParamDiasExp();
                    dia.dias = DBNull.Value.Equals(rdr["DIAS"]) ? 0 : double.Parse(rdr["DIAS"].ToString());
                    list.Add(dia);
                }
                rdr.Close();
                response.ListDias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDiasExp", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        //Dependencias
        public Response saveDependencia(double codigo_dep, string dependencia, string estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_dependencia = new OracleParameter("fa_codigo_dependencia", OracleDbType.Double, codigo_dep, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_nom_dependencia", OracleDbType.Varchar2, dependencia, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_dependencia);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_estado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_depen");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updDependencia(double secuencia, string dependencia, string estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_dependencia", OracleDbType.Varchar2, dependencia, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_estado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_depen");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteDependencia(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_depen");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDependencia loadDependencia(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDependencia response = new OutParamDependencia();
            var ora = new OracleServer(connectionString);

            ParamDependecia dependencias = new ParamDependecia();
            List<ParamDependecia> list = new List<ParamDependecia>();
            string command = string.Empty;
            try
            {
                command = "SELECT CODIGO_PAGADURIA, NOMBRE_PAGADURIA, ESTADO FROM BB_LIB_CONVENIO_PAGADURIA ";
                command += string.Format("WHERE CODIGO_PAGADURIA = {0} ORDER BY CODIGO_PAGADURIA", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dependencias = new ParamDependecia();
                    dependencias.secuencia = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA"].ToString());
                    dependencias.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    dependencias.estado = DBNull.Value.Equals(rdr["ESTADO"]) ? "" : rdr["ESTADO"].ToString();
                    list.Add(dependencias);
                }
                rdr.Close();
                response.ListDependencias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.loadDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Tipos de Solicitud */
        public OutParamTipo getTipoSolicitud()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipo response = new OutParamTipo();
            var ora = new OracleServer(connectionString);

            ParamTipoSolicitud tipoSolicitud;
            List<ParamTipoSolicitud> list = new List<ParamTipoSolicitud>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_tipo_solicitud ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    tipoSolicitud = new ParamTipoSolicitud();
                    tipoSolicitud.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    tipoSolicitud.tipoSolicitud = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();
                    list.Add(tipoSolicitud);
                }
                rdr.Close();
                response.ListTipoSolicitud = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveTipoSolicitud(string tipoSolicitud)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_tipo = new OracleParameter("fa_tipo", OracleDbType.Varchar2, tipoSolicitud, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_tipo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_tipo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updTipoSolicitud(double secuencia, string tipoSolicitud)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_tipo = new OracleParameter("fa_tipo", OracleDbType.Varchar2, tipoSolicitud, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_tipo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_tipo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteTipoSolicitud(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_tipo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamTipo getIdTipoSolicitud(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipo response = new OutParamTipo();
            var ora = new OracleServer(connectionString);

            ParamTipoSolicitud dependencias = new ParamTipoSolicitud();
            List<ParamTipoSolicitud> list = new List<ParamTipoSolicitud>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, TIPO FROM dlg_param_tipo_solicitud ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dependencias = new ParamTipoSolicitud();
                    dependencias.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    dependencias.tipoSolicitud = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();
                    list.Add(dependencias);
                }
                rdr.Close();
                response.ListTipoSolicitud = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Periodos */
        public OutParamPeriodos getPeriodos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPeriodos response = new OutParamPeriodos();
            var ora = new OracleServer(connectionString);

            ParamPeriodos periodosAplicables;
            List<ParamPeriodos> list = new List<ParamPeriodos>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_periodos_aplicables ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    periodosAplicables = new ParamPeriodos();
                    periodosAplicables.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    periodosAplicables.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    list.Add(periodosAplicables);
                }
                rdr.Close();
                response.ListPeriodos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPeriodos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response savePeriodos(string periodo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_perido = new OracleParameter("fa_periodo", OracleDbType.Varchar2, periodo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_perido);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_periodos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savePeriodos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updPeriodos(double secuencia, string periodo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_periodo = new OracleParameter("fa_periodo", OracleDbType.Varchar2, periodo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_periodo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_periodos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updPeriodos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deletePeridos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_periodos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deletePeridos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPeriodos getIdPeriodos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPeriodos response = new OutParamPeriodos();
            var ora = new OracleServer(connectionString);

            ParamPeriodos dependencias = new ParamPeriodos();
            List<ParamPeriodos> list = new List<ParamPeriodos>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PERIODO FROM dlg_param_periodos_aplicables ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    dependencias = new ParamPeriodos();
                    dependencias.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    dependencias.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    list.Add(dependencias);
                }
                rdr.Close();
                response.ListPeriodos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdPeriodos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*  Plazos */
        public OutParamPlazos getPlazos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPlazos response = new OutParamPlazos();
            var ora = new OracleServer(connectionString);

            ParamPlazos plazos;
            List<ParamPlazos> list = new List<ParamPlazos>();
            string command = string.Empty;
            try
            {
                command = "SELECT pl.SECUENCIA, p.PERIODO, pl.PLAZO FROM dlg_param_plazos_aplicables pl, dlg_param_periodos_aplicables p ";
                command += " WHERE p.secuencia = pl.periodo ORDER BY pl.SECUENCIA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    plazos = new ParamPlazos();
                    plazos.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    plazos.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    plazos.plazos = DBNull.Value.Equals(rdr["PLAZO"]) ? "" : rdr["PLAZO"].ToString();
                    list.Add(plazos);
                }
                rdr.Close();
                response.ListPlazos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPlazos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPlazos getIdPlazo(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPlazos response = new OutParamPlazos();
            var ora = new OracleServer(connectionString);

            ParamPlazos plazos;
            List<ParamPlazos> list = new List<ParamPlazos>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PERIODO, PLAZO FROM dlg_param_plazos_aplicables";
                command += string.Format(" WHERE secuencia = {0}", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    plazos = new ParamPlazos();
                    plazos.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    plazos.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    plazos.plazos = DBNull.Value.Equals(rdr["PLAZO"]) ? "" : rdr["PLAZO"].ToString();
                    list.Add(plazos);
                }
                rdr.Close();
                response.ListPlazos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdPlazo", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPlazosPeriodos getPlazosPeriodo(double periodo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPlazosPeriodos response = new OutParamPlazosPeriodos();
            var ora = new OracleServer(connectionString);

            ParamPlazosPeriodos plazos;
            List<ParamPlazosPeriodos> list = new List<ParamPlazosPeriodos>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PLAZO FROM dlg_param_plazos_aplicables ";
                command += string.Format("WHERE PERIODO = {0} ORDER BY SECUENCIA", periodo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    plazos = new ParamPlazosPeriodos();
                    plazos.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    plazos.plazos = DBNull.Value.Equals(rdr["PLAZO"]) ? "" : rdr["PLAZO"].ToString();
                    list.Add(plazos);
                }
                rdr.Close();
                response.ListPlazoPeriodo = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPlazosPeriodo", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response savePlazo(double? periodo, string plazo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_plazo = new OracleParameter("fa_plazos", OracleDbType.Varchar2, plazo, ParameterDirection.Input);
            var pi_perido = new OracleParameter("fa_periodo", OracleDbType.Double, periodo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_plazo);
                ora.AddParameter(pi_perido);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_plazo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savePeriodos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updPlazo(double secuencia, string plazo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_plazo = new OracleParameter("fa_plazo", OracleDbType.Varchar2, plazo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_plazo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_plazo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updPlazo", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteplazo(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_plazo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteplazo", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* PUESTOS */
        public OutParamPuesto getPuestos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPuesto response = new OutParamPuesto();
            var ora = new OracleServer(connectionString);

            ParamPuesto puesto;
            List<ParamPuesto> list = new List<ParamPuesto>();
            string command = string.Empty;
            try
            {
                command = "SELECT p.SECUENCIA, s.SECTOR, p.PUESTO FROM dlg_param_puesto_sector p, dlg_param_sector s ";
                command += " WHERE s.secuencia = p.sector ORDER BY p.SECUENCIA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    puesto = new ParamPuesto();
                    puesto.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    puesto.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    puesto.puestoSector = DBNull.Value.Equals(rdr["PUESTO"]) ? "" : rdr["PUESTO"].ToString();
                    list.Add(puesto);
                }
                rdr.Close();
                response.ListPuestos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPuestos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPuesto getIdPuestos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPuesto response = new OutParamPuesto();
            var ora = new OracleServer(connectionString);

            ParamPuesto puesto;
            List<ParamPuesto> list = new List<ParamPuesto>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, SECTOR, PUESTO FROM dlg_param_puesto_sector ";
                command += string.Format(" WHERE secuencia = {0}", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    puesto = new ParamPuesto();
                    puesto.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    puesto.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    puesto.puestoSector = DBNull.Value.Equals(rdr["PUESTO"]) ? "" : rdr["PUESTO"].ToString();
                    list.Add(puesto);
                }
                rdr.Close();
                response.ListPuestos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPuestos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPuestoSector getPuestosSector(double sector)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPuestoSector response = new OutParamPuestoSector();
            var ora = new OracleServer(connectionString);

            ParamPuestoSector puestoSector;
            List<ParamPuestoSector> list = new List<ParamPuestoSector>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PUESTO FROM dlg_param_puesto_sector ";
                command += string.Format("WHERE SECTOR = {0} ORDER BY SECUENCIA", sector);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    puestoSector = new ParamPuestoSector();
                    puestoSector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    puestoSector.puestoSector = DBNull.Value.Equals(rdr["PUESTO"]) ? "" : rdr["PUESTO"].ToString();
                    list.Add(puestoSector);
                }
                rdr.Close();
                response.ListPuestoSector = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPuestosSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response savePuesto(double sector, string puesto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_puesto = new OracleParameter("fa_plazos", OracleDbType.Varchar2, puesto, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_periodo", OracleDbType.Double, sector, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_puesto);
                ora.AddParameter(pi_sector);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_puesto");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savePuesto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updPuesto(double secuencia, string puesto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_puesto = new OracleParameter("fa_puesto", OracleDbType.Varchar2, puesto, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_puesto);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_puesto");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updPuesto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deletePuesto(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_puesto");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deletePuesto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Destino */
        public OutParamDestinoCred getDestinoCredito()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDestinoCred response = new OutParamDestinoCred();
            var ora = new OracleServer(connectionString);

            ParamDestinoCred destinoCredito;
            List<ParamDestinoCred> list = new List<ParamDestinoCred>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_destino_credito ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    destinoCredito = new ParamDestinoCred();
                    destinoCredito.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    destinoCredito.destinoCredito = DBNull.Value.Equals(rdr["DESTINO_CREDITO"]) ? "" : rdr["DESTINO_CREDITO"].ToString();
                    list.Add(destinoCredito);
                }
                rdr.Close();
                response.ListDestinoCred = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDependencias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response savedestinoCredito(string destinoCredito)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_destinoCredito = new OracleParameter("fa_destino", OracleDbType.Varchar2, destinoCredito, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_destinoCredito);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_destino");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savedestinoCredito", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updDestinoCredito(double secuencia, string destinoCredito)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_destinoCredito = new OracleParameter("fa_destino", OracleDbType.Varchar2, destinoCredito, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_destinoCredito);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_destino");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updDestinoCredito", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteDestinoCredito(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_destino");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteDestinoCredito", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDestinoCred getIdDestinoCredito(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDestinoCred response = new OutParamDestinoCred();
            var ora = new OracleServer(connectionString);

            ParamDestinoCred destinoCredito;
            List<ParamDestinoCred> list = new List<ParamDestinoCred>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, DESTINO_CREDITO FROM dlg_param_destino_credito ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    destinoCredito = new ParamDestinoCred();
                    destinoCredito.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    destinoCredito.destinoCredito = DBNull.Value.Equals(rdr["DESTINO_CREDITO"]) ? "" : rdr["DESTINO_CREDITO"].ToString();
                    list.Add(destinoCredito);
                }
                rdr.Close();
                response.ListDestinoCred = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdDestinoCredito", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Empresa Telefonica */
        public OutParamEmpresatelefonica getEmpresaTelefonica()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamEmpresatelefonica response = new OutParamEmpresatelefonica();
            var ora = new OracleServer(connectionString);

            ParamEmpresaTelefonica empresatelefonica;
            List<ParamEmpresaTelefonica> list = new List<ParamEmpresaTelefonica>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_empresa_telefonica ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    empresatelefonica = new ParamEmpresaTelefonica();
                    empresatelefonica.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    empresatelefonica.empresa = DBNull.Value.Equals(rdr["EMPRESA"]) ? "" : rdr["EMPRESA"].ToString();
                    list.Add(empresatelefonica);
                }
                rdr.Close();
                response.ListEmpresas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.OutParamEmpresatelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveEmpresaTelefonica(string empresaTelefonica)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_empresaTelefonica = new OracleParameter("fa_empresa_telef", OracleDbType.Varchar2, empresaTelefonica, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_empresaTelefonica);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_emp_telef");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveEmpresaTelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updEmpresaTelefonica(double secuencia, string empresaTelefonica)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_empresaTelefonica = new OracleParameter("fa_empresa_telef", OracleDbType.Varchar2, empresaTelefonica, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_empresaTelefonica);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_emp_telef");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updEmpresaTelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteEmpresaTelefonica(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_emp_telef");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteEmpresaTelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamEmpresatelefonica getIdEmpresaTelefonica(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamEmpresatelefonica response = new OutParamEmpresatelefonica();
            var ora = new OracleServer(connectionString);

            ParamEmpresaTelefonica destinoCredito;
            List<ParamEmpresaTelefonica> list = new List<ParamEmpresaTelefonica>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, EMPRESA FROM dlg_param_empresa_telefonica ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    destinoCredito = new ParamEmpresaTelefonica();
                    destinoCredito.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    destinoCredito.empresa = DBNull.Value.Equals(rdr["EMPRESA"]) ? "" : rdr["EMPRESA"].ToString();
                    list.Add(destinoCredito);
                }
                rdr.Close();
                response.ListEmpresas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdEmpresaTelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Estado Civil*/
        public OutParamEstadoCivil getEstadoCivil()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamEstadoCivil response = new OutParamEstadoCivil();
            var ora = new OracleServer(connectionString);

            ParamEstadoCivil estadoCivil;
            List<ParamEstadoCivil> list = new List<ParamEstadoCivil>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_estado_civil ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    estadoCivil = new ParamEstadoCivil();
                    estadoCivil.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    estadoCivil.estadoCivil = DBNull.Value.Equals(rdr["ESTADO_CIVIL"]) ? "" : rdr["ESTADO_CIVIL"].ToString();
                    list.Add(estadoCivil);
                }
                rdr.Close();
                response.ListEstados = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.OutParamEmpresatelefonica", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveEstadoCivil(string estadoCivil)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_estadoCivil = new OracleParameter("fa_estado_civil", OracleDbType.Varchar2, estadoCivil, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_estadoCivil);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_estado");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveEstadoCivil", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updEstadoCivil(double secuencia, string estadoCivil)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_estadoCivil = new OracleParameter("fa_estado_civil", OracleDbType.Varchar2, estadoCivil, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_estadoCivil);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_estado");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updEstadoCivil", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteEstadoCivil(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_estado");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteEstadoCivil", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamEstadoCivil getIdEstadoCivil(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamEstadoCivil response = new OutParamEstadoCivil();
            var ora = new OracleServer(connectionString);

            ParamEstadoCivil estadoCivil;
            List<ParamEstadoCivil> list = new List<ParamEstadoCivil>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, ESTADO_CIVIL FROM dlg_param_estado_civil ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    estadoCivil = new ParamEstadoCivil();
                    estadoCivil.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    estadoCivil.estadoCivil = DBNull.Value.Equals(rdr["ESTADO_CIVIL"]) ? "" : rdr["ESTADO_CIVIL"].ToString();
                    list.Add(estadoCivil);
                }
                rdr.Close();
                response.ListEstados = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdEstadoCivil", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Identificacion */
        public OutParamTipoIdentificacion getTiposIdentificacion()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipoIdentificacion response = new OutParamTipoIdentificacion();
            var ora = new OracleServer(connectionString);

            ParamTipoidentificacion identificacion;
            List<ParamTipoidentificacion> list = new List<ParamTipoidentificacion>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dfptpr13 ORDER BY TIPO_PERSONA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    identificacion = new ParamTipoidentificacion();
                    identificacion.secuencia = DBNull.Value.Equals(rdr["TIPO_PERSONA"]) ? 0 : double.Parse(rdr["TIPO_PERSONA"].ToString());
                    identificacion.tipoIdentificacion = DBNull.Value.Equals(rdr["NOMBRE_TIPO"]) ? "" : rdr["NOMBRE_TIPO"].ToString();
                    list.Add(identificacion);
                }
                rdr.Close();
                response.ListTipoIdentificacion = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getTiposIdentificacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveTiposIdentificacion(string identificacion)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_identificacion = new OracleParameter("fa_identificaion", OracleDbType.Varchar2, identificacion, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_identificacion);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_ident");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveTiposIdentificacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updTiposIdetificacion(double secuencia, string identificacion)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_identificacion = new OracleParameter("fa_identificaion", OracleDbType.Varchar2, identificacion, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_identificacion);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_ident");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updTiposIdetificacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteTiposIdentificacion(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_ident");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteTiposIdentificacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamTipoIdentificacion getIdTipoIdentificacion(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipoIdentificacion response = new OutParamTipoIdentificacion();
            var ora = new OracleServer(connectionString);

            ParamTipoidentificacion identificacion;
            List<ParamTipoidentificacion> list = new List<ParamTipoidentificacion>();
            string command = string.Empty;
            try
            {
                command = "SELECT TIPO_PERSONA, NOMBRE_TIPO FROM dfptpr13 ";
                command += string.Format("WHERE TIPO_PERSONA = {0} ORDER BY TIPO_PERSONA", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    identificacion = new ParamTipoidentificacion();
                    identificacion.secuencia = DBNull.Value.Equals(rdr["TIPO_PERSONA"]) ? 0 : double.Parse(rdr["TIPO_PERSONA"].ToString());
                    identificacion.tipoIdentificacion = DBNull.Value.Equals(rdr["NOMBRE_TIPO"]) ? "" : rdr["NOMBRE_TIPO"].ToString();
                    list.Add(identificacion);
                }
                rdr.Close();
                response.ListTipoIdentificacion = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdTipoIdentificacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Ingreso */
        public OutParamIngresos getTiposIngresos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamIngresos response = new OutParamIngresos();
            var ora = new OracleServer(connectionString);

            ParamIngresos identificacion;
            List<ParamIngresos> list = new List<ParamIngresos>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_ingreso_neto ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    identificacion = new ParamIngresos();
                    identificacion.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    identificacion.ingresoNeto = DBNull.Value.Equals(rdr["SALARIO"]) ? "" : rdr["SALARIO"].ToString();
                    list.Add(identificacion);
                }
                rdr.Close();
                response.ListIngresos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getTiposIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveIngresos(string ingresos)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_ingresos = new OracleParameter("fa_ingreso", OracleDbType.Varchar2, ingresos, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_ingresos);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_ingreso");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updIngresos(double secuencia, string ingresos)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_ingresos = new OracleParameter("fa_ingreso", OracleDbType.Varchar2, ingresos, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_ingresos);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_ingreso");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteIngresos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_ingreso");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamIngresos getIdIngresos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamIngresos response = new OutParamIngresos();
            var ora = new OracleServer(connectionString);

            ParamIngresos identificacion;
            List<ParamIngresos> list = new List<ParamIngresos>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, SALARIO FROM dlg_param_ingreso_neto ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    identificacion = new ParamIngresos();
                    identificacion.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    identificacion.ingresoNeto = DBNull.Value.Equals(rdr["SALARIO"]) ? "" : rdr["SALARIO"].ToString();
                    list.Add(identificacion);
                }
                rdr.Close();
                response.ListIngresos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Medios de Disposicion */
        public OutParamMedios getMedios()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamMedios response = new OutParamMedios();
            var ora = new OracleServer(connectionString);

            ParamMedios medios;
            List<ParamMedios> list = new List<ParamMedios>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_medio_disposicion ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    medios = new ParamMedios();
                    medios.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    medios.medioDisposicion = DBNull.Value.Equals(rdr["MEDIO"]) ? "" : rdr["MEDIO"].ToString();
                    list.Add(medios);
                }
                rdr.Close();
                response.ListMedios = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getMedios", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveMedios(string medios)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_medios = new OracleParameter("fa_medio_disp", OracleDbType.Varchar2, medios, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_medios);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_medio");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveMedios", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updMedios(double secuencia, string medios)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_medios = new OracleParameter("fa_medio_disp", OracleDbType.Varchar2, medios, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_medios);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_medio");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updMedios", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteMedios(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_medio");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteMedios", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamMedios getIdMedios(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamMedios response = new OutParamMedios();
            var ora = new OracleServer(connectionString);

            ParamMedios medios;
            List<ParamMedios> list = new List<ParamMedios>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, MEDIO FROM dlg_param_medio_disposicion ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    medios = new ParamMedios();
                    medios.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    medios.medioDisposicion = DBNull.Value.Equals(rdr["MEDIO"]) ? "" : rdr["MEDIO"].ToString();
                    list.Add(medios);
                }
                rdr.Close();
                response.ListMedios = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Nomina*/
        public OutParamTipoNomina getNomina()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipoNomina response = new OutParamTipoNomina();
            var ora = new OracleServer(connectionString);

            ParamTipoNomina nomina;
            List<ParamTipoNomina> list = new List<ParamTipoNomina>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_tipo_nomina ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    nomina = new ParamTipoNomina();
                    nomina.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    nomina.tipoNomina = DBNull.Value.Equals(rdr["TIPO_NOMINA"]) ? "" : rdr["TIPO_NOMINA"].ToString();
                    list.Add(nomina);
                }
                rdr.Close();
                response.ListTipoNomina = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getNomina", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveNomina(string nomina)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_nomina = new OracleParameter("fa_tipo_nomina", OracleDbType.Varchar2, nomina, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_nomina);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_nomina");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveNomina", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updNomina(double secuencia, string nomina)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_nomina = new OracleParameter("fa_tipo_nomina", OracleDbType.Varchar2, nomina, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_nomina);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_nomina");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updNomina", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteNomina(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_nomina");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteNomina", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamTipoNomina getIdNomina(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipoNomina response = new OutParamTipoNomina();
            var ora = new OracleServer(connectionString);

            ParamTipoNomina nomina;
            List<ParamTipoNomina> list = new List<ParamTipoNomina>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, TIPO_NOMINA FROM dlg_param_tipo_nomina ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    nomina = new ParamTipoNomina();
                    nomina.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    nomina.tipoNomina = DBNull.Value.Equals(rdr["TIPO_NOMINA"]) ? "" : rdr["TIPO_NOMINA"].ToString();
                    list.Add(nomina);
                }
                rdr.Close();
                response.ListTipoNomina = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdIngresos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Quincena Descuento */
        public double quincenaDescuento(string quincena)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            double response = 0;
            var ora = new OracleServer(connectionString);

            List<ParamQuincenaDscto> list = new List<ParamQuincenaDscto>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA FROM dlg_param_quincena_dscto ";
                command += string.Format("WHERE UPPER(QUINCENA_DSCTO) LIKE '{0}%'", quincena);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    response = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamQuincenaDscto getQuincenaDscto()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamQuincenaDscto response = new OutParamQuincenaDscto();
            var ora = new OracleServer(connectionString);

            ParamQuincenaDscto quincena;
            List<ParamQuincenaDscto> list = new List<ParamQuincenaDscto>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_quincena_dscto ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    quincena = new ParamQuincenaDscto();
                    quincena.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    quincena.QuincenaDscto = DBNull.Value.Equals(rdr["QUINCENA_DSCTO"]) ? "" : rdr["QUINCENA_DSCTO"].ToString();
                    list.Add(quincena);
                }
                rdr.Close();
                response.ListQuincenaDscto = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveQuincenaDscto(string quincena)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_quincena = new OracleParameter("fa_quincena_dscto", OracleDbType.Varchar2, quincena, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_quincena);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_quincena");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updQuincenaDscto(double secuencia, string quincena)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_quincena = new OracleParameter("fa_quincena_dscto", OracleDbType.Varchar2, quincena, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_quincena);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_quincena");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteQuincenaDscto(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_quincena");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamQuincenaDscto getIdQuincenaDscto(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamQuincenaDscto response = new OutParamQuincenaDscto();
            var ora = new OracleServer(connectionString);

            ParamQuincenaDscto quincena;
            List<ParamQuincenaDscto> list = new List<ParamQuincenaDscto>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, QUINCENA_DSCTO FROM dlg_param_quincena_dscto ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    quincena = new ParamQuincenaDscto();
                    quincena.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    quincena.QuincenaDscto = DBNull.Value.Equals(rdr["QUINCENA_DSCTO"]) ? "" : rdr["QUINCENA_DSCTO"].ToString();
                    list.Add(quincena);
                }
                rdr.Close();
                response.ListQuincenaDscto = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdQuincenaDscto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Sector */
        public OutParamSector getSector()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSector response = new OutParamSector();
            var ora = new OracleServer(connectionString);

            ParamSector sector;
            List<ParamSector> list = new List<ParamSector>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_sector ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSector();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSector = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveSector(string sector)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_sector);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_sector");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updSector(double secuencia, string sector)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_sector);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_sector");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteSector(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_sector");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamSector getIdSector(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSector response = new OutParamSector();
            var ora = new OracleServer(connectionString);

            ParamSector sector;
            List<ParamSector> list = new List<ParamSector>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, SECTOR FROM dlg_param_sector ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSector();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSector = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        /* SectorGuias */
        public OutParamSectorGuias getSectorGuias()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorGuias response = new OutParamSectorGuias();
            var ora = new OracleServer(connectionString);

            ParamSectorGuias sector;
            List<ParamSectorGuias> list = new List<ParamSectorGuias>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_sectorGuias ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSectorGuias();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    sector.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSectorGuias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveSectorGuias(string sector,double estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var pi_indEstado = new OracleParameter("fa_estado", OracleDbType.Double, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_indEstado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_SecGuias");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updSectorGuias(double secuencia, string sector,double estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var pi_indEstado = new OracleParameter("fa_estado", OracleDbType.Double, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_indEstado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_SecGuias");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteSectorGuias(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_SecGuias");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamSectorGuias getIdSectorGuias(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorGuias response = new OutParamSectorGuias();
            var ora = new OracleServer(connectionString);

            ParamSectorGuias sector;
            List<ParamSectorGuias> list = new List<ParamSectorGuias>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_sectorGuias ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSectorGuias();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    sector.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSectorGuias = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* SectorTablas */
        public OutParamSectorTablas getSectorTablas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorTablas response = new OutParamSectorTablas();
            var ora = new OracleServer(connectionString);

            ParamSectorTablas sector;
            List<ParamSectorTablas> list = new List<ParamSectorTablas>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_sectorTablas ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSectorTablas();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    sector.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSectorTablas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveSectorTablas(string sector, double estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var pi_indEstado = new OracleParameter("fa_estado", OracleDbType.Double, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_indEstado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_SecTablas");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updSectorTablas(double secuencia, string sector, double estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, sector, ParameterDirection.Input);
            var pi_indEstado = new OracleParameter("fa_estado", OracleDbType.Double, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_indEstado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_SecTablas");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteSectorTablas(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_SecTablas");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamSectorTablas getIdSectorTablas(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorTablas response = new OutParamSectorTablas();
            var ora = new OracleServer(connectionString);

            ParamSectorTablas sector;
            List<ParamSectorTablas> list = new List<ParamSectorTablas>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_sectorTablas ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSectorTablas();
                    sector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    sector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                    sector.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSectorTablas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Sucursal */
        public OutParamSucursales getSucursal()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSucursales response = new OutParamSucursales();
            var ora = new OracleServer(connectionString);

            ParamSucursales sucursal;
            List<ParamSucursales> list = new List<ParamSucursales>();
            string command = string.Empty;
            try
            {
                command = "SELECT CODIGO_SUCURSAL,NOMBRE_SUCURSAL FROM BBS_LIQCOM2_SUCURSALES ORDER BY CODIGO_SUCURSAL";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sucursal = new ParamSucursales();
                    sucursal.secuencia = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    sucursal.sucursal = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? "" : rdr["NOMBRE_SUCURSAL"].ToString();
                    list.Add(sucursal);
                }
                rdr.Close();
                response.ListSucursales = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getSucursal", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveSucursal(string sucursal, string identificador, double tipo, string division, string region, string emailCoordinador, string emailAuxiliar)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_sucursal = new OracleParameter("fa_sucursal", OracleDbType.Varchar2, sucursal, ParameterDirection.Input);
            var pi_identificador = new OracleParameter("fa_id_sucursal", OracleDbType.Varchar2, identificador, ParameterDirection.Input);
            var pi_tipo = new OracleParameter("fa_tipo_sucursal", OracleDbType.Double, tipo, ParameterDirection.Input);
            var pi_division = new OracleParameter("fa_division", OracleDbType.Varchar2, division, ParameterDirection.Input);
            var pi_region = new OracleParameter("fa_region", OracleDbType.Varchar2, region, ParameterDirection.Input);
            var pi_email_Aux = new OracleParameter("fa_email_auxiliar", OracleDbType.Varchar2, emailAuxiliar, ParameterDirection.Input);
            var pi_email_Cooor = new OracleParameter("fa_email_coordinador", OracleDbType.Varchar2, emailCoordinador, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_sucursal);
                ora.AddParameter(pi_identificador);
                ora.AddParameter(pi_tipo);
                ora.AddParameter(pi_division);
                ora.AddParameter(pi_region);
                ora.AddParameter(pi_email_Aux);
                ora.AddParameter(pi_email_Cooor);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_sucursal");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updSucursal(double secuencia, string sucursal, double tipo_sucursal, string emailCoordinador, string emailAuxiliar)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_sucursal = new OracleParameter("fa_sucursal", OracleDbType.Varchar2, sucursal, ParameterDirection.Input);
            var pi_tipo_sucursal = new OracleParameter("fa_tipo_sucursal", OracleDbType.Double, tipo_sucursal, ParameterDirection.Input);
            var pi_email_Aux = new OracleParameter("fa_email_auxiliar", OracleDbType.Varchar2, emailAuxiliar, ParameterDirection.Input);
            var pi_email_Cooor = new OracleParameter("fa_email_coordinador", OracleDbType.Varchar2, emailCoordinador, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_sucursal);
                ora.AddParameter(pi_tipo_sucursal);
                ora.AddParameter(pi_email_Cooor);
                ora.AddParameter(pi_email_Aux);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_sucursal");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updSucursal", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteSucursal(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_sucursal");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteSucursal", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamSucursales getIdSucursal(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSucursales response = new OutParamSucursales();
            var ora = new OracleServer(connectionString);

            ParamSucursales sector;
            List<ParamSucursales> list = new List<ParamSucursales>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM BBS_LIQCOM2_SUCURSALES ";
                command += string.Format("WHERE CODIGO_SUCURSAL = {0} ORDER BY CODIGO_SUCURSAL", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    sector = new ParamSucursales();
                    sector.secuencia = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    sector.sucursal = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? "" : rdr["NOMBRE_SUCURSAL"].ToString();
                    sector.codigo_sucursal = DBNull.Value.Equals(rdr["ID_SUCURSAL"]) ? "" : rdr["ID_SUCURSAL"].ToString();
                    sector.region = DBNull.Value.Equals(rdr["ID_REGION"]) ? "" : rdr["ID_REGION"].ToString();
                    sector.division = DBNull.Value.Equals(rdr["ID_DIVISION"]) ? "" : rdr["ID_DIVISION"].ToString();
                    sector.tipo_sucursal = DBNull.Value.Equals(rdr["TIPO_SUCURSAL"]) ? "" : rdr["TIPO_SUCURSAL"].ToString();
                    sector.emailCoordinador = DBNull.Value.Equals(rdr["CORREO_COORDINADOR"]) ? "" : rdr["CORREO_COORDINADOR"].ToString();
                    sector.emailAuxiliar = DBNull.Value.Equals(rdr["CORREO_AUXILIAR"]) ? "" : rdr["CORREO_AUXILIAR"].ToString();
                    list.Add(sector);
                }
                rdr.Close();
                response.ListSucursales = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdSucursal", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Reca */
        public OutParamReca getReca()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamReca response = new OutParamReca();
            var ora = new OracleServer(connectionString);

            ParamReca reca;
            List<ParamReca> list = new List<ParamReca>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dlg_param_reca ORDER BY secuencia";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    reca = new ParamReca();
                    reca.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    reca.reca = DBNull.Value.Equals(rdr["RECA"]) ? "" : rdr["RECA"].ToString();
                    list.Add(reca);
                }
                rdr.Close();
                response.ListRecas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getReca", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveReca(string reca)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_reca = new OracleParameter("fa_reca", OracleDbType.Varchar2, reca, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_reca);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_reca");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveReca", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updReca(double secuencia, string reca)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_reca = new OracleParameter("fa_reca", OracleDbType.Varchar2, reca, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_reca);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_reca");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updReca", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteReca(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_reca");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteReca", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamReca getIdReca(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamReca response = new OutParamReca();
            var ora = new OracleServer(connectionString);

            ParamReca reca;
            List<ParamReca> list = new List<ParamReca>();
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, RECA FROM dlg_param_reca ";
                command += string.Format("WHERE SECUENCIA = {0} ORDER BY secuencia", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    reca = new ParamReca();
                    reca.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    reca.reca = DBNull.Value.Equals(rdr["RECA"]) ? "" : rdr["RECA"].ToString();
                    list.Add(reca);
                }
                rdr.Close();
                response.ListRecas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdReca", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*PRODUCTOS*/
        public OutParamProductos getProductos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamProductos response = new OutParamProductos();
            var ora = new OracleServer(connectionString);

            ParamProducto producto;
            List<ParamProducto> list = new List<ParamProducto>();
            string command = string.Empty;
            try
            {
                command = "SELECT p.LINEA_CREDITO, d.NOMBRE_PAGADURIA, p.DESCRIPCION_LIN, p.ESTADO_LINEA FROM DCPLIN01 p, BB_LIB_CONVENIO_PAGADURIA d ";
                command += " WHERE d.CODIGO_PAGADURIA = p.CODIGO_PAGADURIA ORDER BY p.LINEA_CREDITO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    producto = new ParamProducto();
                    producto.secuencia = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : double.Parse(rdr["LINEA_CREDITO"].ToString());
                    producto.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    producto.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    producto.estado = DBNull.Value.Equals(rdr["ESTADO_LINEA"]) ? "" : (rdr["ESTADO_LINEA"].ToString() == "A" ? "ACTIVO" : "INACTIVO");
                    list.Add(producto);
                }
                rdr.Close();
                response.ListProductos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getProductos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamProductos getIdProductos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamProductos response = new OutParamProductos();
            var ora = new OracleServer(connectionString);

            ParamProducto producto;
            List<ParamProducto> list = new List<ParamProducto>();
            string command = string.Empty;
            try
            {
                command = "SELECT LINEA_CREDITO, CODIGO_PAGADURIA, DESCRIPCION_LIN, ESTADO_LINEA FROM DCPLIN01";
                command += string.Format(" WHERE LINEA_CREDITO = {0}", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    producto = new ParamProducto();
                    producto.secuencia = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : double.Parse(rdr["LINEA_CREDITO"].ToString());
                    producto.dependencia = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? "" : rdr["CODIGO_PAGADURIA"].ToString();
                    producto.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    producto.estado = DBNull.Value.Equals(rdr["ESTADO_LINEA"]) ? "" : rdr["ESTADO_LINEA"].ToString();
                    list.Add(producto);
                }
                rdr.Close();
                response.ListProductos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdProductos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamProductoDependencia getProductosDependencia(double dependencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamProductoDependencia response = new OutParamProductoDependencia();
            var ora = new OracleServer(connectionString);

            ParamProductoDependencia productoDependencia;
            List<ParamProductoDependencia> list = new List<ParamProductoDependencia>();
            string command = string.Empty;
            try
            {
                command = "SELECT LINEA_CREDITO, DESCRIPCION_LIN FROM DCPLIN01 ";
                command += string.Format("WHERE ESTADO_LINEA = 'A' AND CODIGO_PAGADURIA = {0} ORDER BY LINEA_CREDITO", dependencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    productoDependencia = new ParamProductoDependencia();
                    productoDependencia.secuencia = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : double.Parse(rdr["LINEA_CREDITO"].ToString());
                    productoDependencia.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    list.Add(productoDependencia);
                }
                rdr.Close();
                response.ListProductosDependencia = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getProductosDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveProductos(double codigo_pro, double depeendencia, string producto, string estado )
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_producto = new OracleParameter("fa_secuencia", OracleDbType.Double, codigo_pro, ParameterDirection.Input);
            var pi_producto = new OracleParameter("fa_producto", OracleDbType.Varchar2, producto, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_dependencia", OracleDbType.Double, depeendencia, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_producto);
                ora.AddParameter(pi_producto);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_estado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_productos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveProductos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updProductos(double secuencia, string producto, string estado )
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var pi_plazo = new OracleParameter("fa_producto", OracleDbType.Varchar2, producto, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_plazo);
                ora.AddParameter(pi_estado);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_producto");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updProductos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteProductos(double secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Double, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_producto");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteProductos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Division*/
        public OutParamDivision getDivision()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDivision response = new OutParamDivision();
            var ora = new OracleServer(connectionString);

            ParamDivision division;
            List<ParamDivision> list = new List<ParamDivision>();
            string command = string.Empty;
            try
            {
                command = "SELECT ID_DIVISION, NOMBRE_DIVISION FROM  BBS_LIQCOM2_DIVISIONES ORDER BY CODIGO_DIVISION";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    division = new ParamDivision();
                    division.codigo_division = DBNull.Value.Equals(rdr["ID_DIVISION"]) ? "" : (rdr["ID_DIVISION"].ToString());
                    division.nombre_division = DBNull.Value.Equals(rdr["NOMBRE_DIVISION"]) ? "" : rdr["NOMBRE_DIVISION"].ToString();
                    list.Add(division);
                }
                rdr.Close();
                response.divisionList = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDivision", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Region*/
        public OutParamRegion getRegionDivision(string ID_DIVISION)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamRegion response = new OutParamRegion();
            var ora = new OracleServer(connectionString);

            ParamRegion region;
            List<ParamRegion> list = new List<ParamRegion>();
            string command = string.Empty;
            try
            {
                command = "SELECT ID_REGION, NOMBRE_REGION FROM  BBS_LIQCOM2_REGIONES ";
                command += string.Format("WHERE ID_DIVISION = '{0}' ORDER BY CODIGO_REGION", ID_DIVISION);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    region = new ParamRegion();
                    region.codigo_region = DBNull.Value.Equals(rdr["ID_REGION"]) ? "" : (rdr["ID_REGION"].ToString());
                    region.nombre_region = DBNull.Value.Equals(rdr["NOMBRE_REGION"]) ? "" : rdr["NOMBRE_REGION"].ToString();
                    list.Add(region);
                }
                rdr.Close();
                response.regionList = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getRegionDivision", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*TipoSucursal*/
        public OutParamTipoSucursal getTipoSucursal()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamTipoSucursal response = new OutParamTipoSucursal();
            var ora = new OracleServer(connectionString);

            ParamTipoSucursal tipoSucursal;
            List<ParamTipoSucursal> list = new List<ParamTipoSucursal>();
            string command = string.Empty;
            try
            {
                command = "SELECT CODIGO_TIPO_SUCURSAL, NOMBRE_TIPO_SUCURSAL FROM  BBS_LIQCOM2_TIPO_SUCURSAL ORDER BY CODIGO_TIPO_SUCURSAL";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    tipoSucursal = new ParamTipoSucursal();
                    tipoSucursal.codigo_tipo = DBNull.Value.Equals(rdr["CODIGO_TIPO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_TIPO_SUCURSAL"].ToString());
                    tipoSucursal.nombre_tipo = DBNull.Value.Equals(rdr["NOMBRE_TIPO_SUCURSAL"]) ? "" : rdr["NOMBRE_TIPO_SUCURSAL"].ToString();
                    list.Add(tipoSucursal);
                }
                rdr.Close();
                response.TipoSucursalList = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getRegionDivision", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Buscar Entidad para carga de CSV*/
        public ParamDependecia findDependencia(string dependencia)
        {
            ParamDependecia objDependencia = new ParamDependecia();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, DEPENDENCIA FROM DLG_PARAM_DEPENDENCIAS ";
                command += string.Format("WHERE UPPER(DEPENDENCIA) LIKE UPPER('{0}%')", dependencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    objDependencia = new ParamDependecia();
                    objDependencia.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    objDependencia.dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? "" : (rdr["DEPENDENCIA"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.findDependencia", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return objDependencia;
        }
        public ParamPeriodos findPeriodo(string periodo)
        {
            ParamPeriodos objPeriodo = new ParamPeriodos();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PERIODO FROM DLG_PARAM_PERIODOS_APLICABLES ";
                command += string.Format("WHERE UPPER(PERIODO) LIKE UPPER('{0}%')", periodo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    objPeriodo = new ParamPeriodos();
                    objPeriodo.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    objPeriodo.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.findPeriodo", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return objPeriodo;
        }
        public ParamSector findSector(string sector)
        {
            ParamSector objSector = new ParamSector();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, SECTOR FROM DLG_PARAM_SECTOR ";
                command += string.Format("WHERE UPPER(SECTOR) LIKE UPPER('{0}%')", sector);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    objSector = new ParamSector();
                    objSector.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    objSector.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.findSector", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return objSector;
        }
        public ParamProducto findProducto(string producto, double dependencia)
        {
            ParamProducto objProducto = new ParamProducto();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, PRODUCTO FROM dlg_param_productos_dependencia ";
                command += string.Format("WHERE UPPER(PRODUCTO) LIKE UPPER('{0}%') AND DEPENDENCIA = {1}", producto, dependencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    objProducto = new ParamProducto();
                    objProducto.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    objProducto.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? "" : rdr["PRODUCTO"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.findProducto", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return objProducto;
        }
        public ParamTipoSolicitud findTipoSolicitud(string tipoSolicitud)
        {
            ParamTipoSolicitud objTipoSolicitud = new ParamTipoSolicitud();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT SECUENCIA, TIPO FROM dlg_param_tipo_solicitud ";
                command += string.Format("WHERE UPPER(TIPO) LIKE UPPER('{0}%')", tipoSolicitud);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    objTipoSolicitud = new ParamTipoSolicitud();
                    objTipoSolicitud.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    objTipoSolicitud.tipoSolicitud = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.findTipoSolicitud", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return objTipoSolicitud;
        }
        /* Pais */
        public Response savePais(double codigo, string nombre, string codigo_pais, string user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_pais", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_moneda = new OracleParameter("fa_modena", OracleDbType.Varchar2, codigo_pais, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_moneda);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_paises");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savePais", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPais getPaises()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPais response = new OutParamPais();
            var ora = new OracleServer(connectionString);

            ParamPais pais;
            List<ParamPais> list = new List<ParamPais>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_PAIS,NOMBRE, SIGLA_MONEDA FROM PAIS ORDER BY COD_PAIS";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    pais = new ParamPais();
                    pais.codigo_pais = DBNull.Value.Equals(rdr["COD_PAIS"]) ? 0 : double.Parse(rdr["COD_PAIS"].ToString());
                    pais.nombre_pais = DBNull.Value.Equals(rdr["NOMBRE"]) ? "" : rdr["NOMBRE"].ToString();
                    pais.codigo = DBNull.Value.Equals(rdr["SIGLA_MONEDA"]) ? "" : rdr["SIGLA_MONEDA"].ToString();
                    list.Add(pais);
                }
                rdr.Close();
                response.ListPais = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPaises" +
                    "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamPais getIdPaises(double id_pais)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamPais response = new OutParamPais();
            var ora = new OracleServer(connectionString);

            ParamPais pais;
            List<ParamPais> list = new List<ParamPais>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_PAIS, NOMBRE, SIGLA_MONEDA FROM PAIS ";
                command += string.Format("WHERE COD_PAIS = {0}", id_pais);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    pais = new ParamPais();
                    pais.codigo_pais = DBNull.Value.Equals(rdr["COD_PAIS"]) ? 0 : double.Parse(rdr["COD_PAIS"].ToString());
                    pais.nombre_pais = DBNull.Value.Equals(rdr["NOMBRE"]) ? "" : rdr["NOMBRE"].ToString();
                    pais.codigo = DBNull.Value.Equals(rdr["SIGLA_MONEDA"]) ? "" : rdr["SIGLA_MONEDA"].ToString();
                    list.Add(pais);
                }
                rdr.Close();
                response.ListPais = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdPaises", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updPais(double codigo, string nombre, string codigo_pais, string usuario)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_pais", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_codigo_pais = new OracleParameter("fa_codigo_pais", OracleDbType.Varchar2, codigo_pais, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usuario, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_codigo_pais);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_pais");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updPais", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deletePais(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_pais");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.savePais", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Entidades */
        public Response saveEntidad(double codigo_pais, double codigo, string nombre, string user, string abreviatura)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_entidad", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_cod_pais = new OracleParameter("fa_cod_pais", OracleDbType.Double, codigo_pais, ParameterDirection.Input);
            var pi_abrev = new OracleParameter("fa_abreviatura", OracleDbType.Varchar2, abreviatura, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_pais);
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_abrev);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_entidades");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveEntidad", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updEntidad(double codigo_entidad, string nombre_entidad, string user, string abreviatura)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_codigo = new OracleParameter("fa_codigo_entidad", OracleDbType.Double, codigo_entidad, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_nombre_entidad", OracleDbType.Varchar2, nombre_entidad, ParameterDirection.Input);
            var pi_abrev = new OracleParameter("fa_abreviatura", OracleDbType.Varchar2, abreviatura, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario_modifica", OracleDbType.Varchar2, user, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_abrev);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_entidades");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updEntidad", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteEntidad(double codigo_entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_codigo = new OracleParameter("fa_codigo_entidad", OracleDbType.Double, codigo_entidad, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_entidades_fed");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteEntidad", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutparamEntidadFederativa getEntidad()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutparamEntidadFederativa response = new OutparamEntidadFederativa();
            var ora = new OracleServer(connectionString);

            ParamEntidadfederativa entidad;
            List<ParamEntidadfederativa> list = new List<ParamEntidadfederativa>();
            string command = string.Empty;
            try
            {
                command = "SELECT E.COD_ENTIDAD, E.NOMBRE_ENTIDAD, P.NOMBRE FROM BBS_LIQCOM2_ENTIDADES E, PAIS P ";
                command += "WHERE E.COD_PAIS = P.COD_PAIS ORDER BY E.COD_ENTIDAD, E.COD_PAIS";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    entidad = new ParamEntidadfederativa();
                    entidad.codigo_entidad = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                    entidad.nombre_entidad = DBNull.Value.Equals(rdr["NOMBRE_ENTIDAD"]) ? "" : rdr["NOMBRE_ENTIDAD"].ToString();
                    entidad.codigo_pais = DBNull.Value.Equals(rdr["NOMBRE"]) ? "" : rdr["NOMBRE"].ToString();
                    list.Add(entidad);
                }
                rdr.Close();
                response.ListEntidades = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPaises" +
                    "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutparamEntidadFederativa getIdEntidad(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutparamEntidadFederativa response = new OutparamEntidadFederativa();
            var ora = new OracleServer(connectionString);

            ParamEntidadfederativa entidad;
            List<ParamEntidadfederativa> list = new List<ParamEntidadfederativa>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM BBS_LIQCOM2_ENTIDADES  ";
                command += string.Format("WHERE COD_ENTIDAD = {0}", codigo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    entidad = new ParamEntidadfederativa();
                    entidad.codigo_entidad = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                    entidad.nombre_entidad = DBNull.Value.Equals(rdr["NOMBRE_ENTIDAD"]) ? "" : rdr["NOMBRE_ENTIDAD"].ToString();
                    entidad.codigo_pais = DBNull.Value.Equals(rdr["COD_PAIS"]) ? "" : rdr["COD_PAIS"].ToString();
                    entidad.abreviatura = DBNull.Value.Equals(rdr["ABREVIATURA"]) ? "" : rdr["ABREVIATURA"].ToString();
                    list.Add(entidad);
                }
                rdr.Close();
                response.ListEntidades = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPaises" +
                    "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public double getEntidadAberv(string abreviatura)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            double response = 0;
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            try
            {
                command = "SELECT COD_ENTIDAD FROM BBS_LIQCOM2_ENTIDADES  ";
                command += string.Format("WHERE ABREVIATURA LIKE '{0}%'", abreviatura);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    response = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ParamsDAO.getEntidadAberv " + command + "", e);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutparamEntidadFederativa getEntidadPais(double pais)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutparamEntidadFederativa response = new OutparamEntidadFederativa();
            var ora = new OracleServer(connectionString);

            ParamEntidadfederativa entidad;
            List<ParamEntidadfederativa> list = new List<ParamEntidadfederativa>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_ENTIDAD, NOMBRE_ENTIDAD FROM BBS_LIQCOM2_ENTIDADES ";
                command += string.Format("WHERE COD_PAIS = {0} ORDER BY COD_ENTIDAD ", pais);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    entidad = new ParamEntidadfederativa();
                    entidad.codigo_entidad = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                    entidad.nombre_entidad = DBNull.Value.Equals(rdr["NOMBRE_ENTIDAD"]) ? "" : rdr["NOMBRE_ENTIDAD"].ToString();
                    list.Add(entidad);
                }
                rdr.Close();
                response.ListEntidades = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getEntidadPais " + command + "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Municipio */
        public Response saveMunicipio(double pais, double codigo_entidad, double codigo, string nombre, string user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_entidad", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_cod_entidad = new OracleParameter("fa_cod_entidad", OracleDbType.Double, codigo_entidad, ParameterDirection.Input);
            var pi_cod_pais = new OracleParameter("fa_codigo_pais", OracleDbType.Double, pais, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_pais);
                ora.AddParameter(pi_cod_entidad);
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_municipios");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveMunicipio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updMunicipio(double codigo_municipio, string municipio, string usuario, double pais, double entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo_municipio", OracleDbType.Double, codigo_municipio, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_municipio", OracleDbType.Varchar2, municipio, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usuario, ParameterDirection.Input);
            var pi_cod_entidad = new OracleParameter("fa_cod_entidad", OracleDbType.Double, entidad, ParameterDirection.Input);
            var pi_cod_pais = new OracleParameter("fa_cod_pais", OracleDbType.Double, pais, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(pi_cod_entidad);
                ora.AddParameter(pi_cod_pais);                                                                                                                                                                                      
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_municipios");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updMunicipio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteMunicipio(double codigo, double pais, double entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_cod_entidad = new OracleParameter("fa_cod_entidad", OracleDbType.Double, entidad, ParameterDirection.Input);
            var pi_cod_pais = new OracleParameter("fa_cod_pais", OracleDbType.Double, pais, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_cod_entidad);
                ora.AddParameter(pi_cod_pais);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_municipios");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteMunicipio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamMunicipios getMunicipios()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamMunicipios response = new OutParamMunicipios();
            var ora = new OracleServer(connectionString);

            ParamMunicipio municipio;
            List<ParamMunicipio> list = new List<ParamMunicipio>();
            string command = string.Empty;
            try
            {
                command = "SELECT M.COD_MUNICIPIO, E.NOMBRE_ENTIDAD, P.NOMBRE, M.NOMBRE_MUNICIPIO, M.COD_ENTIDAD, M.COD_PAIS FROM BBS_LIQCOM2_MUNICIPIOS M, BBS_LIQCOM2_ENTIDADES E, PAIS P ";
                command += "WHERE P.COD_PAIS = M.COD_PAIS AND E.COD_ENTIDAD = M.COD_ENTIDAD ORDER BY M.COD_MUNICIPIO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    municipio = new ParamMunicipio();
                    municipio.codigo_municipio = DBNull.Value.Equals(rdr["COD_MUNICIPIO"]) ? 0 : double.Parse(rdr["COD_MUNICIPIO"].ToString());
                    municipio.entidad_federativa = DBNull.Value.Equals(rdr["NOMBRE_ENTIDAD"]) ? "" : rdr["NOMBRE_ENTIDAD"].ToString();
                    municipio.pais = DBNull.Value.Equals(rdr["NOMBRE"]) ? "" : rdr["NOMBRE"].ToString();
                    municipio.nombre_municipio = DBNull.Value.Equals(rdr["NOMBRE_MUNICIPIO"]) ? "" : rdr["NOMBRE_MUNICIPIO"].ToString();
                    municipio.cod_entidad = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                    municipio.cod_pais = DBNull.Value.Equals(rdr["COD_PAIS"]) ? 0 : double.Parse(rdr["COD_PAIS"].ToString());
                    list.Add(municipio);
                }
                rdr.Close();
                response.ListMunicipios = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getPaises" +
                    "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamMunicipios getMunicipioEntidad(double pais, double entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamMunicipios response = new OutParamMunicipios();
            var ora = new OracleServer(connectionString);

            ParamMunicipio municipio;
            List<ParamMunicipio> list = new List<ParamMunicipio>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_MUNICIPIO, NOMBRE_MUNICIPIO FROM BBS_LIQCOM2_MUNICIPIOS ";
                command += string.Format("WHERE COD_PAIS = {0} AND COD_ENTIDAD = {1} ORDER BY COD_MUNICIPIO", pais, entidad);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    municipio = new ParamMunicipio();
                    municipio.codigo_municipio = DBNull.Value.Equals(rdr["COD_MUNICIPIO"]) ? 0 : double.Parse(rdr["COD_MUNICIPIO"].ToString());
                    municipio.nombre_municipio = DBNull.Value.Equals(rdr["NOMBRE_MUNICIPIO"]) ? "" : rdr["NOMBRE_MUNICIPIO"].ToString();
                    list.Add(municipio);
                }
                rdr.Close();
                response.ListMunicipios = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getMunicipiosEntidad" + command + " ", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamMunicipios getIdMunicipios(double codigo, double pais, double entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamMunicipios response = new OutParamMunicipios();
            var ora = new OracleServer(connectionString);

            ParamMunicipio municipio;
            List<ParamMunicipio> list = new List<ParamMunicipio>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_MUNICIPIO, COD_ENTIDAD, COD_PAIS, NOMBRE_MUNICIPIO FROM BBS_LIQCOM2_MUNICIPIOS ";
                command += string.Format("WHERE COD_MUNICIPIO = {0} AND COD_ENTIDAD = {1} AND COD_PAIS = {2}", codigo, entidad, pais);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    municipio = new ParamMunicipio();
                    municipio.codigo_municipio = DBNull.Value.Equals(rdr["COD_MUNICIPIO"]) ? 0 : double.Parse(rdr["COD_MUNICIPIO"].ToString());
                    municipio.entidad_federativa = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? "" : rdr["COD_ENTIDAD"].ToString();
                    municipio.pais = DBNull.Value.Equals(rdr["COD_PAIS"]) ? "" : rdr["COD_PAIS"].ToString();
                    municipio.nombre_municipio = DBNull.Value.Equals(rdr["NOMBRE_MUNICIPIO"]) ? "" : rdr["NOMBRE_MUNICIPIO"].ToString();
                    list.Add(municipio);
                }
                rdr.Close();
                response.ListMunicipios = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdMunicipios" +
                    "", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Colonia*/
        /*Bancos*/
        public OutParamBanco getBancos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamBanco response = new OutParamBanco();
            var ora = new OracleServer(connectionString);

            ParamBanco bancos;
            List<ParamBanco> list = new List<ParamBanco>();
            string command = string.Empty;
            try
            {
                command = "SELECT CODIGO_BANCO, NOMBRE_BANCO FROM DLG_PARAM_BANCOS ORDER BY CODIGO_BANCO ";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    bancos = new ParamBanco();
                    bancos.codigo_banco = DBNull.Value.Equals(rdr["CODIGO_BANCO"]) ? "" : (rdr["CODIGO_BANCO"].ToString());
                    bancos.nombre_banco = DBNull.Value.Equals(rdr["NOMBRE_BANCO"]) ? "" : rdr["NOMBRE_BANCO"].ToString();
                    list.Add(bancos);
                }
                rdr.Close();
                response.ListBancos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getBancos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamBanco getIdBanco(string codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamBanco response = new OutParamBanco();
            var ora = new OracleServer(connectionString);

            ParamBanco bancos;
            List<ParamBanco> list = new List<ParamBanco>();
            string command = string.Empty;
            try
            {
                command = "SELECT CODIGO_BANCO, NOMBRE_BANCO FROM DLG_PARAM_BANCOS ";
                command += string.Format("WHERE CODIGO_BANCO = '{0}'", codigo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    bancos = new ParamBanco();
                    bancos.codigo_banco = DBNull.Value.Equals(rdr["CODIGO_BANCO"]) ? "" : (rdr["CODIGO_BANCO"].ToString());
                    bancos.nombre_banco = DBNull.Value.Equals(rdr["NOMBRE_BANCO"]) ? "" : rdr["NOMBRE_BANCO"].ToString();
                    list.Add(bancos);
                }
                rdr.Close();
                response.ListBancos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                response.msg.errorCode = "88";
                response.msg.errorMessage = "Error al consultar los bancos. " + ex.ToString();
                throw new Exception("ParamsDAO.getIdBanco", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveBanco(string codigo, string nombre, string user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_codigo_banco = new OracleParameter("fa_codigo_banco", OracleDbType.Varchar2, codigo, ParameterDirection.Input);
            var pi_nombre_banco = new OracleParameter("fa_nombre_banco", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo_banco);
                ora.AddParameter(pi_nombre_banco);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_banco");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updBanco(string codigo, string nombre, string user)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_cod_banco = new OracleParameter("fa_codigo_banco", OracleDbType.Varchar2, codigo, ParameterDirection.Input);
            var pi_nombre_banco = new OracleParameter("fa_nombre_banco", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, user, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_banco);
                ora.AddParameter(pi_nombre_banco);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_banco");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updBanco", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteBanco(string codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_banco = new OracleParameter("fa_codigo_campo", OracleDbType.Varchar2, codigo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_banco);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_banco");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Campos parámterizados*/
        public OutParamCampos getCampos()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamCampos response = new OutParamCampos();
            var ora = new OracleServer(connectionString);

            ParamCampoParametrizado campos;
            List<ParamCampoParametrizado> list = new List<ParamCampoParametrizado>();
            string command = string.Empty;
            try
            {
                command = "SELECT c.COD_CAMPO_PARAMETRIZADO, d.NOMBRE_PAGADURIA, s.TIPO , p.DESCRIPCION_LIN, pe.PERIODO, c.CAMPO_ADICIONAL, c.TIPO_DATO, c.OPCIONES_CAMPO, c.OBLIGATORIO ";
                command += "FROM CAMPOS_PARAMETRIZADOS c, BB_LIB_CONVENIO_PAGADURIA d, dlg_param_tipo_solicitud s, DCPLIN01 p, dlg_param_periodos_aplicables pe ";
                command += "WHERE s.secuencia(+) = c.tipo_solicitud and d.CODIGO_PAGADURIA(+) = c.dependencia and p.TIPO_LINEA_LIN(+) = c.producto and pe.secuencia(+) = c.periodo ";
                command += "ORDER BY c.COD_CAMPO_PARAMETRIZADO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    campos = new ParamCampoParametrizado();
                    campos.codigo_campo = DBNull.Value.Equals(rdr["COD_CAMPO_PARAMETRIZADO"]) ? 0 : double.Parse(rdr["COD_CAMPO_PARAMETRIZADO"].ToString());
                    campos.solicitud = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();
                    campos.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    campos.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    campos.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    campos.campo = DBNull.Value.Equals(rdr["CAMPO_ADICIONAL"]) ? "" : rdr["CAMPO_ADICIONAL"].ToString();
                    campos.tipo_dato = DBNull.Value.Equals(rdr["TIPO_DATO"]) ? "" : rdr["TIPO_DATO"].ToString();
                    campos.opciones = DBNull.Value.Equals(rdr["OPCIONES_CAMPO"]) ? "" : rdr["OPCIONES_CAMPO"].ToString();
                    campos.obligatorio = DBNull.Value.Equals(rdr["OBLIGATORIO"]) ? "" : rdr["OBLIGATORIO"].ToString();
                    list.Add(campos);
                }
                rdr.Close();
                response.ListCampos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamCampos getIdCampos(double cod_campo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamCampos response = new OutParamCampos();
            var ora = new OracleServer(connectionString);

            ParamCampoParametrizado campos;
            List<ParamCampoParametrizado> list = new List<ParamCampoParametrizado>();
            string command = string.Empty;
            try
            {
                command = "SELECT COD_CAMPO_PARAMETRIZADO, TIPO_SOLICITUD, DEPENDENCIA, PRODUCTO, PERIODO, CAMPO_ADICIONAL, TIPO_DATO, OPCIONES_CAMPO, OBLIGATORIO ";
                command += string.Format("FROM CAMPOS_PARAMETRIZADOS WHERE COD_CAMPO_PARAMETRIZADO = {0}", cod_campo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    campos = new ParamCampoParametrizado();
                    campos.codigo_campo = DBNull.Value.Equals(rdr["COD_CAMPO_PARAMETRIZADO"]) ? 0 : double.Parse(rdr["COD_CAMPO_PARAMETRIZADO"].ToString());
                    campos.solicitud = DBNull.Value.Equals(rdr["TIPO_SOLICITUD"]) ? "" : rdr["TIPO_SOLICITUD"].ToString();
                    campos.dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? "" : rdr["DEPENDENCIA"].ToString();
                    campos.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? "" : rdr["PRODUCTO"].ToString();
                    campos.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();
                    campos.campo = DBNull.Value.Equals(rdr["CAMPO_ADICIONAL"]) ? "" : rdr["CAMPO_ADICIONAL"].ToString();
                    campos.tipo_dato = DBNull.Value.Equals(rdr["TIPO_DATO"]) ? "" : rdr["TIPO_DATO"].ToString();
                    campos.opciones = DBNull.Value.Equals(rdr["OPCIONES_CAMPO"]) ? "" : rdr["OPCIONES_CAMPO"].ToString();
                    campos.obligatorio = DBNull.Value.Equals(rdr["OBLIGATORIO"]) ? "" : rdr["OBLIGATORIO"].ToString();
                    list.Add(campos);
                }
                rdr.Close();
                response.ListCampos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveCampos(double? tipoSolicitud, double dependencia, double? producto, double? periodo, string campo, string tipo, string opciones, string requerido)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_tipo_solicitud = new OracleParameter("fa_tipo_solicitud", OracleDbType.Double, tipoSolicitud, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_dependencia", OracleDbType.Double, dependencia, ParameterDirection.Input);
            var pi_producto = new OracleParameter("fa_producto", OracleDbType.Double, producto, ParameterDirection.Input);
            var pi_periodo = new OracleParameter("fa_periodo", OracleDbType.Double, periodo, ParameterDirection.Input);
            var pi_nombre_campo = new OracleParameter("fa_nombre_campo", OracleDbType.Varchar2, campo, ParameterDirection.Input);
            var pi_tipo_dato = new OracleParameter("fa_tipo_dato", OracleDbType.Varchar2, tipo, ParameterDirection.Input);
            var pi_opciones = new OracleParameter("fa_opciones", OracleDbType.Varchar2, opciones, ParameterDirection.Input);
            var pi_obligatorio = new OracleParameter("fa_obligatorio", OracleDbType.Varchar2, requerido, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_tipo_solicitud);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_producto);
                ora.AddParameter(pi_periodo);
                ora.AddParameter(pi_nombre_campo);
                ora.AddParameter(pi_tipo_dato);
                ora.AddParameter(pi_opciones);
                ora.AddParameter(pi_obligatorio);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_campos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updCampos(double cod_campo, string campo, string tipo, string opciones, string requerido)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_cod_campo = new OracleParameter("fa_codigo_campo", OracleDbType.Double, cod_campo, ParameterDirection.Input);
            var pi_nombre_campo = new OracleParameter("fa_campo", OracleDbType.Varchar2, campo, ParameterDirection.Input);
            var pi_tipo_dato = new OracleParameter("fa_tipo_dato", OracleDbType.Varchar2, tipo, ParameterDirection.Input);
            var pi_opciones = new OracleParameter("fa_opciones", OracleDbType.Varchar2, opciones, ParameterDirection.Input);
            var pi_obligatorio = new OracleParameter("fa_obligatorio", OracleDbType.Varchar2, requerido, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_campo);
                ora.AddParameter(pi_nombre_campo);
                ora.AddParameter(pi_tipo_dato);
                ora.AddParameter(pi_opciones);
                ora.AddParameter(pi_obligatorio);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_campos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteCampos(double cod_campo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_campo = new OracleParameter("fa_codigo_campo", OracleDbType.Double, cod_campo, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_campo);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_campo");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteCampos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Documentos */
        public OutParamDocumentos getDocumentos(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDocumentos response = new OutParamDocumentos();
            var ora = new OracleServer(connectionString);

            ParamDocumentos documentos;
            List<ParamDocumentos> list = new List<ParamDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT DO.CODIGO_DOCUMENTO, DO.NOMBRE_DOCUMENTO, D.NOMBRE_PAGADURIA, P.DESCRIPCION_LIN FROM DLG_PARAM_DOCUMENTOS DO, BB_LIB_CONVENIO_PAGADURIA D, DCPLIN01 P ";
                command += "WHERE DO.DEPENDENCIA = D.CODIGO_PAGADURIA AND DO.PRODUCTO = P.TIPO_LINEA_LIN(+) ";
                command += string.Format("AND DO.DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND DO.PRODUCTO = {0} ", producto);
                }
                command += "ORDER BY DO.ORDEN";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    documentos = new ParamDocumentos();
                    documentos.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    documentos.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    documentos.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    documentos.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    list.Add(documentos);
                }
                rdr.Close();
                response.ListDocumentos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDocumentos getIdDocumentos(double? cod_documento)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDocumentos response = new OutParamDocumentos();
            var ora = new OracleServer(connectionString);

            ParamDocumentos documento;
            List<ParamDocumentos> list = new List<ParamDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM DLG_PARAM_DOCUMENTOS ";
                command += string.Format("WHERE CODIGO_DOCUMENTO = {0} ORDER BY ORDEN", cod_documento);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    documento = new ParamDocumentos();
                    documento.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    documento.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    documento.dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? "" : rdr["DEPENDENCIA"].ToString();
                    documento.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? "" : rdr["PRODUCTO"].ToString();
                    documento.firma = DBNull.Value.Equals(rdr["FIRMA"]) ? 0 : double.Parse(rdr["FIRMA"].ToString());
                    documento.llena_auto = DBNull.Value.Equals(rdr["LLENA_AUTOMATICO"]) ? 0 : double.Parse(rdr["LLENA_AUTOMATICO"].ToString());
                    documento.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? "" : rdr["URL_PLANTILLA"].ToString();
                    documento.orden = DBNull.Value.Equals(rdr["ORDEN"]) ? 0 : double.Parse(rdr["ORDEN"].ToString());
                    documento.expedientillo = DBNull.Value.Equals(rdr["EXPENDIENTILLO"]) ? 0 : double.Parse(rdr["EXPENDIENTILLO"].ToString());
                    documento.pagina_firma = DBNull.Value.Equals(rdr["PAGINA_FIRMA"]) ? 0 : double.Parse(rdr["PAGINA_FIRMA"].ToString());
                    documento.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    documento.max_item = DBNull.Value.Equals(rdr["MAXIMO_ITEMS"]) ? 0 : double.Parse(rdr["MAXIMO_ITEMS"].ToString());
                    list.Add(documento);
                }
                rdr.Close();
                response.ListDocumentos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveDocumentos(documents doc, double usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_orden_doc = new OracleParameter("fa_orden", OracleDbType.Double, doc.orden, ParameterDirection.Input);
            var pi_documento = new OracleParameter("fa_documento", OracleDbType.Varchar2, doc.nombre, ParameterDirection.Input);
            var pi_firma = new OracleParameter("fa_firma", OracleDbType.Double, doc.firma, ParameterDirection.Input);
            var pi_automatico = new OracleParameter("fa_automatico", OracleDbType.Double, doc.llena_auto, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_dependencia", OracleDbType.Double, doc.dependencia, ParameterDirection.Input);
            var pi_producto = new OracleParameter("fa_producto", OracleDbType.Double, doc.producto, ParameterDirection.Input);
            var pi_path = new OracleParameter("fa_path", OracleDbType.Varchar2, doc.path, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input);
            var pi_expedientillo = new OracleParameter("fa_expedientillo", OracleDbType.Double, doc.expedientillo, ParameterDirection.Input);
            var pi_pagina_firma = new OracleParameter("fa_pagina_firma", OracleDbType.Double, doc.pagina_firma, ParameterDirection.Input);
            var pi_compra = new OracleParameter("fa_compra", OracleDbType.Double, doc.compra, ParameterDirection.Input);
            var pi_max_item = new OracleParameter("fa_max_item", OracleDbType.Double, doc.max_item, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            //5742742
            try
            {
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_documento);
                ora.AddParameter(pi_orden_doc);
                ora.AddParameter(pi_firma);
                ora.AddParameter(pi_automatico);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_producto);
                ora.AddParameter(pi_path);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(pi_expedientillo);
                ora.AddParameter(pi_pagina_firma);
                ora.AddParameter(pi_compra);
                ora.AddParameter(pi_max_item);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_documentos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updDocumentos(documents doc, double usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_cod_documento = new OracleParameter("fa_codigo", OracleDbType.Double, doc.cod_documento, ParameterDirection.Input);
            var pi_documento = new OracleParameter("fa_documento", OracleDbType.Varchar2, doc.nombre, ParameterDirection.Input);
            var pi_path = new OracleParameter("fa_path", OracleDbType.Varchar2, doc.path, ParameterDirection.Input);
            var pi_firma = new OracleParameter("fa_firma", OracleDbType.Double, doc.firma, ParameterDirection.Input);
            var pi_llena = new OracleParameter("fa_llena_auto", OracleDbType.Double, doc.llena_auto, ParameterDirection.Input);
            var pi_orden = new OracleParameter("fa_orden", OracleDbType.Double, doc.orden, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input);
            var pi_producto = new OracleParameter("fa_producto", OracleDbType.Double, doc.producto, ParameterDirection.Input);
            var pi_dependencia = new OracleParameter("fa_dependencia", OracleDbType.Double, doc.dependencia, ParameterDirection.Input);
            var pi_expedientillo = new OracleParameter("fa_expedientillo", OracleDbType.Double, doc.expedientillo, ParameterDirection.Input);
            var pi_pagina_firma = new OracleParameter("fa_pagina_firma", OracleDbType.Double, doc.pagina_firma, ParameterDirection.Input);
            var pi_compra = new OracleParameter("fa_compra", OracleDbType.Double, doc.compra, ParameterDirection.Input);
            var pi_max_item = new OracleParameter("fa_max_item", OracleDbType.Double, doc.max_item, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_documento);
                ora.AddParameter(pi_documento);
                ora.AddParameter(pi_path);
                ora.AddParameter(pi_firma);
                ora.AddParameter(pi_llena);
                ora.AddParameter(pi_orden);
                ora.AddParameter(pi_dependencia);
                ora.AddParameter(pi_producto);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(pi_expedientillo);
                ora.AddParameter(pi_pagina_firma);
                ora.AddParameter(pi_compra);
                ora.AddParameter(pi_max_item);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_documentos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteDocumentos(double cod_documento)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            var pi_cod_documento = new OracleParameter("fa_codigo_doc", OracleDbType.Double, cod_documento, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_cod_documento);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_documentos");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDocumentos getDocumentosOriginacion(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDocumentos response = new OutParamDocumentos();
            var ora = new OracleServer(connectionString);

            ParamDocumentos documento;
            List<ParamDocumentos> list = new List<ParamDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT COMPRA, MAXIMO_ITEMS, ORDEN, CODIGO_DOCUMENTO, NOMBRE_DOCUMENTO, DEPENDENCIA, PRODUCTO, FIRMA, LLENA_AUTOMATICO, URL_PLANTILLA FROM DLG_PARAM_DOCUMENTOS ";
                command += string.Format("WHERE DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND PRODUCTO = {0} ", producto);
                }
                command += "ORDER BY ORDEN";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    documento = new ParamDocumentos();
                    documento.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    documento.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    documento.dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? "" : rdr["DEPENDENCIA"].ToString();
                    documento.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? "" : rdr["PRODUCTO"].ToString();
                    documento.firma = DBNull.Value.Equals(rdr["FIRMA"]) ? 0 : double.Parse(rdr["FIRMA"].ToString());
                    documento.llena_auto = DBNull.Value.Equals(rdr["LLENA_AUTOMATICO"]) ? 0 : double.Parse(rdr["LLENA_AUTOMATICO"].ToString());
                    documento.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? "" : rdr["URL_PLANTILLA"].ToString();
                    documento.orden = DBNull.Value.Equals(rdr["ORDEN"]) ? 0 : double.Parse(rdr["ORDEN"].ToString());
                    documento.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    documento.max_item = DBNull.Value.Equals(rdr["MAXIMO_ITEMS"]) ? 0 : double.Parse(rdr["MAXIMO_ITEMS"].ToString());

                    list.Add(documento);
                }
                rdr.Close();
                response.ListDocumentos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDocumentosOriginacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamDocumentos getDocumentosConfig(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDocumentos response = new OutParamDocumentos();
            var ora = new OracleServer(connectionString);

            ParamDocumentos documentos;
            List<ParamDocumentos> list = new List<ParamDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT DO.COMPRA,DO.MAXIMO_ITEMS, DO.CODIGO_DOCUMENTO, DO.NOMBRE_DOCUMENTO, D.NOMBRE_PAGADURIA, P.DESCRIPCION_LIN FROM DLG_PARAM_DOCUMENTOS DO, BB_LIB_CONVENIO_PAGADURIA D, DCPLIN01 P ";
                command += "WHERE DO.DEPENDENCIA = D.CODIGO_PAGADURIA AND DO.PRODUCTO = P.TIPO_LINEA_LIN(+) AND DO.LLENA_AUTOMATICO = 1 ";
                command += string.Format("AND DO.DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND DO.PRODUCTO = {0} ", producto);
                }
                command += "ORDER BY DO.ORDEN";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    documentos = new ParamDocumentos();
                    documentos.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    documentos.max_item = DBNull.Value.Equals(rdr["MAXIMO_ITEMS"]) ? 0 : double.Parse(rdr["MAXIMO_ITEMS"].ToString());
                    documentos.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    documentos.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    documentos.dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();
                    documentos.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();
                    list.Add(documentos);
                }
                rdr.Close();
                response.ListDocumentos = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDocumentosConfig", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Configuracion de Documentos*/
        public Response saveConfigDoc(double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo_doc = new OracleParameter("fa_codigo_doc", OracleDbType.Double, documento, ParameterDirection.Input);
            var pi_tipo_obt = new OracleParameter("fa_tipo_obtencion", OracleDbType.Double, obtencion, ParameterDirection.Input);
            var pi_pos_x = new OracleParameter("fa_posicion_x", OracleDbType.Double, x, ParameterDirection.Input);
            var pi_pos_y = new OracleParameter("fa_posicion_y", OracleDbType.Double, y, ParameterDirection.Input);
            var pi_valor = new OracleParameter("fa_valor", OracleDbType.Varchar2, dato, ParameterDirection.Input);
            var pi_pagina = new OracleParameter("fa_pagina", OracleDbType.Double, pagina, ParameterDirection.Input);
            var pi_fuente = new OracleParameter("fa_fuente", OracleDbType.Double, fuente, ParameterDirection.Input);
            var pi_sn_valida = new OracleParameter("fa_sn_valida", OracleDbType.Varchar2, valida, ParameterDirection.Input);
            var pi_campo_valida = new OracleParameter("fa_campo_validar", OracleDbType.Varchar2, campoComparar, ParameterDirection.Input);
            var pi_vlr_campo = new OracleParameter("fa_vlr_campo", OracleDbType.Varchar2, valorComparar, ParameterDirection.Input);
            var pi_variacion = new OracleParameter("fa_variacion", OracleDbType.Double, variacion, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            //5742742
            try
            {
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(pi_tipo_obt);
                ora.AddParameter(pi_pos_x);
                ora.AddParameter(pi_pos_y);
                ora.AddParameter(pi_valor);
                ora.AddParameter(pi_pagina);
                ora.AddParameter(pi_fuente);
                ora.AddParameter(pi_sn_valida);
                ora.AddParameter(pi_campo_valida);
                ora.AddParameter(pi_vlr_campo);
                ora.AddParameter(pi_variacion);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_conf_doc");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveConfigDoc", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updConfigDoc(double codigo, double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo_config = new OracleParameter("fa_codigo_config", OracleDbType.Double, codigo, ParameterDirection.Input);
            var pi_codigo_doc = new OracleParameter("fa_codigo_doc", OracleDbType.Double, documento, ParameterDirection.Input);
            var pi_tipo_obt = new OracleParameter("fa_tipo_obtencion", OracleDbType.Double, obtencion, ParameterDirection.Input);
            var pi_pos_x = new OracleParameter("fa_posicion_x", OracleDbType.Double, x, ParameterDirection.Input);
            var pi_pos_y = new OracleParameter("fa_posicion_y", OracleDbType.Double, y, ParameterDirection.Input);
            var pi_valor = new OracleParameter("fa_valor", OracleDbType.Varchar2, dato, ParameterDirection.Input);
            var pi_pagina = new OracleParameter("fa_pagina", OracleDbType.Double, pagina, ParameterDirection.Input);
            var pi_fuente = new OracleParameter("fa_fuente", OracleDbType.Double, fuente, ParameterDirection.Input);
            var pi_sn_valida = new OracleParameter("fa_sn_valida", OracleDbType.Varchar2, valida, ParameterDirection.Input);
            var pi_campo_valida = new OracleParameter("fa_campo_validar", OracleDbType.Varchar2, campoComparar, ParameterDirection.Input);
            var pi_vlr_campo = new OracleParameter("fa_vlr_campo", OracleDbType.Varchar2, valorComparar, ParameterDirection.Input);
            var pi_variacion = new OracleParameter("fa_variacion", OracleDbType.Double, variacion, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo_config);
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(pi_tipo_obt);
                ora.AddParameter(pi_pos_x);
                ora.AddParameter(pi_pos_y);
                ora.AddParameter(pi_valor);
                ora.AddParameter(pi_pagina);
                ora.AddParameter(pi_fuente);
                ora.AddParameter(pi_sn_valida);
                ora.AddParameter(pi_campo_valida);
                ora.AddParameter(pi_vlr_campo);
                ora.AddParameter(pi_variacion);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_config_doc");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updConfigDoc", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteConfigDoc(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo_config = new OracleParameter("fa_codigo_config", OracleDbType.Double, codigo, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo_config);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_config_docs");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteConfigDoc", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamConfiguracionDoc getConfigDocumentos(double dependencia, double? producto, double? doc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamConfiguracionDoc response = new OutParamConfiguracionDoc();
            var ora = new OracleServer(connectionString);

            ParamConfiguracionDocumentos configuracion;
            List<ParamConfiguracionDocumentos> list = new List<ParamConfiguracionDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT CF.VALOR_AUMENTA,CF.CODIGO_CONFIG_DOC, CF.CODIGO_DOCUMENTO, D.NOMBRE_DOCUMENTO, CF.TIPO_OPTENCION, CF.POSICION_X, CF.POSICION_Y, CF.VALOR1, CF.PAGINA, CF.TAM_FUENTE, CF.SN_VALIDACION, CF.CAMPO_VALIDACION, CF.VALOR_COMPARAR ";
                command += "FROM DLG_PARAM_LLENAR_DOC CF, DLG_PARAM_DOCUMENTOS D ";
                command += string.Format("WHERE D.DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND D.PRODUCTO = {0} ", producto);
                }
                if (doc != null)
                {
                    command += string.Format("AND CF.CODIGO_DOCUMENTO = {0} ", doc);
                }
                command += "AND CF.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO ORDER BY CF.PAGINA, CF.CODIGO_CONFIG_DOC";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    configuracion = new ParamConfiguracionDocumentos();
                    configuracion.codigo_config = DBNull.Value.Equals(rdr["CODIGO_CONFIG_DOC"]) ? 0 : double.Parse(rdr["CODIGO_CONFIG_DOC"].ToString());
                    configuracion.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    configuracion.nombre_documento = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    configuracion.tipo_optencion = DBNull.Value.Equals(rdr["TIPO_OPTENCION"]) ? "" : rdr["TIPO_OPTENCION"].ToString();
                    configuracion.posicion_x = DBNull.Value.Equals(rdr["POSICION_X"]) ? 0 : double.Parse(rdr["POSICION_X"].ToString());
                    configuracion.posicion_y = DBNull.Value.Equals(rdr["POSICION_Y"]) ? 0 : double.Parse(rdr["POSICION_Y"].ToString());
                    configuracion.valor = DBNull.Value.Equals(rdr["VALOR1"]) ? "" : rdr["VALOR1"].ToString();
                    configuracion.pagina = DBNull.Value.Equals(rdr["PAGINA"]) ? 0 : double.Parse(rdr["PAGINA"].ToString());
                    configuracion.fuente = DBNull.Value.Equals(rdr["TAM_FUENTE"]) ? 0 : double.Parse(rdr["TAM_FUENTE"].ToString());
                    configuracion.tvalidacion = DBNull.Value.Equals(rdr["SN_VALIDACION"]) ? "N" : rdr["SN_VALIDACION"].ToString();
                    configuracion.campoValidar = DBNull.Value.Equals(rdr["CAMPO_VALIDACION"]) ? "" : rdr["CAMPO_VALIDACION"].ToString();
                    configuracion.valor_validacion = DBNull.Value.Equals(rdr["VALOR_COMPARAR"]) ? "" : rdr["VALOR_COMPARAR"].ToString();
                    configuracion.aumentoy = DBNull.Value.Equals(rdr["VALOR_AUMENTA"]) ? 0 : double.Parse(rdr["VALOR_AUMENTA"].ToString());

                    list.Add(configuracion);
                }
                rdr.Close();
                response.ListConfiguracion = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamConfiguracionDoc getConfigDocumentosFirma(double dependencia, double? producto, double? doc, double pagina)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamConfiguracionDoc response = new OutParamConfiguracionDoc();
            var ora = new OracleServer(connectionString);

            ParamConfiguracionDocumentos configuracion;
            List<ParamConfiguracionDocumentos> list = new List<ParamConfiguracionDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT CF.VALOR_AUMENTA,CF.CODIGO_CONFIG_DOC, CF.CODIGO_DOCUMENTO, D.NOMBRE_DOCUMENTO, CF.TIPO_OPTENCION, CF.POSICION_X, CF.POSICION_Y, CF.VALOR1, CF.PAGINA, CF.TAM_FUENTE, CF.SN_VALIDACION, CF.CAMPO_VALIDACION, CF.VALOR_COMPARAR ";
                command += "FROM DLG_PARAM_LLENAR_DOC CF, DLG_PARAM_DOCUMENTOS D ";
                command += string.Format("WHERE D.DEPENDENCIA = {0} AND PAGINA = {1} ", dependencia, pagina);
                if (producto != null)
                {
                    command += string.Format("AND D.PRODUCTO = {0} ", producto);
                }
                if (doc != null)
                {
                    command += string.Format("AND CF.CODIGO_DOCUMENTO = {0} ", doc);
                }
                command += "AND CF.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO ORDER BY CF.PAGINA, CF.CODIGO_CONFIG_DOC";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    configuracion = new ParamConfiguracionDocumentos();
                    configuracion.codigo_config = DBNull.Value.Equals(rdr["CODIGO_CONFIG_DOC"]) ? 0 : double.Parse(rdr["CODIGO_CONFIG_DOC"].ToString());
                    configuracion.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    configuracion.nombre_documento = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    configuracion.tipo_optencion = DBNull.Value.Equals(rdr["TIPO_OPTENCION"]) ? "" : rdr["TIPO_OPTENCION"].ToString();
                    configuracion.posicion_x = DBNull.Value.Equals(rdr["POSICION_X"]) ? 0 : double.Parse(rdr["POSICION_X"].ToString());
                    configuracion.posicion_y = DBNull.Value.Equals(rdr["POSICION_Y"]) ? 0 : double.Parse(rdr["POSICION_Y"].ToString());
                    configuracion.valor = DBNull.Value.Equals(rdr["VALOR1"]) ? "" : rdr["VALOR1"].ToString();
                    configuracion.pagina = DBNull.Value.Equals(rdr["PAGINA"]) ? 0 : double.Parse(rdr["PAGINA"].ToString());
                    configuracion.fuente = DBNull.Value.Equals(rdr["TAM_FUENTE"]) ? 0 : double.Parse(rdr["TAM_FUENTE"].ToString());
                    configuracion.tvalidacion = DBNull.Value.Equals(rdr["SN_VALIDACION"]) ? "N" : rdr["SN_VALIDACION"].ToString();
                    configuracion.campoValidar = DBNull.Value.Equals(rdr["CAMPO_VALIDACION"]) ? "" : rdr["CAMPO_VALIDACION"].ToString();
                    configuracion.valor_validacion = DBNull.Value.Equals(rdr["VALOR_COMPARAR"]) ? "" : rdr["VALOR_COMPARAR"].ToString();
                    configuracion.aumentoy = DBNull.Value.Equals(rdr["VALOR_AUMENTA"]) ? 0 : double.Parse(rdr["VALOR_AUMENTA"].ToString());

                    list.Add(configuracion);
                }
                rdr.Close();
                response.ListConfiguracion = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getConfigDocumentosFirma", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamConfiguracionDoc getIdConfigDocumentos(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamConfiguracionDoc response = new OutParamConfiguracionDoc();
            var ora = new OracleServer(connectionString);

            ParamConfiguracionDocumentos configuracion;
            List<ParamConfiguracionDocumentos> list = new List<ParamConfiguracionDocumentos>();
            string command = string.Empty;
            try
            {
                command = "SELECT CF.VALOR_AUMENTA, CF.CODIGO_CONFIG_DOC, CF.CODIGO_DOCUMENTO, D.NOMBRE_DOCUMENTO, D.DEPENDENCIA, D.PRODUCTO, CF.TIPO_OPTENCION, CF.POSICION_X, CF.POSICION_Y, CF.VALOR1, CF.PAGINA, CF.TAM_FUENTE, CF.SN_VALIDACION, CF.CAMPO_VALIDACION, CF.VALOR_COMPARAR ";
                command += "FROM DLG_PARAM_LLENAR_DOC CF, DLG_PARAM_DOCUMENTOS D ";
                command += string.Format("WHERE CF.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO AND CF.CODIGO_CONFIG_DOC = {0} ", codigo);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    configuracion = new ParamConfiguracionDocumentos();
                    configuracion.codigo_config = DBNull.Value.Equals(rdr["CODIGO_CONFIG_DOC"]) ? 0 : double.Parse(rdr["CODIGO_CONFIG_DOC"].ToString());
                    configuracion.cod_documento = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    configuracion.nombre_documento = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? "" : rdr["NOMBRE_DOCUMENTO"].ToString();
                    configuracion.tipo_optencion = DBNull.Value.Equals(rdr["TIPO_OPTENCION"]) ? "" : rdr["TIPO_OPTENCION"].ToString();
                    configuracion.posicion_x = DBNull.Value.Equals(rdr["POSICION_X"]) ? 0 : double.Parse(rdr["POSICION_X"].ToString());
                    configuracion.posicion_y = DBNull.Value.Equals(rdr["POSICION_Y"]) ? 0 : double.Parse(rdr["POSICION_Y"].ToString());
                    configuracion.valor = DBNull.Value.Equals(rdr["VALOR1"]) ? "" : rdr["VALOR1"].ToString();
                    configuracion.pagina = DBNull.Value.Equals(rdr["PAGINA"]) ? 0 : double.Parse(rdr["PAGINA"].ToString());
                    configuracion.fuente = DBNull.Value.Equals(rdr["TAM_FUENTE"]) ? 0 : double.Parse(rdr["TAM_FUENTE"].ToString());
                    configuracion.dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? 0 : double.Parse(rdr["DEPENDENCIA"].ToString());
                    configuracion.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? 0 : double.Parse(rdr["PRODUCTO"].ToString());
                    configuracion.tvalidacion = DBNull.Value.Equals(rdr["SN_VALIDACION"]) ? "N" : rdr["SN_VALIDACION"].ToString();
                    configuracion.campoValidar = DBNull.Value.Equals(rdr["CAMPO_VALIDACION"]) ? "" : rdr["CAMPO_VALIDACION"].ToString();
                    configuracion.valor_validacion = DBNull.Value.Equals(rdr["VALOR_COMPARAR"]) ? "" : rdr["VALOR_COMPARAR"].ToString();
                    configuracion.aumentoy = DBNull.Value.Equals(rdr["VALOR_AUMENTA"]) ? 0 : double.Parse(rdr["VALOR_AUMENTA"].ToString());

                    list.Add(configuracion);
                }
                rdr.Close();
                response.ListConfiguracion = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdConfigDocumentos", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        //PARENTESCO
        public OutParamParentesco parentesco()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamParentesco response = new OutParamParentesco();
            var ora = new OracleServer(connectionString);

            ParamParentesco parentesco;
            List<ParamParentesco> list = new List<ParamParentesco>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM dfppar19 ORDER BY CODIGO_PARENTESCO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    parentesco = new ParamParentesco();
                    parentesco.codigo = DBNull.Value.Equals(rdr["CODIGO_PARENTESCO"]) ? 0 : double.Parse(rdr["CODIGO_PARENTESCO"].ToString());
                    parentesco.nombre_parentesco = DBNull.Value.Equals(rdr["NOMBRE_PARENTESCO"]) ? "" : rdr["NOMBRE_PARENTESCO"].ToString();
                    list.Add(parentesco);
                }
                rdr.Close();
                response.listParentesco = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.parentesco", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* Casas Financieras*/
        public OutParamCasaFinanciera getCasas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamCasaFinanciera response = new OutParamCasaFinanciera();
            var ora = new OracleServer(connectionString);

            CasaFinanciera casas_financieras;
            List<CasaFinanciera> list = new List<CasaFinanciera>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM DLG_PARAM_CASAS_FINANCIERAS ORDER BY RFC_CASA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    casas_financieras = new CasaFinanciera();
                    casas_financieras.rfc_casa = DBNull.Value.Equals(rdr["RFC_CASA"]) ? "" : rdr["RFC_CASA"].ToString();
                    casas_financieras.casa_financera = DBNull.Value.Equals(rdr["NOMBRE_CASA"]) ? "" : rdr["NOMBRE_CASA"].ToString();
                    casas_financieras.estado = DBNull.Value.Equals(rdr["ESTADO"]) ? "" : (rdr["ESTADO"].ToString() == "A" ? "ACTIVO" : "INACTIVO");
                    list.Add(casas_financieras);
                }
                rdr.Close();
                response.ListCasas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getCasas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamCasaFinanciera getCasasActivas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamCasaFinanciera response = new OutParamCasaFinanciera();
            var ora = new OracleServer(connectionString);

            CasaFinanciera casas_financieras;
            List<CasaFinanciera> list = new List<CasaFinanciera>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM DLG_PARAM_CASAS_FINANCIERAS WHERE ESTADO = 'A' ORDER BY RFC_CASA";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    casas_financieras = new CasaFinanciera();
                    casas_financieras.rfc_casa = DBNull.Value.Equals(rdr["RFC_CASA"]) ? "" : rdr["RFC_CASA"].ToString();
                    casas_financieras.casa_financera = DBNull.Value.Equals(rdr["NOMBRE_CASA"]) ? "" : rdr["NOMBRE_CASA"].ToString();
                    list.Add(casas_financieras);
                }
                rdr.Close();
                response.ListCasas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getCasasActivas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response saveCasa(string rfc, string casa_financiera, string estado, double asesor)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo_casa", OracleDbType.Varchar2, rfc, ParameterDirection.Input);
            var pi_casa_financiera = new OracleParameter("fa_nombre_casa", OracleDbType.Varchar2, casa_financiera, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, asesor, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_casa_financiera);
                ora.AddParameter(pi_estado);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_param_casa");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveCasa", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response updCasa(string rfc, string casa, string estado, double asesor)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_codigo_casa", OracleDbType.Varchar2, rfc, ParameterDirection.Input);
            var pi_casa_financiera = new OracleParameter("fa_nombre_casa", OracleDbType.Varchar2, casa, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Varchar2, estado, ParameterDirection.Input);
            var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, asesor, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_casa_financiera);
                ora.AddParameter(pi_estado);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_param_casa");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updCasa", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response deleteCasa(string secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Varchar2, secuencia, ParameterDirection.Input);
            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_param_casa");
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteCasa", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamCasaFinanciera getIdCasas(string secuencia)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamCasaFinanciera response = new OutParamCasaFinanciera();
            var ora = new OracleServer(connectionString);

            CasaFinanciera casa_financiera = new CasaFinanciera();
            List<CasaFinanciera> list = new List<CasaFinanciera>();
            string command = string.Empty;
            try
            {
                command = "SELECT * FROM DLG_PARAM_CASAS_FINANCIERAS ";
                command += string.Format("WHERE RFC_CASA = '{0}' ORDER BY RFC_CASA", secuencia);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    casa_financiera = new CasaFinanciera();
                    casa_financiera.rfc_casa = DBNull.Value.Equals(rdr["RFC_CASA"]) ? "" : (rdr["RFC_CASA"].ToString());
                    casa_financiera.casa_financera = DBNull.Value.Equals(rdr["NOMBRE_CASA"]) ? "" : rdr["NOMBRE_CASA"].ToString();
                    casa_financiera.estado = DBNull.Value.Equals(rdr["ESTADO"]) ? "" : rdr["ESTADO"].ToString();
                    list.Add(casa_financiera);
                }
                rdr.Close();
                response.ListCasas = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdCasas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /* CLAVE DELEGACION */
        public OutParamClaveDelg getClaves()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutParamClaveDelg data = new OutParamClaveDelg();
            List<ClaveDelg> list = new List<ClaveDelg>();
            ClaveDelg claves;
            string command = "";
            try
            {
                command = "SELECT * FROM CLAVE_DELEGACION ORDER BY CLAVE";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    claves = new ClaveDelg();
                    claves.clave = DBNull.Value.Equals(rdr["CLAVE"]) ? "" : (rdr["CLAVE"].ToString());
                    claves.delegacion = DBNull.Value.Equals(rdr["DELEGACION"]) ? "" : (rdr["DELEGACION"].ToString());
                    list.Add(claves);
                }
                rdr.Dispose();
                data.ListClavesDelg = list;
                data.msg = new Response();
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getClaves", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutParamClaveDelg getIdClaves(string cod)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutParamClaveDelg data = new OutParamClaveDelg();
            List<ClaveDelg> list = new List<ClaveDelg>();
            ClaveDelg claves;
            string command = "";
            try
            {
                command = $@"SELECT * FROM CLAVE_DELEGACION WHERE CLAVE = '{cod}' ORDER BY CLAVE";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    claves = new ClaveDelg();
                    claves.clave = DBNull.Value.Equals(rdr["CLAVE"]) ? "" : (rdr["CLAVE"].ToString());
                    claves.delegacion = DBNull.Value.Equals(rdr["DELEGACION"]) ? "" : (rdr["DELEGACION"].ToString());
                    list.Add(claves);
                }
                rdr.Dispose();
                data.ListClavesDelg = list;
                data.msg = new Response();
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.getIdClaves", ex);

            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response saveClaves(string cod, string delg, string usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pi_secuencia = new OracleParameter("fa_clave", OracleDbType.Varchar2, cod, ParameterDirection.Input);
                var pi_delegacion = new OracleParameter("fa_delegacion", OracleDbType.Varchar2, delg, ParameterDirection.Input);
                var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_delegacion);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_insert_clave_delg");
                //Respuesta del procedimiento
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.saveClaves", ex);

            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response updClaves(string cod, string delg, string usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pi_secuencia = new OracleParameter("fa_clave", OracleDbType.Varchar2, cod, ParameterDirection.Input);
                var pi_casa_financiera = new OracleParameter("fa_delegacion", OracleDbType.Varchar2, delg, ParameterDirection.Input);
               var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(pi_casa_financiera);
                ora.AddParameter(pi_usuario);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_upd_clave_delg");
                //Respuesta del procedimiento
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.updClaves", ex);

            }
            finally
            {
                ora.Dispose();
            }
            
            return data;
        }
        public Response deleteClaves(string cod)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pi_secuencia = new OracleParameter("fa_secuencia", OracleDbType.Varchar2, cod, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_secuencia);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_dlg_delete_clave_delg");
                //Respuesta del procedimiento
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.deleteClaves", ex);

            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
    }
}
