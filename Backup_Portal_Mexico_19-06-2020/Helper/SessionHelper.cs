using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using Entities;


namespace Helper
{
    public class SessionHelper
    {
        public static bool ExistUserInSession()
        {
            try { 
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return false;
            }
            }catch(Exception e)
            {
                //LogHelper.WriteLog("Models", "ManageProfile", "ExistUserInSession", e, "");
            }
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }
        public static string GetUser()
        {
            string user_id = String.Empty;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = ticket.UserData;
                }
            }
            return user_id;
        }
        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool GetMenuType()
        {
            bool menu =  false;
            
            try
            {
                if (System.Web.HttpContext.Current.Session["menuType"] != null)
                {
                    if (((Entities.OutMenuType)System.Web.HttpContext.Current.Session["menuType"]).menuType == 1)
                        menu = true;

                }
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerLogin", "GetMenuType", ex, "");
            }


            return menu;
        }
    }
}