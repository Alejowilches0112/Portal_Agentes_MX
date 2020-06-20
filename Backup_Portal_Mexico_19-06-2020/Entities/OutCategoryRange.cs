using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCategoryRange
    {
        public List<CategoryRange> lstCategoryRange { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class CategoryRange
    {
        public string SchemeName { get; set; }
        public double SchemeCode { get; set; }
        public double InitialRange { get; set; }
        public double FinalRange { get; set; }
        public string CategoryName { get; set; }
        public double CategoryCode { get; set; }
    }
}
