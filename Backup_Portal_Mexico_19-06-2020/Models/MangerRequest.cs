using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MangerRequest
    {
        public bool MapEntity(InProfileService inputData, ProfileCustomer objProfile, ref OutOffer objOffer)
        {
            bool valid = true;
            string message = string.Empty;
            double maxAmount = 75000000, minAmountEmployee = 500000, minAmountCustomer = 2000000;
            try
            {
                /* Validacion sobre Numero de Documento*/
                inputData.numDoc = inputData.numDoc.Replace(".", "");
                if ((int.Parse(inputData.numDoc) > 0) && (inputData.numDoc.Length <= 12))
                {
                    objProfile.identificationNumber = inputData.numDoc.Replace(".", "");
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en Documento";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "401";

                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntitmonthlyPaymentRefinancingy", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }
                /* Validacion sobre primer nombre*/
                if (!string.IsNullOrEmpty(inputData.pNombre))
                {
                    objProfile.name = inputData.pNombre;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en primer nombre";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "402";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /* Segundo nombre */


                /* Validacion sobre primer apellido*/
                if (!string.IsNullOrEmpty(inputData.pApellido))
                {
                    objProfile.surname = inputData.pApellido;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en primer apellido";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "403";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                // Segundo apellido
                objProfile.secondSurname = inputData.sApellido != null ? inputData.sApellido : string.Empty;


                // Validacion celular
                if (!string.IsNullOrEmpty(inputData.Celular))
                {
                    objProfile.phone = inputData.Celular;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en el celular";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "404";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }
                /*Validacion sobre fecha de Nacimiento*/

                if (inputData.fecNac != null && inputData.fecNac.Length <= 10 && inputData.fecNac != string.Empty)
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    objProfile.birthday = DateTime.ParseExact(inputData.fecNac.Substring(0, 10), "dd/MM/yyyy", provider);

                    var anios = DateTime.Today.Year - objProfile.birthday.Year;

                    if ((anios < 18) || (anios > 80))
                    {
                        valid = false;
                        message = "Inconsistencias en la edad, esta debe ser entre 18 y 80 años ";
                        objOffer.msg.errorMessage = message;
                        objOffer.msg.errorCode = "405";
                        LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                        return valid;
                    }

                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en Fecha Nacimiento";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "406";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion sobre pagaduria*/
                if (inputData.pagaduria > 0)
                {
                    objProfile.payable = inputData.pagaduria;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en pagaduria";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "407";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion sobre fecha de ingreso*/
                if (inputData.fecNac != null && inputData.fecNac.Length <= 10 && inputData.fecNac != string.Empty)
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    objProfile.admissionDate = DateTime.ParseExact(inputData.fecNac.Substring(0, 10), "dd/MM/yyyy", provider);

                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en Fecha Nacimiento";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "408";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion sobre tipo de contrato
                if (inputData.tipocontrato > 0)
                {
                    objProfile.contractType = inputData.tipocontrato;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en tipo de contrato";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "409";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }*/

                /*Validacion sobre tipo de credito*/
                if (inputData.tipoCredito > 0)
                {
                    objProfile.LoanType = inputData.tipoCredito;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en tipo de credito";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "410";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion sobre linea de credito*/
                if (inputData.lineCredito > 0)
                {
                    objProfile.LoanLine = inputData.lineCredito;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en linea de credito";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "411";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion sobre estado
                if (inputData.estado != "0")
                {
                    objProfile.activePensioner = int.Parse(inputData.estado);
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en estado";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "412";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }*/

                /*Validacion sobre rango
                if (!string.IsNullOrEmpty(inputData.rango))
                {
                    objProfile.teacher = inputData.rango;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en rango";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "413";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }*/

                /*Validacion sobre monto solicitado*/
                inputData.monto = inputData.monto.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.monto) && int.Parse(inputData.monto) > 0)
                {
                    objProfile.amount = int.Parse(inputData.monto);

                    if (objProfile.LoanLine == 2007) // Se valida la linea de empleados
                    {
                        if (objProfile.amount < minAmountEmployee || objProfile.amount > maxAmount)
                        {
                            valid = false;
                            message = "Inconsistencias el monto solicitado debe estar entre 500.000 y 75.000.000";
                            objOffer.msg.errorMessage = message;
                            objOffer.msg.errorCode = "414";
                            LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                            return valid;
                        }
                    }
                    else if (objProfile.LoanLine == 2005)
                    {
                        if (objProfile.amount < minAmountCustomer || objProfile.amount > maxAmount)
                        {
                            valid = false;
                            message = "Inconsistencias el monto solicitado debe estar entre 2.000.000 y 75.000.000";
                            objOffer.msg.errorMessage = message;
                            objOffer.msg.errorCode = "415";
                            LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                            return valid;
                        }
                    }
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en monto solicitado";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "416";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }
                /*
                /*Descuentos
                objProfile.otherDiscounts = !string.IsNullOrEmpty(inputData.descuentos) ? int.Parse(inputData.descuentos.Replace(".", "")) : 0;
*/
                /*Otros ingresos
                objProfile.additionalIncome = !string.IsNullOrEmpty(inputData.otroingreso) ? int.Parse(inputData.otroingreso.Replace(".", "")) : 0;
*/
                /*Retefuente
                objProfile.retefuente = !string.IsNullOrEmpty(inputData.retefuente) ? int.Parse(inputData.retefuente.Replace(".", "")) : 0;
                */

                /*Validacion sobre Salario
                inputData.salario = !string.IsNullOrEmpty(inputData.salario) ? inputData.salario.Replace(".", "") : "0";
                if (!string.IsNullOrEmpty(inputData.salario) && int.Parse(inputData.salario) > 0)
                {
                    objProfile.salary = int.Parse(inputData.salario);
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en valor salario";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "417";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }*/

                /*Valor refinanciacion
                inputData.refinanciacion = !string.IsNullOrEmpty(inputData.refinanciacion) ? inputData.refinanciacion.Replace(".", "") : "0";
                if (!string.IsNullOrEmpty(inputData.refinanciacion))
                {
                    objProfile.monthlyPaymentRefinancing = int.Parse(inputData.refinanciacion);
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en valor de refinanciacion";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "418";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }*/

                /*Fondo solidaridad
                objProfile.solidarityFunds = !string.IsNullOrEmpty(inputData.solidaridad) ? int.Parse(inputData.solidaridad.Replace(".", "")) : 0;
*/
                /*Dcto Pension
                objProfile.pension = !string.IsNullOrEmpty(inputData.valorPension) ? int.Parse(inputData.valorPension.Replace(".", "")) : 0;
*/
                /*Dcto Salud
                objProfile.health = !string.IsNullOrEmpty(inputData.valorSalud) ? int.Parse(inputData.valorSalud.Replace(".", "")) : 0;
*/
                /*Compra de cartera
                objProfile.amountPurchasePortfolio = !string.IsNullOrEmpty(inputData.valorCartera) ? int.Parse(inputData.valorCartera.Replace(".", "")) : 0;
                if (objProfile.amountPurchasePortfolio > 0 )
                {
                    if (objProfile.amountPurchasePortfolio > objProfile.amount)
                    {
                        valid = false;
                        message = "Inconsistencias valor de compra de cartera debe ser menor al monto solicitado";
                        objOffer.msg.errorMessage = message;
                        objOffer.msg.errorCode = "419";
                        LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                        return valid;
                    }

                    if (inputData.cartera.Count == 0)
                    {
                        valid = false;
                        message = "Inconsistencias compra de cartera debe inlcuir por lo menos una compra";
                        objOffer.msg.errorMessage = message;
                        objOffer.msg.errorCode = "420";
                        LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                        return valid;

                    }
                }*/
                //else
                //{
                //    valid = false;
                //    message = "Inconsistencias en monto solicitado";
                //    objOffer.msg.errorMessage = message;
                //    objOffer.msg.errorCode = "400";
                //    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                //    return valid;
                //}

                /*Validacion convenio*/
                if (!string.IsNullOrEmpty(inputData.convenio))
                {
                    objProfile.agreement = inputData.convenio;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en el convenio";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "421";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /*Validacion celular*/
                if (!string.IsNullOrEmpty(inputData.Celular))
                {
                    objProfile.phone = inputData.Celular;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en el número de celular";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "422";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }

                /* Detalle compra de cartera*/
                if (inputData.cartera != null)
                {
                    if (inputData.cartera.Count > 0)
                    {
                        objProfile.lstPurchase = new List<PurchaseDetail>();
                        PurchaseDetail detail;
                        foreach (var cartera in inputData.cartera)
                        {
                            detail = new PurchaseDetail();
                            detail.balance = int.Parse(cartera.saldo.Replace(".", ""));
                            detail.entity = cartera.name;
                            detail.monthlyPayment = int.Parse(cartera.cuota.Replace(".", ""));
                            detail.desprendible = cartera.desprendible;
                            objProfile.lstPurchase.Add(detail);
                        }
                    }
                }


                objProfile.gender = inputData.gender;

                /*Asesor*/
                objProfile.agent = inputData.executiveCode;
                objProfile.branch = inputData.brachCode;
                objProfile.regional = inputData.regional;

                /*Validacion sobre plazo*/

                if (!string.IsNullOrEmpty(inputData.plazo) && int.Parse(inputData.plazo) > 0 && int.Parse(inputData.plazo) < 181)
                {
                    objProfile.term = int.Parse(inputData.plazo);
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en el plazo";
                    objOffer.msg.errorMessage = message;
                    objOffer.msg.errorCode = "423";
                    LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.numDoc);
                    return valid;
                }
                
                if (string.IsNullOrEmpty(inputData.folderNumber) )
                {
                    objProfile.folderNumber = 0;
                    objProfile.otro_intento = 0;
                }
                else
                {
                    if (inputData.folderNumber != "undefined")
                    {
                        objProfile.otro_intento = 1;
                        objProfile.folderNumber = int.Parse(inputData.folderNumber);
                    }
                }
                    


                objOffer.msg.errorMessage = "/Profile/OfferResult";
                objOffer.msg.errorCode = "200";

            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "MangerRequest", "MapEntity", ex, "");
                objOffer.msg.errorMessage = ex.InnerException.ToString();
                objOffer.msg.errorCode = "424";
            }
            return valid;
        }
    }
}
