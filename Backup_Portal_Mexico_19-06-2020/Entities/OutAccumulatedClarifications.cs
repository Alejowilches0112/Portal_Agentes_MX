using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
   public  class OutAccumulatedClarifications
    {
        public List<AccumulatedClarifications> lstAccumulatedClarifications  { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class AccumulatedClarifications
    {
        public string month { get; set; }
        public double total { get; set; }
        public double answered { get; set; }
        public double pending { get; set; }
        public double percentageAnswered { get; set; }
        public double percentagePending { get; set; }

    }
}
