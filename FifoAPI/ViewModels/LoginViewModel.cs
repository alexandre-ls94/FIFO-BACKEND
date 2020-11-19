using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o Nickname")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }
    }
}
