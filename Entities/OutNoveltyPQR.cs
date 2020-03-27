using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutNoveltyPQR
    {
        public List<NoveltyPQR> lstNoveltyPQR { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class NoveltyPQR
    {
        public int loanNumber { get; set; }
        public int processNumber { get; set; }
        public int SequenceNovelty { get; set; }
        public int noveltyCode { get; set; }
        public string description { get; set; }
        public string dateInsertBusiness { get; set; }
        public string userInsert { get; set; }
        public string dateInsertSYS { get; set; }
        public string NoveltyName { get; set; }
        public string flow { get; set; }
        public string executiveName { get; set; }
    }
}
