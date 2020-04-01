using System;
using System.Web.Mvc;
using Entities;
using Models;
using Newtonsoft.Json;

namespace BayPortColombia.Controllers
{
    public class DocumentsController : Controller
    {
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }
        // GET: Documents
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult guia()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Tablas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult guiaAs()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult TablasAs()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public JsonResult DownloadDocFiles(string FileName)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (FileName.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            try
            {
                char[] s = new char[FileName.Length - FileName.LastIndexOf("\\") - 1];
                FileName.CopyTo(FileName.LastIndexOf("\\") + 1, s, 0, FileName.Length - FileName.LastIndexOf("\\") - 1);
                string fileName = new string(s);
                string rootPath = Server.MapPath("~/Files");
                if (System.IO.File.Exists(rootPath + "\\" + fileName))
                {
                    System.IO.File.Delete(rootPath + "\\" + fileName);
                }
                System.IO.File.Copy(FileName, rootPath + "\\" + fileName);
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                string filevirtual = baseUrl + "/Files/" + fileName;
                OutGetDocument document = new OutGetDocument
                {
                    filename = fileName,
                    virtualpath = filevirtual
                };
                return new JsonResult { Data = document, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                throw new Exception("CodumentsController.DownloadDocFiles", ex);
            }
        }
        public JsonResult GetddSectorGuias()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetddSectorGuias();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetddSectorTablas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetddSectorTablas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //Asesor
        public JsonResult GetGuiasAs()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetGuiasAs();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTablasAs()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetTablasAs();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //Coordinador
        public JsonResult GetGuias()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetGuias();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetTablas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetTablas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetIdGuias(double codigo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetIdGuias(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetIdTablas(double codigo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageDocuments().GetIdTablas(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GuardarArchvio (string archivo, string opcion)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (opcion.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            ParamGuias _doc = JsonConvert.DeserializeObject<ParamGuias>(archivo);
            var data = new ManageDocuments().GuardarArchvio(ref _doc, opcion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EditarArchvio(string archivo, string opcion)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (opcion.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            ParamGuias _doc = JsonConvert.DeserializeObject<ParamGuias>(archivo);
            var data = new ManageDocuments().EditarArchivo(ref _doc, opcion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult EliminarArchvio(double codigo, string opcion)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (opcion.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageDocuments().EliminarArchvio(codigo, opcion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}