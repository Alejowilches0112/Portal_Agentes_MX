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
    public class ManagerFolio
    {
        public OutFolioDetail GetFolioDetail(string executiveID, int type, DateTime startDate, DateTime endDate, string folioNumber, string child)
        {
            OutFolioDetail response = new OutFolioDetail();
            try
            {
                FolioDAO dao = new FolioDAO();
                response = dao.GetFolioDetail(executiveID, type, startDate, endDate, folioNumber, child);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerFolio", "GetFolioDetail", ex, "");
            }
            return response;
        }
    }
}
