using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ParametersDAO
    {
        /// <summary>
        /// Gets the civil status.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetCivilStatus</exception>

        public OutCivilStatus GetCivilStatus()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCivilStatus response = new OutCivilStatus();
            var ora = new OracleServer(connectionString);

            CivilStatus status;
            List<CivilStatus> list = new List<CivilStatus>();
            string command = string.Empty;

            try
            {
                command = "SELECT dfpecv09.codigo_civil, dfpecv09.nombre_civil FROM dfpecv09 WHERE CODIGO_CIVIL<> 0 ORDER BY dfpecv09.codigo_civil ASC ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    status = new CivilStatus();
                    status.statusID = DBNull.Value.Equals(rdr["codigo_civil"]) ? 0 : int.Parse(rdr["codigo_civil"].ToString());
                    status.statusName = DBNull.Value.Equals(rdr["nombre_civil"]) ? string.Empty : rdr["nombre_civil"].ToString();
                    list.Add(status);
                }
                rdr.Close();
                response.lstCivilStatus = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetCivilStatus", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetDepartments</exception>

        public OutDepartments GetDepartments()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutDepartments response = new OutDepartments();
            var ora = new OracleServer(connectionString);

            Departments department;
            List<Departments> list = new List<Departments>();
            string command = string.Empty;

            try
            {
                command = "SELECT distinct nom_depto as nombre_producto, cod_depto as codigo_producto FROM dfpctropoblado ORDER BY 1 ASC ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    department = new Departments();
                    department.departmentID = DBNull.Value.Equals(rdr["codigo_producto"]) ? 0 : int.Parse(rdr["codigo_producto"].ToString());
                    department.departmentName = DBNull.Value.Equals(rdr["nombre_producto"]) ? string.Empty : rdr["nombre_producto"].ToString();
                    list.Add(department);
                }
                rdr.Close();
                response.lstDepartments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetDepartments", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="departmentID">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetCities</exception>

        public OutDepartments GetCities(int departmentID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutDepartments response = new OutDepartments();
            var ora = new OracleServer(connectionString);

            Departments department;
            List<Departments> list = new List<Departments>();
            string command = string.Empty;

            try
            {
                command = "select distinct A.cod_munic, A.nom_munic, A.cod_depto from dfpctropoblado A where CONSECUTIVO > 0 ";
                command = command + string.Format("  and A.cod_depto = {0} ", departmentID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    department = new Departments();
                    department.municipalityID = DBNull.Value.Equals(rdr["cod_munic"]) ? 0 : int.Parse(rdr["cod_munic"].ToString());
                    department.municipalityName = DBNull.Value.Equals(rdr["nom_munic"]) ? string.Empty : rdr["nom_munic"].ToString();
                    department.departmentID = DBNull.Value.Equals(rdr["cod_depto"]) ? 0 : int.Parse(rdr["cod_depto"].ToString());  
                    list.Add(department);
                }
                rdr.Close();
                response.lstDepartments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetCities", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the neighborhood.
        /// </summary>
        /// <param name="municipalityID">The municipality identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetNeighborhood</exception>

        public OutDepartments GetNeighborhood(int municipalityID)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutDepartments response = new OutDepartments();
            var ora = new OracleServer(connectionString);

            Departments department;
            List<Departments> list = new List<Departments>();
            string command = string.Empty;

            try
            {
                command = " select cod_ctro_pobla, nom_ctro_pobla, cod_munic from dfpctropoblado ";
                command = command + string.Format("  where cod_munic = {0} ", municipalityID);
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    department = new Departments();
                    department.populationID = DBNull.Value.Equals(rdr["cod_ctro_pobla"]) ? 0 : int.Parse(rdr["cod_ctro_pobla"].ToString());
                    department.populationName = DBNull.Value.Equals(rdr["nom_ctro_pobla"]) ? string.Empty : rdr["nom_ctro_pobla"].ToString();
                    department.municipalityID = DBNull.Value.Equals(rdr["cod_munic"]) ? 0 : int.Parse(rdr["cod_munic"].ToString());
                    list.Add(department);
                }
                rdr.Close();
                response.lstDepartments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetNeighborhood", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the type of the housing.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetHousingType</exception>

        public OutHousingType GetHousingType()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutHousingType response = new OutHousingType();
            var ora = new OracleServer(connectionString);

            HousingType housing;
            List<HousingType> list = new List<HousingType>();
            string command = string.Empty;

            try
            {
                command = " SELECT tipos_vivienda.codigo, tipos_vivienda.descripcion FROM tipos_vivienda ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    housing = new HousingType();
                    housing.housingID = DBNull.Value.Equals(rdr["codigo"]) ? 0 : int.Parse(rdr["codigo"].ToString());
                    housing.housingName = DBNull.Value.Equals(rdr["descripcion"]) ? string.Empty : rdr["descripcion"].ToString();
                    list.Add(housing);
                }
                rdr.Close();
                response.lstHousingType = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetHousingType", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the applied studies.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetAppliedStudies</exception>

        public OutAppliedStudies GetAppliedStudies()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutAppliedStudies response = new OutAppliedStudies();
            var ora = new OracleServer(connectionString);

            AppliedStudies studies;
            List<AppliedStudies> list = new List<AppliedStudies>();
            string command = string.Empty;

            try
            {
                command = " SELECT DFPNES08.codigo_estudio, DFPNES08.nombre_estudio FROM DFPNES08  WHERE DFPNES08.CODIGO_ESTUDIO > 0 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    studies = new AppliedStudies();
                    studies.studiesID = DBNull.Value.Equals(rdr["codigo_estudio"]) ? 0 : int.Parse(rdr["codigo_estudio"].ToString());
                    studies.studiesName = DBNull.Value.Equals(rdr["nombre_estudio"]) ? string.Empty : rdr["nombre_estudio"].ToString();
                    list.Add(studies);
                }
                rdr.Close();
                response.lstAppliedStudies = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetAppliedStudies", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the afp.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetAFP</exception>

        public OutAFP GetAFP()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutAFP response = new OutAFP();
            var ora = new OracleServer(connectionString);

            AFP afp;
            List<AFP> list = new List<AFP>();
            string command = string.Empty;

            try
            {
                command = " SELECT  NIT, NOMBRE, CODIGO_MINISTERIO  FROM BBS_WFC_PAR_AFP where nit> 0 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    afp = new AFP();
                    afp.afpNIT = DBNull.Value.Equals(rdr["NIT"]) ? string.Empty : rdr["NIT"].ToString();
                    afp.afpName = DBNull.Value.Equals(rdr["NOMBRE"]) ? string.Empty : rdr["NOMBRE"].ToString();
                    afp.afpCodeMinistry = DBNull.Value.Equals(rdr["CODIGO_MINISTERIO"]) ? string.Empty : rdr["CODIGO_MINISTERIO"].ToString();
                    list.Add(afp);
                }
                rdr.Close();
                response.lstAFP = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetAFP", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the arp.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetARP</exception>

        public OutARP GetARP()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutARP response = new OutARP();
            var ora = new OracleServer(connectionString);

            ARP arp;
            List<ARP> list = new List<ARP>();
            string command = string.Empty;

            try
            {
                command = " SELECT  NIT, NOMBRE, CODIGO_MINISTERIO  FROM BBS_WFC_PAR_ARP where nit> 0 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    arp = new ARP();
                    arp.arpNIT = DBNull.Value.Equals(rdr["NIT"]) ? string.Empty : rdr["NIT"].ToString();
                    arp.arpName = DBNull.Value.Equals(rdr["NOMBRE"]) ? string.Empty : rdr["NOMBRE"].ToString();
                    arp.arpCodeMinistry = DBNull.Value.Equals(rdr["CODIGO_MINISTERIO"]) ? string.Empty : rdr["CODIGO_MINISTERIO"].ToString();
                    list.Add(arp);
                }
                rdr.Close();
                response.lstARP = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetARP", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the eps.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetEPS</exception>

        public OutEPS GetEPS()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutEPS response = new OutEPS();
            var ora = new OracleServer(connectionString);

            EPS eps;
            List<EPS> list = new List<EPS>();
            string command = string.Empty;

            try
            {
                command = " SELECT  NIT, NOMBRE, CODIGO_MINISTERIO  FROM BBS_WFC_PAR_EPS where nit> 0 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    eps = new EPS();
                    eps.epsNIT = DBNull.Value.Equals(rdr["NIT"]) ? string.Empty : rdr["NIT"].ToString();
                    eps.epsName = DBNull.Value.Equals(rdr["NOMBRE"]) ? string.Empty : rdr["NOMBRE"].ToString();
                    eps.epsCodeMinistry = DBNull.Value.Equals(rdr["CODIGO_MINISTERIO"]) ? string.Empty : rdr["CODIGO_MINISTERIO"].ToString();
                    list.Add(eps);
                }
                rdr.Close();
                response.lstEPS = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetEPS", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }
        /// <summary>
        /// Gets the banks.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetBanks</exception>

        public OutBanks GetBanks()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutBanks response = new OutBanks();
            var ora = new OracleServer(connectionString);

            Banks bank;
            List<Banks> list = new List<Banks>();
            string command = string.Empty;

            try
            {
                command = " select trim(A.BCO_NIT_ENTIDAD)  as BCO_NIT_ENTIDAD, trim(A.Bco_Nombre_Entidad )  as Bco_Nombre_Entidad ";
                command = command + " from dfcpsn02_bancos A   where A.nui > 0  order by 2";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    bank = new Banks();
                    bank.entityNIT = DBNull.Value.Equals(rdr["BCO_NIT_ENTIDAD"]) ? string.Empty : rdr["BCO_NIT_ENTIDAD"].ToString();
                    bank.entityName = DBNull.Value.Equals(rdr["Bco_Nombre_Entidad"]) ? string.Empty : rdr["Bco_Nombre_Entidad"].ToString();
                    list.Add(bank);
                }
                rdr.Close();
                response.lstBanks = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetBanks", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Gets the born city.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">ParametersDAO.GetBornCity</exception>
        public OutBornCities GetBornCity()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutBornCities response = new OutBornCities();
            var ora = new OracleServer(connectionString);

            BornCities city;
            List<BornCities> list = new List<BornCities>();
            string command = string.Empty;

            try
            {
                command = " SELECT to_char(A.nombre_ciudad || ' / ' || B.nombre_depto) as nombre_ciudad, A.codigo_ciudad as codigo_ciudad ";
                command = command + " FROM dfpcdd07 A, dfpdpt06 B  WHERE B.codigo_estado = A.codigo_estado and B.codigo_depto = A.codigo_depto ";
                command = command + " and A.Codigo_Ciudad > 0 order by 1 ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    city = new BornCities();
                    city.cityName = DBNull.Value.Equals(rdr["nombre_ciudad"]) ? string.Empty : rdr["nombre_ciudad"].ToString();
                    city.cityCode = DBNull.Value.Equals(rdr["codigo_ciudad"]) ? 0 : int.Parse( rdr["codigo_ciudad"].ToString());
                    list.Add(city);
                }
                rdr.Close();
                response.lstBornCities = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetBornCity", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }



        public OutCancellationCausal GetCancellationCausal()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCancellationCausal response = new OutCancellationCausal();
            var ora = new OracleServer(connectionString);

            CancellationCausal cancellationCausal;
            List<CancellationCausal> list = new List<CancellationCausal>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO, NOMBRE  FROM BBS_LIQCOM_V_CAUSAL_CANCELA ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    cancellationCausal = new CancellationCausal();
                    cancellationCausal.Code = DBNull.Value.Equals(rdr["CODIGO"]) ? "0" :rdr["CODIGO"].ToString();  
                    cancellationCausal.Name = DBNull.Value.Equals(rdr["NOMBRE"]) ? string.Empty : rdr["NOMBRE"].ToString();
                    list.Add(cancellationCausal);
                }
                rdr.Close();
                response.lstCancellationCausal = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetCausalCancelacion", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutCategories GetCategory()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCategories response = new OutCategories();
            var ora = new OracleServer(connectionString);

            Category category;
            List<Category> list = new List<Category>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_CATEGORIA, NOMBRE_CATEGORIA FROM BBS_LIQCOM_V_CATEGORIA ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    category = new Category();
                    category.Code = DBNull.Value.Equals(rdr["CODIGO_CATEGORIA"]) ? string.Empty : rdr["CODIGO_CATEGORIA"].ToString();
                    category.Name = DBNull.Value.Equals(rdr["NOMBRE_CATEGORIA"]) ? string.Empty : rdr["NOMBRE_CATEGORIA"].ToString();
                    list.Add(category);
                }
                rdr.Close();
                response.lstCategory = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetCategory", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutBranches GetBranches()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutBranches response = new OutBranches();
            var ora = new OracleServer(connectionString);

            Branch branch;
            List<Branch> list = new List<Branch>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_SUCURSAL, NOMBRE_SUCURSAL FROM BBS_LIQCOM_V_SUCURSALES ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    branch = new Branch();
                    branch.Code = DBNull.Value.Equals(rdr["CODIGO_SUCURSAL"]) ? 0 : double.Parse(rdr["CODIGO_SUCURSAL"].ToString());
                    branch.Name = DBNull.Value.Equals(rdr["NOMBRE_SUCURSAL"]) ? string.Empty : rdr["NOMBRE_SUCURSAL"].ToString();
                    list.Add(branch);
                }
                rdr.Close();
                response.lstBranches = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetBranches", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutRegional GetRegionals()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutRegional response = new OutRegional();
            var ora = new OracleServer(connectionString);

            Regional regional;
            List<Regional> list = new List<Regional>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_REGIONAL, NOMBRE FROM BBS_LIQCOM_V_REGIONAL_BAYP ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    regional = new Regional();
                    regional.Code = DBNull.Value.Equals(rdr["CODIGO_REGIONAL"]) ? 0 : double.Parse(rdr["CODIGO_REGIONAL"].ToString());  
                    regional.Name = DBNull.Value.Equals(rdr["NOMBRE"]) ? string.Empty : rdr["NOMBRE"].ToString();
                    list.Add(regional);
                }
                rdr.Close();
                response.lstRegionals = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetRegionals", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutCoordinator GetCoordinators()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutCoordinator response = new OutCoordinator();
            var ora = new OracleServer(connectionString);

            Coordinator coordinator;
            List<Coordinator> list = new List<Coordinator>();
            string command = string.Empty;

            try
            {
                command = " SELECT CODIGO_EJECUTIVO, NOMBRE_EJECUTIVO FROM BBS_LIQCOM_V_COORDINADOR  ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    coordinator = new Coordinator();
                    coordinator.Code = DBNull.Value.Equals(rdr["CODIGO_EJECUTIVO"]) ? 0 : double.Parse(rdr["CODIGO_EJECUTIVO"].ToString());  
                    coordinator.Name = DBNull.Value.Equals(rdr["NOMBRE_EJECUTIVO"]) ? string.Empty : rdr["NOMBRE_EJECUTIVO"].ToString();
                    list.Add(coordinator);
                }
                rdr.Close();
                response.lstCoordinator = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetCoordinators", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutExecutiveType GetExecutiveType()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutExecutiveType response = new OutExecutiveType();
            var ora = new OracleServer(connectionString);

            ExecutiveType executiveType;
            List<ExecutiveType> list = new List<ExecutiveType>();
            string command = string.Empty;

            try
            {
                command = " SELECT ind_tipo_ejecutivo, descripcion  FROM BBS_LIQCOM_V_TIPO_EJECUTIVO";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    executiveType = new ExecutiveType();
                    executiveType.Code = DBNull.Value.Equals(rdr["ind_tipo_ejecutivo"]) ? 0 : double.Parse(rdr["ind_tipo_ejecutivo"].ToString());
                    executiveType.Name = DBNull.Value.Equals(rdr["descripcion"]) ? string.Empty : rdr["descripcion"].ToString();
                    list.Add(executiveType);
                }
                rdr.Close();
                response.lstExecutiveType = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetExecutiveType", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutChannelType GetChannelType()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutChannelType response = new OutChannelType();
            var ora = new OracleServer(connectionString);

            ChannelType channelType;
            List<ChannelType> list = new List<ChannelType>();
            string command = string.Empty;

            try
            {
                command = " SELECT cod_cnl_vta, nom_cnl_vta FROM BBS_LIQCOM_V_TIPO_CANAL";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    channelType = new ChannelType();
                    channelType.Code = DBNull.Value.Equals(rdr["cod_cnl_vta"]) ? 0 : double.Parse(rdr["cod_cnl_vta"].ToString()); 
                    channelType.Name = DBNull.Value.Equals(rdr["nom_cnl_vta"]) ? string.Empty : rdr["nom_cnl_vta"].ToString();
                    list.Add(channelType);
                }
                rdr.Close();
                response.lstChannelType = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetChannelType", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutSalesChannel GetSalesChannel()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutSalesChannel response = new OutSalesChannel();
            var ora = new OracleServer(connectionString);

            SalesChannel salesChannel;
            List<SalesChannel> list = new List<SalesChannel>();
            string command = string.Empty;

            try
            {
                command = " SELECT codigo_tipo, nombre_tipo FROM BBS_LIQCOM_V_CNL_VENTA";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    salesChannel = new SalesChannel();
                    salesChannel.Code = DBNull.Value.Equals(rdr["codigo_tipo"]) ? 0 : double.Parse(rdr["codigo_tipo"].ToString()); 
                    salesChannel.Name = DBNull.Value.Equals(rdr["nombre_tipo"]) ? string.Empty : rdr["nombre_tipo"].ToString();
                    list.Add(salesChannel);
                }
                rdr.Close();
                response.lstSalesChannel = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetSalesChannel", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutParamDocuments GetLisDocuments(string documentType)
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutParamDocuments response = new OutParamDocuments();
            var ora = new OracleServer(connectionString);

            ParamDocuments paramDocuments;
            List<ParamDocuments> list = new List<ParamDocuments>();
            string command = string.Empty;

            try
            {
                command = " SELECT SECUENCIA,DESCRIPCION_DOCUMENTO,RUTA_RAIZ,NOMBRE_DOCUMENTO,TIPO_PARAMETRO,CARPETA ";
                command = string.Format("{0} FROM BBS_PORTAL_RUTA_IMAGENES WHERE TIPO_PARAMETRO = '{1}'", command,documentType);
               
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    paramDocuments = new ParamDocuments();
                    paramDocuments.Code = DBNull.Value.Equals(rdr["SECUENCIA"]) ? 0 : double.Parse(rdr["SECUENCIA"].ToString());
                    paramDocuments.Description = DBNull.Value.Equals(rdr["DESCRIPCION_DOCUMENTO"]) ? string.Empty : rdr["DESCRIPCION_DOCUMENTO"].ToString();
                    paramDocuments.Path = DBNull.Value.Equals(rdr["RUTA_RAIZ"]) ? string.Empty : rdr["RUTA_RAIZ"].ToString();
                    paramDocuments.Name = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : rdr["NOMBRE_DOCUMENTO"].ToString();
                    paramDocuments.DocumentType = DBNull.Value.Equals(rdr["NOMBRE_DOCUMENTO"]) ? string.Empty : rdr["NOMBRE_DOCUMENTO"].ToString();
                    paramDocuments.Folder = DBNull.Value.Equals(rdr["CARPETA"]) ? string.Empty : rdr["CARPETA"].ToString();
                    list.Add(paramDocuments);
                }
                rdr.Close();
                response.lstParamDocuments = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetLisDocuments", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        public OutExecutiveLevel GetExecutiveLevel()
        {
            string connectionString = DataBaseHelper.GetConnectionString("DLG");
            OutExecutiveLevel response = new OutExecutiveLevel();
            var ora = new OracleServer(connectionString);

            ExecutiveLevel executiveLevel;
            List<ExecutiveLevel> list = new List<ExecutiveLevel>();
            string command = string.Empty;

            try
            {
                command = "select bbs_liqcom_v_nivel.codigo_nivel, bbs_liqcom_v_nivel.nombre_nivel from bbs_liqcom_v_nivel ";
                var rdr = ora.ExecuteCommand(command);

                while (rdr.Read())
                {
                    executiveLevel = new ExecutiveLevel();
                    executiveLevel.Code = DBNull.Value.Equals(rdr["codigo_nivel"]) ? 0 : double.Parse(rdr["codigo_nivel"].ToString());
                    executiveLevel.Name = DBNull.Value.Equals(rdr["nombre_nivel"]) ? string.Empty : rdr["nombre_nivel"].ToString();
                    list.Add(executiveLevel);
                }
                rdr.Close();
                response.lstExecutiveLevel = list;
                response.msg = new Response();
                response.msg.errorCode = "200";
                response.msg.errorMessage = "OK";
            }
            catch (Exception ex)
            {
                throw new Exception("ParametersDAO.GetExecutiveLevel", ex);
            }
            finally
            {
                ora.Dispose();
            }
            return response;
        }

        
    }
}
