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
    public class ManageComplianceGoal
    {
        public OutNextCategory GetNextCategory(string executiveID)
        {
            OutNextCategory data = new OutNextCategory();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetNextCategory(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetNextCategory", ex, "");
            }
            return data;
        }


        public OutAccumulatedLoan GetAccumulatedLoan(string executiveID)
        {
            OutAccumulatedLoan data = new OutAccumulatedLoan();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetAccumulatedLoan(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetAccumulatedLoan", ex, "");
            }
            return data;
        }

        public OutAccumulatedClarifications GetAccumulatedClarifications(string executiveID)
        {
            OutAccumulatedClarifications data = new OutAccumulatedClarifications();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetAccumulatedClarifications(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetAccumulatedClarifications", ex, "");
            }
            return data;
        }

        public OutGoalExecutive GetGoalExecutive(string executiveID)
        {
            OutGoalExecutive data = new OutGoalExecutive();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetGoalExecutive(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetGoalExecutive", ex, "");
            }
            return data;
        }

        public OutProductivity GetProductivity(string executiveID)
        {
            OutProductivity data = new OutProductivity();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetProductivity(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetProductivity", ex, "");
            }
            return data;
        }

        public OutCategoryRange GetCategoryRange()
        {
            OutCategoryRange data = new OutCategoryRange();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetCategoryRange();
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetCategoryRange", ex, "");
            }
            return data;
        }

        public OutGoalSupervisor GetGoalSupervisor(string executiveID)
        {
            OutGoalSupervisor data = new OutGoalSupervisor();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetGoalSupervisor(executiveID);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetGoalSupervisor", ex, "");
            }
            return data;
        }

        public OutProgressExecutive GetProgressExecutive(string executiveID, DateTime startDate, DateTime endDate)
        {
            OutProgressExecutive data = new OutProgressExecutive();
            try
            {
                ComplianceGoalDAO dao = new ComplianceGoalDAO();
                data = dao.GetProgressExecutive(executiveID, startDate, endDate);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManageComplianceGoal", "GetProgressExecutive", ex, "");
            }
            return data;
        }

       
    }
}
