using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamEmpresaTelefonica
    {
        public double secuencia { get; set; }
        public string empresa { get; set; } 
    }
    public class OutParamEmpresatelefonica
    {
        public List<ParamEmpresaTelefonica> ListEmpresas { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
