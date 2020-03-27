using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutHousingType
    {
        public List<HousingType> lstHousingType { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class HousingType
    {
        public int housingID { get; set; }
        public string housingName { get; set; }
    }
}
