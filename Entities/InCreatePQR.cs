using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InCreatePQR
    {
        public decimal company { get; set; }
        public decimal flowType { get; set; }
        public decimal reason { get; set; }
        public string executiveID { get; set; }
        public string customerID { get; set; }
        public string description { get; set; }
        public decimal loanNumber { get; set; }
    }


    public class FormCreatePQR
    {        
        public string TipoFlujo { get; set; }
        public string TipoJustificacion { get; set; }
        public string numDocAse { get; set; }
        public string numDocCli { get; set; }
        public string Observaciones { get; set; }
        public string NumeroCredito { get; set; }
    }
}
