using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProfileCustomer
    {
        public string identificationNumber { get; set; }
        public string name { get; set; } = string.Empty;
        public string secondName { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string secondSurname { get; set; } = string.Empty;
        public int gender { get; set; }

        [System.Xml.Serialization.SoapElementAttribute(DataType = "date")]
        public DateTime birthday { get; set; }
        public int payable { get; set; }

        [System.Xml.Serialization.SoapElementAttribute(DataType = "date")]
        public DateTime admissionDate { get; set; }
        public int contractType { get; set; }
        public int LoanType { get; set; }
        public int LoanLine { get; set; }
        public int activePensioner { get; set; }
        public string teacher { get; set; }
        public int term { get; set; }
        public int amount { get; set; }
        public List<PurchaseDetail> lstPurchase { get; set; }
        public int monthlyPaymentRefinancing { get; set; } = 0;
        public int branch { get; set; }
        public string regional { get; set; }
        public int agent { get; set; }
        public int salary { get; set; } = 0;
        public string formatURL { get; set; } = "C:\\";
        public int health { get; set; } = 0;
        public int pension { get; set; } = 0;
        public int retefuente { get; set; } = 0;
        public int solidarityFunds { get; set; } = 0;
        public int otherDiscounts { get; set; } = 0;
        public int amountPurchasePortfolio { get; set; } = 0;
        public int additionalIncome { get; set; } = 0;
        public int otro_intento { get; set; } = 0;
        public int folderNumber { get; set; } = 0;
        public string phone { get; set; }
        public int numericalField1 { get; set; } = 0;
        public int numericalField2 { get; set; } = 0;
        public int numericalField3 { get; set; } = 0;
        public int numericalField4 { get; set; } = 0;
        public int numericalField5 { get; set; } = 0;
        public int numericalField6 { get; set; } = 0;
        public int numericalField7 { get; set; } = 0;
        public int numericalField8 { get; set; } = 0;
        public int numericalField9 { get; set; } = 0;
        public int numericalField10 { get; set; } = 0;
        public int numericalField11 { get; set; } = 0;
        public int numericalField12 { get; set; } = 0;
        public int numericalField13 { get; set; } = 0;
        public int numericalField14 { get; set; } = 0;
        public int numericalField15 { get; set; } = 0;
        public int numericalField16 { get; set; } = 0;
        public int numericalField17 { get; set; } = 0;
        public int numericalField18 { get; set; } = 0;
        public int numericalField19 { get; set; } = 0;
        public int numericalField20 { get; set; } = 0;
        public string stringField1 { get; set; } = string.Empty;
        public string stringField2 { get; set; } = string.Empty;
        public string stringField3 { get; set; } = string.Empty;
        public string stringField4 { get; set; } = string.Empty;
        public string stringField5 { get; set; } = string.Empty;
        public string stringField6 { get; set; } = string.Empty;
        public string stringField7 { get; set; } = string.Empty;
        public string stringField8 { get; set; } = string.Empty;
        public string stringField9 { get; set; } = string.Empty;
        public string stringField10 { get; set; } = string.Empty;
        public string stringField11 { get; set; } = string.Empty;
        public string stringField12 { get; set; } = string.Empty;
        public string stringField13 { get; set; } = string.Empty;
        public string stringField14 { get; set; } = string.Empty;
        public string stringField15 { get; set; } = string.Empty;
        public string stringField16 { get; set; } = string.Empty;
        public string stringField17 { get; set; } = string.Empty;
        public string stringField18 { get; set; } = string.Empty;
        public string stringField19 { get; set; } = string.Empty;
        public string stringField20 { get; set; } = string.Empty;
        public DateTime dateField1 { get; set; } = DateTime.Today;
        public DateTime dateField2 { get; set; } = DateTime.Today;
        public DateTime dateField3 { get; set; } = DateTime.Today;
        public DateTime dateField4 { get; set; } = DateTime.Today;
        public DateTime dateField5 { get; set; } = DateTime.Today;
        public string agreement { get; set; }
    }
}
