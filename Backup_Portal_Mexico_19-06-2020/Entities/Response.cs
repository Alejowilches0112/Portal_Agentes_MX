using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Response
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }

        public Response() {
            this.errorCode = string.Empty;
            this.errorMessage = string.Empty;
        }

    }
}
