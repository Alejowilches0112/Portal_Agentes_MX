using Models;
using System;
using System.Web.Mvc;


namespace BayPortColombia.Controllers
{
    public class ComplianceGoalController : Controller
    {
        // GET: ComplianceGoal
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNextCategory()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var detail = new ManageComplianceGoal().GetNextCategory(executiveID);
            return new JsonResult { Data = detail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetAccumulatedLoan()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var accummulatedL = new ManageComplianceGoal().GetAccumulatedLoan(executiveID);
            return new JsonResult { Data = accummulatedL, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetAccumulatedClarifications()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var accummulatedC = new ManageComplianceGoal().GetAccumulatedClarifications(executiveID);
            return new JsonResult { Data = accummulatedC, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetGoalExecutive()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var goal = new ManageComplianceGoal().GetGoalExecutive(executiveID);
            return new JsonResult { Data = goal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetProductivity()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var goal = new ManageComplianceGoal().GetProductivity(executiveID);
            return new JsonResult { Data = goal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ProgressReport()
        {
            return View();
        }

        public JsonResult GetCategoryRange()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var goal = new ManageComplianceGoal().GetCategoryRange();
            return new JsonResult { Data = goal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetGoalSupervisor()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = usr.userName;

            var goal = new ManageComplianceGoal().GetGoalSupervisor(executiveID);
            return new JsonResult { Data = goal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetProgress(string pStartDate, string pEndDate, string pChild)
        {
            DateTime startDate = new DateTime(), endDate = new DateTime();
            //var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = pChild;

            if (!string.IsNullOrEmpty(pStartDate) && !string.IsNullOrEmpty(pEndDate))
            {
                startDate = Convert.ToDateTime(pStartDate);
                endDate = Convert.ToDateTime(pEndDate);
            }

            var progress = new ManageComplianceGoal().GetProgressExecutive(executiveID, startDate, endDate);
            return new JsonResult { Data = progress, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        

    }
}