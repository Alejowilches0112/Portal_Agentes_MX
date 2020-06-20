using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutAppliedStudies
    {
        public List<AppliedStudies> lstAppliedStudies { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class AppliedStudies
    {
        public int studiesID { get; set; }
        public string studiesName { get; set; }
    }
}
