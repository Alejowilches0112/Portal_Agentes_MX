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
    public class ManageCommissions
    {
        public OutCommissionsHeader GetCommissionsHeader(string executiveID)
        {
            OutCommissionsHeader data = new OutCommissionsHeader();
            try
            {
                CommissionsDAO dao = new CommissionsDAO();
                data = dao.GetCommissionsHeader(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageCommissions", "GetCommissionsHeader", ex, "");
            }
            return data;
        }
        public OutMovesCommission GetMovesCommissions(string executiveID, double accountNumber, string creditNumber, int reportType, DateTime startDate, 
                DateTime endDate, string[] childList)
        {
            OutMovesCommission data = new OutMovesCommission();
            try
            {
                string childs = string.Empty;
                if (childList != null)
                {
                    for (int i = 0; i < childList.Length; i++)
                    {
                        childs += "'" + childList[i] + "'" + ",";
                    }

                    childs = childs.Substring(0, childs.Length - 1);
                }



                CommissionsDAO dao = new CommissionsDAO();
                data = dao.GetMovesCommissions(executiveID, accountNumber, creditNumber, reportType, startDate, endDate, childs);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageCommissions", "GetMovesCommissions", ex, "");
            }
            return data;
        }
        public OutBalancesCommision GetBalancesCommissions(string executiveID, double accountNumber)
        {
            OutBalancesCommision data = new OutBalancesCommision();
            try
            {
                CommissionsDAO dao = new CommissionsDAO();
                data = dao.GetBalancesCommissions(executiveID, accountNumber);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageCommissions", "GetBalancesCommissions", ex, "");
            }
            return data;
        }
    }
}
