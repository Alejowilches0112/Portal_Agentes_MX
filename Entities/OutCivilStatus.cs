using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCivilStatus
    {
        public List<CivilStatus> lstCivilStatus { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class CivilStatus
    {
        public int statusID { get; set; }
        public string statusName { get; set; }

    }
}
