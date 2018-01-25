using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
{
    [Table("tblUsuario")]
    public class Usuario
    {
        [Display(Name = "Código")]
        public int UsuarioID { get; set; }
        [Required(ErrorMessage = "O campo Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Senha { get; set; }
    }
}