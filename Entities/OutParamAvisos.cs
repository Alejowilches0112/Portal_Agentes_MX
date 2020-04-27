using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamAviso
    {
        public double secuencia { get; set; }
        public string titulo { get; set; }
        public double fuente { get; set; }
        public string contenido { get; set; }
        public List<imagenes> imgs { get; set; }
        public List<enlace> enlaces { get; set; }
        public string allenlaces { get; set; }
        public string allimgs { get; set; }
        public string fch_inicio { get; set; }
        public string fch_fin { get; set; }
    }
    public class enlace
    {
        public string titulo { get; set; }
        public string link { get; set; }
    }
    public class imagenes
    {
        public string name { get; set; }
        public string imageDataUrl { get; set; }
        public string path { get; set; }
    }
    public class OutParamAvisos
    {
        public List<ParamAviso> ListAvisos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
