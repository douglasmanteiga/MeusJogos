﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tblJogo")]
    public class Jogo
    {
        [Display(Name = "Código")]
        public int JogoID { get; set; }

        [Required(ErrorMessage = "O campo Nome do Jogo é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Ativo é obrigatório")]
        public bool Ativo { get; set; }
    }
}
