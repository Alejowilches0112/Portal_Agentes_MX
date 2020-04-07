using Entities;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

using Newtonsoft;
using Newtonsoft.Json;
namespace DAO
{
    public class ProfileDAO
    {
        public OutSolicitudProgreso saveFormulario(string pestana, double usr, string Formulario)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutSolicitudProgreso data = new OutSolicitudProgreso();
            FormularioSolicitud form;
            solicitudesProgreso solicitud;
            List<solicitudesProgreso> list = new List<solicitudesProgreso>();
            string sp = "";
            try
            {
                form = JsonConvert.DeserializeObject<FormularioSolicitud>(Formulario);
                List<OracleParameter> param = new List<OracleParameter>();
                var po_numero_folder = new OracleParameter("fa_numero_folder", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                switch (pestana)
                {
                    case "solicitud":
                        if (form.folderNumber == null || form.folderNumber == "")
                        {
                            sp = "sp_insert_formulario_solicitud";
                            param.Add(new OracleParameter("fa_estado_solicitud", OracleDbType.Varchar2, "NUEVA", ParameterDirection.Input));
                            param.Add(new OracleParameter("fa_sub_estado", OracleDbType.Varchar2, "INCOMPLETAS", ParameterDirection.Input));
                        }
                        else
                        {
                            sp = "sp_upd_formulario_solicitud";
                            param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                            param.Add(new OracleParameter("fa_estado_solicitud", OracleDbType.Varchar2, form.estado, ParameterDirection.Input));
                            param.Add(new OracleParameter("fa_sub_estado", OracleDbType.Varchar2, form.subestado, ParameterDirection.Input));
                        }
                        param.Add(new OracleParameter("fa_asesor", OracleDbType.Varchar2, usr, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_fch_solicitud", OracleDbType.Varchar2, form.fchsolicitud, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tipo_solicitud", OracleDbType.Double, form.tipoSolicitud, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_monto", OracleDbType.Double, form.monto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_periodo", OracleDbType.Double, form.periodo, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_plazo", OracleDbType.Double, form.plazo, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_liq_base", OracleDbType.Double, form.LBase, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_no_plazas", OracleDbType.Double, form.nPlazas, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dependencia", OracleDbType.Double, form.Dependencia, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_producto", OracleDbType.Double, form.producto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_destino_cred", OracleDbType.Double, form.destino, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tip_nomina", OracleDbType.Double, form.tNomina, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dscto", OracleDbType.Double, form.dscto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tasa_anual", OracleDbType.Double, form.tAnual, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_cat", OracleDbType.Double, form.cat, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sucursal", OracleDbType.Double, form.sucursal, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_quincena", OracleDbType.Double, form.quincenaDscto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_fchPrPago", OracleDbType.Varchar2, form.fchPrPago, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_fchUltPago", OracleDbType.Varchar2, form.fchUltPago, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_capacidad_pago", OracleDbType.Double, form.cPago, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_monto_maximo", OracleDbType.Double, form.mMaxPlaz, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_matricula", OracleDbType.Varchar2, form.matricula, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nss", OracleDbType.Varchar2, form.nss, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_grupo", OracleDbType.Varchar2, form.grupo, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_especificar", OracleDbType.Varchar2, form.especificar, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_monto_deudor", OracleDbType.Double, form.monto_deudor, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clave", OracleDbType.Varchar2, form.clave_trabajdor, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_reca", OracleDbType.Double, form.reca, ParameterDirection.Input));
                        /**/
                        break;
                    case "datos":
                        sp = "sp_insert_formulario_datos";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_rfc", OracleDbType.Varchar2, form.RFC, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_pnombre", OracleDbType.Varchar2, form.pNombre, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_snombre", OracleDbType.Varchar2, form.sNombre, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_pApellido", OracleDbType.Varchar2, form.pApellido, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sApellido", OracleDbType.Varchar2, form.sApellido, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tipoDoc", OracleDbType.Varchar2, form.tipoDoc, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_otra", OracleDbType.Varchar2, form.otraIdentificacion, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_curp", OracleDbType.Varchar2, form.CURP, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_fch_nacimiento", OracleDbType.Varchar2, form.fecNac, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_pais", OracleDbType.Varchar2, form.paisN, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_entidad_fed", OracleDbType.Varchar2, form.entidadN, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_pais_Res", OracleDbType.Varchar2, form.paisR, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_forma_migratoria", OracleDbType.Varchar2, form.fMigratoria, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_genero", OracleDbType.Varchar2, form.gender, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_estado_civil", OracleDbType.Double, form.estadoCivil, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_genero", OracleDbType.Varchar2, form.nacionalidad, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_cliente_siebel", OracleDbType.Varchar2, form.cliente_siebel, ParameterDirection.Input));
                        break;
                    case "ocupacion":
                        sp = "sp_insert_formulario_ocupacion";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sector", OracleDbType.Double, form.sector, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_otro_sector", OracleDbType.Varchar2, form.otroSector, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_puesto", OracleDbType.Double, form.puesto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_antiguedad", OracleDbType.Varchar2, form.antiguedad, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_ingreso_mensual", OracleDbType.Double, form.ingresos, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_personal", OracleDbType.Varchar2, form.Celular, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clave_presupuestal", OracleDbType.Varchar2, form.cPresupuestal, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_pagaduria", OracleDbType.Varchar2, form.Pagaduria, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_fch_ingreso", OracleDbType.Varchar2, form.fchIngreso, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clave", OracleDbType.Varchar2, form.clave, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_lugar_trabajo", OracleDbType.Varchar2, form.lugTrabajo, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_calle", OracleDbType.Varchar2, form.calle, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_no_exterior", OracleDbType.Varchar2, form.nExterior, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_colonia", OracleDbType.Varchar2, form.colonia, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_otra_colonia", OracleDbType.Varchar2, form.otraColonia, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tel_fijo", OracleDbType.Varchar2, form.telFijo, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_extension", OracleDbType.Varchar2, form.extension, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_entidad_fed", OracleDbType.Varchar2, form.entidadT, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_municipio", OracleDbType.Varchar2, form.municipio, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_cod_postal_ocu", OracleDbType.Varchar2, form.CodigoPost, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sn_cargo_publico", OracleDbType.Varchar2, form.tCargoPu, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_periodo_ejecucion", OracleDbType.Varchar2, form.pEjecucion, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sn_cargo_fam", OracleDbType.Varchar2, form.tCargoPuF, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nombre_familiar", OracleDbType.Varchar2, form.nombFamiliar, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_puesto_familiar", OracleDbType.Varchar2, form.puestoFam, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_perioo_familiar", OracleDbType.Varchar2, form.perEjecucionFam, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_sn_beneficiario", OracleDbType.Varchar2, form.tBeneneficiario, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nombre_beneficiario", OracleDbType.Varchar2, form.nombBene, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tipo_pension", OracleDbType.Varchar2, form.tipPension, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_ads_pago", OracleDbType.Varchar2, form.ubiPago, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_delegacion", OracleDbType.Varchar2, form.delegacionImss, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nombre_testigo_1", OracleDbType.Varchar2, form.nombTest1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_matirula_tetigo_1", OracleDbType.Varchar2, form.matricula1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_gafete_testigo_1", OracleDbType.Varchar2, form.gafete1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nombre_testigo_2", OracleDbType.Varchar2, form.nombTest2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_matirula_tetigo_2", OracleDbType.Varchar2, form.matricula2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_gafete_testigo_2", OracleDbType.Varchar2, form.gafete2, ParameterDirection.Input));
                        break;
                    case "domicilio":
                        sp = "sp_insert_formulario_domicilio";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_codigo_postal", OracleDbType.Varchar2, form.codPostDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tiempo_residencia", OracleDbType.Double, form.yearResidencia, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_entidad_fed_dom", OracleDbType.Double, form.entidadDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_delegacion_dom", OracleDbType.Varchar2, form.municipioDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_colonia_dom", OracleDbType.Varchar2, form.coloniaDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_otra_colonia", OracleDbType.Varchar2, form.otraColoniaDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_domicilio_calle", OracleDbType.Varchar2, form.domicilioCalle, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_exterior", OracleDbType.Varchar2, form.noExteriorDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_interior", OracleDbType.Varchar2, form.noInteriorDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_entre_calles", OracleDbType.Varchar2, form.entreCalleDom, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_email", OracleDbType.Varchar2, form.emailContacto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_celular", OracleDbType.Varchar2, form.CelularContacto, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_empresa_telefonica", OracleDbType.Double, form.CompanyPhone, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_tel_propio", OracleDbType.Varchar2, form.telefonoPropio, ParameterDirection.Input));
                        break;
                    case "referencias":
                        sp = "sp_insert_form_referencias";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_nombre_ref1", OracleDbType.Varchar2, form.nombreRef1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_apellido1_ref1", OracleDbType.Varchar2, form.pApellidoRef1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_apellido2_ref1", OracleDbType.Varchar2, form.sApellidoRef1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_telefono_ref1", OracleDbType.Varchar2, form.TelefonoRef1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_celular_ref1", OracleDbType.Varchar2, form.CelularRef1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_hora1_ref1", OracleDbType.Varchar2, form.Hora1Ref1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_hora2_ref1", OracleDbType.Varchar2, form.Hora2Ref1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dia1_ref1", OracleDbType.Varchar2, form.dia1Ref1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dia2_ref1", OracleDbType.Varchar2, form.dia2Ref1, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_parentesco_ref1", OracleDbType.Varchar2, form.ParentescoRef1, ParameterDirection.Input));

                        param.Add(new OracleParameter("fa_nombre_ref2", OracleDbType.Varchar2, form.nombreRef2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_apellido1_ref2", OracleDbType.Varchar2, form.pApellidoRef2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_apellido2_ref2", OracleDbType.Varchar2, form.sApellidoRef2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_telefono_ref2", OracleDbType.Varchar2, form.TelefonoRef2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_celular_ref2", OracleDbType.Varchar2, form.CelularRef2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_hora1_ref2", OracleDbType.Varchar2, form.Hora1Ref2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_hora2_ref2", OracleDbType.Varchar2, form.Hora2Ref2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dia1_ref2", OracleDbType.Varchar2, form.dia1Ref2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dia2_ref2", OracleDbType.Varchar2, form.dia2Ref2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_parentesco_ref2", OracleDbType.Varchar2, form.ParentescoRef2, ParameterDirection.Input));
                        break;
                    case "medios":
                        sp = "sp_insert_formulario_medio";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_medio_dispo", OracleDbType.Double, form.medioDisp, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clabe_cuenta", OracleDbType.Varchar2, form.ClabeDisp, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_banco", OracleDbType.Varchar2, form.NombreBanco, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_cuenta", OracleDbType.Varchar2, form.NumCuentaBanc, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_medio_dispo_alt", OracleDbType.Double, form.medioDispAlt, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clabe_cuenta_alt", OracleDbType.Varchar2, form.ClabeDispAlt, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_banco_alt", OracleDbType.Varchar2, form.NombreBancoAlt, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_cuenta_alt", OracleDbType.Varchar2, form.NumCuentaBancAlt, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_medio_dispo_alt2", OracleDbType.Double, form.medioDispAlt2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_clabe_cuenta_alt2", OracleDbType.Varchar2, form.ClabeDispAlt2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_banco_alt2", OracleDbType.Varchar2, form.NombreBancoAlt2, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_numero_cuenta_alt2", OracleDbType.Varchar2, form.NumCuentaBancAlt2, ParameterDirection.Input));

                        break;
                    case "cartera":
                        sp = "sp_insert_form_compra_cartera";
                        param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, form.folderNumber, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_deposito", OracleDbType.Varchar2, form.depositoCliente, ParameterDirection.Input));
                        param.Add(new OracleParameter("fa_dias_a_pagar", OracleDbType.Varchar2, form.DiasPagar, ParameterDirection.Input));
                        for (var i = 0; i < form.cartera.Count; i++)
                        {
                            var cartera = form.cartera[i];
                            if (cartera.item == null)
                            {
                                cartera.msg = guardarCartera(cartera, form.folderNumber);
                            }
                        }
                        break;
                }
                param.Add(new OracleParameter("fa_expediente_completo", OracleDbType.Double, form.expediente_completo, ParameterDirection.Input));
                foreach (var item in param)
                {
                    ora.AddParameter(item);
                }
                ora.AddParameter(po_numero_folder);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery(sp);
                solicitud = new solicitudesProgreso();
                solicitud.folderNumber = ora.GetParameter("fa_numero_folder").ToString();
                solicitud.asesor = usr + "";
                solicitud.fecha_soliciud = form.fchsolicitud;
                solicitud.cartera = form.cartera;
                list.Add(solicitud);
                data.listSolicitudes = list;
                data.msg.errorCode = ora.GetParameter("fa_Error").ToString();
                data.msg.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.saveFormulario", e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public void updFormularioSibel(string idSibel, string folder, string estado)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            try
            {
                estado = (estado.Equals("NUEVA")) ? "Pre Capturada" : (estado.Equals("Devolucion")) ? "Recuperado" : estado;
                var pi_folder = new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input);
                var pi_id_sibel = new OracleParameter("fa_sibel", OracleDbType.Varchar2, idSibel, ParameterDirection.Input);
                var pi_status = new OracleParameter("fa_estatus", OracleDbType.Varchar2, estado, ParameterDirection.Input);

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_folder);
                ora.AddParameter(pi_id_sibel);
                ora.AddParameter(pi_status);

                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_upd_formulario_sibel");
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.updFormularioSibel", e);
            }
            finally
            {
                ora.Dispose();
            }
        }
        public void updFormularioPoliza(double? id_poliza, string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            try
            {
                var pi_folder = (new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input));
                var pi_poliza = (new OracleParameter("fa_id_poliza", OracleDbType.Double, id_poliza, ParameterDirection.Input));
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_folder);
                ora.AddParameter(pi_poliza);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_upd_formulario_poliza");
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.updFormularioSibel ", e);
            }
            finally
            {
                ora.Dispose();
            }
        }
        public Response guardarCartera(compra_cartera cartera, string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            List<OracleParameter> param = new List<OracleParameter>();
            try
            {
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_item = (new OracleParameter("fa_item_cartera", OracleDbType.Double, ParameterDirection.Output));
                po_ErrorMessage.Size = 300;
                param.Add(new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_fch_contrato", OracleDbType.Varchar2, cartera.fecha, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_casa_financiera", OracleDbType.Varchar2, cartera.entidad, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_monto_capital", OracleDbType.Double, cartera.capital, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_monto_total", OracleDbType.Double, cartera.totPagar, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_descuento", OracleDbType.Double, cartera.descuento, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_plazo", OracleDbType.Double, cartera.plazo, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_saldo_insoluto", OracleDbType.Double, cartera.saldoInsoluto, ParameterDirection.Input));
                param.Add(new OracleParameter("fa_tasa", OracleDbType.Double, cartera.tasa, ParameterDirection.Input));
                foreach (var item in param)
                {
                    ora.AddParameter(item);
                }
                ora.AddParameter(po_item);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_insert_formulario_cartera");
                cartera.item = double.Parse(ora.GetParameter("fa_item_cartera").ToString());
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception e)
            {
                data.errorCode = "88";
                data.errorMessage = "No se pudo guardar este item";
                throw new Exception("ProfileDAO.guardarCartera", e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response eliminarCartera(double item, string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            List<OracleParameter> param = new List<OracleParameter>();
            try
            {
                var pi_folder = (new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input));
                var pi_item = (new OracleParameter("fa_item_cartera", OracleDbType.Double, item, ParameterDirection.Input));
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;

                ora.AddParameter(pi_folder);
                ora.AddParameter(pi_item);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_delete_formulario_cartera");
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception e)
            {
                data.errorCode = "88";
                data.errorMessage = "No se pudo eliminar este item";
                throw new Exception("ProfileDAO.eliminarCartera", e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutSolicitudProgreso listSolicitudes( double asesor)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutSolicitudProgreso data = new OutSolicitudProgreso();
            List<solicitudesProgreso> list = new List<solicitudesProgreso>();
            solicitudesProgreso solicitud;
            string command = null;
            try
            {
                command = $@"SELECT rownum consecutivo, NUMERO_FOLDER, FCH_SOLICITUD, RFC, cliente, TIPO, ESTADO_SOLICITUD,
                    MONTO, PLAZO, DESCUENTO, asesor, asesor_superior, dependencia, sucursal, producto, monto_deudor, monto_remanente,
                    fecha_desembolso, productoCodigo, dependenciaCodigo
                    FROM V_DLG_LISTA_SOLICITUDES v
                    WHERE
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor}) = 1243 and CODIGO_ASESOR={asesor}
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor}) = 1 and CODIGO_ASESOR={asesor}
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor}) = 2 and CODIGO_ASESOR={asesor}
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor})=1242 and
                    CODIGO_ASESOR={asesor}
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor}) = 18 and 1 = 1
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor})=7 and
                    v.CODIGO_SUCURSAL=nvl((SELECT nvl(codigo_sucursal,0) FROM BBS_LIQCOM2_ASESORES a where codigo_asesor={asesor}),0)
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor})=8 and
                    v.regional =(SELECT a.codigo_region FROM BBS_LIQCOM2_ASESORES a where codigo_asesor={asesor})
                    )or
                    ((select u.codigo_rol
                        from dv11usr1 u, dlg_ingresos_portal s
                       where u.cod_usuario = s.codigo_usuario
                         and s.codigo_asesor = {asesor})=10 and
                    v.division =(SELECT a.codigo_division FROM BBS_LIQCOM2_ASESORES a where codigo_asesor={asesor})
                    )";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    solicitud = new solicitudesProgreso();
                    solicitud.folderNumber = DBNull.Value.Equals(rdr["NUMERO_FOLDER"]) ? "" : rdr["NUMERO_FOLDER"].ToString();
                    solicitud.fecha_soliciud = DBNull.Value.Equals(rdr["FCH_SOLICITUD"]) ? "" : rdr["FCH_SOLICITUD"].ToString();
                    solicitud.rfc = DBNull.Value.Equals(rdr["RFC"]) ? "" : rdr["RFC"].ToString();
                    solicitud.cliente = DBNull.Value.Equals(rdr["cliente"]) ? "" : rdr["cliente"].ToString();
                    solicitud.tipo = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();
                    solicitud.estado = DBNull.Value.Equals(rdr["ESTADO_SOLICITUD"]) ? "" : rdr["ESTADO_SOLICITUD"].ToString();
                    solicitud.monto = DBNull.Value.Equals(rdr["MONTO"]) ? 0 : double.Parse(rdr["MONTO"].ToString());
                    solicitud.plazo = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : double.Parse(rdr["PLAZO"].ToString());
                    solicitud.cuota = DBNull.Value.Equals(rdr["DESCUENTO"]) ? 0 : double.Parse(rdr["DESCUENTO"].ToString());
                    solicitud.asesor = DBNull.Value.Equals(rdr["asesor"]) ? "" : rdr["asesor"].ToString();
                    solicitud.asesor_superior = DBNull.Value.Equals(rdr["asesor_superior"]) ? "" : rdr["asesor_superior"].ToString();
                    solicitud.dependencia = DBNull.Value.Equals(rdr["dependencia"]) ? "" : rdr["dependencia"].ToString();
                    solicitud.sucursal = DBNull.Value.Equals(rdr["sucursal"]) ? "" : rdr["sucursal"].ToString();
                    solicitud.producto = DBNull.Value.Equals(rdr["producto"]) ? "" : rdr["producto"].ToString();
                    solicitud.monto_deudor = DBNull.Value.Equals(rdr["monto_deudor"]) ? 0 : double.Parse(rdr["monto_deudor"].ToString());
                    solicitud.monto_remanente = DBNull.Value.Equals(rdr["monto_remanente"]) ? 0 : double.Parse(rdr["monto_remanente"].ToString());
                    solicitud.consecutivo = DBNull.Value.Equals(rdr["consecutivo"]) ? 0 : double.Parse(rdr["consecutivo"].ToString());
                    solicitud.fecha_desembolso = DBNull.Value.Equals(rdr["fecha_desembolso"]) ? "" : rdr["fecha_desembolso"].ToString();
                    solicitud.productoCodigo = DBNull.Value.Equals(rdr["productoCodigo"]) ? 0 : double.Parse(rdr["productoCodigo"].ToString());
                    solicitud.dependenciaCodigo = DBNull.Value.Equals(rdr["dependenciaCodigo"]) ? 0 : double.Parse(rdr["dependenciaCodigo"].ToString());
                    list.Add(solicitud);
                }
                rdr.Close();
                data.listSolicitudes = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorCode = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.listSolicitudes" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public FormularioSolicitud getIdSolicitud(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            FormularioSolicitud data = new FormularioSolicitud();
            string command = null;
            try
            {
                
                command = "SELECT * FROM FORMULARIO ";
                command += string.Format("WHERE NUMERO_FOLDER = '{0}' ", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data = new FormularioSolicitud();
                    data.folderNumber = DBNull.Value.Equals(rdr["NUMERO_FOLDER"]) ? "" : rdr["NUMERO_FOLDER"].ToString();
                    data.estado = DBNull.Value.Equals(rdr["ESTADO_SOLICITUD"]) ? "" : rdr["ESTADO_SOLICITUD"].ToString();
                    data.subestado = DBNull.Value.Equals(rdr["SUB_ESTADO"]) ? "" : rdr["SUB_ESTADO"].ToString();
                    data.fchsolicitud = DBNull.Value.Equals(rdr["FECHA_SOLICITUD"]) ? "" : rdr["FECHA_SOLICITUD"].ToString();
                    data.tipoSolicitud = DBNull.Value.Equals(rdr["TIPO_SOLICITUD"]) ? 0 : double.Parse(rdr["TIPO_SOLICITUD"].ToString());
                    data.monto = DBNull.Value.Equals(rdr["MONTO"]) ? 0 : double.Parse(rdr["MONTO"].ToString());
                    data.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? 0 : double.Parse(rdr["PERIODO"].ToString());
                    data.plazo = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : double.Parse(rdr["PLAZO"].ToString()); ;
                    data.LBase = DBNull.Value.Equals(rdr["LIQUIDO_BASE"]) ? 0 : double.Parse(rdr["LIQUIDO_BASE"].ToString());
                    data.nPlazas = DBNull.Value.Equals(rdr["NO_PLAZAS"]) ? 0 : double.Parse(rdr["NO_PLAZAS"].ToString());
                    data.Dependencia = DBNull.Value.Equals(rdr["DEPENDENCIA"]) ? 0 : double.Parse(rdr["DEPENDENCIA"].ToString());
                    data.producto = DBNull.Value.Equals(rdr["PRODUCTO"]) ? 0 : double.Parse(rdr["PRODUCTO"].ToString());
                    data.destino = DBNull.Value.Equals(rdr["DESTINO_CREDITO"]) ? 0 : double.Parse(rdr["DESTINO_CREDITO"].ToString());
                    data.tNomina = DBNull.Value.Equals(rdr["TIPO_NOMINA"]) ? 0 : double.Parse(rdr["TIPO_NOMINA"].ToString());
                    data.dscto = DBNull.Value.Equals(rdr["DESCUENTO"]) ? 0 : double.Parse(rdr["DESCUENTO"].ToString());
                    data.tAnual = DBNull.Value.Equals(rdr["TASA_ANUAL"]) ? 0 : double.Parse(rdr["TASA_ANUAL"].ToString());
                    data.cat = DBNull.Value.Equals(rdr["CAT"]) ? 0 : double.Parse(rdr["CAT"].ToString());
                    data.sucursal = DBNull.Value.Equals(rdr["SUCURSAL"]) ? 0 : double.Parse(rdr["SUCURSAL"].ToString());
                    data.quincenaDscto = DBNull.Value.Equals(rdr["QUINCENA_DSCTO"]) ? 0 : double.Parse(rdr["QUINCENA_DSCTO"].ToString());
                    data.fchPrPago = DBNull.Value.Equals(rdr["FECHA_PRIMER_PAGO"]) ? "" : rdr["FECHA_PRIMER_PAGO"].ToString();
                    data.fchUltPago = DBNull.Value.Equals(rdr["FECHA_ULTIMO_PAGO"]) ? "" : rdr["FECHA_ULTIMO_PAGO"].ToString();
                    data.cPago = DBNull.Value.Equals(rdr["CAPACIDAD_PAGO"]) ? 0 : double.Parse(rdr["CAPACIDAD_PAGO"].ToString());
                    data.mMaxPlaz = DBNull.Value.Equals(rdr["MONTO_MAXIMO"]) ? 0 : double.Parse(rdr["MONTO_MAXIMO"].ToString());
                    data.monto_deudor = DBNull.Value.Equals(rdr["MONTO_DEUDOR"]) ? 0 : double.Parse(rdr["MONTO_DEUDOR"].ToString());
                    data.matricula = DBNull.Value.Equals(rdr["MATRICULA"]) ? "" : rdr["MATRICULA"].ToString();
                    data.reca = DBNull.Value.Equals(rdr["RECA"]) ? 0 : double.Parse(rdr["RECA"].ToString());
                    data.nss = DBNull.Value.Equals(rdr["NSS"]) ? "" : rdr["NSS"].ToString();
                    data.grupo = DBNull.Value.Equals(rdr["GRUPO"]) ? "" : rdr["GRUPO"].ToString();
                    data.clave_trabajdor = DBNull.Value.Equals(rdr["CLAVE_TRABAJADOR"]) ? "" : (rdr["CLAVE_TRABAJADOR"].ToString());
                    data.RFC = DBNull.Value.Equals(rdr["RFC"]) ? "" : rdr["RFC"].ToString();
                    data.CURP = DBNull.Value.Equals(rdr["CURP"]) ? "" : rdr["CURP"].ToString();
                    data.pNombre = DBNull.Value.Equals(rdr["PRIMER_NOMBRE"]) ? "" : rdr["PRIMER_NOMBRE"].ToString();
                    data.sNombre = DBNull.Value.Equals(rdr["SEGUNDO_NOMBRE"]) ? "" : rdr["SEGUNDO_NOMBRE"].ToString();
                    data.pApellido = DBNull.Value.Equals(rdr["PRIMER_APELLIDO"]) ? "" : rdr["PRIMER_APELLIDO"].ToString();
                    data.sApellido = DBNull.Value.Equals(rdr["SEGUNDO_APELLIDO"]) ? "" : rdr["SEGUNDO_APELLIDO"].ToString();
                    data.tipoDoc = DBNull.Value.Equals(rdr["IDENTIFICACION_OFICIAL"]) ? 0 : double.Parse(rdr["IDENTIFICACION_OFICIAL"].ToString());
                    data.otraIdentificacion = DBNull.Value.Equals(rdr["OTRA_IDENTIFICACION"]) ? "" : rdr["OTRA_IDENTIFICACION"].ToString();
                    data.fecNac = DBNull.Value.Equals(rdr["FECHA_NACIMIENTO"]) ? "" : rdr["FECHA_NACIMIENTO"].ToString();
                    data.paisN = DBNull.Value.Equals(rdr["PAIS_NACIMIENTO"]) ? 0 : double.Parse(rdr["PAIS_NACIMIENTO"].ToString());
                    data.entidadN = DBNull.Value.Equals(rdr["ENTIDAD_NACIMIENTO"]) ? 0 : double.Parse(rdr["ENTIDAD_NACIMIENTO"].ToString());
                    data.paisR = DBNull.Value.Equals(rdr["PAIS_RESIDENCIA"]) ? 0 : double.Parse(rdr["PAIS_RESIDENCIA"].ToString());
                    data.fMigratoria = DBNull.Value.Equals(rdr["FORMA_MIGRATORIA"]) ? "" : rdr["FORMA_MIGRATORIA"].ToString();
                    data.estadoCivil = DBNull.Value.Equals(rdr["ESTADO_CIVIL"]) ? 0 : double.Parse(rdr["ESTADO_CIVIL"].ToString());
                    data.nacionalidad = DBNull.Value.Equals(rdr["NACIONALIDAD"]) ? "" : rdr["NACIONALIDAD"].ToString();
                    data.gender = DBNull.Value.Equals(rdr["GENERO"]) ? "" : rdr["GENERO"].ToString();
                    data.puesto = DBNull.Value.Equals(rdr["PUESTO"]) ? 0 : double.Parse(rdr["PUESTO"].ToString());
                    data.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? 0 : double.Parse(rdr["SECTOR"].ToString());
                    data.otroSector = DBNull.Value.Equals(rdr["OTRO_SECTOR"]) ? "" : (rdr["OTRO_SECTOR"].ToString());
                    data.antiguedad = DBNull.Value.Equals(rdr["ANTIGUEDAD"]) ? 0 : double.Parse(rdr["ANTIGUEDAD"].ToString());
                    data.ingresos = DBNull.Value.Equals(rdr["INGRESO_MENSUAL"]) ? 0 : double.Parse(rdr["INGRESO_MENSUAL"].ToString());
                    data.Celular = DBNull.Value.Equals(rdr["NUMERO_PERSONAL"]) ? "" : rdr["NUMERO_PERSONAL"].ToString();
                    data.cPresupuestal = DBNull.Value.Equals(rdr["CLAVE_PRESUPUESTAL"]) ? "" : rdr["CLAVE_PRESUPUESTAL"].ToString();
                    data.Pagaduria = DBNull.Value.Equals(rdr["PAGADURIA"]) ? "" : rdr["PAGADURIA"].ToString();
                    data.fchIngreso = DBNull.Value.Equals(rdr["FECHA_INGRESO"]) ? "" : rdr["FECHA_INGRESO"].ToString();
                    data.clave = DBNull.Value.Equals(rdr["CLAVE"]) ? "" : rdr["CLAVE"].ToString();
                    data.lugTrabajo = DBNull.Value.Equals(rdr["LUGAR_TRABAJO"]) ? "" : rdr["LUGAR_TRABAJO"].ToString();
                    data.calle = DBNull.Value.Equals(rdr["CALLE"]) ? "" : rdr["CALLE"].ToString();
                    data.nExterior = DBNull.Value.Equals(rdr["NUMERO_EXTERIOR"]) ? "" : rdr["NUMERO_EXTERIOR"].ToString();
                    data.colonia = DBNull.Value.Equals(rdr["COLONIA"]) ? "" : rdr["COLONIA"].ToString();
                    data.otraColonia = DBNull.Value.Equals(rdr["OTRA_COLONIA"]) ? "" : rdr["OTRA_COLONIA"].ToString();
                    data.telFijo = DBNull.Value.Equals(rdr["TELEFONO_FIJO"]) ? "" : rdr["TELEFONO_FIJO"].ToString();
                    data.extension = DBNull.Value.Equals(rdr["EXTENSION"]) ? "" : rdr["EXTENSION"].ToString();
                    data.entidadT = DBNull.Value.Equals(rdr["ENTIDAD"]) ? 0 : double.Parse(rdr["ENTIDAD"].ToString());
                    data.municipio = DBNull.Value.Equals(rdr["MUNICIPIO"]) ? "" : (rdr["MUNICIPIO"].ToString());
                    data.CodigoPost = DBNull.Value.Equals(rdr["CODIGO_POSTAL_OCUPACION"]) ? "" : rdr["CODIGO_POSTAL_OCUPACION"].ToString();
                    data.tCargoPu = DBNull.Value.Equals(rdr["TIENE_CARGO_PUBLICO"]) ? "" : rdr["TIENE_CARGO_PUBLICO"].ToString();
                    data.pEjecucion = DBNull.Value.Equals(rdr["PERIODO_EJECUCION"]) ? "" : rdr["PERIODO_EJECUCION"].ToString();
                    data.tCargoPuF = DBNull.Value.Equals(rdr["CARGO_PUBLICO_FAMILIAR"]) ? "" : rdr["CARGO_PUBLICO_FAMILIAR"].ToString();
                    data.nombFamiliar = DBNull.Value.Equals(rdr["NOMBRE_FAMILIAR"]) ? "" : rdr["NOMBRE_FAMILIAR"].ToString();
                    data.puestoFam = DBNull.Value.Equals(rdr["PUESTO_FAMILIAR"]) ? "" : rdr["PUESTO_FAMILIAR"].ToString();
                    data.perEjecucionFam = DBNull.Value.Equals(rdr["PERIODO_EJERCICO_FAMILIAR"]) ? "" : rdr["PERIODO_EJERCICO_FAMILIAR"].ToString();
                    data.tBeneneficiario = DBNull.Value.Equals(rdr["BENEFICIARIO"]) ? "" : rdr["BENEFICIARIO"].ToString();
                    data.nombBene = DBNull.Value.Equals(rdr["NOMBRE_BENEFICIARIO"]) ? "" : rdr["NOMBRE_BENEFICIARIO"].ToString();
                    data.tipPension = DBNull.Value.Equals(rdr["TIPO_PENSION"]) ? "" : rdr["TIPO_PENSION"].ToString();
                    data.ubiPago = DBNull.Value.Equals(rdr["ADSCRIPCION_PAGO"]) ? "" : rdr["ADSCRIPCION_PAGO"].ToString();
                    data.delegacionImss = DBNull.Value.Equals(rdr["DELEGACION"]) ? "" : rdr["DELEGACION"].ToString();
                    data.nombTest1 = DBNull.Value.Equals(rdr["NOMBRE_TESTIGO1"]) ? "" : rdr["NOMBRE_TESTIGO1"].ToString();
                    data.matricula1 = DBNull.Value.Equals(rdr["MATRICULA_TESTIGO1"]) ? "" : rdr["MATRICULA_TESTIGO1"].ToString();
                    data.gafete1 = DBNull.Value.Equals(rdr["GAFETE_TESTIGO1"]) ? "" : rdr["GAFETE_TESTIGO1"].ToString();
                    data.nombTest2 = DBNull.Value.Equals(rdr["NOMBRE_TESTIGO2"]) ? "" : rdr["NOMBRE_TESTIGO2"].ToString();
                    data.matricula2 = DBNull.Value.Equals(rdr["MATRICULA_TESTIGO2"]) ? "" : rdr["MATRICULA_TESTIGO2"].ToString();
                    data.gafete2 = DBNull.Value.Equals(rdr["GAFETE_TESTIGO2"]) ? "" : rdr["GAFETE_TESTIGO2"].ToString();
                    data.codPostDom = DBNull.Value.Equals(rdr["CODIGO_POSTAL_DOMICILIO"]) ? "" : rdr["CODIGO_POSTAL_DOMICILIO"].ToString();
                    data.yearResidencia = DBNull.Value.Equals(rdr["TIEMPO_RESIDENCIA"]) ? "" : rdr["TIEMPO_RESIDENCIA"].ToString();
                    data.entidadDom = DBNull.Value.Equals(rdr["ENTIDAD_DOMICILIO"]) ? 0 : double.Parse(rdr["ENTIDAD_DOMICILIO"].ToString());
                    data.municipioDom = DBNull.Value.Equals(rdr["DELEGACION_DOMICILIO"]) ? "" : (rdr["DELEGACION_DOMICILIO"].ToString());
                    data.coloniaDom = DBNull.Value.Equals(rdr["COLONIA_DOMICILIO"]) ? "" : rdr["COLONIA_DOMICILIO"].ToString();
                    data.otraColoniaDom = DBNull.Value.Equals(rdr["OTRA_COLONIA_DOM"]) ? "" : rdr["OTRA_COLONIA_DOM"].ToString();
                    data.domicilioCalle = DBNull.Value.Equals(rdr["DOMICILIO_CALLE"]) ? "" : rdr["DOMICILIO_CALLE"].ToString();
                    data.noExteriorDom = DBNull.Value.Equals(rdr["NUMERO_EXTERIOR_DOMICILIO"]) ? "" : rdr["NUMERO_EXTERIOR_DOMICILIO"].ToString();
                    data.noInteriorDom = DBNull.Value.Equals(rdr["NUMERO_INTERIOR_DOMICILIO"]) ? "" : rdr["NUMERO_INTERIOR_DOMICILIO"].ToString();
                    data.entreCalleDom = DBNull.Value.Equals(rdr["ENTRE_CALLES_DOMICILIO"]) ? "" : rdr["ENTRE_CALLES_DOMICILIO"].ToString();
                    data.emailContacto = DBNull.Value.Equals(rdr["EMAIL_CONTACTO"]) ? "" : rdr["EMAIL_CONTACTO"].ToString();
                    data.CelularContacto = DBNull.Value.Equals(rdr["CELULAR"]) ? "" : rdr["CELULAR"].ToString();
                    data.CompanyPhone = DBNull.Value.Equals(rdr["EMPRESA_TELEFONICA"]) ? "" : rdr["EMPRESA_TELEFONICA"].ToString();
                    data.telefonoPropio = DBNull.Value.Equals(rdr["TELEFONO_PROPIO"]) ? "" : rdr["TELEFONO_PROPIO"].ToString();
                    data.nombreRef1 = DBNull.Value.Equals(rdr["NOMBRE_REFERENCIA1"]) ? "" : rdr["NOMBRE_REFERENCIA1"].ToString();
                    data.pApellidoRef1 = DBNull.Value.Equals(rdr["APELLIDO1_REFERENCIA1"]) ? "" : rdr["APELLIDO1_REFERENCIA1"].ToString();
                    data.sApellidoRef1 = DBNull.Value.Equals(rdr["APELLIDO2_REFERENCIA1"]) ? "" : rdr["APELLIDO2_REFERENCIA1"].ToString();
                    data.TelefonoRef1 = DBNull.Value.Equals(rdr["TELEFONO_REFERENCIA1"]) ? "" : rdr["TELEFONO_REFERENCIA1"].ToString();
                    data.CelularRef1 = DBNull.Value.Equals(rdr["CELULAR_REFERENCIA1"]) ? "" : rdr["CELULAR_REFERENCIA1"].ToString();
                    data.Hora1Ref1 = DBNull.Value.Equals(rdr["HORA1_REF1"]) ? "" : (rdr["HORA1_REF1"].ToString());
                    data.Hora2Ref1 = DBNull.Value.Equals(rdr["HORA2_REF1"]) ? "" : (rdr["HORA2_REF1"].ToString());
                    data.dia1Ref1 = DBNull.Value.Equals(rdr["DIA1_REF1"]) ? "" : rdr["DIA1_REF1"].ToString();
                    data.dia2Ref1 = DBNull.Value.Equals(rdr["DIA2_REF1"]) ? "" : rdr["DIA2_REF1"].ToString();
                    data.ParentescoRef1 = DBNull.Value.Equals(rdr["PARENTESCO_REFERENCIA1"]) ? "" : rdr["PARENTESCO_REFERENCIA1"].ToString();
                    data.nombreRef2 = DBNull.Value.Equals(rdr["NOMBRE_REFERENCIA2"]) ? "" : rdr["NOMBRE_REFERENCIA2"].ToString();
                    data.pApellidoRef2 = DBNull.Value.Equals(rdr["APELLIDO1_REFERENCIA2"]) ? "" : rdr["APELLIDO1_REFERENCIA2"].ToString();
                    data.sApellidoRef2 = DBNull.Value.Equals(rdr["APELLIDO2_REFERENCIA2"]) ? "" : rdr["APELLIDO2_REFERENCIA2"].ToString();
                    data.TelefonoRef2 = DBNull.Value.Equals(rdr["TELEFONO_REFERENCIA2"]) ? "" : rdr["TELEFONO_REFERENCIA2"].ToString();
                    data.CelularRef2 = DBNull.Value.Equals(rdr["CELULAR_REFERENCIA2"]) ? "" : rdr["CELULAR_REFERENCIA2"].ToString();
                    data.Hora1Ref2 = DBNull.Value.Equals(rdr["HORA1_REF2"]) ? "" : (rdr["HORA1_REF2"].ToString());
                    data.Hora2Ref2 = DBNull.Value.Equals(rdr["HORA2_REF2"]) ? "" : (rdr["HORA2_REF2"].ToString());
                    data.dia1Ref2 = DBNull.Value.Equals(rdr["DIA1_REF2"]) ? "" : rdr["DIA1_REF2"].ToString();
                    data.dia2Ref2 = DBNull.Value.Equals(rdr["DIA2_REF2"]) ? "" : rdr["DIA2_REF2"].ToString();
                    data.ParentescoRef2 = DBNull.Value.Equals(rdr["PARENTESCO_REFERENCIA2"]) ? "" : rdr["PARENTESCO_REFERENCIA2"].ToString();
                    data.medioDisp = DBNull.Value.Equals(rdr["MEDIO_DISPOSICION"]) ? 0 : double.Parse(rdr["MEDIO_DISPOSICION"].ToString());
                    data.ClabeDisp = DBNull.Value.Equals(rdr["CLABE_CUENTA"]) ? "" : rdr["CLABE_CUENTA"].ToString();
                    data.NombreBanco = DBNull.Value.Equals(rdr["BANCO"]) ? "" : rdr["BANCO"].ToString();
                    data.NumCuentaBanc = DBNull.Value.Equals(rdr["NUMERO_CUENTA"]) ? "" : rdr["NUMERO_CUENTA"].ToString();
                    data.medioDispAlt = DBNull.Value.Equals(rdr["MEDIO_DISPOSICION_ALTERNO"]) ? 0 : double.Parse(rdr["MEDIO_DISPOSICION_ALTERNO"].ToString());
                    data.ClabeDispAlt = DBNull.Value.Equals(rdr["CLABE_CUENTA_ALTERNO"]) ? "" : rdr["CLABE_CUENTA_ALTERNO"].ToString();
                    data.NombreBancoAlt = DBNull.Value.Equals(rdr["BANCO_ALTERNO"]) ? "" : rdr["BANCO_ALTERNO"].ToString();
                    data.NumCuentaBancAlt = DBNull.Value.Equals(rdr["NUMERO_CUENTA_ALTERNO"]) ? "" : rdr["NUMERO_CUENTA_ALTERNO"].ToString();
                    data.medioDispAlt2 = DBNull.Value.Equals(rdr["MEDIO_DISPOSICION_ALTERNO_2"]) ? 0 : double.Parse(rdr["MEDIO_DISPOSICION_ALTERNO_2"].ToString());
                    data.ClabeDispAlt2 = DBNull.Value.Equals(rdr["CLABE_CUENTA_ALTERNO_2"]) ? "" : rdr["CLABE_CUENTA_ALTERNO_2"].ToString();
                    data.NombreBancoAlt2 = DBNull.Value.Equals(rdr["BANCO_ALTERNO_2"]) ? "" : rdr["BANCO_ALTERNO_2"].ToString();
                    data.NumCuentaBancAlt2 = DBNull.Value.Equals(rdr["NUMERO_CUENTA_ALTERNO_2"]) ? "" : rdr["NUMERO_CUENTA_ALTERNO_2"].ToString();
                    data.depositoCliente = DBNull.Value.Equals(rdr["DEPOSITO_CLIENTE"]) ? 0 : double.Parse(rdr["DEPOSITO_CLIENTE"].ToString());
                    data.expediente_completo = DBNull.Value.Equals(rdr["EXPEDIENTE_COMPLETO"]) ? 0 : double.Parse(rdr["EXPEDIENTE_COMPLETO"].ToString());
                    data.DiasPagar = DBNull.Value.Equals(rdr["DIAS_A_PAGAR"]) ? "" : rdr["DIAS_A_PAGAR"].ToString();
                    data.cliente_siebel = DBNull.Value.Equals(rdr["IND_CLI_EXIS_SIEBEL"]) ? 0 : double.Parse(rdr["IND_CLI_EXIS_SIEBEL"].ToString());
                    data.tiene_seguro = DBNull.Value.Equals(rdr["TIENE_POLIZA"]) ? -1 : double.Parse(rdr["TIENE_POLIZA"].ToString());
                    data.codePlan = DBNull.Value.Equals(rdr["CODIGO_POLIZA"]) ? -1 : double.Parse(rdr["CODIGO_POLIZA"].ToString());
                    data.planValue = DBNull.Value.Equals(rdr["VALOR_POLIZA"]) ? -1 : double.Parse(rdr["VALOR_POLIZA"].ToString());


                }
                rdr.Close();
                data.cartera = getIdSolicitudCartera(folder);
                data.msg.errorCode = "0";
                data.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.listSolicitudes" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public List<compra_cartera> getIdSolicitudCartera(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            List<compra_cartera> data = new List<compra_cartera>();
            compra_cartera cartera;
            string command = null;
            try
            {
                command = "SELECT C.ITEM_CARTERA,C.FECHA_CONTRATO,B.NOMBRE_CASA, C.CASA_FINANCIERA,C.MONTO_CAPITAL,C.MONTO_TOTAL_CAPITAL,C.DESCUENTO,C.SALDO_INSOLUTO,C.TASA,C.PLAZO ";
                command += "FROM COMPRA_CARTERA C, DLG_PARAM_CASAS_FINANCIERAS B ";
                command += string.Format("WHERE B.RFC_CASA(+) = C.CASA_FINANCIERA AND C.NUMERO_FOLDER = '{0}' ORDER BY C.ITEM_CARTERA", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    cartera = new compra_cartera();
                    cartera.item = DBNull.Value.Equals(rdr["ITEM_CARTERA"]) ? 0 : double.Parse(rdr["ITEM_CARTERA"].ToString());
                    cartera.fecha = DBNull.Value.Equals(rdr["FECHA_CONTRATO"]) ? "" : rdr["FECHA_CONTRATO"].ToString();
                    cartera.entidad = DBNull.Value.Equals(rdr["NOMBRE_CASA"]) ? "" : rdr["NOMBRE_CASA"].ToString();
                    cartera.capital = DBNull.Value.Equals(rdr["MONTO_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_CAPITAL"].ToString());
                    cartera.totPagar = DBNull.Value.Equals(rdr["MONTO_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_CAPITAL"].ToString());
                    cartera.descuento = DBNull.Value.Equals(rdr["DESCUENTO"]) ? 0 : double.Parse(rdr["DESCUENTO"].ToString());
                    cartera.plazo = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : double.Parse(rdr["PLAZO"].ToString());
                    cartera.saldoInsoluto = DBNull.Value.Equals(rdr["SALDO_INSOLUTO"]) ? 0 : double.Parse(rdr["SALDO_INSOLUTO"].ToString());
                    cartera.tasa = DBNull.Value.Equals(rdr["TASA"]) ? 0 : double.Parse(rdr["TASA"].ToString());
                    cartera.rfc_casa = DBNull.Value.Equals(rdr["CASA_FINANCIERA"]) ? "" : (rdr["CASA_FINANCIERA"].ToString());
                    cartera.totPagar = DBNull.Value.Equals(rdr["MONTO_TOTAL_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_TOTAL_CAPITAL"].ToString());
                    data.Add(cartera);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.listSolicitudes" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public List<compra_cartera> getIdSolicitudCarteraDocumentos(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            List<compra_cartera> data = new List<compra_cartera>();
            compra_cartera cartera;
            string command = null;
            try
            {
                command = "SELECT C.ITEM_CARTERA,C.FECHA_CONTRATO,B.NOMBRE_CASA, C.CASA_FINANCIERA,C.MONTO_CAPITAL,C.MONTO_TOTAL_CAPITAL,C.DESCUENTO,C.SALDO_INSOLUTO,C.TASA,C.PLAZO ";
                command += "FROM COMPRA_CARTERA C, DLG_PARAM_CASAS_FINANCIERAS B ";
                command += string.Format("WHERE B.RFC_CASA(+) = C.CASA_FINANCIERA AND C.NUMERO_FOLDER = '{0}' ORDER BY  B.RFC_CASA", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    cartera = new compra_cartera();
                    cartera.item = DBNull.Value.Equals(rdr["ITEM_CARTERA"]) ? 0 : double.Parse(rdr["ITEM_CARTERA"].ToString());
                    cartera.fecha = DBNull.Value.Equals(rdr["FECHA_CONTRATO"]) ? "" : rdr["FECHA_CONTRATO"].ToString();
                    cartera.entidad = DBNull.Value.Equals(rdr["NOMBRE_CASA"]) ? "" : rdr["NOMBRE_CASA"].ToString();
                    cartera.capital = DBNull.Value.Equals(rdr["MONTO_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_CAPITAL"].ToString());
                    cartera.totPagar = DBNull.Value.Equals(rdr["MONTO_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_CAPITAL"].ToString());
                    cartera.descuento = DBNull.Value.Equals(rdr["DESCUENTO"]) ? 0 : double.Parse(rdr["DESCUENTO"].ToString());
                    cartera.plazo = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : double.Parse(rdr["PLAZO"].ToString());
                    cartera.saldoInsoluto = DBNull.Value.Equals(rdr["SALDO_INSOLUTO"]) ? 0 : double.Parse(rdr["SALDO_INSOLUTO"].ToString());
                    cartera.tasa = DBNull.Value.Equals(rdr["TASA"]) ? 0 : double.Parse(rdr["TASA"].ToString());
                    cartera.rfc_casa = DBNull.Value.Equals(rdr["CASA_FINANCIERA"]) ? "" : (rdr["CASA_FINANCIERA"].ToString());
                    cartera.totPagar = DBNull.Value.Equals(rdr["MONTO_TOTAL_CAPITAL"]) ? 0 : double.Parse(rdr["MONTO_TOTAL_CAPITAL"].ToString());

                    data.Add(cartera);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.listSolicitudes" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutParamCampos campoAdicional(double tipoSolicitud, double dependencia, double? producto, double periodo)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutParamCampos data = new OutParamCampos();
            List<ParamCampoParametrizado> list = new List<ParamCampoParametrizado>();
            ParamCampoParametrizado campoAdicional = new ParamCampoParametrizado();
            string command = null;
            try
            {
                var camposAdicionales = campoAdicionalOcupacion(dependencia, producto);
                list = camposAdicionales.ListCampos;
                command = "SELECT COD_CAMPO_PARAMETRIZADO, CAMPO_ADICIONAL, TIPO_DATO, OPCIONES_CAMPO, OBLIGATORIO FROM CAMPOS_PARAMETRIZADOS ";
                command += string.Format("WHERE TIPO_SOLICITUD = {0} AND DEPENDENCIA = {1} AND PERIODO = {2} ", tipoSolicitud, dependencia, periodo);
                command += (producto != null) ? string.Format("AND PRODUCTO = {0}", producto) : "";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    campoAdicional = new ParamCampoParametrizado();
                    campoAdicional.codigo_campo = DBNull.Value.Equals(rdr["COD_CAMPO_PARAMETRIZADO"]) ? 0 : double.Parse(rdr["COD_CAMPO_PARAMETRIZADO"].ToString());
                    campoAdicional.campo = DBNull.Value.Equals(rdr["CAMPO_ADICIONAL"]) ? string.Empty : rdr["CAMPO_ADICIONAL"].ToString();
                    campoAdicional.tipo_dato = DBNull.Value.Equals(rdr["TIPO_DATO"]) ? string.Empty : rdr["TIPO_DATO"].ToString();
                    campoAdicional.opciones = DBNull.Value.Equals(rdr["OPCIONES_CAMPO"]) ? string.Empty : rdr["OPCIONES_CAMPO"].ToString();
                    campoAdicional.obligatorio = DBNull.Value.Equals(rdr["OBLIGATORIO"]) ? string.Empty : rdr["OBLIGATORIO"].ToString();
                    list.Add(campoAdicional);
                }
                rdr.Close();
                data.ListCampos = list;
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Ok";

            }
            catch (Exception e)
            {
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Error Consultando los Campos: " + e.ToString();
                throw new Exception("ProfileDAO.campoAdicional" + command, e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutParamCampos campoAdicionalOcupacion(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutParamCampos data = new OutParamCampos();
            List<ParamCampoParametrizado> list = new List<ParamCampoParametrizado>();
            ParamCampoParametrizado campoAdicional = new ParamCampoParametrizado();
            string command = null;
            try
            {
                command = "SELECT COD_CAMPO_PARAMETRIZADO, CAMPO_ADICIONAL, TIPO_DATO, OPCIONES_CAMPO, OBLIGATORIO FROM CAMPOS_PARAMETRIZADOS ";
                command += string.Format("WHERE DEPENDENCIA = {0} ", dependencia);
                command += (producto != null) ? string.Format("AND PRODUCTO = {0} ", producto) : "";
                command += "AND TIPO_SOLICITUD IS NULL AND PERIODO IS NULL";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    campoAdicional = new ParamCampoParametrizado();
                    campoAdicional.codigo_campo = DBNull.Value.Equals(rdr["COD_CAMPO_PARAMETRIZADO"]) ? 0 : double.Parse(rdr["COD_CAMPO_PARAMETRIZADO"].ToString());
                    campoAdicional.campo = DBNull.Value.Equals(rdr["CAMPO_ADICIONAL"]) ? string.Empty : rdr["CAMPO_ADICIONAL"].ToString();
                    campoAdicional.tipo_dato = DBNull.Value.Equals(rdr["TIPO_DATO"]) ? string.Empty : rdr["TIPO_DATO"].ToString();
                    campoAdicional.opciones = DBNull.Value.Equals(rdr["OPCIONES_CAMPO"]) ? string.Empty : rdr["OPCIONES_CAMPO"].ToString();
                    campoAdicional.obligatorio = DBNull.Value.Equals(rdr["OBLIGATORIO"]) ? string.Empty : rdr["OBLIGATORIO"].ToString();
                    list.Add(campoAdicional);
                }
                rdr.Close();
                data.ListCampos = list;
                data.msg.errorCode = "200";
                data.msg.errorMessage = "Ok";

            }
            catch (Exception e)
            {
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Error Consultando los Campos: " + e.ToString();
                throw new Exception("ProfileDAO.campoAdicional" + command, e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response cargaDocumentosOriginacion(ref DocumentoOriginacion doc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pI_folder = new OracleParameter("fa_folder", OracleDbType.Double, doc.folder, ParameterDirection.Input);
                var pi_codigo_doc = new OracleParameter("fa_codigo_doc", OracleDbType.Double, doc.codigo_doc, ParameterDirection.Input);
                var pi_nombre = new OracleParameter("fa_Nombre_doc", OracleDbType.Varchar2, doc.nombreDoc, ParameterDirection.Input);
                var pi_url = new OracleParameter("fa_path", OracleDbType.Varchar2, doc.path, ParameterDirection.Input);
                var pi_expediente = new OracleParameter("fa_exp_completo", OracleDbType.Varchar2, doc.expedienteCompleto, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pI_folder);
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_url);
                ora.AddParameter(pi_expediente);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_insert_doc_originacion");
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                data.errorCode = "88";
                data.errorMessage = "Mo se pudo Cargar El Documento Correctamente. " + ex.ToString();
                throw new Exception("ProfileDAO.cargaDocumentosOriginacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion buscarDocumentos(string folder, double codigoDoc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT CODIGO_DOC_CARGADO, CODIGO_DOCUMENTO, NUMERO_FOLDER, NOMBRE_DOCUMENTO, URL_DOCUMENTO FROM DOCUMENTOS_CARGADOS ";
                command += string.Format("WHERE NUMERO_FOLDER = '{0}' AND CODIGO_DOCUMENTO = {1} ", folder, codigoDoc);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion buscarExpedienteCompleto(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT CODIGO_DOC_CARGADO, NUMERO_FOLDER, NOMBRE_DOCUMENTO, URL_DOCUMENTO FROM DOCUMENTOS_CARGADOS ";
                command += string.Format("WHERE EXPEDIENTE_COMPLETO = 1 AND NUMERO_FOLDER = {0} ", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarExpedienteCompleto" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response eliminaDocumentosOriginacion(double doc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pi_codigo_doc = new OracleParameter("fa_codigo", OracleDbType.Double, doc, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_delete_documento_cargado");
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                data.errorCode = "88";
                data.errorMessage = "Mo se pudo Eliminar El Documento Correctamente. " + ex.ToString();
                throw new Exception("ProfileDAO.eliminaDocumentosOriginacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response actualizaDocOriginacion(double doc, string folder, string nombre, string url)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            Response data = new Response();
            try
            {
                var pi_folder = new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input);
                var pi_codigo_doc = new OracleParameter("fa_codigo_doc", OracleDbType.Double, doc, ParameterDirection.Input);
                var pi_nombre = new OracleParameter("fa_Nombre_doc", OracleDbType.Varchar2, nombre, ParameterDirection.Input);
                var pi_url = new OracleParameter("fa_path", OracleDbType.Varchar2, url, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_folder);
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(pi_nombre);
                ora.AddParameter(pi_url);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_upd_documentos_originacion");
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                data.errorCode = "88";
                data.errorMessage = "Mo se pudo Eliminar El Documento Correctamente. " + ex.ToString();
                throw new Exception("ProfileDAO.eliminaDocumentosOriginacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion buscarDocumentosFolder(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT CODIGO_DOC_CARGADO, CODIGO_DOCUMENTO, NUMERO_FOLDER, NOMBRE_DOCUMENTO, URL_DOCUMENTO FROM DOCUMENTOS_CARGADOS ";
                command += string.Format("WHERE NUMERO_FOLDER = '{0}' ", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion soloFirmas(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT DC.CODIGO_DOC_CARGADO, DC.CODIGO_DOCUMENTO, DC.NUMERO_FOLDER, DC.NOMBRE_DOCUMENTO, DC.URL_DOCUMENTO ";
                command += "FROM DOCUMENTOS_CARGADOS DC, DLG_PARAM_DOCUMENTOS D ";
                command += "WHERE DC.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO AND D.FIRMA = 1 ";
                command += string.Format("AND DC.NUMERO_FOLDER = '{0}' order by D.ORDEN ", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion soloFirmas(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT * ";
                command += "FROM DLG_PARAM_DOCUMENTOS ";
                command += "WHERE FIRMA = 1 AND LLENA_AUTOMATICO = 1";
                command += string.Format("AND DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND PRODUCTO = {0} ", producto);
                }
                command += "ORDER BY ORDEN";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.pagina_firma = DBNull.Value.Equals(rdr["PAGINA_FIRMA"]) ? 0 : double.Parse(rdr["PAGINA_FIRMA"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? string.Empty : (rdr["URL_PLANTILLA"].ToString());
                    docs.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    docs.max_item = DBNull.Value.Equals(rdr["MAXIMO_ITEMS"]) ? 0 : double.Parse(rdr["MAXIMO_ITEMS"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public string[] docExiste(double? codigo, string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string[] data = new string[3];
            string command = string.Empty;
            try
            {
                command = "SELECT COUNT(CODIGO_DOCUMENTO) CANT FROM DOCUMENTOS_CARGADOS ";
                command += string.Format("WHERE CODIGO_DOCUMENTO = {0} AND NUMERO_FOLDER = '{1}'", codigo, folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data[0] = DBNull.Value.Equals(rdr["CANT"]) ? string.Empty : (rdr["CANT"].ToString());
                }
                rdr.Close();
                if (double.Parse(data[0]) > 0)
                {
                    command = "SELECT URL_DOCUMENTO, CODIGO_DOC_CARGADO FROM DOCUMENTOS_CARGADOS ";
                    command += string.Format("WHERE codigo_documento = {0} AND NUMERO_FOLDER = '{1}' ", codigo, folder);
                    rdr = ora.ExecuteCommand(command);
                    while (rdr.Read())
                    {
                        data[1] = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? "" : (rdr["URL_DOCUMENTO"].ToString());
                        data[2] = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? "" : (rdr["CODIGO_DOC_CARGADO"].ToString());

                    }
                    rdr.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.docExiste" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public List<string> documentsCartera(string folder, double? doc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            List<string> data = new List<string>();
            try
            {
                command = $@"SELECT URL_DOCUMENTO FROM DOCUMENTOS_CARGADOS WHERE NUMERO_FOLDER = {folder} AND CODIGO_DOCUMENTO = {doc}";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    if(!DBNull.Value.Equals(rdr["URL_DOCUMENTO"]))
                        data.Add(rdr["URL_DOCUMENTO"].ToString());
                }
                rdr.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.documentsCartera" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public Response deleteDocCartera(string folder, double? doc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            Response data = new Response();
            try
            {
                var pi_folder = new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input);
                var pi_codigo_doc = new OracleParameter("fa_codigo_doc", OracleDbType.Double, doc, ParameterDirection.Input);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(pi_folder);
                ora.AddParameter(pi_codigo_doc);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedure("sp_delete_docs_cartera");
                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.deleteDocCartera" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public string[] docExisteExpendiente(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string[] data = new string[3];
            string command = string.Empty;
            try
            {
                command = "SELECT COUNT(*) CANT FROM DOCUMENTOS_CARGADOS ";
                command += string.Format("WHERE EXPEDIENTE_COMPLETO = 1 AND NUMERO_FOLDER = '{0}'", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data[0] = DBNull.Value.Equals(rdr["CANT"]) ? string.Empty : (rdr["CANT"].ToString());
                }
                rdr.Close();
                if (double.Parse(data[0]) > 0)
                {
                    command = "SELECT URL_DOCUMENTO, CODIGO_DOC_CARGADO FROM DOCUMENTOS_CARGADOS ";
                    command += string.Format("WHERE EXPEDIENTE_COMPLETO = 1 AND NUMERO_FOLDER = '{0}' ", folder);
                    rdr = ora.ExecuteCommand(command);
                    while (rdr.Read())
                    {
                        data[1] = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? "" : (rdr["URL_DOCUMENTO"].ToString());
                        data[2] = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? "" : (rdr["CODIGO_DOC_CARGADO"].ToString());

                    }
                    rdr.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion expedientillo(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT DC.CODIGO_DOC_CARGADO, DC.CODIGO_DOCUMENTO, DC.NUMERO_FOLDER, DC.NOMBRE_DOCUMENTO, DC.URL_DOCUMENTO ";
                command += "FROM DOCUMENTOS_CARGADOS DC, DLG_PARAM_DOCUMENTOS D ";
                command += "WHERE DC.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO AND D.EXPENDIENTILLO = 1 ";
                command += string.Format("AND DC.NUMERO_FOLDER = '{0}' order by D.ORDEN", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.buscarDocumentos" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion expedientillo(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT * ";
                command += "FROM DLG_PARAM_DOCUMENTOS ";
                command += "WHERE EXPENDIENTILLO = 1 AND LLENA_AUTOMATICO = 1 ";
                command += string.Format("AND DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND PRODUCTO = {0} ", producto);
                }
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? string.Empty : (rdr["URL_PLANTILLA"].ToString());
                    docs.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.expedientillo" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion allDocuments(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT DC.CODIGO_DOC_CARGADO, DC.CODIGO_DOCUMENTO, DC.NUMERO_FOLDER, DC.NOMBRE_DOCUMENTO, DC.URL_DOCUMENTO ";
                command += "FROM DOCUMENTOS_CARGADOS DC, DLG_PARAM_DOCUMENTOS D ";
                command += "WHERE DC.CODIGO_DOCUMENTO = D.CODIGO_DOCUMENTO ";
                command += string.Format("AND DC.NUMERO_FOLDER = '{0}' order by D.ORDEN", folder);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo = DBNull.Value.Equals(rdr["CODIGO_DOC_CARGADO"]) ? 0 : double.Parse(rdr["CODIGO_DOC_CARGADO"].ToString());
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_DOCUMENTO"]) ? string.Empty : (rdr["URL_DOCUMENTO"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.allDocuments" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion allDocuments(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT * FROM  DLG_PARAM_DOCUMENTOS ";
                command += "WHERE LLENA_AUTOMATICO = 1 ";
                command += string.Format("AND DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND PRODUCTO = {0} ", producto);
                }
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.llena_auto = DBNull.Value.Equals(rdr["LLENA_AUTOMATICO"]) ? 0 : double.Parse(rdr["LLENA_AUTOMATICO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? string.Empty : (rdr["URL_PLANTILLA"].ToString());
                    docs.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.allDocuments" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutDocumentoOriginacion allDocumentsImpresion(double dependencia, double? producto)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDocumentoOriginacion data = new OutDocumentoOriginacion();
            List<DocumentoOriginacion> list = new List<DocumentoOriginacion>();
            DocumentoOriginacion docs;
            string command = null;
            try
            {
                command = "SELECT * FROM  DLG_PARAM_DOCUMENTOS WHERE ";
                command += string.Format("DEPENDENCIA = {0} ", dependencia);
                if (producto != null)
                {
                    command += string.Format("AND PRODUCTO = {0} ", producto);
                }
                command += "ORDER BY ORDEN";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    docs = new DocumentoOriginacion();
                    docs.codigo_doc = DBNull.Value.Equals(rdr["CODIGO_DOCUMENTO"]) ? 0 : double.Parse(rdr["CODIGO_DOCUMENTO"].ToString());
                    docs.nombreDoc = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : (rdr["NOMBRE_DOCUMENTO"].ToString());
                    docs.llena_auto = DBNull.Value.Equals(rdr["LLENA_AUTOMATICO"]) ? 0 : double.Parse(rdr["LLENA_AUTOMATICO"].ToString());
                    docs.path = DBNull.Value.Equals(rdr["URL_PLANTILLA"]) ? string.Empty : (rdr["URL_PLANTILLA"].ToString());
                    docs.compra = DBNull.Value.Equals(rdr["COMPRA"]) ? 0 : double.Parse(rdr["COMPRA"].ToString());
                    docs.max_item = DBNull.Value.Equals(rdr["MAXIMO_ITEMS"]) ? 0 : double.Parse(rdr["MAXIMO_ITEMS"].ToString());
                    list.Add(docs);
                }
                rdr.Close();
                data.ListDocumentos = list;
                data.msg.errorCode = "0";
                data.msg.errorMessage = "Ok";
            }
            catch (Exception ex)
            {
                data.msg.errorCode = "10";
                data.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.allDocuments" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutPolizas getOfertasSeguros(string folder, double vlr_solicitado, double vlr_maximo, double usuario)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutPolizas data = new OutPolizas();

            try
            {

                var pi_empresa = (new OracleParameter("fa_empresa", OracleDbType.Double, 1, ParameterDirection.Input));
                var pi_folio = (new OracleParameter("fa_carpeta", OracleDbType.Varchar2, folder, ParameterDirection.Input));
                var pi_vlrsolcitado = (new OracleParameter("fa_valor_solicitado", OracleDbType.Double, vlr_solicitado, ParameterDirection.Input));
                var pi_vlrmaximo = (new OracleParameter("fa_valor_maximo", OracleDbType.Double, vlr_maximo, ParameterDirection.Input));
                var pi_usr = (new OracleParameter("fa_usuario", OracleDbType.Varchar2, usuario, ParameterDirection.Input));
                var po_oferta = new OracleParameter("fa_ofertas", OracleDbType.Varchar2, ParameterDirection.Output);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_oferta.Size = 4000;
                po_ErrorMessage.Size = 300;

                ora.AddParameter(pi_empresa);
                ora.AddParameter(pi_folio);
                ora.AddParameter(pi_vlrsolcitado);
                ora.AddParameter(pi_vlrmaximo);
                ora.AddParameter(pi_usr);
                ora.AddParameter(po_oferta);
                ora.AddParameter(po_ErrorCode);
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("DLGWFC_F_POLIZA_OFERTA");
                data.oferta = ora.GetParameter("fa_ofertas").ToString();
                data.msg.errorCode = ora.GetParameter("fa_Error").ToString();
                data.msg.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.getOfertasSeguros", e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;

        }
        public Response actuliazaFolio(ref FormularioSolicitud doc, double usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            //var ora = new OracleServer(connectionString);
            Response data = new Response();
            var ora1 = new OracleServer(connectionString);
            try
            {
                DateTime fch_nacimiento = DateTime.Parse(doc.fecNac);
                System.Diagnostics.Debug.WriteLine(fch_nacimiento.ToString());
                var nombres = doc.pNombre.Split(' ');


                var pi_empresa = (new OracleParameter("fa_empresa", OracleDbType.Double, 1, ParameterDirection.Input));
                var pi_folio = (new OracleParameter("fa_carpeta", OracleDbType.Double, doc.folderNumber, ParameterDirection.Input));
                var pi_sn_poliza = (new OracleParameter("fa_tiene_segutro", OracleDbType.Double, doc.tiene_seguro, ParameterDirection.Input));
                var pi_cod_plan = (new OracleParameter("fa_poliza_codigo_plan", OracleDbType.Double, doc.codePlan, ParameterDirection.Input));
                var pi_vlr_poliza = (new OracleParameter("fa_poliza_valor_plan", OracleDbType.Double, doc.planValue, ParameterDirection.Input));
                var pi_usuario = (new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input));

                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora1.AddParameter(pi_empresa);
                ora1.AddParameter(pi_folio);
                ora1.AddParameter(pi_sn_poliza);
                ora1.AddParameter(pi_cod_plan);
                ora1.AddParameter(pi_vlr_poliza);
                ora1.AddParameter(pi_usuario);
                ora1.AddParameter(po_ErrorCode);
                ora1.AddParameter(po_ErrorMessage);
                ora1.ExecuteProcedureNonQuery("DLGWFC_F_POLIZA_CARPETA");
                data.errorCode = ora1.GetParameter("fa_Error").ToString();
                data.errorMessage = ora1.GetParameter("fa_Descripcion_Error").ToString();
                ora1.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.actuliazaFolio", e);
            }
            finally
            {
                ora1.Dispose();
            }
            return data;
        }
        public string[] creaPoliza(ref FormularioSolicitud doc, double usr)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            //var ora = new OracleServer(connectionString);
            string[] data = new string[3];
            var ora1 = new OracleServer(connectionString);
            try
            {
                DateTime fch_nacimiento = DateTime.Parse(doc.fecNac);

                var pi_empresa = (new OracleParameter("fa_empresa", OracleDbType.Double, 1, ParameterDirection.Input));
                ora1.AddParameter(pi_empresa);

                var pi_folio = (new OracleParameter("fa_carpeta", OracleDbType.Double, doc.folderNumber, ParameterDirection.Input));
                ora1.AddParameter(pi_folio);

                var pi_rfc = (new OracleParameter("fa_cedula", OracleDbType.Varchar2, doc.RFC, ParameterDirection.Input));
                ora1.AddParameter(pi_rfc);

                var pi_tipo_doc = (new OracleParameter("fa_tipo_doc", OracleDbType.Double, doc.tipoDoc, ParameterDirection.Input));
                ora1.AddParameter(pi_tipo_doc);

                var pi_cod_plan = (new OracleParameter("fa_poliza_codigo_plan", OracleDbType.Double, doc.codePlan, ParameterDirection.Input));
                ora1.AddParameter(pi_cod_plan);

                var pi_usuario = new OracleParameter("fa_usuario", OracleDbType.Varchar2, usr, ParameterDirection.Input);
                ora1.AddParameter(pi_usuario);

                var pi_pnombre = new OracleParameter("fa_primer_nombre", OracleDbType.Varchar2, doc.pNombre, ParameterDirection.Input);
                ora1.AddParameter(pi_pnombre);

                var pi_snombre = new OracleParameter("fa_segundo_nombre", OracleDbType.Varchar2, doc.sNombre, ParameterDirection.Input);
                ora1.AddParameter(pi_snombre);

                var pi_papellido = new OracleParameter("fa_primer_apellido", OracleDbType.Varchar2, doc.pApellido, ParameterDirection.Input);
                ora1.AddParameter(pi_papellido);

                var pi_sapellido = new OracleParameter("fa_segundo_apellido", OracleDbType.Varchar2, doc.sApellido, ParameterDirection.Input);
                ora1.AddParameter(pi_sapellido);

                var pi_genero = new OracleParameter("fa_genero", OracleDbType.Double, (doc.gender == "M") ? 1 : 2, ParameterDirection.Input);
                ora1.AddParameter(pi_genero);

                var pi_fecha = new OracleParameter("fa_fecha_nacimiento", OracleDbType.Date, fch_nacimiento, ParameterDirection.Input);
                ora1.AddParameter(pi_fecha);

                var pi_asesor = new OracleParameter("fa_codigo_asesor", OracleDbType.Double, usr, ParameterDirection.Input);
                ora1.AddParameter(pi_asesor);

                var pi_producto = new OracleParameter("fa_codigo_convenio", OracleDbType.Double, doc.Dependencia, ParameterDirection.Input);
                ora1.AddParameter(pi_producto);

                var pi_dependencia = new OracleParameter("fa_codigo_pagaduria", OracleDbType.Double, doc.Dependencia, ParameterDirection.Input);
                ora1.AddParameter(pi_dependencia);

                var po_id_poliza = new OracleParameter("fa_id_poliza", OracleDbType.Double, ParameterDirection.Output);
                ora1.AddParameter(po_id_poliza);

                var po_ErrorCode = new OracleParameter("fa_error", OracleDbType.Double, ParameterDirection.Output);
                ora1.AddParameter(po_ErrorCode);

                var po_ErrorMessage = new OracleParameter("fa_descripcion_error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora1.AddParameter(po_ErrorMessage);

                ora1.ExecuteProcedureNonQuery("DLGWFC_CREA_POLIZA");
                data[0] = ora1.GetParameter("fa_Error").ToString();
                data[1] = ora1.GetParameter("fa_Descripcion_Error").ToString();
                data[2] = ora1.GetParameter("fa_id_poliza").ToString();
                ora1.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.creaPoliza", e);
            }
            finally
            {
                ora1.Dispose();
            }
            return data;
        }
        public FormularioSolicitudDocs solicitud(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            FormularioSolicitudDocs data = new FormularioSolicitudDocs();
            List<OracleParameter> param = new List<OracleParameter>();
            var command = string.Empty;
            try
            {

                command += $@"SELECT * FROM V_DLG_FORMULARIO WHERE FOLDER = {folder}";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.folderNumber = folder;
                    data.fchsolicitud = DBNull.Value.Equals(rdr["FECHA_SOLICITUD"]) ? "" : rdr["FECHA_SOLICITUD"].ToString();

                    data.tipoSolicitud = DBNull.Value.Equals(rdr["TIPO"]) ? "" : rdr["TIPO"].ToString();

                    data.monto = DBNull.Value.Equals(rdr["MONTO"]) ? "" : rdr["MONTO"].ToString();

                    data.periodo = DBNull.Value.Equals(rdr["PERIODO"]) ? "" : rdr["PERIODO"].ToString();

                    data.plazo = DBNull.Value.Equals(rdr["PLAZO"]) ? "0" : rdr["PLAZO"].ToString();

                    data.LBase = DBNull.Value.Equals(rdr["LIQUIDO_BASE"]) ? "0" : rdr["LIQUIDO_BASE"].ToString();

                    data.nPlazas = DBNull.Value.Equals(rdr["NO_PLAZAS"]) ? "0" : rdr["NO_PLAZAS"].ToString();

                    data.Dependencia = DBNull.Value.Equals(rdr["NOMBRE_PAGADURIA"]) ? "" : rdr["NOMBRE_PAGADURIA"].ToString();

                    data.producto = DBNull.Value.Equals(rdr["DESCRIPCION_LIN"]) ? "" : rdr["DESCRIPCION_LIN"].ToString();

                    data.destino = DBNull.Value.Equals(rdr["DESTINO_CREDITO"]) ? "" : rdr["DESTINO_CREDITO"].ToString();

                    data.tNomina = DBNull.Value.Equals(rdr["TIPO_NOMINA"]) ? "" : rdr["TIPO_NOMINA"].ToString();

                    data.dscto = DBNull.Value.Equals(rdr["DESCUENTO"]) ? "0" : rdr["DESCUENTO"].ToString();

                    data.tAnual = DBNull.Value.Equals(rdr["TASA_ANUAL"]) ? "0" : rdr["TASA_ANUAL"].ToString();

                    data.cat = DBNull.Value.Equals(rdr["CAT"]) ? "0" : rdr["CAT"].ToString();

                    data.quincenaDscto = DBNull.Value.Equals(rdr["QUINCENA_DSCTO"]) ? "" : rdr["QUINCENA_DSCTO"].ToString();

                    data.fchPrPago = DBNull.Value.Equals(rdr["FECHA_PRIMER_PAGO"]) ? "" : rdr["FECHA_PRIMER_PAGO"].ToString();

                    data.fchUltPago = DBNull.Value.Equals(rdr["FECHA_ULTIMO_PAGO"]) ? "" : rdr["FECHA_ULTIMO_PAGO"].ToString();

                    data.cPago = DBNull.Value.Equals(rdr["CAPACIDAD_PAGO"]) ? "0" : rdr["CAPACIDAD_PAGO"].ToString();

                    data.mMaxPlaz = DBNull.Value.Equals(rdr["MONTO_MAXIMO"]) ? "0" : rdr["MONTO_MAXIMO"].ToString();

                    data.monto_deudor = DBNull.Value.Equals(rdr["MONTO_DEUDOR"]) ? "0" : rdr["MONTO_DEUDOR"].ToString();

                    data.matricula = DBNull.Value.Equals(rdr["MATRICULA"]) ? "0" : rdr["MATRICULA"].ToString();

                    data.nss = DBNull.Value.Equals(rdr["NSS"]) ? "0" : rdr["NSS"].ToString();

                    data.grupo = DBNull.Value.Equals(rdr["GRUPO"]) ? "0" : rdr["GRUPO"].ToString();

                    data.clave_trabajdor = DBNull.Value.Equals(rdr["CLAVE_TRABAJADOR"]) ? "0" : rdr["CLAVE_TRABAJADOR"].ToString();

                    data.especificar = DBNull.Value.Equals(rdr["ESPECIFICAR"]) ? "" : rdr["ESPECIFICAR"].ToString();

                    data.reca = DBNull.Value.Equals(rdr["RECA"]) ? "" : rdr["RECA"].ToString();

                    data.RFC = DBNull.Value.Equals(rdr["RFC"]) ? "" : rdr["RFC"].ToString();

                    data.CURP = DBNull.Value.Equals(rdr["CURP"]) ? "" : rdr["CURP"].ToString();

                    data.pNombre = DBNull.Value.Equals(rdr["PRIMER_NOMBRE"]) ? "" : rdr["PRIMER_NOMBRE"].ToString();

                    data.sNombre = DBNull.Value.Equals(rdr["SEGUNDO_NOMBRE"]) ? "" : rdr["SEGUNDO_NOMBRE"].ToString();

                    data.pApellido = DBNull.Value.Equals(rdr["PRIMER_APELLIDO"]) ? "" : rdr["PRIMER_APELLIDO"].ToString();

                    data.sApellido = DBNull.Value.Equals(rdr["SEGUNDO_APELLIDO"]) ? "" : rdr["SEGUNDO_APELLIDO"].ToString();

                    data.fecNac = DBNull.Value.Equals(rdr["FECHA_NACIMIENTO"]) ? "" : rdr["FECHA_NACIMIENTO"].ToString();

                    data.fMigratoria = DBNull.Value.Equals(rdr["FORMA_MIGRATORIA"]) ? "" : rdr["FORMA_MIGRATORIA"].ToString();

                    data.gender = DBNull.Value.Equals(rdr["GENERO"]) ? "" : rdr["GENERO"].ToString();

                    data.paisN = DBNull.Value.Equals(rdr["NOMBRE"]) ? "" : rdr["NOMBRE"].ToString();

                    data.entidadN = DBNull.Value.Equals(rdr["NOMBRE_ENTIDAD"]) ? "" : rdr["NOMBRE_ENTIDAD"].ToString();

                    data.paisR = DBNull.Value.Equals(rdr["PAISR"]) ? "" : rdr["PAISR"].ToString();

                    data.sector = DBNull.Value.Equals(rdr["SECTOR"]) ? "" : rdr["SECTOR"].ToString();

                    data.otroSector = DBNull.Value.Equals(rdr["OTRO_SECTOR"]) ? "" : (rdr["OTRO_SECTOR"].ToString());

                    data.puesto = DBNull.Value.Equals(rdr["PUESTO"]) ? "" : rdr["PUESTO"].ToString();

                    data.antiguedad = DBNull.Value.Equals(rdr["ANTIGUEDAD"]) ? "" : rdr["ANTIGUEDAD"].ToString();

                    data.ingresos = DBNull.Value.Equals(rdr["SALARIO"]) ? "" : rdr["SALARIO"].ToString();

                    data.Celular = DBNull.Value.Equals(rdr["NUMERO_PERSONAL"]) ? "" : rdr["NUMERO_PERSONAL"].ToString();

                    data.cPresupuestal = DBNull.Value.Equals(rdr["CLAVE_PRESUPUESTAL"]) ? "" : rdr["CLAVE_PRESUPUESTAL"].ToString();

                    data.Pagaduria = DBNull.Value.Equals(rdr["PAGADURIA"]) ? "" : rdr["PAGADURIA"].ToString();

                    data.fchIngreso = DBNull.Value.Equals(rdr["FECHA_INGRESO"]) ? "" : rdr["FECHA_INGRESO"].ToString();

                    data.clave = DBNull.Value.Equals(rdr["CLAVE"]) ? "" : rdr["CLAVE"].ToString();

                    data.lugTrabajo = DBNull.Value.Equals(rdr["LUGAR_TRABAJO"]) ? "" : rdr["LUGAR_TRABAJO"].ToString();

                    data.calle = DBNull.Value.Equals(rdr["CALLE"]) ? "" : rdr["CALLE"].ToString();

                    data.nExterior = DBNull.Value.Equals(rdr["NUMERO_EXTERIOR"]) ? "" : rdr["NUMERO_EXTERIOR"].ToString();

                    data.colonia = DBNull.Value.Equals(rdr["COLONIA"]) ? "" : rdr["COLONIA"].ToString();

                    data.otraColonia = DBNull.Value.Equals(rdr["OTRA_COLONIA"]) ? "" : rdr["OTRA_COLONIA"].ToString();

                    data.telFijo = DBNull.Value.Equals(rdr["TELEFONO_FIJO"]) ? "" : rdr["TELEFONO_FIJO"].ToString();

                    data.extension = DBNull.Value.Equals(rdr["EXTENSION"]) ? "" : rdr["EXTENSION"].ToString();

                    data.entidadT = DBNull.Value.Equals(rdr["ENTIDADT"]) ? "" : rdr["ENTIDADT"].ToString();

                    data.municipio = DBNull.Value.Equals(rdr["MUNICIPIO"]) ? "" : rdr["MUNICIPIO"].ToString();

                    data.CodigoPost = DBNull.Value.Equals(rdr["CODIGO_POSTAL_OCUPACION"]) ? "" : rdr["CODIGO_POSTAL_OCUPACION"].ToString();

                    data.tCargoPu = DBNull.Value.Equals(rdr["TIENE_CARGO_PUBLICO"]) ? "" : rdr["TIENE_CARGO_PUBLICO"].ToString();

                    data.pEjecucion = DBNull.Value.Equals(rdr["PERIODO_EJECUCION"]) ? "" : rdr["PERIODO_EJECUCION"].ToString();

                    data.tCargoPuF = DBNull.Value.Equals(rdr["CARGO_PUBLICO_FAMILIAR"]) ? "" : rdr["CARGO_PUBLICO_FAMILIAR"].ToString();

                    data.nombFamiliar = DBNull.Value.Equals(rdr["NOMBRE_FAMILIAR"]) ? "" : rdr["NOMBRE_FAMILIAR"].ToString();

                    data.puestoFam = DBNull.Value.Equals(rdr["PUESTO_FAMILIAR"]) ? "" : rdr["PUESTO_FAMILIAR"].ToString();

                    data.perEjecucionFam = DBNull.Value.Equals(rdr["PERIODO_EJERCICO_FAMILIAR"]) ? "" : rdr["PERIODO_EJERCICO_FAMILIAR"].ToString();

                    data.tBeneneficiario = DBNull.Value.Equals(rdr["BENEFICIARIO"]) ? "" : rdr["BENEFICIARIO"].ToString();

                    data.nombBene = DBNull.Value.Equals(rdr["NOMBRE_BENEFICIARIO"]) ? "" : rdr["NOMBRE_BENEFICIARIO"].ToString();

                    data.tipPension = DBNull.Value.Equals(rdr["TIPO_PENSION"]) ? "" : rdr["TIPO_PENSION"].ToString();

                    data.ubiPago = DBNull.Value.Equals(rdr["ADSCRIPCION_PAGO"]) ? "" : rdr["ADSCRIPCION_PAGO"].ToString();

                    data.delegacionImss = DBNull.Value.Equals(rdr["DELEGACION"]) ? "" : rdr["DELEGACION"].ToString();

                    data.nombTest1 = DBNull.Value.Equals(rdr["NOMBRE_TESTIGO1"]) ? "" : rdr["NOMBRE_TESTIGO1"].ToString();

                    data.matricula1 = DBNull.Value.Equals(rdr["MATRICULA_TESTIGO1"]) ? "" : rdr["MATRICULA_TESTIGO1"].ToString();

                    data.gafete1 = DBNull.Value.Equals(rdr["GAFETE_TESTIGO1"]) ? "" : rdr["GAFETE_TESTIGO1"].ToString();

                    data.nombTest2 = DBNull.Value.Equals(rdr["NOMBRE_TESTIGO2"]) ? "" : rdr["NOMBRE_TESTIGO2"].ToString();

                    data.matricula2 = DBNull.Value.Equals(rdr["MATRICULA_TESTIGO2"]) ? "" : rdr["MATRICULA_TESTIGO2"].ToString();

                    data.gafete2 = DBNull.Value.Equals(rdr["GAFETE_TESTIGO2"]) ? "" : rdr["GAFETE_TESTIGO2"].ToString();

                    data.codPostDom = DBNull.Value.Equals(rdr["CODIGO_POSTAL_DOMICILIO"]) ? "" : rdr["CODIGO_POSTAL_DOMICILIO"].ToString();

                    data.yearResidencia = DBNull.Value.Equals(rdr["TIEMPO_RESIDENCIA"]) ? "" : rdr["TIEMPO_RESIDENCIA"].ToString();

                    data.entidadDom = DBNull.Value.Equals(rdr["ENTIDADDOM"]) ? "" : rdr["ENTIDADDOM"].ToString();

                    data.municipioDom = DBNull.Value.Equals(rdr["DELEGACION_DOMICILIO"]) ? "" : rdr["DELEGACION_DOMICILIO"].ToString();

                    data.coloniaDom = DBNull.Value.Equals(rdr["COLONIA_DOMICILIO"]) ? "" : rdr["COLONIA_DOMICILIO"].ToString();

                    data.otraColoniaDom = DBNull.Value.Equals(rdr["OTRA_COLONIA_DOM"]) ? "": rdr["OTRA_COLONIA_DOM"].ToString();

                    data.domicilioCalle = DBNull.Value.Equals(rdr["DOMICILIO_CALLE"]) ? "" : rdr["DOMICILIO_CALLE"].ToString();

                    data.noExteriorDom = DBNull.Value.Equals(rdr["NUMERO_EXTERIOR_DOMICILIO"]) ? "" : rdr["NUMERO_EXTERIOR_DOMICILIO"].ToString();

                    data.noInteriorDom = DBNull.Value.Equals(rdr["NUMERO_INTERIOR_DOMICILIO"]) ? "" : rdr["NUMERO_INTERIOR_DOMICILIO"].ToString();

                    data.entreCalleDom = DBNull.Value.Equals(rdr["ENTRE_CALLES_DOMICILIO"]) ? "" : rdr["ENTRE_CALLES_DOMICILIO"].ToString();

                    data.emailContacto = DBNull.Value.Equals(rdr["EMAIL_CONTACTO"]) ? "" : rdr["EMAIL_CONTACTO"].ToString();

                    data.CelularContacto = DBNull.Value.Equals(rdr["CELULAR"]) ? "" : rdr["CELULAR"].ToString();

                    data.CompanyPhone = DBNull.Value.Equals(rdr["EMPRESA"]) ? "" : rdr["EMPRESA"].ToString();

                    data.telefonoPropio = DBNull.Value.Equals(rdr["TELEFONO_PROPIO"]) ? "" : rdr["TELEFONO_PROPIO"].ToString();

                    data.nombreRef1 = DBNull.Value.Equals(rdr["NOMBRE_REFERENCIA1"]) ? "" : rdr["NOMBRE_REFERENCIA1"].ToString();

                    data.pApellidoRef1 = DBNull.Value.Equals(rdr["APELLIDO1_REFERENCIA1"]) ? "" : rdr["APELLIDO1_REFERENCIA1"].ToString();

                    data.sApellidoRef1 = DBNull.Value.Equals(rdr["APELLIDO2_REFERENCIA1"]) ? "" : rdr["APELLIDO2_REFERENCIA1"].ToString();

                    data.TelefonoRef1 = DBNull.Value.Equals(rdr["TELEFONO_REFERENCIA1"]) ? "0" : rdr["TELEFONO_REFERENCIA1"].ToString();

                    data.CelularRef1 = DBNull.Value.Equals(rdr["CELULAR_REFERENCIA1"]) ? "0" : rdr["CELULAR_REFERENCIA1"].ToString();

                    data.Hora1Ref1 = DBNull.Value.Equals(rdr["HORA1_REF1"]) ? "" : (rdr["HORA1_REF1"].ToString());

                    data.Hora2Ref1 = DBNull.Value.Equals(rdr["HORA2_REF1"]) ? "" : (rdr["HORA2_REF1"].ToString());

                    data.dia1Ref1 = DBNull.Value.Equals(rdr["DIA1_REF1"]) ? "" : rdr["DIA1_REF1"].ToString();

                    data.dia2Ref1 = DBNull.Value.Equals(rdr["DIA2_REF1"]) ? "" : rdr["DIA2_REF1"].ToString();

                    data.ParentescoRef1 = DBNull.Value.Equals(rdr["PARENTESCO_REFERENCIA1"]) ? "" : rdr["PARENTESCO_REFERENCIA1"].ToString();

                    data.nombreRef2 = DBNull.Value.Equals(rdr["NOMBRE_REFERENCIA2"]) ? "" : rdr["NOMBRE_REFERENCIA2"].ToString();

                    data.pApellidoRef2 = DBNull.Value.Equals(rdr["APELLIDO1_REFERENCIA2"]) ? "" : rdr["APELLIDO1_REFERENCIA2"].ToString();

                    data.sApellidoRef2 = DBNull.Value.Equals(rdr["APELLIDO2_REFERENCIA2"]) ? "" : rdr["APELLIDO2_REFERENCIA2"].ToString();

                    data.TelefonoRef2 = DBNull.Value.Equals(rdr["TELEFONO_REFERENCIA2"]) ? "0" : rdr["TELEFONO_REFERENCIA2"].ToString();

                    data.CelularRef2 = DBNull.Value.Equals(rdr["CELULAR_REFERENCIA2"]) ? "0" : rdr["CELULAR_REFERENCIA2"].ToString();

                    data.Hora1Ref2 = DBNull.Value.Equals(rdr["HORA1_REF2"]) ? "" : (rdr["HORA1_REF2"].ToString());

                    data.Hora2Ref2 = DBNull.Value.Equals(rdr["HORA2_REF2"]) ? "" : (rdr["HORA2_REF2"].ToString());

                    data.dia1Ref2 = DBNull.Value.Equals(rdr["DIA1_REF2"]) ? "" : rdr["DIA1_REF2"].ToString();

                    data.dia2Ref2 = DBNull.Value.Equals(rdr["DIA2_REF2"]) ? "" : rdr["DIA2_REF2"].ToString();

                    data.ParentescoRef2 = DBNull.Value.Equals(rdr["PARENTESCO_REFERENCIA2"]) ? "" : rdr["PARENTESCO_REFERENCIA2"].ToString();

                    data.medioDisp = DBNull.Value.Equals(rdr["MEDIO"]) ? "" : rdr["MEDIO"].ToString();

                    data.ClabeDisp = DBNull.Value.Equals(rdr["CLABE_CUENTA"]) ? "" : rdr["CLABE_CUENTA"].ToString();

                    data.NombreBanco = DBNull.Value.Equals(rdr["NOMBRE_BANCO"]) ? "" : rdr["NOMBRE_BANCO"].ToString();

                    data.NumCuentaBanc = DBNull.Value.Equals(rdr["NUMERO_CUENTA"]) ? "" : rdr["NUMERO_CUENTA"].ToString();

                    data.medioDispAlt = DBNull.Value.Equals(rdr["MEDIO_ALTERNO"]) ? "" : rdr["MEDIO_ALTERNO"].ToString();

                    data.ClabeDispAlt = DBNull.Value.Equals(rdr["CLABE_CUENTA_ALTERNO"]) ? "" : rdr["CLABE_CUENTA_ALTERNO"].ToString();

                    data.NombreBancoAlt = DBNull.Value.Equals(rdr["BANCO_ALT"]) ? "" : rdr["BANCO_ALT"].ToString();

                    data.NumCuentaBancAlt = DBNull.Value.Equals(rdr["NUMERO_CUENTA_ALTERNO"]) ? "" : rdr["NUMERO_CUENTA_ALTERNO"].ToString();

                    data.medioDispAlt2 = DBNull.Value.Equals(rdr["MEDIO_ALTERNO_2"]) ? "" : rdr["MEDIO_ALTERNO_2"].ToString();

                    data.ClabeDispAlt2 = DBNull.Value.Equals(rdr["CLABE_CUENTA_ALTERNO_2"]) ? "" : rdr["CLABE_CUENTA_ALTERNO_2"].ToString();

                    data.NombreBancoAlt2 = DBNull.Value.Equals(rdr["BANCO_ALT2"]) ? "" : rdr["BANCO_ALT2"].ToString();

                    data.NumCuentaBancAlt2 = DBNull.Value.Equals(rdr["NUMERO_CUENTA_ALTERNO_2"]) ? "" : rdr["NUMERO_CUENTA_ALTERNO_2"].ToString();

                    data.sucursal = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? "" : rdr["NOMBRE_SUCURSAL"].ToString();

                    data.asesor = DBNull.Value.Equals(rdr["NOMBRE_ASESOR"]) ? "" : rdr["NOMBRE_ASESOR"].ToString();

                    data.tipoDoc = DBNull.Value.Equals(rdr["NOMBRE_TIPO"]) ? "" : rdr["NOMBRE_TIPO"].ToString();

                    data.otraIdentificacion = DBNull.Value.Equals(rdr["OTRA_IDENTIFICACION"]) ? "" : rdr["OTRA_IDENTIFICACION"].ToString();

                    data.estadoCivil = DBNull.Value.Equals(rdr["ESTADO_CIVIL"]) ? "" : rdr["ESTADO_CIVIL"].ToString();

                    data.id_sibel = DBNull.Value.Equals(rdr["ID_SOLICITUD_SIBEL"]) ? "" : rdr["ID_SOLICITUD_SIBEL"].ToString();

                    data.tiene_seguro = DBNull.Value.Equals(rdr["TIENE_POLIZA"]) ? -1 : double.Parse(rdr["TIENE_POLIZA"].ToString());

                    data.codePlan = DBNull.Value.Equals(rdr["CODIGO_POLIZA"]) ? 0 : double.Parse(rdr["CODIGO_POLIZA"].ToString());

                    data.planValue = DBNull.Value.Equals(rdr["VALOR_POLIZA"]) ? 0 : double.Parse(rdr["VALOR_POLIZA"].ToString());

                    data.idPoliza = DBNull.Value.Equals(rdr["ID_POLIZA"]) ? 0 : double.Parse(rdr["ID_POLIZA"].ToString());

                    data.estado = DBNull.Value.Equals(rdr["ESTADO_SOLICITUD"]) ? "Pre Capturada" : ((rdr["ESTADO_SOLICITUD"].ToString() == "NUEVA") ? "Pre Capturada" : rdr["ESTADO_SOLICITUD"].ToString());

                    data.depositoCliente = DBNull.Value.Equals(rdr["DEPOSITO_CLIENTE"]) ? "" : rdr["DEPOSITO_CLIENTE"].ToString();
                    data.expediente_completo = DBNull.Value.Equals(rdr["EXPEDIENTE_COMPLETO"]) ? 0 : double.Parse(rdr["EXPEDIENTE_COMPLETO"].ToString());
                    data.DiasPagar = DBNull.Value.Equals(rdr["DIAS_A_PAGAR"]) ? "" : rdr["DIAS_A_PAGAR"].ToString();
                    data.nacionalidad = DBNull.Value.Equals(rdr["NACIONALIDAD"]) ? "" : rdr["NACIONALIDAD"].ToString();
                    data.estatus = DBNull.Value.Equals(rdr["ESTADO"]) ? "" : (double.Parse(rdr["ESTADO"].ToString()) == 0) ? "INACTIVA" : (double.Parse(rdr["ESTADO"].ToString()) == 1) ? "ACTIVA" : "CANCELADA";
                    data.tipoPlan = DBNull.Value.Equals(rdr["NOMBRE_PLAN"]) ? "" : rdr["NOMBRE_PLAN"].ToString();
                    data.fechaVigencia = DBNull.Value.Equals(rdr["FEC_INI_VIGENCIA"]) ? "" : rdr["FEC_INI_VIGENCIA"].ToString();
                    data.msg.errorCode = "0";
                    data.msg.errorMessage = "Exito";
                }
                rdr.Close();
                data.tieneSeguro = tiene_seguros(data.RFC);
                data.cartera = getIdSolicitudCarteraDocumentos(folder);
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.solicitud", e);
            }
            finally
            {
                ora.Dispose();
            }
            return data;

        }
        public string tiene_seguros(string rfc)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            string salida = "N";
            try
            {
                command = string.Format("SELECT COUNT(CODIGO_POLIZA) TIENEPOLIZA FROM FORMULARIO WHERE RFC = '{0}'", rfc);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    salida = DBNull.Value.Equals(rdr["TIENEPOLIZA"]) ? "N" : ((double.Parse(rdr["TIENEPOLIZA"].ToString()) > 0) ? "S" : "N");
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.tiene_seguros", e);
            }
            finally
            {
                ora.Dispose();
            }
            return salida;
        }
        public string beneficiariosPoliza(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = string.Empty;
            string salida = "0|0";
            try
            {
                command = "SELECT P.ID_POLIZA, IND_PERMITE_FAMILIARES, IND_PERMITE_NO_FAMILIARES ";
                command += "FROM BBS_POLIZAS_MAESTRO P, FORMULARIO F ";
                command += string.Format("WHERE F.NUMERO_FOLDER = '{0}'", folder);
                command += "AND TO_NUMBER(F.NUMERO_FOLDER) = P.NUMERO_CREDITO ";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    salida = DBNull.Value.Equals(rdr["IND_PERMITE_FAMILIARES"]) ? "0" : rdr["IND_PERMITE_FAMILIARES"].ToString();
                    salida += "|" + (DBNull.Value.Equals(rdr["IND_PERMITE_FAMILIARES"]) ? "0" : rdr["IND_PERMITE_FAMILIARES"].ToString());
                    salida += "|" + (DBNull.Value.Equals(rdr["ID_POLIZA"]) ? "0" : rdr["ID_POLIZA"].ToString());
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ProfileDAO.tiene_seguros", e);
            }
            finally
            {
                ora.Dispose();
            }
            return salida;
        }
        public Response guardarBeneficiarioPoliza(ref InBeneficiarioPoliza beneficiario, double poliza)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            //var ora = new OracleServer(connectionString);
            Response data = new Response();
            var ora = new OracleServer(connectionString);
            try
            {
                DateTime fch_nacimiento = DateTime.Parse(beneficiario.fechaNacimiento);
                var pi_poliza = (new OracleParameter("fa_id_poliza", OracleDbType.Double, poliza, ParameterDirection.Input));
                ora.AddParameter(pi_poliza);
                var pi_inidicador = (new OracleParameter("fa_indicador", OracleDbType.Double, beneficiario.tipo, ParameterDirection.Input));
                ora.AddParameter(pi_inidicador);
                var pi_tip_per = (new OracleParameter("fa_tipo_persona", OracleDbType.Double, beneficiario.tipoDoc, ParameterDirection.Input));
                ora.AddParameter(pi_tip_per);
                var pi_rfc = (new OracleParameter("fa_codigo_persona", OracleDbType.Varchar2, beneficiario.rfc, ParameterDirection.Input));
                ora.AddParameter(pi_rfc);
                var pi_pnombre = (new OracleParameter("fa_primer_nombre", OracleDbType.Varchar2, beneficiario.pNombre, ParameterDirection.Input));
                ora.AddParameter(pi_pnombre);
                var pi_snombre = (new OracleParameter("fa_segundo_nombre", OracleDbType.Varchar2, beneficiario.sNombre, ParameterDirection.Input));
                ora.AddParameter(pi_snombre);
                var pi_papellido = (new OracleParameter("fa_primer_apellido", OracleDbType.Varchar2, beneficiario.pApellido, ParameterDirection.Input));
                ora.AddParameter(pi_papellido);
                var pi_sapellido = (new OracleParameter("fa_segundo_apellido", OracleDbType.Varchar2, beneficiario.sApellido, ParameterDirection.Input));
                ora.AddParameter(pi_sapellido);
                var pi_direccion = (new OracleParameter("fa_direccion", OracleDbType.Varchar2, beneficiario.calleBeneficiario, ParameterDirection.Input));
                ora.AddParameter(pi_direccion);
                var pi_dpto = (new OracleParameter("fa_codigo_departamento", OracleDbType.Double, beneficiario.entidadF, ParameterDirection.Input));
                ora.AddParameter(pi_dpto);
                var pi_ciudad = (new OracleParameter("fa_codigo_ciudad", OracleDbType.Double, beneficiario.municipioF, ParameterDirection.Input));
                ora.AddParameter(pi_ciudad);
                var pi_tel_fijo = (new OracleParameter("fa_telefono_fijo", OracleDbType.Varchar2, beneficiario.tefFijoBeneficiario, ParameterDirection.Input));
                ora.AddParameter(pi_tel_fijo);
                var pi_celular = (new OracleParameter("fa_celular", OracleDbType.Varchar2, beneficiario.celularBeneficiario, ParameterDirection.Input));
                ora.AddParameter(pi_celular);
                var pi_vlr_asegurado = (new OracleParameter("fa_valor_asegurado", OracleDbType.Double, beneficiario.vlrAseguradoBeneficiario, ParameterDirection.Input));
                ora.AddParameter(pi_vlr_asegurado);
                var pi_parentesco = (new OracleParameter("fa_codigo_parentesco", OracleDbType.Double, beneficiario.parentezco, ParameterDirection.Input));
                ora.AddParameter(pi_parentesco);
                var pi_fecha = (new OracleParameter("ldt_fecha_nacto", OracleDbType.Date, fch_nacimiento, ParameterDirection.Input));
                ora.AddParameter(pi_fecha);
                var pi_genero = (new OracleParameter("fa_genero", OracleDbType.Double, beneficiario.gender, ParameterDirection.Input));
                ora.AddParameter(pi_genero);
                var pi_edad = (new OracleParameter("fa_edad", OracleDbType.Double, beneficiario.edad, ParameterDirection.Input));
                ora.AddParameter(pi_edad);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_ErrorCode);

                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("DLGWFC_BEN_FAMI_POLIZA");

                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.tiene_seguros", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public OutBeneficiariosPoliza aseguradoPoliza(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = null;
            OutBeneficiariosPoliza response = new OutBeneficiariosPoliza();
            Beneficiarios_Poliza beneficiario;
            List<Beneficiarios_Poliza> list = new List<Beneficiarios_Poliza>();
            try
            {
                command = "SELECT BP.ID_POLIZA, BP.PRIMER_NOMBRE||' '||BP.SEGUNDO_NOMBRE||' '||BP.PRIMER_APELLIDO||' '||BP.SEGUNDO_APELLIDO NOMBRE_BENEFICIARIO, TO_CHAR(BP.fecha_nacto,'DD/MM/YYYY') fecha_nacto, P.NOMBRE_PARENTESCO FROM bbs_polizas_beneficiarios_2 BP, FORMULARIO F, dfppar19 P ";
                command += string.Format("WHERE F.NUMERO_FOLDER = '{0}' ", folder);
                command += "AND F.ID_POLIZA = BP.ID_POLIZA AND BP.CODIGO_PARENTESCO = P.CODIGO_PARENTESCO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    beneficiario = new Beneficiarios_Poliza();
                    beneficiario.nombre = DBNull.Value.Equals(rdr["NOMBRE_BENEFICIARIO"]) ? "" : rdr["NOMBRE_BENEFICIARIO"].ToString();
                    beneficiario.fecha_ncto = DBNull.Value.Equals(rdr["fecha_nacto"]) ? "" : rdr["fecha_nacto"].ToString();
                    beneficiario.parentesco = DBNull.Value.Equals(rdr["NOMBRE_PARENTESCO"]) ? "" : rdr["NOMBRE_PARENTESCO"].ToString();
                    beneficiario.id_poliza = DBNull.Value.Equals(rdr["ID_POLIZA"]) ? 0 : double.Parse(rdr["ID_POLIZA"].ToString());
                    list.Add(beneficiario);
                }
                rdr.Close();
                response.listBeneficiario = list;
                response.msg = new Response();
                response.msg.errorCode = "0";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.beneficiariosPoliza" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        public OutBeneficiariosPoliza aseguradoFamiliarPoliza(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = null;
            OutBeneficiariosPoliza response = new OutBeneficiariosPoliza();
            Beneficiarios_Poliza beneficiario;
            List<Beneficiarios_Poliza> list = new List<Beneficiarios_Poliza>();
            try
            {
                command = "SELECT BP.ID_POLIZA, BP.PRIMER_NOMBRE||' '||BP.SEGUNDO_NOMBRE||' '||BP.PRIMER_APELLIDO||' '||BP.SEGUNDO_APELLIDO NOMBRE_BENEFICIARIO, TO_CHAR(BP.fecha_nacto,'DD/MM/YYYY') fecha_nacto, P.NOMBRE_PARENTESCO FROM bbs_polizas_beneficiarios BP, FORMULARIO F, dfppar19 P ";
                command += string.Format("WHERE F.NUMERO_FOLDER = '{0}' ", folder);
                command += "AND F.ID_POLIZA = BP.ID_POLIZA AND BP.CODIGO_PARENTESCO = P.CODIGO_PARENTESCO";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    beneficiario = new Beneficiarios_Poliza();
                    beneficiario.nombre = DBNull.Value.Equals(rdr["NOMBRE_BENEFICIARIO"]) ? "" : rdr["NOMBRE_BENEFICIARIO"].ToString();
                    beneficiario.fecha_ncto = DBNull.Value.Equals(rdr["fecha_nacto"]) ? "" : rdr["fecha_nacto"].ToString();
                    beneficiario.parentesco = DBNull.Value.Equals(rdr["NOMBRE_PARENTESCO"]) ? "" : rdr["NOMBRE_PARENTESCO"].ToString();
                    beneficiario.id_poliza = DBNull.Value.Equals(rdr["ID_POLIZA"]) ? 0 : double.Parse(rdr["ID_POLIZA"].ToString());
                    list.Add(beneficiario);
                }
                rdr.Close();
                response.listBeneficiario = list;
                response.msg = new Response();
                response.msg.errorCode = "0";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "Error " + ex.ToString();
                throw new Exception("ProfileDAO.beneficiariosPoliza" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /*Danny 2019 10*/
        public string[] getCorreoAuxiliarSuperior(string folder, double sucursal)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            string command = null;
            string[] emailAuxiliar = new string[2];
            try
            {
                /*command = $@"select B.correo_ele_notificacion as correo_sup
                            from bbs_liqcom_param_estruc_ccial,
                                   bbs_liqcom2_asesores A,
                                   bbs_liqcom2_asesores B,
                                   formulario C
                            where  A.codigo_asesor = bbs_liqcom_param_estruc_ccial.codigo_ejecutico_nivel5 and
                                   B.codigo_asesor = bbs_liqcom_param_estruc_ccial.codigo_ejecutico_nivel4 and
                                   bbs_liqcom_param_estruc_ccial.codigo_ejecutico_nivel5 = C.CODIGO_ASESOR and
                                   c.numero_folder = {folder}";*/
                command = $@"SELECT CORREO_COORDINADOR, CORREO_AUXILIAR 
                             FROM BBS_LIQCOM2_SUCURSALES S, FORMULARIO F
                             WHERE F.NUMERO_FOLDER = {folder}
                               AND S.CODIGO_SUCURSAL = F.SUCURSAL";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    emailAuxiliar[0] = DBNull.Value.Equals(rdr["CORREO_COORDINADOR"]) ? string.Empty : (rdr["CORREO_COORDINADOR"].ToString());
                    emailAuxiliar[1] = DBNull.Value.Equals(rdr["CORREO_AUXILIAR"]) ? string.Empty : (rdr["CORREO_AUXILIAR"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.getCorreoAuxiliarSuperior" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return emailAuxiliar;
        }

        public List<OutComentarios> getComentarios(string folder)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutComentarios data = new OutComentarios();
            List<OutComentarios> list = new List<OutComentarios>();
            string command = null;
            try
            {
                command = $@"select comentario, fecha from 
                            (
                            select comentarios comentario, null fecha, folio from BITACORA_DISPERSADOSBMX
                            union
                            select documentos || '-' || motivo_pendiente || '-' || comentarios, fecha_envio_pendiente, folio from BITACORA_DETPENDBMX
                              union
                            select comentarios, null, folio from BITACORA_PENDIENTESBMX
                            )j
                            where trim(comentario) <> '--' and folio = '{folder}'";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data = new OutComentarios();
                    data.fecha = DBNull.Value.Equals(rdr["fecha"]) ? "" : rdr["fecha"].ToString();
                    data.comentario = DBNull.Value.Equals(rdr["comentario"]) ? "" : rdr["comentario"].ToString();
                    list.Add(data);
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.getComentarios" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return list;
        }

        public OutDashBoard getDashBoard()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            OutDashBoard data = new OutDashBoard();
            string command = null;
            try
            {
                data = new OutDashBoard();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'NUEVA'";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Nueva = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Devolucion Auxiliar'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Devolucion_Auxiliar = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Recuperado Asesor'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Recuperado_Asesor = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Enviada'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Enviada = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Pre Capturada'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Pre_Capturada = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Recibida'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Recibida = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Recibida'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Recibida = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Asignado'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Asignado = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Devolucion'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Devolucion = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Recuperado'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Recuperado = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Pendiente Autorizacion'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Pendiente_Autorizacion = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Autorizado'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Autorizado = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Atendida'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Atendida = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Cancelada'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Cancelada = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
                command = @"select count(estado_solicitud) estado from formulario where estado_solicitud = 'Cancelada Portal'";
                rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data.Cancelada_Portal = DBNull.Value.Equals(rdr["estado"]) ? "0" : (rdr["estado"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.getComentarios" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        /*Danny 2019 10*/
        public double findEntidad(string entidad)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            double data = 0;
            string command = null;
            try
            {
                command = "SELECT COD_ENTIDAD FROM BBS_LIQCOM2_ENTIDADES ";
                command += string.Format("WHERE UPPER(NOMBRE_ENTIDAD) LIKE '{0}'", entidad);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data = DBNull.Value.Equals(rdr["COD_ENTIDAD"]) ? 0 : double.Parse(rdr["COD_ENTIDAD"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.findEntidad" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public double findMunicipio(string municipio)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            double data = 0;
            string command = null;
            try
            {
                command = "SELECT COD_MUNICIPIO FROM BBS_LIQCOM2_MUNICIPIOS ";
                command += string.Format("WHERE UPPER(NOMBRE_MUNICIPIO) LIKE '{0}'", municipio);
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    data = DBNull.Value.Equals(rdr["COD_MUNICIPIO"]) ? 0 : double.Parse(rdr["COD_MUNICIPIO"].ToString());
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.findMunicipio" + command, ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public string monto_escrito(double valor)
        {
            string monto = string.Empty;
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            try
            {
                var pi_genero = (new OracleParameter("fa_valor", OracleDbType.Double, valor, ParameterDirection.Input));
                ora.AddParameter(pi_genero);
                var po_monto = (new OracleParameter("fa_monto_escrito", OracleDbType.Varchar2, ParameterDirection.Output));
                po_monto.Size = 300;
                ora.AddParameter(po_monto);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_ErrorCode);

                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_monto_escrito");

                monto = ora.GetParameter("fa_monto_escrito").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.monto_escrito", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return monto;
        }
        public Response cancelarFolio(string folder)
        {
            Response data = new Response();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            try
            {

                var pi_folder = (new OracleParameter("fa_folder", OracleDbType.Varchar2, folder, ParameterDirection.Input));
                ora.AddParameter(pi_folder);
                var po_ErrorCode = new OracleParameter("fa_Error", OracleDbType.Double, ParameterDirection.Output);
                ora.AddParameter(po_ErrorCode);
                var po_ErrorMessage = new OracleParameter("fa_Descripcion_Error", OracleDbType.Varchar2, ParameterDirection.Output);
                po_ErrorMessage.Size = 300;
                ora.AddParameter(po_ErrorMessage);
                ora.ExecuteProcedureNonQuery("sp_cancelar_folio");

                data.errorCode = ora.GetParameter("fa_Error").ToString();
                data.errorMessage = ora.GetParameter("fa_Descripcion_Error").ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("ProfileDAO.cancelarFolio", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return data;
        }
        public arbolCarpetas arbolCarpetas() {
            var command = string.Empty;
            arbolCarpetas arbol = new arbolCarpetas();
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            var ora = new OracleServer(connectionString);
            try
            {
                command = "SELECT * FROM bbs_liqcom_param_directorioimg";
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    arbol = new arbolCarpetas();
                    arbol.ruta_raiz = DBNull.Value.Equals(rdr["RUTA_RAIZ"]) ? "" : rdr["RUTA_RAIZ"].ToString();
                    arbol.carpeta1 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE1"]) ? "" : rdr["NOMBRE_PAQUETE1"].ToString();
                    arbol.carpeta2 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE2"]) ? "" : rdr["NOMBRE_PAQUETE2"].ToString();
                    arbol.carpeta3 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE3"]) ? "" : rdr["NOMBRE_PAQUETE3"].ToString();
                    arbol.carpeta4 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE4"]) ? "" : rdr["NOMBRE_PAQUETE4"].ToString();
                    arbol.carpeta5 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE5"]) ? "" : rdr["NOMBRE_PAQUETE5"].ToString();
                    arbol.carpeta6 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE6"]) ? "" : rdr["NOMBRE_PAQUETE6"].ToString();
                    arbol.carpeta7 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE7"]) ? "" : rdr["NOMBRE_PAQUETE7"].ToString();
                    arbol.carpeta8 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE8"]) ? "" : rdr["NOMBRE_PAQUETE8"].ToString();
                    arbol.carpeta9 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE9"]) ? "" : rdr["NOMBRE_PAQUETE9"].ToString();
                    arbol.carpeta10 = DBNull.Value.Equals(rdr["NOMBRE_PAQUETE10"]) ? "" : rdr["NOMBRE_PAQUETE10"].ToString();
                }
                rdr.Close();
                arbol.msg = new Response();
                arbol.msg.errorCode = "0";
                arbol.msg.errorMessage = "Exito";
            }
            catch (Exception e)
            {
                arbol.msg = new Response();
                arbol.msg.errorCode = "-10";
                arbol.msg.errorMessage = "Error Consultando el árbol de Carpetas";
                throw new Exception("ProfileDAO.arbolCarpetas", e);
            }
            finally
            {
                ora.Dispose();
            }
            return arbol;
        }
    }
}
