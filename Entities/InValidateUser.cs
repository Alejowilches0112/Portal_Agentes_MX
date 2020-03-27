using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InValidateUser
    {
        public int company { get; set; } = 1;
        public string userID { get; set; }
        public string password { get; set; }
        public double sucursal { get; set; }
        public double asesor { get; set; }
        public Response msg { get; set; } = new Response();

    }
}
