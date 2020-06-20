using Entities;
using Helper;
using System;
using System.Collections.Generic;

namespace DAO
{
    public class RequisitionDAO
    {
        public OutLoanDetail GetLoanDetailList(double folderNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLoanDetail response = new OutLoanDetail();
            var ora = new OracleServer(connectionString);

            LoanDetail detail;
            List<LoanDetail> list = new List<LoanDetail>();
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_CARPETA, TIPO_TRAMITE, DESC_TIPO_TRAMITE, FECHA_TRAMITE, USUARIO_TRAMITE, CODIGO_ESTADO_ANTERIOR, ";
                command = command + "NOMBRE_ESTADO_ANT, CODIGO_ESTADO_NUEVO,NOMBRE_ESTADO_NVO, OBSERVACIONES,TIPO_LOG  ";
                command = command + string.Format("FROM BBS_LIQCOM_V_DET_CARPETAS WHERE NUMERO_CARPETA = {0} ORDER BY FECHA_TRAMITE", folderNumber);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    detail = new LoanDetail();
                    detail.folderNumber = DBNull.Value.Equals(rdr["NUMERO_CARPETA"]) ? 0 : double.Parse(rdr["NUMERO_CARPETA"].ToString());
                    detail.processType = DBNull.Value.Equals(rdr["TIPO_TRAMITE"]) ? 0 : double.Parse(rdr["TIPO_TRAMITE"].ToString());
                    detail.processTypeDescription = DBNull.Value.Equals(rdr["DESC_TIPO_TRAMITE"]) ? string.Empty : rdr["DESC_TIPO_TRAMITE"].ToString();
                    detail.processDate = DBNull.Value.Equals(rdr["FECHA_TRAMITE"]) ? DateTime.Today.ToString() : rdr["FECHA_TRAMITE"].ToString();
                    detail.processUser = DBNull.Value.Equals(rdr["USUARIO_TRAMITE"]) ? string.Empty : rdr["USUARIO_TRAMITE"].ToString();
                    detail.previousStateCode = DBNull.Value.Equals(rdr["CODIGO_ESTADO_ANTERIOR"]) ? 0 : double.Parse(rdr["CODIGO_ESTADO_ANTERIOR"].ToString());
                    detail.previousStateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_ANT"]) ? string.Empty : rdr["NOMBRE_ESTADO_ANT"].ToString();
                    detail.NewStateCode = DBNull.Value.Equals(rdr["CODIGO_ESTADO_NUEVO"]) ? 0 : double.Parse(rdr["CODIGO_ESTADO_NUEVO"].ToString());
                    detail.NewStateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_NVO"]) ? string.Empty : rdr["NOMBRE_ESTADO_NVO"].ToString();
                    detail.observations = DBNull.Value.Equals(rdr["OBSERVACIONES"]) ? string.Empty : rdr["OBSERVACIONES"].ToString();
                    detail.LogType = DBNull.Value.Equals(rdr["TIPO_LOG"]) ? 0 : double.Parse(rdr["TIPO_LOG"].ToString());
                    list.Add(detail);
                }
                rdr.Close();
                response.loanDetailList = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("RequisitionDAO.GetLoanDetailList", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutLoanHeader GetLoanHeader(double folderNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLoanHeader response = new OutLoanHeader();
            var ora = new OracleServer(connectionString);

            //LoanDetail detail;            
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_CARPETA, NUMERO_IDENTIFICACION, NOMBRE_PERSONA, FECHA_CREACION, CODIGO_ESTADO_CARPETA, ";
                command = command + "NOMBRE_ESTADO_CARPETA, MONTO_SOLICITADO, PLAZO_SOLICITADO, TASA_SOLICITADO, MONTO_APROBADO, ";
                command = command + "PLAZO_APROBADO, TASA_APROBADA, VALOR_CUOTA_APROBADA, CODIGO_CONVENIO_LIBRANZA, NOMBRE_CONVENIO, ";
                command = command + "CODIGO_PAGADURIA_LIBRANZA, NOMBRE_PAGADURIA, CODIGO_SUCURSAL, SUCURSAL_HOMOLOGA ";
                command = command + string.Format("FROM BBS_LIQCOM_V_CAB_CARPETAS WHERE NUMERO_CARPETA =  {0}", folderNumber);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    response.folderNumber = DBNull.Value.Equals(rdr["NUMERO_CARPETA"]) ? 0 : double.Parse(rdr["NUMERO_CARPETA"].ToString());
                    response.numberID = DBNull.Value.Equals(rdr["NUMERO_IDENTIFICACION"]) ? string.Empty : rdr["NUMERO_IDENTIFICACION"].ToString();
                    response.personName = DBNull.Value.Equals(rdr["NOMBRE_PERSONA"]) ? string.Empty : rdr["NOMBRE_PERSONA"].ToString();
                    response.creationDate = DBNull.Value.Equals(rdr["FECHA_CREACION"]) ? DateTime.Today : DateTime.Parse(rdr["FECHA_CREACION"].ToString());
                    response.folderStateCode = DBNull.Value.Equals(rdr["CODIGO_ESTADO_CARPETA"]) ? 0 : double.Parse(rdr["CODIGO_ESTADO_CARPETA"].ToString());
                    response.folserStateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_CARPETA"]) ? string.Empty : rdr["NOMBRE_ESTADO_CARPETA"].ToString();
                    response.amountRequested = DBNull.Value.Equals(rdr["MONTO_SOLICITADO"]) ? 0 : decimal.Parse(rdr["MONTO_SOLICITADO"].ToString());
                    response.termRequested = DBNull.Value.Equals(rdr["PLAZO_SOLICITADO"]) ? 0 : decimal.Parse(rdr["PLAZO_SOLICITADO"].ToString());
                    response.rateRequested = DBNull.Value.Equals(rdr["TASA_SOLICITADO"]) ? 0 : decimal.Parse(rdr["TASA_SOLICITADO"].ToString());
                    response.amountApproved = DBNull.Value.Equals(rdr["MONTO_APROBADO"]) ? 0 : decimal.Parse(rdr["MONTO_APROBADO"].ToString());
                    response.termApproved = DBNull.Value.Equals(rdr["PLAZO_APROBADO"]) ? 0 : double.Parse(rdr["PLAZO_APROBADO"].ToString());
                    response.rateApproved = DBNull.Value.Equals(rdr["TASA_APROBADA"]) ? 0 : decimal.Parse(rdr["TASA_APROBADA"].ToString());
                    response.monthlyPaymentApproved = DBNull.Value.Equals(rdr["VALOR_CUOTA_APROBADA"]) ? 0 : decimal.Parse(rdr["VALOR_CUOTA_APROBADA"].ToString());
                    response.agreementCode = DBNull.Value.Equals(rdr["CODIGO_CONVENIO_LIBRANZA"]) ? 0 : double.Parse(rdr["CODIGO_CONVENIO_LIBRANZA"].ToString());
                    response.agreementName = DBNull.Value.Equals(rdr["NOMBRE_CONVENIO"]) ? string.Empty : rdr["NOMBRE_CONVENIO"].ToString();
                    response.payableCode = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA_LIBRANZA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA_LIBRANZA"].ToString());
                    response.payableName = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? string.Empty : rdr["NOMBRE_PAGADURIA"].ToString();
                    response.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    response.branchHomologous = DBNull.Value.Equals(rdr["SUCURSAL_HOMOLOGA"]) ? 0 : double.Parse(rdr["SUCURSAL_HOMOLOGA"].ToString());
                }
                rdr.Close();

                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("RequisitionDAO.GetLoanDetailList", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutLoanInformation GetLoanInformationByLoanOfficer(double officerID, DateTime startDate, DateTime endDate)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLoanInformation response = new OutLoanInformation();
            LoanInformation loan;
            List<LoanInformation> lst = new List<LoanInformation>();
            var ora = new OracleServer(connectionString);

            //LoanDetail detail;            
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_CARPETA, NUMERO_IDENTIFICACION, NOMBRE_PERSONA, CODIGO_ESTADO_CARPETA, NOMBRE_ESTADO_CARPETA, ";
                command = command + "LINEA_CREDITO, MONTO_SOLICITUD, MONTO_APROBADO, PLAZO_APROBADO, TASA_APROBADA, CODIGO_ASESOR ,";
                command = command + "FECHA_CREACION, CODIGO_ZONA, CODIGO_SUCURSAL, NOMBRE_SUCURSAL, CODIGO_CONVENIO, NOMBRE_CONVENIO ,";
                command = command + "CODIGO_PAGADURIA, NOMRE_CONVENIO, ANALISTA_APROBACION, CODIGO_NOVEDAD, NOMBRE_NOVEDAD, CODIGO_JEFE_CCIAL, ";
                command = command + "NOMBRE_JEFE_CCIAL, CEDULA_EJECUTIVO, NOMBRE_EJECUTIVO ";
                command = command + string.Format("FROM BBS_LIQCOM_V_CARPETAS WHERE CEDULA_EJECUTIVO = '{0}'  ", officerID);
                command = command + string.Format(" AND fecha_creacion BETWEEN to_date('{0}', 'dd-mm-yyyy')  AND to_date('{1}', 'dd-mm-yyyy')", startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    loan = new LoanInformation();
                    loan.folderNumber = DBNull.Value.Equals(rdr["NUMERO_CARPETA"]) ? 0 : double.Parse(rdr["NUMERO_CARPETA"].ToString());
                    loan.numberID = DBNull.Value.Equals(rdr["NUMERO_IDENTIFICACION"]) ? string.Empty : rdr["NUMERO_IDENTIFICACION"].ToString();
                    loan.personName = DBNull.Value.Equals(rdr["NOMBRE_PERSONA"]) ? string.Empty : rdr["NOMBRE_PERSONA"].ToString();
                    loan.folderStateCode = DBNull.Value.Equals(rdr["CODIGO_ESTADO_CARPETA"]) ? 0 : double.Parse(rdr["CODIGO_ESTADO_CARPETA"].ToString());
                    loan.folderStateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_CARPETA"]) ? string.Empty : rdr["NOMBRE_ESTADO_CARPETA"].ToString();
                    loan.loanLine = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : double.Parse(rdr["LINEA_CREDITO"].ToString());
                    loan.requisitionAmount = DBNull.Value.Equals(rdr["MONTO_SOLICITUD"]) ? 0 : double.Parse(rdr["MONTO_SOLICITUD"].ToString());
                    loan.amountApproved = DBNull.Value.Equals(rdr["MONTO_APROBADO"]) ? 0 : double.Parse(rdr["MONTO_APROBADO"].ToString());
                    loan.termApproved = DBNull.Value.Equals(rdr["PLAZO_APROBADO"]) ? 0 : int.Parse(rdr["PLAZO_APROBADO"].ToString());
                    loan.rateApproved = DBNull.Value.Equals(rdr["TASA_APROBADA"]) ? 0 : decimal.Parse(rdr["TASA_APROBADA"].ToString());
                    loan.loanOfficerCode = DBNull.Value.Equals(rdr["CODIGO_ASESOR"]) ? 0 : double.Parse(rdr["CODIGO_ASESOR"].ToString());
                    loan.creationDate = DBNull.Value.Equals(rdr["FECHA_CREACION"]) ? DateTime.Today.ToString() : rdr["FECHA_CREACION"].ToString();
                    loan.zoneCode = DBNull.Value.Equals(rdr["CODIGO_ZONA"]) ? 0 : double.Parse(rdr["CODIGO_ZONA"].ToString());
                    loan.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    loan.branchName = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? string.Empty : rdr["NOMBRE_SUCURSAL"].ToString();
                    loan.agreementCode = DBNull.Value.Equals(rdr["CODIGO_CONVENIO"]) ? 0 : double.Parse(rdr["CODIGO_CONVENIO"].ToString());
                    loan.agreementName = DBNull.Value.Equals(rdr["NOMBRE_CONVENIO"]) ? string.Empty : rdr["NOMBRE_CONVENIO"].ToString();
                    loan.payamentCode = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA"].ToString());
                    loan.payamentName = DBNull.Value.Equals(rdr["NOMRE_CONVENIO"]) ? string.Empty : rdr["NOMRE_CONVENIO"].ToString();
                    loan.analystsApproved = DBNull.Value.Equals(rdr["ANALISTA_APROBACION"]) ? string.Empty : rdr["ANALISTA_APROBACION"].ToString();
                    loan.noveltyCode = DBNull.Value.Equals(rdr["CODIGO_NOVEDAD"]) ? 0 : double.Parse(rdr["CODIGO_NOVEDAD"].ToString());
                    loan.noveltyName = DBNull.Value.Equals(rdr["NOMBRE_NOVEDAD"]) ? string.Empty : rdr["NOMBRE_NOVEDAD"].ToString();
                    loan.commercialBossCode = DBNull.Value.Equals(rdr["CODIGO_JEFE_CCIAL"]) ? 0 : double.Parse(rdr["CODIGO_JEFE_CCIAL"].ToString());
                    loan.commercialBossName = DBNull.Value.Equals(rdr["NOMBRE_JEFE_CCIAL"]) ? string.Empty : rdr["NOMBRE_JEFE_CCIAL"].ToString();
                    loan.executiveID = DBNull.Value.Equals(rdr["CEDULA_EJECUTIVO"]) ? string.Empty : rdr["CEDULA_EJECUTIVO"].ToString();
                    loan.executiveName = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();
                    lst.Add(loan);
                }
                rdr.Close();
                response.loanInformationList = lst;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("RequisitionDAO.GetLoanInformationByLoanOfficer", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutLoanInformation GetLoanInformationByCustomer(double customerID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutLoanInformation response = new OutLoanInformation();
            LoanInformation loan;
            List<LoanInformation> lst = new List<LoanInformation>();
            var ora = new OracleServer(connectionString);

            //LoanDetail detail;            
            string command = string.Empty;

            try
            {
                command = "SELECT NUMERO_CARPETA, NUMERO_IDENTIFICACION, NOMBRE_PERSONA, CODIGO_ESTADO_CARPETA, NOMBRE_ESTADO_CARPETA, ";
                command = command + "LINEA_CREDITO, MONTO_SOLICITUD, MONTO_APROBADO, PLAZO_APROBADO, TASA_APROBADA, CODIGO_ASESOR, ";
                command = command + "FECHA_CREACION, CODIGO_ZONA, CODIGO_SUCURSAL, NOMBRE_SUCURSAL, CODIGO_CONVENIO, NOMBRE_CONVENIO, ";
                command = command + "CODIGO_PAGADURIA, NOMRE_CONVENIO, ANALISTA_APROBACION, CODIGO_NOVEDAD, NOMBRE_NOVEDAD, CODIGO_JEFE_CCIAL, ";
                command = command + "NOMBRE_JEFE_CCIAL, CEDULA_EJECUTIVO, NOMBRE_EJECUTIVO ";
                command = command + string.Format("FROM BBS_LIQCOM_V_CARPETAS WHERE NUMERO_IDENTIFICACION = '{0}'", customerID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    loan = new LoanInformation();
                    loan.folderNumber = DBNull.Value.Equals(rdr["NUMERO_CARPETA"]) ? 0 : double.Parse(rdr["NUMERO_CARPETA"].ToString());
                    loan.numberID = DBNull.Value.Equals(rdr["NUMERO_IDENTIFICACION"]) ? string.Empty : rdr["NUMERO_IDENTIFICACION"].ToString();
                    loan.personName = DBNull.Value.Equals(rdr["NOMBRE_PERSONA"]) ? string.Empty : rdr["NOMBRE_PERSONA"].ToString();
                    loan.folderStateCode = DBNull.Value.Equals(rdr["CODIGO_ESTADO_CARPETA"]) ? 0 : double.Parse(rdr["CODIGO_ESTADO_CARPETA"].ToString());
                    loan.folderStateName = DBNull.Value.Equals(rdr["NOMBRE_ESTADO_CARPETA"]) ? string.Empty : rdr["NOMBRE_ESTADO_CARPETA"].ToString();
                    loan.loanLine = DBNull.Value.Equals(rdr["LINEA_CREDITO"]) ? 0 : double.Parse(rdr["LINEA_CREDITO"].ToString());
                    loan.requisitionAmount = DBNull.Value.Equals(rdr["MONTO_SOLICITUD"]) ? 0 : double.Parse(rdr["MONTO_SOLICITUD"].ToString());
                    loan.amountApproved = DBNull.Value.Equals(rdr["MONTO_APROBADO"]) ? 0 : double.Parse(rdr["MONTO_APROBADO"].ToString());
                    loan.termApproved = DBNull.Value.Equals(rdr["PLAZO_APROBADO"]) ? 0 : int.Parse(rdr["PLAZO_APROBADO"].ToString());
                    loan.rateApproved = DBNull.Value.Equals(rdr["TASA_APROBADA"]) ? 0 : decimal.Parse(rdr["TASA_APROBADA"].ToString());
                    loan.loanOfficerCode = DBNull.Value.Equals(rdr["CODIGO_ASESOR"]) ? 0 : double.Parse(rdr["CODIGO_ASESOR"].ToString());
                    loan.creationDate = DBNull.Value.Equals(rdr["FECHA_CREACION"]) ? DateTime.Today.ToString() : rdr["FECHA_CREACION"].ToString();
                    loan.zoneCode = DBNull.Value.Equals(rdr["CODIGO_ZONA"]) ? 0 : double.Parse(rdr["CODIGO_ZONA"].ToString());
                    loan.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    loan.branchName = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? string.Empty : rdr["NOMBRE_SUCURSAL"].ToString();
                    loan.agreementCode = DBNull.Value.Equals(rdr["CODIGO_CONVENIO"]) ? 0 : double.Parse(rdr["CODIGO_CONVENIO"].ToString());
                    loan.agreementName = DBNull.Value.Equals(rdr["NOMBRE_CONVENIO"]) ? string.Empty : rdr["NOMBRE_CONVENIO"].ToString();
                    loan.payamentCode = DBNull.Value.Equals(rdr["CODIGO_PAGADURIA"]) ? 0 : double.Parse(rdr["CODIGO_PAGADURIA"].ToString());
                    loan.payamentName = DBNull.Value.Equals(rdr["NOMRE_CONVENIO"]) ? string.Empty : rdr["NOMRE_CONVENIO"].ToString();
                    loan.analystsApproved = DBNull.Value.Equals(rdr["ANALISTA_APROBACION"]) ? "-" : rdr["ANALISTA_APROBACION"].ToString();
                    loan.noveltyCode = DBNull.Value.Equals(rdr["CODIGO_NOVEDAD"]) ? 0 : double.Parse(rdr["CODIGO_NOVEDAD"].ToString());
                    loan.noveltyName = DBNull.Value.Equals(rdr["NOMBRE_NOVEDAD"]) ? string.Empty : rdr["NOMBRE_NOVEDAD"].ToString();
                    loan.commercialBossCode = DBNull.Value.Equals(rdr["CODIGO_JEFE_CCIAL"]) ? 0 : double.Parse(rdr["CODIGO_JEFE_CCIAL"].ToString());
                    loan.commercialBossName = DBNull.Value.Equals(rdr["NOMBRE_JEFE_CCIAL"]) ? string.Empty : rdr["NOMBRE_JEFE_CCIAL"].ToString();
                    loan.executiveID = DBNull.Value.Equals(rdr["CEDULA_EJECUTIVO"]) ? string.Empty : rdr["CEDULA_EJECUTIVO"].ToString();
                    loan.executiveName = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();
                    lst.Add(loan);
                }
                rdr.Close();
                response.loanInformationList = lst;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("RequisitionDAO.GetLoanInformationByCustomer", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
    }
}
