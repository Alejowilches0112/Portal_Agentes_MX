using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class OutProgressExecutive
    {
        public ProgressExecutive progressExecutive { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ProgressExecutive
    {
        public double newLoanCount { get; set; }
        public double NewDisbursementAmount { get; set; }
        public double NewLoanValue { get; set; }

        public double RenovatedLoanCount { get; set; }
        public double RenovatedDisbursementAmount { get; set; }
        public double RenovatedLoanValue { get; set; }

        public double LoanCount { get; set; }
        public double DisbursementAmount { get; set; }
        public double LoanValue { get; set; }
    }
}


