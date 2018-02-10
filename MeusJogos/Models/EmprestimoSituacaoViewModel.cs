using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
{
    public enum EmprestimoSituacaoEnum
    {
        Emprestado = 1,
        Devolvido = 2,
        Extraviado = 3
    }
    [Table("tblEmprestimoSituacao")]
    public class EmprestimoSituacaoViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Não gerar identity
        [Display(Name = "Código")]
        public int EmprestimoSituacaoID { get; set; }
        [Required(ErrorMessage = "Preencha o campo descrição")]
        public string Descricao { get; set; }
    }
}