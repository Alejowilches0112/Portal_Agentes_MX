using DAO;
using Entities;
using Helper;
using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf.Layer;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font.Constants;
using iText.Layout;
using iText.IO.Image;

namespace Models
{
    public class ManageProfile
    {
        public OutSolicitudProgreso ListSolicitudes(double asesor)
        {
            OutSolicitudProgreso data = new OutSolicitudProgreso();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.listSolicitudes(asesor);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "listSolicitudes", ex, "");
            }
            return data;
        }
        public OutSolicitudProgreso saveFormulario(string pestana, double usr, string formulario)
        {
            OutSolicitudProgreso data = new OutSolicitudProgreso();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.saveFormulario(pestana, usr, formulario);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "saveFormulario", ex, "");
            }
            return data;
        }
        public Response eliminarItemCartera(double item, string folder)
        {
            Response data = new Response();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.eliminarCartera(item, folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "saveFormulario", ex, "");
            }
            return data;
        }
        public OutParamCampos campoAdicional(double tipoSolicitud, double dependencia, double? producto, double periodo)
        {
            OutParamCampos data = new OutParamCampos();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.campoAdicional(tipoSolicitud, dependencia, producto, periodo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageProfile", "campoAdicional", ex, "");
            }
            return data;
        }
        public OutSRFCServicioCliente findRFC(string rfc)
        {
            string soapResult = "";
            OutSRFCServicioCliente responseR = new OutSRFCServicioCliente();
            List<SRFCServicioCliente> list = new List<SRFCServicioCliente>();
            SRFCServicioCliente data;
            try
            {
                HttpWebRequest request = CreateWebRequest("OriginacionRFC");
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string xmlRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:cli=\"http://mx.com.bayport.ws/exposition/originacion/cliente\">" +
                    "<soapenv:Header/> <soapenv:Body> <cli:buscarPorRfc><request>" +
                        addParm("rfc", rfc) +
                        "</request></cli:buscarPorRfc></soapenv:Body></soapenv:Envelope>";
                LogHelper.WriteLog("Models", "ManageProfile", "findRFC ->" + rfc, "", xmlRequest, "Busqueda RFC");
                soapEnvelopeXml.LoadXml(xmlRequest);
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                TextReader tr = new StringReader(soapResult);
                LogHelper.WriteLog("Models", "ManageProfile", "findRFC Result", "", soapResult, "");
                var xml = XDocument.Load(tr);
                XmlTextReader tr2 = new XmlTextReader(new StringReader(xml.Root.ToString()));
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(soapResult);

                //string jsonxml = JsonConvert.SerializeXmlNode(xt);


                XmlNodeList xmlnode;
                if (xt.GetElementsByTagName("codigo").Item(0).InnerText == "000")
                {
                    XmlNodeList xmlnode2 = xt.GetElementsByTagName("cliente");
                    for (var ib = 0; ib <= xmlnode2.Count - 1; ib++)
                    {
                        data = new SRFCServicioCliente();
                        data.numero = (xmlnode2[ib].ChildNodes[0].InnerText != null) ? xmlnode2[ib].ChildNodes[0].InnerText : "";
                        //<identificacion>
                        foreach (XmlNode iden in xmlnode2[ib].ChildNodes[1])
                        {
                            switch (iden.Name)
                            {
                                case "nombre":
                                    data.primer_nombre = (iden["primero"] != null) ? iden["primero"].InnerText : "";
                                    data.segundo_nombre = (iden["segundo"] != null) ? iden["segundo"].InnerText : "";
                                    break;
                                case "apellido":
                                    data.primer_apellido = (iden["primero"] != null) ? iden["primero"].InnerText : "";
                                    data.segundo_apellido = (iden["segundo"] != null) ? iden["segundo"].InnerText : "";
                                    break;
                                case "nacimiento":
                                    data.fecha_nacimiento = (iden["fecha"] != null) ? iden["fecha"].InnerText : "";
                                    break;
                                case "numero":
                                    break;


                            }
                        }
                        //</identificacion>
                        //<generales>
                        foreach (XmlNode gen in xmlnode2[ib].ChildNodes[2])
                        {
                            switch (gen.Name)
                            {
                                case "rfc":
                                    data.rfc = (gen != null) ? gen.InnerText : "";
                                    break;
                                case "curp":
                                    data.curp = (gen != null) ? gen.InnerText : "";
                                    break;
                                case "genero":
                                    data.genero = (gen != null) ? gen.InnerText : "";
                                    break;
                                case "nacionalidad":
                                    data.nacionalidad = (gen != null) ? gen.InnerText : "";
                                    break;
                                case "email":
                                    data.email = (gen != null) ? gen.InnerText : "";
                                    break;
                                case "telefono":
                                    data.celular = (gen != null) ? double.Parse(gen.InnerText) : 0;
                                    break;
                            }
                        }
                        //</generales>
                        //<domicilio>
                        foreach (XmlNode domi in xmlnode2[ib].ChildNodes[3])
                        {
                            switch (domi.Name)
                            {
                                case "direccion":
                                    data.codigo_postal = (domi["codigoPostal"] != null) ? double.Parse(domi["codigoPostal"].InnerText) : 0;
                                    data.colonia_Domicilio = (domi["colonia"] != null) ? domi["colonia"].InnerText : "";
                                    data.muncipio_domicilio = (domi["municipioDelegacion"] != null) ? domi["municipioDelegacion"].InnerText : "";
                                    data.domicilio_Calle = (domi["domicilioCalle"] != null) ? domi["domicilioCalle"].InnerText : "";
                                    data.entre_calles_domicilio = (domi["entreCalles"] != null) ? domi["entreCalles"].InnerText : "";
                                    break;
                                case "paisResidencia":
                                    data.pais = (domi != null) ? domi.InnerText : "";
                                    break;
                                case "tiempoResidencia":
                                    data.tiempo_residencia = (domi != null) ? double.Parse(domi.InnerText) : 0;
                                    break;
                            }
                        }
                        //</domicilio>                                                         
                        list.Add(data);
                    }
                    responseR.ListRfcs = list;
                    responseR.estatus = xt.GetElementsByTagName("codigo").Item(0).InnerText;
                }
                else
                {
                    xmlnode = xt.GetElementsByTagName("error");
                    for (var i = 0; i < xmlnode.Count - 1; i++)
                    {
                        responseR.estatus = xmlnode[i].ChildNodes.Item(0).InnerText.Trim(); ;
                        responseR.descripcionMovimiento = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                responseR.estatus = "-10";
                responseR.descripcionMovimiento = ex.ToString();
                LogHelper.WriteLog("Models", "ManageProfile", "findRFC", ex, rfc);
            }
            return responseR;
        }
        public OutServiceCodigoPostal BuscaCodigoPostal(string codigoPostal)
        {
            string soapResult = "";
            OutServiceCodigoPostal data = new OutServiceCodigoPostal();
            try
            {
                HttpWebRequest request = CreateWebRequest("CodigoPostal");
                XmlDocument soapEnvelopeXml = new XmlDocument();
                string xmlRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:cod=\"http://mx.com.bayport.ws/exposition/codigospostales\">" +
                "<soapenv:Header/><soapenv:Body><cod:consultarPorCodigo><request>" +
                        addParm("codigoPostal", codigoPostal) +
                        "</request></cod:consultarPorCodigo></soapenv:Body></soapenv:Envelope>";
                LogHelper.WriteLog("Models", "ManageProfile", "CodigoPostal", "", xmlRequest, "CodigoPostal");
                soapEnvelopeXml.LoadXml(xmlRequest);
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                TextReader tr = new StringReader(soapResult);
                LogHelper.WriteLog("Models", "ManageProfile", "CodigoPostal Result", "", soapResult, "");
                var xml = XDocument.Load(tr);
                XmlTextReader tr2 = new XmlTextReader(new StringReader(xml.Root.ToString()));
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(soapResult);
                XmlNodeList xmlnode;
                if (xt.GetElementsByTagName("codigo").Item(0).InnerText == "000")
                {
                    var estado = xt.GetElementsByTagName("estado").Item(0).InnerText;
                    data.estado = new ProfileDAO().findEntidad(estado.ToUpper());
                    data.municipio = xt.GetElementsByTagName("municipio").Item(0).InnerText;
                    xmlnode = xt.GetElementsByTagName("colonias");
                    List<string> list = new List<string>();
                    for (var i = 0; i < xmlnode[0].ChildNodes.Count; i++)
                    {
                        list.Add(xmlnode[0].ChildNodes.Item(i).InnerText.Trim());
                    }
                    data.colonias = list;
                    data.estatus = xt.GetElementsByTagName("codigo").Item(0).InnerText;
                }
                else
                {
                    xmlnode = xt.GetElementsByTagName("error");
                    for (var i = 0; i < xmlnode.Count - 1; i++)
                    {
                        data.estatus = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                        data.descripcionMovimiento = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    }
                }

            }
            catch (Exception ex)
            {
                data.estatus = "-10";
                data.descripcionMovimiento = "El servicio no esta disponible";
                LogHelper.WriteLog("Models", "ManageProfile", "CodigoPostal", ex, codigoPostal);
            }
            return data;
        }
        public OutCalculoServicioCalculo CalculoOferta(ref FormularioSolicitud form)
        {
            string soapResult = "";
            string xmlRequest = "";
            OutCalculoServicioCalculo data = new OutCalculoServicioCalculo();
            ParamsDAO dao = new ParamsDAO();
            try
            {
                var dependencia = new ManageParams().loadDependencia(form.Dependencia).ListDependencias[0].dependencia;
                var producto = "";
                if (form.producto != 0)
                {
                    producto = new ManageParams().getIdProductos(form.producto).ListProductos[0].producto;
                }
                var periodo = new ManageParams().getIdPeriodos(form.periodo).ListPeriodos[0].periodo;
                var solicitud = new ParamsDAO().getIdTipoSolicitud(form.tipoSolicitud).ListTipoSolicitud[0].tipoSolicitud;
                HttpWebRequest request = CreateWebRequest("CalculoOferta");
                XmlDocument soapEnvelopeXml = new XmlDocument();
                xmlRequest = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:tem=\"http://tempuri.org/\"><soap:Header/><soap:Body><tem:calculo_descuento>" +
                        addParm("tem:dependencia", dependencia) +
                        addParm("tem:producto", producto) +
                        addParm("tem:monto_solicitado", form.monto) +
                        addParm("tem:Periodo", periodo) +
                        addParm("tem:plazo", form.plazo) +
                        addParm("tem:liquido_base", form.LBase) +
                        addParm("tem:tipo_sol", solicitud) +
                        addParm("tem:Monto_deudor", form.monto_deudor) +
                        "</tem:calculo_descuento></soap:Body></soap:Envelope> ";
                LogHelper.WriteLog("Models", "ManageProfile", "CalculoOferta", "", xmlRequest, "Calcular Oferta");
                soapEnvelopeXml.LoadXml(xmlRequest);
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                TextReader tr = new StringReader(soapResult);
                //LogHelper.WriteLog("Models", "ManageProfile", "findRFC", "", soapResult, "");
                var xml = XDocument.Load(tr);
                XmlTextReader tr2 = new XmlTextReader(new StringReader(xml.Root.ToString()));
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(soapResult);


                data.estatus = xt.GetElementsByTagName("Codigo").Item(0).InnerText;
                if (data.estatus != "0")
                {
                    data.descripcionMovimiento = xt.GetElementsByTagName("Error").Item(0).InnerText;
                }
                else
                {
                    data.fecha_1pago = (xt.GetElementsByTagName("fecha_1pago").Item(0).InnerText);
                    data.fecha_upago = (xt.GetElementsByTagName("fecha_upago").Item(0).InnerText);
                    data.capacidad_pago = (xt.GetElementsByTagName("capacidad_pago").Item(0).InnerText);
                    data.monto_maxp = (xt.GetElementsByTagName("monto_maxp").Item(0).InnerText);
                    data.descuento = (xt.GetElementsByTagName("descuento").Item(0).InnerText);
                    data.tasa_anual = (xt.GetElementsByTagName("tasa_anual").Item(0).InnerText);
                    data.cat = (xt.GetElementsByTagName("cat").Item(0).InnerText);
                    var qna = xt.GetElementsByTagName("qna_descuento").Item(0).InnerText;
                    var quincena = dao.quincenaDescuento(qna);
                    data.qna_descuento = quincena;
                }
            }
            catch (Exception ex)
            {
                data.estatus = "-10";
                data.descripcionMovimiento = "El servicio no esta disponible " + ex;
                LogHelper.WriteLog("Models", "ManageProfile", "CalculoOferta", ex, xmlRequest);
            }
            return data;
        }
        public Response createSolicitud(string folder, double dependencia, double? producto, string rootpath, string baseurl, double sucursal)
        {
            string soapResult = "";
            FormularioSolicitudDocs inOriginacionServicioSolicitud = new FormularioSolicitudDocs();
            Response data = new Response();
            ProfileDAO dao = new ProfileDAO();
            try
            {
                try
                {
                    inOriginacionServicioSolicitud = dao.solicitud(folder);
                }
                catch (Exception e)
                {
                    data.errorCode = "88";
                    data.errorMessage = "Error consultando los datos";
                    LogHelper.WriteLog("Models", "ManageProfile", "createSolicitud", e, inOriginacionServicioSolicitud.RFC);
                    return data;
                }
                HttpWebRequest request = CreateWebRequest("OfertaOriginacion");
                XmlDocument soapEnvelopeXml = new XmlDocument();
                inOriginacionServicioSolicitud.medioDisp = (inOriginacionServicioSolicitud.medioDisp.IndexOf("DEPOSITO") > -1) ? "SPEI" : inOriginacionServicioSolicitud.medioDisp;
                inOriginacionServicioSolicitud.medioDispAlt = (inOriginacionServicioSolicitud.medioDispAlt.IndexOf("DEPOSITO") > -1) ? "SPEI" : inOriginacionServicioSolicitud.medioDisp;

                string xmlRequest = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sol=\"http://mx.com.bayport.ws/exposition/originacion/solicitud\">" +
                "<soapenv:Header/><soapenv:Body><sol:creacionActualizacion><request><solicitud>" +
                                addParm("id", inOriginacionServicioSolicitud.id_sibel) +
                                addParm("tipo", inOriginacionServicioSolicitud.tipoSolicitud) +
                                /*addParm("dependenciaTrabajo", inOriginacionServicioSolicitud.Dependencia) +
                                addParm("producto", inOriginacionServicioSolicitud.producto) +*/
                                addParm("montoSolicitado", double.Parse(inOriginacionServicioSolicitud.monto.Replace(",", "."))) +
                                addParm("periodo", inOriginacionServicioSolicitud.periodo) +
                                addParm("plazo", (inOriginacionServicioSolicitud.plazo)) +
                                addParm("destinoCredito", inOriginacionServicioSolicitud.destino.Replace(",", ".")) +
                                //addParm("tipoNomina", inOriginacionServicioSolicitud.tNomina) +
                                //addParm("plazas", (inOriginacionServicioSolicitud.nPlazas)) +
                                addParm("descuento", double.Parse(inOriginacionServicioSolicitud.dscto.Replace(",", "."))) +
                                /*addParm("tasaAnual", inOriginacionServicioSolicitud.tAnual) +
                                addParm("cat", inOriginacionServicioSolicitud.cat) +
                                addParm("sucursal", inOriginacionServicioSolicitud.sucursal) +
                                addParm("quincenaDescuento", inOriginacionServicioSolicitud.quincenaDscto) +
                                addParm("fechaPrimerPago", inOriginacionServicioSolicitud.fchPrPago) +
                                addParm("fechaUltimoPago", inOriginacionServicioSolicitud.fchUltPago) +*/
                                addParm("liquidoBase", double.Parse(inOriginacionServicioSolicitud.LBase.Replace(",", "."))) +
                                addParm("capacidadPago", double.Parse(inOriginacionServicioSolicitud.cPago.Replace(",", "."))) +
                                //addParm("montoMaximo", inOriginacionServicioSolicitud.mMaxPlaz) +
                                addParm("fechaSolicitud", inOriginacionServicioSolicitud.fchsolicitud.Replace(",", ".")) +
                                //addParm("asesor", inOriginacionServicioSolicitud.asesor) +
                                addParm("folio", double.Parse(inOriginacionServicioSolicitud.folderNumber)) +
                                addParm("montoDeudor", double.Parse(inOriginacionServicioSolicitud.monto_deudor.Replace(",", "."))) +
                                //addParm("matricula", inOriginacionServicioSolicitud.matricula) +
                                addParm("nss", (inOriginacionServicioSolicitud.nss)) +
                                addParm("grupo", inOriginacionServicioSolicitud.grupo) +
                                addParm("estatus", inOriginacionServicioSolicitud.estado) +
                            //addParm("claveTrabajador", inOriginacionServicioSolicitud.clave_trabajdor) +
                            //addParm("especificar", inOriginacionServicioSolicitud.especificar) +
                            //addParm("reca", inOriginacionServicioSolicitud.reca) +
                            "</solicitud>" +
                            "<cliente>" +
                                "<nombre>" +
                                    addParm("primero", inOriginacionServicioSolicitud.pNombre) +
                                    addParm("segundo", inOriginacionServicioSolicitud.sNombre) +
                                "</nombre>" +
                                "<apellido>" +
                                    addParm("primero", inOriginacionServicioSolicitud.pApellido) +
                                    addParm("segundo", inOriginacionServicioSolicitud.sApellido) +
                                "</apellido>" +
                                addParm("rfc", inOriginacionServicioSolicitud.RFC) +
                                addParm("curp", inOriginacionServicioSolicitud.CURP) +
                                addParm("genero", inOriginacionServicioSolicitud.gender) +
                                addParm("estadoCivil", inOriginacionServicioSolicitud.estadoCivil) +
                                "<nacimiento>" +
                                    addParm("fecha", inOriginacionServicioSolicitud.fecNac) +
                                    addParm("pais", inOriginacionServicioSolicitud.paisN) +
                                    addParm("lugar", inOriginacionServicioSolicitud.entidadN) +
                                "</nacimiento>" +
                                //addParm("nacionalidad", inOriginacionServicioSolicitud.nacionalidad) +
                                //addParm("paisResidencia", inOriginacionServicioSolicitud.paisR) +
                                addParm("formaMigratoria", inOriginacionServicioSolicitud.fMigratoria) +
                                addParm("identificacionOficial", inOriginacionServicioSolicitud.tipoDoc) +
                            //addParm("otraIdentificacion", inOriginacionServicioSolicitud.otraIdentificacion) +
                            "</cliente>" +
                            "<ocupacion>" +
                                //addParm("sector", inOriginacionServicioSolicitud.sector) +
                                //addParm("puesto", inOriginacionServicioSolicitud.puesto) +
                                addParm("tiempoAntig", inOriginacionServicioSolicitud.antiguedad) +
                                //addParm("ingresoNetoMensual", inOriginacionServicioSolicitud.ingresos) +
                                addParm("telefonoOficina", inOriginacionServicioSolicitud.telFijo) +
                            /*addParm("clavePresupuestal", inOriginacionServicioSolicitud.cPresupuestal) +
                            addParm("pagaduria", inOriginacionServicioSolicitud.pagaduria) +
                            addParm("fechaIngreso", inOriginacionServicioSolicitud.fechaIngreso) +
                            addParm("clave", inOriginacionServicioSolicitud.clave) +
                            addParm("lugarTrabajo", inOriginacionServicioSolicitud.lugarTrabajo) +
                            "<direccion>" +
                                addParm("codigoPostal", inOriginacionServicioSolicitud.codigoPostalOcupacion) +
                                addParm("entidad", inOriginacionServicioSolicitud.entidadOcupacion) +
                                addParm("municipioDelegacion", inOriginacionServicioSolicitud.municipioDelegacionOcupacion) +
                                addParm("colonia", inOriginacionServicioSolicitud.coloniaOcupacion) +
                                addParm("otra", inOriginacionServicioSolicitud.otraOcupacion) +
                                addParm("domicilioCalle", inOriginacionServicioSolicitud.domicilioCalleOcupacion) +
                                addParm("numeroExterior", inOriginacionServicioSolicitud.numeroExteriorOcupacion) +
                                addParm("numeroInterior", inOriginacionServicioSolicitud.numeroInteriorOcupacion) +
                                addParm("entreCalles", inOriginacionServicioSolicitud.entreCallesOcupacion) +
                            "</direccion>" +
                            "<telefonoOficina>" +
                                addParm("telefono", inOriginacionServicioSolicitud.telefono) +
                                addParm("extension", inOriginacionServicioSolicitud.extension) +
                            "</telefonoOficina>" +
                            "<cargoPublico>" +
                                "<propio>" +
                                addParm("tieneCargoPublico", inOriginacionServicioSolicitud.tieneCargoPublicoP) +
                                addParm("nombre", inOriginacionServicioSolicitud.nombreCargoP) +
                                addParm("puesto", inOriginacionServicioSolicitud.puestoCargoP) +
                                addParm("periodoEjercicio", inOriginacionServicioSolicitud.periodoEjercicioP) +
                                "</propio>" +
                                "<familiar>" +
                                addParm("tieneCargoPublico", inOriginacionServicioSolicitud.tieneCargoPublicoF) +
                                addParm("nombre", inOriginacionServicioSolicitud.nombreCargoF) +
                                addParm("puesto", inOriginacionServicioSolicitud.puestoCargoF) +
                                addParm("periodoEjercicio", inOriginacionServicioSolicitud.periodoEjercicioF) +
                                "</familiar>" +
                            "</cargoPublico>" +
                            addParm("beneficiario", inOriginacionServicioSolicitud.beneficiario) +
                            addParm("beneficiarioNombre", inOriginacionServicioSolicitud.nombreBeneficiario) +
                            addParm("categoriaTipoPension", inOriginacionServicioSolicitud.categoriaTipoPension) +
                            addParm("adscripcionUbicacionPago", inOriginacionServicioSolicitud.adscripcionUbicacionPago) +
                            addParm("delegacion", inOriginacionServicioSolicitud.delegacion) +
                            "<testigos>" +
                                "<testigo1>" +
                                addParm("nombre", inOriginacionServicioSolicitud.nombreT1) +
                                addParm("matricula", inOriginacionServicioSolicitud.matriculaT1) +
                                addParm("gafete", inOriginacionServicioSolicitud.gafeteT1) +
                                "</testigo1>" +
                                "<testigo2>" +
                                addParm("nombre", inOriginacionServicioSolicitud.nombreT2) +
                                addParm("matricula", inOriginacionServicioSolicitud.matriculaT2) +
                                addParm("gafete", inOriginacionServicioSolicitud.gafeteT2) +
                                "</testigo2>" +
                            "</testigos>" +*/
                            "</ocupacion>" +
                            "<domicilio>" +
                                "<direccion>" +
                                    addParm("codigoPostal", inOriginacionServicioSolicitud.codPostDom) +
                                    /*addParm("municipioDelegacion", inOriginacionServicioSolicitud.municipioDelegacionDomicilio) +
                                    addParm("colonia", inOriginacionServicioSolicitud.coloniaDomicilio) +
                                    addParm("otra", inOriginacionServicioSolicitud.otraDomicilio) +*/
                                    addParm("domicilioCalle", inOriginacionServicioSolicitud.domicilioCalle) +
                                    addParm("numeroExterior", inOriginacionServicioSolicitud.noExteriorDom) +
                                    addParm("numeroInterior", inOriginacionServicioSolicitud.noInteriorDom) +
                                    addParm("entreCalles", inOriginacionServicioSolicitud.entreCalleDom) +
                                    addParm("pais", inOriginacionServicioSolicitud.paisR) +
                                "</direccion>" +
                                //addParm("tiempoResidencia", inOriginacionServicioSolicitud.tiempoResidencia) +
                                addParm("email", inOriginacionServicioSolicitud.emailContacto) +
                                "<telefonos>" +
                                    addParm("celular", (inOriginacionServicioSolicitud.CelularContacto)) +
                                    addParm("proveedor", inOriginacionServicioSolicitud.CompanyPhone.Replace("&", "&amp;")) +
                                    addParm("propioRecados", (inOriginacionServicioSolicitud.telefonoPropio)) +
                                "</telefonos>" +
                            "</domicilio>" +
                            /*"<referenciasPersonales>" +
                                "<referencia1>" +
                                    addParm("nombres", inOriginacionServicioSolicitud.nombreRef1) +
                                    "<apellido>" +
                                    addParm("primero", inOriginacionServicioSolicitud.pApellidoRef1) +
                                    addParm("segundo", inOriginacionServicioSolicitud.sApellidoRef1) +
                                    "</apellido>" +
                                    "<telefonos>" +
                                    addParm("telefono", inOriginacionServicioSolicitud.TelefonoRef1) +
                                    addParm("celular", inOriginacionServicioSolicitud.CelularRef1) +
                                    "</telefonos>" +
                                    addParm("horarioContacto", inOriginacionServicioSolicitud.HoraContactoRef1) +
                                    addParm("parentesco", inOriginacionServicioSolicitud.ParentescoRef1) +
                                "</referencia1>" +
                                "<referencia2>" +
                                    addParm("nombres", inOriginacionServicioSolicitud.nombreRef2) +
                                    "<apellido>" +
                                    addParm("primero", inOriginacionServicioSolicitud.pApellidoRef2) +
                                    addParm("segundo", inOriginacionServicioSolicitud.sApellidoRef2) +
                                    "</apellido>" +
                                    "<telefonos>" +
                                    addParm("telefono", inOriginacionServicioSolicitud.TelefonoRef2) +
                                    addParm("celular", inOriginacionServicioSolicitud.CelularRef2) +
                                    "</telefonos>" +
                                    addParm("horarioContacto", inOriginacionServicioSolicitud.HoraContactoRef2) +
                                    addParm("parentesco", inOriginacionServicioSolicitud.ParentescoRef2) +
                                "</referencia2>" +
                            "</referenciasPersonales>" +*/
                            "<mediosDisposicion>" +
                                "<cuenta>" +
                                    addParm("medio", inOriginacionServicioSolicitud.medioDisp) +
                                    addParm("clabeCuenta", (inOriginacionServicioSolicitud.ClabeDisp.Equals("")) ? inOriginacionServicioSolicitud.RFC : inOriginacionServicioSolicitud.ClabeDisp) +
                                    addParm("institucionBancaria", (inOriginacionServicioSolicitud.NombreBanco.Equals("") ? "PAGO VENTANILLA" : inOriginacionServicioSolicitud.NombreBanco)) +
                                //addParm("cuenta", inOriginacionServicioSolicitud.NumCuentaBanc) +
                                "</cuenta>" +
                            /*"<cuentaAlternativa>" +
                                addParm("medio", inOriginacionServicioSolicitud.medioA) +
                                addParm("clabe", inOriginacionServicioSolicitud.clabeA) +
                                addParm("institucionBancaria", inOriginacionServicioSolicitud.institucionBancariaA) +
                                addParm("cuenta", inOriginacionServicioSolicitud.cuentaA) +
                            "</cuentaAlternativa>" +*/
                            "</mediosDisposicion>" +
                /*"<compraCartera>" +
                    addParm("depositoCliente", inOriginacionServicioSolicitud.depositoCliente) +
                    addParm("diasPagar", inOriginacionServicioSolicitud.DiasPagar) +
                    addParm("fechaContrato", inOriginacionServicioSolicitud.cartera) +
                    addParm("casaFinanciera", inOriginacionServicioSolicitud.cartera) +
                    addParm("montoCapital", inOriginacionServicioSolicitud.cartera) +
                    addParm("montoTotalPagar", inOriginacionServicioSolicitud.cartera) +
                    addParm("descuento", inOriginacionServicioSolicitud.cartera) +
                    addParm("plazo", inOriginacionServicioSolicitud.cartera) +
                    addParm("montoSaldosInsolutos", inOriginacionServicioSolicitud.cartera) +
                    addParm("tasa", inOriginacionServicioSolicitud.cartera) +
                    addParm("fechaVencimientoCarta", inOriginacionServicioSolicitud.cartera) +
                "</compraCartera>" +
                "<beneficiariosSeguro>" +
                    "<beneficiario>" +
                        "<nombre>" +
                        addParm("primero", inOriginacionServicioSolicitud.primerNombreSeguro) +
                        addParm("segundo", inOriginacionServicioSolicitud.segundoNombreSeguro) +
                        "</nombre>" +
                        "<apellido>" +
                        addParm("primero", inOriginacionServicioSolicitud.primerApellidoSeguro) +
                        addParm("segundo", inOriginacionServicioSolicitud.segundoApellidoSeguro) +
                        "</apellido>" +
                        addParm("parentesco", inOriginacionServicioSolicitud.parentescoSeguro) +
                        addParm("porcentaje", inOriginacionServicioSolicitud.porcentaje) +
                        addParm("curp", inOriginacionServicioSolicitud.curpSeguro) +
                    "</beneficiario>" +
                "</beneficiariosSeguro>" +*/
                "<seguroVoluntario>" +
                                    addParm("tieneSeguro", (inOriginacionServicioSolicitud.tiene_seguro == 1) ? "S":"N");
                if (inOriginacionServicioSolicitud.idPoliza != 0)
                {
                    xmlRequest += addParm("tipoPlan", inOriginacionServicioSolicitud.tipoPlan) +
                                    addParm("montoPrima", inOriginacionServicioSolicitud.planValue) +
                                    addParm("estatus", inOriginacionServicioSolicitud.estatus) + "";
                    if (inOriginacionServicioSolicitud.fechaVigencia != null && inOriginacionServicioSolicitud.fechaVigencia != string.Empty)
                    {
                        xmlRequest += addParm("fechaVigencia", inOriginacionServicioSolicitud.fechaVigencia);
                    }

                    xmlRequest += addParm("idPoliza", inOriginacionServicioSolicitud.idPoliza);
                }
                xmlRequest += "</seguroVoluntario>"+
                "</request>" +
                "</sol:creacionActualizacion>" +
                "</soapenv:Body>" +
                "</soapenv:Envelope> ";
                LogHelper.WriteLog("Models", "ManageProfile", "Crea Solicitud", "", xmlRequest, "Busqueda RFC");
                soapEnvelopeXml.LoadXml(xmlRequest);
                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                }

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Fragment;
                TextReader tr = new StringReader(soapResult);
                LogHelper.WriteLog("Models", "ManageProfile", "resultCreateSolicitud", "", soapResult, "");
                var xml = XDocument.Load(tr);
                XmlTextReader tr2 = new XmlTextReader(new StringReader(xml.Root.ToString()));
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(soapResult);
                LogHelper.WriteLog("Models", "ManageProfile", "Respuesta Solicituds Sibel", null, xt.ToString());
                data.errorCode = xt.GetElementsByTagName("codigo").Item(0).InnerText;
                data.errorMessage = xt.GetElementsByTagName("codigoMensaje").Item(0).InnerText;
                if (data.errorCode.Equals("000"))
                {

                    var idSibel = xt.GetElementsByTagName("idSolicitud").Item(0).InnerText;
                    try
                    {
                        dao.updFormularioSibel(idSibel, folder, inOriginacionServicioSolicitud.estado);
                        var cuerpo1 = $@"{inOriginacionServicioSolicitud.asesor} ha iniciado la CAPTURA de una {inOriginacionServicioSolicitud.tipoSolicitud} solicitud de un crédito con el folio: {folder}. Ingrese al sistema para su seguimiento a la solicitud de BAYPORT.";
                        var cuerpo2 = $@"El estatus de este folio en el portal de Agentes es :{inOriginacionServicioSolicitud.estado}.";
                        var body = String.Format("<p>A quien corresponda:</p> <p></p> <p></p> <p></p> <p>{1}</p> <p></p> <p></p> {2}", Environment.NewLine,cuerpo1, cuerpo2);
                        var send = sendMailAuxiliar(dependencia, producto, folder, rootpath, baseurl, sucursal, inOriginacionServicioSolicitud.expediente_completo, body);
                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog("Models", "ManageProfile", "createSolicitud", e, "Envio de email");
                    }
                    LogHelper.WriteLog("Models", "ManageProfile", "Guarda ID SIBEL", null, idSibel + " " + folder);
                }
                else
                {
                    XmlNodeList xmlnode2 = xt.GetElementsByTagName("error");
                    data.errorMessage = xmlnode2[0].ChildNodes.Item(1).InnerText.Trim();
                    data.errorMessage += " - "+xmlnode2[0].ChildNodes.Item(2).InnerText.Trim();
                }
            }
            catch (Exception ex)
            {
                data.errorCode = "-10";
                data.errorMessage = ex.ToString();
                LogHelper.WriteLog("Models", "ManageProfile", "createSolicitud", ex, inOriginacionServicioSolicitud.RFC);
            }
            return data;
        }
        public static HttpWebRequest CreateWebRequest(string service)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings[service]);
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        public string addParm(string field, dynamic value)
        {
            return "<" + field + ">" + value + "</" + field + ">";
        }
        public string documentoOriginacion(double dependencia, double? producto, string folder, double? documento, string rootPath, string baseURL, double file)
        {
            string filevirtual = "";
            string Outpdf = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\DOCUMENTOS_ORIGINACION";
            ParamDocumentos _document = new ParamDocumentos();
            FormularioSolicitudDocs formulario;
            try
            {
                _document = new ManageParams().getIdDocumentos(documento).ListDocumentos[0];
                formulario = new ProfileDAO().solicitud(folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "writePDF", ex, "");
                return "";
            }
            Outpdf += "\\" + folder + "\\" + dependencia;
            Outpdf += (producto != null) ? "\\" + producto : "";
            var carpeta = Outpdf;
            _document.nombre = _document.nombre.ToLower();
            _document.nombre = _document.nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
            Outpdf += "\\" + formulario.RFC + "_" + _document.nombre.Replace(" ", "_") + "_" + folder + ".pdf";
            bool existe = File.Exists(Outpdf);
            if (existe)
            {
                File.Delete(Outpdf);
            }
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);
            FileStream fs = new FileStream(Outpdf, FileMode.Create, FileAccess.Write, FileShare.None);
            var pagina_mayor = 0;
            var dao = new ProfileDAO();
            string[] meses = {"ENERO","FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE"
                        ,"OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            try
            {
                var existeDoc = new ProfileDAO().docExiste(documento, folder);
                
                OutParamConfiguracionDoc configuraciones = new ManageParams().getConfigDocumentos(dependencia, producto, documento);
                
                
                //Document doc = new Document(reader.GetPageSizeWithRotation(1));
                
                var pag_actual = 1;
                PdfReader reader = new PdfReader(_document.path);
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument doc = new PdfDocument(reader, writer);
                PdfPage page;
                PdfCanvas canvas;
                var fecha_solicitud = formulario.fchsolicitud.Split('/');
                var fecha_primer_pago = formulario.fchPrPago.Split('/');
                var fecha_ultimo_pago = formulario.fchUltPago.Split('/');
                var fecha_nacimiento = formulario.fecNac.Split('/');
                formulario.colonia = (formulario.colonia.Equals("OTRA")) ? "" : formulario.colonia;
                formulario.coloniaDom = (formulario.coloniaDom.Equals("OTRA")) ? "" : formulario.coloniaDom;
                int n = -1;
                for (var i = 0; i < configuraciones.ListConfiguracion.Count(); i++)
                {
                    var config = configuraciones.ListConfiguracion.ElementAt(i);
                    if (config.tipo_optencion == "2")
                    {
                        switch (config.valor)
                        {
                            case "NUMERO_FOLDER":
                                config.valor = formulario.folderNumber;
                                break;
                            case "ASESOR":
                                config.valor = formulario.asesor;
                                break;
                            case "FECHA_SOLICITUD":
                                config.valor = formulario.fchsolicitud;
                                break;
                            case "DIA_SOLICITUD":
                                config.valor = fecha_solicitud[0];
                                break;
                            case "MES_SOLICITUD":
                                config.valor = fecha_solicitud[1];
                                break;
                            case "MES_SOLICITUD_LETRAS":
                                n = int.Parse(fecha_solicitud[1]);
                                config.valor = meses[n - 1];
                                break;
                            case "AÑO_SOLICITUD_4_DIGITOS":
                                config.valor = fecha_solicitud[2];
                                break;
                            case "AÑO_SOLICITUD_2_DIGITOS":
                                config.valor = fecha_solicitud[2].Substring(2, 2);
                                break;
                            case "TIPO_SOLICITUD":
                                config.valor = formulario.tipoSolicitud;
                                break;
                            case "MONTO":
                                config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "PERIODO":
                                config.valor = formulario.periodo;
                                break;
                            case "PLAZO":
                                config.valor = formulario.plazo;
                                break;
                            case "LIQUIDO_BASE":
                                config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "NO_PLAZAS":
                                config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                break;
                            case "DEPENDENCIA":
                                config.valor = formulario.Dependencia;
                                break;
                            case "PRODUCTO":
                                config.valor = formulario.producto;
                                break;
                            case "DESTINO_CREDITO":
                                config.valor = formulario.destino;
                                break;
                            case "TIPO_NOMINA":
                                config.valor = formulario.tNomina;
                                break;
                            case "DESCUENTO":
                                config.valor = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "TASA_ANUAL":
                                config.valor = formulario.tAnual;
                                break;
                            case "CAT":
                                config.valor = formulario.cat;
                                break;
                            case "SUCURSAL":
                                config.valor = formulario.sucursal;
                                break;
                            case "QUINCENA_DSCTO":
                                config.valor = formulario.quincenaDscto;
                                break;
                            case "FECHA_PRIMER_PAGO":
                                config.valor = formulario.fchPrPago;
                                break;
                            case "DIA_PRIMER_PAGO":
                                config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                break;
                            case "MES_PRIMER_PAGO":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    config.valor = fecha_primer_pago[1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MES_PRIMER_PAGO_LETRAS":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    n = int.Parse(fecha_primer_pago[1]);
                                    config.valor = meses[n - 1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                break;
                            case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                break;
                            case "FECHA_ULTIMO_PAGO":
                                config.valor = formulario.fchUltPago;
                                break;
                            case "DIA_ULTIMO_PAGO":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                break;
                            case "MES_ULTIMO_PAGO":
                                if (fecha_ultimo_pago.Length == 3)
                                {
                                    config.valor = fecha_ultimo_pago[1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MES_ULTIMO_PAGO_LETRAS":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    n = int.Parse(fecha_ultimo_pago[1]);
                                    config.valor = meses[n - 1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                break;
                            case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                break;
                            case "CAPACIDAD_PAGO":
                                config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MONTO_MAXIMO":
                                config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MONTO_DEUDOR":
                                config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MATRICULA":
                                config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                break;
                            case "NSS":
                                config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                break;
                            case "GRUPO":
                                config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                break;
                            case "CLAVE_TRABAJADOR":
                                config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                break;
                            case "ESPECIFICAR":
                                config.valor = formulario.especificar;
                                break;
                            case "RECA":
                                config.valor = formulario.reca;
                                break;
                            case "RFC":
                                config.valor = formulario.RFC;
                                break;
                            case "NOMBRES":
                                config.valor = formulario.pNombre + " " + formulario.sNombre;
                                break;
                            case "PRIMER_APELLIDO":
                                config.valor = formulario.pApellido;
                                break;
                            case "SEGUNDO_APELLIDO":
                                config.valor = formulario.sApellido;
                                break;
                            case "IDENTIFICACION_OFICIAL":
                                config.valor = formulario.tipoDoc;
                                break;
                            case "FECHA_NACIMIENTO":
                                config.valor = formulario.fecNac;
                                break;
                            case "DIA_NACIMIENTO":
                                config.valor = fecha_nacimiento[0];
                                break;
                            case "MES_NACIMIENTO":
                                config.valor = fecha_nacimiento[1];
                                break;
                            case "MES_NACIMIENTO_LETRAS":
                                n = int.Parse(fecha_solicitud[1]);
                                config.valor = meses[n - 1];
                                break;
                            case "AÑO_NACIMIENTO_2_DIGITOS":
                                config.valor = fecha_nacimiento[2].Substring(2, 2);
                                break;
                            case "AÑO_NACIMIENTO_4_DIGITOS":
                                config.valor = fecha_nacimiento[2];
                                break;
                            case "PAIS_NACIMIENTO":
                                config.valor = formulario.paisN;
                                break;
                            case "ENTIDAD_NACIMIENTO":
                                config.valor = formulario.entidadN;
                                break;
                            case "PAIS_RESIDENCIA":
                                config.valor = formulario.paisR;
                                break;
                            case "FORMA_MIGRATORIA":
                                config.valor = formulario.fMigratoria;
                                break;
                            case "GENERO":
                                config.valor = formulario.gender;
                                break;
                            case "SECTOR":
                                config.valor = formulario.sector;
                                break;
                            case "OTRO_SECTOR":
                                config.valor = formulario.otroSector;
                                break;
                            case "PUESTO":
                                config.valor = formulario.puesto;
                                break;
                            case "ANTIGUEDAD":
                                config.valor = formulario.antiguedad;
                                break;
                            case "INGRESO_MENSUAL":
                                config.valor = formulario.ingresos;
                                break;
                            case "NUMERO_PERSONAL":
                                config.valor = formulario.Celular;
                                break;
                            case "CLAVE_PRESUPUESTAL":
                                config.valor = formulario.cPresupuestal;
                                break;
                            case "PAGADURIA":
                                config.valor = formulario.Pagaduria;
                                break;
                            case "FECHA_INGRESO":
                                config.valor = formulario.fchIngreso;
                                break;
                            case "CLAVE":
                                config.valor = formulario.clave;
                                break;
                            case "LUGAR_TRABAJO":
                                config.valor = formulario.lugTrabajo;
                                break;
                            case "CALLE":
                                config.valor = formulario.calle;
                                break;
                            case "NUMERO_EXTERIOR":
                                config.valor = formulario.nExterior;
                                break;
                            case "COLONIA":
                                config.valor = formulario.colonia;
                                break;
                            case "OTRA_COLONIA":
                                config.valor = formulario.otraColonia;
                                break;
                            case "TELEFONO_FIJO":
                                config.valor = formulario.telFijo;
                                break;
                            case "EXTENSION":
                                config.valor = formulario.extension;
                                break;
                            case "ENTIDAD":
                                config.valor = formulario.entidadT;
                                break;
                            case "MUNICIPIO":
                                config.valor = formulario.municipio;
                                break;
                            case "CODIGO_POSTAL_OCUPACION":
                                config.valor = formulario.CodigoPost;
                                break;
                            case "TIENE_CARGO_PUBLICO":
                                config.valor = formulario.tCargoPu;
                                break;
                            case "PERIODO_DE_EJECUCION":
                                config.valor = formulario.pEjecucion;
                                break;
                            case "CARGO_PUBLICO_FAMILIAR":
                                config.valor = formulario.tCargoPuF;
                                break;
                            case "NOMBRE_FAMILIAR":
                                config.valor = formulario.nombFamiliar;
                                break;
                            case "PUESTO_FAMILIAR":
                                config.valor = formulario.puestoFam;
                                break;
                            case "PERIODO_EJERCICO_FAMILIAR":
                                config.valor = formulario.perEjecucionFam;
                                break;
                            case "BENEFICIARIO":
                                config.valor = formulario.tBeneneficiario;
                                break;
                            case "NOMBRE_BENEFICIARIO":
                                config.valor = formulario.nombBene;
                                break;
                            case "TIPO_PENSION":
                                config.valor = formulario.tipPension;
                                break;
                            case "ADSCRIPCION_PAGO":
                                config.valor = formulario.ubiPago;
                                break;
                            case "DELEGACION":
                                config.valor = formulario.delegacionImss;
                                break;
                            case "NOMBRE_TESTIGO1":
                                config.valor = formulario.nombTest1;
                                break;
                            case "MATRICULA_TESTIGO1":
                                config.valor = formulario.matricula1;
                                break;
                            case "GAFETE_TESTIGO1":
                                config.valor = formulario.gafete1;
                                break;
                            case "NOMBRE_TESTIGO2":
                                config.valor = formulario.nombTest2;
                                break;
                            case "MATRICULA_TESTIGO2":
                                config.valor = formulario.matricula2;
                                break;
                            case "GAFETE_TESTIGO2":
                                config.valor = formulario.gafete1;
                                break;
                            case "CODIGO_POSTAL_DOMICILIO":
                                config.valor = formulario.codPostDom;
                                break;
                            case "TIEMPO_RESIDENCIA":
                                config.valor = formulario.yearResidencia;
                                break;
                            case "ENTIDAD_DOMICILIO":
                                config.valor = formulario.entidadDom;
                                break;
                            case "DELEGACION_DOMICILIO":
                                config.valor = formulario.municipioDom;
                                break;
                            case "COLONIA_DOMICILIO":
                                config.valor = formulario.coloniaDom;
                                break;
                            case "OTRA_COLONIA_DOMICILIO":
                                config.valor = formulario.otraColoniaDom;
                                break;
                            case "DOMICILIO_CALLE":
                                config.valor = formulario.domicilioCalle;
                                break;
                            case "NUMERO_EXTERIOR_DOMICILIO":
                                config.valor = formulario.noExteriorDom;
                                break;
                            case "NUMERO_INTERIOR_DOMICILIO":
                                config.valor = formulario.noInteriorDom;
                                break;
                            case "ENTRE_CALLES_DOMICILIO":
                                config.valor = formulario.entreCalleDom;
                                break;
                            case "EMAIL_CONTACTO":
                                config.valor = formulario.emailContacto;
                                break;
                            case "CELULAR":
                                config.valor = formulario.CelularContacto;
                                break;
                            case "EMPRESA_TELEFONICA":
                                config.valor = formulario.CompanyPhone;
                                break;
                            case "TELEFONO_PROPIO":
                                config.valor = formulario.telefonoPropio;
                                break;
                            case "NOMBRE_REFERENCIA1":
                                config.valor = formulario.nombreRef1;
                                break;
                            case "APELLIDO1_REFERENCIA1":
                                config.valor = formulario.pApellidoRef1;
                                break;
                            case "APELLIDO2_REFERENCIA1":
                                config.valor = formulario.sApellidoRef1;
                                break;
                            case "TELEFONO_REFERENCIA1":
                                config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                break;
                            case "CELULAR_REFERENCIA1":
                                config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                break;
                            case "HORA1_REF1":
                                var hora = Convert.ToDateTime(formulario.Hora1Ref1);
                                config.valor = hora.ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                break;
                            case "HORA2_REF1":
                                config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                break;
                            case "DIA1_REF1":
                                config.valor = formulario.dia1Ref1;
                                break;
                            case "DIA2_REF1":
                                config.valor = formulario.dia2Ref1;
                                break;
                            case "PARENTESCO_REFERENCIA1":
                                config.valor = formulario.ParentescoRef1;
                                break;
                            case "NOMBRE_REFERENCIA2":
                                config.valor = formulario.nombreRef2;
                                break;
                            case "APELLIDO1_REFERENCIA2":
                                config.valor = formulario.pApellidoRef2;
                                break;
                            case "APELLIDO2_REFERENCIA2":
                                config.valor = formulario.sApellidoRef2;
                                break;
                            case "TELEFONO_REFERENCIA2":
                                config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                break;
                            case "CELULAR_REFERENCIA2":
                                config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                break;
                            case "HORA1_REF2":
                                config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                break;
                            case "HORA2_REF2":
                                config.valor = Convert.ToDateTime(formulario.Hora2Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                break;
                            case "DIA1_REF2":
                                config.valor = formulario.dia1Ref2;
                                break;
                            case "DIA2_REF2":
                                config.valor = formulario.dia2Ref2;
                                break;
                            case "PARENTESCO_REFERENCIA2":
                                config.valor = formulario.ParentescoRef2;
                                break;
                            case "MEDIO_DISPOSICION":
                                config.valor = formulario.medioDisp;
                                break;
                            case "CLABE_CUENTA":
                                config.valor = formulario.ClabeDisp;
                                break;
                            case "CLABE_CUENTA DIGITO 1":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 2":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 3":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 4":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 5":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 6":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 7":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 8":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 9":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 10":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 11":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 12":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 13":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 14":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 15":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 16":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 17":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 18":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                break;
                            case "BANCO":
                                config.valor = formulario.NombreBanco;
                                break;
                            case "NUMERO_CUENTA":
                                config.valor = formulario.NumCuentaBanc;
                                break;
                            case "NUMERO_CUENTA DIGITO 1":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 2":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 3":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 4":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 5":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 6":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 7":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 8":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 9":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 10":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 11":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                break;
                            case "MEDIO_DISPOSICION_ALTERNO":
                                config.valor = formulario.medioDispAlt;
                                break;
                            case "CLABE_CUENTA_ALTERNO":
                                config.valor = formulario.ClabeDispAlt;
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(17) + "" : "";
                                break;
                            case "BANCO_ALTERNO":
                                config.valor = formulario.NombreBancoAlt;
                                break;
                            case "NUMERO_CUENTA_ALTERNO":
                                config.valor = formulario.NumCuentaBancAlt;
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                break;
                            case "MEDIO_DISPOSICION_ALTERNO_2":
                                config.valor = formulario.medioDispAlt2;
                                break;
                            case "CLABE_CUENTA_ALTERNO_2":
                                config.valor = formulario.ClabeDispAlt2;
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                break;
                            case "BANCO_ALTERNO_2":
                                config.valor = formulario.NombreBancoAlt2;
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2":
                                config.valor = formulario.NumCuentaBancAlt2;
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                break;
                            case "CURP":
                                config.valor = formulario.CURP;
                                break;
                            case "MONTO_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                break;
                            case "MONTO_ESCRITO_SIN_PESOS":
                                config.valor = dao.monto_escrito(double.Parse(formulario.monto)).Replace("PESOS", "");
                                break;
                            case "LIQUIDO_BASE_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.LBase));
                                break;
                            case "DESCUENTO_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.dscto));
                                break;
                            case "MONTO_MAXIMO_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz));
                                break;
                            case "MONTO_DEUDOR_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor));
                                break;
                            case "NACIONALIDAD":
                                config.valor = formulario.nacionalidad;
                                break;
                            case "EDAD_CLIENTE":
                                DateTime fechaActual = DateTime.Today;
                                var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                config.valor = edad + "";
                                break;
                            case "TASA_MENSUAL":
                                config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                break;
                            case "TOTAL_A_PAGAR_CON_INTERES":
                                config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                config.valor = config.valor = dao.monto_escrito(dto);
                                break;
                            case "NOMBRE_COMPLETO":
                                config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                break;
                        }
                    }
                    if (pagina_mayor < config.pagina)
                    {
                        pagina_mayor = (int)config.pagina;
                    }
                    var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    page = doc.GetPage((int)config.pagina);
                    if ((int)config.pagina != pag_actual)
                    {
                        page = doc.GetPage((int)config.pagina);
                    }
                    pag_actual = (int)config.pagina;
                    if (config.tvalidacion == "S")
                    {
                        switch (config.campoValidar)
                        {
                            case "TIPO_SOLICITUD":
                                if (formulario.tipoSolicitud != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "PERIODO":
                                if (formulario.periodo != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "DEPENDENCIA":
                                if (formulario.Dependencia != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "PRODUCTO":
                                if (formulario.producto != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "DESTINO":
                                if (formulario.destino != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "QUINCENA_DSCTO":
                                if (formulario.quincenaDscto != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "IDENTIFICACION":
                                if (formulario.tipoDoc != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "OTRA_IDENTIFICACION":
                                if (formulario.otraIdentificacion != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "GENERO":
                                if (formulario.gender != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "SECTOR":
                                if (formulario.sector != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "PUESTO":
                                if (formulario.puesto != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "INGRESOS":
                                if (formulario.ingresos != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "CARGO_PUBLICO":
                                if (formulario.tCargoPu != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "CARGO_PUBLICO_FAM":
                                if (formulario.tCargoPuF != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "BENEFICIARIO":
                                if (formulario.tBeneneficiario != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "EMP_TEL":
                                if (formulario.CompanyPhone != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MED_DISP":
                                if (formulario.medioDisp != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MED_DISP_ALT1":
                                if (formulario.medioDispAlt != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MED_DISP_ALT2":
                                if (formulario.medioDispAlt2 != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "BANCO":
                                if (formulario.NombreBanco != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "BANCO_ALT1":
                                if (formulario.NombreBancoAlt != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "BANCO_ALT2":
                                if (formulario.NombreBancoAlt2 != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "RECA":
                                if (formulario.reca != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "ESTADO_CIVIL":
                                if (formulario.estadoCivil != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                            case "NACIONALIDAD":
                                if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                {
                                    if (config.valor_validacion != "MEXICANO")
                                    {
                                        config.valor = "";
                                    }

                                }
                                else
                                {
                                    if (config.valor_validacion != "OTRA")
                                    {
                                        config.valor = "";
                                    }
                                }
                                break;
                            case "PAIS_RESIDENCIA":
                                if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                {
                                    if (config.valor_validacion != "MÉXICO")
                                    {
                                        config.valor = "";
                                    }

                                }
                                else
                                {
                                    if (config.valor_validacion != "OTRA")
                                    {
                                        config.valor = "";
                                    }
                                }
                                break;
                            case "PAIS_NACIMIENTO":
                                if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                {
                                    if (config.valor_validacion != "MÉXICO")
                                    {
                                        config.valor = "";
                                    }

                                }
                                else
                                {
                                    if (config.valor_validacion != "OTRA")
                                    {
                                        config.valor = "";
                                    }
                                }
                                break;
                            case "CLAVE_TRABAJADOR":
                                if (formulario.clave_trabajdor != config.valor_validacion)
                                {
                                    config.valor = "";
                                }
                                break;
                        }
                    }
                    canvas = new PdfCanvas(page);
                    Color myColor = new DeviceRgb(010, 010, 010);
                    canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).SetColor(myColor,true).EndText();
                    canvas.SaveState();
                    canvas.RestoreState();
                }
                if (pagina_mayor <= doc.GetNumberOfPages())
                {
                    pagina_mayor += 1;
                    for (; pagina_mayor <= doc.GetNumberOfPages(); pagina_mayor++)
                    {
                        page = doc.GetPage(pagina_mayor);
                        var paginasTotal = doc.GetNumberOfPages();
                        canvas = new PdfCanvas(page);
                        canvas.SaveState();
                        canvas.RestoreState();
                    }
                }
                doc.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                var docs = new DocumentoOriginacion();
                docs.nombreDoc = _document.nombre.Replace(" ", "_") + ".pdf";
                docs.folder = folder;
                docs.path = Outpdf;
                docs.codigo_doc = documento;
                if (double.Parse(existeDoc[0]) > 0)
                {
                    dao.actualizaDocOriginacion(double.Parse(existeDoc[2]), folder, docs.nombreDoc, docs.path);
                }
                else
                {
                    dao.cargaDocumentosOriginacion(ref docs);
                }
                if (file == 1)
                {
                    var data = DownloadDocFiles(Outpdf, rootPath, baseURL);
                    filevirtual = data.virtualpath;
                }
                
            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "writePDF", ex, "");
                return "";
            }
            return filevirtual;
        }
        public List<string> documentoCartera(double dependencia, double? producto, string folder, double? documento, string rootPath, string baseURL, double file)
        {
            List<string> dataResult = new List<string>();
            List<string> docsExistentes = new List<string>();

            string Outpdf = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\DOCUMENTOS_ORIGINACION";
            OutGetDocument data;
            var dao = new ProfileDAO();
            ParamDocumentos _document = new ParamDocumentos();
            try
            {
                _document = new ManageParams().getIdDocumentos(documento).ListDocumentos[0];
                docsExistentes = dao.documentsCartera(folder, documento);
                var delete = dao.deleteDocCartera(folder,documento);
                if (delete.errorCode == "0")
                {
                    if (docsExistentes.Count() > 0)
                    {
                        for (var t = 0; t < docsExistentes.Count(); t++)
                        {
                            File.Delete(docsExistentes.ElementAt(t));
                        }
                    }
                }
                else
                {
                    return dataResult;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "writePDF", ex, "");
                return dataResult;
            }
            Outpdf += "\\" + folder + "\\" + dependencia;
            Outpdf += (producto != null) ? "\\" + producto : "";
            var carpeta = Outpdf;
            _document.nombre = _document.nombre.ToLower();
            _document.nombre = _document.nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
            Outpdf += "\\" + _document.nombre.Replace(" ", "_") + "_" + folder + ".pdf";
            var config_anterior = "";
            bool existe = File.Exists(Outpdf);
            if (existe)
            {
                File.Delete(Outpdf);
            }
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);
            FileStream fs = new FileStream(Outpdf, FileMode.Create, FileAccess.Write, FileShare.None);
            var pagina_mayor = 0;
            string[] meses = {"ENERO","FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE"
                        ,"OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            try
            {
                var existeDoc = new ProfileDAO().docExiste(documento, folder);
                //Document doc = new Document(reader.GetPageSizeWithRotation(1));
                
                var formulario = new ProfileDAO().solicitud(folder);
                List<List<compra_cartera>> cartera = new List<List<compra_cartera>>();
                List<compra_cartera> carteras = new List<compra_cartera>();
                var casa = "";
                for (var l = 0; l < formulario.cartera.Count(); l++)
                {
                    if (casa != formulario.cartera.ElementAt(l).rfc_casa)
                    {
                        if (carteras.Count > 0)
                        {
                            cartera.Add(carteras);
                            carteras = new List<compra_cartera>();
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        else
                        {
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        casa = formulario.cartera.ElementAt(l).rfc_casa;
                    }
                    else
                    {
                        carteras.Add(formulario.cartera.ElementAt(l));
                    }
                }
                if (carteras.Count > 0)
                {
                    cartera.Add(carteras);
                }
                var pag_actual = 1;
                PdfReader reader = new PdfReader(_document.path);
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument doc = new PdfDocument(reader, writer);
                PdfPage page;
                PdfCanvas canvas;
                var fecha_solicitud = formulario.fchsolicitud.Split('/');
                var fecha_primer_pago = formulario.fchPrPago.Split('/');
                var fecha_ultimo_pago = formulario.fchUltPago.Split('/');
                var fecha_nacimiento = formulario.fecNac.Split('/');
                int n = -1;
                var go = 0;
                var outpdf1 = "";
                var x = 0;
                double item = 0;
                var docs = new DocumentoOriginacion();
                for (var k = 0; k < cartera.Count(); k++)
                {
                    pag_actual = 1;
                    pagina_mayor = 0;
                    item = 0;
                    x = 0;
                    var cart = cartera.ElementAt(k);
                    doc.Close();
                    writer.Close();
                    reader.Close();
                    fs.Close();  
                    if (k == 0)
                    {
                        File.Delete(Outpdf);
                        outpdf1 = Outpdf;
                    }
                    if (k > 0)
                    {
                        dao.cargaDocumentosOriginacion(ref docs);
                        if (file == 1)
                        {
                            data = DownloadDocFiles(Outpdf, rootPath, baseURL);
                            dataResult.Add(data.virtualpath);
                        }
                    }
                    Outpdf = outpdf1.Replace("_" + folder, "_" + folder + "_" + cart.ElementAt(0).rfc_casa);
                    docs.nombreDoc = _document.nombre.Replace(" ", "_")+ folder + "_" + cart.ElementAt(0).rfc_casa + ".pdf";
                    docs.folder = folder;
                    docs.path = Outpdf;
                    docs.codigo_doc = documento;
                    fs = new FileStream(Outpdf, FileMode.Create, FileAccess.Write, FileShare.None);
                    reader = new PdfReader(_document.path);
                    writer = new PdfWriter(fs);
                    doc = new PdfDocument(reader, writer);
                    double sum = 0;
                    Found:
                    if (item > 0)
                    {
                        if (item >= cart.Count())
                        {
                            goto Finish;
                        }
                        go++;
                        x = (int)item;
                        doc.Close();
                        writer.Close();
                        reader.Close();
                        fs.Close();
                        dao.cargaDocumentosOriginacion(ref docs);
                        data = DownloadDocFiles(Outpdf, rootPath, baseURL);
                        dataResult.Add(data.virtualpath);
                        Outpdf = outpdf1.Replace("_" + folder, "_" + folder + "_" + cart.ElementAt(0).rfc_casa + "" + go);
                        docs.nombreDoc = _document.nombre.Replace(" ", "_") + folder + "_" + cart.ElementAt(0).rfc_casa + "" + go + ".pdf";
                        docs.folder = folder;
                        docs.path = Outpdf;
                        fs = new FileStream(Outpdf, FileMode.Create, FileAccess.Write, FileShare.None);
                        reader = new PdfReader(_document.path);
                        writer = new PdfWriter(fs);
                        doc = new PdfDocument(reader, writer);
                    }
                    
                        OutParamConfiguracionDoc configuraciones = new ManageParams().getConfigDocumentos(dependencia, producto, documento);
                        for (var i = 0; i < configuraciones.ListConfiguracion.Count(); i++)
                        {

                            var config = configuraciones.ListConfiguracion.ElementAt(i);
                            config_anterior = config.valor;
                        switch (config.valor)
                        {
                            case "NUMERO_FOLDER":
                                config.valor = formulario.folderNumber;
                                break;
                            case "ASESOR":
                                config.valor = formulario.asesor;
                                break;
                            case "FECHA_SOLICITUD":
                                config.valor = formulario.fchsolicitud;
                                break;
                            case "DIA_SOLICITUD":
                                config.valor = fecha_solicitud[0];
                                break;
                            case "MES_SOLICITUD":
                                config.valor = fecha_solicitud[1];
                                break;
                            case "MES_SOLICITUD_LETRAS":
                                n = int.Parse(fecha_solicitud[1]);
                                config.valor = meses[n - 1];
                                break;
                            case "AÑO_SOLICITUD_4_DIGITOS":
                                config.valor = fecha_solicitud[2];
                                break;
                            case "AÑO_SOLICITUD_2_DIGITOS":
                                config.valor = fecha_solicitud[2].Substring(2, 2);
                                break;
                            case "TIPO_SOLICITUD":
                                config.valor = formulario.tipoSolicitud;
                                break;
                            case "MONTO":
                                config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "PERIODO":
                                config.valor = formulario.periodo;
                                break;
                            case "PLAZO":
                                config.valor = formulario.plazo;
                                break;
                            case "LIQUIDO_BASE":
                                config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "NO_PLAZAS":
                                config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                break;
                            case "DEPENDENCIA":
                                config.valor = formulario.Dependencia;
                                break;
                            case "PRODUCTO":
                                config.valor = formulario.producto;
                                break;
                            case "DESTINO_CREDITO":
                                config.valor = formulario.destino;
                                break;
                            case "TIPO_NOMINA":
                                config.valor = formulario.tNomina;
                                break;
                            case "DESCUENTO":
                                config.valor = formulario.dscto;
                                break;
                            case "TASA_ANUAL":
                                config.valor = formulario.tAnual;
                                break;
                            case "CAT":
                                config.valor = formulario.cat;
                                break;
                            case "SUCURSAL":
                                config.valor = formulario.sucursal;
                                break;
                            case "QUINCENA_DSCTO":
                                config.valor = formulario.quincenaDscto;
                                break;
                            case "FECHA_PRIMER_PAGO":
                                config.valor = formulario.fchPrPago;
                                break;
                            case "DIA_PRIMER_PAGO":
                                config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                break;
                            case "MES_PRIMER_PAGO":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    config.valor = fecha_primer_pago[1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MES_PRIMER_PAGO_LETRAS":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    n = int.Parse(fecha_primer_pago[1]);
                                    config.valor = meses[n - 1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                break;
                            case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                break;
                            case "FECHA_ULTIMO_PAGO":
                                config.valor = formulario.fchUltPago;
                                break;
                            case "DIA_ULTIMO_PAGO":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                break;
                            case "MES_ULTIMO_PAGO":
                                if (fecha_ultimo_pago.Length == 3)
                                {
                                    config.valor = fecha_ultimo_pago[1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "MES_ULTIMO_PAGO_LETRAS":
                                if (fecha_primer_pago.Length == 3)
                                {
                                    n = int.Parse(fecha_ultimo_pago[1]);
                                    config.valor = meses[n - 1];
                                }
                                else
                                {
                                    config.valor = "";
                                }
                                break;
                            case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                break;
                            case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                break;
                            case "CAPACIDAD_PAGO":
                                config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MONTO_MAXIMO":
                                config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MONTO_DEUDOR":
                                config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MATRICULA":
                                config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                break;
                            case "NSS":
                                config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                break;
                            case "GRUPO":
                                config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                break;
                            case "CLAVE_TRABAJADOR":
                                config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                break;
                            case "ESPECIFICAR":
                                config.valor = formulario.especificar;
                                break;
                            case "RECA":
                                config.valor = formulario.reca;
                                break;
                            case "RFC":
                                config.valor = formulario.RFC;
                                break;
                            case "NOMBRES":
                                config.valor = formulario.pNombre + " " + formulario.sNombre;
                                break;
                            case "PRIMER_APELLIDO":
                                config.valor = formulario.pApellido;
                                break;
                            case "SEGUNDO_APELLIDO":
                                config.valor = formulario.sApellido;
                                break;
                            case "IDENTIFICACION_OFICIAL":
                                config.valor = formulario.tipoDoc;
                                break;
                            case "FECHA_NACIMIENTO":
                                config.valor = formulario.fecNac;
                                break;
                            case "DIA_NACIMIENTO":
                                config.valor = fecha_nacimiento[0];
                                break;
                            case "MES_NACIMIENTO":
                                config.valor = fecha_nacimiento[1];
                                break;
                            case "MES_NACIMIENTO_LETRAS":
                                n = int.Parse(fecha_solicitud[1]);
                                config.valor = meses[n - 1];
                                break;
                            case "AÑO_NACIMIENTO_2_DIGITOS":
                                config.valor = fecha_nacimiento[2].Substring(2, 2);
                                break;
                            case "AÑO_NACIMIENTO_4_DIGITOS":
                                config.valor = fecha_nacimiento[2];
                                break;
                            case "PAIS_NACIMIENTO":
                                config.valor = formulario.paisN;
                                break;
                            case "ENTIDAD_NACIMIENTO":
                                config.valor = formulario.entidadN;
                                break;
                            case "PAIS_RESIDENCIA":
                                config.valor = formulario.paisR;
                                break;
                            case "FORMA_MIGRATORIA":
                                config.valor = formulario.fMigratoria;
                                break;
                            case "GENERO":
                                config.valor = formulario.gender;
                                break;
                            case "SECTOR":
                                config.valor = formulario.sector;
                                break;
                            case "OTRO_SECTOR":
                                config.valor = formulario.otroSector;
                                break;
                            case "PUESTO":
                                config.valor = formulario.puesto;
                                break;
                            case "ANTIGUEDAD":
                                config.valor = formulario.antiguedad;
                                break;
                            case "INGRESO_MENSUAL":
                                config.valor = formulario.ingresos;
                                break;
                            case "NUMERO_PERSONAL":
                                config.valor = formulario.Celular;
                                break;
                            case "CLAVE_PRESUPUESTAL":
                                config.valor = formulario.cPresupuestal;
                                break;
                            case "PAGADURIA":
                                config.valor = formulario.Pagaduria;
                                break;
                            case "FECHA_INGRESO":
                                config.valor = formulario.fchIngreso;
                                break;
                            case "CLAVE":
                                config.valor = formulario.clave;
                                break;
                            case "LUGAR_TRABAJO":
                                config.valor = formulario.lugTrabajo;
                                break;
                            case "CALLE":
                                config.valor = formulario.calle;
                                break;
                            case "NUMERO_EXTERIOR":
                                config.valor = formulario.nExterior;
                                break;
                            case "COLONIA":
                                config.valor = formulario.colonia;
                                break;
                            case "OTRA_COLONIA":
                                config.valor = formulario.otraColonia;
                                break;
                            case "TELEFONO_FIJO":
                                config.valor = formulario.telFijo;
                                break;
                            case "EXTENSION":
                                config.valor = formulario.extension;
                                break;
                            case "ENTIDAD":
                                config.valor = formulario.entidadT;
                                break;
                            case "MUNICIPIO":
                                config.valor = formulario.municipio;
                                break;
                            case "CODIGO_POSTAL_OCUPACION":
                                config.valor = formulario.CodigoPost;
                                break;
                            case "TIENE_CARGO_PUBLICO":
                                config.valor = formulario.tCargoPu;
                                break;
                            case "PERIODO_DE_EJECUCION":
                                config.valor = formulario.pEjecucion;
                                break;
                            case "CARGO_PUBLICO_FAMILIAR":
                                config.valor = formulario.tCargoPuF;
                                break;
                            case "NOMBRE_FAMILIAR":
                                config.valor = formulario.nombFamiliar;
                                break;
                            case "PUESTO_FAMILIAR":
                                config.valor = formulario.puestoFam;
                                break;
                            case "PERIODO_EJERCICO_FAMILIAR":
                                config.valor = formulario.perEjecucionFam;
                                break;
                            case "BENEFICIARIO":
                                config.valor = formulario.tBeneneficiario;
                                break;
                            case "NOMBRE_BENEFICIARIO":
                                config.valor = formulario.nombBene;
                                break;
                            case "TIPO_PENSION":
                                config.valor = formulario.tipPension;
                                break;
                            case "ADSCRIPCION_PAGO":
                                config.valor = formulario.ubiPago;
                                break;
                            case "DELEGACION":
                                config.valor = formulario.delegacionImss;
                                break;
                            case "NOMBRE_TESTIGO1":
                                config.valor = formulario.nombTest1;
                                break;
                            case "MATRICULA_TESTIGO1":
                                config.valor = formulario.matricula1;
                                break;
                            case "GAFETE_TESTIGO1":
                                config.valor = formulario.gafete1;
                                break;
                            case "NOMBRE_TESTIGO2":
                                config.valor = formulario.nombTest2;
                                break;
                            case "MATRICULA_TESTIGO2":
                                config.valor = formulario.matricula2;
                                break;
                            case "GAFETE_TESTIGO2":
                                config.valor = formulario.gafete1;
                                break;
                            case "CODIGO_POSTAL_DOMICILIO":
                                config.valor = formulario.codPostDom;
                                break;
                            case "TIEMPO_RESIDENCIA":
                                config.valor = formulario.yearResidencia;
                                break;
                            case "ENTIDAD_DOMICILIO":
                                config.valor = formulario.entidadDom;
                                break;
                            case "DELEGACION_DOMICILIO":
                                config.valor = formulario.municipioDom;
                                break;
                            case "COLONIA_DOMICILIO":
                                config.valor = formulario.coloniaDom;
                                break;
                            case "OTRA_COLONIA_DOMICILIO":
                                config.valor = formulario.otraColoniaDom;
                                break;
                            case "DOMICILIO_CALLE":
                                config.valor = formulario.domicilioCalle;
                                break;
                            case "NUMERO_EXTERIOR_DOMICILIO":
                                config.valor = formulario.noExteriorDom;
                                break;
                            case "NUMERO_INTERIOR_DOMICILIO":
                                config.valor = formulario.noInteriorDom;
                                break;
                            case "ENTRE_CALLES_DOMICILIO":
                                config.valor = formulario.entreCalleDom;
                                break;
                            case "EMAIL_CONTACTO":
                                config.valor = formulario.emailContacto;
                                break;
                            case "CELULAR":
                                config.valor = formulario.CelularContacto;
                                break;
                            case "EMPRESA_TELEFONICA":
                                config.valor = formulario.CompanyPhone;
                                break;
                            case "TELEFONO_PROPIO":
                                config.valor = formulario.telefonoPropio;
                                break;
                            case "NOMBRE_REFERENCIA1":
                                config.valor = formulario.nombreRef1;
                                break;
                            case "APELLIDO1_REFERENCIA1":
                                config.valor = formulario.pApellidoRef1;
                                break;
                            case "APELLIDO2_REFERENCIA1":
                                config.valor = formulario.sApellidoRef1;
                                break;
                            case "TELEFONO_REFERENCIA1":
                                config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                break;
                            case "CELULAR_REFERENCIA1":
                                config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                break;
                            case "HORA1_REF1":
                                config.valor = formulario.Hora1Ref1.ToString().Substring(11, 17);
                                break;
                            case "HORA2_REF1":
                                config.valor = formulario.Hora2Ref1.ToString().Substring(11, 17);
                                break;
                            case "DIA1_REF1":
                                config.valor = formulario.dia1Ref1;
                                break;
                            case "DIA2_REF1":
                                config.valor = formulario.dia2Ref1;
                                break;
                            case "PARENTESCO_REFERENCIA1":
                                config.valor = formulario.ParentescoRef1;
                                break;
                            case "NOMBRE_REFERENCIA2":
                                config.valor = formulario.nombreRef2;
                                break;
                            case "APELLIDO1_REFERENCIA2":
                                config.valor = formulario.pApellidoRef2;
                                break;
                            case "APELLIDO2_REFERENCIA2":
                                config.valor = formulario.sApellidoRef2;
                                break;
                            case "TELEFONO_REFERENCIA2":
                                config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                break;
                            case "CELULAR_REFERENCIA2":
                                config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                break;
                            case "HORA1_REF2":
                                config.valor = formulario.Hora1Ref2.ToString();
                                break;
                            case "HORA2_REF2":
                                config.valor = formulario.Hora2Ref2.ToString();
                                break;
                            case "DIA1_REF2":
                                config.valor = formulario.dia1Ref2;
                                break;
                            case "DIA2_REF2":
                                config.valor = formulario.dia2Ref2;
                                break;
                            case "PARENTESCO_REFERENCIA2":
                                config.valor = formulario.ParentescoRef2;
                                break;
                            case "MEDIO_DISPOSICION":
                                config.valor = formulario.medioDisp;
                                break;
                            case "CLABE_CUENTA":
                                config.valor = formulario.ClabeDisp;
                                break;
                            case "CLABE_CUENTA DIGITO 1":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 2":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 3":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 4":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 5":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 6":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 7":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 8":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 9":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 10":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 11":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 12":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 13":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 14":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 15":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 16":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 17":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA DIGITO 18":
                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                break;
                            case "BANCO":
                                config.valor = formulario.NombreBanco;
                                break;
                            case "NUMERO_CUENTA":
                                config.valor = formulario.NumCuentaBanc;
                                break;
                            case "NUMERO_CUENTA DIGITO 1":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 2":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 3":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 4":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 5":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 6":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 7":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 8":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 9":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 10":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA DIGITO 11":
                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                break;
                            case "MEDIO_DISPOSICION_ALTERNO":
                                config.valor = formulario.medioDispAlt;
                                break;
                            case "CLABE_CUENTA_ALTERNO":
                                config.valor = formulario.ClabeDispAlt;
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                break;
                            case "BANCO_ALTERNO":
                                config.valor = formulario.NombreBancoAlt;
                                break;
                            case "NUMERO_CUENTA_ALTERNO":
                                config.valor = formulario.NumCuentaBancAlt;
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                break;
                            case "MEDIO_DISPOSICION_ALTERNO_2":
                                config.valor = formulario.medioDispAlt2;
                                break;
                            case "CLABE_CUENTA_ALTERNO_2":
                                config.valor = formulario.ClabeDispAlt2;
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                break;
                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                break;
                            case "BANCO_ALTERNO_2":
                                config.valor = formulario.NombreBancoAlt2;
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2":
                                config.valor = formulario.NumCuentaBancAlt2;
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                break;
                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                break;
                            case "CURP":
                                config.valor = formulario.CURP;
                                break;
                            case "MONTO_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                break;
                            case "MONTO_ESCRITO_SIN_PESOS":
                                var monto = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                config.valor = dao.monto_escrito(double.Parse(monto)).Replace("PESOS", "");
                                break;
                            case "CENTAVOS_MONTO_ESCRITO":
                                var arr2 = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                config.valor = arr2[1];
                                break;
                            case "LIQUIDO_BASE_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.LBase)).Replace("PESOS", "");
                                break;
                            case "DESCUENTO_ESCRITO":
                                var dscto = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                config.valor = dao.monto_escrito(double.Parse(dscto)).Replace("PESOS", "");
                                break;
                            case "CENTAVOS_DESCUENTO_ESCRITO":
                                var arr3 = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                config.valor = arr3[1];
                                break;
                            case "MONTO_MAXIMO_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz)).Replace("PESOS", "");
                                break;
                            case "MONTO_DEUDOR_ESCRITO":
                                config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor)).Replace("PESOS", "");
                                break;
                            case "NACIONALIDAD":
                                config.valor = formulario.nacionalidad;
                                break;
                            case "EDAD_CLIENTE":
                                DateTime fechaActual = DateTime.Today;
                                var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                config.valor = edad + "";
                                break;
                            case "TASA_MENSUAL":
                                config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                break;
                            case "TOTAL_A_PAGAR_CON_INTERES":
                                config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                config.valor = config.valor = dao.monto_escrito(dto);
                                break;
                            case "NOMBRE_COMPLETO":
                                config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                break;
                            case "CASA_FINANCIERA":
                                config.valor = cart.ElementAt(x).entidad;
                                break;
                            case "SUMA_SALDO_INSOLUTO":
                                sum = 0;
                                var q = (int)item;
                                var lim = ((int)_document.max_item+q == 0) ? cart.Count() : (int)_document.max_item + q;
                                
                                for (; q < lim; q++)
                                {
                                    if (q >= cart.Count()) break;
                                    sum += cart.ElementAt(q).saldoInsoluto;
                                }
                                config.valor = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "SUMA_SALDO_INSOLUTO_LETRA":
                                sum = 0;
                                var q1 = (int)item;
                                var lim1 = ((int)_document.max_item+q1 == 0) ? cart.Count() : (int)_document.max_item + q1;
                                for (; q1 < lim1; q1++)
                                {
                                    if (q1 == cart.Count()) break;
                                    sum += cart.ElementAt(q1).saldoInsoluto;
                                }
                                sum = double.Parse(sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", ""));
                                config.valor = dao.monto_escrito(sum).Replace("PESOS", "");
                                break;
                            case "CENTAVOS_SUMA_SALDO_INSOLUTO_LETRA":
                                sum = 0;
                                var q2 = (int)item;
                                var lim2 = ((int)_document.max_item + q2 == 0) ? cart.Count() : (int)_document.max_item + q2;
                                for (; q2 < lim2; q2++)
                                {
                                    if (q2 >= cart.Count()) break;
                                    sum += cart.ElementAt(q2).saldoInsoluto;
                                }
                                var arr = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                config.valor = arr[1];
                                break;
                            case "DEPOSITO_CLIENTE":
                                config.valor = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "DEPOSITO_CLIENTE_LETRA":
                                var depositoCliente = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                config.valor = dao.monto_escrito(double.Parse(depositoCliente)).Replace("PESOS", "");
                                break;
                            case "CENTAVOS_DEPOSITO_CLIENTE_LETRA":
                                var arr1 = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                config.valor = arr1[1];
                                break;
                            case "DIAS_A_PAGAR":
                                config.valor = formulario.DiasPagar;
                                break;
                            case "FECHA_CONTRATO_COMPRA":
                                config.valor = cart.ElementAt(x).fecha.Substring(0, 10);
                                break;
                            case "MONTO_CREDITO_COMPRA":
                                config.valor = cart.ElementAt(x).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "MONTO_TOTAL":
                                config.valor = cart.ElementAt(x).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "PLAZO_COMPRA":
                                config.valor = cart.ElementAt(x).plazo + "";
                                break;
                            case "SALDO_INSOLUTO":
                                config.valor = cart.ElementAt(x).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                break;
                            case "TASA_COMPRA":
                                config.valor = cart.ElementAt(x).tasa + "";
                                break;
                        }
                        if (pagina_mayor < config.pagina)
                            {
                                pagina_mayor = (int)config.pagina;
                            }
                            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                            page = doc.GetPage((int)config.pagina);
                            if ((int)config.pagina != pag_actual)
                            {
                                page = doc.GetPage((int)config.pagina);
                            }
                            pag_actual = (int)config.pagina;
                            if (config.tvalidacion == "S")
                            {
                                switch (config.campoValidar)
                                {
                                    case "TIPO_SOLICITUD":
                                        if (formulario.tipoSolicitud != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PERIODO":
                                        if (formulario.periodo != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "DEPENDENCIA":
                                        if (formulario.Dependencia != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PRODUCTO":
                                        if (formulario.producto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "DESTINO":
                                        if (formulario.destino != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "QUINCENA_DSCTO":
                                        if (formulario.quincenaDscto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "IDENTIFICACION":
                                        if (formulario.tipoDoc != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "OTRA_IDENTIFICACION":
                                        if (formulario.otraIdentificacion != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "GENERO":
                                        if (formulario.gender != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "SECTOR":
                                        if (formulario.sector != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PUESTO":
                                        if (formulario.puesto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "INGRESOS":
                                        if (formulario.ingresos != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "CARGO_PUBLICO":
                                        if (formulario.tCargoPu != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "CARGO_PUBLICO_FAM":
                                        if (formulario.tCargoPuF != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BENEFICIARIO":
                                        if (formulario.tBeneneficiario != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "EMP_TEL":
                                        if (formulario.CompanyPhone != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MED_DISP":
                                        if (formulario.medioDisp != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BANCO":
                                        if (formulario.NombreBanco != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "RECA":
                                        if (formulario.reca != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "ESTADO_CIVIL":
                                        if (formulario.estadoCivil != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "NACIONALIDAD":
                                        if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                        {
                                            if (config.valor_validacion != "MEXICANO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                    case "PAIS_RESIDENCIA":
                                        if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                        {
                                            if (config.valor_validacion != "MÉXICO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                    case "PAIS_NACIMIENTO":
                                        if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                        {
                                            if (config.valor_validacion != "MÉXICO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                }
                            }
                            canvas = new PdfCanvas(page);
                            Color myColor = new DeviceRgb(010, 010, 010);
                            canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).SetColor(myColor, true).EndText();
                        if (_document.max_item > 0 && config.aumentoy > 0 && cart.Count() >= _document.max_item)
                        {
                            x++;
                            if (x < cart.Count())
                            {
                                for (var l = 1; l < _document.max_item; l++)
                                {
                                    switch (config_anterior)
                                    {
                                        case "CASA_FINANCIERA":
                                            config.valor = cart.ElementAt(x).entidad;
                                            break;
                                        case "FECHA_CONTRATO_COMPRA":
                                            config.valor = cart.ElementAt(x).fecha.Substring(0, 10);
                                            break;
                                        case "MONTO_CREDITO_COMPRA":
                                            config.valor = cart.ElementAt(x).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_TOTAL":
                                            config.valor = cart.ElementAt(x).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "PLAZO_COMPRA":
                                            config.valor = cart.ElementAt(x).plazo + "";
                                            break;
                                        case "SALDO_INSOLUTO":
                                            config.valor = cart.ElementAt(x).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "TASA_COMPRA":
                                            config.valor = cart.ElementAt(x).tasa + "";
                                            break;
                                    }
                                    config.posicion_y -= config.aumentoy;
                                    canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                    x++;
                                }
                            }
                            x = (int)item;
                            }
                            canvas.SaveState();
                        }
                    if (pagina_mayor <= doc.GetNumberOfPages())
                        {
                            pagina_mayor += 1;
                            for (; pagina_mayor <= doc.GetNumberOfPages(); pagina_mayor++)
                            {
                                page = doc.GetPage(pagina_mayor);
                                var paginasTotal = doc.GetNumberOfPages();
                                canvas = new PdfCanvas(page);
                                canvas.SaveState();
                                canvas.RestoreState();
                            }
                        }
                    //}
                    if (_document.max_item > 0)
                    {
                        item = (item == 0) ? item = (double)_document.max_item : item += (double)_document.max_item;
                        goto Found;
                    }
                    Finish:
                     item = 0;
                }
                
                doc.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                
                /*if (double.Parse(existeDoc[0]) > 0)
                {
                    dao.actualizaDocOriginacion(double.Parse(existeDoc[2]), folder, docs.nombreDoc, docs.path);
                }*/
                /*else
                {
                    dao.cargaDocumentosOriginacion(ref docs);
                }*/
                dao.cargaDocumentosOriginacion(ref docs);
                if (file == 1)
                {
                    data = DownloadDocFiles(Outpdf, rootPath, baseURL);
                    dataResult.Add(data.virtualpath);
                }
            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "writePDF", ex, "");
                return dataResult;
            }
            return dataResult;
        }
        public OutGetDocument DownloadDocFiles(string FileName, string rootPath, string baseUrl)
        {
            OutGetDocument document = new OutGetDocument();
            try
            {
                char[] s = new char[FileName.Length - FileName.LastIndexOf("\\") - 1];
                FileName.CopyTo(FileName.LastIndexOf("\\") + 1, s, 0, FileName.Length - FileName.LastIndexOf("\\") - 1);
                string fileName = new string(s);
                if (System.IO.File.Exists(rootPath + "\\" + fileName))
                {
                    System.IO.File.Delete(rootPath + "\\" + fileName);
                }
                System.IO.File.Copy(FileName, rootPath + "\\" + fileName);
                string filevirtual = baseUrl + "/Files/" + fileName;
                document.filename = fileName;
                document.virtualpath = filevirtual;
            }
            catch (Exception ex)
            {
                throw new Exception("CodumentsController.DownloadDocFiles", ex);
            }
            return document;
        }
        public string pruebaConfiguracion(double dependencia, double? producto, double documento, string rootPath, string baseURL)
        {
            string pdfOut = rootPath+"\\";
            string filevirtual = baseURL + "/Files/";

            ParamDocumentos _document = new ParamDocumentos();
            try
            {
                _document = new ManageParams().getIdDocumentos(documento).ListDocumentos[0];
                _document.nombre = _document.nombre.ToLower();
                _document.nombre = _document.nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "pruebaConfiguracionConsulta", ex, "");
                return "";
            }
            pdfOut += dependencia + "_";
            pdfOut += (producto != null) ? producto + "_" : "";
            pdfOut += _document.nombre.Replace(" ", "_") + "_prueba.pdf";
            bool existe = File.Exists(pdfOut);
            if (existe)
            {
                File.Delete(pdfOut);
            }
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                OutParamConfiguracionDoc configuraciones = new ManageParams().getConfigDocumentos(dependencia, producto, documento);
                var pag_actual = 1;
                PdfReader reader = new PdfReader(_document.path);
                PdfWriter writer = new PdfWriter(fs);
                var doc = new PdfDocument(reader, writer);
                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                var pagina_mayor = 0;
                var totPage = doc.GetNumberOfPages();
                PdfPage page;
                PdfCanvas canvas;
                for (var i = 0; i < configuraciones.ListConfiguracion.Count(); i++)
                {
                    var config = configuraciones.ListConfiguracion.ElementAt(i);
                    page = doc.GetPage((int)config.pagina);
                    if (pagina_mayor < config.pagina)
                    {
                        pagina_mayor = (int)config.pagina;
                    }
                    if ((int)config.pagina != pag_actual)
                    {
                        page = doc.GetPage((int)config.pagina);
                    }
                    pag_actual = (int)config.pagina;
                    canvas = new PdfCanvas(page);
                    canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                    if (_document.compra == 1 && _document.max_item > 0)
                    {
                        if (config.aumentoy > 0)
                        {
                            for (var k = 1; k < _document.max_item; k++)
                            {
                                config.posicion_y -= config.aumentoy;
                                canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();

                            }
                        }
                    }
                    canvas.SaveState();
                }
                if (pagina_mayor <= doc.GetNumberOfPages())
                {
                    pagina_mayor += 1;
                    for (; pagina_mayor <= totPage; pagina_mayor++)
                    {
                        
                        page = doc.GetPage(pagina_mayor);
                        doc.AddPage(page);
                    }
                }
                doc.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                filevirtual += dependencia + "_";
                filevirtual += (producto != null) ? producto + "_" : "";
                filevirtual +=  _document.nombre.Replace(" ", "_") + "_prueba.pdf";

            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "pruebaConfiguracion", ex, "");
                return "";
            }
            return filevirtual;
        }
        public OutDocumentoOriginacion buscarDocumentos(string folder, double codigoDoc)
        {
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.buscarDocumentos(folder, codigoDoc);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "buscarDocumentos", ex, "");
            }
            return data;
        }
        public OutDocumentoOriginacion findExpCompleto(string folder)
        {
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.buscarExpedienteCompleto(folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "findExpCompleto", ex, "");
            }
            return data;
        }
        public Response eliminaDocumentosOriginacion(double doc)
        {
            Response data = new Response();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.eliminaDocumentosOriginacion(doc);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "eliminaDocumentosOriginacion", ex, "");
            }
            return data;
        }
        public bool updDocFirma(double? codigo_doc, double doc, string folder, string urlFirma, string docExist)
        {
            bool write = true;
            Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");
            Match match = regex.Match(docExist);
            var extension = "";
            if (match.Success)
            {
                extension = match.Value;
            }
            var nomDoumento = "";
            nomDoumento = regex.Replace(docExist, "_firma" + extension); 
            var infoDoc = new ParamsDAO().getIdDocumentos(codigo_doc).ListDocumentos[0];
            var msg = new Response();
            regex = new Regex("(.png|.jpeg|.jpg)$");
            match = regex.Match(docExist);
            var width = 575f;
            var height = 822f;
            if (match.Success)
            {
                msg = new ProfileDAO().actualizaDocOriginacion(doc, folder, infoDoc.nombre + "_firma", nomDoumento);
                write = (msg.errorCode.Equals("0")) ? true : false;
                File.Delete(docExist);
                File.Delete(urlFirma);
                return write;
            }
            
            PdfReader reader = new PdfReader(docExist);
            PdfDocument docR = new PdfDocument(reader);
            PdfReader reader1;
            PdfDocument docR1;
            FileStream fs = new FileStream(nomDoumento, FileMode.Create, FileAccess.Write, FileShare.None);
            PdfWriter writer = new PdfWriter(fs);
            PdfDocument pdf = new PdfDocument(writer);
            Document documento = new Document(pdf);
            PdfCanvas canvas;
            try
            {
                PdfPage page;
                for (var i = 1; i <= docR.GetNumberOfPages(); i++)
                {
                    if (i == infoDoc.pagina_firma)
                    {
                        regex = new Regex("(.jpg|.png|.jpeg)$");
                        match = regex.Match(urlFirma);
                        var n = docR.GetNumberOfPages();
                        if (match.Success)
                        {
                            ImageData img = ImageDataFactory.Create(urlFirma);
                            if (img.GetWidth() < width)
                                width = img.GetWidth();
                            if (img.GetHeight() < height)
                                height = img.GetHeight();
                            canvas = new PdfCanvas(pdf.AddNewPage(i, PageSize.A4));
                            PdfLayer pdflayer = new PdfLayer("", pdf);
                            canvas.BeginLayer(pdflayer);
                            canvas.AddImage(img, 15, 15, width, false);
                            canvas.EndLayer();
                            canvas.SaveState();
                            canvas.RestoreState();
                            canvas.SaveState();
                            canvas.RestoreState();
                        }
                        else
                        {
                            reader1 = new PdfReader(urlFirma);
                            docR1 = new PdfDocument(reader1);
                            page = docR1.GetPage(1);
                            pdf.AddPage(i,page.CopyTo(pdf));
                            reader1.Close();
                        }
                    }
                    else
                    {
                        page = docR.GetPage(i);
                        pdf.AddPage(i, page.CopyTo(pdf));
                    }
                }
                documento.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                msg = new ProfileDAO().actualizaDocOriginacion(doc,folder,infoDoc.nombre+"_firma",nomDoumento);
                write = (msg.errorCode.Equals("0")) ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "updDocFirma", ex, "");
                write = false;
                documento.Close();
                writer.Close();
                reader.Close();
                fs.Close();
            }
            finally
            {
                reader.Close();
                File.Delete(docExist);
                File.Delete(urlFirma);
            }
            return write;
        }
        public bool updDocFirmaCompra(double? codigo_doc, double doc, string folder, string urlFirma, string docExist, string nombre_doc_cartera)
        {
            bool write = true;
            Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");
            Match match = regex.Match(docExist);
            var extension = "";
            if (match.Success)
            {
                extension = match.Value;
            }
            var nomDoumento = "";
            nomDoumento = regex.Replace(docExist, "_firma" + extension);
            var infoDoc = new ParamsDAO().getIdDocumentos(codigo_doc).ListDocumentos[0];
            var msg = new Response();
            regex = new Regex("(.png|.jpeg|.jpg)$");
            match = regex.Match(docExist);
            var width = 575f;
            var height = 822f;
            if (match.Success)
            {
                msg = new ProfileDAO().actualizaDocOriginacion(doc, folder, nombre_doc_cartera, nomDoumento);
                write = (msg.errorCode.Equals("0")) ? true : false;
                File.Delete(docExist);
                File.Delete(urlFirma);
                return write;
            }

            PdfReader reader = new PdfReader(docExist);
            PdfDocument docR = new PdfDocument(reader);
            PdfReader reader1;
            PdfDocument docR1;
            FileStream fs = new FileStream(nomDoumento, FileMode.Create, FileAccess.Write, FileShare.None);
            PdfWriter writer = new PdfWriter(fs);
            PdfDocument pdf = new PdfDocument(writer);
            Document documento = new Document(pdf);
            PdfCanvas canvas;
            try
            {
                PdfPage page;
                for (var i = 1; i <= docR.GetNumberOfPages(); i++)
                {
                    if (i == infoDoc.pagina_firma)
                    {
                        regex = new Regex("(.jpg|.png|.jpeg)$");
                        match = regex.Match(urlFirma);
                        var n = docR.GetNumberOfPages();
                        if (match.Success)
                        {
                            ImageData img = ImageDataFactory.Create(urlFirma);
                            if (img.GetWidth() < width)
                                width = img.GetWidth();
                            if (img.GetHeight() < height)
                                height = img.GetHeight();
                            canvas = new PdfCanvas(pdf.AddNewPage(i, PageSize.A4));
                            PdfLayer pdflayer = new PdfLayer("", pdf);
                            canvas.BeginLayer(pdflayer);
                            canvas.AddImage(img, 15, 15, width, false);
                            canvas.EndLayer();
                            canvas.SaveState();
                            canvas.RestoreState();
                            canvas.SaveState();
                            canvas.RestoreState();
                        }
                        else
                        {
                            reader1 = new PdfReader(urlFirma);
                            docR1 = new PdfDocument(reader1);
                            page = docR1.GetPage(1);
                            //pdf.AddNewPage(i, PageSize.A4);
                            pdf.AddPage(i, page.CopyTo(pdf));
                            reader1.Close();
                        }
                    }
                    else
                    {
                        page = docR.GetPage(1);
                        //pdf.AddNewPage(i, PageSize.A4);
                        pdf.AddPage(i, page.CopyTo(pdf));
                    }
                }
                documento.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                msg = new ProfileDAO().actualizaDocOriginacion(doc, folder, nombre_doc_cartera, nomDoumento);
                write = (msg.errorCode.Equals("0")) ? true : false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "updDocFirma", ex, "");
                write = false;
                documento.Close();
                writer.Close();
                reader.Close();
                fs.Close();
            }
            finally
            {
                reader.Close();
                File.Delete(docExist);
                File.Delete(urlFirma);
            }
            return write;
        }
        public OutGetDocument soloFirmas(double dependencia, double? producto, string folder, string rootPath, string baseURL)
        {
            
            OutGetDocument pdf = new OutGetDocument();
            string pdfOut = rootPath;
            string filevirtual = baseURL + "/Files/";
            var dao = new ProfileDAO();
            FormularioSolicitudDocs formulario = new FormularioSolicitudDocs();
            try
            {
                formulario = new ProfileDAO().solicitud(folder);

            }
            catch (Exception ex)
            {
                return pdf;
            }
            var nombrepdf = $@"{formulario.RFC}-{folder}-Documento_solo_firmas_{DateTime.Now.ToString("MM-dd-yyyy")}.pdf";
            pdfOut += $@"\\{nombrepdf}";
            bool existe = File.Exists(pdfOut);
            if (existe)
            {
                File.Delete(pdfOut);
            }
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {  
                var data1 = new ProfileDAO().soloFirmas(dependencia, producto);
                OutDocumentoOriginacion data = dao.soloFirmas(dependencia, producto);               
                PdfReader reader = new PdfReader(data1.ListDocumentos[0].path);
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument doc = new PdfDocument(writer);
                PdfDocument doc1;
                PdfPage page;
                PdfPage page1;
                PdfCanvas canvas;
                var config_anterior = "";
                var h = 0;
                double item = 0;
                double sum = 0;
                string[] meses = {"ENERO","FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE"
                        ,"OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                List<List<compra_cartera>> cartera = new List<List<compra_cartera>>();
                List<compra_cartera> carteras = new List<compra_cartera>();
                var casa = "";
                for (var l = 0; l < formulario.cartera.Count(); l++)
                {
                    if (casa != formulario.cartera.ElementAt(l).rfc_casa)
                    {
                        if (carteras.Count > 0)
                        {
                            cartera.Add(carteras);
                            carteras = new List<compra_cartera>();
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        else
                        {
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        casa = formulario.cartera.ElementAt(l).rfc_casa;
                    }
                    else
                    {
                        carteras.Add(formulario.cartera.ElementAt(l));
                    }
                }
                if (carteras.Count > 0)
                {
                    cartera.Add(carteras);
                }
                var fecha_solicitud = formulario.fchsolicitud.Split('/');
                var fecha_primer_pago = formulario.fchPrPago.Split('/');
                var fecha_ultimo_pago = formulario.fchUltPago.Split('/');
                var fecha_nacimiento = formulario.fecNac.Split('/');
                formulario.colonia = (formulario.colonia.Equals("OTRA")) ? "" : formulario.colonia;
                formulario.coloniaDom = (formulario.coloniaDom.Equals("OTRA")) ? "" : formulario.coloniaDom;
                int n = -1;
                var pageDoc = 1;
                for (var i = 0; i < data.ListDocumentos.Count; i++)
                {
                    if (data.ListDocumentos[i].compra == 0)
                    {
                        if (i > 0)
                        {
                            reader.Close();
                            reader = new PdfReader(data1.ListDocumentos[i].path);
                        }
                        doc1 = new PdfDocument(reader);
                        var k = (int)data1.ListDocumentos[i].pagina_firma;
                        page1 = doc1.GetPage(k);
                        doc.AddPage(page1.CopyTo(doc));
                        OutParamConfiguracionDoc configuraciones = new ParamsDAO().getConfigDocumentosFirma(dependencia, producto, data1.ListDocumentos[i].codigo_doc, k);
                        for (var j = 0; j < configuraciones.ListConfiguracion.Count(); j++)
                        {
                            var config = configuraciones.ListConfiguracion.ElementAt(j);
                            if (config.tipo_optencion == "2")
                            {
                                switch (config.valor)
                                {
                                    case "NUMERO_FOLDER":
                                        config.valor = formulario.folderNumber;
                                        break;
                                    case "ASESOR":
                                        config.valor = formulario.asesor;
                                        break;
                                    case "FECHA_SOLICITUD":
                                        config.valor = formulario.fchsolicitud;
                                        break;
                                    case "DIA_SOLICITUD":
                                        config.valor = fecha_solicitud[0];
                                        break;
                                    case "MES_SOLICITUD":
                                        config.valor = fecha_solicitud[1];
                                        break;
                                    case "MES_SOLICITUD_LETRAS":
                                        n = int.Parse(fecha_solicitud[1]);
                                        config.valor = meses[n - 1];
                                        break;
                                    case "AÑO_SOLICITUD_4_DIGITOS":
                                        config.valor = fecha_solicitud[2];
                                        break;
                                    case "AÑO_SOLICITUD_2_DIGITOS":
                                        config.valor = fecha_solicitud[2].Substring(2, 2);
                                        break;
                                    case "TIPO_SOLICITUD":
                                        config.valor = formulario.tipoSolicitud;
                                        break;
                                    case "MONTO":
                                        config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "PERIODO":
                                        config.valor = formulario.periodo;
                                        break;
                                    case "PLAZO":
                                        config.valor = formulario.plazo;
                                        break;
                                    case "LIQUIDO_BASE":
                                        config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "NO_PLAZAS":
                                        config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                        break;
                                    case "DEPENDENCIA":
                                        config.valor = formulario.Dependencia;
                                        break;
                                    case "PRODUCTO":
                                        config.valor = formulario.producto;
                                        break;
                                    case "DESTINO_CREDITO":
                                        config.valor = formulario.destino;
                                        break;
                                    case "TIPO_NOMINA":
                                        config.valor = formulario.tNomina;
                                        break;
                                    case "DESCUENTO":
                                        config.valor = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "TASA_ANUAL":
                                        config.valor = formulario.tAnual;
                                        break;
                                    case "CAT":
                                        config.valor = formulario.cat;
                                        break;
                                    case "SUCURSAL":
                                        config.valor = formulario.sucursal;
                                        break;
                                    case "QUINCENA_DSCTO":
                                        config.valor = formulario.quincenaDscto;
                                        break;
                                    case "FECHA_PRIMER_PAGO":
                                        config.valor = formulario.fchPrPago;
                                        break;
                                    case "DIA_PRIMER_PAGO":
                                        config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                        break;
                                    case "MES_PRIMER_PAGO":
                                        if (fecha_primer_pago.Length == 3)
                                        {
                                            config.valor = fecha_primer_pago[1];
                                        }
                                        else
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MES_PRIMER_PAGO_LETRAS":
                                        if (fecha_primer_pago.Length == 3)
                                        {
                                            n = int.Parse(fecha_primer_pago[1]);
                                            config.valor = meses[n - 1];
                                        }
                                        else
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                        config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                        break;
                                    case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                        config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                        break;
                                    case "FECHA_ULTIMO_PAGO":
                                        config.valor = formulario.fchUltPago;
                                        break;
                                    case "DIA_ULTIMO_PAGO":
                                        config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                        break;
                                    case "MES_ULTIMO_PAGO":
                                        if (fecha_ultimo_pago.Length == 3)
                                        {
                                            config.valor = fecha_ultimo_pago[1];
                                        }
                                        else
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MES_ULTIMO_PAGO_LETRAS":
                                        if (fecha_primer_pago.Length == 3)
                                        {
                                            n = int.Parse(fecha_ultimo_pago[1]);
                                            config.valor = meses[n - 1];
                                        }
                                        else
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                        config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                        break;
                                    case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                        config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                        break;
                                    case "CAPACIDAD_PAGO":
                                        config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "MONTO_MAXIMO":
                                        config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "MONTO_DEUDOR":
                                        config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "MATRICULA":
                                        config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                        break;
                                    case "NSS":
                                        config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                        break;
                                    case "GRUPO":
                                        config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                        break;
                                    case "CLAVE_TRABAJADOR":
                                        config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                        break;
                                    case "ESPECIFICAR":
                                        config.valor = formulario.especificar;
                                        break;
                                    case "RECA":
                                        config.valor = formulario.reca;
                                        break;
                                    case "RFC":
                                        config.valor = formulario.RFC;
                                        break;
                                    case "NOMBRES":
                                        config.valor = formulario.pNombre + " " + formulario.sNombre;
                                        break;
                                    case "PRIMER_APELLIDO":
                                        config.valor = formulario.pApellido;
                                        break;
                                    case "SEGUNDO_APELLIDO":
                                        config.valor = formulario.sApellido;
                                        break;
                                    case "IDENTIFICACION_OFICIAL":
                                        config.valor = formulario.tipoDoc;
                                        break;
                                    case "FECHA_NACIMIENTO":
                                        config.valor = formulario.fecNac;
                                        break;
                                    case "DIA_NACIMIENTO":
                                        config.valor = fecha_nacimiento[0];
                                        break;
                                    case "MES_NACIMIENTO":
                                        config.valor = fecha_nacimiento[1];
                                        break;
                                    case "MES_NACIMIENTO_LETRAS":
                                        n = int.Parse(fecha_solicitud[1]);
                                        config.valor = meses[n - 1];
                                        break;
                                    case "AÑO_NACIMIENTO_2_DIGITOS":
                                        config.valor = fecha_nacimiento[2].Substring(2, 2);
                                        break;
                                    case "AÑO_NACIMIENTO_4_DIGITOS":
                                        config.valor = fecha_nacimiento[2];
                                        break;
                                    case "PAIS_NACIMIENTO":
                                        config.valor = formulario.paisN;
                                        break;
                                    case "ENTIDAD_NACIMIENTO":
                                        config.valor = formulario.entidadN;
                                        break;
                                    case "PAIS_RESIDENCIA":
                                        config.valor = formulario.paisR;
                                        break;
                                    case "FORMA_MIGRATORIA":
                                        config.valor = formulario.fMigratoria;
                                        break;
                                    case "GENERO":
                                        config.valor = formulario.gender;
                                        break;
                                    case "SECTOR":
                                        config.valor = formulario.sector;
                                        break;
                                    case "OTRO_SECTOR":
                                        config.valor = formulario.otroSector;
                                        break;
                                    case "PUESTO":
                                        config.valor = formulario.puesto;
                                        break;
                                    case "ANTIGUEDAD":
                                        config.valor = formulario.antiguedad;
                                        break;
                                    case "INGRESO_MENSUAL":
                                        config.valor = formulario.ingresos;
                                        break;
                                    case "NUMERO_PERSONAL":
                                        config.valor = formulario.Celular;
                                        break;
                                    case "CLAVE_PRESUPUESTAL":
                                        config.valor = formulario.cPresupuestal;
                                        break;
                                    case "PAGADURIA":
                                        config.valor = formulario.Pagaduria;
                                        break;
                                    case "FECHA_INGRESO":
                                        config.valor = formulario.fchIngreso;
                                        break;
                                    case "CLAVE":
                                        config.valor = formulario.clave;
                                        break;
                                    case "LUGAR_TRABAJO":
                                        config.valor = formulario.lugTrabajo;
                                        break;
                                    case "CALLE":
                                        config.valor = formulario.calle;
                                        break;
                                    case "NUMERO_EXTERIOR":
                                        config.valor = formulario.nExterior;
                                        break;
                                    case "COLONIA":
                                        config.valor = formulario.colonia;
                                        break;
                                    case "OTRA_COLONIA":
                                        config.valor = formulario.otraColonia;
                                        break;
                                    case "TELEFONO_FIJO":
                                        config.valor = formulario.telFijo;
                                        break;
                                    case "EXTENSION":
                                        config.valor = formulario.extension;
                                        break;
                                    case "ENTIDAD":
                                        config.valor = formulario.entidadT;
                                        break;
                                    case "MUNICIPIO":
                                        config.valor = formulario.municipio;
                                        break;
                                    case "CODIGO_POSTAL_OCUPACION":
                                        config.valor = formulario.CodigoPost;
                                        break;
                                    case "TIENE_CARGO_PUBLICO":
                                        config.valor = formulario.tCargoPu;
                                        break;
                                    case "PERIODO_DE_EJECUCION":
                                        config.valor = formulario.pEjecucion;
                                        break;
                                    case "CARGO_PUBLICO_FAMILIAR":
                                        config.valor = formulario.tCargoPuF;
                                        break;
                                    case "NOMBRE_FAMILIAR":
                                        config.valor = formulario.nombFamiliar;
                                        break;
                                    case "PUESTO_FAMILIAR":
                                        config.valor = formulario.puestoFam;
                                        break;
                                    case "PERIODO_EJERCICO_FAMILIAR":
                                        config.valor = formulario.perEjecucionFam;
                                        break;
                                    case "BENEFICIARIO":
                                        config.valor = formulario.tBeneneficiario;
                                        break;
                                    case "NOMBRE_BENEFICIARIO":
                                        config.valor = formulario.nombBene;
                                        break;
                                    case "TIPO_PENSION":
                                        config.valor = formulario.tipPension;
                                        break;
                                    case "ADSCRIPCION_PAGO":
                                        config.valor = formulario.ubiPago;
                                        break;
                                    case "DELEGACION":
                                        config.valor = formulario.delegacionImss;
                                        break;
                                    case "NOMBRE_TESTIGO1":
                                        config.valor = formulario.nombTest1;
                                        break;
                                    case "MATRICULA_TESTIGO1":
                                        config.valor = formulario.matricula1;
                                        break;
                                    case "GAFETE_TESTIGO1":
                                        config.valor = formulario.gafete1;
                                        break;
                                    case "NOMBRE_TESTIGO2":
                                        config.valor = formulario.nombTest2;
                                        break;
                                    case "MATRICULA_TESTIGO2":
                                        config.valor = formulario.matricula2;
                                        break;
                                    case "GAFETE_TESTIGO2":
                                        config.valor = formulario.gafete1;
                                        break;
                                    case "CODIGO_POSTAL_DOMICILIO":
                                        config.valor = formulario.codPostDom;
                                        break;
                                    case "TIEMPO_RESIDENCIA":
                                        config.valor = formulario.yearResidencia;
                                        break;
                                    case "ENTIDAD_DOMICILIO":
                                        config.valor = formulario.entidadDom;
                                        break;
                                    case "DELEGACION_DOMICILIO":
                                        config.valor = formulario.municipioDom;
                                        break;
                                    case "COLONIA_DOMICILIO":
                                        config.valor = formulario.coloniaDom;
                                        break;
                                    case "OTRA_COLONIA_DOMICILIO":
                                        config.valor = formulario.otraColoniaDom;
                                        break;
                                    case "DOMICILIO_CALLE":
                                        config.valor = formulario.domicilioCalle;
                                        break;
                                    case "NUMERO_EXTERIOR_DOMICILIO":
                                        config.valor = formulario.noExteriorDom;
                                        break;
                                    case "NUMERO_INTERIOR_DOMICILIO":
                                        config.valor = formulario.noInteriorDom;
                                        break;
                                    case "ENTRE_CALLES_DOMICILIO":
                                        config.valor = formulario.entreCalleDom;
                                        break;
                                    case "EMAIL_CONTACTO":
                                        config.valor = formulario.emailContacto;
                                        break;
                                    case "CELULAR":
                                        config.valor = formulario.CelularContacto;
                                        break;
                                    case "EMPRESA_TELEFONICA":
                                        config.valor = formulario.CompanyPhone;
                                        break;
                                    case "TELEFONO_PROPIO":
                                        config.valor = formulario.telefonoPropio;
                                        break;
                                    case "NOMBRE_REFERENCIA1":
                                        config.valor = formulario.nombreRef1;
                                        break;
                                    case "APELLIDO1_REFERENCIA1":
                                        config.valor = formulario.pApellidoRef1;
                                        break;
                                    case "APELLIDO2_REFERENCIA1":
                                        config.valor = formulario.sApellidoRef1;
                                        break;
                                    case "TELEFONO_REFERENCIA1":
                                        config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                        break;
                                    case "CELULAR_REFERENCIA1":
                                        config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                        break;
                                    case "HORA1_REF1":
                                        var hora = Convert.ToDateTime(formulario.Hora1Ref1);
                                        config.valor = hora.ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                        break;
                                    case "HORA2_REF1":
                                        config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                        break;
                                    case "DIA1_REF1":
                                        config.valor = formulario.dia1Ref1;
                                        break;
                                    case "DIA2_REF1":
                                        config.valor = formulario.dia2Ref1;
                                        break;
                                    case "PARENTESCO_REFERENCIA1":
                                        config.valor = formulario.ParentescoRef1;
                                        break;
                                    case "NOMBRE_REFERENCIA2":
                                        config.valor = formulario.nombreRef2;
                                        break;
                                    case "APELLIDO1_REFERENCIA2":
                                        config.valor = formulario.pApellidoRef2;
                                        break;
                                    case "APELLIDO2_REFERENCIA2":
                                        config.valor = formulario.sApellidoRef2;
                                        break;
                                    case "TELEFONO_REFERENCIA2":
                                        config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                        break;
                                    case "CELULAR_REFERENCIA2":
                                        config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                        break;
                                    case "HORA1_REF2":
                                        config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                        break;
                                    case "HORA2_REF2":
                                        config.valor = Convert.ToDateTime(formulario.Hora2Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                        break;
                                    case "DIA1_REF2":
                                        config.valor = formulario.dia1Ref2;
                                        break;
                                    case "DIA2_REF2":
                                        config.valor = formulario.dia2Ref2;
                                        break;
                                    case "PARENTESCO_REFERENCIA2":
                                        config.valor = formulario.ParentescoRef2;
                                        break;
                                    case "MEDIO_DISPOSICION":
                                        config.valor = formulario.medioDisp;
                                        break;
                                    case "CLABE_CUENTA":
                                        config.valor = formulario.ClabeDisp;
                                        break;
                                    case "CLABE_CUENTA DIGITO 1":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 2":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 3":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 4":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 5":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 6":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 7":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 8":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 9":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 10":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 11":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 12":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 13":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 14":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 15":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 16":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 17":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                        break;
                                    case "CLABE_CUENTA DIGITO 18":
                                        config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                        break;
                                    case "BANCO":
                                        config.valor = formulario.NombreBanco;
                                        break;
                                    case "NUMERO_CUENTA":
                                        config.valor = formulario.NumCuentaBanc;
                                        break;
                                    case "NUMERO_CUENTA DIGITO 1":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 2":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 3":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 4":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 5":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 6":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 7":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 8":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 9":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 10":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA DIGITO 11":
                                        config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                        break;
                                    case "MEDIO_DISPOSICION_ALTERNO":
                                        config.valor = formulario.medioDispAlt;
                                        break;
                                    case "CLABE_CUENTA_ALTERNO":
                                        config.valor = formulario.ClabeDispAlt;
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(13) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(14) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(15) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(16) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                        config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(17) + "" : "";
                                        break;
                                    case "BANCO_ALTERNO":
                                        config.valor = formulario.NombreBancoAlt;
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO":
                                        config.valor = formulario.NumCuentaBancAlt;
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                        config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                        break;
                                    case "MEDIO_DISPOSICION_ALTERNO_2":
                                        config.valor = formulario.medioDispAlt2;
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2":
                                        config.valor = formulario.ClabeDispAlt2;
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                        break;
                                    case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                        config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                        break;
                                    case "BANCO_ALTERNO_2":
                                        config.valor = formulario.NombreBancoAlt2;
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2":
                                        config.valor = formulario.NumCuentaBancAlt2;
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                        break;
                                    case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                        config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                        break;
                                    case "CURP":
                                        config.valor = formulario.CURP;
                                        break;
                                    case "MONTO_ESCRITO":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                        break;
                                    case "MONTO_ESCRITO_SIN_PESOS":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.monto)).Replace("PESOS", "");
                                        break;
                                    case "LIQUIDO_BASE_ESCRITO":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.LBase));
                                        break;
                                    case "DESCUENTO_ESCRITO":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.dscto));
                                        break;
                                    case "MONTO_MAXIMO_ESCRITO":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz));
                                        break;
                                    case "MONTO_DEUDOR_ESCRITO":
                                        config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor));
                                        break;
                                    case "NACIONALIDAD":
                                        config.valor = formulario.nacionalidad;
                                        break;
                                    case "EDAD_CLIENTE":
                                        DateTime fechaActual = DateTime.Today;
                                        var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                        edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                        config.valor = edad + "";
                                        break;
                                    case "TASA_MENSUAL":
                                        config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                        break;
                                    case "TOTAL_A_PAGAR_CON_INTERES":
                                        config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                        break;
                                    case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                        var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                        config.valor = config.valor = dao.monto_escrito(dto);
                                        break;
                                    case "NOMBRE_COMPLETO":
                                        config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                        break;
                                }

                            }
                            if (config.tvalidacion == "S")
                            {
                                switch (config.campoValidar)
                                {
                                    case "TIPO_SOLICITUD":
                                        if (formulario.tipoSolicitud != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PERIODO":
                                        if (formulario.periodo != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "DEPENDENCIA":
                                        if (formulario.Dependencia != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PRODUCTO":
                                        if (formulario.producto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "DESTINO":
                                        if (formulario.destino != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "QUINCENA_DSCTO":
                                        if (formulario.quincenaDscto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "IDENTIFICACION":
                                        if (formulario.tipoDoc != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "OTRA_IDENTIFICACION":
                                        if (formulario.otraIdentificacion != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "GENERO":
                                        if (formulario.gender != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "SECTOR":
                                        if (formulario.sector != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "PUESTO":
                                        if (formulario.puesto != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "INGRESOS":
                                        if (formulario.ingresos != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "CARGO_PUBLICO":
                                        if (formulario.tCargoPu != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "CARGO_PUBLICO_FAM":
                                        if (formulario.tCargoPuF != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BENEFICIARIO":
                                        if (formulario.tBeneneficiario != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "EMP_TEL":
                                        if (formulario.CompanyPhone != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MED_DISP":
                                        if (formulario.medioDisp != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MED_DISP_ALT1":
                                        if (formulario.medioDispAlt != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "MED_DISP_ALT2":
                                        if (formulario.medioDispAlt2 != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BANCO":
                                        if (formulario.NombreBanco != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BANCO_ALT1":
                                        if (formulario.NombreBancoAlt != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "BANCO_ALT2":
                                        if (formulario.NombreBancoAlt2 != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "RECA":
                                        if (formulario.reca != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "ESTADO_CIVIL":
                                        if (formulario.estadoCivil != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                    case "NACIONALIDAD":
                                        if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                        {
                                            if (config.valor_validacion != "MEXICANO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                    case "PAIS_RESIDENCIA":
                                        if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                        {
                                            if (config.valor_validacion != "MÉXICO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                    case "PAIS_NACIMIENTO":
                                        if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                        {
                                            if (config.valor_validacion != "MÉXICO")
                                            {
                                                config.valor = "";
                                            }

                                        }
                                        else
                                        {
                                            if (config.valor_validacion != "OTRA")
                                            {
                                                config.valor = "";
                                            }
                                        }
                                        break;
                                    case "CLAVE_TRABAJADOR":
                                        if (formulario.clave_trabajdor != config.valor_validacion)
                                        {
                                            config.valor = "";
                                        }
                                        break;
                                }
                            }
                            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                            page = doc.GetPage(pageDoc);
                            //page.SetPageRotationInverseMatrixWritten();
                            canvas = new PdfCanvas(page);
                            Color myColor = new DeviceRgb(000, 000, 000);
                            canvas.BeginText().SetColor(myColor, true).SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                            canvas.SaveState();
                            canvas.RestoreState();
                            //break;
                        }
                        //break;
                        pageDoc++;
                        //break;
                    }
                    else if (data.ListDocumentos[i].compra == 1)
                    {
                        if (i > 0)
                        {
                            reader.Close();
                            reader = new PdfReader(data1.ListDocumentos[i].path);
                        }
                        doc1 = new PdfDocument(reader);
                        for (var m = 0; m < cartera.Count(); m++)
                        {
                            var cart = cartera.ElementAt(m);
                        Found:
                            if (item > 0)
                            {
                                if (item >= cart.Count())
                                {
                                    h = 0;
                                    goto Finish;
                                }
                                h = (int)item;
                            }
                            var k = (int)data1.ListDocumentos[i].pagina_firma;
                            page1 = doc1.GetPage(k);
                            doc.AddPage(page1.CopyTo(doc));
                            OutParamConfiguracionDoc configuraciones = new ParamsDAO().getConfigDocumentosFirma(dependencia, producto, data1.ListDocumentos[i].codigo_doc, k);
                            for (var j = 0; j < configuraciones.ListConfiguracion.Count(); j++)
                            {
                                var config = configuraciones.ListConfiguracion.ElementAt(j);
                                config_anterior = config.valor;
                                if (config.tipo_optencion == "2")
                                {
                                    switch (config.valor)
                                    {
                                        case "NUMERO_FOLDER":
                                            config.valor = formulario.folderNumber;
                                            break;
                                        case "ASESOR":
                                            config.valor = formulario.asesor;
                                            break;
                                        case "FECHA_SOLICITUD":
                                            config.valor = formulario.fchsolicitud;
                                            break;
                                        case "DIA_SOLICITUD":
                                            config.valor = fecha_solicitud[0];
                                            break;
                                        case "MES_SOLICITUD":
                                            config.valor = fecha_solicitud[1];
                                            break;
                                        case "MES_SOLICITUD_LETRAS":
                                            n = int.Parse(fecha_solicitud[1]);
                                            config.valor = meses[n - 1];
                                            break;
                                        case "AÑO_SOLICITUD_4_DIGITOS":
                                            config.valor = fecha_solicitud[2];
                                            break;
                                        case "AÑO_SOLICITUD_2_DIGITOS":
                                            config.valor = fecha_solicitud[2].Substring(2, 2);
                                            break;
                                        case "TIPO_SOLICITUD":
                                            config.valor = formulario.tipoSolicitud;
                                            break;
                                        case "MONTO":
                                            config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "PERIODO":
                                            config.valor = formulario.periodo;
                                            break;
                                        case "PLAZO":
                                            config.valor = formulario.plazo;
                                            break;
                                        case "LIQUIDO_BASE":
                                            config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "NO_PLAZAS":
                                            config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                            break;
                                        case "DEPENDENCIA":
                                            config.valor = formulario.Dependencia;
                                            break;
                                        case "PRODUCTO":
                                            config.valor = formulario.producto;
                                            break;
                                        case "DESTINO_CREDITO":
                                            config.valor = formulario.destino;
                                            break;
                                        case "TIPO_NOMINA":
                                            config.valor = formulario.tNomina;
                                            break;
                                        case "DESCUENTO":
                                            config.valor = formulario.dscto;
                                            break;
                                        case "TASA_ANUAL":
                                            config.valor = formulario.tAnual;
                                            break;
                                        case "CAT":
                                            config.valor = formulario.cat;
                                            break;
                                        case "SUCURSAL":
                                            config.valor = formulario.sucursal;
                                            break;
                                        case "QUINCENA_DSCTO":
                                            config.valor = formulario.quincenaDscto;
                                            break;
                                        case "FECHA_PRIMER_PAGO":
                                            config.valor = formulario.fchPrPago;
                                            break;
                                        case "DIA_PRIMER_PAGO":
                                            config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                            break;
                                        case "MES_PRIMER_PAGO":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                config.valor = fecha_primer_pago[1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MES_PRIMER_PAGO_LETRAS":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                n = int.Parse(fecha_primer_pago[1]);
                                                config.valor = meses[n - 1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                            config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                            break;
                                        case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                            config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                            break;
                                        case "FECHA_ULTIMO_PAGO":
                                            config.valor = formulario.fchUltPago;
                                            break;
                                        case "DIA_ULTIMO_PAGO":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                            break;
                                        case "MES_ULTIMO_PAGO":
                                            if (fecha_ultimo_pago.Length == 3)
                                            {
                                                config.valor = fecha_ultimo_pago[1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MES_ULTIMO_PAGO_LETRAS":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                n = int.Parse(fecha_ultimo_pago[1]);
                                                config.valor = meses[n - 1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                            break;
                                        case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                            break;
                                        case "CAPACIDAD_PAGO":
                                            config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_MAXIMO":
                                            config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_DEUDOR":
                                            config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MATRICULA":
                                            config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                            break;
                                        case "NSS":
                                            config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                            break;
                                        case "GRUPO":
                                            config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                            break;
                                        case "CLAVE_TRABAJADOR":
                                            config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                            break;
                                        case "ESPECIFICAR":
                                            config.valor = formulario.especificar;
                                            break;
                                        case "RECA":
                                            config.valor = formulario.reca;
                                            break;
                                        case "RFC":
                                            config.valor = formulario.RFC;
                                            break;
                                        case "NOMBRES":
                                            config.valor = formulario.pNombre + " " + formulario.sNombre;
                                            break;
                                        case "PRIMER_APELLIDO":
                                            config.valor = formulario.pApellido;
                                            break;
                                        case "SEGUNDO_APELLIDO":
                                            config.valor = formulario.sApellido;
                                            break;
                                        case "IDENTIFICACION_OFICIAL":
                                            config.valor = formulario.tipoDoc;
                                            break;
                                        case "FECHA_NACIMIENTO":
                                            config.valor = formulario.fecNac;
                                            break;
                                        case "DIA_NACIMIENTO":
                                            config.valor = fecha_nacimiento[0];
                                            break;
                                        case "MES_NACIMIENTO":
                                            config.valor = fecha_nacimiento[1];
                                            break;
                                        case "MES_NACIMIENTO_LETRAS":
                                            n = int.Parse(fecha_solicitud[1]);
                                            config.valor = meses[n - 1];
                                            break;
                                        case "AÑO_NACIMIENTO_2_DIGITOS":
                                            config.valor = fecha_nacimiento[2].Substring(2, 2);
                                            break;
                                        case "AÑO_NACIMIENTO_4_DIGITOS":
                                            config.valor = fecha_nacimiento[2];
                                            break;
                                        case "PAIS_NACIMIENTO":
                                            config.valor = formulario.paisN;
                                            break;
                                        case "ENTIDAD_NACIMIENTO":
                                            config.valor = formulario.entidadN;
                                            break;
                                        case "PAIS_RESIDENCIA":
                                            config.valor = formulario.paisR;
                                            break;
                                        case "FORMA_MIGRATORIA":
                                            config.valor = formulario.fMigratoria;
                                            break;
                                        case "GENERO":
                                            config.valor = formulario.gender;
                                            break;
                                        case "SECTOR":
                                            config.valor = formulario.sector;
                                            break;
                                        case "OTRO_SECTOR":
                                            config.valor = formulario.otroSector;
                                            break;
                                        case "PUESTO":
                                            config.valor = formulario.puesto;
                                            break;
                                        case "ANTIGUEDAD":
                                            config.valor = formulario.antiguedad;
                                            break;
                                        case "INGRESO_MENSUAL":
                                            config.valor = formulario.ingresos;
                                            break;
                                        case "NUMERO_PERSONAL":
                                            config.valor = formulario.Celular;
                                            break;
                                        case "CLAVE_PRESUPUESTAL":
                                            config.valor = formulario.cPresupuestal;
                                            break;
                                        case "PAGADURIA":
                                            config.valor = formulario.Pagaduria;
                                            break;
                                        case "FECHA_INGRESO":
                                            config.valor = formulario.fchIngreso;
                                            break;
                                        case "CLAVE":
                                            config.valor = formulario.clave;
                                            break;
                                        case "LUGAR_TRABAJO":
                                            config.valor = formulario.lugTrabajo;
                                            break;
                                        case "CALLE":
                                            config.valor = formulario.calle;
                                            break;
                                        case "NUMERO_EXTERIOR":
                                            config.valor = formulario.nExterior;
                                            break;
                                        case "COLONIA":
                                            config.valor = formulario.colonia;
                                            break;
                                        case "OTRA_COLONIA":
                                            config.valor = formulario.otraColonia;
                                            break;
                                        case "TELEFONO_FIJO":
                                            config.valor = formulario.telFijo;
                                            break;
                                        case "EXTENSION":
                                            config.valor = formulario.extension;
                                            break;
                                        case "ENTIDAD":
                                            config.valor = formulario.entidadT;
                                            break;
                                        case "MUNICIPIO":
                                            config.valor = formulario.municipio;
                                            break;
                                        case "CODIGO_POSTAL_OCUPACION":
                                            config.valor = formulario.CodigoPost;
                                            break;
                                        case "TIENE_CARGO_PUBLICO":
                                            config.valor = formulario.tCargoPu;
                                            break;
                                        case "PERIODO_DE_EJECUCION":
                                            config.valor = formulario.pEjecucion;
                                            break;
                                        case "CARGO_PUBLICO_FAMILIAR":
                                            config.valor = formulario.tCargoPuF;
                                            break;
                                        case "NOMBRE_FAMILIAR":
                                            config.valor = formulario.nombFamiliar;
                                            break;
                                        case "PUESTO_FAMILIAR":
                                            config.valor = formulario.puestoFam;
                                            break;
                                        case "PERIODO_EJERCICO_FAMILIAR":
                                            config.valor = formulario.perEjecucionFam;
                                            break;
                                        case "BENEFICIARIO":
                                            config.valor = formulario.tBeneneficiario;
                                            break;
                                        case "NOMBRE_BENEFICIARIO":
                                            config.valor = formulario.nombBene;
                                            break;
                                        case "TIPO_PENSION":
                                            config.valor = formulario.tipPension;
                                            break;
                                        case "ADSCRIPCION_PAGO":
                                            config.valor = formulario.ubiPago;
                                            break;
                                        case "DELEGACION":
                                            config.valor = formulario.delegacionImss;
                                            break;
                                        case "NOMBRE_TESTIGO1":
                                            config.valor = formulario.nombTest1;
                                            break;
                                        case "MATRICULA_TESTIGO1":
                                            config.valor = formulario.matricula1;
                                            break;
                                        case "GAFETE_TESTIGO1":
                                            config.valor = formulario.gafete1;
                                            break;
                                        case "NOMBRE_TESTIGO2":
                                            config.valor = formulario.nombTest2;
                                            break;
                                        case "MATRICULA_TESTIGO2":
                                            config.valor = formulario.matricula2;
                                            break;
                                        case "GAFETE_TESTIGO2":
                                            config.valor = formulario.gafete1;
                                            break;
                                        case "CODIGO_POSTAL_DOMICILIO":
                                            config.valor = formulario.codPostDom;
                                            break;
                                        case "TIEMPO_RESIDENCIA":
                                            config.valor = formulario.yearResidencia;
                                            break;
                                        case "ENTIDAD_DOMICILIO":
                                            config.valor = formulario.entidadDom;
                                            break;
                                        case "DELEGACION_DOMICILIO":
                                            config.valor = formulario.municipioDom;
                                            break;
                                        case "COLONIA_DOMICILIO":
                                            config.valor = formulario.coloniaDom;
                                            break;
                                        case "OTRA_COLONIA_DOMICILIO":
                                            config.valor = formulario.otraColoniaDom;
                                            break;
                                        case "DOMICILIO_CALLE":
                                            config.valor = formulario.domicilioCalle;
                                            break;
                                        case "NUMERO_EXTERIOR_DOMICILIO":
                                            config.valor = formulario.noExteriorDom;
                                            break;
                                        case "NUMERO_INTERIOR_DOMICILIO":
                                            config.valor = formulario.noInteriorDom;
                                            break;
                                        case "ENTRE_CALLES_DOMICILIO":
                                            config.valor = formulario.entreCalleDom;
                                            break;
                                        case "EMAIL_CONTACTO":
                                            config.valor = formulario.emailContacto;
                                            break;
                                        case "CELULAR":
                                            config.valor = formulario.CelularContacto;
                                            break;
                                        case "EMPRESA_TELEFONICA":
                                            config.valor = formulario.CompanyPhone;
                                            break;
                                        case "TELEFONO_PROPIO":
                                            config.valor = formulario.telefonoPropio;
                                            break;
                                        case "NOMBRE_REFERENCIA1":
                                            config.valor = formulario.nombreRef1;
                                            break;
                                        case "APELLIDO1_REFERENCIA1":
                                            config.valor = formulario.pApellidoRef1;
                                            break;
                                        case "APELLIDO2_REFERENCIA1":
                                            config.valor = formulario.sApellidoRef1;
                                            break;
                                        case "TELEFONO_REFERENCIA1":
                                            config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                            break;
                                        case "CELULAR_REFERENCIA1":
                                            config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                            break;
                                        case "HORA1_REF1":
                                            config.valor = formulario.Hora1Ref1.ToString().Substring(11, 17);
                                            break;
                                        case "HORA2_REF1":
                                            config.valor = formulario.Hora2Ref1.ToString().Substring(11, 17);
                                            break;
                                        case "DIA1_REF1":
                                            config.valor = formulario.dia1Ref1;
                                            break;
                                        case "DIA2_REF1":
                                            config.valor = formulario.dia2Ref1;
                                            break;
                                        case "PARENTESCO_REFERENCIA1":
                                            config.valor = formulario.ParentescoRef1;
                                            break;
                                        case "NOMBRE_REFERENCIA2":
                                            config.valor = formulario.nombreRef2;
                                            break;
                                        case "APELLIDO1_REFERENCIA2":
                                            config.valor = formulario.pApellidoRef2;
                                            break;
                                        case "APELLIDO2_REFERENCIA2":
                                            config.valor = formulario.sApellidoRef2;
                                            break;
                                        case "TELEFONO_REFERENCIA2":
                                            config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                            break;
                                        case "CELULAR_REFERENCIA2":
                                            config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                            break;
                                        case "HORA1_REF2":
                                            config.valor = formulario.Hora1Ref2.ToString();
                                            break;
                                        case "HORA2_REF2":
                                            config.valor = formulario.Hora2Ref2.ToString();
                                            break;
                                        case "DIA1_REF2":
                                            config.valor = formulario.dia1Ref2;
                                            break;
                                        case "DIA2_REF2":
                                            config.valor = formulario.dia2Ref2;
                                            break;
                                        case "PARENTESCO_REFERENCIA2":
                                            config.valor = formulario.ParentescoRef2;
                                            break;
                                        case "MEDIO_DISPOSICION":
                                            config.valor = formulario.medioDisp;
                                            break;
                                        case "CLABE_CUENTA":
                                            config.valor = formulario.ClabeDisp;
                                            break;
                                        case "CLABE_CUENTA DIGITO 1":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 2":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 3":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 4":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 5":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 6":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 7":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 8":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 9":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 10":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 11":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 12":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 13":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 14":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 15":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 16":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 17":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 18":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO":
                                            config.valor = formulario.NombreBanco;
                                            break;
                                        case "NUMERO_CUENTA":
                                            config.valor = formulario.NumCuentaBanc;
                                            break;
                                        case "NUMERO_CUENTA DIGITO 1":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 2":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 3":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 4":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 5":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 6":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 7":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 8":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 9":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 10":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 11":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                            break;
                                        case "MEDIO_DISPOSICION_ALTERNO":
                                            config.valor = formulario.medioDispAlt;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO":
                                            config.valor = formulario.ClabeDispAlt;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO_ALTERNO":
                                            config.valor = formulario.NombreBancoAlt;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO":
                                            config.valor = formulario.NumCuentaBancAlt;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                            break;
                                        case "MEDIO_DISPOSICION_ALTERNO_2":
                                            config.valor = formulario.medioDispAlt2;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2":
                                            config.valor = formulario.ClabeDispAlt2;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO_ALTERNO_2":
                                            config.valor = formulario.NombreBancoAlt2;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2":
                                            config.valor = formulario.NumCuentaBancAlt2;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                            break;
                                        case "CURP":
                                            config.valor = formulario.CURP;
                                            break;
                                        case "MONTO_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                            break;
                                        case "MONTO_ESCRITO_SIN_PESOS":
                                            var monto = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                            config.valor = dao.monto_escrito(double.Parse(monto)).Replace("PESOS", "");
                                            break;
                                        case "CENTAVOS_MONTO_ESCRITO":
                                            var arr2 = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                            config.valor = arr2[1];
                                            break;
                                        case "LIQUIDO_BASE_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.LBase)).Replace("PESOS", "");
                                            break;
                                        case "DESCUENTO_ESCRITO":
                                            var dscto = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                            config.valor = dao.monto_escrito(double.Parse(dscto)).Replace("PESOS", "");
                                            break;
                                        case "CENTAVOS_DESCUENTO_ESCRITO":
                                            var arr3 = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                            config.valor = arr3[1];
                                            break;
                                        case "MONTO_MAXIMO_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz)).Replace("PESOS", "");
                                            break;
                                        case "MONTO_DEUDOR_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor)).Replace("PESOS", "");
                                            break;
                                        case "NACIONALIDAD":
                                            config.valor = formulario.nacionalidad;
                                            break;
                                        case "EDAD_CLIENTE":
                                            DateTime fechaActual = DateTime.Today;
                                            var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                            edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                            config.valor = edad + "";
                                            break;
                                        case "TASA_MENSUAL":
                                            config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                            break;
                                        case "TOTAL_A_PAGAR_CON_INTERES":
                                            config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                            var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                            config.valor = config.valor = dao.monto_escrito(dto);
                                            break;
                                        case "NOMBRE_COMPLETO":
                                            config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                            break;
                                        case "CASA_FINANCIERA":
                                            config.valor = cart.ElementAt(h).entidad;
                                            break;
                                        case "SUMA_SALDO_INSOLUTO":
                                            sum = 0;
                                            var q = (int)item;
                                            var lim = ((int)data1.ListDocumentos[i].max_item + q == 0) ? cart.Count() : (int)data1.ListDocumentos[i].max_item + q;

                                            for (; q < lim; q++)
                                            {
                                                if (q >= cart.Count()) break;
                                                sum += cart.ElementAt(q).saldoInsoluto;
                                            }
                                            config.valor = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "SUMA_SALDO_INSOLUTO_LETRA":
                                            sum = 0;
                                            var q1 = (int)item;
                                            var lim1 = ((int)data1.ListDocumentos[i].max_item + q1 == 0) ? cart.Count() : (int)data1.ListDocumentos[i].max_item + q1;
                                            for (; q1 < lim1; q1++)
                                            {
                                                if (q1 == cart.Count()) break;
                                                sum += cart.ElementAt(q1).saldoInsoluto;
                                            }
                                            sum = double.Parse(sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", ""));
                                            config.valor = dao.monto_escrito(sum).Replace("PESOS", "");
                                            break;
                                        case "CENTAVOS_SUMA_SALDO_INSOLUTO_LETRA":
                                            sum = 0;
                                            var q2 = (int)item;
                                            var lim2 = ((int)data1.ListDocumentos[i].max_item + q2 == 0) ? cart.Count() : (int)data1.ListDocumentos[i].max_item + q2;
                                            for (; q2 < lim2; q2++)
                                            {
                                                if (q2 >= cart.Count()) break;
                                                sum += cart.ElementAt(q2).saldoInsoluto;
                                            }
                                            var arr = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                            config.valor = arr[1];
                                            break;
                                        case "DEPOSITO_CLIENTE":
                                            config.valor = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "DEPOSITO_CLIENTE_LETRA":
                                            var depositoCliente = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                            config.valor = dao.monto_escrito(double.Parse(depositoCliente)).Replace("PESOS", "");
                                            break;
                                        case "CENTAVOS_DEPOSITO_CLIENTE_LETRA":
                                            var arr1 = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                            config.valor = arr1[1];
                                            break;
                                        case "DIAS_A_PAGAR":
                                            config.valor = formulario.DiasPagar;
                                            break;
                                        case "FECHA_CONTRATO_COMPRA":
                                            config.valor = cart.ElementAt(h).fecha.Substring(0, 10);
                                            break;
                                        case "MONTO_CREDITO_COMPRA":
                                            config.valor = cart.ElementAt(h).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_TOTAL":
                                            config.valor = cart.ElementAt(h).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "PLAZO_COMPRA":
                                            config.valor = cart.ElementAt(h).plazo + "";
                                            break;
                                        case "SALDO_INSOLUTO":
                                            config.valor = cart.ElementAt(h).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "TASA_COMPRA":
                                            config.valor = cart.ElementAt(h).tasa + "";
                                            break;
                                    }
                                }
                                if (config.tvalidacion == "S")
                                {
                                    switch (config.campoValidar)
                                    {
                                        case "TIPO_SOLICITUD":
                                            if (formulario.tipoSolicitud != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PERIODO":
                                            if (formulario.periodo != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "DEPENDENCIA":
                                            if (formulario.Dependencia != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PRODUCTO":
                                            if (formulario.producto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "DESTINO":
                                            if (formulario.destino != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "QUINCENA_DSCTO":
                                            if (formulario.quincenaDscto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "IDENTIFICACION":
                                            if (formulario.tipoDoc != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "OTRA_IDENTIFICACION":
                                            if (formulario.otraIdentificacion != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "GENERO":
                                            if (formulario.gender != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "SECTOR":
                                            if (formulario.sector != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PUESTO":
                                            if (formulario.puesto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "INGRESOS":
                                            if (formulario.ingresos != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "CARGO_PUBLICO":
                                            if (formulario.tCargoPu != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "CARGO_PUBLICO_FAM":
                                            if (formulario.tCargoPuF != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BENEFICIARIO":
                                            if (formulario.tBeneneficiario != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "EMP_TEL":
                                            if (formulario.CompanyPhone != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP":
                                            if (formulario.medioDisp != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP_ALT1":
                                            if (formulario.medioDispAlt != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP_ALT2":
                                            if (formulario.medioDispAlt2 != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO":
                                            if (formulario.NombreBanco != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO_ALT1":
                                            if (formulario.NombreBancoAlt != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO_ALT2":
                                            if (formulario.NombreBancoAlt2 != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "RECA":
                                            if (formulario.reca != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "ESTADO_CIVIL":
                                            if (formulario.estadoCivil != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "NACIONALIDAD":
                                            if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                            {
                                                if (config.valor_validacion != "MEXICANO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "PAIS_RESIDENCIA":
                                            if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                            {
                                                if (config.valor_validacion != "MÉXICO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "PAIS_NACIMIENTO":
                                            if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                            {
                                                if (config.valor_validacion != "MÉXICO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "CLAVE_TRABAJADOR":
                                            if (formulario.clave_trabajdor != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                    }
                                }
                                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                                page = doc.GetPage(pageDoc);
                                canvas = new PdfCanvas(page);
                                Color myColor = new DeviceRgb(000, 000, 000);
                                canvas.BeginText().SetColor(myColor, true).SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                if (data1.ListDocumentos[i].max_item > 0 && config.aumentoy > 0 && cart.Count() >= data1.ListDocumentos[i].max_item)
                                {
                                    var limite = data1.ListDocumentos[i].max_item + h;
                                    h++;
                                    for (var l = 1; l < limite; l++)
                                    {
                                        if (h < cart.Count())
                                        {
                                            switch (config_anterior)
                                            {
                                                case "CASA_FINANCIERA":
                                                    config.valor = cart.ElementAt(l).entidad;
                                                    break;
                                                case "FECHA_CONTRATO_COMPRA":
                                                    config.valor = cart.ElementAt(l).fecha.Substring(0, 10);
                                                    break;
                                                case "MONTO_CREDITO_COMPRA":
                                                    config.valor = cart.ElementAt(l).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                    break;
                                                case "MONTO_TOTAL":
                                                    config.valor = cart.ElementAt(l).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                    break;
                                                case "PLAZO_COMPRA":
                                                    config.valor = cart.ElementAt(l).plazo + "";
                                                    break;
                                                case "SALDO_INSOLUTO":
                                                    config.valor = cart.ElementAt(l).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                    break;
                                                case "TASA_COMPRA":
                                                    config.valor = cart.ElementAt(l).tasa + "";
                                                    break;
                                            }
                                            config.posicion_y -= config.aumentoy;
                                            canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                            h++;
                                        }
                                    }
                                    h = (int)item;
                                }
                                canvas.SaveState();
                                canvas.RestoreState();
                            }
                            pageDoc++;
                            if (data1.ListDocumentos[i].max_item > 0)
                            {
                                item = (item == 0) ? item = (double)data1.ListDocumentos[i].max_item : item += (double)data1.ListDocumentos[i].max_item;
                                goto Found;
                            }
                        Finish:
                            item = 0;
                        }
                    }

                }
                doc.Close();
                writer.Close();
                reader.Close();
                fs.Close();
                filevirtual += nombrepdf;
                pdf.filename = pdfOut;
                pdf.virtualpath = filevirtual;
            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "soloFirmas", ex, "");
            }
            return pdf;
        }
        public OutGetDocument Imprimir(double dependencia, double? producto, string folder, string rootPath, string baseURL)
        {
            OutGetDocument pdf = new OutGetDocument();
            string pdfOut = rootPath;
            string filevirtual = baseURL + "/Files/";
            var dao = new ProfileDAO();
            FormularioSolicitudDocs formulario = new FormularioSolicitudDocs();
            try
            {
                formulario = new ProfileDAO().solicitud(folder);

            }
            catch (Exception ex)
            {
                return pdf;
            }
            var nombrepdf = $@"{formulario.RFC}-{folder}-impresion_{DateTime.Now.ToString("MM-dd-yyyy")}.pdf";
            pdfOut += $@"\\{nombrepdf}";
            bool existe = File.Exists(pdfOut);
            if (existe)
            {
                File.Delete(pdfOut);
            }
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                var data1 = new ProfileDAO().allDocumentsImpresion(dependencia, producto);               
                PdfReader reader = new PdfReader(data1.ListDocumentos[0].path);
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument doc = new PdfDocument(writer);
                PdfDocument doc1;
                Document pdf1;
                PdfPage page;
                PdfPage page1;
                PdfCanvas canvas;
                var config_anterior = "";
                var h = 0;
                double item = 0;
                string[] meses = {"ENERO","FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE"
                        ,"OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
                List<List<compra_cartera>> cartera = new List<List<compra_cartera>>();
                List<compra_cartera> carteras = new List<compra_cartera>();
                var casa = "";
                for (var l = 0; l < formulario.cartera.Count(); l++)
                {
                    if (casa != formulario.cartera.ElementAt(l).rfc_casa)
                    {
                        if (carteras.Count > 0)
                        {
                            cartera.Add(carteras);
                            carteras = new List<compra_cartera>();
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        else
                        {
                            carteras.Add(formulario.cartera.ElementAt(l));
                        }
                        casa = formulario.cartera.ElementAt(l).rfc_casa;
                    }
                    else
                    {
                        carteras.Add(formulario.cartera.ElementAt(l));
                    }
                }
                if (carteras.Count > 0)
                {
                    cartera.Add(carteras);
                }
                var fecha_solicitud = formulario.fchsolicitud.Split('/');
                var fecha_primer_pago = formulario.fchPrPago.Split('/');
                var fecha_ultimo_pago = formulario.fchUltPago.Split('/');
                var fecha_nacimiento = formulario.fecNac.Split('/');
                formulario.colonia = (formulario.colonia.Equals("OTRA")) ? "" : formulario.colonia;
                formulario.coloniaDom = (formulario.coloniaDom.Equals("OTRA")) ? "" : formulario.coloniaDom;
                int n = -1;
                var pageDoc = 1;
                var pagina = 0;
                double sum = 0;
                for (var i = 0; i < data1.ListDocumentos.Count; i++)
                {
                    if (data1.ListDocumentos[i].llena_auto == 1 && data1.ListDocumentos[i].compra == 0)
                    {
                        if (i > 0)
                        {
                            reader.Close();
                            reader = new PdfReader(data1.ListDocumentos[i].path);
                        }
                        doc1 = new PdfDocument(reader);
                        for (var k = 1; k <= doc1.GetNumberOfPages(); k++)
                        {
                            page1 = doc1.GetPage(k);
                            doc.AddPage(page1.CopyTo(doc));
                            OutParamConfiguracionDoc configuraciones = new ParamsDAO().getConfigDocumentosFirma(dependencia, producto, data1.ListDocumentos[i].codigo_doc, k);
                            for (var j = 0; j < configuraciones.ListConfiguracion.Count(); j++)
                            {
                                var config = configuraciones.ListConfiguracion.ElementAt(j);
                                if (config.tipo_optencion == "2")
                                {
                                    switch (config.valor)
                                    {
                                        case "NUMERO_FOLDER":
                                            config.valor = formulario.folderNumber;
                                            break;
                                        case "ASESOR":
                                            config.valor = formulario.asesor;
                                            break;
                                        case "FECHA_SOLICITUD":
                                            config.valor = formulario.fchsolicitud;
                                            break;
                                        case "DIA_SOLICITUD":
                                            config.valor = fecha_solicitud[0];
                                            break;
                                        case "MES_SOLICITUD":
                                            config.valor = fecha_solicitud[1];
                                            break;
                                        case "MES_SOLICITUD_LETRAS":
                                            n = int.Parse(fecha_solicitud[1]);
                                            config.valor = meses[n - 1];
                                            break;
                                        case "AÑO_SOLICITUD_4_DIGITOS":
                                            config.valor = fecha_solicitud[2];
                                            break;
                                        case "AÑO_SOLICITUD_2_DIGITOS":
                                            config.valor = fecha_solicitud[2].Substring(2, 2);
                                            break;
                                        case "TIPO_SOLICITUD":
                                            config.valor = formulario.tipoSolicitud;
                                            break;
                                        case "MONTO":
                                            config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "PERIODO":
                                            config.valor = formulario.periodo;
                                            break;
                                        case "PLAZO":
                                            config.valor = formulario.plazo;
                                            break;
                                        case "LIQUIDO_BASE":
                                            config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "NO_PLAZAS":
                                            config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                            break;
                                        case "DEPENDENCIA":
                                            config.valor = formulario.Dependencia;
                                            break;
                                        case "PRODUCTO":
                                            config.valor = formulario.producto;
                                            break;
                                        case "DESTINO_CREDITO":
                                            config.valor = formulario.destino;
                                            break;
                                        case "TIPO_NOMINA":
                                            config.valor = formulario.tNomina;
                                            break;
                                        case "DESCUENTO":
                                            config.valor = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "TASA_ANUAL":
                                            config.valor = formulario.tAnual;
                                            break;
                                        case "CAT":
                                            config.valor = formulario.cat;
                                            break;
                                        case "SUCURSAL":
                                            config.valor = formulario.sucursal;
                                            break;
                                        case "QUINCENA_DSCTO":
                                            config.valor = formulario.quincenaDscto;
                                            break;
                                        case "FECHA_PRIMER_PAGO":
                                            config.valor = formulario.fchPrPago;
                                            break;
                                        case "DIA_PRIMER_PAGO":
                                            config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                            break;
                                        case "MES_PRIMER_PAGO":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                config.valor = fecha_primer_pago[1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MES_PRIMER_PAGO_LETRAS":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                n = int.Parse(fecha_primer_pago[1]);
                                                config.valor = meses[n - 1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                            config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                            break;
                                        case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                            config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                            break;
                                        case "FECHA_ULTIMO_PAGO":
                                            config.valor = formulario.fchUltPago;
                                            break;
                                        case "DIA_ULTIMO_PAGO":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                            break;
                                        case "MES_ULTIMO_PAGO":
                                            if (fecha_ultimo_pago.Length == 3)
                                            {
                                                config.valor = fecha_ultimo_pago[1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MES_ULTIMO_PAGO_LETRAS":
                                            if (fecha_primer_pago.Length == 3)
                                            {
                                                n = int.Parse(fecha_ultimo_pago[1]);
                                                config.valor = meses[n - 1];
                                            }
                                            else
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                            break;
                                        case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                            config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                            break;
                                        case "CAPACIDAD_PAGO":
                                            config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_MAXIMO":
                                            config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MONTO_DEUDOR":
                                            config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "MATRICULA":
                                            config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                            break;
                                        case "NSS":
                                            config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                            break;
                                        case "GRUPO":
                                            config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                            break;
                                        case "CLAVE_TRABAJADOR":
                                            config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                            break;
                                        case "ESPECIFICAR":
                                            config.valor = formulario.especificar;
                                            break;
                                        case "RECA":
                                            config.valor = formulario.reca;
                                            break;
                                        case "RFC":
                                            config.valor = formulario.RFC;
                                            break;
                                        case "NOMBRES":
                                            config.valor = formulario.pNombre + " " + formulario.sNombre;
                                            break;
                                        case "PRIMER_APELLIDO":
                                            config.valor = formulario.pApellido;
                                            break;
                                        case "SEGUNDO_APELLIDO":
                                            config.valor = formulario.sApellido;
                                            break;
                                        case "IDENTIFICACION_OFICIAL":
                                            config.valor = formulario.tipoDoc;
                                            break;
                                        case "FECHA_NACIMIENTO":
                                            config.valor = formulario.fecNac;
                                            break;
                                        case "DIA_NACIMIENTO":
                                            config.valor = fecha_nacimiento[0];
                                            break;
                                        case "MES_NACIMIENTO":
                                            config.valor = fecha_nacimiento[1];
                                            break;
                                        case "MES_NACIMIENTO_LETRAS":
                                            n = int.Parse(fecha_solicitud[1]);
                                            config.valor = meses[n - 1];
                                            break;
                                        case "AÑO_NACIMIENTO_2_DIGITOS":
                                            config.valor = fecha_nacimiento[2].Substring(2, 2);
                                            break;
                                        case "AÑO_NACIMIENTO_4_DIGITOS":
                                            config.valor = fecha_nacimiento[2];
                                            break;
                                        case "PAIS_NACIMIENTO":
                                            config.valor = formulario.paisN;
                                            break;
                                        case "ENTIDAD_NACIMIENTO":
                                            config.valor = formulario.entidadN;
                                            break;
                                        case "PAIS_RESIDENCIA":
                                            config.valor = formulario.paisR;
                                            break;
                                        case "FORMA_MIGRATORIA":
                                            config.valor = formulario.fMigratoria;
                                            break;
                                        case "GENERO":
                                            config.valor = formulario.gender;
                                            break;
                                        case "SECTOR":
                                            config.valor = formulario.sector;
                                            break;
                                        case "OTRO_SECTOR":
                                            config.valor = formulario.otroSector;
                                            break;
                                        case "PUESTO":
                                            config.valor = formulario.puesto;
                                            break;
                                        case "ANTIGUEDAD":
                                            config.valor = formulario.antiguedad;
                                            break;
                                        case "INGRESO_MENSUAL":
                                            config.valor = formulario.ingresos;
                                            break;
                                        case "NUMERO_PERSONAL":
                                            config.valor = formulario.Celular;
                                            break;
                                        case "CLAVE_PRESUPUESTAL":
                                            config.valor = formulario.cPresupuestal;
                                            break;
                                        case "PAGADURIA":
                                            config.valor = formulario.Pagaduria;
                                            break;
                                        case "FECHA_INGRESO":
                                            config.valor = formulario.fchIngreso;
                                            break;
                                        case "CLAVE":
                                            config.valor = formulario.clave;
                                            break;
                                        case "LUGAR_TRABAJO":
                                            config.valor = formulario.lugTrabajo;
                                            break;
                                        case "CALLE":
                                            config.valor = formulario.calle;
                                            break;
                                        case "NUMERO_EXTERIOR":
                                            config.valor = formulario.nExterior;
                                            break;
                                        case "COLONIA":
                                            config.valor = formulario.colonia;
                                            break;
                                        case "OTRA_COLONIA":
                                            config.valor = formulario.otraColonia;
                                            break;
                                        case "TELEFONO_FIJO":
                                            config.valor = formulario.telFijo;
                                            break;
                                        case "EXTENSION":
                                            config.valor = formulario.extension;
                                            break;
                                        case "ENTIDAD":
                                            config.valor = formulario.entidadT;
                                            break;
                                        case "MUNICIPIO":
                                            config.valor = formulario.municipio;
                                            break;
                                        case "CODIGO_POSTAL_OCUPACION":
                                            config.valor = formulario.CodigoPost;
                                            break;
                                        case "TIENE_CARGO_PUBLICO":
                                            config.valor = formulario.tCargoPu;
                                            break;
                                        case "PERIODO_DE_EJECUCION":
                                            config.valor = formulario.pEjecucion;
                                            break;
                                        case "CARGO_PUBLICO_FAMILIAR":
                                            config.valor = formulario.tCargoPuF;
                                            break;
                                        case "NOMBRE_FAMILIAR":
                                            config.valor = formulario.nombFamiliar;
                                            break;
                                        case "PUESTO_FAMILIAR":
                                            config.valor = formulario.puestoFam;
                                            break;
                                        case "PERIODO_EJERCICO_FAMILIAR":
                                            config.valor = formulario.perEjecucionFam;
                                            break;
                                        case "BENEFICIARIO":
                                            config.valor = formulario.tBeneneficiario;
                                            break;
                                        case "NOMBRE_BENEFICIARIO":
                                            config.valor = formulario.nombBene;
                                            break;
                                        case "TIPO_PENSION":
                                            config.valor = formulario.tipPension;
                                            break;
                                        case "ADSCRIPCION_PAGO":
                                            config.valor = formulario.ubiPago;
                                            break;
                                        case "DELEGACION":
                                            config.valor = formulario.delegacionImss;
                                            break;
                                        case "NOMBRE_TESTIGO1":
                                            config.valor = formulario.nombTest1;
                                            break;
                                        case "MATRICULA_TESTIGO1":
                                            config.valor = formulario.matricula1;
                                            break;
                                        case "GAFETE_TESTIGO1":
                                            config.valor = formulario.gafete1;
                                            break;
                                        case "NOMBRE_TESTIGO2":
                                            config.valor = formulario.nombTest2;
                                            break;
                                        case "MATRICULA_TESTIGO2":
                                            config.valor = formulario.matricula2;
                                            break;
                                        case "GAFETE_TESTIGO2":
                                            config.valor = formulario.gafete1;
                                            break;
                                        case "CODIGO_POSTAL_DOMICILIO":
                                            config.valor = formulario.codPostDom;
                                            break;
                                        case "TIEMPO_RESIDENCIA":
                                            config.valor = formulario.yearResidencia;
                                            break;
                                        case "ENTIDAD_DOMICILIO":
                                            config.valor = formulario.entidadDom;
                                            break;
                                        case "DELEGACION_DOMICILIO":
                                            config.valor = formulario.municipioDom;
                                            break;
                                        case "COLONIA_DOMICILIO":
                                            config.valor = formulario.coloniaDom;
                                            break;
                                        case "OTRA_COLONIA_DOMICILIO":
                                            config.valor = formulario.otraColoniaDom;
                                            break;
                                        case "DOMICILIO_CALLE":
                                            config.valor = formulario.domicilioCalle;
                                            break;
                                        case "NUMERO_EXTERIOR_DOMICILIO":
                                            config.valor = formulario.noExteriorDom;
                                            break;
                                        case "NUMERO_INTERIOR_DOMICILIO":
                                            config.valor = formulario.noInteriorDom;
                                            break;
                                        case "ENTRE_CALLES_DOMICILIO":
                                            config.valor = formulario.entreCalleDom;
                                            break;
                                        case "EMAIL_CONTACTO":
                                            config.valor = formulario.emailContacto;
                                            break;
                                        case "CELULAR":
                                            config.valor = formulario.CelularContacto;
                                            break;
                                        case "EMPRESA_TELEFONICA":
                                            config.valor = formulario.CompanyPhone;
                                            break;
                                        case "TELEFONO_PROPIO":
                                            config.valor = formulario.telefonoPropio;
                                            break;
                                        case "NOMBRE_REFERENCIA1":
                                            config.valor = formulario.nombreRef1;
                                            break;
                                        case "APELLIDO1_REFERENCIA1":
                                            config.valor = formulario.pApellidoRef1;
                                            break;
                                        case "APELLIDO2_REFERENCIA1":
                                            config.valor = formulario.sApellidoRef1;
                                            break;
                                        case "TELEFONO_REFERENCIA1":
                                            config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                            break;
                                        case "CELULAR_REFERENCIA1":
                                            config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                            break;
                                        case "HORA1_REF1":
                                            var hora = Convert.ToDateTime(formulario.Hora1Ref1);
                                            config.valor = hora.ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                            break;
                                        case "HORA2_REF1":
                                            config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", System.Globalization.CultureInfo.CurrentCulture);
                                            break;
                                        case "DIA1_REF1":
                                            config.valor = formulario.dia1Ref1;
                                            break;
                                        case "DIA2_REF1":
                                            config.valor = formulario.dia2Ref1;
                                            break;
                                        case "PARENTESCO_REFERENCIA1":
                                            config.valor = formulario.ParentescoRef1;
                                            break;
                                        case "NOMBRE_REFERENCIA2":
                                            config.valor = formulario.nombreRef2;
                                            break;
                                        case "APELLIDO1_REFERENCIA2":
                                            config.valor = formulario.pApellidoRef2;
                                            break;
                                        case "APELLIDO2_REFERENCIA2":
                                            config.valor = formulario.sApellidoRef2;
                                            break;
                                        case "TELEFONO_REFERENCIA2":
                                            config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                            break;
                                        case "CELULAR_REFERENCIA2":
                                            config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                            break;
                                        case "HORA1_REF2":
                                            config.valor = Convert.ToDateTime(formulario.Hora1Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                            break;
                                        case "HORA2_REF2":
                                            config.valor = Convert.ToDateTime(formulario.Hora2Ref2).ToString("hh:mm", CultureInfo.CurrentCulture);
                                            break;
                                        case "DIA1_REF2":
                                            config.valor = formulario.dia1Ref2;
                                            break;
                                        case "DIA2_REF2":
                                            config.valor = formulario.dia2Ref2;
                                            break;
                                        case "PARENTESCO_REFERENCIA2":
                                            config.valor = formulario.ParentescoRef2;
                                            break;
                                        case "MEDIO_DISPOSICION":
                                            config.valor = formulario.medioDisp;
                                            break;
                                        case "CLABE_CUENTA":
                                            config.valor = formulario.ClabeDisp;
                                            break;
                                        case "CLABE_CUENTA DIGITO 1":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 2":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 3":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 4":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 5":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 6":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 7":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 8":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 9":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 10":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 11":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 12":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 13":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 14":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 15":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 16":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 17":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA DIGITO 18":
                                            config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO":
                                            config.valor = formulario.NombreBanco;
                                            break;
                                        case "NUMERO_CUENTA":
                                            config.valor = formulario.NumCuentaBanc;
                                            break;
                                        case "NUMERO_CUENTA DIGITO 1":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 2":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 3":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 4":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 5":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 6":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 7":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 8":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 9":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 10":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA DIGITO 11":
                                            config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                            break;
                                        case "MEDIO_DISPOSICION_ALTERNO":
                                            config.valor = formulario.medioDispAlt;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO":
                                            config.valor = formulario.ClabeDispAlt;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                            config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO_ALTERNO":
                                            config.valor = formulario.NombreBancoAlt;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO":
                                            config.valor = formulario.NumCuentaBancAlt;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                            config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                            break;
                                        case "MEDIO_DISPOSICION_ALTERNO_2":
                                            config.valor = formulario.medioDispAlt2;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2":
                                            config.valor = formulario.ClabeDispAlt2;
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                            break;
                                        case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                            config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                            break;
                                        case "BANCO_ALTERNO_2":
                                            config.valor = formulario.NombreBancoAlt2;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2":
                                            config.valor = formulario.NumCuentaBancAlt2;
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                            break;
                                        case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                            config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                            break;
                                        case "CURP":
                                            config.valor = formulario.CURP;
                                            break;
                                        case "MONTO_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                            break;
                                        case "MONTO_ESCRITO_SIN_PESOS":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.monto)).Replace("PESOS", "");
                                            break;
                                        case "LIQUIDO_BASE_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.LBase));
                                            break;
                                        case "DESCUENTO_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.dscto));
                                            break;
                                        case "MONTO_MAXIMO_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz));
                                            break;
                                        case "MONTO_DEUDOR_ESCRITO":
                                            config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor));
                                            break;
                                        case "NACIONALIDAD":
                                            config.valor = formulario.nacionalidad;
                                            break;
                                        case "EDAD_CLIENTE":
                                            DateTime fechaActual = DateTime.Today;
                                            var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                            edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                            config.valor = edad + "";
                                            break;
                                        case "TASA_MENSUAL":
                                            config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                            break;
                                        case "TOTAL_A_PAGAR_CON_INTERES":
                                            config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                            break;
                                        case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                            var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                            config.valor = config.valor = dao.monto_escrito(dto);
                                            break;
                                        case "NOMBRE_COMPLETO":
                                            config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                            break;
                                    }

                                }
                                if (config.tvalidacion == "S")
                                {
                                    switch (config.campoValidar)
                                    {
                                        case "TIPO_SOLICITUD":
                                            if (formulario.tipoSolicitud != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PERIODO":
                                            if (formulario.periodo != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "DEPENDENCIA":
                                            if (formulario.Dependencia != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PRODUCTO":
                                            if (formulario.producto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "DESTINO":
                                            if (formulario.destino != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "QUINCENA_DSCTO":
                                            if (formulario.quincenaDscto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "IDENTIFICACION":
                                            if (formulario.tipoDoc != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "OTRA_IDENTIFICACION":
                                            if (formulario.otraIdentificacion != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "GENERO":
                                            if (formulario.gender != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "SECTOR":
                                            if (formulario.sector != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "PUESTO":
                                            if (formulario.puesto != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "INGRESOS":
                                            if (formulario.ingresos != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "CARGO_PUBLICO":
                                            if (formulario.tCargoPu != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "CARGO_PUBLICO_FAM":
                                            if (formulario.tCargoPuF != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BENEFICIARIO":
                                            if (formulario.tBeneneficiario != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "EMP_TEL":
                                            if (formulario.CompanyPhone != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP":
                                            if (formulario.medioDisp != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP_ALT1":
                                            if (formulario.medioDispAlt != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "MED_DISP_ALT2":
                                            if (formulario.medioDispAlt2 != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO":
                                            if (formulario.NombreBanco != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO_ALT1":
                                            if (formulario.NombreBancoAlt != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "BANCO_ALT2":
                                            if (formulario.NombreBancoAlt2 != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "RECA":
                                            if (formulario.reca != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "ESTADO_CIVIL":
                                            if (formulario.estadoCivil != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                        case "NACIONALIDAD":
                                            if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                            {
                                                if (config.valor_validacion != "MEXICANO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "PAIS_RESIDENCIA":
                                            if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                            {
                                                if (config.valor_validacion != "MÉXICO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "PAIS_NACIMIENTO":
                                            if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                            {
                                                if (config.valor_validacion != "MÉXICO")
                                                {
                                                    config.valor = "";
                                                }

                                            }
                                            else
                                            {
                                                if (config.valor_validacion != "OTRA")
                                                {
                                                    config.valor = "";
                                                }
                                            }
                                            break;
                                        case "CLAVE_TRABAJADOR":
                                            if (formulario.clave_trabajdor != config.valor_validacion)
                                            {
                                                config.valor = "";
                                            }
                                            break;
                                    }
                                }
                                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                                page = doc.GetPage(pageDoc);
                                //page.SetPageRotationInverseMatrixWritten();
                                canvas = new PdfCanvas(page);
                                Color myColor = new DeviceRgb(000, 000, 000);
                                canvas.BeginText().SetColor(myColor, true).SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                canvas.SaveState();
                                canvas.RestoreState();
                                //break;
                            }
                            //break;
                            pageDoc++;
                            //break;
                        }
                    }
                    else if (data1.ListDocumentos[i].llena_auto == 1 && data1.ListDocumentos[i].compra == 1)
                    {
                        if (i > 0)
                        {
                            reader.Close();
                            reader = new PdfReader(data1.ListDocumentos[i].path);
                        }
                        doc1 = new PdfDocument(reader);
                        for (var m = 0; m < cartera.Count(); m++)
                        {
                            var cart = cartera.ElementAt(m);
                        Found:
                            if (item > 0)
                            {
                                if (item >= cart.Count())
                                {
                                    h = 0;
                                    goto Finish;
                                }
                                h = (int)item;
                            }
                            for (var k = 1; k <= doc1.GetNumberOfPages(); k++)
                            {
                                page1 = doc1.GetPage(k);
                                doc.AddPage(page1.CopyTo(doc));
                                OutParamConfiguracionDoc configuraciones = new ParamsDAO().getConfigDocumentosFirma(dependencia, producto, data1.ListDocumentos[i].codigo_doc, k);
                                for (var j = 0; j < configuraciones.ListConfiguracion.Count(); j++)
                                {
                                    var config = configuraciones.ListConfiguracion.ElementAt(j);
                                    config_anterior = config.valor;
                                    if (config.tipo_optencion == "2")
                                    {
                                        switch (config.valor)
                                        {
                                            case "NUMERO_FOLDER":
                                                config.valor = formulario.folderNumber;
                                                break;
                                            case "ASESOR":
                                                config.valor = formulario.asesor;
                                                break;
                                            case "FECHA_SOLICITUD":
                                                config.valor = formulario.fchsolicitud;
                                                break;
                                            case "DIA_SOLICITUD":
                                                config.valor = fecha_solicitud[0];
                                                break;
                                            case "MES_SOLICITUD":
                                                config.valor = fecha_solicitud[1];
                                                break;
                                            case "MES_SOLICITUD_LETRAS":
                                                n = int.Parse(fecha_solicitud[1]);
                                                config.valor = meses[n - 1];
                                                break;
                                            case "AÑO_SOLICITUD_4_DIGITOS":
                                                config.valor = fecha_solicitud[2];
                                                break;
                                            case "AÑO_SOLICITUD_2_DIGITOS":
                                                config.valor = fecha_solicitud[2].Substring(2, 2);
                                                break;
                                            case "TIPO_SOLICITUD":
                                                config.valor = formulario.tipoSolicitud;
                                                break;
                                            case "MONTO":
                                                config.valor = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "PERIODO":
                                                config.valor = formulario.periodo;
                                                break;
                                            case "PLAZO":
                                                config.valor = formulario.plazo;
                                                break;
                                            case "LIQUIDO_BASE":
                                                config.valor = double.Parse(formulario.LBase).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "NO_PLAZAS":
                                                config.valor = (formulario.nPlazas.Equals("0")) ? "" : formulario.nPlazas;
                                                break;
                                            case "DEPENDENCIA":
                                                config.valor = formulario.Dependencia;
                                                break;
                                            case "PRODUCTO":
                                                config.valor = formulario.producto;
                                                break;
                                            case "DESTINO_CREDITO":
                                                config.valor = formulario.destino;
                                                break;
                                            case "TIPO_NOMINA":
                                                config.valor = formulario.tNomina;
                                                break;
                                            case "DESCUENTO":
                                                config.valor = formulario.dscto;
                                                break;
                                            case "TASA_ANUAL":
                                                config.valor = formulario.tAnual;
                                                break;
                                            case "CAT":
                                                config.valor = formulario.cat;
                                                break;
                                            case "SUCURSAL":
                                                config.valor = formulario.sucursal;
                                                break;
                                            case "QUINCENA_DSCTO":
                                                config.valor = formulario.quincenaDscto;
                                                break;
                                            case "FECHA_PRIMER_PAGO":
                                                config.valor = formulario.fchPrPago;
                                                break;
                                            case "DIA_PRIMER_PAGO":
                                                config.valor = config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[0] : "";
                                                break;
                                            case "MES_PRIMER_PAGO":
                                                if (fecha_primer_pago.Length == 3)
                                                {
                                                    config.valor = fecha_primer_pago[1];
                                                }
                                                else
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "MES_PRIMER_PAGO_LETRAS":
                                                if (fecha_primer_pago.Length == 3)
                                                {
                                                    n = int.Parse(fecha_primer_pago[1]);
                                                    config.valor = meses[n - 1];
                                                }
                                                else
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "AÑO_PRIMER_PAGO_2_DIGITOS":
                                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2].Substring(2, 2) : "";
                                                break;
                                            case "AÑO_PRIMER_PAGO_4_DIGITOS":
                                                config.valor = (fecha_primer_pago.Length == 3) ? fecha_primer_pago[2] : "";
                                                break;
                                            case "FECHA_ULTIMO_PAGO":
                                                config.valor = formulario.fchUltPago;
                                                break;
                                            case "DIA_ULTIMO_PAGO":
                                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[0] : "";
                                                break;
                                            case "MES_ULTIMO_PAGO":
                                                if (fecha_ultimo_pago.Length == 3)
                                                {
                                                    config.valor = fecha_ultimo_pago[1];
                                                }
                                                else
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "MES_ULTIMO_PAGO_LETRAS":
                                                if (fecha_primer_pago.Length == 3)
                                                {
                                                    n = int.Parse(fecha_ultimo_pago[1]);
                                                    config.valor = meses[n - 1];
                                                }
                                                else
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "AÑO_ULTIMO_PAGO_2_DIGITOS":
                                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2].Substring(2, 2) : "";
                                                break;
                                            case "AÑO_ULTIMO_PAGO_4_DIGITOS":
                                                config.valor = (fecha_ultimo_pago.Length == 3) ? fecha_ultimo_pago[2] : "";
                                                break;
                                            case "CAPACIDAD_PAGO":
                                                config.valor = double.Parse(formulario.cPago).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "MONTO_MAXIMO":
                                                config.valor = double.Parse(formulario.mMaxPlaz).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "MONTO_DEUDOR":
                                                config.valor = (formulario.monto_deudor.Equals("0")) ? "" : double.Parse(formulario.monto_deudor).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "MATRICULA":
                                                config.valor = (formulario.matricula.Equals("0")) ? "" : formulario.matricula;
                                                break;
                                            case "NSS":
                                                config.valor = (formulario.nss.Equals("0")) ? "" : formulario.nss;
                                                break;
                                            case "GRUPO":
                                                config.valor = (formulario.grupo.Equals("0")) ? "" : formulario.grupo;
                                                break;
                                            case "CLAVE_TRABAJADOR":
                                                config.valor = (formulario.clave_trabajdor.Equals("0")) ? "" : formulario.clave_trabajdor;
                                                break;
                                            case "ESPECIFICAR":
                                                config.valor = formulario.especificar;
                                                break;
                                            case "RECA":
                                                config.valor = formulario.reca;
                                                break;
                                            case "RFC":
                                                config.valor = formulario.RFC;
                                                break;
                                            case "NOMBRES":
                                                config.valor = formulario.pNombre + " " + formulario.sNombre;
                                                break;
                                            case "PRIMER_APELLIDO":
                                                config.valor = formulario.pApellido;
                                                break;
                                            case "SEGUNDO_APELLIDO":
                                                config.valor = formulario.sApellido;
                                                break;
                                            case "IDENTIFICACION_OFICIAL":
                                                config.valor = formulario.tipoDoc;
                                                break;
                                            case "FECHA_NACIMIENTO":
                                                config.valor = formulario.fecNac;
                                                break;
                                            case "DIA_NACIMIENTO":
                                                config.valor = fecha_nacimiento[0];
                                                break;
                                            case "MES_NACIMIENTO":
                                                config.valor = fecha_nacimiento[1];
                                                break;
                                            case "MES_NACIMIENTO_LETRAS":
                                                n = int.Parse(fecha_solicitud[1]);
                                                config.valor = meses[n - 1];
                                                break;
                                            case "AÑO_NACIMIENTO_2_DIGITOS":
                                                config.valor = fecha_nacimiento[2].Substring(2, 2);
                                                break;
                                            case "AÑO_NACIMIENTO_4_DIGITOS":
                                                config.valor = fecha_nacimiento[2];
                                                break;
                                            case "PAIS_NACIMIENTO":
                                                config.valor = formulario.paisN;
                                                break;
                                            case "ENTIDAD_NACIMIENTO":
                                                config.valor = formulario.entidadN;
                                                break;
                                            case "PAIS_RESIDENCIA":
                                                config.valor = formulario.paisR;
                                                break;
                                            case "FORMA_MIGRATORIA":
                                                config.valor = formulario.fMigratoria;
                                                break;
                                            case "GENERO":
                                                config.valor = formulario.gender;
                                                break;
                                            case "SECTOR":
                                                config.valor = formulario.sector;
                                                break;
                                            case "OTRO_SECTOR":
                                                config.valor = formulario.otroSector;
                                                break;
                                            case "PUESTO":
                                                config.valor = formulario.puesto;
                                                break;
                                            case "ANTIGUEDAD":
                                                config.valor = formulario.antiguedad;
                                                break;
                                            case "INGRESO_MENSUAL":
                                                config.valor = formulario.ingresos;
                                                break;
                                            case "NUMERO_PERSONAL":
                                                config.valor = formulario.Celular;
                                                break;
                                            case "CLAVE_PRESUPUESTAL":
                                                config.valor = formulario.cPresupuestal;
                                                break;
                                            case "PAGADURIA":
                                                config.valor = formulario.Pagaduria;
                                                break;
                                            case "FECHA_INGRESO":
                                                config.valor = formulario.fchIngreso;
                                                break;
                                            case "CLAVE":
                                                config.valor = formulario.clave;
                                                break;
                                            case "LUGAR_TRABAJO":
                                                config.valor = formulario.lugTrabajo;
                                                break;
                                            case "CALLE":
                                                config.valor = formulario.calle;
                                                break;
                                            case "NUMERO_EXTERIOR":
                                                config.valor = formulario.nExterior;
                                                break;
                                            case "COLONIA":
                                                config.valor = formulario.colonia;
                                                break;
                                            case "OTRA_COLONIA":
                                                config.valor = formulario.otraColonia;
                                                break;
                                            case "TELEFONO_FIJO":
                                                config.valor = formulario.telFijo;
                                                break;
                                            case "EXTENSION":
                                                config.valor = formulario.extension;
                                                break;
                                            case "ENTIDAD":
                                                config.valor = formulario.entidadT;
                                                break;
                                            case "MUNICIPIO":
                                                config.valor = formulario.municipio;
                                                break;
                                            case "CODIGO_POSTAL_OCUPACION":
                                                config.valor = formulario.CodigoPost;
                                                break;
                                            case "TIENE_CARGO_PUBLICO":
                                                config.valor = formulario.tCargoPu;
                                                break;
                                            case "PERIODO_DE_EJECUCION":
                                                config.valor = formulario.pEjecucion;
                                                break;
                                            case "CARGO_PUBLICO_FAMILIAR":
                                                config.valor = formulario.tCargoPuF;
                                                break;
                                            case "NOMBRE_FAMILIAR":
                                                config.valor = formulario.nombFamiliar;
                                                break;
                                            case "PUESTO_FAMILIAR":
                                                config.valor = formulario.puestoFam;
                                                break;
                                            case "PERIODO_EJERCICO_FAMILIAR":
                                                config.valor = formulario.perEjecucionFam;
                                                break;
                                            case "BENEFICIARIO":
                                                config.valor = formulario.tBeneneficiario;
                                                break;
                                            case "NOMBRE_BENEFICIARIO":
                                                config.valor = formulario.nombBene;
                                                break;
                                            case "TIPO_PENSION":
                                                config.valor = formulario.tipPension;
                                                break;
                                            case "ADSCRIPCION_PAGO":
                                                config.valor = formulario.ubiPago;
                                                break;
                                            case "DELEGACION":
                                                config.valor = formulario.delegacionImss;
                                                break;
                                            case "NOMBRE_TESTIGO1":
                                                config.valor = formulario.nombTest1;
                                                break;
                                            case "MATRICULA_TESTIGO1":
                                                config.valor = formulario.matricula1;
                                                break;
                                            case "GAFETE_TESTIGO1":
                                                config.valor = formulario.gafete1;
                                                break;
                                            case "NOMBRE_TESTIGO2":
                                                config.valor = formulario.nombTest2;
                                                break;
                                            case "MATRICULA_TESTIGO2":
                                                config.valor = formulario.matricula2;
                                                break;
                                            case "GAFETE_TESTIGO2":
                                                config.valor = formulario.gafete1;
                                                break;
                                            case "CODIGO_POSTAL_DOMICILIO":
                                                config.valor = formulario.codPostDom;
                                                break;
                                            case "TIEMPO_RESIDENCIA":
                                                config.valor = formulario.yearResidencia;
                                                break;
                                            case "ENTIDAD_DOMICILIO":
                                                config.valor = formulario.entidadDom;
                                                break;
                                            case "DELEGACION_DOMICILIO":
                                                config.valor = formulario.municipioDom;
                                                break;
                                            case "COLONIA_DOMICILIO":
                                                config.valor = formulario.coloniaDom;
                                                break;
                                            case "OTRA_COLONIA_DOMICILIO":
                                                config.valor = formulario.otraColoniaDom;
                                                break;
                                            case "DOMICILIO_CALLE":
                                                config.valor = formulario.domicilioCalle;
                                                break;
                                            case "NUMERO_EXTERIOR_DOMICILIO":
                                                config.valor = formulario.noExteriorDom;
                                                break;
                                            case "NUMERO_INTERIOR_DOMICILIO":
                                                config.valor = formulario.noInteriorDom;
                                                break;
                                            case "ENTRE_CALLES_DOMICILIO":
                                                config.valor = formulario.entreCalleDom;
                                                break;
                                            case "EMAIL_CONTACTO":
                                                config.valor = formulario.emailContacto;
                                                break;
                                            case "CELULAR":
                                                config.valor = formulario.CelularContacto;
                                                break;
                                            case "EMPRESA_TELEFONICA":
                                                config.valor = formulario.CompanyPhone;
                                                break;
                                            case "TELEFONO_PROPIO":
                                                config.valor = formulario.telefonoPropio;
                                                break;
                                            case "NOMBRE_REFERENCIA1":
                                                config.valor = formulario.nombreRef1;
                                                break;
                                            case "APELLIDO1_REFERENCIA1":
                                                config.valor = formulario.pApellidoRef1;
                                                break;
                                            case "APELLIDO2_REFERENCIA1":
                                                config.valor = formulario.sApellidoRef1;
                                                break;
                                            case "TELEFONO_REFERENCIA1":
                                                config.valor = (formulario.TelefonoRef1.Equals("0")) ? "" : formulario.TelefonoRef1;
                                                break;
                                            case "CELULAR_REFERENCIA1":
                                                config.valor = (formulario.CelularRef1.Equals("0")) ? "" : formulario.CelularRef1;
                                                break;
                                            case "HORA1_REF1":
                                                config.valor = formulario.Hora1Ref1.ToString().Substring(11, 17);
                                                break;
                                            case "HORA2_REF1":
                                                config.valor = formulario.Hora2Ref1.ToString().Substring(11, 17);
                                                break;
                                            case "DIA1_REF1":
                                                config.valor = formulario.dia1Ref1;
                                                break;
                                            case "DIA2_REF1":
                                                config.valor = formulario.dia2Ref1;
                                                break;
                                            case "PARENTESCO_REFERENCIA1":
                                                config.valor = formulario.ParentescoRef1;
                                                break;
                                            case "NOMBRE_REFERENCIA2":
                                                config.valor = formulario.nombreRef2;
                                                break;
                                            case "APELLIDO1_REFERENCIA2":
                                                config.valor = formulario.pApellidoRef2;
                                                break;
                                            case "APELLIDO2_REFERENCIA2":
                                                config.valor = formulario.sApellidoRef2;
                                                break;
                                            case "TELEFONO_REFERENCIA2":
                                                config.valor = (formulario.TelefonoRef2.Equals("0")) ? "" : formulario.TelefonoRef2;
                                                break;
                                            case "CELULAR_REFERENCIA2":
                                                config.valor = (formulario.CelularRef2.Equals("0")) ? "" : formulario.CelularRef2;
                                                break;
                                            case "HORA1_REF2":
                                                config.valor = formulario.Hora1Ref2.ToString();
                                                break;
                                            case "HORA2_REF2":
                                                config.valor = formulario.Hora2Ref2.ToString();
                                                break;
                                            case "DIA1_REF2":
                                                config.valor = formulario.dia1Ref2;
                                                break;
                                            case "DIA2_REF2":
                                                config.valor = formulario.dia2Ref2;
                                                break;
                                            case "PARENTESCO_REFERENCIA2":
                                                config.valor = formulario.ParentescoRef2;
                                                break;
                                            case "MEDIO_DISPOSICION":
                                                config.valor = formulario.medioDisp;
                                                break;
                                            case "CLABE_CUENTA":
                                                config.valor = formulario.ClabeDisp;
                                                break;
                                            case "CLABE_CUENTA DIGITO 1":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(0) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 2":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(1) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 3":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(2) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 4":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(3) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 5":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(4) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 6":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(5) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 7":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(6) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 8":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(7) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 9":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(8) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 10":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(9) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 11":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(10) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 12":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(11) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 13":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(12) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 14":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 15":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 16":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 17":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                                break;
                                            case "CLABE_CUENTA DIGITO 18":
                                                config.valor = (formulario.ClabeDisp.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                                break;
                                            case "BANCO":
                                                config.valor = formulario.NombreBanco;
                                                break;
                                            case "NUMERO_CUENTA":
                                                config.valor = formulario.NumCuentaBanc;
                                                break;
                                            case "NUMERO_CUENTA DIGITO 1":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(0) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 2":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(1) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 3":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(2) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 4":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(3) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 5":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(4) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 6":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(5) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 7":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(6) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 8":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(7) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 9":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(8) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 10":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(9) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA DIGITO 11":
                                                config.valor = (formulario.NumCuentaBanc.Length == 11) ? formulario.NumCuentaBanc.ElementAt(10) + "" : "";
                                                break;
                                            case "MEDIO_DISPOSICION_ALTERNO":
                                                config.valor = formulario.medioDispAlt;
                                                break;
                                            case "CLABE_CUENTA_ALTERNO":
                                                config.valor = formulario.ClabeDispAlt;
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 1":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(0) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 2":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(1) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 3":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(2) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 4":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(3) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 5":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(4) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 6":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(5) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 7":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(6) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 8":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(7) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 9":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(8) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 10":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(9) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 11":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(10) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 12":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(11) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 13":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDispAlt.ElementAt(12) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 14":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(13) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 15":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(14) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 16":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(15) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 17":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(16) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO DIGITO 18":
                                                config.valor = (formulario.ClabeDispAlt.Length == 18) ? formulario.ClabeDisp.ElementAt(17) + "" : "";
                                                break;
                                            case "BANCO_ALTERNO":
                                                config.valor = formulario.NombreBancoAlt;
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO":
                                                config.valor = formulario.NumCuentaBancAlt;
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 1":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(0) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 2":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(1) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 3":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(2) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 4":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(3) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 5":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(4) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 6":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(5) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 7":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(6) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 8":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(7) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 9":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(8) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 10":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(9) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO DIGITO 11":
                                                config.valor = (formulario.NumCuentaBancAlt.Length == 11) ? formulario.NumCuentaBancAlt.ElementAt(10) + "" : "";
                                                break;
                                            case "MEDIO_DISPOSICION_ALTERNO_2":
                                                config.valor = formulario.medioDispAlt2;
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2":
                                                config.valor = formulario.ClabeDispAlt2;
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 1":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(0) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 2":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(1) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 3":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(2) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 4":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(3) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 5":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(4) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 6":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(5) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 7":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(6) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 8":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(7) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 9":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(8) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 10":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(9) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 11":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(10) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 12":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(11) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 13":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(12) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 14":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(13) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 15":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(14) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 16":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(15) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 17":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(16) + "" : "";
                                                break;
                                            case "CLABE_CUENTA_ALTERNO_2 DIGITO 18":
                                                config.valor = (formulario.ClabeDispAlt2.Length == 18) ? formulario.ClabeDispAlt2.ElementAt(17) + "" : "";
                                                break;
                                            case "BANCO_ALTERNO_2":
                                                config.valor = formulario.NombreBancoAlt2;
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2":
                                                config.valor = formulario.NumCuentaBancAlt2;
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 1":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(0) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 2":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(1) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 3":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(2) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 4":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(3) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 5":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(4) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 6":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(5) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 7":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(6) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 8":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(7) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 9":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(8) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 10":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(9) + "" : "";
                                                break;
                                            case "NUMERO_CUENTA_ALTERNO_2 DIGITO 11":
                                                config.valor = (formulario.NumCuentaBancAlt2.Length == 11) ? formulario.NumCuentaBancAlt2.ElementAt(10) + "" : "";
                                                break;
                                            case "CURP":
                                                config.valor = formulario.CURP;
                                                break;
                                            case "MONTO_ESCRITO":
                                                config.valor = dao.monto_escrito(double.Parse(formulario.monto));
                                                break;
                                            case "MONTO_ESCRITO_SIN_PESOS":
                                                var monto = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                                config.valor = dao.monto_escrito(double.Parse(monto)).Replace("PESOS", "");
                                                break;
                                            case "CENTAVOS_MONTO_ESCRITO":
                                                var arr2 = double.Parse(formulario.monto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                                config.valor = arr2[1];
                                                break;
                                            case "LIQUIDO_BASE_ESCRITO":
                                                config.valor = dao.monto_escrito(double.Parse(formulario.LBase)).Replace("PESOS", "");
                                                break;
                                            case "DESCUENTO_ESCRITO":
                                                var dscto = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                                config.valor = dao.monto_escrito(double.Parse(dscto)).Replace("PESOS", "");
                                                break;
                                            case "CENTAVOS_DESCUENTO_ESCRITO":
                                                var arr3 = double.Parse(formulario.dscto).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                                config.valor = arr3[1];
                                                break;
                                            case "MONTO_MAXIMO_ESCRITO":
                                                config.valor = dao.monto_escrito(double.Parse(formulario.mMaxPlaz)).Replace("PESOS", "");
                                                break;
                                            case "MONTO_DEUDOR_ESCRITO":
                                                config.valor = dao.monto_escrito(double.Parse(formulario.monto_deudor)).Replace("PESOS", "");
                                                break;
                                            case "NACIONALIDAD":
                                                config.valor = formulario.nacionalidad;
                                                break;
                                            case "EDAD_CLIENTE":
                                                DateTime fechaActual = DateTime.Today;
                                                var edad = fechaActual.Year - int.Parse(fecha_nacimiento[2]);
                                                edad = (fechaActual.Month < int.Parse(fecha_nacimiento[1])) ? edad - 1 : edad;
                                                config.valor = edad + "";
                                                break;
                                            case "TASA_MENSUAL":
                                                config.valor = double.Parse(formulario.tAnual) / 12 + "";
                                                break;
                                            case "TOTAL_A_PAGAR_CON_INTERES":
                                                config.valor = (double.Parse(formulario.dscto) * double.Parse(formulario.plazo)).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "TOTAL_A_PAGAR_CON_INTERES_LETRAS":
                                                var dto = double.Parse(formulario.dscto) * double.Parse(formulario.plazo);
                                                config.valor = config.valor = dao.monto_escrito(dto);
                                                break;
                                            case "NOMBRE_COMPLETO":
                                                config.valor = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                                                break;
                                            case "CASA_FINANCIERA":
                                                config.valor = cart.ElementAt(h).entidad;
                                                break;
                                            case "SUMA_SALDO_INSOLUTO":
                                                sum = 0;
                                                var q3 = h;
                                                var lim3 = (data1.ListDocumentos[i].max_item + q3 == 0) ? cart.Count() : data1.ListDocumentos[i].max_item + q3;
                                                for (; q3 < lim3; q3++)
                                                {
                                                    if (q3 == cart.Count()) break;
                                                    sum += cart.ElementAt(q3).saldoInsoluto;
                                                }
                                                config.valor = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "SUMA_SALDO_INSOLUTO_LETRA":
                                                sum = 0;
                                                var q1 = h;
                                                var lim2 = (data1.ListDocumentos[i].max_item + q1 == 0) ? cart.Count() : data1.ListDocumentos[i].max_item + q1;
                                                for (; q1 < lim2; q1++)
                                                {
                                                    if (q1 == cart.Count()) break;
                                                    sum += cart.ElementAt(q1).saldoInsoluto;
                                                }
                                                sum = double.Parse(sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", ""));
                                                config.valor = dao.monto_escrito(sum).Replace("PESOS", "");
                                                break;
                                            case "CENTAVOS_SUMA_SALDO_INSOLUTO_LETRA":
                                                sum = 0;
                                                var q = h;
                                                var lim = (data1.ListDocumentos[i].max_item + q == 0) ? cart.Count() : data1.ListDocumentos[i].max_item + q;
                                                for (; q < lim; q++)
                                                {
                                                    if (q == cart.Count()) break;
                                                    sum += cart.ElementAt(q).saldoInsoluto;
                                                }
                                                var arr = sum.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                                config.valor = arr[1];
                                                break;
                                            case "DEPOSITO_CLIENTE":
                                                config.valor = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "DEPOSITO_CLIENTE_LETRA":
                                                var depositoCliente = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.')[0].Replace(",", "");
                                                config.valor = dao.monto_escrito(double.Parse(depositoCliente)).Replace("PESOS", "");
                                                break;
                                            case "CENTAVOS_DEPOSITO_CLIENTE_LETRA":
                                                var arr1 = double.Parse(formulario.depositoCliente).ToString("N2", CultureInfo.CreateSpecificCulture("es-MX")).Split('.');
                                                config.valor = arr1[1];
                                                break;
                                            case "DIAS_A_PAGAR":
                                                config.valor = formulario.DiasPagar;
                                                break;
                                            case "FECHA_CONTRATO_COMPRA":
                                                config.valor = cart.ElementAt(h).fecha.Substring(0, 10);
                                                break;
                                            case "MONTO_CREDITO_COMPRA":
                                                config.valor = cart.ElementAt(h).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "MONTO_TOTAL":
                                                config.valor = cart.ElementAt(h).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "PLAZO_COMPRA":
                                                config.valor = cart.ElementAt(h).plazo + "";
                                                break;
                                            case "SALDO_INSOLUTO":
                                                config.valor = cart.ElementAt(h).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                break;
                                            case "TASA_COMPRA":
                                                config.valor = cart.ElementAt(h).tasa + "";
                                                break;
                                        }
                                    }
                                    if (config.tvalidacion == "S")
                                    {
                                        switch (config.campoValidar)
                                        {
                                            case "TIPO_SOLICITUD":
                                                if (formulario.tipoSolicitud != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "PERIODO":
                                                if (formulario.periodo != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "DEPENDENCIA":
                                                if (formulario.Dependencia != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "PRODUCTO":
                                                if (formulario.producto != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "DESTINO":
                                                if (formulario.destino != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "QUINCENA_DSCTO":
                                                if (formulario.quincenaDscto != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "IDENTIFICACION":
                                                if (formulario.tipoDoc != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "OTRA_IDENTIFICACION":
                                                if (formulario.otraIdentificacion != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "GENERO":
                                                if (formulario.gender != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "SECTOR":
                                                if (formulario.sector != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "PUESTO":
                                                if (formulario.puesto != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "INGRESOS":
                                                if (formulario.ingresos != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "CARGO_PUBLICO":
                                                if (formulario.tCargoPu != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "CARGO_PUBLICO_FAM":
                                                if (formulario.tCargoPuF != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "BENEFICIARIO":
                                                if (formulario.tBeneneficiario != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "EMP_TEL":
                                                if (formulario.CompanyPhone != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "MED_DISP":
                                                if (formulario.medioDisp != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "MED_DISP_ALT1":
                                                if (formulario.medioDispAlt != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "MED_DISP_ALT2":
                                                if (formulario.medioDispAlt2 != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "BANCO":
                                                if (formulario.NombreBanco != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "BANCO_ALT1":
                                                if (formulario.NombreBancoAlt != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "BANCO_ALT2":
                                                if (formulario.NombreBancoAlt2 != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "RECA":
                                                if (formulario.reca != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "ESTADO_CIVIL":
                                                if (formulario.estadoCivil != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                            case "NACIONALIDAD":
                                                if (formulario.nacionalidad.ToUpper().IndexOf("MEXICAN") > -1)
                                                {
                                                    if (config.valor_validacion != "MEXICANO")
                                                    {
                                                        config.valor = "";
                                                    }

                                                }
                                                else
                                                {
                                                    if (config.valor_validacion != "OTRA")
                                                    {
                                                        config.valor = "";
                                                    }
                                                }
                                                break;
                                            case "PAIS_RESIDENCIA":
                                                if (formulario.paisR.ToUpper().IndexOf("MÉXICO") > -1)
                                                {
                                                    if (config.valor_validacion != "MÉXICO")
                                                    {
                                                        config.valor = "";
                                                    }

                                                }
                                                else
                                                {
                                                    if (config.valor_validacion != "OTRA")
                                                    {
                                                        config.valor = "";
                                                    }
                                                }
                                                break;
                                            case "PAIS_NACIMIENTO":
                                                if (formulario.paisN.ToUpper().IndexOf("MÉXICO") > -1)
                                                {
                                                    if (config.valor_validacion != "MÉXICO")
                                                    {
                                                        config.valor = "";
                                                    }

                                                }
                                                else
                                                {
                                                    if (config.valor_validacion != "OTRA")
                                                    {
                                                        config.valor = "";
                                                    }
                                                }
                                                break;
                                            case "CLAVE_TRABAJADOR":
                                                if (formulario.clave_trabajdor != config.valor_validacion)
                                                {
                                                    config.valor = "";
                                                }
                                                break;
                                        }
                                    }
                                    var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                                    page = doc.GetPage(pageDoc);
                                    canvas = new PdfCanvas(page);
                                    Color myColor = new DeviceRgb(000, 000, 000);
                                    canvas.BeginText().SetColor(myColor, true).SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                    if (data1.ListDocumentos[i].max_item > 0 && config.aumentoy > 0 && cart.Count() >= data1.ListDocumentos[i].max_item)
                                    {
                                        var limite = data1.ListDocumentos[i].max_item + h;
                                        h++;
                                        for (var l = 1; l < limite; l++)
                                        {
                                            if (h < cart.Count())
                                            {
                                                switch (config_anterior)
                                                {
                                                    case "CASA_FINANCIERA":
                                                        config.valor = cart.ElementAt(l).entidad;
                                                        break;
                                                    case "FECHA_CONTRATO_COMPRA":
                                                        config.valor = cart.ElementAt(l).fecha.Substring(0, 10);
                                                        break;
                                                    case "MONTO_CREDITO_COMPRA":
                                                        config.valor = cart.ElementAt(l).capital.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                        break;
                                                    case "MONTO_TOTAL":
                                                        config.valor = cart.ElementAt(l).totPagar.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                        break;
                                                    case "PLAZO_COMPRA":
                                                        config.valor = cart.ElementAt(l).plazo + "";
                                                        break;
                                                    case "SALDO_INSOLUTO":
                                                        config.valor = cart.ElementAt(l).saldoInsoluto.ToString("N2", CultureInfo.CreateSpecificCulture("es-MX"));
                                                        break;
                                                    case "TASA_COMPRA":
                                                        config.valor = cart.ElementAt(l).tasa + "";
                                                        break;
                                                }
                                                config.posicion_y -= config.aumentoy;
                                                canvas.BeginText().SetFontAndSize(font, (float)config.fuente).MoveText((float)config.posicion_x, (float)config.posicion_y).ShowText(config.valor).EndText();
                                                h++;
                                            }
                                        }
                                        h = (int)item;
                                    }
                                    canvas.SaveState();
                                    canvas.RestoreState();
                                }
                                pageDoc++;
                                if (data1.ListDocumentos[i].max_item > 0)
                                {
                                    item = (item == 0) ? item = (double)data1.ListDocumentos[i].max_item : item += (double)data1.ListDocumentos[i].max_item;
                                    goto Found;
                                }
                            }
                        Finish:
                            item = 0;
                        }
                    }
                    else
                    {
                        var docExiste = dao.docExiste(data1.ListDocumentos[i].codigo_doc, folder);
                        if (int.Parse(docExiste[0]) > 0)
                        {
                            var documentosCargados = dao.buscarDocumentos(folder, (double)data1.ListDocumentos[i].codigo_doc);
                            for (var x = 0; x < documentosCargados.ListDocumentos.Count(); x++)
                            {
                                Regex regex = new Regex("(.png|.jpeg|.jpg)$");
                                Match match = regex.Match(documentosCargados.ListDocumentos[x].path);
                                if (match.Success)
                                {
                                    ImageData img = ImageDataFactory.Create(documentosCargados.ListDocumentos[x].path);
                                    var width = 575f;
                                    var height = 822f;
                                    if (img.GetWidth() < width)
                                        width = img.GetWidth();
                                    if (img.GetHeight() < height)
                                        height = img.GetHeight();
                                    pagina = doc.GetNumberOfPages();
                                    canvas = new PdfCanvas(doc.AddNewPage(pageDoc, PageSize.A4));
                                    PdfLayer pdflayer = new PdfLayer("", doc);
                                    canvas.BeginLayer(pdflayer);
                                    canvas.AddImage(img, 15, 15, width, false);
                                    canvas.EndLayer();
                                    canvas.SaveState();
                                    canvas.RestoreState();
                                    canvas.SaveState();
                                    canvas.RestoreState();
                                    pageDoc++;
                                }
                                else
                                {
                                    reader.Close();
                                    reader = new PdfReader(documentosCargados.ListDocumentos[x].path);
                                    doc1 = new PdfDocument(reader);
                                    pdf1 = new Document(doc1);
                                    for (var j = 1; j <= doc1.GetNumberOfPages(); j++)
                                    {
                                        page1 = doc1.GetPage(j);
                                        doc.AddPage(page1.CopyTo(doc));
                                        pageDoc++;
                                    }
                                }
                            }
                        }
                    }
                    //break;
                }
                doc.Close();
                writer.Close();
                reader.Close();
                fs.Close();

                filevirtual += nombrepdf;
                pdf.filename = pdfOut;
                pdf.virtualpath = filevirtual;
            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "Imprimir", ex, "");
            }
            return pdf;
        }
        public OutGetDocument expedientillo(double dependencia, double? producto, string folder, string rootPath, string baseURL, double cambio)
        {
            OutGetDocument doc = new OutGetDocument();
            string pdfOut = rootPath;
            string filevirtual = baseURL + "/Files/";
            FormularioSolicitudDocs solicitud = new FormularioSolicitudDocs();
            try
            {
                solicitud = new ProfileDAO().solicitud(folder);

            }
            catch (Exception ex)
            {
                return doc;
            }
            var nombrepdf = $@"{solicitud.RFC}-{folder}-Expedientillo_{DateTime.Now.ToString("MM-dd-yyyy")}.pdf";
            pdfOut += $@"\\{nombrepdf}";
            bool existe = File.Exists(pdfOut);
            if (existe)
            {
                File.Delete(pdfOut);
            }
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                OutDocumentoOriginacion data1 = new ProfileDAO().expedientillo(dependencia, producto);
                for (var k = 0; k < data1.ListDocumentos.Count(); k++)
                {
                    var file = data1.ListDocumentos.ElementAt(k);
                    var docExiste = new ProfileDAO().docExiste(file.codigo_doc, folder);
                    if (docExiste[0].Equals("0") || (int.Parse(docExiste[0]) > 0 && cambio == 1))
                    {
                        if (file.compra == 0)
                        {
                            documentoOriginacion(dependencia, producto, folder, file.codigo_doc, rootPath, baseURL,0);
                        }
                        else
                        {
                            if (solicitud.tipoSolicitud.IndexOf("COMPRA") > -1)
                                documentoCartera(dependencia, producto, folder, file.codigo_doc, rootPath, baseURL,0);
                        }
                        
                    }
                }
                OutDocumentoOriginacion data = new ProfileDAO().expedientillo(folder);
                if(data.ListDocumentos.Count > 0)
                {
                    PdfWriter writer = new PdfWriter(fs);
                    PdfDocument documento = new PdfDocument(writer);
                    PdfDocument doc1;
                    Document pdf1;
                    PdfPage page;
                    PdfCanvas canvas;
                    var docCargado = data.ListDocumentos.ElementAt(0);
                    Regex regex = new Regex("(.png|.jpeg|.jpg)$");
                    Match match = regex.Match(docCargado.path);
                    var isImage = false;
                    var i = 0;
                    var width = 575f;
                    var height = 822f;
                    if (match.Success)
                    {
                        ImageData img = ImageDataFactory.Create(docCargado.path);
                        if (img.GetWidth() < width)
                            width = img.GetWidth();
                        if (img.GetHeight() < height)
                            height = img.GetHeight();
                        var pagina = documento.GetNumberOfPages();
                        canvas = new PdfCanvas(documento.AddNewPage(pagina + 1, PageSize.A4));
                        PdfLayer pdflayer = new PdfLayer("", documento);
                        canvas.BeginLayer(pdflayer);
                        canvas.AddImage(img, 15, 15, width, false);
                        canvas.EndLayer();
                        canvas.SaveState();
                        canvas.RestoreState();
                        canvas.SaveState();
                        canvas.RestoreState();
                        i++;
                    }
                    width = 575f;
                    height = 822f;
                    docCargado = data.ListDocumentos.ElementAt(i);
                    PdfReader reader = new PdfReader(docCargado.path);
                    doc1 = new PdfDocument(reader);
                    for (; i < data.ListDocumentos.Count; i++)
                    {
                        if (i > 0)
                        {
                            docCargado = data.ListDocumentos.ElementAt(i);
                            match = regex.Match(docCargado.path);
                            if (match.Success)
                            {
                                isImage = true;
                                ImageData img = ImageDataFactory.Create(docCargado.path);
                                var pagina = documento.GetNumberOfPages();
                                if (img.GetWidth() < width)
                                    width = img.GetWidth();
                                if (img.GetHeight() < height)
                                    height = img.GetHeight();
                                canvas = new PdfCanvas(documento.AddNewPage(pagina + 1, PageSize.A4));
                                PdfLayer pdflayer = new PdfLayer("", documento);
                                canvas.BeginLayer(pdflayer);
                                canvas.AddImage(img, 15, 15, width, false);
                                canvas.EndLayer();
                                canvas.SaveState();
                                canvas.RestoreState();
                                isImage = true;
                                width = 575f;
                                height = 822f;
                            }
                            else
                            {
                                isImage = false;
                                reader.Close();
                                reader = new PdfReader(docCargado.path);
                                doc1 = new PdfDocument(reader);
                                pdf1 = new Document(doc1);
                            }
                        }
                        if (!isImage)
                        {
                            for (var j = 1; j <= doc1.GetNumberOfPages(); j++)
                            {
                                page = doc1.GetPage(j);
                                documento.AddPage(page.CopyTo(documento));
                            }
                        }
                    }
                    documento.Close();
                    writer.Close();
                    reader.Close();
                    fs.Dispose();
                    documento.Close();
                    writer.Close();
                    reader.Close();
                    fs.Close();
                    filevirtual += nombrepdf;
                    doc.filename = pdfOut;
                    doc.virtualpath = filevirtual;
                }
                else
                {
                    fs.Close();
                    doc.filename = "";
                    doc.virtualpath = "";
                }

            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "expedientillo", ex, "");
            }
            return doc;
        } 
        public OutGetDocument allDocuments(double dependencia, double? producto, string folder, string rootPath, string baseURL, double cambio)
        {
            OutGetDocument doc = new OutGetDocument();
            string pdfOut = rootPath;
            string filevirtual = baseURL + "/Files/";
            
            bool existe = File.Exists(pdfOut);
            FormularioSolicitudDocs solicitud;
            try
            {
                solicitud = new ProfileDAO().solicitud(folder);
            }
            catch (Exception ex)
            {
                return doc;
            }
            var nombrepdf = $@"{solicitud.RFC}-{folder}-ORGINACION_{DateTime.Now.ToString("MM-dd-yyyy")}.pdf"; 
            pdfOut += $@"\\{nombrepdf}";
            if (existe)
            {
                File.Delete(pdfOut);
            }
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                var docOriginacion = new ProfileDAO().allDocuments(dependencia, producto);
                for (var k = 0; k < docOriginacion.ListDocumentos.Count(); k++)
                {
                    var file = docOriginacion.ListDocumentos.ElementAt(k);
                    var docExiste = new ProfileDAO().docExiste(file.codigo_doc, folder);
                    if (docExiste[0].Equals("0") || (int.Parse(docExiste[0]) > 0 && cambio == 1))
                    {
                        if (file.compra == 0)
                        {
                            documentoOriginacion(dependencia, producto, folder, file.codigo_doc, rootPath, baseURL,0);
                        }
                        else
                        {
                            if(solicitud.tipoSolicitud.IndexOf("COMPRA") > -1)
                                documentoCartera(dependencia, producto, folder, file.codigo_doc, rootPath, baseURL,0);
                        }
                    }
                }
                OutDocumentoOriginacion data = new ProfileDAO().allDocuments(folder);
                if (data.ListDocumentos.Count > 0)
                {
                    PdfWriter writer = new PdfWriter(fs);
                    PdfDocument documento = new PdfDocument(writer);
                    PdfDocument doc1;
                    Document pdf1;
                    PdfPage page;
                    PdfCanvas canvas;
                    var docCargado = data.ListDocumentos.ElementAt(0);
                    Regex regex = new Regex("(.png|.jpeg|.jpg)$");
                    Match match = regex.Match(docCargado.path);
                    var isImage = false;
                    var i = 0;
                    var width = 575f;
                    var height = 822f;
                    if (match.Success)
                    {
                        ImageData img = ImageDataFactory.Create(docCargado.path);
                        if (img.GetWidth() < width)
                            width = img.GetWidth();
                        if (img.GetHeight() < height)
                            height = img.GetHeight();
                        var pagina = documento.GetNumberOfPages();
                        canvas = new PdfCanvas(documento.AddNewPage(pagina + 1, PageSize.A4));
                        PdfLayer pdflayer = new PdfLayer("", documento);
                        canvas.BeginLayer(pdflayer);
                        canvas.AddImage(img, 15, 15, width, false);
                        canvas.EndLayer();
                        canvas.SaveState();
                        canvas.RestoreState();
                        canvas.SaveState();
                        canvas.RestoreState();
                        i++;
                    }
                    width = 575f;
                    height = 822f;
                    PdfReader reader = new PdfReader(docCargado.path);
                    doc1 = new PdfDocument(reader);
                    for (; i < data.ListDocumentos.Count; i++)
                    {
                        if (i > 0)
                        {
                            docCargado = data.ListDocumentos.ElementAt(i);
                            match = regex.Match(docCargado.path);
                            if (match.Success)
                            {
                                isImage = true;
                                ImageData img = ImageDataFactory.Create(docCargado.path);
                                if (img.GetWidth() < width)
                                    width = img.GetWidth();
                                if (img.GetHeight() < height)
                                    height = img.GetHeight();
                                var pagina = documento.GetNumberOfPages();
                                canvas = new PdfCanvas(documento.AddNewPage(pagina + 1, PageSize.A4));
                                PdfLayer pdflayer = new PdfLayer("", documento);
                                canvas.BeginLayer(pdflayer);
                                canvas.AddImage(img, 15, 15, width, false);
                                canvas.EndLayer();
                                canvas.SaveState();
                                canvas.RestoreState();
                                isImage = true;
                                width = 575f;
                                height = 822f;
                            }
                            else
                            {
                                isImage = false;
                                reader.Close();
                                reader = new PdfReader(docCargado.path);
                                doc1 = new PdfDocument(reader);
                                pdf1 = new Document(doc1);
                            }
                        }
                        if (!isImage)
                        {
                            for (var j = 1; j <= doc1.GetNumberOfPages(); j++)
                            {
                                page = doc1.GetPage(j);
                                documento.AddPage(page.CopyTo(documento));
                            }
                        }
                    }
                    documento.Close();
                    writer.Close();
                    reader.Close();
                    fs.Dispose();
                    filevirtual += nombrepdf;
                    doc.filename = pdfOut;
                    doc.virtualpath = filevirtual;
                }
                else
                {
                    fs.Close();
                    doc.filename = "";
                    doc.virtualpath = "";
                }
            }
            catch (Exception ex)
            {
                fs.Close();
                LogHelper.WriteLog("Models", "ManageProfile", "allDocuments", ex, "");
            }
            return doc;
        }
        public OutGetDocument docPoliza(string folder, string rootPath, string baseURL)
        {
            string[] meses = {"ENERO","FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE"
                        ,"OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            OutGetDocument doc = new OutGetDocument();
            string pdfOut = rootPath;
            string filevirtual = baseURL + "/Files/";
            pdfOut += "\\Certificado_Individual_Poliza_" + folder + ".pdf";
            bool existe = File.Exists(pdfOut);
            if (existe)
            {
                File.Delete(pdfOut);
            }
            PdfReader reader = new PdfReader("D:\\PORTAL_MEXICO\\Certificado_Individual_Poliza.pdf");
            FileStream fs = new FileStream(pdfOut, FileMode.Create, FileAccess.Write, FileShare.None);
            PdfWriter writer = new PdfWriter(fs);
            PdfDocument pdf = new PdfDocument(reader, writer);
            PdfCanvas canvas;
            try
            {

                var beneficiarios = new ProfileDAO().aseguradoPoliza(folder);
                var familiares = new ProfileDAO().aseguradoFamiliarPoliza(folder);
                var formulario = new ProfileDAO().solicitud(folder);
                var nombre_asegurado = formulario.pNombre + " " + formulario.sNombre + " " + formulario.pApellido + " " + formulario.sApellido;
                var fecha = formulario.fchsolicitud.Split('/');
                fecha[0] = ((int.Parse(fecha[0]) + 2) > 10) ? int.Parse(fecha[0]) + 2 + "" : "0" + int.Parse(fecha[0]) + 2;
                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                float size = 9;
                PdfPage page = pdf.GetPage(1);
                canvas = new PdfCanvas(page);

                canvas.BeginText().SetFontAndSize(font, size).MoveText(470, 745).ShowText(formulario.idPoliza + "").EndText();

                canvas.BeginText().SetFontAndSize(font, size).MoveText(130, 630).ShowText(nombre_asegurado).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(85, 610).ShowText(formulario.RFC).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(360, 610).ShowText(fecha[0]).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(390, 610).ShowText(fecha[1]).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(430, 610).ShowText(fecha[2]).EndText();
                if (formulario.gender == "M")
                {
                    canvas.BeginText().SetFontAndSize(font, size).MoveText(545, 610).ShowText("X").EndText();
                }
                else
                {
                    canvas.BeginText().SetFontAndSize(font, size).MoveText(515, 610).ShowText("X").EndText();
                }
                canvas.BeginText().SetFontAndSize(font, size).MoveText(120, 598).ShowText(formulario.domicilioCalle).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(420, 598).ShowText(formulario.emailContacto).EndText();
                if (formulario.tipoPlan.IndexOf("Familiar") > -1)
                {
                    canvas.BeginText().SetFontAndSize(font, size).MoveText(315, 545).ShowText("X").EndText();

                }
                else
                {
                    canvas.BeginText().SetFontAndSize(font, size).MoveText(50, 545).ShowText("X").EndText();
                }
                var bnfRegis = 0;
                size = 8;
                if (familiares.listBeneficiario.Count > 0)
                {
                    var x = 50;
                    var x1 = 190;
                    var x2 = 230;
                    var x3 = 255;
                    var x4 = 275;
                    var y = 240;
                    var y1 = 240;
                    var y2 = 240;
                    var y3 = 240;
                    var y4 = 240;
                    for (var i = 0; i < familiares.listBeneficiario.Count; i++)
                    {
                        if (i == 0)
                        {
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(40, 282).ShowText(familiares.listBeneficiario[i].nombre).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(280, 282).ShowText(familiares.listBeneficiario[i].parentesco).EndText();
                            bnfRegis++;
                        }
                        else if (i > 0 && i < 5)
                        {
                            if (i > 1)
                            {
                                y = y + 10;
                                y1 = y1 + 10;
                                y2 = y2 + 10;
                                y3 = y3 + 10;
                                y4 = y4 + 10;
                            }
                            var d = familiares.listBeneficiario[i].fecha_ncto.Split('/');
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x, y).ShowText(familiares.listBeneficiario[i].nombre).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x1, y1).ShowText(familiares.listBeneficiario[i].parentesco).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x2, y2).ShowText(d[0]).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x3, y3).ShowText(d[1]).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x4, y4).ShowText(d[2]).EndText();
                        }
                        else if (i >= 5 && i < 8)
                        {
                            if (i == 5)
                            {
                                x = 325;
                                x1 = 465;
                                x2 = 505;
                                x3 = 530;
                                x4 = 550;
                                y = 240;
                                y1 = 240;
                                y2 = 240;
                                y3 = 240;
                                y4 = 240;
                            }
                            if (i > 5)
                            {
                                y = y + 10;
                                y1 = y1 + 10;
                                y2 = y2 + 10;
                                y3 = y3 + 10;
                                y4 = y4 + 10;
                            }
                            var d = familiares.listBeneficiario[i].fecha_ncto.Split('/');
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x, y).ShowText(familiares.listBeneficiario[i].nombre).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x1, y1).ShowText(familiares.listBeneficiario[i].parentesco).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x2, y2).ShowText(d[0]).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x3, y3).ShowText(d[1]).EndText();
                            canvas.BeginText().SetFontAndSize(font, size).MoveText(x4, y4).ShowText(d[2]).EndText();
                        }
                    }

                }
                canvas.SaveState();
                canvas.RestoreState();

                

                if (beneficiarios.listBeneficiario.Count > 0)
                {
                    for (var i = 0; i < familiares.listBeneficiario.Count; i++)
                    {
                        if (bnfRegis > 0)
                        {
                            page = pdf.GetPage(1);
                            canvas = new PdfCanvas(page);
                        }
                        else
                        {
                            bnfRegis++;
                        }
                        canvas.BeginText().SetFontAndSize(font, size).MoveText(40, 282).ShowText(familiares.listBeneficiario[i].nombre).EndText();
                        canvas.BeginText().SetFontAndSize(font, size).MoveText(280, 282).ShowText(familiares.listBeneficiario[i].parentesco).EndText();
                        canvas.SaveState();
                        canvas.RestoreState();
                    }
                }

                page = pdf.GetPage(2);
                canvas = new PdfCanvas(page);
                size = 8.5f;
                canvas.BeginText().SetFontAndSize(font, size).MoveText(185, 71).ShowText(fecha[0]).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(220, 71).ShowText(fecha[1]).EndText();
                canvas.BeginText().SetFontAndSize(font, size).MoveText(257, 71).ShowText(fecha[2]).EndText();
                size = 10;
                canvas.BeginText().SetFontAndSize(font, size).MoveText(350, 71).ShowText(formulario.idPoliza + "").EndText();
                canvas.SaveState();
                canvas.RestoreState();
                page = pdf.GetPage(3);
                canvas = new PdfCanvas(page);
                canvas.SaveState();
                canvas.RestoreState();
                filevirtual += "\\Certificado_Individual_Poliza_" + folder + ".pdf";
                doc.filename = pdfOut;
                doc.virtualpath = filevirtual;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "docPoliza", ex, "");
            }
            finally
            {
                pdf.Close();
                writer.Close();
                fs.Close();
                reader.Close();
            }
            return doc;
        }     
        public FormularioSolicitud getIdSolicitud(string folder)
        {
            FormularioSolicitud data = new FormularioSolicitud();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.getIdSolicitud(folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "getIdSolicitud", ex, "");
            }
            return data;
        }
        public OutOfferPolizas getOfertasSeguros(string folder, double vlr_solicitado, double vlr_maximo, double usuario)
        {
            OutOfferPolizas outOffer = new OutOfferPolizas();
            InsurancePolicyOffer offer = new InsurancePolicyOffer();
            OutPolizas poliza = new OutPolizas();
            List<InsurancePolicyOffer> list = new List<InsurancePolicyOffer>();
            bool valutf8 = true;
            try
            {
                ProfileDAO dao = new ProfileDAO();
                poliza = dao.getOfertasSeguros(folder, vlr_solicitado, vlr_maximo, usuario);
                valutf8 = true;

            }
            catch (Exception ex)
            {
                valutf8 = false;
                poliza.oferta = "0|Sin Ofertas|0.00;";
                poliza.msg.errorCode = "88";
                poliza.msg.errorMessage = "No se encontraron Polizas Configuradas.";
                LogHelper.WriteLog("Models", "ManageProfile", "getIdSolicitud", ex, "");
            }
            var offerList = poliza.oferta.Split(';');
            foreach (var item in offerList)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var detail = item.Split('|');
                    if (detail.Length != 3)
                    {
                        if (double.Parse(detail[0]) > 0)
                        {
                            offer = new InsurancePolicyOffer();
                            offer.PlanCode = double.Parse(detail[0]);
                            //Adicion para codificar correctamente las cadenas recibidas del WS, Danny Romero Lozano, 23/07/2018
                            if (valutf8)
                            {
                                byte[] data = Encoding.Default.GetBytes(detail[1].ToString());
                                string output = Encoding.UTF8.GetString(data);
                                offer.PlanName = output;
                            }
                            else
                            {
                                offer.PlanName = detail[1].ToString();
                            }
                            //Fin Adicion
                            offer.PolicyValue = double.Parse(detail[2]);
                            offer.Principal = int.Parse(detail[3]);
                            offer.YearsValidity = int.Parse(detail[4]);
                            offer.CovertureValue = double.Parse(detail[5]);
                            /*offer.AllowRelatives = int.Parse(detail[4]);
                            offer.MonthlyValue = double.Parse(detail[8]);*/
                            list.Add(offer);
                        }
                    }
                }
            }
            outOffer.ofertas = list;
            outOffer.msg = poliza.msg;
            return outOffer;
        }
        public Response aceptarOferta(ref FormularioSolicitud doc, double usuario)
        {
            Response poliza = new Response();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                var actualiza = dao.actuliazaFolio(ref doc, usuario);
                if (actualiza.errorCode == "0")
                {
                    if (doc.tiene_seguro == 1)
                    {
                        var data = dao.creaPoliza(ref doc, usuario);
                        if (data[0] == "0")
                        {
                            doc.id_poliza = double.Parse(data[2]);

                            dao.updFormularioPoliza(doc.id_poliza, doc.folderNumber);
                            arbolCarpetas carpetas = dao.arbolCarpetas();
                            creaCarpetas(carpetas, doc.folderNumber);
                        }
                        poliza.errorCode = data[0];
                        poliza.errorMessage = data[1];
                    }
                    else
                    {
                        poliza = actualiza;
                    }
                }
                else
                {
                    poliza = actualiza;
                }
            }
            catch (Exception e)
            {
                poliza.errorCode = "88";
                poliza.errorMessage = e.ToString();
                LogHelper.WriteLog("Models", "ManageProfile", "aceptarOferta", e, "");
            }
            return poliza;
        }
        public bool creaCarpetas(arbolCarpetas carpetas, string folder) {
            string filename = carpetas.ruta_raiz+folder;

            try
            {
                if (!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }
                if (Directory.Exists(filename))
                {
                    filename += "\\";
                    if (!"".Equals(carpetas.carpeta1) && carpetas.carpeta1 != null)
                    {
                        Directory.CreateDirectory(filename+ carpetas.carpeta1);
                    }
                    if (!"".Equals(carpetas.carpeta2) && carpetas.carpeta2 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta2);
                    }
                    if (!"".Equals(carpetas.carpeta3) && carpetas.carpeta3 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta3);
                    }
                    if (!"".Equals(carpetas.carpeta4) && carpetas.carpeta4 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta4);
                    }
                    if (!"".Equals(carpetas.carpeta5) && carpetas.carpeta5 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta5);
                    }
                    if (!"".Equals(carpetas.carpeta6) && carpetas.carpeta6 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta6);
                    }
                    if (!"".Equals(carpetas.carpeta7) && carpetas.carpeta7 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta7);
                    }
                    if (!"".Equals(carpetas.carpeta8) && carpetas.carpeta8 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta8);
                    }
                    if (!"".Equals(carpetas.carpeta9) && carpetas.carpeta9 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta9);
                    }
                    if (!"".Equals(carpetas.carpeta10) && carpetas.carpeta10 != null)
                    {
                        Directory.CreateDirectory(filename + carpetas.carpeta10);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "creaCarpetas", e, "");
                return false;
            }
            return true;
        }
        public string beneficiariosPoliza(string folder)
        {
            string data = string.Empty;
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.beneficiariosPoliza(folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "beneficiariosPoliza", ex, "");
            }
            return data;
        }
        public Response guardarBeneficiarioPoliza(ref InBeneficiarioPoliza beneficiario, double poliza)
        {
            Response data = new Response();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.guardarBeneficiarioPoliza(ref beneficiario, poliza);
            }
            catch (Exception ex)
            {
                data.errorCode = "-10";
                data.errorMessage = "No se pudo agregar el Beneficiaerio " + ex.Message;
            }
            return data;
        }
        public bool sendMailAuxiliar(double dependencia, double? producto, string folder, string rootPath, string baseURL, double sucursal, double? expediente, string cuerpo)
        {
            var emailAuxiliar = new ProfileDAO().getCorreoAuxiliarSuperior(folder, sucursal);
            if ("".Equals(emailAuxiliar[0]) && "".Equals(emailAuxiliar[1]))
            {
                return false;
            }
            var pdf = new string[2];
            if (expediente == 0)
            {
                pdf[0] = allDocuments(dependencia, producto, folder, rootPath, baseURL,0).filename;
                pdf[1] = expedientillo(dependencia, producto, folder, rootPath, baseURL, 0).filename;
            }
            else {
                pdf[0] = findExpCompleto(folder).ListDocumentos[0].path;
            }
            if ("".Equals(pdf[0]) || pdf[0] == null)
            {
                return false;
            }
            LogHelper.WriteLog("Models", "ManageProfile", "sendMailAuxiliar", "", cuerpo, folder);

            return new Helper.Utilities().sendEmailAuxliar(folder, emailAuxiliar, pdf, cuerpo);
        }
        public double entidadAbreviatura(string abreviatura)
        {
            double data = 0;
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getEntidadAberv(abreviatura);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "entidadAbreviatura", ex, abreviatura);
            }
            return data;
        }
        public List<OutComentarios> getComentarios(string folder)
        {
            List<OutComentarios> data = new List<OutComentarios>();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.getComentarios(folder);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "getComentarios", ex, "");
            }
            return data;
        }
        public OutDashBoard getDashBoard()
        {
            OutDashBoard data = new OutDashBoard();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.getDashBoard();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "getDashBoard", ex, "");
            }
            return data;
        }
        /*Danny 2019 10*/
        public double findEntidad(string entidad)
        {
            double codigo = 0;
            try
            {
                ProfileDAO dao = new ProfileDAO();
                codigo = dao.findEntidad(entidad);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "findEntidad", ex, "");
            }
            return codigo;
        }
        public double findMunicipio(string municipio)
        {
            double codigo = 0;
            try
            {
                ProfileDAO dao = new ProfileDAO();
                codigo = dao.findMunicipio(municipio);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageProfile", "findMunicipio", ex, "");
            }
            return codigo;
        }
        public Response cancelarFolio(string folder)
        {
            Response data = new Response();
            try
            {
                ProfileDAO dao = new ProfileDAO();
                data = dao.cancelarFolio(folder);
            }
            catch (Exception ex)
            {
                data.errorCode = "88";
                data.errorMessage = "No se pudo actualizar el estado. Favor intente más tarde";
                LogHelper.WriteLog("Models", "ManageProfile", "cancelarFolio", ex, "");
            }
            return data;
        }
    }
}
