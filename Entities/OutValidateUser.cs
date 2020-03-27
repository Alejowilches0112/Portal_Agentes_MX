using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutValidateUser: InValidateUser
    {
        public int changePassword { get; set; }
        public string userName { get; set; }
        public string url { get; set; } = string.Empty;
    }
}
