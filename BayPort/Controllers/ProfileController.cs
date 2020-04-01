using System;
using System.Web.Mvc;
using Tags;
using Models;
using Entities;
using Newtonsoft.Json;
using System.Web;
using Helper;
using System.IO;


namespace BayPortColombia.Controllers
{
    [Autenticado]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Listado()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public JsonResult GetUserOptions(string ind_menu)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null) {
                Login();
                return null;
            }
            string executiveID = usr != null ? usr.userName : string.Empty;
            var userOptions = new ManagerUser().GetUserOptions(executiveID, ind_menu, Request.Url.GetLeftPart(UriPartial.Authority));
            return new JsonResult { Data = userOptions, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Login()
        {
            return RedirectToAction("Index","Home");
        }

        public JsonResult listSolicitudes()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().ListSolicitudes(usr.asesor);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveFormulario(string pestana, string formulario) {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().saveFormulario(pestana, usr.asesor, formulario);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult calcularOferta(string formulario)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            FormularioSolicitud form = JsonConvert.DeserializeObject<FormularioSolicitud>(formulario);
            var data = new ManageProfile().CalculoOferta(ref form);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult getSucursalAsesor()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            return new JsonResult { Data= usr, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult campoAdicional(double tipoSolicitud, double dependencia, double? producto, double periodo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().campoAdicional(tipoSolicitud, dependencia, producto, periodo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult buscarCliente(string rfc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rfcR = rfc.Substring(0, 10);
            var data = new ManageProfile().findRFC(rfcR);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult guardaDocumentoOriginacion(string documento)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            DocumentoOriginacion _doc = JsonConvert.DeserializeObject<DocumentoOriginacion>(documento);
            ManageDocuments manage = new ManageDocuments();
            manage.CargarArchivoOriginacion(ref _doc);
            return new JsonResult { Data = _doc, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult guardaDocumentoOriginacionCompra(string documento)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            DocumentoOriginacion _doc = JsonConvert.DeserializeObject<DocumentoOriginacion>(documento);
            ManageDocuments manage = new ManageDocuments();
            manage.CargarArchivoOriginacionCompra(ref _doc);
            return new JsonResult { Data = _doc, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult guardaDocumentoOriginacion1()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            HttpPostedFileBase hpf = Request.Files[0] as HttpPostedFileBase;
            DocumentoOriginacion _doc = new DocumentoOriginacion();
            //Extraer Documentos
            var ext = Path.GetExtension(hpf.FileName);
            //Crea Entidad para guardar
            _doc.folder = Request.Form["folder"];
            _doc.dependencia = double.Parse(Request.Form["dependencia"]);
            _doc.producto = double.Parse(Request.Form["dependencia"]);
            _doc.expedienteCompleto = 1;
            _doc.firma = 0;
            _doc.nombreDoc = hpf.FileName;
            _doc.codigo_doc = null;
            //DocumentoOriginacion _doc = JsonConvert.DeserializeObject<DocumentoOriginacion>(documento);
            ManageDocuments manage = new ManageDocuments();
            LogHelper.WriteLog("Controller", "ProfileController", "guardaDocumentoOriginacion1", new Exception(), "Subir Expediente Completo");
            manage.CargarExpedienteCompleto(ref _doc, hpf);
            LogHelper.WriteLog("Controller", "ProfileController", "guardaDocumentoOriginacion1", new Exception(), "Respuesta Carga de Expediente Completo");
            return new JsonResult { Data = _doc, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult buscarDocumentos(double folder, double codigoDoc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().buscarDocumentos(folder.ToString(), codigoDoc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult eliminaDocumentosOriginacion(double codigoDoc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().eliminaDocumentosOriginacion(codigoDoc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult eliminarItemCartera(double item, double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().eliminarItemCartera(item,folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult documentoOriginacion(double dependencia, double? producto, double folder, double doc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().documentoOriginacion(dependencia, producto,folder.ToString(), doc, rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult documentoCartera(double dependencia, double? producto, double folder, double doc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().documentoCartera(dependencia, producto,folder.ToString(), doc, rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult soloFirmas(double dependencia, double? producto,double folder)  
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().soloFirmas(dependencia, producto,folder.ToString(), rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public JsonResult Expedientillo(double dependencia, double? producto, double folder, double cambioDoc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().expedientillo(dependencia, producto,folder.ToString(), rootPath, baseUrl, cambioDoc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AllDocuments(double dependencia, double? producto, double folder, double cambioDoc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().allDocuments(dependencia, producto,folder.ToString(), rootPath, baseUrl, cambioDoc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult impresion(double dependencia, double? producto, double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().Imprimir(dependencia, producto,folder.ToString(), rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdSolicitud(double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().getIdSolicitud(folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getOfertasSeguros(double folder, double vlr_solcitado, double vlr_maximo )
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().getOfertasSeguros(folder.ToString(), vlr_solcitado, vlr_maximo, usr.asesor);
            TempData["offerPolicy"] = data.ofertas;
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult aceptarOferta(string formulario)
        {
            FormularioSolicitud _doc = JsonConvert.DeserializeObject<FormularioSolicitud>(formulario);

            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().aceptarOferta(ref _doc, usr.asesor);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult procesarOferta(double folder, double dependencia, double? producto)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().createSolicitud(folder.ToString().ToString(), dependencia, producto, rootPath, baseUrl, usr.sucursal);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult beneficiariosPoliza(double folder) {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().beneficiariosPoliza(folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult guardarBeneficiarioPoliza (string beneficiario, double poliza)
        {
            InBeneficiarioPoliza benef = JsonConvert.DeserializeObject<InBeneficiarioPoliza>(beneficiario);
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().guardarBeneficiarioPoliza(ref benef, poliza);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult entidadAbreviatura(string abreviatura)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().entidadAbreviatura(abreviatura);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Danny 2019 10*/
        public JsonResult getComentarios(double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().getComentarios(folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult getDashBoard()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().getDashBoard();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Danny 2019 10*/
        public JsonResult docPoliza(double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().docPoliza(folder.ToString(), rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult findEntidad(string entidadFederativa)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().findEntidad(entidadFederativa);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult findMunicipio(string Municipio)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().findMunicipio(Municipio);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult BuscaCodigoPostal(string codigoPostal)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().BuscaCodigoPostal(codigoPostal);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult cancelarFolio(double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().cancelarFolio(folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult findExpCompleto (double folder)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageProfile().findExpCompleto(folder.ToString());
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}