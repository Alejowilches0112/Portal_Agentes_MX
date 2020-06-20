using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutFolder
    {
        public List<Folder> lstFolder { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Folder
    {
        public double folder { get; set; }
        public double monto { get; set; }
        public double plazo { get; set; }
        public string create_date { get; set; }
    }
}
