using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutParamDocuments
    {
        public List<ParamDocuments> lstParamDocuments { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class ParamDocuments
    {
        public double Code { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string Folder { get; set; }
    }
}
