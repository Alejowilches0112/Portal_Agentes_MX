using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutSalesChannel
    {
        public List<SalesChannel> lstSalesChannel { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class SalesChannel
    {
        public double Code { get; set; }
        public string Name { get; set; }
    }
}
