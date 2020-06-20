using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutSummaryPQR
    {
        public List<SummaryPQR> lstSummaryPQR { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class SummaryPQR
    {
        public string month  { get; set; }
        public string processName { get; set; }
        public double status { get; set; }
        public string stateName { get; set; }
        public double PQRNumber { get; set; }
    }
}
