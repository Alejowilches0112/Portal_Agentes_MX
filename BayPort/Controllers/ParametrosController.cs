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
    public class ParametrosController : Controller
    {
        public ActionResult Parametros()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ListParametros()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Campos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Dependencias()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Descuentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Destino()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Empresas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Estado()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Ingreso()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Medio()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Periodos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Plazos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult DiasExp()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Sector()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult SectorGuia()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult SectorTablas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Puesto()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Reca()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Sucursales()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Tipo()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Nomina()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Producto()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Identificacion()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Documentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Paises()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult EntidadesFederativas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Municipios()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult ConfigDocumentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Bancos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult CasasFinancieras()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult clave_delegacion()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //Dias Expiracion
        [HttpGet]
        public JsonResult getDiasExp()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDiasExp();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdDiasExp(double dias)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdDiasExp(dias);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveDiasExp(double dias)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().saveDiasExp(dias);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* DEPENDENCIAS */
        [HttpGet]
        public JsonResult getDependencias()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDependencias();
            //var prueba = new ManageReadCSV().readCSV();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult getDependenciasActivas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDependenciasActivas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveDependencia(double codigo_dep, string dependencia, string estado)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string[] wordKey = new string[]{ "SELECT", "FROM", "WHERE", "="};
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (dependencia.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveDependencia(codigo_dep, dependencia, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updDependencia(double secuencia, string dependencia, string estado)
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
                if (dependencia.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updDependencia(secuencia, dependencia, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteDependencia(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteDependencia(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult loadDependencia(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().loadDependencia(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* TIPOS DE SOLICITUD */
        [HttpGet]
        public JsonResult getTipoSolicitud()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getTipoSolicitud();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdTipoSolicitud(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdTipoSolicitud(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveTipoSolicitud(string tipoSolicitud)
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
                if (tipoSolicitud.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveTipoSolicitud(tipoSolicitud);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updTipoSolicitud(double secuencia, string tipoSolicitud)
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
                if (tipoSolicitud.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updTipoSolicitud(secuencia, tipoSolicitud);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteTipoSolicitud(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteTipoSolicitud(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* PERIODOS APLICABLES */
        [HttpGet]
        public JsonResult getPeriodos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPeriodos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdPeriodos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdPeriodos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult savePeriodos(string Periodo)
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
                if (Periodo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().savePeriodos(Periodo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updPeriodos(double secuencia, string periodo)
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
                if (periodo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updPeriodos(secuencia, periodo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deletePeriodos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deletePeridos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Plazos */
        [HttpGet]
        public JsonResult getPlazos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPlazos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getPlazosPeriodo(double periodoAplicable)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPlazosPeriodo(periodoAplicable);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdPlazo(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdPlazo(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult savePlazos(double periodo, string plazo)
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
                if (plazo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().savePlazo(periodo, plazo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updPlazos(double secuencia, string plazo)
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
                if (plazo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updPlazo(secuencia, plazo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deletePlazos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deletePlazo(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Quincena Descuentos */
        [HttpGet]
        public JsonResult getQuincenaDescuentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getQuincenaDscto();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdQuincenaDescuentos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdQuincenaDscto(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveQuincenaDescuentos(string descuetno)
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
                if (descuetno.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveQuincenaDscto(descuetno);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult upQuincenaDescuentos(double secuencia, string descuento)
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
                if (descuento.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updQuincenaDscto(secuencia, descuento);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteQuincenaDescuentos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteQuincenaDscto(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Destino Créditos */
        [HttpGet]
        public JsonResult getDestinoCredito()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDestinoCredito();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdDestinoCredito(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdDestinoCredito(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveDestinoCredito(string destinoCredito)
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
                if (destinoCredito.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveDestinoCredito(destinoCredito);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updDestinoCredito(double secuencia, string destinoCredito)
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
                if (destinoCredito.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updDestinoCredito(secuencia, destinoCredito);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteDestinoCredito(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteDestinoCredito(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Empresa Telefonica */
        [HttpGet]
        public JsonResult getEmpresaTelefonica()
        {
            var data = new ManageParams().getEmpresaTelefonica();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdEmpresaTelefonica(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdEmpresaTelefonica(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveEmpresaTelefonica(string empresaTelefonica)
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
                if (empresaTelefonica.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveEmpresaTelefonica(empresaTelefonica);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updEmpresaTelefonica(double secuencia, string empresaTelefonica)
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
                if (empresaTelefonica.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updEmpresaTelefonica(secuencia, empresaTelefonica);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteEmpresaTelefonica(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteEmpresaTelefonica(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Tipos de indentificacion */
        [HttpGet]
        public JsonResult getTiposIdentificacion()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getTiposIdentificacion();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdTiposIdentificacion(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdTipoIdentificacion(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveTiposIdentificacion(string tipoIdentificacion)
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
                if (tipoIdentificacion.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveTiposIdentificacion(tipoIdentificacion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updTiposIdentificacion(double secuencia, string tipoIdentificacion)
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
                if (tipoIdentificacion.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updTiposIdetificacion(secuencia, tipoIdentificacion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteTiposIdentificacion(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteTiposIdentificacion(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Estado Civil */
        [HttpGet]
        public JsonResult getEstadoCivil()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getEstadoCivil();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdEstadoCivil(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdEstadoCivil(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveEstadoCivil(string estadoCivil)
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
                if (estadoCivil.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveEstadoCivil(estadoCivil);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updEstadoCivil(double secuencia, string estadoCivil)
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
                if (estadoCivil.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updEstadoCivil(secuencia, estadoCivil);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteEstadoCivil(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteEstadoCivil(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Ingresos */
        [HttpGet]
        public JsonResult getIngresos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getTiposIngresos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdIngresos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdIngresos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveIngresos(string Ingresos)
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
                if (Ingresos.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveIngresos(Ingresos);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updIngresos(double secuencia, string Ingresos)
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
                if (Ingresos.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updIngresos(secuencia, Ingresos);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteIngresos(double secuencia)
        {
            var data = new ManageParams().deleteIngresos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Medios de Disposicion */
        [HttpGet]
        public JsonResult getMediosDisposicion()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getMedios();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdMediosDisposicion(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdMedios(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveMediosDisposicion(string Medios)
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
                if (Medios.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveMedios(Medios);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updMediosDisposicion(double secuencia, string Medios)
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
                if (Medios.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updMedios(secuencia, Medios);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteMediosDisposicion(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteMedios(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Tipoos de Nomina */
        [HttpGet]
        public JsonResult getTipoNomina()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getNomina();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdTipoNomina(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdNomina(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveTipoNomina(string tipoNomina)
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
                if (tipoNomina.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveNomina(tipoNomina);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updTipoNomina(double secuencia, string tipoNomina)
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
                if (tipoNomina.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updNomina(secuencia, tipoNomina);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteTipoNomina(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteNomina(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Puestos */
        [HttpGet]
        public JsonResult getPuestos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPuestos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdPuestos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdPuesto(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult savePuestos(double sector, string puestos)
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
                if (puestos.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().savePuestos(sector, puestos);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updPuestos(double secuencia, string puestos)
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
                if (puestos.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updPuestos(secuencia, puestos);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deletePuestos(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deletePuestos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getPuestoSector(double sector)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPuestoSector(sector);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Reca */
        [HttpGet]
        public JsonResult getReca()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getReca();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdReca(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdReca(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveReca(string Reca)
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
                if (Reca.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveReca(Reca);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updReca(double secuencia, string Reca)
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
                if (Reca.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updReca(secuencia, Reca);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteReca(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteReca(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Sector */
        [HttpGet]
        public JsonResult getSector()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getSector();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdSector(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdSector(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveSector(string sector)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveSector(sector);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updSector(double secuencia, string sector)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updSector(secuencia, sector);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteSector(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteSector(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* SectorGuias */
        [HttpGet]
        public JsonResult getSectorGuias()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getSectorGuias();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdSectorGuias(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdSectorGuias(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveSectorGuias(string sector,double estado)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveSectorGuias(sector,estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updSectorGuias(double secuencia, string sector,double estado)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updSectorGuias(secuencia, sector, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteSectorGuias(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteSectorGuias(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* SectorTablas */
        [HttpGet]
        public JsonResult getSectorTablas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getSectorTablas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdSectorTablas(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdSectorTablas(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveSectorTablas(string sector, double estado)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveSectorTablas(sector, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updSectorTablas(double secuencia, string sector, double estado)
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
                if (sector.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updSectorTablas(secuencia, sector, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteSectorTablas(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteSectorTablas(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Sucursales */
        [HttpGet]
        public JsonResult getSucursales()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getSucursal();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdSucursales(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdSucursal(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveSucursales(string sucursal, string id_sucursal, double tipo, string division, string region, string emailCoordinador, string emailAuxiliar)
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
                if (sucursal.IndexOf(wordKey[i]) > -1 || id_sucursal.IndexOf(wordKey[i]) > -1 || division.IndexOf(wordKey[i]) > -1 || emailCoordinador.IndexOf(wordKey[i]) > -1 || emailAuxiliar.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveSucursal(sucursal, id_sucursal, tipo, division, region, emailCoordinador, emailAuxiliar);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updSucursales(double secuencia, string sucursal, double tipo_sucursal, string emailCoordinador, string emailAuxiliar)
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
                if (sucursal.IndexOf(wordKey[i]) > -1 || sucursal.IndexOf(wordKey[i]) > -1 || emailCoordinador.IndexOf(wordKey[i]) > -1 || emailAuxiliar.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updSucursal(secuencia, sucursal, tipo_sucursal, emailCoordinador, emailAuxiliar);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteSucursales(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteSucursal(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Productos */
        [HttpGet]
        public JsonResult getProductos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getProductos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getProductosDependencia(double dependencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getProductosDependencia(dependencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdProducto(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdProductos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveProductos(double codigo_pro, double dependencia, string producto, string estado)
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
                if (producto.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveProductos(codigo_pro,dependencia, producto, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updProducto(double secuencia, string producto, string estado)
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
                if (producto.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updProductos(secuencia, producto, estado);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteProducto(double secuencia)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteProductos(secuencia);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Carga Archivo CSV*/
        /*[HttpPost]
        public JsonResult savePlazoCSV(string periodo, string plazo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var Objperiodo = new ManageParams().findPeriodo(periodo);
            Response data = new Response();
            if(Objperiodo.secuencia != 0)
            {
                data = new ManageParams().savePlazo(Objperiodo.secuencia,plazo);
                if(data.errorCode == "200")
                {
                    data.errorMessage += ". " + periodo;
                }
            }
            else
            {
                data.errorCode = "77";
                data.errorMessage = "No se encontro el periodo indicado. "+periodo;
            }
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult savePuestoCSV(string sector, string puesto)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var Objsector = new ManageParams().findSector(sector);
            Response data = new Response();
            if (Objsector.secuencia != 0)
            {
                data = new ManageParams().savePuestos(Objsector.secuencia, puesto);
                if (data.errorCode == "200")
                {
                    data.errorMessage += ". " + sector;
                }
            }
            else
            {
                data.errorCode = "88";
                data.errorMessage = "No se encontro el sector indicado. " + sector;
            }
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult saveCamposCSV(string tipoSolicitud, string dependencia, string producto, string perido, string campo, string dato, string opciones, string obligatorio)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            ParamTipoSolicitud objSolciitud = new ParamTipoSolicitud();
            objSolciitud = new ManageParams().findTipoSolicitud(tipoSolicitud);
            ParamDependecia objDependencia = new ParamDependecia();
            objDependencia = new ManageParams().findDependencia(dependencia);
            ParamProducto objProducto = new ParamProducto();
            objProducto = new ManageParams().findProducto(producto,objDependencia.secuencia);
            ParamPeriodos objPeriodo = new ParamPeriodos();
            objPeriodo = new ManageParams().findPeriodo(perido);

            Response data = new Response();
            if (objDependencia.secuencia == 0)
            {
                data.errorCode = "66";
                data.errorMessage = "No se encontro la Dependencia indicada. " + dependencia;
            }
            else
            {
                double? product = (objProducto.secuencia == 0) ? (null) : objProducto.secuencia;
                double? solicit = (objSolciitud.secuencia == 0) ? (null) : objSolciitud.secuencia;
                double? period = (objPeriodo.secuencia == 0) ? (null) : objPeriodo.secuencia;
                data = new ManageParams().saveCampos(solicit, objDependencia.secuencia, product, period, campo, dato, opciones, obligatorio);
            }
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }*/
        /*Division*/
        public JsonResult getDivision()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDivision();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getRegionDivision(string division)
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
                if (division.IndexOf(wordKey[i]) > -1 )
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().getRegionDivision(division);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getTipoSucursal()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getTipoSucursal();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*PAISES*/
        public JsonResult savePais(double codigo, string nombre, string codigo_pais)
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
                if (nombre.IndexOf(wordKey[i]) > -1 || codigo_pais.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().savePais(codigo, nombre, codigo_pais, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getPais()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getPais();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdPais(double id_pais)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdPais(id_pais);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updPais(double id_pais, string nombre_pais, string codigo_pais) 
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
                if (nombre_pais.IndexOf(wordKey[i]) > -1 || codigo_pais.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updPais(id_pais, nombre_pais, codigo_pais, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deletePais(double id_pais)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deletePais(id_pais);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Entidades Federativas */
        public JsonResult saveEntidad(double codigo_pais, double codigo, string nombre, string abreviatura)
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
                if (nombre.IndexOf(wordKey[i]) > -1 || abreviatura.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveEntidad(codigo_pais, codigo, nombre, usr.userName, abreviatura);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updEntidad(double codigo_entidad, string nombre_entidad, string abreviatura)
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
                if (nombre_entidad.IndexOf(wordKey[i]) > -1 || abreviatura.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updEntidad(codigo_entidad, nombre_entidad, usr.userName, abreviatura);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getEntidadFederativa()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getEntidad();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getEntidadPais(double pais)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getEntidadPais(pais);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdEntidad(double codigo_entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdEntidad(codigo_entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteEntidad(double codigo_entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteEntidad(codigo_entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Municipios*/
        /*[HttpPost]
        public JsonResult saveMunicipio(double cod_pais, double codigo_entidad, double codigo, string nombre)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().saveMunicipio(cod_pais,codigo_entidad, codigo, nombre, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updMunicipio(double codigo_municipio, string municipio, double pais, double entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().updMunicipio(codigo_municipio, municipio, usr.userName, pais, entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteMunicipio(double codigo_municipio, double pais, double entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteMunicipio(codigo_municipio, pais, entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getMunicipios()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getMunicipios();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getMunicipioEntidad(double pais, double entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getMunicipioEntidad(pais, entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdMunicipios(double codigo_municipio, double pais, double entidad)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdMunicipios(codigo_municipio, pais, entidad);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }*/
        /* Campos Parámetrizables */
        [HttpPost]
        public JsonResult saveCampos(double? tipoSolicitud, double dependencia, double? producto, double? periodo, string campo, string tipo, string opciones, string requerido)
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
                if (campo.IndexOf(wordKey[i]) > -1 || tipo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            producto = (producto == 0) ? null : producto;
            var data = new ManageParams().saveCampos(tipoSolicitud, dependencia, producto, periodo, campo, tipo, opciones, requerido);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult getCampos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getCampos();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updCampos(double cod_campo, string campo, string tipo, string opciones, string requerido)
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
                if (campo.IndexOf(wordKey[i]) > -1 || tipo.IndexOf(wordKey[i]) > -1 || opciones.IndexOf(wordKey[i]) > -1 || requerido.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updCampos(cod_campo, campo, tipo, opciones, requerido);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdCampos(double cod_campo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }

            var data = new ManageParams().getIdCampos(cod_campo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteCampos(double cod_campo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteCampos(cod_campo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Documentos */
        [HttpPost]
        public JsonResult getDocumentos(double dependencia, double? producto)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDocumentos(dependencia, producto);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getIdDocumentos(double cod_documento)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdDocumentos(cod_documento);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveDocumentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            documents _documents = JsonConvert.DeserializeObject<documents>(Request.Form["Documento"]);
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (_documents.nombre.IndexOf(wordKey[i]) > -1 || _documents.nombreDoc.IndexOf(wordKey[i]) > -1 || _documents.dependencia.IndexOf(wordKey[i]) > -1 || (_documents.path != null &&_documents.path.IndexOf(wordKey[i]) > -1) || _documents.producto.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            if (Request.Files.Count > 0)
            {
                _documents.file = "EXISTE";
            }
            HttpPostedFileBase hpf = Request.Files[0] as HttpPostedFileBase;
            //documents _documents = JsonConvert.DeserializeObject<documents>(documento);
            var data = new ManageParams().saveDocumentos(ref _documents, usr.asesor, hpf);
            return new JsonResult { Data = _documents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updDocumentos()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            documents _documents = JsonConvert.DeserializeObject<documents>(Request.Form["Documento"]);
            string[] wordKey = new string[] { "SELECT", "FROM", "WHERE", "=" };
            for (var i = 0; i < wordKey.Length; i++)
            {
                if (_documents.nombre.IndexOf(wordKey[i]) > -1 || (_documents.nombreDoc != null && _documents.nombreDoc.IndexOf(wordKey[i]) > -1) || _documents.dependencia.IndexOf(wordKey[i]) > -1 || _documents.path.IndexOf(wordKey[i]) > -1 || (_documents.producto != null &&_documents.producto.IndexOf(wordKey[i]) > -1))
                {
                    Login();
                    return null;
                }
            }
            HttpPostedFileBase hpf = null;
            if (Request.Files.Count > 0)
            {
                _documents.file = "EXISTE";
                hpf = Request.Files[0] as HttpPostedFileBase;
            }

            var data = new ManageParams().updDocumentos(ref _documents, usr.asesor, hpf);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteDocumentos(double cod_documento)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteDocumentos(cod_documento);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getDocumentosConfig(double dependencia, double? producto)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDocumentosConfig(dependencia, producto);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getDocumentosOriginacion(double dependencia, double? producto)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getDocumentosOriginacion(dependencia, producto);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*Configuración de Documentos*/
        [HttpPost]
        public JsonResult saveConfigDoc(double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
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
                if (dato.IndexOf(wordKey[i]) > -1 || valida.IndexOf(wordKey[i]) > -1 || (campoComparar != null && campoComparar.IndexOf(wordKey[i]) > -1) || (valorComparar != null && valorComparar.IndexOf(wordKey[i]) > -1))
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveConfigDoc(documento, obtencion, x, y, dato, pagina, fuente, valida, campoComparar, valorComparar, variacion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult updConfigDoc(double codigo, double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
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
                if (dato.IndexOf(wordKey[i]) > -1 || valida.IndexOf(wordKey[i]) > -1 || (campoComparar != null && campoComparar.IndexOf(wordKey[i]) > -1) || (campoComparar != null && valorComparar.IndexOf(wordKey[i]) > -1))
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updConfigDoc(codigo, documento, obtencion, x, y, dato, pagina, fuente, valida, campoComparar, valorComparar, variacion);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult deleteConfigDoc(double codigo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteConfigDoc(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult getConfigDocumentos(double dependencia, double? producto, double? doc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getConfigDocumentos(dependencia, producto, doc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdConfigDocumentos(double codigo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getIdConfigDocumentos(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult pruebaConfiguracion(double dependencia, double? producto, double doc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            string rootPath = Server.MapPath("~/Files");
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            var data = new ManageProfile().pruebaConfiguracion(dependencia, producto, doc, rootPath, baseUrl);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult camposFormulario()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().camposFormulario();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //Bancos
        public JsonResult saveBanco(string codigo, string banco)
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
                if (codigo.IndexOf(wordKey[i]) > -1 || banco.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveBanco(codigo, banco, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updBanco(string codigo, string banco)
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
                if (codigo.IndexOf(wordKey[i]) > -1 || banco.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updBanco(codigo, banco, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteBanco(string codigo)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteBanco(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getBanco()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getBanco();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdBanco(string codigo)
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
                if (codigo.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().getIdBanco(codigo);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult parentesco()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().parentesco();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /* Casas Financieras */
        [HttpGet]
        public JsonResult getCasas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getCasas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public JsonResult getCasasActivas()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getCasasActivas();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult saveCasa(string rfc, string casa_financiera, string estado)
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
                if (rfc.IndexOf(wordKey[i]) > -1 || casa_financiera.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveCasa(rfc, casa_financiera, estado, usr.asesor);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updCasa(string rfc, string casa_financiera, string estado)
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
                if (rfc.IndexOf(wordKey[i]) > -1 || casa_financiera.IndexOf(wordKey[i]) > -1 || estado.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updCasa(rfc, casa_financiera, estado, usr.asesor);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteCasa(string rfc)
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().deleteCasa(rfc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdCasas(string rfc)
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
                if (rfc.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().getIdCasas(rfc);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /*CLAVE DELEGACION*/
        public JsonResult getClaves()
        {
            var usr = (Login)System.Web.HttpContext.Current.Session["usr"];
            if (usr == null)
            {
                Login();
                return null;
            }
            var data = new ManageParams().getClaves();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getIdClaves(string cod)
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
                if (cod.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().getIdClaves(cod);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult saveClaves(string cod, string delg)
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
                if (cod.IndexOf(wordKey[i]) > -1 || delg.IndexOf(wordKey[i]) > -1 )
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().saveClaves(cod, delg, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult updClaves(string cod, string delg)
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
                if (cod.IndexOf(wordKey[i]) > -1 || delg.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().updClaves(cod, delg, usr.userName);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult deleteClaves(string cod)
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
                if (cod.IndexOf(wordKey[i]) > -1)
                {
                    Login();
                    return null;
                }
            }
            var data = new ManageParams().deleteClaves(cod);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}