using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutUploadDocuments
    {
        public List<UploadDocuments> lstUploadDocuments { get; set; }
        public Response msg { get; set; } = new Response();
    }

    public class UploadDocuments
    {
        public double Consecutive { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
