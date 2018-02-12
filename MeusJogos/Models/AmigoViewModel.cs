using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
{
    //View Model não deve ter comportamentos, são só representações

    [Table("tblAmigo")]
    public class AmigoViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int AmigoID { get; set; }
        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo de 2 caracteres")]
        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Preencha o campo e-mail")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail válido")]
        public string Email { get; set; }
    }

}