using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Models;
using Entities;
using Helper;
using Tags;
using GenCorreos;
using System.Text.RegularExpressions;

namespace AdministradorColas.Controllers
{
    public class HomeController : Controller
    {

        public string modulo;
        [NoLogin]// GET: Home
        public ActionResult Index()
        {
            var s = RouteData.Values;
            if (s.Values.Count == 3)
            {
                var val = s["id"].ToString();
                if (val.Length > 5)
                {
                    var guion = val.IndexOf("-");
                    modulo = val.Substring(guion + 1, 1);
                    val = Regex.Replace(val, @"[^\d]", "");
                    TempData["modulo"] = modulo;
                    return Authenticate_asesor(val.ToString());
                }
            }
            if (TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"];
            }
            else
            {
                ViewBag.msg = string.Empty;
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(Login user)
        {

            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            //Autenticar a Base de datos
            ManagerLogin validate = new ManagerLogin();
            var userinfo = validate.ValidateUser(user);
            userinfo.userID = user.userName;
            user.asesor = userinfo.asesor;
            user.sucursal = userinfo.sucursal;
            TempData["user"] = userinfo;
            TempData["asesor"] = userinfo.asesor;
            //Generar la cookie
            if (userinfo.msg.errorCode.Equals("0"))
            {
                SessionHelper.AddUserToSession(userinfo.userName);

               
                if (userinfo.url != string.Empty)
                {
                    return RedirectToAction("Index", userinfo.url, userinfo);
                }

            }
            else
            {
                TempData["msg"] = userinfo.msg.errorMessage;
                return RedirectToAction("Index");

            }
            System.Web.HttpContext.Current.Session["usr"] = user;
            System.Web.HttpContext.Current.Session["menuType"] = validate.GetMenuType(user.userName);   
            return RedirectToAction("MainMenu");

        }
        /*[Autenticado]
        public ActionResult MainMenu()
        {

            ViewBag.correo = (string)TempData["msgCambio"] == null ? "" : (string)TempData["msgCambio"];

            return View();
        }
        */
        public ActionResult Authenticate_asesor(string asesor)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            //Autenticar a Base de datos
            ManagerLogin validate = new ManagerLogin();
            Login user = new Login();
            var userinfo = validate.ValidateUser(asesor, ref user);
            userinfo.asesor = int.Parse(asesor);
            user.asesor = int.Parse(asesor);
            //userinfo.userID = user.userName;
            TempData["user"] = userinfo;
            if (userinfo.changePassword == 1)
            {
                userinfo.msg.errorCode = "0";
                userinfo.url = string.Empty;
            }
            //Generar la cookie
            if (userinfo.msg.errorCode.Equals("0"))
            {
                SessionHelper.AddUserToSession(userinfo.userName);
                if (userinfo.url != string.Empty)
                {
                    return RedirectToAction("Index", userinfo.url, userinfo);
                }
            }
            else
            {
                //Adicion para redirigir a la ventana de Cambio de clave, Danny Romero Lozano, 19/07/2018
                TempData["msg"] = userinfo.msg.errorMessage;
                return RedirectToAction("Index");
                //Fin Adicion
            }
            System.Web.HttpContext.Current.Session["usr"] = user;
            return RedirectToAction("MainMenu");
        }
        [Autenticado]
        public ActionResult MainMenu()
        {
            var s = RouteData.Values;
            if (TempData["modulo"] != null)
            {
                modulo = TempData["modulo"].ToString();
            }
            else
            {
                modulo = "666";
            }
            TempData["modulo"] = string.Empty;
            ViewBag.correo = (string)TempData["msgCambio"] == null ? "" : (string)TempData["msgCambio"];
            switch (modulo)
            {
                case "A":
                    View();
                    return RedirectToAction("Index", "Profile", null);
                case "B":
                    View();
                    return RedirectToAction("Index", "ReqByClient", null);
                case "C":
                    View();
                    return RedirectToAction("Index", "ReqByDate", null);
                case "D":
                    View();
                    return RedirectToAction("Index", "Commissions", null);
                default:
                    return View();
            };
        }
        public ActionResult MailPwd()
        {
            var msg = "";
            if (TempData["user"] != null)
            {
                var usr = (OutValidateUser)TempData["user"];
                int contador = 0;
                Mail send = new Mail();
                OutMailInformation info = Utilities.GetMailInformation("~/Templates/TemplateChangePasswordMail.xml");
                info.message = info.message.Replace(":nombreAgente", usr.userName).Replace(":documentoAgente", usr.userID);
                send.EnviarCorreo(info.to, "", "", info.subject, info.message, ref contador);
                msg = "Se ha enviado un correo con su solicitud de Cambio de Clave.";
            }
            TempData["msgCambio"] = msg;
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return RedirectToAction("/MainMenu");
        }

    }
}