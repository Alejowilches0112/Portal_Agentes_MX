using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BayPortColombia.Controllers
{
    public class PQRController : Controller
    {
        // GET: PQR
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Create()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            ViewBag.documentoAsesor = usr.userName;
            return View();
        }


        public JsonResult GetHeaderPQR(int type, string pStartDate, string pEndDate, string loanNumber, string PQRnumber, 
                string flowType, string status, string[] childs)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            string executiveID = usr.userName;

            if (!string.IsNullOrEmpty(pStartDate) && !string.IsNullOrEmpty(pEndDate))
            {
                startDate = Convert.ToDateTime(pStartDate);
                endDate = Convert.ToDateTime(pEndDate);
            }
            
         

            var header = new ManagerPQR().GetHeaderPQR(executiveID, type, startDate, endDate, loanNumber, PQRnumber, int.Parse(flowType == null ? "0" : flowType), status, childs);
            return new JsonResult { Data = header, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetLogPQR(int processNumber)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var log = new ManagerPQR().GetLogPQR(processNumber);
            return new JsonResult { Data = log, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetNoveltyPQR(int processNumber)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var novelty = new ManagerPQR().GetNoveltyPQR(processNumber);
            return new JsonResult { Data = novelty, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult CreatePQR(FormCreatePQR input)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            InCreatePQR pqr = new InCreatePQR()
            {
                company = 1,
                customerID = usr.userName,
                description = input.Observaciones,
                executiveID = usr.userName,
                flowType = decimal.Parse(input.TipoFlujo),
                reason = decimal.Parse(input.TipoJustificacion),
                loanNumber = string.IsNullOrEmpty(input.NumeroCredito) ? 0 : decimal.Parse(input.NumeroCredito)
            };

            var create = new ManagerPQR().CreatePQR(pqr);
            return new JsonResult
            {
                Data = create,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetFlow()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var flow = new ManagerPQR().GetFlow();
            return new JsonResult { Data = flow, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetJustification(int flowType)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var justify = new ManagerPQR().GetJustification(flowType);
            return new JsonResult { Data = justify, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetLoanResume(string loanNumber)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var justify = new ManagerPQR().GetLoanResume(double.Parse(loanNumber));
            return new JsonResult { Data = justify, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetStates()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var flow = new ManagerPQR().GetStates();
            return new JsonResult { Data = flow, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSummaryPQR( string child, int type )
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var userName = string.Empty;

            if (type != 7)
                userName = usr.userName;

            if (child != null)
                userName = child;

            var summary = new ManagerPQR().GetSummaryPQR(userName);
            return new JsonResult { Data = summary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetExecutiveLevel()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var levels = new ManagerParameters().GetExecutiveLevel();
            return new JsonResult { Data = levels, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetExecutiveChilds(int level)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            var childs = new ManageExecutive().GetExecutiveChilds(usr.userName, level);
            return new JsonResult { Data = childs, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}