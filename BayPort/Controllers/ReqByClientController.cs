using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tags;

namespace BayPortColombia.Controllers
{
    [Autenticado]
    public class ReqByClientController : Controller
    {
        // GET: ReqByClient
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public JsonResult GetRequisitionByClient(string documentID)
        {

            var requisition = new MangerRequisition().GetLoanInformationByCustomer(double.Parse(documentID));
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetLoanHeader(string folder)
        {
            double folderNumber = double.Parse(folder);
            var requisition = new MangerRequisition().GetLoanHeader(folderNumber);
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetLoanDetail(string folder)
        {
            double folderNumber = double.Parse(folder);
            var requisition = new MangerRequisition().GetLoanDetailList(folderNumber);
            return new JsonResult { Data = requisition, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}