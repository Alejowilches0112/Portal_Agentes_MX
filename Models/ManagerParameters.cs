using DAO;
using Entities;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ManagerParameters
    {

        /// <summary>
        /// Gets the civil status.
        /// </summary>
        /// <returns></returns>
        public OutCivilStatus GetCivilStatus()
        {
            OutCivilStatus data = new OutCivilStatus();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetCivilStatus();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetCivilStatus", ex, "");
            }
            return data;
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public OutDepartments GetDepartments()
        {
            OutDepartments data = new OutDepartments();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetDepartments();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetDepartments", ex, "");
            }
            return data;
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="departmentID">The department identifier.</param>
        /// <returns></returns>
        public OutDepartments GetCities(int departmentID)
        {
            OutDepartments data = new OutDepartments();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetCities(departmentID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetCities", ex, "");
            }
            return data;
        }

        /// <summary>
        /// Gets the neighborhood.
        /// </summary>
        /// <param name="municipalityID">The municipality identifier.</param>
        /// <returns></returns>
        public OutDepartments GetNeighborhood(int municipalityID)
        {
            OutDepartments data = new OutDepartments();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetNeighborhood(municipalityID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetNeighborhood", ex, "");
            }
            return data;
        }

        /// <summary>
        /// Gets the type of the housing.
        /// </summary>
        /// <returns></returns>
        public OutHousingType GetHousingType()
        {
            OutHousingType data = new OutHousingType();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetHousingType();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetHousingType", ex, "");
            }
            return data;
        }
        /// <summary>
        /// Gets the applied studies.
        /// </summary>
        /// <returns></returns>
        public OutAppliedStudies GetAppliedStudies()
        {
            OutAppliedStudies data = new OutAppliedStudies();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetAppliedStudies();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetAppliedStudies", ex, "");
            }
            return data;
        }
        /// <summary>
        /// Gets the afp.
        /// </summary>
        /// <returns></returns>
        public OutAFP GetAFP()
        {
            OutAFP data = new OutAFP();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetAFP();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetAFP", ex, "");
            }
            return data;
        }
        /// <summary>
        /// Gets the arp.
        /// </summary>
        /// <returns></returns>
        public OutARP GetARP()
        {
            OutARP data = new OutARP();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetARP();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetARP", ex, "");
            }
            return data;
        }
        public OutEPS GetEPS()
        {
            OutEPS data = new OutEPS();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetEPS();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetEPS", ex, "");
            }
            return data;
        }
        public OutBanks GetBanks()
        {
            OutBanks data = new OutBanks();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetBanks();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetBanks", ex, "");
            }
            return data;
        }

        public OutBornCities GetBornCity()
        {
            OutBornCities data = new OutBornCities();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetBornCity();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetBornCity", ex, "");
            }
            return data;
        }

        public OutCancellationCausal GetCancellationCausal()
        {
            OutCancellationCausal data = new OutCancellationCausal();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetCancellationCausal();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetCausalCancelacion", ex, "");
            }
            return data;
        }

        public OutCategories GetCategory()
        {
            OutCategories data = new OutCategories();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetCategory();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetCategory", ex, "");
            }
            return data;
        }

        public OutBranches GetBranches()
        {
            OutBranches data = new OutBranches();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetBranches();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetBranches", ex, "");
            }
            return data;
        }

        public OutRegional GetRegionals()
        {
            OutRegional data = new OutRegional();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetRegionals();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetRegionals", ex, "");
            }
            return data;
        }

        public OutCoordinator GetCoordinators()
        {
            OutCoordinator data = new OutCoordinator();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetCoordinators();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetCoordinators", ex, "");
            }
            return data;
        }

        public OutExecutiveType GetExecutiveType()
        {
            OutExecutiveType data = new OutExecutiveType();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetExecutiveType();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetExecutiveType", ex, "");
            }
            return data;
        }

        public OutChannelType GetChannelType()
        {
            OutChannelType data = new OutChannelType();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetChannelType();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetChannelType", ex, "");
            }
            return data;
        }

        public OutSalesChannel GetSalesChannel()
        {
            OutSalesChannel data = new OutSalesChannel();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetSalesChannel();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetSalesChannel", ex, "");
            }
            return data;
        }

        public OutParamDocuments GetLisDocuments(string documentType)
        {
            OutParamDocuments data = new OutParamDocuments();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetLisDocuments(documentType);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetLisDocuments", ex, "");
            }
            return data;
        }

        public OutExecutiveLevel GetExecutiveLevel()
        {
            OutExecutiveLevel data = new OutExecutiveLevel();
            try
            {
                ParametersDAO dao = new ParametersDAO();
                data = dao.GetExecutiveLevel();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerParameters", "GetExecutiveLevel", ex, "");
            }
            return data;
        }
    }
}
