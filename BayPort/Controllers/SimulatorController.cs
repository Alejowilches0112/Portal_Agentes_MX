using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayPortColombia.Controllers
{
    public class SimulatorController : Controller
    {
        // GET: Simulator
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetCategorySimulation(double amount)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            string executiveID = usr.userName;
            var input = new InCategorySimulation()
            {
                executiveCode = executiveID,
                amount = amount
            };

            var header = new ManagerSimulator().GetCategorySimulation(input);
            return new JsonResult { Data = header, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}