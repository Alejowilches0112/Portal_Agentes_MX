using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParamDocumentos
    {
        public double cod_documento { get; set; }
        public string nombre { get; set; } 
        public string dependencia { get; set; }
        public string producto { get; set; }
        public double firma { get; set; }
        public double llena_auto { get; set; }
        public double orden { get; set; }
        public string path { get; set; }
        public double expedientillo { get; set; }
        public double? pagina_firma { get; set; }
        public double compra { get; set; }
        public double? max_item { get; set; }
    }
    public class OutParamDocumentos
    {
        public List<ParamDocumentos> ListDocumentos { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class documents
    {
        public double? cod_documento { get; set; }
        public string nombre { get; set; }
        public string nombreDoc { get; set; }
        public string dependencia { get; set; }
        public string producto { get; set; }
        public double firma { get; set; }
        public double llena_auto { get; set; }
        public double orden { get; set; }
        public string path { get; set; }
        public string file { get; set; }
        public double expedientillo { get; set; }
        public double pagina_firma { get; set; }
        public double compra { get; set; }
        public double? max_item { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class OutGetDocument
    {

        public string virtualpath { get; set; }
        public string filename { get; set; }
    }
    public class DocumentoOriginacion
    {
        public double? codigo { get; set; }
        public double? producto { get; set; }
        public double dependencia { get; set; }
        public string folder { get; set; }
        public double? codigo_doc { get; set; }
        public string file { get; set; }
        public string nombreDoc { get; set; }
        public string nombre_cartera { get; set; }
        public string path { get; set; }
        public double firma { get; set; }
        public double expedienteCompleto { get; set; }
        public double pagina_firma { get; set; }
        public double? llena_auto { get; set; }
        public double compra { get; set; }
        public double? max_item { get; set; }
        public Response msg { get; set; } = new Response();
    }
    public class OutDocumentoOriginacion
    {
        public List<DocumentoOriginacion> ListDocumentos { get; set; }
        public Response msg { get; set; } = new Response();
    }
}
