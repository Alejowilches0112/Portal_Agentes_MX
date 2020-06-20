using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutCustomer
    {
        public int documentType { get; set; }
        public string documentID { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string surname1 { get; set; }
        public string surname2 { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public double cellphone { get; set; }
        public double agreement { get; set; }
        public double payable { get; set; }
        public double position { get; set; }
        public string vinculationDate { get; set; }
        public int contractType { get; set; }
        public double salary { get; set; }
        public double health { get; set; }
        public double pension { get; set; }
        public double retefuente { get; set; }
        public double otherDiscounts { get; set; }
        public double solidarityFunds { get; set; }
        public int LoanLine { get; set; }
        public int LoanType { get; set; }
        public int term { get; set; }
        public double amount { get; set; }
        public double otherIncome { get; set; }        
        public Response msg { get; set; } = new Response();

    }
}
