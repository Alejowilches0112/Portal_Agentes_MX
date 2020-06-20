using DAO;
using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MangerRequisition
    {
        public OutLoanInformation GetLoanInformationByLoanOfficer(double officerID, DateTime startDate, DateTime endDate)
        {
            OutLoanInformation loanInformation = new OutLoanInformation();
            try
            {
                RequisitionDAO dao = new RequisitionDAO();
                loanInformation = dao.GetLoanInformationByLoanOfficer(officerID, startDate, endDate);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "MangerRequisition", "GetLoanInformationByLoanOfficer", ex, "");
            }
            return loanInformation;
        }

        public OutLoanInformation GetLoanInformationByCustomer(double customerID)
        {
            OutLoanInformation loanInformation = new OutLoanInformation();
            try
            {
                RequisitionDAO dao = new RequisitionDAO();
                loanInformation = dao.GetLoanInformationByCustomer(customerID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "MangerRequisition", "GetLoanInformationByCustomer", ex, "");
            }
            return loanInformation;

        }

        public OutLoanHeader GetLoanHeader(double folderNumber)
        {
            OutLoanHeader loanHeader = new OutLoanHeader();
            try
            {
                RequisitionDAO dao = new RequisitionDAO();
                loanHeader = dao.GetLoanHeader(folderNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "MangerRequisition", "GetLoanHeader", ex, "");
            }
            return loanHeader;

        }

        public OutLoanDetail GetLoanDetailList(double folderNumber)
        {
            OutLoanDetail loanDetail = new OutLoanDetail();
            try
            {
                RequisitionDAO dao = new RequisitionDAO();
                loanDetail = dao.GetLoanDetailList(folderNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "MangerRequisition", "GetLoanDetailList", ex, "");
            }
            return loanDetail;
        }
    }
}
