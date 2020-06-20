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
    public class DocumentsDAO
    {
        public OutDocuments GetDocuments()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutDocuments response = new OutDocuments();
            var ora = new OracleServer(connectionString);

            Documents document;
            List<Documents> list = new List<Documents>();
            string command = string.Empty;

            try
            {
                command = " SELECT BBS_LIQCOM_PARAM_CARGA_DOCTOS.TIPO_DOCTO, BBS_LIQCOM_PARAM_CARGA_DOCTOS.NOMBRE_DOCTO FROM BBS_LIQCOM_PARAM_CARGA_DOCTOS  ";
                command = command + " WHERE BBS_LIQCOM_PARAM_CARGA_DOCTOS.IND_ACTIVO = 1 order by ORDEN , TIPO_DOCTO ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    document = new Documents();
                    document.documentType = DBNull.Value.Equals(rdr["TIPO_DOCTO"]) ? 0 : int.Parse(rdr["TIPO_DOCTO"].ToString());
                    document.documentName = DBNull.Value.Equals(rdr["NOMBRE_DOCTO"]) ? string.Empty : rdr["NOMBRE_DOCTO"].ToString();
                    list.Add(document);
                }
                rdr.Close();
                response.lstDocuments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetDocuments", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public Response LogUploadDocuments(InLogDocuments input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            string esNuloError = string.Empty;

            try
            {
                var pi_sequence = new OracleParameter("A_SECUENCIA", OracleDbType.Double, input.Sequence, ParameterDirection.Input);
                ora.AddParameter(pi_sequence);

                var pi_path = new OracleParameter("A_RUTA_DOCUMENTO_CARGADO", OracleDbType.Varchar2, input.Path, ParameterDirection.Input);
                ora.AddParameter(pi_path);

                var pi_name = new OracleParameter("A_NOMBRE_DOCUMENTO_CARGADO", OracleDbType.Varchar2, input.Name, ParameterDirection.Input);
                ora.AddParameter(pi_name);

                var pi_documentID = new OracleParameter("A_NUMERO_CEDULA", OracleDbType.Varchar2, input.DocumentID, ParameterDirection.Input);
                ora.AddParameter(pi_documentID);

                var po_ErrorCode = new OracleParameter("fa_codigo_error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_LIQCOM_SP_LOG_CARGA_IMG");

                response.errorCode = ora.GetParameter("fa_codigo_error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

                ora.Dispose();

            }
            catch (Exception ex)
            {
                //response.errorMessage = ex.InnerException.ToString();
                //throw new Exception("ExecutiveDAO.UpdateExecutive", ex);
                LogHelper.WriteLog("Models", "ManagerPQR", "CreatePQR" + esNuloError, ex, "");
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutUploadDocuments GetUploadDocuments(string documentID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutUploadDocuments response = new OutUploadDocuments();
            var ora = new OracleServer(connectionString);

            UploadDocuments document;
            List<UploadDocuments> list = new List<UploadDocuments>();
            string command = string.Empty;

            try
            {
                command = " SELECT BBS_PORTAL_LOG_IMG.CONSECUTIVO, BBS_PORTAL_LOG_IMG.DESCRIPCION_DOCUMENTO, BBS_PORTAL_LOG_IMG.NOMBRE_DOCUMENTO_CARGADO, ";
                command = string.Format("{0} BBS_PORTAL_LOG_IMG.YEAR, BBS_PORTAL_LOG_IMG.MONTH FROM BBS_PORTAL_LOG_IMG WHERE ", command);
                command = string.Format("{0} BBS_PORTAL_LOG_IMG.NUMERO_CEDULA = '{1}' ORDER BY BBS_PORTAL_LOG_IMG.CONSECUTIVO DESC ", command, documentID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    document = new UploadDocuments();
                    document.Consecutive = DBNull.Value.Equals(rdr["CONSECUTIVO"]) ? 0 : double.Parse(rdr["CONSECUTIVO"].ToString());
                    document.Description = DBNull.Value.Equals(rdr["DESCRIPCION_DOCUMENTO"]) ? string.Empty : rdr["DESCRIPCION_DOCUMENTO"].ToString();
                    document.Name = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO_CARGADO"]) ? string.Empty : rdr["NOMBRE_DOCUMENTO_CARGADO"].ToString();
                    document.Year = DBNull.Value.Equals(rdr["YEAR"]) ? 0 : int.Parse(rdr["YEAR"].ToString());
                    document.Month = DBNull.Value.Equals(rdr["MONTH"]) ?0 : int.Parse(rdr["MONTH"].ToString());

                    list.Add(document);
                }
                rdr.Close();
                response.lstUploadDocuments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetUploadDocuments", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }


        public OutParamSectorGuias GetddSectorGuias()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorGuias response = new OutParamSectorGuias();
            var ora = new OracleServer(connectionString);

            ParamSectorGuias Secguias;
            response.ListSectorGuias = new List<ParamSectorGuias>();
            string command = string.Empty;

            try
            {
                command = " select * from dlg_param_sectorGuias where ind_estado = 1 order by sector ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    Secguias = new ParamSectorGuias();
                    Secguias.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    Secguias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : (rdr["SECTOR"].ToString());
                    response.ListSectorGuias.Add(Secguias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetddSectorGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutParamSectorTablas GetddSectorTablas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamSectorTablas response = new OutParamSectorTablas();
            var ora = new OracleServer(connectionString);

            ParamSectorTablas Secguias;
            response.ListSectorTablas = new List<ParamSectorTablas>();
            string command = string.Empty;

            try
            {
                command = " select * from dlg_param_sectorTablas where ind_estado = 1 order by sector ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    Secguias = new ParamSectorTablas();
                    Secguias.secuencia = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    Secguias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : (rdr["SECTOR"].ToString());
                    response.ListSectorTablas.Add(Secguias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetddSectorTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutParamGuias GetGuias()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guias;
            response.ListDoc = new List<ParamGuias>();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM GUIAS ORDER BY SECTOR ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guias = new ParamGuias();
                    guias.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_GUIA"]) ? 0 : double.Parse(rdr["CODIGO_GUIA"].ToString());
                    guias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guias.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guias.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guias.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guias.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    guias.fecha = guias.fecha.Substring(0, 10);
                    guias.nombre = guias.nombre.Replace(".pdf", "");
                    response.ListDoc.Add(guias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamGuias GetGuiasAs()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guias;
            response.ListDoc = new List<ParamGuias>();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM GUIAS where IND_ESTADO = 1 ORDER BY SECTOR ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guias = new ParamGuias();
                    guias.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_GUIA"]) ? 0 : double.Parse(rdr["CODIGO_GUIA"].ToString());
                    guias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guias.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guias.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guias.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guias.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    guias.fecha = guias.fecha.Substring(0, 10);
                    guias.nombre = guias.nombre.Replace(".pdf", "");
                    response.ListDoc.Add(guias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public Response GuardarArchvio(ref ParamGuias guia, string tabla)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_nombre = new OracleParameter("fa_Nombre_doc", OracleDbType.Varchar2, guia.nombre, ParameterDirection.Input);
            var pi_url = new OracleParameter("fa_path", OracleDbType.Varchar2, guia.path, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, guia.sector, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Double, guia.ind_estado, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_url);
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_estado);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                var sp = (tabla == "GUIA") ? "sp_insert_doc_guias" : "sp_insert_doc_tablas";
                ora.ExecuteProcedureNonQuery(sp);
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.GuardarArchvio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response EditarArchvio(ref ParamGuias guia, string tabla)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo_doc", OracleDbType.Double, guia.codigo_guia, ParameterDirection.Input);
            var pi_nombre = new OracleParameter("fa_Nombre_doc", OracleDbType.Varchar2, guia.nombre, ParameterDirection.Input);
            var pi_url = new OracleParameter("fa_path", OracleDbType.Varchar2, guia.path, ParameterDirection.Input);
            var pi_sector = new OracleParameter("fa_sector", OracleDbType.Varchar2, guia.sector, ParameterDirection.Input);
            var pi_estado = new OracleParameter("fa_estado", OracleDbType.Double, guia.ind_estado, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_url);
                ora.AddParameter(pi_sector);
                ora.AddParameter(pi_estado);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                var sp = (tabla == "GUIA") ? "sp_upd_doc_guias" : "sp_upd_doc_tablas";
                ora.ExecuteProcedureNonQuery(sp);
                //Respuesta del procedimiento
                response.errorCode = ora.GetParameter("fa_Error").ToString();
                response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();

            }
            catch (Exception ex)
            {
                throw new Exception("ParamsDAO.EditarArchvio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public Response EliminarArchvio(double codigo, string tabla)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();

            var pi_codigo = new OracleParameter("fa_codigo_doc", OracleDbType.Double, codigo, ParameterDirection.Input);

            var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
            var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
            try
            {
                po_ErrorMessage.Size = 200;
                ora.AddParameter(pi_codigo);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                var sp = (tabla == "GUIA") ? "sp_delete_doc_guias" : "sp_delete_doc_tablas";
                ora.ExecuteProcedureNonQuery(sp);
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
        public ParamGuias GetIdGuias(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guia = new ParamGuias();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM GUIAS ";
                command += string.Format("WHERE CODIGO_GUIA  = {0}", codigo);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guia = new ParamGuias();
                    guia.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_GUIA"]) ? 0 : double.Parse(rdr["CODIGO_GUIA"].ToString());
                    guia.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guia.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guia.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guia.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guia.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                }
                rdr.Close();

                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return guia;
        }
        public OutParamGuias GetTablas()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guias;
            response.ListDoc = new List<ParamGuias>();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM tABLAS ORDER BY SECTOR ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guias = new ParamGuias();
                    guias.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_TABLA"]) ? 0 : double.Parse(rdr["CODIGO_TABLA"].ToString());
                    guias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guias.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guias.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guias.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guias.fecha = guias.fecha.Substring(0, 10);
                    guias.nombre = guias.nombre.Replace(".pdf", "");
                    guias.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    response.ListDoc.Add(guias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutParamGuias GetTablasAs()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guias;
            response.ListDoc = new List<ParamGuias>();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM TABLAS WHERE IND_ESTADO = 1 ORDER BY SECTOR ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guias = new ParamGuias();
                    guias.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_TABLA"]) ? 0 : double.Parse(rdr["CODIGO_TABLA"].ToString());
                    guias.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guias.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guias.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guias.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guias.fecha = guias.fecha.Substring(0, 10);
                    guias.nombre = guias.nombre.Replace(".pdf", "");
                    guias.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                    response.ListDoc.Add(guias);
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetTablas", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public ParamGuias GetIdTablas(double codigo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamGuias response = new OutParamGuias();
            var ora = new OracleServer(connectionString);

            ParamGuias guia = new ParamGuias();
            string command = string.Empty;

            try
            {
                command = " SELECT * FROM TABLAS ";
                command += string.Format("WHERE CODIGO_TABLA  = {0}", codigo);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    guia = new ParamGuias();
                    guia.codigo_guia = DBNull.Value.Equals(rdr["CODIGO_TABLA"]) ? 0 : double.Parse(rdr["CODIGO_TABLA"].ToString());
                    guia.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    guia.nombre = DBNull.Value.Equals(rdr["NOMBRE_DOC"]) ? "" : (rdr["NOMBRE_DOC"].ToString());
                    guia.path = DBNull.Value.Equals(rdr["URL_DOC"]) ? "" : (rdr["URL_DOC"].ToString());
                    guia.fecha = DBNull.Value.Equals(rdr["FECHA_ACT"]) ? "" : (rdr["FECHA_ACT"].ToString());
                    guia.ind_estado = DBNull.Value.Equals(rdr["IND_ESTADO"]) ? 0 : double.Parse(rdr["IND_ESTADO"].ToString());
                }
                rdr.Close();
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("DocumentsDAO.GetGuias", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return guia;
        }

    }
}
