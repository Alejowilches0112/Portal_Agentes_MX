using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutBanks
    {
        public List<Banks> lstBanks { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Banks
    {
        public string entityNIT { get; set; }
        public string entityName { get; set; }
    }
}
