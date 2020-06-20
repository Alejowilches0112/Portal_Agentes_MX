using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FolioDAO
    {
        public OutFolioDetail GetFolioDetail(string executiveID, int reportType, DateTime startDate, DateTime endDate, string folioNumber, string child)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutFolioDetail response = new OutFolioDetail();
            var ora = new OracleServer(connectionString);

            FolioDetail detail;
            List<FolioDetail> list = new List<FolioDetail>();
            string command = string.Empty;

            try
            {
                command = "SELECT  folio, nombre_sucursal, nombre_region, nombre_division, nombre_asesor,  Monto_Solicitado, Monto_Aprobado, " +
                      " PLAZO, CUOTA,  FECHA_SOLICITUD,  FECHA_CAPTURA, FECHA_DESEMBOLSO,  ESTATUS  FROM   BBS_LIQCOM_V_FOLIOS WHERE ";

                switch (reportType)
                {
                    case 1:
                        command = command + string.Format(" BBS_LIQCOM_V_FOLIOS.cedula_asesor = '{0}' AND folio = '{1}'", executiveID, folioNumber);
                        break;
                    case 2:
                        command = command + string.Format(" BBS_LIQCOM_V_FOLIOS.cedula_asesor = '{0}' AND FECHA_SOLICITUD BETWEEN to_date('{1}', 'dd-mm-yyyy')  AND to_date('{2}', 'dd-mm-yyyy')", executiveID, startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 3:
                        command = command + string.Format(" BBS_LIQCOM_V_FOLIOS.cedula_asesor = '{0}' AND FECHA_DESEMBOLSO BETWEEN to_date('{1}', 'dd-mm-yyyy')  AND to_date('{2}', 'dd-mm-yyyy')", executiveID, startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 4:
                        command = command + string.Format(" BBS_LIQCOM_V_FOLIOS.cedula_asesor = '{0}' AND FECHA_APROBACION BETWEEN to_date('{1}', 'dd-mm-yyyy')  AND to_date('{2}', 'dd-mm-yyyy')", executiveID, startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 5:
                        command = command + string.Format(" BBS_LIQCOM_V_FOLIOS.cedula_asesor in  ('{0}') ", child);
                        break;

                }
                
                var rdr = ora.ExecuteCommand(command);
                while (rdr.Read())
                {
                    detail = new FolioDetail();
                    detail.folioNumber = DBNull.Value.Equals(rdr["folio"]) ? string.Empty : rdr["folio"].ToString();
                    detail.branch = DBNull.Value.Equals(rdr["nombre_sucursal"]) ? string.Empty : rdr["nombre_sucursal"].ToString();
                    detail.region = DBNull.Value.Equals(rdr["nombre_region"]) ? string.Empty : rdr["nombre_region"].ToString();
                    detail.division = DBNull.Value.Equals(rdr["nombre_division"]) ? string.Empty : rdr["nombre_division"].ToString();
                    detail.loanExecutive = DBNull.Value.Equals(rdr["nombre_asesor"]) ? string.Empty : rdr["nombre_asesor"].ToString();
                    detail.amount = DBNull.Value.Equals(rdr["Monto_Solicitado"]) ? 0 : double.Parse(rdr["Monto_Solicitado"].ToString());
                    detail.disbursementAmount = DBNull.Value.Equals(rdr["Monto_Aprobado"]) ? 0 : double.Parse(rdr["Monto_Aprobado"].ToString());
                    detail.term = DBNull.Value.Equals(rdr["PLAZO"]) ? 0 : int.Parse(rdr["PLAZO"].ToString());
                    detail.monthlyAmount = DBNull.Value.Equals(rdr["CUOTA"]) ? 0 : double.Parse(rdr["CUOTA"].ToString());
                    detail.requestDate = DBNull.Value.Equals(rdr["FECHA_SOLICITUD"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_SOLICITUD"].ToString()).ToString("dd/MM/yyyy");
                    detail.captureDate = DBNull.Value.Equals(rdr["FECHA_CAPTURA"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_CAPTURA"].ToString()).ToString("dd/MM/yyyy");
                    detail.disbursementDate = DBNull.Value.Equals(rdr["FECHA_DESEMBOLSO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_DESEMBOLSO"].ToString()).ToString("dd/MM/yyyy");
                    detail.loanStatus = DBNull.Value.Equals(rdr["ESTATUS"]) ? string.Empty : rdr["ESTATUS"].ToString();

                    list.Add(detail);
                }
                rdr.Close();
                response.loanFolioDetail = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("FolioDAO.GetFolioDetail", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;



        }
    }
}

