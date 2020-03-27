using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutNextCategory
    {
        public double amount { get; set; }
        public string categoryName { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
