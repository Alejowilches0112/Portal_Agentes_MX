using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Helper;

namespace Models
{
    public class ManagerPQR
    {
        public OutHeaderPQR GetHeaderPQR(string executiveID, int type, DateTime startDate, DateTime endDate, string loanNumber, string PQRnumber, 
                int flowType, string status, string[] childList)
        {           
            OutHeaderPQR response = new OutHeaderPQR();
            try
            {
                string childs = string.Empty;

                for (int i = 0; i < childList.Length; i++)
                {
                    childs += "'"+childList[i]+"'"+ ",";
                }

                childs = childs.Substring(0, childs.Length - 1);
                

                PqrDAO dao = new PqrDAO();
                response = dao.GetHeaderPQR(executiveID, type, startDate, endDate, loanNumber, PQRnumber, flowType, status, childs);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetHeaderPQR", ex, "");
            }
            return response;
        }

        public OutLogPQR GetLogPQR(int processNumber)
        {
            OutLogPQR response = new OutLogPQR();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetLogPQR(processNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetLogPQR", ex, "");
            }
            return response;
        }
        public OutNoveltyPQR GetNoveltyPQR(int processNumber)
        {
            OutNoveltyPQR response = new OutNoveltyPQR();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetNoveltyPQR(processNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetNoveltyPQR", ex, "");
            }
            return response;
        }

        public Response CreatePQR (InCreatePQR input)
        {
            Response response = new Response();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.CreatePQR( input ) ;
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "CreatePQR", ex, "");
            }
            return response;
        }

        public OutFlowType GetFlow ()
        {
            var response = new OutFlowType();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetFlows();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetFlow", ex, "");
            }
            return response;
        }

        public OutJustification GetJustification (int flowType)
        {
            var response = new OutJustification();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetJustification(flowType);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetJustification", ex, "");
            }
            return response;
        }

        public OutLoanResume GetLoanResume(double loanNumber)
        {
            var response = new OutLoanResume();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetLoanResume(loanNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetLoanResume", ex, "");
            }
            return response;
        }

        public OutStates GetStates()
        {
            var response = new OutStates();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetStates();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetStates", ex, "");
            }
            return response;
        }

        public OutSummaryPQR GetSummaryPQR(string executiveID)
        {
            var response = new OutSummaryPQR();
            try
            {
                PqrDAO dao = new PqrDAO();
                response = dao.GetSummaryPQR(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerPQR", "GetSummaryPQR", ex, "");
            }
            return response;
        }

    }
}
