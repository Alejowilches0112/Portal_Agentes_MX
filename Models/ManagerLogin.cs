using Entities;
using DAO;
using System;
using Helper;

namespace Models
{
    public class ManagerLogin
    {
        public OutValidateUser ValidateUser(Login usr)
        {
            InValidateUser userData = new InValidateUser();
            OutValidateUser uservalidated = new OutValidateUser();
            try
            {

                AuthenticationDAO dao = new AuthenticationDAO();
                userData.password = usr.password;
                userData.userID = usr.userName;
                userData.sucursal = usr.sucursal;
                uservalidated = dao.ValidateUser(userData);
                if (uservalidated.changePassword == 1)
                {
                    uservalidated.url= "ChangePwd";
                }
                
            }
            catch (Exception ex)
            {
                //escribir en el log
                System.Diagnostics.Debug.WriteLine("Error --> ", ex);
                LogHelper.WriteLog("Models", "ManagerLogin", "ValidateUser", ex, userData.userID);
            }
            return uservalidated;
        }
        public OutValidateUser ValidateUser(string asesor, ref Login usr)
        {
            InValidateUser userData = new InValidateUser();
            OutValidateUser uservalidated = new OutValidateUser();
            try
            {

                AuthenticationDAO dao = new AuthenticationDAO();
                uservalidated = dao.ValidateUser(asesor, ref userData);
                usr.password = uservalidated.password;
                usr.userName = uservalidated.userID;
                if (uservalidated.changePassword == 1)
                {
                    uservalidated.url = "ChangePwd";
                }

            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerLogin", "ValidateUser", ex, userData.userID);
            }
            return uservalidated;
        }
        public Response ChangePassword(InValidateUser input)
        {
            var respChangePswd = new Response();
            try
            {
                AuthenticationDAO dao = new AuthenticationDAO();
                respChangePswd = dao.ChangePassword(input);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerLogin", "ChangePassword", ex, input.userID);
            }
            return respChangePswd;
        }

        public OutMenuType GetMenuType (string  user)
        {
            var respMenuType = new OutMenuType();
            try
            {
                AuthenticationDAO dao = new AuthenticationDAO();
                respMenuType = dao.GetMenuType(user);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerLogin", "GetMenuType", ex, user);
            }
            return respMenuType;
        }
    }
 
}
