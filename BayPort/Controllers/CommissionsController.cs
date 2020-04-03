using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayPortColombia.Controllers
{
    public class CommissionsController : Controller
    {
        // GET: Commissions
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetCommissionsHeader( string childID, int type)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            var commission = new OutCommissionsHeader();
            string executiveID = string.Empty;

            if (type != 4)
                executiveID = usr.userName;

            if (childID != null )
                executiveID = childID;


            commission = new ManageCommissions().GetCommissionsHeader(executiveID);
            return new JsonResult { Data = commission, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetMovesCommissions(double accountNumber, string creditNumber, int type, string pStartDate, string pEndDate, string[] childs)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;
            if (!string.IsNullOrEmpty(pStartDate)  && !string.IsNullOrEmpty(pEndDate))
            {
                startDate = Convert.ToDateTime(pStartDate);
                endDate = Convert.ToDateTime(pEndDate);
            }


            var movement = new ManageCommissions().GetMovesCommissions(executiveID, accountNumber,  creditNumber, type,  startDate, endDate, childs);
            return new JsonResult { Data = movement, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            
        }
        public JsonResult GetBalancesCommissions(double accountNumber, string child,  int type)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            if (type != 4)
                executiveID = usr.userName;

            if (child != null)
                executiveID = child;

            var balance = new ManageCommissions().GetBalancesCommissions(executiveID, accountNumber);
            return new JsonResult { Data = balance, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetUploadDocuments()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;
            var documents = new ManageDocuments().GetUploadDocuments(executiveID);
            return new JsonResult { Data = documents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}