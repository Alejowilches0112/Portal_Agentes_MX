using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutLogPQR
    {
        public List<LogPQR> lstLogPQR { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class LogPQR
    {    
        public int processNumber { get; set; }
        public int SequenceNovelty { get; set; }
        public int previousState { get; set; }
        public int newState { get; set; }
        public string description { get; set; }
        public string dateInsertBusiness { get; set; }
        public string userInsert { get; set; }
        public string dateInsertSYS { get; set; }
        public string statusNameNew { get; set; }
        public string statusNamePrevious { get; set; }
    }
}
