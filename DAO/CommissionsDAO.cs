using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CommissionsDAO
    {
        public OutCommissionsHeader GetCommissionsHeader( string executiveID )
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCommissionsHeader response = new OutCommissionsHeader();
            var ora = new OracleServer(connectionString);

            CommissionHeader commission;
            List<CommissionHeader> list = new List<CommissionHeader>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_CUENTA, CODIGO_SUCURSAL, NOMBRE_SUCURSAL, TIPO_PROMOTOR, NOMBRE_TIPO_PROMOTOR,CODIGO_PUESTO, NOMBRE_CODIGO_PUESTO, " +
                    " CODIGO_CATEGORIA, NOMBRE_CATEGORIA, CODIGO_CATEGORIA_ANT, NOMBRE_CATEGORIA_ANTERIOR, PUNTOS_ACUM_MES_ANTERIOR, COMISIONES_MES_ANTERIOR, " +
                    " CODIGO_EJECUTIVO, NOMBRE_EJECUTIVO, DIRECCION_NOTIFICACION, CEDULA, TELEFONO_EJECUTIVO, ESTADO, DESCRIPCION_ESTADO, CORREO_ELECTRONICO, " +
                    " CORREO_ELE_NOTIFICACION,CORREO_ELE_BAYPORT,FECHA_NACTO, CODIGO_JEFE_CCIAL, NOMBRE_JEFE_CCIAL,TELEFONO_JEFE_CCIAL,  ";
                command = command + string.Format(" CORREO_JEFE_CCIAL, SALDO_COMISION, RFC,NOMBRE_DIVISION, NOMBRE_REGION FROM BBS_LIQCOM_V_CUENTAS WHERE cedula = '{0}' ", executiveID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    commission = new CommissionHeader();
                    commission.accountCode = DBNull.Value.Equals(rdr["CODIGO_CUENTA"]) ? 0 : double.Parse(rdr["CODIGO_CUENTA"].ToString());
                    commission.branchCode = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    commission.branchName = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? string.Empty : rdr["NOMBRE_SUCURSAL"].ToString();
                    commission.executiveType = DBNull.Value.Equals(rdr["TIPO_PROMOTOR"]) ? 0 : double.Parse(rdr["TIPO_PROMOTOR"].ToString());
                    commission.executiveTypeName = DBNull.Value.Equals(rdr["NOMBRE_TIPO_PROMOTOR"]) ? string.Empty : rdr["NOMBRE_TIPO_PROMOTOR"].ToString();
                    commission.positionCode = DBNull.Value.Equals(rdr["CODIGO_PUESTO"]) ? 0 : double.Parse(rdr["CODIGO_PUESTO"].ToString());
                    commission.positionCodeName = DBNull.Value.Equals(rdr["NOMBRE_CODIGO_PUESTO"]) ? string.Empty : rdr["NOMBRE_CODIGO_PUESTO"].ToString();
                    commission.categoryCode = DBNull.Value.Equals(rdr["CODIGO_CATEGORIA"]) ? 0 : double.Parse(rdr["CODIGO_CATEGORIA"].ToString());
                    commission.categoryName = DBNull.Value.Equals(rdr["NOMBRE_CATEGORIA"]) ? string.Empty : rdr["NOMBRE_CATEGORIA"].ToString();
                    commission.previousCategoryCode = DBNull.Value.Equals(rdr["CODIGO_CATEGORIA_ANT"]) ? 0 : double.Parse(rdr["CODIGO_CATEGORIA_ANT"].ToString());
                    commission.previousCategoryName = DBNull.Value.Equals(rdr["NOMBRE_CATEGORIA_ANTERIOR"]) ? string.Empty : rdr["NOMBRE_CATEGORIA_ANTERIOR"].ToString();
                    commission.previousPointsAccum = DBNull.Value.Equals(rdr["PUNTOS_ACUM_MES_ANTERIOR"]) ? 0 : double.Parse(rdr["PUNTOS_ACUM_MES_ANTERIOR"].ToString());
                    commission.previousCommissionBalance = DBNull.Value.Equals(rdr["COMISIONES_MES_ANTERIOR"]) ? 0 : double.Parse(rdr["COMISIONES_MES_ANTERIOR"].ToString());
                    commission.executiveCode = DBNull.Value.Equals(rdr["CODIGO_EJECUTIVO"]) ? 0 : double.Parse(rdr["CODIGO_EJECUTIVO"].ToString());
                    commission.executiveName = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();
                    commission.notifyAddress = DBNull.Value.Equals(rdr["DIRECCION_NOTIFICACION"]) ? string.Empty : rdr["DIRECCION_NOTIFICACION"].ToString();
                    commission.documentID = DBNull.Value.Equals(rdr["CEDULA"]) ? string.Empty : rdr["CEDULA"].ToString();
                    commission.executivePhone = DBNull.Value.Equals(rdr["TELEFONO_EJECUTIVO"]) ? string.Empty : rdr["TELEFONO_EJECUTIVO"].ToString();
                    commission.state = DBNull.Value.Equals(rdr["ESTADO"]) ? 0 : double.Parse(rdr["ESTADO"].ToString());
                    commission.stateDescription = DBNull.Value.Equals(rdr["DESCRIPCION_ESTADO"]) ? string.Empty : rdr["DESCRIPCION_ESTADO"].ToString();
                    commission.email = DBNull.Value.Equals(rdr["CORREO_ELECTRONICO"]) ? string.Empty : rdr["CORREO_ELECTRONICO"].ToString();
                    commission.notificationEmail = DBNull.Value.Equals(rdr["CORREO_ELE_NOTIFICACION"]) ? string.Empty : rdr["CORREO_ELE_NOTIFICACION"].ToString();
                    commission.bayportEmail = DBNull.Value.Equals(rdr["CORREO_ELE_BAYPORT"]) ? string.Empty : rdr["CORREO_ELE_BAYPORT"].ToString();
                    commission.birthday = DBNull.Value.Equals(rdr["FECHA_NACTO"]) ? DateTime.Today.ToString("dd/MM/yyyy") : DateTime.Parse(rdr["FECHA_NACTO"].ToString()).ToString("dd/MM/yyyy");
                    commission.commercialBossCode = DBNull.Value.Equals(rdr["CODIGO_JEFE_CCIAL"]) ? 0 : double.Parse(rdr["CODIGO_JEFE_CCIAL"].ToString());
                    commission.commercialBossName = DBNull.Value.Equals(rdr["NOMBRE_JEFE_CCIAL"]) ? string.Empty : rdr["NOMBRE_JEFE_CCIAL"].ToString();
                    commission.commercialBossPhone = DBNull.Value.Equals(rdr["TELEFONO_JEFE_CCIAL"]) ? string.Empty : rdr["TELEFONO_JEFE_CCIAL"].ToString();
                    commission.commercialBossMail = DBNull.Value.Equals(rdr["CORREO_JEFE_CCIAL"]) ? string.Empty : rdr["CORREO_JEFE_CCIAL"].ToString();
                    commission.commissionBalance = DBNull.Value.Equals(rdr["SALDO_COMISION"]) ? 0 : double.Parse(rdr["SALDO_COMISION"].ToString());
                    commission.divisionName = DBNull.Value.Equals(rdr["NOMBRE_DIVISION"]) ? string.Empty : rdr["NOMBRE_DIVISION"].ToString();
                    commission.regionName = DBNull.Value.Equals(rdr["NOMBRE_REGION"]) ? string.Empty : rdr["NOMBRE_REGION"].ToString();
                    commission.rfc = DBNull.Value.Equals(rdr["RFC"]) ? string.Empty : rdr["RFC"].ToString();
                    list.Add(commission);
                }
                rdr.Close();
                response.lstCommissionHeader = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("CommissionsDAO.GetCommissionsHeader", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="executiveID"></param>
        /// <param name="creditNumber"></param>
        /// <param name="reportType">1 Por fecha de desembolso, 2 Por fecha de pago, 3 Por credito</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public OutMovesCommission GetMovesCommissions(string executiveID, double accountNumber, string creditNumber, int reportType, DateTime startDate, DateTime endDate, string childs)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutMovesCommission response = new OutMovesCommission();
            var ora = new OracleServer(connectionString);

            MovementCommission movement;
            List<MovementCommission> list = new List<MovementCommission>();
            string command = string.Empty;

            try
            {
                command = "SELECT CONCEPTO, NOMBRE_CONCEPTO, VALOR_DB, VALOR_CR, FECHA_PAGO, NRO_CREDITO, FECHA_DESEMBOLSO, NUEVO_RENOVADO," +
                    "DESEMBOLSO_TOTAL, COLOCACION_TOTAL, PORC_COMISION, IMPORTE_COMISION, TIPO_ESQUEMA_PAGO, OBSERVACIONES, NATURALEZA, " +
                    " PORC_BONOS, IMPORTE_BONOS, TOTAL_PAGADO FROM BBS_LIQCOM_V_MOVIMIENTOSMX WHERE ";

                switch (reportType)
                {
                    case 1:
                        command = command + string.Format("  cedula = '{0}' AND CODIGO_CUENTA = {1} ", executiveID, accountNumber);
                        command = command + string.Format(" AND FECHA_DESEMBOLSO BETWEEN to_date('{0}', 'dd-mm-yyyy')  AND to_date('{1}', 'dd-mm-yyyy')", startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 2:
                        command = command + string.Format("  cedula = '{0}' AND CODIGO_CUENTA = {1} ", executiveID, accountNumber);
                        command = command + string.Format(" AND FECHA_PAGO BETWEEN to_date('{0}', 'dd-mm-yyyy')  AND to_date('{1}', 'dd-mm-yyyy')", startDate.ToString("dd-MM-yyyy"), endDate.ToString("dd-MM-yyyy"));
                        break;
                    case 3:
                        command = command + string.Format("  cedula = '{0}' AND CODIGO_CUENTA = {1} ", executiveID, accountNumber);
                        command = command + string.Format(" AND NRO_CREDITO = {0}", creditNumber);
                        break;
                    case 4: 
                        command = command + string.Format("  cedula in ({0}) ", childs);
                        break;
                }

                LogHelper.WriteLog("DAO", "CommisionsDAO", "GetMovesCommissions" + command, new Exception(), "");
                var rdr = ora.ExecuteCommand(command);
               
                while (rdr.Read())
                {
                    movement = new MovementCommission();
                    movement.conceptCode = DBNull.Value.Equals(rdr["concepto"]) ? 0 : double.Parse(rdr["concepto"].ToString());
                    movement.conceptName = DBNull.Value.Equals(rdr["NOMBRE_CONCEPTO"]) ? string.Empty : rdr["NOMBRE_CONCEPTO"].ToString();
                    movement.ammountDB = DBNull.Value.Equals(rdr["valor_db"]) ? 0 : double.Parse(rdr["valor_db"].ToString());
                    movement.ammountCR = DBNull.Value.Equals(rdr["valor_cr"]) ? 0 : double.Parse(rdr["valor_cr"].ToString());
                    movement.paymentDate = DBNull.Value.Equals(rdr["fecha_pago"]) ? DateTime.Today : DateTime.Parse( rdr["fecha_pago"].ToString());
                    movement.creditNumber = DBNull.Value.Equals(rdr["nro_credito"]) ? 0 : double.Parse(rdr["nro_credito"].ToString());
                    movement.disbursementDate = DBNull.Value.Equals(rdr["fecha_desembolso"]) ? DateTime.Today : DateTime.Parse(rdr["fecha_desembolso"].ToString());
                    movement.newRenovated = DBNull.Value.Equals(rdr["nuevo_renovado"]) ? string.Empty : rdr["nuevo_renovado"].ToString();
                    movement.disbursementAmount = DBNull.Value.Equals(rdr["desembolso_total"]) ? 0 : double.Parse(rdr["desembolso_total"].ToString());
                    movement.investmentAmount = DBNull.Value.Equals(rdr["colocacion_total"]) ? 0 : double.Parse(rdr["colocacion_total"].ToString());
                    movement.commissionPercentage = DBNull.Value.Equals(rdr["porc_comision"]) ? 0 : double.Parse(rdr["porc_comision"].ToString());
                    movement.commissionAmount = DBNull.Value.Equals(rdr["importe_comision"]) ? 0 : double.Parse(rdr["importe_comision"].ToString());
                    movement.schemePayment = DBNull.Value.Equals(rdr["tipo_esquema_pago"]) ? string.Empty : rdr["tipo_esquema_pago"].ToString();
                    movement.observations = DBNull.Value.Equals(rdr["OBSERVACIONES"]) ? string.Empty : rdr["OBSERVACIONES"].ToString();
                    movement.nature = DBNull.Value.Equals(rdr["NATURALEZA"]) ? string.Empty : rdr["NATURALEZA"].ToString();
                    movement.bonnusPercentage = DBNull.Value.Equals(rdr["PORC_BONOS"]) ? 0 : double.Parse(rdr["PORC_BONOS"].ToString());
                    movement.bonnusAmount = DBNull.Value.Equals(rdr["IMPORTE_BONOS"]) ? 0 : double.Parse(rdr["IMPORTE_BONOS"].ToString());
                    movement.paymentTotal = DBNull.Value.Equals(rdr["TOTAL_PAGADO"]) ? 0 : double.Parse(rdr["TOTAL_PAGADO"].ToString());
                    list.Add(movement);
                }
                rdr.Close();
                response.lstMovesCommission = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("CommissionsDAO.GetMovesCommissions", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutBalancesCommision GetBalancesCommissions(string executiveID, double accountNumber)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutBalancesCommision response = new OutBalancesCommision();
            var ora = new OracleServer(connectionString);

            BalancesCommision balance;
            List<BalancesCommision> list = new List<BalancesCommision>();
            string command = string.Empty;

            try
            {
                command = "SELECT CODIGO_CONCEPTO, NOMBRE_CONCEPTO, SALDO_CONCEPTO, CODIGO_CUENTA, CODIGO_EJECUTIVO, CEDULA, NOMBRE_EJECUTIVO ";
                command = command + string.Format(" FROM BBS_LIQCOM_V_SALDOS WHERE cedula = '{0}' AND CODIGO_CUENTA = {1} ", executiveID, accountNumber);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    balance = new BalancesCommision();
                    balance.conceptCode = DBNull.Value.Equals(rdr["CODIGO_CONCEPTO"]) ? 0 : double.Parse(rdr["CODIGO_CONCEPTO"].ToString());
                    balance.conceptName = DBNull.Value.Equals(rdr["NOMBRE_CONCEPTO"]) ? string.Empty : rdr["NOMBRE_CONCEPTO"].ToString();
                    balance.conceptBalance = DBNull.Value.Equals(rdr["SALDO_CONCEPTO"]) ? 0 : double.Parse(rdr["SALDO_CONCEPTO"].ToString());
                    balance.accountCode = DBNull.Value.Equals(rdr["CODIGO_CUENTA"]) ? 0 : double.Parse(rdr["CODIGO_CUENTA"].ToString());
                    balance.executiveCode = DBNull.Value.Equals(rdr["CODIGO_EJECUTIVO"]) ? 0 : double.Parse(rdr["CODIGO_EJECUTIVO"].ToString());
                    balance.documentID = DBNull.Value.Equals(rdr["CEDULA"]) ? string.Empty : rdr["CEDULA"].ToString();
                    balance.executiveName = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();       
                    list.Add(balance);
                }
                rdr.Close();
                response.lstBalancesCommision = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("CommissionsDAO.GetBalancesCommissions", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

    }
}
