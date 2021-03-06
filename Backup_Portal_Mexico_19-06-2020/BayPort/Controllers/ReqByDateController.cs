﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tags;
using Helper;

namespace BayPortColombia.Controllers
{
    [Autenticado]
    public class ReqByDateController : Controller
    {
        // GET: ReqByDate

        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetRequisitionByDate(string pStartDate, string pEndDate)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            if (pStartDate != null && pEndDate != null)
            {
                startDate = Convert.ToDateTime(pStartDate);
                endDate = Convert.ToDateTime(pEndDate);
            }

            var requisition = new MangerRequisition().GetLoanInformationByLoanOfficer(Double.Parse(usr.userName), startDate, endDate);
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetLoanHeader(string folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            double folderNumber = double.Parse(folder);
            var requisition = new MangerRequisition().GetLoanHeader(folderNumber);
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetLoanDetail(string folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                RedirectToAction("Index", "Home");
                return null;
            }
            double folderNumber = double.Parse(folder);
            var requisition = new MangerRequisition().GetLoanDetailList(folderNumber);
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}