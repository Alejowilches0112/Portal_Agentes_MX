using DAO;
using Entities;
using Helper;
using System;

namespace Models
{
    public class ManagerCustomer
    {
        public OutCustomer GetCustomerInformation(string customerID)
        {
            OutCustomer response = new OutCustomer();
            try
            {
                CustomerDAO dao = new CustomerDAO();
                response = dao.GetCustomerInformation(customerID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerCustomer", "GetCustomerInformation", ex, "");
            }
            return response;
        }
        public OutFolder GetFolderInformation(string customerID)
        {
            OutFolder response = new OutFolder();
            try
            {
                CustomerDAO dao = new CustomerDAO();
                response = dao.GetFolderInformation(customerID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerCustomer", "GetFolderInformation", ex, "");
            }
            return response;
        }
    }
}
