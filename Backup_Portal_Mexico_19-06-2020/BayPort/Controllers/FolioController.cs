using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayPortColombia.Controllers
{
    public class FolioController : Controller
    {
        // GET: Folio
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetFolioDetail(int type, string pStartDate, string pEndDate, string folioNumber, string child)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            string executiveID = string.Empty;

            if (!string.IsNullOrEmpty(pStartDate) && !string.IsNullOrEmpty(pEndDate))
            {
                startDate = Convert.ToDateTime(pStartDate);
                endDate = Convert.ToDateTime(pEndDate);
            }

            if (type != 5)
                executiveID = usr.userName;

            if (child != null)
                executiveID = child;

            var detail = new ManagerFolio().GetFolioDetail(executiveID, type, startDate, endDate, folioNumber, child);
            return new JsonResult { Data = detail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}