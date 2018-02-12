using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
{
    [Table("tblUsuario")]
    public class UsuarioViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int UsuarioID { get; set; }
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo de 2 caracteres")]
        [Required(ErrorMessage = "Preencha o campo login")]
        public string Login { get; set; }
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Required(ErrorMessage = "Preencha o campo o campo senha")]
        public string Senha { get; set; }
    }
}