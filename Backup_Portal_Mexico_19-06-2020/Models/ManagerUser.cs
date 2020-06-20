using DAO;
using Entities;
using Helper;
using System;

namespace Models
{
    public class ManagerUser
    {
        public OutUserOptions GetUserOptions(string executiveID, string ind_menu, string svrpath)
        {
            OutUserOptions userOptions = new OutUserOptions();
            try
            {
                UserDAO dao = new UserDAO();
                userOptions = dao.GetUserOptions(executiveID, ind_menu, svrpath);
                dao.ChangeEstadoSoli(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerUser", "GetUserOptions", ex, "");
            }
            return userOptions;
        }
    }
}
