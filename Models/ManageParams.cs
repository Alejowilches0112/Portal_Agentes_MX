using DAO;
using Entities;
using Helper;
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Configuration;

namespace Models
{
    public class ManageParams
    {
        //Dias Expiracion
        public OutParamDiasExp getDiasExp()
        {
            OutParamDiasExp data = new OutParamDiasExp();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDiasExp();
            }
            catch (Exception ex)
            {
                //escribir en el log 
                LogHelper.WriteLog("Models", "ManageParams", "getDiasExp", ex, "");
            }
            return data;
        }
        public OutParamDiasExp getIdDiasExp(double dias)
        {
            OutParamDiasExp data = new OutParamDiasExp();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdDiasExp(dias);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdDiasExp" + "", ex, "");
            }
            return data;
        }
        public Response saveDiasExp(double dias)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveDiasExp(dias);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveDiasExp", ex, "");
            }
            return data;
        }
        /* Dependencias */
        public OutParamDependencia getDependencias()
        {
            OutParamDependencia data = new OutParamDependencia();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDependencias();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDependencias", ex, "");
            }
            return data;
        }
        public OutParamDependencia getDependenciasActivas()
        {
            OutParamDependencia data = new OutParamDependencia();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDependenciasActivas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDependencias", ex, "");
            }
            return data;
        }
        public Response saveDependencia(double codigo_dep, string dependencia, string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveDependencia(codigo_dep, dependencia, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveDependencia", ex, "");
            }
            return data;
        }
        public Response updDependencia(double secuencia, string dependencia, string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updDependencia(secuencia, dependencia, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveDependencia", ex, "");
            }
            return data;
        }
        public Response deleteDependencia(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteDependencia(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteDependencia", ex, "");
            }
            return data;
        }
        public OutParamDependencia loadDependencia(double secuencia)
        {
            OutParamDependencia data = new OutParamDependencia();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.loadDependencia(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "loadDependencia", ex, "");
            }
            return data;
        }
        /* Tipo de solicitud */
        public OutParamTipo getTipoSolicitud()
        {
            OutParamTipo data = new OutParamTipo();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getTipoSolicitud();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getTipoSolicitud", ex, "");
            }
            return data;
        }
        public Response saveTipoSolicitud(string tipoSolicitud)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveTipoSolicitud(tipoSolicitud);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveTipoSolicitud", ex, "");
            }
            return data;
        }
        public Response updTipoSolicitud(double secuencia, string tipoSolicitud)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updTipoSolicitud(secuencia, tipoSolicitud);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updTipoSolicitud", ex, "");
            }
            return data;
        }
        public Response deleteTipoSolicitud(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteTipoSolicitud(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteTipoSolicitud", ex, "");
            }
            return data;
        }
        public OutParamTipo getIdTipoSolicitud(double secuencia)
        {
            OutParamTipo data = new OutParamTipo();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdTipoSolicitud(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdTipoSolicitud", ex, "");
            }
            return data;
        }
        /* Periodos */
        public OutParamPeriodos getPeriodos()
        {
            OutParamPeriodos data = new OutParamPeriodos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPeriodos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPeriodos", ex, "");
            }
            return data;
        }
        public Response savePeriodos(string periodo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.savePeriodos(periodo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "savePeriodos", ex, "");
            }
            return data;
        }
        public Response updPeriodos(double secuencia, string periodo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updPeriodos(secuencia, periodo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updPeriodos", ex, "");
            }
            return data;
        }
        public Response deletePeridos(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deletePeridos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deletePeridos", ex, "");
            }
            return data;
        }
        public OutParamPeriodos getIdPeriodos(double secuencia)
        {
            OutParamPeriodos data = new OutParamPeriodos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdPeriodos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdPeriodos", ex, "");
            }
            return data;
        }
        /* Plazos */
        public OutParamPlazosPeriodos getPlazosPeriodo(double periodoAplicable)
        {
            OutParamPlazosPeriodos data = new OutParamPlazosPeriodos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPlazosPeriodo(periodoAplicable);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPeriodos", ex, "");
            }
            return data;
        }
        public OutParamPlazos getPlazos()
        {
            OutParamPlazos data = new OutParamPlazos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPlazos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPeriodos", ex, "");
            }
            return data;
        }
        public OutParamPlazos getIdPlazo(double secuencia)
        {
            OutParamPlazos data = new OutParamPlazos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdPlazo(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPeriodos", ex, "");
            }
            return data;
        }
        public Response savePlazo(double? periodo, string plazos)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.savePlazo(periodo, plazos);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "savePlazo", ex, "");
            }
            return data;
        }
        public Response updPlazo(double secuencia, string plazo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updPlazo(secuencia, plazo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updPlazo", ex, "");
            }
            return data;
        }
        public Response deletePlazo(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteplazo(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deletePlazo", ex, "");
            }
            return data;
        }
        /* Puestos */
        public OutParamPuestoSector getPuestoSector(double sector)
        {
            OutParamPuestoSector data = new OutParamPuestoSector();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPuestosSector(sector);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPeriodos", ex, "");
            }
            return data;
        }
        public OutParamPuesto getPuestos()
        {
            OutParamPuesto data = new OutParamPuesto();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPuestos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPuestos", ex, "");
            }
            return data;
        }
        public OutParamPuesto getIdPuesto(double secuencia)
        {
            OutParamPuesto data = new OutParamPuesto();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdPuestos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdPuesto", ex, "");
            }
            return data;
        }
        public Response savePuestos(double sector, string puesto)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.savePuesto(sector, puesto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "savePuestos", ex, "");
            }
            return data;
        }
        public Response updPuestos(double secuencia, string puesto)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updPuesto(secuencia, puesto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updPuesto", ex, "");
            }
            return data;
        }
        public Response deletePuestos(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deletePuesto(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deletePuesto", ex, "");
            }
            return data;
        }
        /* Destino Credito */
        public OutParamDestinoCred getDestinoCredito()
        {
            OutParamDestinoCred data = new OutParamDestinoCred();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDestinoCredito();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDestinoCredito", ex, "");
            }
            return data;
        }
        public Response saveDestinoCredito(string destinio)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.savedestinoCredito(destinio);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "savedestinoCredito", ex, "");
            }
            return data;
        }
        public Response updDestinoCredito(double secuencia, string destino)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updDestinoCredito(secuencia, destino);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updDestinoCredito", ex, "");
            }
            return data;
        }
        public Response deleteDestinoCredito(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteDestinoCredito(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deletePeridos", ex, "");
            }
            return data;
        }
        public OutParamDestinoCred getIdDestinoCredito(double secuencia)
        {
            OutParamDestinoCred data = new OutParamDestinoCred();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdDestinoCredito(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdDestinoCredito", ex, "");
            }
            return data;
        }
        /* Empresa Telefonica */
        public OutParamEmpresatelefonica getEmpresaTelefonica()
        {
            OutParamEmpresatelefonica data = new OutParamEmpresatelefonica();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getEmpresaTelefonica();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getEmpresaTelefonica", ex, "");
            }
            return data;
        }
        public Response saveEmpresaTelefonica(string empresa)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveEmpresaTelefonica(empresa);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveEmpresaTelefonica", ex, "");
            }
            return data;
        }
        public Response updEmpresaTelefonica(double secuencia, string empresa)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updEmpresaTelefonica(secuencia, empresa);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updEmpresaTelefonica", ex, "");
            }
            return data;
        }
        public Response deleteEmpresaTelefonica(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteEmpresaTelefonica(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteEmpresaTelefonica", ex, "");
            }
            return data;
        }
        public OutParamEmpresatelefonica getIdEmpresaTelefonica(double secuencia)
        {
            OutParamEmpresatelefonica data = new OutParamEmpresatelefonica();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdEmpresaTelefonica(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdEmpresaTelefonica", ex, "");
            }
            return data;
        }
        /*Estado Civil*/
        public OutParamEstadoCivil getEstadoCivil()
        {
            OutParamEstadoCivil data = new OutParamEstadoCivil();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getEstadoCivil();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getEstadoCivil", ex, "");
            }
            return data;
        }
        public Response saveEstadoCivil(string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveEstadoCivil(estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveEstadoCivil", ex, "");
            }
            return data;
        }
        public Response updEstadoCivil(double secuencia, string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updEstadoCivil(secuencia, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updEstadoCivil", ex, "");
            }
            return data;
        }
        public Response deleteEstadoCivil(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteEstadoCivil(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteEstadoCivil", ex, "");
            }
            return data;
        }
        public OutParamEstadoCivil getIdEstadoCivil(double secuencia)
        {
            OutParamEstadoCivil data = new OutParamEstadoCivil();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdEstadoCivil(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdEstadoCivil", ex, "");
            }
            return data;
        }
        /* Identificacion */
        public OutParamTipoIdentificacion getTiposIdentificacion()
        {
            OutParamTipoIdentificacion data = new OutParamTipoIdentificacion();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getTiposIdentificacion();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getTiposIdentificacion", ex, "");
            }
            return data;
        }
        public Response saveTiposIdentificacion(string indentificacion)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveTiposIdentificacion(indentificacion);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveTiposIdentificacion", ex, "");
            }
            return data;
        }
        public Response updTiposIdetificacion(double secuencia, string indentificacion)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updTiposIdetificacion(secuencia, indentificacion);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updTiposIdetificacion", ex, "");
            }
            return data;
        }
        public Response deleteTiposIdentificacion(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteTiposIdentificacion(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteTiposIdentificacion", ex, "");
            }
            return data;
        }
        public OutParamTipoIdentificacion getIdTipoIdentificacion(double secuencia)
        {
            OutParamTipoIdentificacion data = new OutParamTipoIdentificacion();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdTipoIdentificacion(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdTipoIdentificacion" +
                    "", ex, "");
            }
            return data;
        }
        /* Ingreso */
        public OutParamIngresos getTiposIngresos()
        {
            OutParamIngresos data = new OutParamIngresos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getTiposIngresos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getTiposIngresos", ex, "");
            }
            return data;
        }
        public Response saveIngresos(string ingresos)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveIngresos(ingresos);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveIngresos", ex, "");
            }
            return data;
        }
        public Response updIngresos(double secuencia, string ingresos)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updIngresos(secuencia, ingresos);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updIngresos", ex, "");
            }
            return data;
        }
        public Response deleteIngresos(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteIngresos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteIngresos", ex, "");
            }
            return data;
        }
        public OutParamIngresos getIdIngresos(double secuencia)
        {
            OutParamIngresos data = new OutParamIngresos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdIngresos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdIngresos" +
 "", ex, "");
            }
            return data;
        }
        /* Medios de Disposicion */
        public OutParamMedios getMedios()
        {
            OutParamMedios data = new OutParamMedios();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getMedios();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getMedios", ex, "");
            }
            return data;
        }
        public Response saveMedios(string medios)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveMedios(medios);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveMedios", ex, "");
            }
            return data;
        }
        public Response updMedios(double secuencia, string medios)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updMedios(secuencia, medios);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updMedios", ex, "");
            }
            return data;
        }
        public Response deleteMedios(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteMedios(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteMedios", ex, "");
            }
            return data;
        }
        public OutParamMedios getIdMedios(double secuencia)
        {
            OutParamMedios data = new OutParamMedios();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdMedios(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdMedios" + "", ex, "");
            }
            return data;
        }
        /* Nomina*/
        public OutParamTipoNomina getNomina()
        {
            OutParamTipoNomina data = new OutParamTipoNomina();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getNomina();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getNomina", ex, "");
            }
            return data;
        }
        public Response saveNomina(string nomina)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveNomina(nomina);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveNomina", ex, "");
            }
            return data;
        }
        public Response updNomina(double secuencia, string nomina)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updNomina(secuencia, nomina);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updNomina", ex, "");
            }
            return data;
        }
        public Response deleteNomina(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteNomina(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteNomina", ex, "");
            }
            return data;
        }
        public OutParamTipoNomina getIdNomina(double secuencia)
        {
            OutParamTipoNomina data = new OutParamTipoNomina();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdNomina(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdNomina" + "", ex, "");
            }
            return data;
        }
        /* Quincena Descuento */
        public OutParamQuincenaDscto getQuincenaDscto()
        {
            OutParamQuincenaDscto data = new OutParamQuincenaDscto();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getQuincenaDscto();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getQuincenaDscto", ex, "");
            }
            return data;
        }
        public Response saveQuincenaDscto(string quincena)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveQuincenaDscto(quincena);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveQuincenaDscto", ex, "");
            }
            return data;
        }
        public Response updQuincenaDscto(double secuencia, string quincena)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updQuincenaDscto(secuencia, quincena);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updQuincenaDscto", ex, "");
            }
            return data;
        }
        public Response deleteQuincenaDscto(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteQuincenaDscto(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteQuincenaDscto", ex, "");
            }
            return data;
        }
        public OutParamQuincenaDscto getIdQuincenaDscto(double secuencia)
        {
            OutParamQuincenaDscto data = new OutParamQuincenaDscto();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdQuincenaDscto(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdNomina" + "", ex, "");
            }
            return data;
        }
        /* Sector */
        public OutParamSector getSector()
        {
            OutParamSector data = new OutParamSector();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getSector();
            }
            catch (Exception ex)
            {
                //escribir en el log 
                LogHelper.WriteLog("Models", "ManageParams", "getSector", ex, "");
            }
            return data;
        }
        public Response saveSector(string sector)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveSector(sector);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveSector", ex, "");
            }
            return data;
        }
        public Response updSector(double secuencia, string sector)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updSector(secuencia, sector);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updSector", ex, "");
            }
            return data;
        }
        public Response deleteSector(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteSector(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteSector", ex, "");
            }
            return data;
        }
        public OutParamSector getIdSector(double secuencia)
        {
            OutParamSector data = new OutParamSector();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdSector(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdSector" + "", ex, "");
            }
            return data;
        }
        /* SectorGuias */
        public OutParamSectorGuias getSectorGuias()
        {
            OutParamSectorGuias data = new OutParamSectorGuias();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getSectorGuias();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getSectorGuias", ex, "");
            }
            return data;
        }
        public Response saveSectorGuias(string sector, double estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveSectorGuias(sector, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveSectorGuias", ex, "");
            }
            return data;
        }
        public Response updSectorGuias(double secuencia, string sector, double estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updSectorGuias(secuencia, sector, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updSectorGuias", ex, "");
            }
            return data;
        }
        public Response deleteSectorGuias(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteSectorGuias(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteSectorGuias", ex, "");
            }
            return data;
        }
        public OutParamSectorGuias getIdSectorGuias(double secuencia)
        {
            OutParamSectorGuias data = new OutParamSectorGuias();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdSectorGuias(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdSectorGuias" + "", ex, "");
            }
            return data;
        }
        /* SectorTablas */
        public OutParamSectorTablas getSectorTablas()
        {
            OutParamSectorTablas data = new OutParamSectorTablas();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getSectorTablas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getSectorTablas", ex, "");
            }
            return data;
        }
        public Response saveSectorTablas(string sector, double estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveSectorTablas(sector, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveSectorTablas", ex, "");
            }
            return data;
        }
        public Response updSectorTablas(double secuencia, string sector, double estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updSectorTablas(secuencia, sector, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updSectorTablas", ex, "");
            }
            return data;
        }
        public Response deleteSectorTablas(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteSectorTablas(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteSectorTablas", ex, "");
            }
            return data;
        }
        public OutParamSectorTablas getIdSectorTablas(double secuencia)
        {
            OutParamSectorTablas data = new OutParamSectorTablas();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdSectorTablas(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdSectorTablas" + "", ex, "");
            }
            return data;
        }
        /* Sucursal */
        public OutParamSucursales getSucursal()
        {
            OutParamSucursales data = new OutParamSucursales();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getSucursal();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getSucursal", ex, "");
            }
            return data;
        }
        public Response saveSucursal(string sucursal, string id_sucursal, double tipo, string division, string region, string emailCoordinador, string emailAuxiliar)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveSucursal(sucursal, id_sucursal, tipo, division, region, emailCoordinador, emailAuxiliar);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveSucursal", ex, "");
            }
            return data;
        }
        public Response updSucursal(double secuencia, string sucursal, double tipo_sucursal, string emailCoordinador, string emailAuxiliar)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updSucursal(secuencia, sucursal, tipo_sucursal, emailCoordinador, emailAuxiliar);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updSucursal", ex, "");
            }
            return data;
        }
        public Response deleteSucursal(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteSucursal(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteSucursal", ex, "");
            }
            return data;
        }
        public OutParamSucursales getIdSucursal(double secuencia)
        {
            OutParamSucursales data = new OutParamSucursales();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdSucursal(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdSucursal" + "", ex, "");
            }
            return data;
        }
        /* Reca */
        public OutParamReca getReca()
        {
            OutParamReca data = new OutParamReca();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getReca();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getReca", ex, "");
            }
            return data;
        }
        public Response saveReca(string reca)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveReca(reca);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveSucursal", ex, "");
            }
            return data;
        }
        public Response updReca(double secuencia, string reca)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updReca(secuencia, reca);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updReca", ex, "");
            }
            return data;
        }
        public Response deleteReca(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteReca(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteReca", ex, "");
            }
            return data;
        }
        public OutParamReca getIdReca(double secuencia)
        {
            OutParamReca data = new OutParamReca();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdReca(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "OutParamReca" + "", ex, "");
            }
            return data;
        }
        /* Productos */
        public OutParamProductoDependencia getProductosDependencia(double dependencia)
        {
            OutParamProductoDependencia data = new OutParamProductoDependencia();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getProductosDependencia(dependencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getProductosDependencia", ex, "");
            }
            return data;
        }
        public OutParamProductos getProductos()
        {
            OutParamProductos data = new OutParamProductos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getProductos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getProductos", ex, "");
            }
            return data;
        }
        public OutParamProductos getIdProductos(double secuencia)
        {
            OutParamProductos data = new OutParamProductos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdProductos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdProductos", ex, "");
            }
            return data;
        }
        public Response saveProductos(double codigo_pro, double dependencia, string producto, string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveProductos(codigo_pro, dependencia, producto, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveProductos", ex, "");
            }
            return data;
        }
        public Response updProductos(double secuencia, string producto, string estado)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updProductos(secuencia, producto, estado);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updProductos", ex, "");
            }
            return data;
        }
        public Response deleteProductos(double secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteProductos(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteProductos", ex, "");
            }
            return data;
        }
        /*Division*/
        public OutParamDivision getDivision()
        {
            OutParamDivision data = new OutParamDivision();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDivision();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDivision" + "", ex, "");
            }
            return data;
        }
        /*Region*/
        public OutParamRegion getRegionDivision(string division)
        {
            OutParamRegion data = new OutParamRegion();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getRegionDivision(division);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getRegionDivision" + "", ex, "");
            }
            return data;
        }
        /*TipoSucursal*/
        public OutParamTipoSucursal getTipoSucursal()
        {
            OutParamTipoSucursal data = new OutParamTipoSucursal();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getTipoSucursal();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getTipoSucursal" + "", ex, "");
            }
            return data;
        }
        /* Campos Parámterizados */
        public OutParamCampos getCampos()
        {
            OutParamCampos data = new OutParamCampos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getCampos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getCampos", ex, "");
            }
            return data;
        }
        public OutParamCampos getIdCampos(double cod_campo)
        {
            OutParamCampos data = new OutParamCampos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdCampos(cod_campo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdCampos", ex, "");
            }
            return data;
        }
        public Response saveCampos(double? tipoSolicitud, double dependencia, double? producto, double? periodo, string campo, string tipo, string opciones, string requerido)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveCampos(tipoSolicitud, dependencia, producto, periodo, campo, tipo, opciones, requerido);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveCampos", ex, "");
            }
            return data;
        }
        public Response updCampos(double cod_campo, string campo, string tipo, string opciones, string requerido)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updCampos(cod_campo, campo, tipo, opciones, requerido);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updCampos", ex, "");
            }
            return data;
        }
        public Response deleteCampos(double cod_campo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteCampos(cod_campo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteCampos", ex, "");
            }
            return data;
        }
        /*Buscar Entidad para carga de CSV*/
        public ParamDependecia findDependencia(string dependencia)
        {
            ParamDependecia data = new ParamDependecia();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.findDependencia(dependencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findDependencia" + "", ex, "");
            }
            return data;
        }
        public ParamProducto findProducto(string producto, double dependencia)
        {
            ParamProducto data = new ParamProducto();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.findProducto(producto, dependencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findProducto" + "", ex, "");
            }
            return data;
        }
        public ParamTipoSolicitud findTipoSolicitud(string tipoSolicitud)
        {
            ParamTipoSolicitud data = new ParamTipoSolicitud();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.findTipoSolicitud(tipoSolicitud);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findTipoSolicitud" + "", ex, "");
            }
            return data;
        }
        public ParamSector findSector(string sector)
        {
            ParamSector data = new ParamSector();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.findSector(sector);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findSector" + "", ex, "");
            }
            return data;
        }
        public ParamPeriodos findPeriodo(string periodo)
        {
            ParamPeriodos data = new ParamPeriodos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.findPeriodo(periodo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findPeriodo" + "", ex, "");
            }
            return data;
        }
        /*Paises*/
        public Response savePais(double codigo, string nombre, string codigo_pais, string user)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.savePais(codigo, nombre, codigo_pais, user);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "savePais" + "", ex, "");
            }
            return data;
        }
        public OutParamPais getPais()
        {
            OutParamPais data = new OutParamPais();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getPaises();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getPais" +
                    "", ex, "");
            }
            return data;
        }
        public OutParamPais getIdPais(double id_pais)
        {
            OutParamPais data = new OutParamPais();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdPaises(id_pais);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdPaises", ex, "");
            }
            return data;
        }
        public Response updPais(double codigo, string nombre, string codigo_pais, string usuario)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updPais(codigo, nombre, codigo_pais, usuario);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updPais" + "", ex, "");
            }
            return data;
        }
        public Response deletePais(double codigo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deletePais(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deletePais" + "", ex, "");
            }
            return data;
        }
        /*Entidades/Despartamentos*/
        public Response saveEntidad(double codigo_pais, double codigo, string nombre, string user, string abreviatura)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveEntidad(codigo_pais, codigo, nombre, user, abreviatura);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveEntidad" + "", ex, "");
            }
            return data;
        }
        public Response updEntidad(double codigo_entidad, string nombre_entidad, string user, string abreviatura)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updEntidad(codigo_entidad, nombre_entidad, user, abreviatura);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updEntidad" + "", ex, "");
            }
            return data;
        }
        public OutparamEntidadFederativa getEntidad()
        {
            OutparamEntidadFederativa data = new OutparamEntidadFederativa();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getEntidad();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getEntidad" + "", ex, "");
            }
            return data;
        }
        public OutparamEntidadFederativa getEntidadPais(double pais)
        {
            OutparamEntidadFederativa data = new OutparamEntidadFederativa();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getEntidadPais(pais);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getEntidadPais" + "", ex, "");
            }
            return data;
        }
        public OutparamEntidadFederativa getIdEntidad(double codigo)
        {
            OutparamEntidadFederativa data = new OutparamEntidadFederativa();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdEntidad(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getEntidad" + "", ex, "");
            }
            return data;
        }
        public Response deleteEntidad(double codigo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteEntidad(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteEntidad" + "", ex, "");
            }
            return data;
        }
        /*Municipios*/
        public Response saveMunicipio(double cod_pais, double codigo_entidad, double codigo, string nombre, string user)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveMunicipio(cod_pais, codigo_entidad, codigo, nombre, user);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveMunicipio" + "", ex, "");
            }
            return data;
        }
        public Response updMunicipio(double codigo_municipio, string municipio, string usuario, double pais, double entidad)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updMunicipio(codigo_municipio, municipio, usuario, pais, entidad);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updMunicipio" + "", ex, "");
            }
            return data;
        }
        public Response deleteMunicipio(double codigo, double pais, double entidad)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteMunicipio(codigo, pais, entidad);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteMunicipio" + "", ex, "");
            }
            return data;
        }
        public OutParamMunicipios getMunicipios()
        {
            OutParamMunicipios data = new OutParamMunicipios();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getMunicipios();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getMunicipios" + "", ex, "");
            }
            return data;
        }
        public OutParamMunicipios getMunicipioEntidad(double pais, double entidad)
        {
            OutParamMunicipios data = new OutParamMunicipios();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getMunicipioEntidad(pais, entidad);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getMunicipios" + "", ex, "");
            }
            return data;
        }
        public OutParamMunicipios getIdMunicipios(double codigo, double pais, double entidad)
        {
            OutParamMunicipios data = new OutParamMunicipios();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdMunicipios(codigo, pais, entidad);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdMunicipios" + "", ex, "");
            }
            return data;
        }
        /* Documentos */
        public OutParamDocumentos getDocumentos(double dependencia, double? producto)
        {
            OutParamDocumentos data = new OutParamDocumentos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDocumentos(dependencia, producto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "findDependencia" + "", ex, "");
            }
            return data;
        }
        public OutParamDocumentos getIdDocumentos(double? cod_documento)
        {
            OutParamDocumentos data = new OutParamDocumentos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdDocumentos(cod_documento);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdDocumentos" + "", ex, "");
            }
            return data;
        }
        public documents saveDocumentos(ref documents documents, double usr, HttpPostedFileBase hpf)
        {
            try
            {
                ParamsDAO dAO = new ParamsDAO();
                ManageDocuments manage = new ManageDocuments();
                var load = manage.CargarArchivo(ref documents, hpf);
                if (load)
                {
                    documents.msg = dAO.saveDocumentos(documents, usr);
                }
                else
                {
                    documents.msg.errorCode = "88";
                    documents.msg.errorMessage = "Error al Cargar el Archivo";
                }
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveDocumentos" + "", ex, "");
            }
            return documents;
        }
        public Response updDocumentos(ref documents documents, double usr, HttpPostedFileBase hpf)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                ManageDocuments manage = new ManageDocuments();
                if (documents.file == null)
                {
                    data = dao.updDocumentos(documents, usr);
                }
                else
                {
                    var carga = manage.CargarArchivo(ref documents, hpf);
                    if (carga)
                    {
                        data = dao.updDocumentos(documents, usr);
                    }
                    else
                    {
                        data.errorCode = "66";
                        data.errorMessage = "Error Cargando el Archivo";
                    }
                }
            }
            catch (Exception ex)
            {
                //escribir en el log
                data.errorCode = "67";
                data.errorMessage = "Error Cargando el Archivo";
                LogHelper.WriteLog("Models", "ManageParams", "updDocumentos" + "", ex, "");
            }
            return data;
        }
        public Response deleteDocumentos(double cod_documento)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteDocumentos(cod_documento);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteDocumentos" + "", ex, "");
            }
            return data;
        }
        public OutParamDocumentos getDocumentosOriginacion(double dependencia, double producto)
        {
            OutParamDocumentos data = new OutParamDocumentos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDocumentosOriginacion(dependencia, producto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDocumentosOriginacion" + "", ex, "");
            }
            return data;
        }
        public OutParamDocumentos getDocumentosConfig(double dependencia, double? producto)
        {
            OutParamDocumentos data = new OutParamDocumentos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDocumentosConfig(dependencia, producto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDocumentosConfig" + "", ex, "");
            }
            return data;
        }
        public OutParamDocumentos getDocumentosOriginacion(double dependencia, double? producto)
        {
            OutParamDocumentos data = new OutParamDocumentos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getDocumentosOriginacion(dependencia, producto);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getDocumentosOriginacion" + "", ex, "");
            }
            return data;
        }
        public Response saveConfigDoc(double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveConfigDoc(documento, obtencion, x, y, dato, pagina, fuente, valida, campoComparar, valorComparar, variacion);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveConfigDoc" + "", ex, "");
            }
            return data;
        }
        public Response updConfigDoc(double codigo, double documento, double obtencion, double x, double y, string dato, double pagina, double fuente, string valida, string campoComparar, string valorComparar, double? variacion)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updConfigDoc(codigo, documento, obtencion, x, y, dato, pagina, fuente, valida, campoComparar, valorComparar, variacion);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updConfigDoc" + "", ex, "");
            }
            return data;
        }
        public Response deleteConfigDoc(double codigo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteConfigDoc(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updConfigDoc" + "", ex, "");
            }
            return data;
        }
        public OutParamConfiguracionDoc getConfigDocumentos(double dependencia, double? producto, double? doc)
        {
            OutParamConfiguracionDoc data = new OutParamConfiguracionDoc();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getConfigDocumentos(dependencia, producto, doc);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getConfigDocumentos" + "", ex, "");
            }
            return data;
        }
        public OutParamConfiguracionDoc getIdConfigDocumentos(double codigo)
        {
            OutParamConfiguracionDoc data = new OutParamConfiguracionDoc();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdConfigDocumentos(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdConfigDocumentos" + "", ex, "");
            }
            return data;
        }
        //Bancos
        public Response saveBanco(string codigo, string nombre, string user)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveBanco(codigo, nombre, user);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveMunicipio" + "", ex, "");
            }
            return data;
        }
        public Response updBanco(string codigo, string nombre, string user)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updBanco(codigo, nombre, user);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updMunicipio" + "", ex, "");
            }
            return data;
        }
        public Response deleteBanco(string codigo)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteBanco(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteMunicipio" + "", ex, "");
            }
            return data;
        }
        public OutParamBanco getBanco()
        {
            OutParamBanco data = new OutParamBanco();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getBancos();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getMunicipios" + "", ex, "");
            }
            return data;
        }
        public OutParamBanco getIdBanco(string codigo)
        {
            OutParamBanco data = new OutParamBanco();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdBanco(codigo);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdMunicipios" + "", ex, "");
            }
            return data;
        }
        //Campos 
        public List<string> camposFormulario()
        {
            List<string> campos = new List<string>();
            campos.Add("NUMERO_FOLDER");
            campos.Add("FECHA_SOLICITUD");
            campos.Add("DIA_SOLICITUD");
            campos.Add("MES_SOLICITUD");
            campos.Add("MES_SOLICITUD_LETRAS");
            campos.Add("AÑO_SOLICITUD_2_DIGITOS");
            campos.Add("AÑO_SOLICITUD_4_DIGITOS");
            campos.Add("TIPO_SOLICITUD");
            campos.Add("MONTO");
            campos.Add("MONTO_ESCRITO");
            campos.Add("MONTO_ESCRITO_SIN_PESOS");
            campos.Add("CENTAVOS_MONTO_ESCRITO");
            campos.Add("PERIODO");
            campos.Add("PLAZO");
            campos.Add("LIQUIDO_BASE");
            campos.Add("LIQUIDO_BASE_ESCRITO");
            campos.Add("NO_PLAZAS");
            campos.Add("DEPENDENCIA");
            campos.Add("PRODUCTO");
            campos.Add("DESTINO_CREDITO");
            campos.Add("TIPO_NOMINA");
            campos.Add("DESCUENTO");
            campos.Add("DESCUENTO_ESCRITO");
            campos.Add("CENTAVOS_DESCUENTO_ESCRITO");
            campos.Add("TASA_ANUAL");
            campos.Add("CAT");
            campos.Add("SUCURSAL");
            campos.Add("QUINCENA_DSCTO");
            campos.Add("FECHA_PRIMER_PAGO");
            campos.Add("DIA_PRIMER_PAGO");
            campos.Add("MES_PRIMER_PAGO");
            campos.Add("MES_PRIMER_PAGO_LETRAS");
            campos.Add("AÑO_PRIMER_PAGO_2_DIGITOS");
            campos.Add("AÑO_PRIMER_PAGO_4_DIGITOS");
            campos.Add("FECHA_ULTIMO_PAGO");
            campos.Add("DIA_ULTIMO_PAGO");
            campos.Add("MES_ULTIMO_PAGO");
            campos.Add("MES_ULTIMO_PAGO_LETRAS");
            campos.Add("AÑO_ULTIMO_PAGO_2_DIGITOS");
            campos.Add("AÑO_ULTIMO_PAGO_4_DIGITOS");
            campos.Add("CAPACIDAD_PAGO");
            campos.Add("MONTO_MAXIMO");
            campos.Add("MONTO_MAXIMO_ESCRITO");
            campos.Add("MONTO_DEUDOR");
            campos.Add("MONTO_DEUDOR_ESCRITO");
            campos.Add("MATRICULA");
            campos.Add("NSS");
            campos.Add("GRUPO");
            campos.Add("CLAVE_TRABAJADOR");
            campos.Add("ESPECIFICAR");
            campos.Add("RECA");
            campos.Add("RFC");
            campos.Add("CURP");
            campos.Add("NOMBRES");
            campos.Add("PRIMER_APELLIDO");
            campos.Add("SEGUNDO_APELLIDO");
            campos.Add("IDENTIFICACION_OFICIAL");
            campos.Add("OTRA_IDENTIFICACION");
            campos.Add("FECHA_NACIMIENTO");
            campos.Add("DIA_NACIMIENTO");
            campos.Add("MES_NACIMIENTO");
            campos.Add("MES_NACIMIENTO_LETRAS");
            campos.Add("AÑO_NACIMIENTO_2_DIGITOS");
            campos.Add("AÑO_NACIMIENTO_4_DIGITOS");
            campos.Add("PAIS_NACIMIENTO");
            campos.Add("ENTIDAD_NACIMIENTO");
            campos.Add("PAIS_RESIDENCIA");
            campos.Add("NACIONALIDAD");
            campos.Add("FORMA_MIGRATORIA");
            campos.Add("GENERO");
            campos.Add("SECTOR");
            campos.Add("OTRO_SECTOR");
            campos.Add("PUESTO");
            campos.Add("ANTIGUEDAD");
            campos.Add("INGRESO_MENSUAL");
            campos.Add("NUMERO_PERSONAL");
            campos.Add("CLAVE_PRESUPUESTAL");
            campos.Add("PAGADURIA");
            campos.Add("FECHA_INGRESO");
            campos.Add("CLAVE");
            campos.Add("LUGAR_TRABAJO");
            campos.Add("CALLE");
            campos.Add("NUMERO_EXTERIOR");
            campos.Add("COLONIA");
            campos.Add("OTRA_COLONIA");
            campos.Add("TELEFONO_FIJO");
            campos.Add("EXTENSION");
            campos.Add("ENTIDAD");
            campos.Add("MUNICIPIO");
            campos.Add("CODIGO_POSTAL_OCUPACION");
            campos.Add("TIENE_CARGO_PUBLICO");
            campos.Add("PERIODO_DE_EJECUCION");
            campos.Add("CARGO_PUBLICO_FAMILIAR");
            campos.Add("NOMBRE_FAMILIAR");
            campos.Add("PUESTO_FAMILIAR");
            campos.Add("PERIODO_EJERCICO_FAMILIAR");
            campos.Add("BENEFICIARIO");
            campos.Add("NOMBRE_BENEFICIARIO");
            campos.Add("TIPO_PENSION");
            campos.Add("ADSCRIPCION_PAGO");
            campos.Add("DELEGACION");
            campos.Add("NOMBRE_TESTIGO1");
            campos.Add("MATRICULA_TESTIGO1");
            campos.Add("GAFETE_TESTIGO1");
            campos.Add("NOMBRE_TESTIGO2");
            campos.Add("MATRICULA_TESTIGO2");
            campos.Add("GAFETE_TESTIGO2");
            campos.Add("CODIGO_POSTAL_DOMICILIO");
            campos.Add("TIEMPO_RESIDENCIA");
            campos.Add("ENTIDAD_DOMICILIO");
            campos.Add("DELEGACION_DOMICILIO");
            campos.Add("COLONIA_DOMICILIO");
            campos.Add("OTRA_COLONIA_DOMICILIO");
            campos.Add("DOMICILIO_CALLE");
            campos.Add("NUMERO_EXTERIOR_DOMICILIO");
            campos.Add("NUMERO_INTERIOR_DOMICILIO");
            campos.Add("ENTRE_CALLES_DOMICILIO");
            campos.Add("EMAIL_CONTACTO");
            campos.Add("CELULAR");
            campos.Add("EMPRESA_TELEFONICA");
            campos.Add("TELEFONO_PROPIO");
            campos.Add("NOMBRE_REFERENCIA1");
            campos.Add("APELLIDO1_REFERENCIA1");
            campos.Add("APELLIDO2_REFERENCIA1");
            campos.Add("TELEFONO_REFERENCIA1");
            campos.Add("CELULAR_REFERENCIA1");
            campos.Add("HORA1_REF1");
            campos.Add("HORA2_REF1");
            campos.Add("DIA1_REF1");
            campos.Add("DIA2_REF1");
            campos.Add("PARENTESCO_REFERENCIA1");
            campos.Add("NOMBRE_REFERENCIA2");
            campos.Add("APELLIDO1_REFERENCIA2");
            campos.Add("APELLIDO2_REFERENCIA2");
            campos.Add("TELEFONO_REFERENCIA2");
            campos.Add("CELULAR_REFERENCIA2");
            campos.Add("HORA1_REF2");
            campos.Add("HORA2_REF2");
            campos.Add("DIA1_REF2");
            campos.Add("DIA2_REF2");
            campos.Add("PARENTESCO_REFERENCIA2");
            campos.Add("MEDIO_DISPOSICION");
            campos.Add("CLABE_CUENTA");
            campos.Add("CLABE_CUENTA DIGITO 1");
            campos.Add("CLABE_CUENTA DIGITO 2");
            campos.Add("CLABE_CUENTA DIGITO 3");
            campos.Add("CLABE_CUENTA DIGITO 4");
            campos.Add("CLABE_CUENTA DIGITO 5");
            campos.Add("CLABE_CUENTA DIGITO 6");
            campos.Add("CLABE_CUENTA DIGITO 7");
            campos.Add("CLABE_CUENTA DIGITO 8");
            campos.Add("CLABE_CUENTA DIGITO 9");
            campos.Add("CLABE_CUENTA DIGITO 10");
            campos.Add("CLABE_CUENTA DIGITO 11");
            campos.Add("CLABE_CUENTA DIGITO 12");
            campos.Add("CLABE_CUENTA DIGITO 13");
            campos.Add("CLABE_CUENTA DIGITO 14");
            campos.Add("CLABE_CUENTA DIGITO 15");
            campos.Add("CLABE_CUENTA DIGITO 16");
            campos.Add("CLABE_CUENTA DIGITO 17");
            campos.Add("CLABE_CUENTA DIGITO 18");
            campos.Add("BANCO");
            campos.Add("NUMERO_CUENTA");
            campos.Add("NUMERO_CUENTA DIGITO 1");
            campos.Add("NUMERO_CUENTA DIGITO 2");
            campos.Add("NUMERO_CUENTA DIGITO 3");
            campos.Add("NUMERO_CUENTA DIGITO 4");
            campos.Add("NUMERO_CUENTA DIGITO 5");
            campos.Add("NUMERO_CUENTA DIGITO 6");
            campos.Add("NUMERO_CUENTA DIGITO 7");
            campos.Add("NUMERO_CUENTA DIGITO 8");
            campos.Add("NUMERO_CUENTA DIGITO 9");
            campos.Add("NUMERO_CUENTA DIGITO 10");
            campos.Add("NUMERO_CUENTA DIGITO 11");
            campos.Add("MEDIO_DISPOSICION_ALTERNO");
            campos.Add("CLABE_CUENTA_ALTERNO");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 1");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 2");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 3");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 4");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 5");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 6");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 7");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 8");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 9");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 10");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 11");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 12");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 13");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 14");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 15");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 16");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 17");
            campos.Add("CLABE_CUENTA_ALTERNO DIGITO 18");
            campos.Add("BANCO_ALTERNO");
            campos.Add("NUMERO_CUENTA_ALTERNO");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 1");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 2");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 3");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 4");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 5");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 6");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 7");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 8");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 9");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 10");
            campos.Add("NUMERO_CUENTA_ALTERNO DIGITO 11");
            campos.Add("MEDIO_DISPOSICION_ALTERNO_2");
            campos.Add("CLABE_CUENTA_ALTERNO_2");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 1");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 2");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 3");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 4");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 5");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 6");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 7");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 8");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 9");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 10");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 11");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 12");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 13");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 14");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 15");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 16");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 17");
            campos.Add("CLABE_CUENTA_ALTERNO_2 DIGITO 18");
            campos.Add("BANCO_ALTERNO_2");
            campos.Add("NUMERO_CUENTA_ALTERNO_2");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 1");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 2");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 3");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 4");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 5");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 6");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 7");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 8");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 9");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 10");
            campos.Add("NUMERO_CUENTA_ALTERNO_2 DIGITO 11");
            campos.Add("ASESOR");
            campos.Add("EDAD_CLIENTE");
            campos.Add("TASA_MENSUAL");
            campos.Add("TOTAL_A_PAGAR_CON_INTERES");
            campos.Add("TOTAL_A_PAGAR_CON_INTERES_LETRAS");
            campos.Add("NOMBRE_COMPLETO");
            campos.Add("CASA_FINANCIERA");
            campos.Add("SUMA_SALDO_INSOLUTO_TABLA");
            campos.Add("SUMA_SALDO_INSOLUTO");
            campos.Add("SUMA_SALDO_INSOLUTO_LETRA");
            campos.Add("CENTAVOS_SUMA_SALDO_INSOLUTO_LETRA");
            campos.Add("DEPOSITO_CLIENTE");
            campos.Add("DEPOSITO_CLIENTE_LETRA");
            campos.Add("CENTAVOS_DEPOSITO_CLIENTE_LETRA");
            campos.Add("DIAS_A_PAGAR");
            campos.Add("FECHA_CONTRATO_COMPRA");
            campos.Add("MONTO_CREDITO_COMPRA");
            campos.Add("MONTO_TOTAL");
            campos.Add("PLAZO_COMPRA");
            campos.Add("SALDO_INSOLUTO");
            campos.Add("TASA_COMPRA");
            return campos;
        }
        //Parentesco
        public OutParamParentesco parentesco()
        {
            OutParamParentesco data = new OutParamParentesco();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.parentesco();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Models", "ManageParams", "parentesco" + "", ex, "");
            }
            return data;
        }
        /*Casas Financieras*/
        public OutParamCasaFinanciera getCasas()
        {
            OutParamCasaFinanciera data = new OutParamCasaFinanciera();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getCasas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getCasas", ex, "");
            }
            return data;
        }
        public OutParamCasaFinanciera getCasasActivas()
        {
            OutParamCasaFinanciera data = new OutParamCasaFinanciera();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getCasasActivas();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getCasasActivas", ex, "");
            }
            return data;
        }
        public Response saveCasa(string rfc, string casa, string estado, double asesor)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveCasa(rfc, casa, estado, asesor);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "saveCasa", ex, "");
            }
            return data;
        }
        public Response updCasa(string secuencia, string casa, string estado, double asesor)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updCasa(secuencia, casa, estado, asesor);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "updCasa", ex, "");
            }
            return data;
        }
        public Response deleteCasa(string secuencia)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteCasa(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "deleteCasa", ex, "");
            }
            return data;
        }
        public OutParamCasaFinanciera getIdCasas(string secuencia)
        {
            OutParamCasaFinanciera data = new OutParamCasaFinanciera();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdCasas(secuencia);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageParams", "getIdCasas", ex, "");
            }
            return data;
        }
        /*Clave Delegacion*/
        public OutParamClaveDelg getClaves()
        {
            OutParamClaveDelg data = new OutParamClaveDelg();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getClaves();
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error Listando las Claves";
                LogHelper.WriteLog("Models", "ManageParams", "getClaves", ex, "");
            }
            return data;
        }
        public OutParamClaveDelg getIdClaves(string cod)
        {
            OutParamClaveDelg data = new OutParamClaveDelg();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdClaves(cod);
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error Listando las Claves";
                LogHelper.WriteLog("Models", "ManageParams", "getIdClaves", ex, "");
            }
            return data;
        }
        public Response saveClaves(string cod, string delg, string usr)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.saveClaves(cod, delg, usr);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Listando las Claves";
                LogHelper.WriteLog("Models", "ManageParams", "saveClaves", ex, "");
            }
            return data;
        }
        public Response updClaves(string cod, string delg, string usr)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.updClaves(cod, delg, usr);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Listando las Claves";
                LogHelper.WriteLog("Models", "ManageParams", "updClaves", ex, "");
            }
            return data;
        }
        public Response deleteClaves(string cod)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteClaves(cod);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Listando las Claves";
                LogHelper.WriteLog("Models", "ManageParams", "deleteClaves", ex, "");
            }
            return data;
        }
        /*Avisos*/
        public OutParamAvisos getAvisos()
        {
            OutParamAvisos data = new OutParamAvisos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getAvisos();
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error Listando los Avisos";
                LogHelper.WriteLog("Models", "ManageParams", "getAvisos", ex, "");
            }
            return data;
        }
        public OutParamAvisos getAvisoActual(string fch, string baseUrl, string rootPath)
        {
            OutParamAvisos data = new OutParamAvisos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getAvisoActual(fch);
                if (data.ListAvisos[0].imgs.Count > 0)
                {
                    for (var i = 0; i < data.ListAvisos[0].imgs.Count; i++)
                    {
                        rootPath = (rootPath.IndexOf("http://originacionbayport") > -1) ? rootPath.Replace("http", "https") : rootPath;
                        var FileName = data.ListAvisos[0].imgs[i].path;
                        char[] s = new char[FileName.Length - FileName.LastIndexOf("\\") - 1];
                        FileName.CopyTo(FileName.LastIndexOf("\\") + 1, s, 0, FileName.Length - FileName.LastIndexOf("\\") - 1);
                        string fileName = new string(s);
                        if (File.Exists(baseUrl + "\\" + fileName))
                        {
                            File.Delete(baseUrl + "\\" + fileName);
                        }
                        File.Copy(FileName, baseUrl + "\\" + fileName);
                        string filevirtual = rootPath + "/Files/" + fileName;
                        data.ListAvisos[0].imgs[i].imageDataUrl = filevirtual;
                    }
                }
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error Listando los Avisos";
                LogHelper.WriteLog("Models", "ManageParams", "getAvisos", ex, "");
            }
            return data;
        }
        public Response saveFilesAvisos(ParamAviso av, List<HttpPostedFileBase> hpfs)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                av.allimgs = "";
                for (var i = 0; i < hpfs.Count; i++)
                {
                    HttpPostedFileBase hpf = hpfs[i];
                    var fileName = hpf.FileName;
                    fileName = fileName.Replace("Ñ", "N").Replace("ñ", "n");
                    fileName = fileName.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                    fileName = fileName.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                    var path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\IMGAVISOS\";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var saveFile = Path.Combine(path, fileName);
                    if (File.Exists(saveFile))
                        File.Delete(saveFile);
                    hpf.SaveAs(saveFile);
                    if (!File.Exists(saveFile))
                    {
                        data.errorCode = "300";
                        data.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                        LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, saveFile);
                        return data;
                    }
                    av.allimgs += (av.allimgs.Length == 0) ? fileName + "," + saveFile : ";" + fileName + "," + saveFile;
                }
                data = dao.saveAviso(av);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Actualizando el Aviso";
                LogHelper.WriteLog("Models", "ManageParams", "updAvisos", ex, "");
            }
            return data;
        }
        public OutParamAvisos getIdAviso(double cod, string baseUrl, string rootPath)
        {
            OutParamAvisos data = new OutParamAvisos();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.getIdAviso(cod);
                if (data.ListAvisos[0].imgs.Count > 0)
                {
                    for (var i = 0; i < data.ListAvisos[0].imgs.Count; i++)
                    {
                        rootPath = (rootPath.IndexOf("http://originacionbayport") > -1) ? rootPath.Replace("http", "https") : rootPath;
                        var FileName = data.ListAvisos[0].imgs[i].path;
                        char[] s = new char[FileName.Length - FileName.LastIndexOf("\\") - 1];
                        FileName.CopyTo(FileName.LastIndexOf("\\") + 1, s, 0, FileName.Length - FileName.LastIndexOf("\\") - 1);
                        string fileName = new string(s);
                        if (File.Exists(baseUrl + "\\" + fileName))
                        {
                            File.Delete(baseUrl + "\\" + fileName);
                        }
                        File.Copy(FileName, baseUrl + "\\" + fileName);
                        string filevirtual = rootPath + "/Files/" + fileName;
                        data.ListAvisos[0].imgs[i].imageDataUrl = filevirtual;
                    }
                }
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error Actualizando el Aviso";
                LogHelper.WriteLog("Models", "ManageParams", "getIdAviso", ex, "");
            }
            return data;
        }
        public Response updAvisos(ParamAviso av, List<HttpPostedFileBase> hpfs, string baseUrl, string deleteImgs)
        { 
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                var path = $@"{ConfigurationManager.AppSettings["rutaRaiz"]}\IMGAVISOS\";
                if (deleteImgs != null && deleteImgs.Length > 0)
                {
                    var img = deleteImgs.Split(',');
                    for (var i = 0; i < img.Length; i++)
                    {
                        File.Delete(Path.Combine(path + img[i]));
                        if(File.Exists(Path.Combine(baseUrl,img[i])))
                            File.Delete(Path.Combine(baseUrl, img[i]));
                    }
                }
                for (var i = 0; i < hpfs.Count; i++)
                {
                    HttpPostedFileBase hpf = hpfs[i];
                    var fileName = hpf.FileName;
                    fileName = fileName.Replace("Ñ", "N").Replace("ñ", "n");
                    fileName = fileName.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
                    fileName = fileName.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U");

                    
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var saveFile = Path.Combine(path, fileName);
                    if (File.Exists(saveFile))
                        File.Delete(saveFile);
                    hpf.SaveAs(saveFile);
                    if (!File.Exists(saveFile))
                    {
                        data.errorCode = "300";
                        data.errorMessage = "Error Al subir el Archivo Intente Nuevamente";
                        LogHelper.WriteLog("Models", "ManageDocuments", "CargarArchivo", null, saveFile);
                        return data;
                    }
                    av.allimgs += (av.allimgs.Length == 0) ? fileName + "," + saveFile : ";" + fileName + "," + saveFile;
                }
                data = dao.updAvisos(av);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Actualizando el Aviso";
                LogHelper.WriteLog("Models", "ManageParams", "updAvisos", ex, "");
            }
            return data;
        }
        public Response deleteAvisos(double cod)
        {
            Response data = new Response();
            try
            {
                ParamsDAO dao = new ParamsDAO();
                data = dao.deleteAvisos(cod);
            }
            catch (Exception ex)
            {
                data.errorCode = "200";
                data.errorMessage = "Error Eliminando el Aviso";
                LogHelper.WriteLog("Models", "ManageParams", "deleteAvisos", ex, "");
            }
            return data;
        }
    }
}
