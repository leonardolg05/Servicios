using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSUsuariosTurnero.Models
{
    public class UserInfo
    {   [Key]
        public string Id { get;set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage ="Email con formato incorrecto")]
        public string Email { get; set; }
        public bool ExisteUsuario { get; set; }
        public int Identificacion { get; set; }
    }
}
