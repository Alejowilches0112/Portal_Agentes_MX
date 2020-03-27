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
    public class ComplianceGoalDAO
    {
        public OutNextCategory GetNextCategory(string executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutNextCategory response = new OutNextCategory();

            try
            {
                var pi_documentID = new OracleParameter("fa_codigo_asesor", OracleDbType.Varchar2, executiveID, ParameterDirection.Input);
                ora.AddParameter(pi_documentID);

                var pi_amount = new OracleParameter("fa_MONTO_COLOCAR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(pi_amount);

                var pi_categoryCode = new OracleParameter("fa_CODIGO_CATEGORIA", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(pi_categoryCode);

                var pi_categoryName = new OracleParameter("fa_NOMBRE_CATEGORIA", OracleDbType.Varchar2, ParameterDirection.Output);
                pi_categoryName.Size = 100;
                ora.AddParameter(pi_categoryName);

                var pi_schemeCode = new OracleParameter("fa_codigo_esquema", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(pi_schemeCode);

                var pi_subscheme = new OracleParameter("fa_codigo_subesquema", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(pi_subscheme);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;


                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);


                ora.ExecuteProcedureNonQuery("BBS_LIQCOM2_F_BUSCA_SIG_CAT");

                response.msg = new Response();
                response.amount = double.Parse(ora.GetParameter("fa_MONTO_COLOCAR").ToString());
                response.categoryName = ora.GetParameter("fa_NOMBRE_CATEGORIA").ToString();
                response.msg.errorCode = ora.GetParameter("fa_Error").ToString();
                response.msg.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();


                ora.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetNextCategory", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }



        public OutAccumulatedLoan GetAccumulatedLoan(string executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutAccumulatedLoan response = new OutAccumulatedLoan();
            var ora = new OracleServer(connectionString);

            AccumulatedLoan accumulated;
            List<AccumulatedLoan> list = new List<AccumulatedLoan>();
            string command = string.Empty;

            try
            {
                command = "SELECT  BBS_LIQCOM_V_COLOCA.CEDULA_ASESOR, BBS_LIQCOM_V_COLOCA.MES, BBS_LIQCOM_V_COLOCA.MONTO, BBS_LIQCOM_V_COLOCA.NRO_CREDITOS ";
                command = command + string.Format(" FROM BBS_LIQCOM_V_COLOCA WHERE BBS_LIQCOM_V_COLOCA.CEDULA_ASESOR  = '{0}'  ", executiveID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    accumulated = new AccumulatedLoan();
                    accumulated.amount = DBNull.Value.Equals(rdr["MONTO"]) ? 0 : double.Parse(rdr["MONTO"].ToString());
                    accumulated.executiveID = DBNull.Value.Equals(rdr["CEDULA_ASESOR"]) ? string.Empty : rdr["CEDULA_ASESOR"].ToString();
                    accumulated.month = DBNull.Value.Equals(rdr["MES"]) ? string.Empty : rdr["MES"].ToString();
                    accumulated.loanCount = DBNull.Value.Equals(rdr["NRO_CREDITOS"]) ? 0 : double.Parse(rdr["NRO_CREDITOS"].ToString());
                    list.Add(accumulated);
                }
                rdr.Close();
                response.lstAccumulatedLoan = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetBalancesCommissions", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutAccumulatedClarifications GetAccumulatedClarifications(string executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutAccumulatedClarifications response = new OutAccumulatedClarifications();
            var ora = new OracleServer(connectionString);

            AccumulatedClarifications accumulated;
            List<AccumulatedClarifications> list = new List<AccumulatedClarifications>();
            string command = string.Empty;

            try
            {
                command = "SELECT MES,NUMERO_ACLARACIONES,NUMERO_RESPONDIDAS,NUMERO_PENDIENTES,PORCENTAJE_RESPONDIDAS,PORCENTAJE_PENDIENTES  ";
                command = command + string.Format(" FROM BBS_LIQCOM_V_PQR_RESUM WHERE BBS_LIQCOM_V_PQR_RESUM.CODIGO_ASESOR   = '{0}'  ", executiveID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    accumulated = new AccumulatedClarifications();
                    accumulated.month = DBNull.Value.Equals(rdr["MES"]) ? string.Empty : rdr["MES"].ToString();
                    accumulated.total = DBNull.Value.Equals(rdr["NUMERO_ACLARACIONES"]) ? 0 : double.Parse(rdr["NUMERO_ACLARACIONES"].ToString());
                    accumulated.answered = DBNull.Value.Equals(rdr["NUMERO_RESPONDIDAS"]) ? 0 : double.Parse(rdr["NUMERO_RESPONDIDAS"].ToString());
                    accumulated.pending = DBNull.Value.Equals(rdr["NUMERO_PENDIENTES"]) ? 0 : double.Parse(rdr["NUMERO_PENDIENTES"].ToString());
                    accumulated.percentageAnswered = DBNull.Value.Equals(rdr["PORCENTAJE_RESPONDIDAS"]) ? 0 : double.Parse(rdr["PORCENTAJE_RESPONDIDAS"].ToString());
                    accumulated.percentagePending = DBNull.Value.Equals(rdr["PORCENTAJE_PENDIENTES"]) ? 0 : double.Parse(rdr["PORCENTAJE_PENDIENTES"].ToString());
                    list.Add(accumulated);
                }
                rdr.Close();
                response.lstAccumulatedClarifications = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetAccumulatedClarifications", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutGoalExecutive GetGoalExecutive(string executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutGoalExecutive response = new OutGoalExecutive();
            var ora = new OracleServer(connectionString);

            try
            {
                var po_executiveID = new OracleParameter("fa_cedula_asesor", OracleDbType.Varchar2, executiveID, ParameterDirection.Input);
                ora.AddParameter(po_executiveID);

                var po_goalValue = new OracleParameter("fa_META_VLR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalValue);

                var po_complianceValue = new OracleParameter("fa_CUMPLIMIENTO_VLR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceValue);

                var po_compliancePercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_compliancePercentage);

                var po_goalNewValue = new OracleParameter("fa_META_VLR_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalNewValue);

                var po_complianceNewValue = new OracleParameter("fa_CUMPLIMIENTO_VLR_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceNewValue);

                var po_complianceNewPercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceNewPercentage);

                var po_goalRenovatedValue = new OracleParameter("fa_META_VLR_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalRenovatedValue);

                var po_complianceRenovatedValue = new OracleParameter("fa_CUMPLIMIENTO_VLR_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceRenovatedValue);

                var po_complianceRenovatedPercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceRenovatedPercentage);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_LIQCOM2_F_META_ASESOR");
                response.goalExecutive = new GoalExecutive();

                response.goalExecutive.goalValue = double.Parse(ora.GetParameter("fa_META_VLR").ToString());
                response.goalExecutive.complianceValue = double.Parse(ora.GetParameter("fa_CUMPLIMIENTO_VLR").ToString());
                response.goalExecutive.compliancePercentage = double.Parse(ora.GetParameter("fa_CUMPLIMIENTO_PORC").ToString());

            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetGoalExecutive", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }


        public OutProductivity GetProductivity(string executiveID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutProductivity response = new OutProductivity();
            var ora = new OracleServer(connectionString);

            Productivity productivity;
            List<Productivity> list = new List<Productivity>();
            string command = string.Empty;

            try
            {
                command = "SELECT FIGURA,NRO_CREDITOS,MONTO_CREDITOS,PROMEDIO FROM BBS_LIQCOM_V_PROD_JEFE WHERE  ";
                command = command + string.Format(" CEDULA_ASESOR = '{0}'  ", executiveID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    productivity = new Productivity();
                    productivity.Figure = DBNull.Value.Equals(rdr["FIGURA"]) ? string.Empty : rdr["FIGURA"].ToString();
                    productivity.LoanCount = DBNull.Value.Equals(rdr["NRO_CREDITOS"]) ? 0 : double.Parse(rdr["NRO_CREDITOS"].ToString());
                    productivity.LoanAmount = DBNull.Value.Equals(rdr["MONTO_CREDITOS"]) ? 0 : double.Parse(rdr["MONTO_CREDITOS"].ToString());
                    productivity.Average = DBNull.Value.Equals(rdr["PROMEDIO"]) ? 0 : double.Parse(rdr["PROMEDIO"].ToString());
                    list.Add(productivity);
                }
                rdr.Close();
                response.lstProductivity = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetProductivity", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }


        public OutCategoryRange GetCategoryRange()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCategoryRange response = new OutCategoryRange();
            var ora = new OracleServer(connectionString);

            CategoryRange categoryRange;
            List<CategoryRange> list = new List<CategoryRange>();
            string command = string.Empty;

            try
            {
                command = "SELECT bbs_liqcom_v_par_categoria.codigo_esquema, bbs_liqcom_v_par_categoria.nombre_esquema,  bbs_liqcom_v_par_categoria.rango_inicial, bbs_liqcom_v_par_categoria.rango_final,  " +
                    " bbs_liqcom_v_par_categoria.codigo_categoria,  bbs_liqcom_v_par_categoria.nombre_categoria   FROM bbs_liqcom_v_par_categoria    ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    categoryRange = new CategoryRange();
                    categoryRange.SchemeCode = DBNull.Value.Equals(rdr["codigo_esquema"]) ? 0 : double.Parse(rdr["codigo_esquema"].ToString());
                    categoryRange.SchemeName = DBNull.Value.Equals(rdr["nombre_esquema"]) ? string.Empty : rdr["nombre_esquema"].ToString();
                    categoryRange.InitialRange = DBNull.Value.Equals(rdr["rango_inicial"]) ? 0 : double.Parse(rdr["rango_inicial"].ToString());
                    categoryRange.FinalRange = DBNull.Value.Equals(rdr["rango_final"]) ? 0 : double.Parse(rdr["rango_final"].ToString());
                    categoryRange.CategoryCode = DBNull.Value.Equals(rdr["codigo_categoria"]) ? 0 : double.Parse(rdr["codigo_categoria"].ToString());
                    categoryRange.CategoryName = DBNull.Value.Equals(rdr["nombre_categoria"]) ? string.Empty : rdr["nombre_categoria"].ToString();
                    list.Add(categoryRange);
                }
                rdr.Close();
                response.lstCategoryRange = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetCategoryRange", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }


        public OutGoalSupervisor GetGoalSupervisor(string supervisorID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutGoalSupervisor response = new OutGoalSupervisor();
            var ora = new OracleServer(connectionString);

            try
            {
                var po_executiveID = new OracleParameter("fa_cedula_asesor", OracleDbType.Varchar2, supervisorID, ParameterDirection.Input);
                ora.AddParameter(po_executiveID);

                var po_goalValue = new OracleParameter("fa_META_VLR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalValue);

                var po_complianceValue = new OracleParameter("fa_CUMPLIMIENTO_VLR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceValue);

                var po_compliancePercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_compliancePercentage);

                var po_goalNewValue = new OracleParameter("fa_META_VLR_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalNewValue);

                var po_complianceNewValue = new OracleParameter("fa_CUMPLIMIENTO_VLR_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceNewValue);

                var po_complianceNewPercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceNewPercentage);

                var po_goalRenovatedValue = new OracleParameter("fa_META_VLR_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_goalRenovatedValue);

                var po_complianceRenovatedValue = new OracleParameter("fa_CUMPLIMIENTO_VLR_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceRenovatedValue);

                var po_complianceRenovatedPercentage = new OracleParameter("fa_CUMPLIMIENTO_PORC_RENOVADO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_complianceRenovatedPercentage);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_LIQCOM2_F_META_JEFE");
                response.goalSupervisor = new GoalSupervisor();

                response.goalSupervisor.goalValue = double.Parse(ora.GetParameter("fa_META_VLR").ToString());
                response.goalSupervisor.complianceValue = double.Parse(ora.GetParameter("fa_CUMPLIMIENTO_VLR").ToString());
                response.goalSupervisor.compliancePercentage = double.Parse(ora.GetParameter("fa_CUMPLIMIENTO_PORC").ToString());

            }
            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetGoalSupervisor", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutProgressExecutive GetProgressExecutive(string executiveID, DateTime startDate, DateTime endDate)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutProgressExecutive response = new OutProgressExecutive();
            var ora = new OracleServer(connectionString);

            try
            {
                var po_executiveID = new OracleParameter("fa_cedula_asesor", OracleDbType.Varchar2, executiveID, ParameterDirection.Input);
                ora.AddParameter(po_executiveID);
                var po_iniDate = new OracleParameter("fa_fecha_ini", OracleDbType.Date, startDate, ParameterDirection.Input);
                ora.AddParameter(po_iniDate);
                var po_endDate = new OracleParameter("fa_fecha_fin", OracleDbType.Date, endDate, ParameterDirection.Input);
                ora.AddParameter(po_endDate);

                var po_newLoanCount = new OracleParameter("fa_nro_credito_nuevos", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_newLoanCount);
                var po_NewDisbursementAmount = new OracleParameter("fa_valor_desembolso_nuevos", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_NewDisbursementAmount);
                var po_NewLoanValue = new OracleParameter("fa_valor_credito_nuevos", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_NewLoanValue);

                var po_RenovatedLoanCount = new OracleParameter("fa_nro_credito_renovados", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_RenovatedLoanCount);
                var po_RenovatedDisbursementAmount = new OracleParameter("fa_valor_desembolso_renovados", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_RenovatedDisbursementAmount);
                var po_RenovatedLoanValue = new OracleParameter("fa_valor_credito_renovados", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_RenovatedLoanValue);

                var po_LoanCount = new OracleParameter("fa_nro_creditos", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_LoanCount);
                var po_DisbursementAmount = new OracleParameter("fa_valor_desembolso", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_DisbursementAmount);
                var po_LoanValue = new OracleParameter("fa_valor_credito", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_LoanValue);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_LIQCOM2_F_COLOCA_RESUM");
                response.progressExecutive = new ProgressExecutive();

                response.progressExecutive.newLoanCount = double.Parse(ora.GetParameter("fa_nro_credito_nuevos").ToString());
                response.progressExecutive.NewDisbursementAmount = double.Parse(ora.GetParameter("fa_valor_desembolso_nuevos").ToString());
                response.progressExecutive.NewLoanValue = double.Parse(ora.GetParameter("fa_valor_credito_nuevos").ToString());

                response.progressExecutive.RenovatedLoanCount = double.Parse(ora.GetParameter("fa_nro_credito_renovados").ToString());
                response.progressExecutive.RenovatedDisbursementAmount = double.Parse(ora.GetParameter("fa_valor_desembolso_renovados").ToString());
                response.progressExecutive.RenovatedLoanValue = double.Parse(ora.GetParameter("fa_valor_credito_renovados").ToString());

                response.progressExecutive.LoanCount = double.Parse(ora.GetParameter("fa_nro_creditos").ToString());
                response.progressExecutive.DisbursementAmount = double.Parse(ora.GetParameter("fa_valor_desembolso").ToString());
                response.progressExecutive.LoanValue = double.Parse(ora.GetParameter("fa_valor_credito").ToString());

            }

            catch (Exception ex)
            {
                throw new Exception("ComplianceGoalDAO.GetProgressExecutive", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
