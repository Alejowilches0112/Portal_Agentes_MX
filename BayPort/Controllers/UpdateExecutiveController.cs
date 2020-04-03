using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Entities;
using Helper;
using System.IO;

namespace BayPortColombia.Controllers
{
    public class UpdateExecutiveController : Controller
    {
        // GET: UpdateExceutive
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public JsonResult GetCivilStatus()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return null;
            }
            var civilStatus = new ManagerParameters().GetCivilStatus();
            return new JsonResult { Data = civilStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetDepartments()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return null;
            }
            var departments = new ManagerParameters().GetDepartments();
            return new JsonResult { Data = departments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetHousingType()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return null;
            }
            var housing = new ManagerParameters().GetHousingType();
            return new JsonResult { Data = housing, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetAppliedStudies()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return null;
            }
            var studies = new ManagerParameters().GetAppliedStudies();
            return new JsonResult { Data = studies, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetAFP()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return null;
            }
            var afp = new ManagerParameters().GetAFP();
            return new JsonResult { Data = afp, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetARP()
        {
            var arp = new ManagerParameters().GetARP();
            return new JsonResult { Data = arp, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetEPS()
        {
            var eps = new ManagerParameters().GetEPS();
            return new JsonResult { Data = eps, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetBanks()
        {
            var banks = new ManagerParameters().GetBanks();
            return new JsonResult { Data = banks, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult GetBornCity()
        {
            var bornCity = new ManagerParameters().GetBornCity();
            return new JsonResult { Data = bornCity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetCancellationCausal()
        {
            var cancellationCausal = new ManagerParameters().GetCancellationCausal();
            return new JsonResult { Data = cancellationCausal, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetCategory()
        {
            var category = new ManagerParameters().GetCategory();
            return new JsonResult { Data = category, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetBranches()
        {
            var branches = new ManagerParameters().GetBranches();
            return new JsonResult { Data = branches, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetRegionals()
        {
            var regionals = new ManagerParameters().GetRegionals();
            return new JsonResult { Data = regionals, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetCoordinators()
        {
            var coordinators = new ManagerParameters().GetCoordinators();
            return new JsonResult { Data = coordinators, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetExecutiveType()
        {
            var executiveType = new ManagerParameters().GetExecutiveType();
            return new JsonResult { Data = executiveType, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetChannelType()
        {
            var channelType = new ManagerParameters().GetChannelType();
            return new JsonResult { Data = channelType, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetSalesChannel()
        {
            var salesChannel = new ManagerParameters().GetSalesChannel();
            return new JsonResult { Data = salesChannel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetCities(string departmentID)
        {
            int value = int.Parse(departmentID);
            var cities = new ManagerParameters().GetCities(value);
            return new JsonResult { Data = cities, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetNeighborhood(string municipalityID)
        {
            int value = int.Parse(municipalityID);
            var neighborhood = new ManagerParameters().GetNeighborhood(value);
            return new JsonResult { Data = neighborhood, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetExecutiveInformation()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = String.Empty;

            if (usr != null)
                executiveID = usr.userName;

            var dataExecutive = new ManageExecutive().GetExecutiveInformation(executiveID, usr.asesor);
            return new JsonResult { Data = dataExecutive, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetLisDocuments()
        {
            var documents = new ManagerParameters().GetLisDocuments("ASESOR");
            return new JsonResult { Data = documents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateExcecutive(InUpdateExecutiveService formulario, List<HttpPostedFile> files)
        {
            InUpdateExecutive infoExecutive = new InUpdateExecutive();
            var response = new Response();

            var valid = new ManageExecutive().MapEntity(formulario, infoExecutive, ref response);
            if (valid)
            {
                response = new ManageExecutive().UpdateExecutive(infoExecutive);
            }

            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public void PostFilesExcecutive()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            string executiveID = String.Empty;
            string path = string.Empty;
            string fileName = string.Empty;       

            if (usr != null)
                executiveID = usr.userName;
            var documents = new ManagerParameters().GetLisDocuments("ASESOR");

            if (Request.Files.Count > 0)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    if (hpf.ContentLength == 0)
                        continue;

                    var infoDocument = documents.lstParamDocuments.Where(x => x.Name == file).FirstOrDefault();
                    path = infoDocument.Path + @"\" + executiveID + @"\" + infoDocument.Folder + @"\";
                    fileName = executiveID + @"_" + infoDocument.Name + ".pdf";

                    //ManageDocuments.SaveFile(path, fileName, executiveID, infoDocument.Code, hpf);                    
                }
            }
        }
    }
}