using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutDepartments
    {
        public List<Departments> lstDepartments { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Departments
    {
        public int consecutive { get; set; }
        public int departmentID { get; set; }
        public int municipalityID { get; set; }
        public int populationID { get; set; }
        public string departmentName { get; set; }
        public string municipalityName { get; set; }
        public string populationName { get; set; }
        public string populationType { get; set; }
        public int countryID { get; set; }
        public string countryName { get; set; }
    }
}
