using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutDocuments
    {
        public List<Documents> lstDocuments { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class Documents
    {
        public int documentType{ get; set; }
        public string documentName { get; set; }
    }
}
