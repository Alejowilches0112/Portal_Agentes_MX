using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OutUserOptions
    {
        public List<UserOptions> lstUserOptions { get; set; }
        public Response msg { get; set; } = new Response();

    }

    public class UserOptions
    {
        public string cedula { get; set; }
        public string ind_menu_inicio { get; set; }
        public string funcionalidad_nombre { get; set; }
        public string ventana_objeto { get; set; }

    }

}
