using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Login
    {   [Required(ErrorMessage ="Por favor Ingrese Usuario")]
        [RegularExpression("[a-zA-Z0-9_]{1,45}", ErrorMessage = "Usuario/password Incorrecto")]
        [MinLength(3,ErrorMessage = "Usuario/password Incorrecto"),MaxLength(19,ErrorMessage = "Usuario/password Incorrecto")]
        public string userName { get; set; }

        [Required(ErrorMessage ="Por favor Ingrese la Contraseña")]
        [MinLength(3,ErrorMessage ="Usuario/password Incorrecto"), MaxLength(15,ErrorMessage = "Usuario/password Incorrecto")]
        public string password { get; set; }

        public string email { get; set; } = string.Empty;
        public double asesor { get; set; }
        public double sucursal { get; set; }
    }
}
