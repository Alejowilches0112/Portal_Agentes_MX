using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCategories
    {
        public List<Category> lstCategory { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class Category
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
