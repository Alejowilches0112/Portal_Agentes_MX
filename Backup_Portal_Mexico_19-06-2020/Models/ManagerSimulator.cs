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
    public class ManagerSimulator
    {
        public OutCategorySimulation GetCategorySimulation(InCategorySimulation input)
        {
            OutCategorySimulation response = new OutCategorySimulation();
            try
            {
                SimulatorDAO dao = new SimulatorDAO();
                response = dao.GetCategorySimulation(input);
            }
            catch (Exception ex)
            {
                //escribir en el log
                LogHelper.WriteLog("Models", "ManagerSimulator", "GetCategorySimulation", ex, "");
            }
            return response;
        }
    }
}
