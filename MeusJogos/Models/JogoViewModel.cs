using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
{
    [Table("tblJogo")]
    public class JogoViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int JogoID { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo de 2 caracteres")]
        [Required(ErrorMessage = "Preencha o campo nome")]
        public string Nome{ get; set; }

        [Required(ErrorMessage = "O campo Ativo é obrigatório")]
        public bool Ativo { get; set; }
    }
}