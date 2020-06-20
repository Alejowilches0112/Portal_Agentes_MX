using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutBornCities
    {
        public List<BornCities> lstBornCities { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class BornCities
    {
        public string cityName { get; set; }
        public int cityCode { get; set; }

    }
}
