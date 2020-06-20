using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tags;
using Entities;
using BayPortColombia.ViewModels;
using Models;
using Helper;

namespace BayPortColombia.Controllers
{
    [Autenticado]
    public class ChangePwdController : Controller
    {
        // GET: ChangePwd
        public ActionResult Index(OutValidateUser usr)
        {
            if (usr.changePassword == 1)
            {
                var ViewModel = new UserPwdChgViewModel
                {
                    name = usr.userName,
                    pwd1 = string.Empty,
                    pwd2 = string.Empty,
                    userID = usr.userID
                };
                return View(ViewModel);
            }
            else {           
                return RedirectToAction("MainMenu","Home");
            }
            
        }

        public ActionResult SavePwd(UserPwdChgViewModel pwdchg)
        {
            var newPswd = new InValidateUser();
            newPswd.userID = pwdchg.userID;
            newPswd.password = pwdchg.pwd1;
            var valid = new ManagerLogin().ChangePassword(newPswd);
            SessionHelper.DestroyUserSession();
            TempData["msg"] = valid.errorMessage;
            return RedirectToAction("Index","Home");
        }
    }
}