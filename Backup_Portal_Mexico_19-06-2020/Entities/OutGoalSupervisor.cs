using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutGoalSupervisor
    {
        public GoalSupervisor goalSupervisor { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class GoalSupervisor
    {
        public double goalValue { get; set; }
        public double complianceValue { get; set; }
        public double compliancePercentage { get; set; }

        public double goalNewValue { get; set; }
        public double complianceNewValue { get; set; }
        public double complianceNewPercentage { get; set; }

        public double goalRenovatedValue { get; set; }
        public double complianceRenovatedValue { get; set; }
        public double complianceRenovatedPercentage { get; set; }
    }
}
