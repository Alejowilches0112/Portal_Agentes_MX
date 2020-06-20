using Entities;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace DAO
{
    public class SimulatorDAO
    {
        public OutCategorySimulation GetCategorySimulation(InCategorySimulation input)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutCategorySimulation response = new OutCategorySimulation();

            try
            {
                var pi_executiveCode = new OracleParameter("fa_codigo_asesor", OracleDbType.Varchar2, input.executiveCode, ParameterDirection.Input);
                ora.AddParameter(pi_executiveCode);

                var pi_amount = new OracleParameter("fa_monto_digitado", OracleDbType.Double, input.amount, ParameterDirection.Input);
                ora.AddParameter(pi_amount);

                var pi_currentInd = new OracleParameter("fa_ind_actual", OracleDbType.Double, 0, ParameterDirection.Input);
                ora.AddParameter(pi_currentInd);
                
                var po_case = new OracleParameter("fa_CASO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_case);

                var po_previousFigure = new OracleParameter("fa_FIGURA_ANTERIOR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_previousFigure);

                var po_previousFigureName = new OracleParameter("fa_NOMBRE_FIGURA_ANTERIOR", OracleDbType.Varchar2, ParameterDirection.Output);
                ora.AddParameter(po_previousFigureName);

                var po_currentFigure = new OracleParameter("fa_FIGURA_ACTUAL", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_currentFigure);

                var po_currentFigureName = new OracleParameter("fa_NOMBRE_FIGURA_ACTUAL", OracleDbType.Varchar2, ParameterDirection.Output);
                ora.AddParameter(po_currentFigureName);

                var po_iniDayFigAnt = new OracleParameter("fa_DIA_INI_ALTA_FIG_ACT", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_iniDayFigAnt);

                var po_endDayFigAnt = new OracleParameter("fa_DIA_FIN_ALTA_FIG_ACT", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_endDayFigAnt);

                var po_daysIniFigCur = new OracleParameter("fa_DIF_DIAS_INI_FIG_ACT", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_daysIniFigCur);

                var po_daysEndFigCur = new OracleParameter("fa_DIF_DIAS_FIN_FIG_ACT ", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_daysEndFigCur);

                var po_indDateRange = new OracleParameter("fa_IND_ACUMULA_RANGO_FECHAS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_indDateRange);

                var po_categoryValue = new OracleParameter("fa_VALOR_TMP_PARA_CATEGORIZAR", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_categoryValue);

                var po_fromDate = new OracleParameter("fa_FECHA_DESDE", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_fromDate);

                var po_toDate = new OracleParameter("fa_FECHA_HASTA", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_toDate);

                var po_averagingNro = new OracleParameter("fa_PROMEDIAR_EN_NRO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_averagingNro);

                var po_schemeCode = new OracleParameter("fa_CODIGO_ESQUEMA", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_schemeCode);

                var po_subschemeCode = new OracleParameter("fa_CODIGO_SUBESQUEMA", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_subschemeCode);

                var po_amountLoans = new OracleParameter("fa_MONTO_TOTAL_CREDITOS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_amountLoans);

                var po_averagingPeriod = new OracleParameter("fa_PROMEDIO_PERIODO", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_averagingPeriod);

                var po_iniDateAcum = new OracleParameter("fa_FECHA_INI_ACUM", OracleDbType.Date, ParameterDirection.Output);
                ora.AddParameter(po_iniDateAcum);

                var po_endDateAcum = new OracleParameter("fa_FECHA_FIN_ACUM", OracleDbType.Date, ParameterDirection.Output);
                ora.AddParameter(po_endDateAcum);

                var po_dateCurrent = new OracleParameter("fa_FEC_ALTA_FIG_ACTUAL", OracleDbType.Date, ParameterDirection.Output);
                ora.AddParameter(po_dateCurrent);

                var po_dateprevious = new OracleParameter("fa_FEC_ALTA_FIG_ANTERIOR", OracleDbType.Date, ParameterDirection.Output);
                ora.AddParameter(po_dateprevious);

                var po_dayDif = new OracleParameter("fa_DIFERENCIA_DIAS", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_dayDif); 

                 var po_categoryCode = new OracleParameter("fa_CODIGO_CATEGORIA", OracleDbType.Double, ParameterDirection.Output);
                var po_categoryName = new OracleParameter("fa_NOMBRE_CATEGORIA", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_feeNew = new OracleParameter("fa_TASA_NUEVOS", OracleDbType.Double, ParameterDirection.Output);
                var po_feeRenovated = new OracleParameter("fa_TASA_RENOVADOS", OracleDbType.Double, ParameterDirection.Output);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);

                po_ErrorMessage.Size = 100;
                po_categoryName.Size = 100;
                po_previousFigureName.Size = 100;
                po_currentFigureName.Size = 100;

                ora.AddParameter(po_categoryCode);
                ora.AddParameter(po_categoryName);
                ora.AddParameter(po_feeNew);
                ora.AddParameter(po_feeRenovated);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);

                ora.ExecuteProcedureNonQuery("BBS_LIQCOM2_F_SIMULA_CAT");

                response.categoryCode = double.Parse(ora.GetParameter("fa_CODIGO_CATEGORIA").ToString()); 
                response.categoryName =  ora.GetParameter("fa_NOMBRE_CATEGORIA").ToString();
                response.feeNew =ora.GetParameter("fa_TASA_NUEVOS").ToString(); 
                response.feeRenovated = ora.GetParameter("fa_TASA_RENOVADOS").ToString(); 
                ora.Dispose();

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "SimulatorDAO", "GetCategorySimulation" , ex, "");
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
