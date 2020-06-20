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
    public class PqrDAO
    {
        public OutFlowType GetFlows()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutFlowType response = new OutFlowType();
            var ora = new OracleServer(connectionString);

            FlowType flow;
            List<FlowType> list = new List<FlowType>();
            string command = string.Empty;

            try
            {
                command = "SELECT BBS_WFG_TIPO_FLUJO.TIPO_FLUJO,  BBS_WFG_TIPO_FLUJO.NOMBRE_FLUJO FROM   BBS_WFG_TIPO_FLUJO ";
                command = command + " WHERE  BBS_WFG_TIPO_FLUJO.IND_VER_DESDE_PORTAL = 1 AND BBS_WFG_TIPO_FLUJO.ESTADO_FLUJO = 1 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    flow = new FlowType();
                    flow.flowType = DBNull.Value.Equals(rdr["TIPO_FLUJO"]) ? 0 : int.Parse(rdr["TIPO_FLUJO"].ToString());
                    flow.flowName = DBNull.Value.Equals(rdr["NOMBRE_FLUJO"]) ? string.Empty : rdr["NOMBRE_FLUJO"].ToString();
                    list.Add(flow);
                }
                rdr.Close();
                response.lstFlowType = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetFlows", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;

        }


        public OutStates GetStates()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutStates response = new OutStates();
            var ora = new OracleServer(connectionString);

            State state;
            List<State> list = new List<State>();
            string command = string.Empty;

            try
            {
                command = "SELECT TIPO_FLUJO, NOMBRE_FLUJO, CODIGO_ESTADO, NOMBRE_ESTADO FROM BBS_LIQCOM_V_EDO_PQR ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    state = new State();
                    state.flowType = DBNull.Value.Equals(rdr["TIPO_FLUJO"]) ? 0 : int.Parse(rdr["TIPO_FLUJO"].ToString());
                    state.flowName = DBNull.Value.Equals(rdr["NOMBRE_FLUJO"]) ? string.Empty : rdr["NOMBRE_FLUJO"].ToString();
                    state.codeState = DBNull.Value.Equals(rdr["CODIGO_ESTADO"]) ? 0 : int.Parse(rdr["CODIGO_ESTADO"].ToString());
                    state.stateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO"]) ? string.Empty : rdr["NOMBRE_ESTADO"].ToString();
                    list.Add(state);
                }
                rdr.Close();
                response.lstStates = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetStates", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;

        }

        public OutJustification GetJustification(int flowType)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutJustification response = new OutJustification();
            var ora = new OracleServer(connectionString);

            Justification justification;
            List<Justification> list = new List<Justification>();
            string command = string.Empty;

            try
            {
                command = "SELECT BBS_WFG_TIPO_JUSTIFICA.TIPO_REQUISITO, BBS_WFG_TIPO_JUSTIFICA.NOMBRE_REQUISITO FROM BBS_WFG_TIPO_JUSTIFICA ";
                command = command + string.Format(" WHERE BBS_WFG_TIPO_JUSTIFICA.TIPO_FLUJO = {0} ", flowType);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    justification = new Justification();
                    justification.justificationType = DBNull.Value.Equals(rdr["TIPO_REQUISITO"]) ? 0 : int.Parse(rdr["TIPO_REQUISITO"].ToString());
                    justification.justificationName = DBNull.Value.Equals(rdr["NOMBRE_REQUISITO"]) ? string.Empty : rdr["NOMBRE_REQUISITO"].ToString();
                    list.Add(justification);
                }
                rdr.Close();
                response.lstJustification = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetJustification", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;

        }

        public OutHeaderPQR GetHeaderPQR(string executiveID, int reportType, DateTime startDate, DateTime endDate, string loanNumber, string PQRnumber, 
            int flowType, string status, string childs)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutHeaderPQR response = new OutHeaderPQR();
            var ora = new OracleServer(connectionString);

            HeaderPQR justification;
            List<HeaderPQR> list = new List<HeaderPQR>();
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_PROCESO, NOMBRE_PROCESO, TIPO_FLUJO, NOMBRE_FLUJO, ESTADO, LLAVE_PROCESO, NUI, NUMERO_CARPETA, NRO_CREDITO, " +
                    "FECHA_INSERTA_NEGOCIO, USUARIO_INSERTA, FECHA_INSERTA_SYS, TIPO_LLAVE, NOMBRE_CLIENTE, NOMBRE_ESTADO, PRIORIDAD_MANUAL, " +
                    "NOMBRE_PRIORIDAD, CEDULA, ANALISTA_ASIGNADO, CEDULA_ASESOR_RADICA, FECHA_CIERRE, CODIGO_JUSTIFICACION , MOTIVO, PROMOTOR_NOMBRE FROM BBS_WFG_V_PQR_CAB WHERE";

                switch (reportType)
                {
                    case 1:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}' AND NUMERO_PROCESO = {1}", executiveID, PQRnumber);
                        break;
                    case 2:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}' AND NRO_CREDITO = {1}", executiveID, loanNumber);
                        break;
                    case 3:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}' AND FECHA_DESEMBOLSO BETWEEN to_date('{1}', 'dd-mm-yyyy')  AND to_date('{2}', 'dd-mm-yyyy')", executiveID, startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 4:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}' AND FECHA_PAGO BETWEEN to_date('{1}', 'dd-mm-yyyy')  AND to_date('{2}', 'dd-mm-yyyy')", executiveID, startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 5:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}'AND TIPO_FLUJO  = {1}", executiveID, flowType);
                        break;
                    case 6:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA = '{0}' AND TIPO_FLUJO  = {1} AND ESTADO ={2} ", executiveID, flowType, status);
                        break;
                    case 7:
                        command = command + string.Format(" CEDULA_ASESOR_RADICA in  ({0}) ", childs);
                        break;

                }

                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    justification = new HeaderPQR();
                    justification.processNumber = DBNull.Value.Equals(rdr["NUMERO_PROCESO"]) ? 0 : int.Parse(rdr["NUMERO_PROCESO"].ToString());
                    justification.processName = DBNull.Value.Equals(rdr["NOMBRE_PROCESO"]) ? string.Empty : rdr["NOMBRE_PROCESO"].ToString();
                    justification.flowType = DBNull.Value.Equals(rdr["TIPO_FLUJO"]) ? 0 : int.Parse(rdr["TIPO_FLUJO"].ToString());
                    justification.flowName = DBNull.Value.Equals(rdr["NOMBRE_FLUJO"]) ? string.Empty : rdr["NOMBRE_FLUJO"].ToString();
                    justification.state = DBNull.Value.Equals(rdr["ESTADO"]) ? 0 : int.Parse(rdr["ESTADO"].ToString());
                    justification.processKey = DBNull.Value.Equals(rdr["LLAVE_PROCESO"]) ? 0 : int.Parse(rdr["LLAVE_PROCESO"].ToString());
                    justification.nui = DBNull.Value.Equals(rdr["NUI"]) ? 0 : double.Parse(rdr["NUI"].ToString());
                    justification.folderNumber = DBNull.Value.Equals(rdr["NUMERO_CARPETA"]) ? 0 : int.Parse(rdr["NUMERO_CARPETA"].ToString());
                    justification.loanNumber = DBNull.Value.Equals(rdr["NRO_CREDITO"]) ? 0 : int.Parse(rdr["NRO_CREDITO"].ToString());
                    justification.dateInsertBusiness = DBNull.Value.Equals(rdr["FECHA_INSERTA_NEGOCIO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_NEGOCIO"].ToString()).ToString("dd/MM/yyyy");
                    justification.userInsert = DBNull.Value.Equals(rdr["USUARIO_INSERTA"]) ? string.Empty : rdr["USUARIO_INSERTA"].ToString();
                    justification.dateInsertSYS = DBNull.Value.Equals(rdr["FECHA_INSERTA_SYS"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_SYS"].ToString()).ToString("dd/MM/yyyy");
                    justification.keyType = DBNull.Value.Equals(rdr["TIPO_LLAVE"]) ? 0 : int.Parse(rdr["TIPO_LLAVE"].ToString());
                    justification.customerName = DBNull.Value.Equals(rdr["NOMBRE_CLIENTE"]) ? string.Empty : rdr["NOMBRE_CLIENTE"].ToString();
                    justification.stateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO"]) ? string.Empty : rdr["NOMBRE_ESTADO"].ToString();
                    justification.priorityManual = DBNull.Value.Equals(rdr["PRIORIDAD_MANUAL"]) ? 0 : int.Parse(rdr["PRIORIDAD_MANUAL"].ToString());
                    justification.priorityName = DBNull.Value.Equals(rdr["NOMBRE_PRIORIDAD"]) ? string.Empty : rdr["NOMBRE_PRIORIDAD"].ToString();
                    justification.documentID = DBNull.Value.Equals(rdr["CEDULA"]) ? string.Empty : rdr["CEDULA"].ToString();
                    justification.AssignedAnalyst = DBNull.Value.Equals(rdr["ANALISTA_ASIGNADO"]) ? string.Empty : rdr["ANALISTA_ASIGNADO"].ToString();
                    justification.closeDate = DBNull.Value.Equals(rdr["FECHA_CIERRE"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_CIERRE"].ToString()).ToString("dd/MM/yyyy");
                    justification.justificationCode = DBNull.Value.Equals(rdr["CODIGO_JUSTIFICACION"]) ? 0 : int.Parse(rdr["CODIGO_JUSTIFICACION"].ToString());
                    justification.reason = DBNull.Value.Equals(rdr["MOTIVO"]) ? string.Empty : rdr["MOTIVO"].ToString();
                    justification.executiveName = DBNull.Value.Equals(rdr["PROMOTOR_NOMBRE"]) ? string.Empty : rdr["PROMOTOR_NOMBRE"].ToString();
                    list.Add(justification);
                }
                rdr.Close();
                response.lstHeaderPQR = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetJustification", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;



        }

        public OutLogPQR GetLogPQR(int processNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLogPQR response = new OutLogPQR();
            var ora = new OracleServer(connectionString);

            LogPQR log;
            List<LogPQR> list = new List<LogPQR>();
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_PROCESO, SECUENCIA_NOVEDAD, ESTADO_ANTERIOR, ESTADO_NUEVO, DESCRIPCION, FECHA_INSERTA_NEGOCIO, USUARIO_INSERTA,  ";
                command = command + "FECHA_INSERTA_SYS, NOMBRE_ESTADO_NVO,  NOMBRE_ESTADO_ANT ";
                command = command + string.Format("FROM  BBS_WFG_V_PQR_LOG WHERE NUMERO_PROCESO = {0}  ", processNumber.ToString());
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    log = new LogPQR();
                    log.processNumber = DBNull.Value.Equals(rdr["NUMERO_PROCESO"]) ? 0 : int.Parse(rdr["NUMERO_PROCESO"].ToString());
                    log.SequenceNovelty = DBNull.Value.Equals(rdr["SECUENCIA_NOVEDAD"]) ? 0 : int.Parse(rdr["SECUENCIA_NOVEDAD"].ToString());
                    log.previousState = DBNull.Value.Equals(rdr["ESTADO_ANTERIOR"]) ? 0 : int.Parse(rdr["ESTADO_ANTERIOR"].ToString());
                    log.newState = DBNull.Value.Equals(rdr["ESTADO_NUEVO"]) ? 0 : int.Parse(rdr["ESTADO_NUEVO"].ToString());
                    log.description = DBNull.Value.Equals(rdr["DESCRIPCION"]) ? string.Empty : rdr["DESCRIPCION"].ToString();
                    log.dateInsertBusiness = DBNull.Value.Equals(rdr["FECHA_INSERTA_NEGOCIO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_NEGOCIO"].ToString()).ToString("dd/MM/yyyy");
                    log.userInsert = DBNull.Value.Equals(rdr["USUARIO_INSERTA"]) ? string.Empty : rdr["USUARIO_INSERTA"].ToString();
                    log.dateInsertSYS = DBNull.Value.Equals(rdr["FECHA_INSERTA_SYS"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_SYS"].ToString()).ToString("dd/MM/yyyy");
                    log.statusNameNew = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_NVO"]) ? string.Empty : rdr["NOMBRE_ESTADO_NVO"].ToString();
                    log.statusNamePrevious = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_ANT"]) ? string.Empty : rdr["NOMBRE_ESTADO_ANT"].ToString();
                    list.Add(log);
                }
                rdr.Close();
                response.lstLogPQR = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetLogPQR", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;

        }

        public OutNoveltyPQR GetNoveltyPQR(int processNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutNoveltyPQR response = new OutNoveltyPQR();
            var ora = new OracleServer(connectionString);

            NoveltyPQR novelty;
            List<NoveltyPQR> list = new List<NoveltyPQR>();
            string command = string.Empty;

            try
            {
                command = "SELECT CREDITO, NUMERO_PROCESO, SECUENCIA_NOVEDAD,  CODIGO_NOVEDAD, DESCRIPCION, FECHA_INSERTA_NEGOCIO, USUARIO_INSERTA, FECHA_INSERTA_SYS,   ";
                command = command + string.Format("NOMBRE_NOVEDAD, FLUJO, PROMOTOR_NOMBRE FROM BBS_WFG_V_PQR_NOVEDAD WHERE NUMERO_PROCESO = {0}  ", processNumber.ToString());
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    novelty = new NoveltyPQR();
                    novelty.loanNumber = DBNull.Value.Equals(rdr["CREDITO"]) ? 0 : int.Parse(rdr["CREDITO"].ToString());
                    novelty.processNumber = DBNull.Value.Equals(rdr["NUMERO_PROCESO"]) ? 0 : int.Parse(rdr["NUMERO_PROCESO"].ToString());
                    novelty.SequenceNovelty = DBNull.Value.Equals(rdr["SECUENCIA_NOVEDAD"]) ? 0 : int.Parse(rdr["SECUENCIA_NOVEDAD"].ToString());
                    novelty.noveltyCode = DBNull.Value.Equals(rdr["CODIGO_NOVEDAD"]) ? 0 : int.Parse(rdr["CODIGO_NOVEDAD"].ToString());
                    novelty.description = DBNull.Value.Equals(rdr["DESCRIPCION"]) ? string.Empty : rdr["DESCRIPCION"].ToString();
                    novelty.dateInsertBusiness = DBNull.Value.Equals(rdr["FECHA_INSERTA_NEGOCIO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_NEGOCIO"].ToString()).ToString("dd/MM/yyyy");
                    novelty.userInsert = DBNull.Value.Equals(rdr["USUARIO_INSERTA"]) ? string.Empty : rdr["USUARIO_INSERTA"].ToString();
                    novelty.dateInsertSYS = DBNull.Value.Equals(rdr["FECHA_INSERTA_SYS"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INSERTA_SYS"].ToString()).ToString("dd/MM/yyyy");
                    novelty.NoveltyName = DBNull.Value.Equals(rdr["NOMBRE_NOVEDAD"]) ? string.Empty : rdr["NOMBRE_NOVEDAD"].ToString();
                    novelty.flow = DBNull.Value.Equals(rdr["FLUJO"]) ? string.Empty : rdr["FLUJO"].ToString();
                    novelty.executiveName = DBNull.Value.Equals(rdr["PROMOTOR_NOMBRE"]) ? string.Empty : rdr["PROMOTOR_NOMBRE"].ToString();
                    list.Add(novelty);
                }
                rdr.Close();
                response.lstNoveltyPQR = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetNoveltyPQR", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public Response CreatePQR(InCreatePQR input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response response = new Response();
            string esNuloError = string.Empty;

            try
            {
                var pi_company = new OracleParameter("fa_empresa", OracleDbType.Double, input.company, ParameterDirection.Input);
                ora.AddParameter(pi_company);

                var pi_flowType = new OracleParameter("fa_tipo_flujo", OracleDbType.Double, input.flowType, ParameterDirection.Input);
                ora.AddParameter(pi_flowType);

                var pi_reason = new OracleParameter("fa_motivo", OracleDbType.Double, input.reason, ParameterDirection.Input);
                ora.AddParameter(pi_reason);

                var pi_executiveID = new OracleParameter("fa_cedula_asesor", OracleDbType.Varchar2, input.executiveID, ParameterDirection.Input);
                ora.AddParameter(pi_executiveID);

                var pi_customerID = new OracleParameter("fa_cedula_cliente", OracleDbType.Varchar2, input.customerID, ParameterDirection.Input);
                ora.AddParameter(pi_customerID);

                var pi_description = new OracleParameter("fa_breve_descripcion", OracleDbType.Varchar2, input.description, ParameterDirection.Input);
                ora.AddParameter(pi_description);

                var pi_loanNumber = new OracleParameter("fa_numero_credito", OracleDbType.Varchar2, input.loanNumber, ParameterDirection.Input);
                ora.AddParameter(pi_loanNumber);


                var pi_PQRnumber = new OracleParameter("fa_numero_pqrs", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(pi_PQRnumber);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_PQRS_F_CREAR_CASO");



                if (ora.GetParameter("fa_Error") == null)
                {
                    esNuloError = "fa_error es nulo";
                }
                else
                {
                    response.errorCode = ora.GetParameter("fa_Error").ToString();
                }

                if (ora.GetParameter("fa_Descripcion_Error") == null)
                {
                    esNuloError = esNuloError + "fa_descripcion_error es nulo";
                }
                else
                {
                    response.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
                }



                if (ora.GetParameter("fa_numero_pqrs") == null)
                {
                    esNuloError = esNuloError + "fa_numero_pqrs es nulo";
                }
                else
                {
                    response.errorMessage = "Se ha creado el PQR con identificador: " + ora.GetParameter("fa_numero_pqrs").ToString();

                }
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


        public OutLoanResume GetLoanResume(double loanNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLoanResume response = new OutLoanResume();
            var ora = new OracleServer(connectionString);
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_CREDITO, FECHA_INICIO_CREDITO, MONTO, VALOR_DESEMBOSO, CODIGO_ASESOR," +
                  " CODIGO_SUCURSAL, NOMBRE_CODIGO_SUCURSAL, CEDULA_ASESOR FROM BBS_LIQCOM_V_CREDITOSMX ";
                command = command + string.Format(" WHERE NUMERO_CREDITO = {0}  ", loanNumber.ToString());
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    response.loanNumber = DBNull.Value.Equals(rdr["NUMERO_CREDITO"]) ? 0 : double.Parse(rdr["NUMERO_CREDITO"].ToString());
                    response.startDate = DBNull.Value.Equals(rdr["FECHA_INICIO_CREDITO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_INICIO_CREDITO"].ToString()).ToString("dd/MM/yyyy");
                    response.amount = DBNull.Value.Equals(rdr["MONTO"]) ? 0 : double.Parse(rdr["MONTO"].ToString());
                    response.disbursement = DBNull.Value.Equals(rdr["VALOR_DESEMBOSO"]) ? 0 : double.Parse(rdr["VALOR_DESEMBOSO"].ToString());
                    response.executiveCode = DBNull.Value.Equals(rdr["CODIGO_ASESOR"]) ? 0 : double.Parse(rdr["CODIGO_ASESOR"].ToString());
                    response.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    response.branchName = DBNull.Value.Equals(rdr["NOMBRE_CODIGO_SUCURSAL"]) ? string.Empty : rdr["NOMBRE_CODIGO_SUCURSAL"].ToString();
                    response.documentID = DBNull.Value.Equals(rdr["CEDULA_ASESOR"]) ? string.Empty : rdr["CEDULA_ASESOR"].ToString();
                }
                rdr.Close();

                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetLoanResume", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutSummaryPQR GetSummaryPQR(string  executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutSummaryPQR response = new OutSummaryPQR();
            List<SummaryPQR> summaryList  = new List<SummaryPQR>();
            var ora = new OracleServer(connectionString);
            string command = string.Empty;

            try
            {
                command = "SELECT TO_CHAR(BBS_LIQCOM_V_PQRS.FECHA_INSERTA_SYS,'MM/YYYY') AS MES, BBS_LIQCOM_V_PQRS.NOMBRE_PROCESO, BBS_LIQCOM_V_PQRS.ESTADO, " +
                           "BBS_LIQCOM_V_PQRS.NOMBRE_ESTADO, COUNT(*) AS NRO_ACLARACIONES FROM BBS_LIQCOM_V_PQRS ";
                command = command + string.Format(" WHERE CEDULA = '{0}'", executiveID);
                command = command + "GROUP BY TO_CHAR(BBS_LIQCOM_V_PQRS.FECHA_INSERTA_SYS, 'MM/YYYY'), BBS_LIQCOM_V_PQRS.NOMBRE_PROCESO, ";
                command = command + "BBS_LIQCOM_V_PQRS.ESTADO, BBS_LIQCOM_V_PQRS.NOMBRE_ESTADO ORDER BY TO_CHAR(BBS_LIQCOM_V_PQRS.FECHA_INSERTA_SYS,'MM/YYYY'), ";
                command = command + "BBS_LIQCOM_V_PQRS.NOMBRE_PROCESO, BBS_LIQCOM_V_PQRS.ESTADO"; 
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    SummaryPQR summary = new SummaryPQR();
                    summary.month = DBNull.Value.Equals(rdr["MES"]) ? string.Empty : rdr["MES"].ToString();
                    summary.processName = DBNull.Value.Equals(rdr["NOMBRE_PROCESO"]) ? string.Empty : rdr["NOMBRE_PROCESO"].ToString();
                    summary.status = DBNull.Value.Equals(rdr["ESTADO"]) ? 0 : double.Parse(rdr["ESTADO"].ToString());
                    summary.stateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO"]) ? string.Empty : rdr["NOMBRE_ESTADO"].ToString();
                    summary.PQRNumber =  DBNull.Value.Equals(rdr["NRO_ACLARACIONES"]) ? 0 : double.Parse(rdr["NRO_ACLARACIONES"].ToString());
                    summaryList.Add(summary);


                }
                rdr.Close();

                response.lstSummaryPQR = summaryList;


                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("PqrDAO.GetLoanResume", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;

        }

    }
}
