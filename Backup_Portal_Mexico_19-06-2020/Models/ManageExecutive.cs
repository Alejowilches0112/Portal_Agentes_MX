using DAO;
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
    public class ManageExecutive
    {
        public Response UpdateExecutive(InUpdateExecutive input)
        {
            InUpdateExecutive userData = new InUpdateExecutive();
            Response response = new Response();
            try
            {
                ExecutiveDAO dao = new ExecutiveDAO();
                response = dao.UpdateExecutive(input);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageExecutive", "UpdateExecutive", ex, "");
            }
            return response;
        }

        public OutExecutiveInformation GetExecutiveInformation(string executiveID, double asesor)
        {
            OutExecutiveInformation response = new OutExecutiveInformation();
            try
            {
                ExecutiveDAO dao = new ExecutiveDAO();
                response = dao.GetExecutiveInformation(executiveID, asesor);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageExecutive", "GetExecutiveInformation", ex, "");
            }
            return response;
        }

        public bool MapEntity(InUpdateExecutiveService inputData, InUpdateExecutive infoExecutive, ref Response response)
        {
            bool valid = true;
            string message = string.Empty;

            try
            {
                /* Validacion sobre primer nombre*/
                if (!string.IsNullOrEmpty(inputData.nombre1))
                {
                    infoExecutive.name1 = inputData.nombre1;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en primer nombre";
                    response.errorMessage = message;
                    response.errorCode = "400";
                    LogHelper.WriteLog("Models", "MangerExecutive", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.nombre1);
                    return valid;
                }

                /* Validacion sobre primer apellido*/
                if (!string.IsNullOrEmpty(inputData.apellido1))
                {
                    infoExecutive.surname1 = inputData.apellido1;
                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en primer apellido";
                    response.errorMessage = message;
                    response.errorCode = "400";
                    LogHelper.WriteLog("Models", "MangerExecutive", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.apellido1);
                    return valid;
                }

                infoExecutive.documentID = inputData.numDocto;

                if (!string.IsNullOrEmpty(inputData.nombre2))
                    infoExecutive.name2 = inputData.nombre2;

                if (!string.IsNullOrEmpty(inputData.apellido2))
                    infoExecutive.surname1 = inputData.apellido2;

                /*Validacion sobre fecha de nacimiento*/
                if (inputData.fechaNacimiento != null && inputData.fechaNacimiento.Length <= 10 && inputData.fechaNacimiento != string.Empty)
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    infoExecutive.birthDate = DateTime.ParseExact(inputData.fechaNacimiento.Substring(0, 10), "dd/MM/yyyy", provider);

                }
                else
                {
                    valid = false;
                    message = "Inconsistencias en Fecha Nacimiento";
                    response.errorMessage = message;
                    response.errorCode = "400";
                    LogHelper.WriteLog("Models", "MangerExecutive", "MapEntity", Helper.Utilities.ConvertToXml(inputData), "400-" + "|" + message, inputData.fechaNacimiento);
                    return valid;
                }

                if (!string.IsNullOrEmpty(inputData.lugarNac) && double.Parse(inputData.lugarNac) > 0)
                    infoExecutive.placeBirth = double.Parse(inputData.lugarNac);

                if (!string.IsNullOrEmpty(inputData.gender) && double.Parse(inputData.gender) > 0)
                    infoExecutive.gender = double.Parse(inputData.gender);

                if (!string.IsNullOrEmpty(inputData.civilStatus) && double.Parse(inputData.civilStatus) > 0)
                    infoExecutive.civilStatus = double.Parse(inputData.civilStatus);

                if (!string.IsNullOrEmpty(inputData.dirNotifica))
                    infoExecutive.notifyAddress = inputData.dirNotifica;

                if (!string.IsNullOrEmpty(inputData.DepartamentoID) && double.Parse(inputData.DepartamentoID) > 0)
                    infoExecutive.department = double.Parse(inputData.DepartamentoID);

                if (!string.IsNullOrEmpty(inputData.ciudad) && double.Parse(inputData.ciudad) > 0)
                    infoExecutive.city = double.Parse(inputData.ciudad);

                if (!string.IsNullOrEmpty(inputData.barrio) && double.Parse(inputData.barrio) > 0)
                    infoExecutive.neighborhood = double.Parse(inputData.barrio);

                if (!string.IsNullOrEmpty(inputData.celular))
                    infoExecutive.executivePhone = inputData.celular;

                if (!string.IsNullOrEmpty(inputData.telFijo))
                    infoExecutive.housePhone = inputData.telFijo;

                if (!string.IsNullOrEmpty(inputData.tipoVivienda) && double.Parse(inputData.tipoVivienda) > 0)
                    infoExecutive.housingType = double.Parse(inputData.tipoVivienda);

                if (!string.IsNullOrEmpty(inputData.correo))
                    infoExecutive.email = inputData.correo;

                if (!string.IsNullOrEmpty(inputData.estudios) && double.Parse(inputData.estudios) > 0)
                    infoExecutive.appliedStudies = double.Parse(inputData.estudios);

                if (!string.IsNullOrEmpty(inputData.correo))
                    infoExecutive.notifyEmail = inputData.correo;

                if (!string.IsNullOrEmpty(inputData.correoBayport))
                    infoExecutive.bayportEmail = inputData.correoBayport;

                if (!string.IsNullOrEmpty(inputData.celular))
                    infoExecutive.emergencyPhone = inputData.celular;

                if (!string.IsNullOrEmpty(inputData.numeroCuenta))
                    infoExecutive.bankAccount = inputData.numeroCuenta;

                if (!string.IsNullOrEmpty(inputData.tipoCuenta) && double.Parse(inputData.tipoCuenta) > 0)
                    infoExecutive.accountType = double.Parse(inputData.tipoCuenta);

                if (!string.IsNullOrEmpty(inputData.Banco) && double.Parse(inputData.Banco) > 0)
                    infoExecutive.entityBank = double.Parse(inputData.Banco);

                if (!string.IsNullOrEmpty(inputData.nombreConyuge))
                    infoExecutive.spouseName = inputData.nombreConyuge;

                if (!string.IsNullOrEmpty(inputData.cedulaConyuge))
                    infoExecutive.spouseID = inputData.cedulaConyuge;

                if (!string.IsNullOrEmpty(inputData.celularConyuge))
                    infoExecutive.spouseCellphone = inputData.celularConyuge;

                if (!string.IsNullOrEmpty(inputData.emailConyuge))
                    infoExecutive.spouseEmail = inputData.emailConyuge;

                inputData.activos = inputData.activos.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.activos) && double.Parse(inputData.activos) > 0)
                    infoExecutive.assets = double.Parse(inputData.activos);

                inputData.pasivos = inputData.pasivos.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.pasivos) && double.Parse(inputData.pasivos) > 0)
                    infoExecutive.liabilities = double.Parse(inputData.pasivos);

                inputData.ingresos = inputData.ingresos.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.ingresos) && double.Parse(inputData.ingresos) > 0)
                    infoExecutive.income = double.Parse(inputData.ingresos);

                inputData.gastos = inputData.gastos.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.gastos) && double.Parse(inputData.gastos) > 0)
                    infoExecutive.expenses = double.Parse(inputData.gastos);

                inputData.otrosIngresos = inputData.otrosIngresos.Replace(".", "");
                if (!string.IsNullOrEmpty(inputData.otrosIngresos) && double.Parse(inputData.otrosIngresos) > 0)
                    infoExecutive.otherIncome = double.Parse(inputData.otrosIngresos);

                if (!string.IsNullOrEmpty(inputData.AFP))
                    infoExecutive.afpNIT = inputData.AFP;

                if (!string.IsNullOrEmpty(inputData.ARP))
                    infoExecutive.arpNIT = inputData.ARP;

                if (!string.IsNullOrEmpty(inputData.EPS))
                    infoExecutive.epsNIT = inputData.EPS;

            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageExecutive", "MapEntity", ex, "");
                response.errorMessage = ex.InnerException.ToString();
                response.errorCode = "400";
            }
            return valid;
        }

        public OutExecutiveChilds GetExecutiveChilds(string executiveID, int level)
        {
            OutExecutiveChilds response = new OutExecutiveChilds();
            try
            {
                ExecutiveDAO dao = new ExecutiveDAO();
                response = dao.GetExecutiveChilds(executiveID, level);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageExecutive", "GetExecutiveChilds", ex, "");
            }
            return response;
        }
    }
}

