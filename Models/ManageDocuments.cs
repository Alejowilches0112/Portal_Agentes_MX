using DAO;
using Entities;
using Helper;
using System;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;
namespace Models
{
    public class ManageDocuments
    {
        public OutDocuments GetDocuments()
        {
            OutDocuments data = new OutDocuments();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetDocuments();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetDocuments", ex, "");
            }
            return data;

        }

        public OutUploadDocuments GetUploadDocuments(string documentID)
        {
            OutUploadDocuments data = new OutUploadDocuments();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetUploadDocuments(documentID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetUploadDocuments", ex, "");
            }
            return data;
        }

        public Response LogUploadDocuments(InLogDocuments input)
        {
            Response data = new Response();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.LogUploadDocuments(input);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "LogUploadDocuments", ex, "");
            }
            return data;

        }

        public bool CargarArchivo(ref documents documents, HttpPostedFileBase hpf)
        {
            try
            {
                
                documents.nombreDoc = documents.nombreDoc.Replace("Ñ", "N").Replace("ñ", "n");
                documents.nombreDoc = documents.nombreDoc.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                documents.nombreDoc = documents.nombreDoc.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");

                Match match = regex.Match(documents.nombreDoc);

                String extension = "";

                if (match.Success)
                {
                    extension = match.Value;
                }
                
                documents.nombreDoc = regex.Replace(documents.nombreDoc, "_plantilla" + extension);
                documents.nombreDoc = documents.nombreDoc.Replace(" ", "_");
                documents.path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\PLANTILLAS_DOCS\\" + documents.dependencia;
                documents.path += (documents.producto != null) ? "\\"+documents.producto : "";
                if (!Directory.Exists(documents.path))
                    Directory.CreateDirectory(documents.path);
                if (File.Exists(documents.path + "\\" + documents.nombreDoc))
                    File.Delete(documents.path + "\\" + documents.nombreDoc);
                /*File.WriteAllBytes(documents.path + "\\" + documents.nombreDoc, Convert.FromBase64String(documents.file));
                    if (!File.Exists(documents.path + "\\" + documents.nombreDoc))
                    {
                        documents.msg.errorCode = "300";
                        documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                        LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, documents.nombre);
                        return false;
                    }*/
                hpf.SaveAs(documents.path + "\\" + documents.nombreDoc);
                if (!File.Exists(documents.path + "\\" + documents.nombreDoc))
                {
                    documents.msg.errorCode = "300";
                    documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, documents.path + "\\" + documents.nombreDoc);
                    return false;
                }
                documents.file = null;
                documents.path = documents.path + "\\" + documents.nombreDoc;
                return true;
            }
            catch (Exception e)
            {
                documents.msg.errorCode = "300";
                documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", e, documents.nombre);
                return false;
            }
        }    
        public bool CargarArchivoOriginacion(ref DocumentoOriginacion documents, HttpPostedFileBase hpf)
        {
            try
            {

                documents.nombreDoc = documents.nombreDoc.Replace("Ñ", "N").Replace("ñ", "n");
                documents.nombreDoc = documents.nombreDoc.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                documents.nombreDoc = documents.nombreDoc.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");

                Match match = regex.Match(documents.nombreDoc);

                String extension = "";

                if (match.Success)
                {
                    extension = match.Value;
                }
                documents.path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\DOCUMENTOS_ORIGINACION\"+ documents.folder + "\\"+ documents.dependencia;
                documents.path += (documents.producto != null) ? "\\"+documents.producto : "";
                var firma = (documents.firma == 1) ? "_firma" : "";
                var expediente = (documents.expedienteCompleto == 1) ? "_expediente_completo" : ""; 
                documents.nombreDoc = regex.Replace(documents.nombreDoc, "_"+ firma + expediente + extension);
                documents.nombreDoc = documents.nombreDoc.Replace(" ", "_");
                if (!Directory.Exists(documents.path))
                    Directory.CreateDirectory(documents.path);
                if (File.Exists(documents.path+"\\"+documents.nombreDoc))
                    File.Delete(documents.path + "\\" + documents.nombreDoc);

                var savedFileName = Path.Combine(documents.path, documents.nombreDoc);
                hpf.SaveAs(savedFileName);
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", new Exception(), "Guarda Expediente Completo");
                if (!File.Exists(savedFileName))
                {
                    documents.msg.errorCode = "300";
                    documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, savedFileName);
                    return false;
                }
                documents.file = null;
                documents.path = savedFileName;
                ProfileDAO dao = new ProfileDAO();
                if (documents.firma == 1)
                {
                    var existe = dao.docExiste(documents.codigo_doc,documents.folder);
                    if (existe[0] == "0")
                    {
                        documents.msg = dao.cargaDocumentosOriginacion(ref documents);
                    }
                    else
                    {
                        var actualiza = new ManageProfile().updDocFirma(documents.codigo_doc, double.Parse(existe[2]), documents.folder, documents.path, existe[1]);
                        if (actualiza)
                        {
                            documents.msg.errorCode = "0";
                            documents.msg.errorMessage = "Documento Actualizado con Exito";
                        }
                        else
                        {
                            documents.msg.errorCode = "88";
                            documents.msg.errorMessage = "Error actualizando el documento. Por favor intente más tarde";
                        }
                        //documents.msg = dao.actualizaDocOriginacion(double.Parse(existe[2]), documents.folder, documents.nombreDoc, documents.path);
                    }
                }
                else
                {
                    documents.msg = dao.cargaDocumentosOriginacion(ref documents);
                }
                return true;
            }
            catch (Exception e)
            {
                documents.msg.errorCode = "320";
                documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", e, documents.nombreDoc);
                return false;
            }
        }
        public bool CargarArchivoOriginacionCompra(ref DocumentoOriginacion documents, HttpPostedFileBase hpf)
        {
            try
            {

                documents.nombreDoc = documents.nombreDoc.Replace("Ñ", "N").Replace("ñ", "n");
                documents.nombreDoc = documents.nombreDoc.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                documents.nombreDoc = documents.nombreDoc.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                documents.nombre_cartera = documents.nombreDoc.Replace("Ñ", "N").Replace("ñ", "n");
                documents.nombre_cartera = documents.nombreDoc.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                documents.nombre_cartera = documents.nombreDoc.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");

                Match match = regex.Match(documents.nombreDoc);

                String extension = "";

                if (match.Success)
                {
                    extension = match.Value;
                }
                var path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\DOCUMENTOS_ORIGINACION\\" + documents.folder + "\\" + documents.dependencia;
                path += (documents.producto != null) ? "\\" + documents.producto : "";
                var firma = (documents.firma == 1) ? "_firma" : "";
                var expediente = (documents.expedienteCompleto == 1) ? "_expediente_completo" : "";
                documents.nombreDoc = regex.Replace(documents.nombreDoc, "_" + firma + expediente + extension);
                documents.nombreDoc = documents.nombreDoc.Replace(" ", "_");

                documents.nombre_cartera = regex.Replace(documents.nombre_cartera, "_" + firma + expediente + extension);
                documents.nombre_cartera = documents.nombre_cartera.Replace(" ", "_");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (File.Exists(path + "\\" + documents.nombreDoc))
                    File.Delete(path + "\\" + documents.nombreDoc);
                var savedFileName = Path.Combine(path, documents.nombreDoc);
                hpf.SaveAs(savedFileName);
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", new Exception(), "Guarda Expediente Completo");
                if (!File.Exists(savedFileName))
                {
                    documents.msg.errorCode = "300";
                    documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, savedFileName);
                    return false;
                }
                documents.file = null;
                documents.path = savedFileName;
                ProfileDAO dao = new ProfileDAO();

                        var actualiza = new ManageProfile().updDocFirmaCompra(documents.codigo_doc, (double)(documents.codigo), documents.folder, savedFileName, documents.path, documents.nombre_cartera);
                        if (actualiza)
                        {
                            documents.msg.errorCode = "0";
                            documents.msg.errorMessage = "Documento Actualizado con Exito";
                        }
                        else
                        {
                            documents.msg.errorCode = "88";
                            documents.msg.errorMessage = "Error actualizando el documento. Por favor intente más tarde";
                        }
                        //documents.msg = dao.actualizaDocOriginacion(double.Parse(existe[2]), documents.folder, documents.nombreDoc, documents.path);
                return true;
            }
            catch (Exception e)
            {
                documents.msg.errorCode = "320";
                documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", e, documents.nombreDoc);
                return false;
            }
        }
        public bool CargarExpedienteCompleto(ref DocumentoOriginacion documents, HttpPostedFileBase hpf)
        {
            try
            {

                documents.nombreDoc = documents.nombreDoc.Replace("Ñ", "N").Replace("ñ", "n");
                documents.nombreDoc = documents.nombreDoc.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                documents.nombreDoc = documents.nombreDoc.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                Regex regex = new Regex("(.pdf|.png|.jpeg|.jpg)$");

                Match match = regex.Match(documents.nombreDoc);

                String extension = "";

                if (match.Success)
                {
                    extension = match.Value;
                }
                documents.path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\DOCUMENTOS_ORIGINACION \\"+ documents.folder + "\\" + documents.dependencia;
                documents.path += (documents.producto != null) ? "\\" + documents.producto : "";
                var firma = (documents.firma == 1) ? "_firma" : "";
                var expediente = (documents.expedienteCompleto == 1) ? "_expediente_completo" : "";
                documents.nombreDoc = regex.Replace(documents.nombreDoc, "_" + firma + expediente + extension);
                documents.nombreDoc = documents.nombreDoc.Replace(" ", "_");
                if (!Directory.Exists(documents.path))
                    Directory.CreateDirectory(documents.path + "\\" + documents.folder + "\\" + documents.dependencia);

                if (File.Exists(documents.path + "\\" + documents.nombreDoc))
                    File.Delete(documents.path + "\\" + documents.nombreDoc);
                var savedFileName = Path.Combine(documents.path, documents.nombreDoc);
                hpf.SaveAs(savedFileName);
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", new Exception(), "Guarda Expediente Completo");
                if (!File.Exists(savedFileName))
                {
                    documents.msg.errorCode = "300";
                    documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, savedFileName);
                    return false;
                }
                documents.file = null;
                documents.path = savedFileName;
                ProfileDAO dao = new ProfileDAO();
                    var existe = dao.docExisteExpendiente(documents.folder);
                    if (existe[0] == "0")
                    {
                        documents.msg = dao.cargaDocumentosOriginacion(ref documents);
                    }
                    else
                    {
                        documents.msg = dao.actualizaDocOriginacion(double.Parse(existe[2]), documents.folder, documents.nombreDoc, documents.path);
                    }
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", new Exception(), "Guarda Expediente Completo BD");
                return true;
            }
            catch (Exception e)
            {
                documents.msg.errorCode = "320";
                documents.msg.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", e, documents.nombreDoc);
                return false;
            }
        }
        public OutParamSectorGuias GetddSectorGuias()
        {
            OutParamSectorGuias data = new OutParamSectorGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetddSectorGuias();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetddSectorGuias", ex, "");
            }
            return data;
        }
        public OutParamSectorTablas GetddSectorTablas()
        {
            OutParamSectorTablas data = new OutParamSectorTablas();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetddSectorTablas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetddSectorTablas", ex, "");
            }
            return data;
        }
        public OutParamGuias GetGuiasAs()
        {
            OutParamGuias data = new OutParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetGuiasAs();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetGuias", ex, "");
            }
            return data;
        }
        public OutParamGuias GetGuias()
        {
            OutParamGuias data = new OutParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetGuias();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetGuias", ex, "");
            }
            return data;
        }
        public ParamGuias GetIdGuias(double codigo)
        {
            ParamGuias data = new ParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetIdGuias(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetGuias", ex, "");
            }
            return data;
        }
        public OutParamGuias GetTablasAs()
        {
            OutParamGuias data = new OutParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetTablasAs();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetTablas", ex, "");
            }
            return data;
        }
        public OutParamGuias GetTablas()
        {
            OutParamGuias data = new OutParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetTablas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetTablas", ex, "");
            }
            return data;
        }
        public ParamGuias GetIdTablas(double codigo)
        {
            ParamGuias data = new ParamGuias();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GetIdTablas(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageDocuments", "GetGuias", ex, "");
            }
            return data;
        }
        public Response GuardarArchvio (ref ParamGuias documento, string tabla)
        {
            Response data = new Response();
            var sector = documento.nsector;
            //var sector = (documento.sector == 1) ? "EDUCACION" : ((documento.sector == 2) ? "GOBIERNO" : "SALUD");
            documento.nombre = documento.nombre.Replace(" ", "_");
            documento.path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\"+tabla +"\\"+sector;
            try
            {
                if (!Directory.Exists(documento.path))
                    Directory.CreateDirectory(documento.path);
                if (File.Exists(documento.path + "\\" + documento.nombre))
                    File.Delete(documento.path + "\\" + documento.nombre);
                File.WriteAllBytes(documento.path + "\\" + documento.nombre, Convert.FromBase64String(documento.file));
                if (!File.Exists(documento.path + "\\" + documento.nombre))
                {
                    data.errorCode = "310";
                    data.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                    LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, documento.nombre);
                    return data;
                }
                documento.file = null;
                documento.path +="\\" +documento.nombre;
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.GuardarArchvio(ref documento, tabla);
            }
            catch (Exception e)
            {
                data.errorCode = "320";
                data.errorMessage = "Error Al subir el Archivo Intente Nuevamente. "+e.ToString();
                LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", e, documento.nombre);
                return data;
            }
            return data;
        }
        public Response EditarArchivo(ref ParamGuias documento, string tabla)
        {
            Response data = new Response();
            var sector = documento.nsector;
            //var sector = (documento.sector == 1) ? "EDUCACION" : ((documento.sector == 2) ? "GOBIERNO" : "SALUD");
            documento.nombre = documento.nombre.Replace(" ", "_");
            documento.path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\" + tabla + "\\" + sector;
            try
            {
                if (documento.file != "" && documento.file != null)
                {
                    if (!Directory.Exists(documento.path))
                        Directory.CreateDirectory(documento.path);
                    if (File.Exists(documento.path + "\\" + documento.nombre))
                        File.Delete(documento.path + "\\" + documento.nombre);

                    File.WriteAllBytes(documento.path + "\\" + documento.nombre, Convert.FromBase64String(documento.file));
                    if (!File.Exists(documento.path + "\\" + documento.nombre))
                    {
                        data.errorCode = "310";
                        data.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                        LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, documento.nombre);
                        return data;
                    }
                    documento.file = null;
                    documento.path += "\\" + documento.nombre;
                }
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.EditarArchvio(ref documento, tabla);
            }
            catch (Exception e)
            {
                data.errorCode = "320";
                data.errorMessage = "Error Al subir el Archivo Intente Nuevamente. " + e.ToString();
                LogHelper.WriteLog("Models", "ManageDocuments", "EditarArchivo", e, documento.nombre);
                return data;
            }
            return data;
        }
        public Response EliminarArchvio(double codigo, string tabla)
        {
            Response data = new Response();
            try
            {
                DocumentsDAO dao = new DocumentsDAO();
                data = dao.EliminarArchvio(codigo, tabla);
            }
            catch (Exception e)
            {
                data.errorCode = "320";
                data.errorMessage = "Error Al Eliminar. " + e.ToString();
                LogHelper.WriteLog("Models", "ManageDocuments", "EditarArchivo", e, "");
                return data;
            }
            return data;
        }
    }
}
