using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BayPortColombia.ViewModels
{
    public class UserPwdChgViewModel
    {
       
        public string name { get; set; }
        [Required(ErrorMessage = "La contraseña es Obligatoria")]
        [MinLength(8, ErrorMessage = "Debe contener mínimo 8 caracteres"), MaxLength(15, ErrorMessage = "Debe contener máximo 15 caracteres")]
        public string pwd1 { get; set; }
        [Required(ErrorMessage = "La contraseña es Obligatoria")]
        [Compare("pwd1", ErrorMessage = "Las contraseñas no son iguales")]
        public string pwd2 { get; set; }
        public string userID { get; set; }
    }
}