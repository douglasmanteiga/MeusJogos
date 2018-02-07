using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("tblEmprestimo")]
    public class Emprestimo
    {
        [Display(Name = "Código")]
        public int EmprestimoID { get; set; }

        [Display(Name = "Data/Hora Cadastro")]
        [Required(ErrorMessage = "O campo Data/Hora é obrigatório")]
        public DateTime DataHora { get; set; }


        [Display(Name = "Data do Empréstimo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "O campo Data do Empréstimo é obrigatório")]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name = "Data para Devolução")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "O campo Data para Devolução é obrigatório")]
        public DateTime DataProgramadaDevolucao { get; set; }


        [Required(ErrorMessage = "O campo Situação é obrigatório")]
        [Display(Name = "Situação")]
        public int EmprestimoSituacaoID { get; set; }
        [ForeignKey("EmprestimoSituacaoID")]
        public virtual EmprestimoSituacao EmprestimoSituacao { get; set; }

        [Required(ErrorMessage = "O campo Amigo é obrigatório")]
        [Display(Name = "Amigo")]
        public int AmigoID { get; set; }
        [ForeignKey("AmigoID")]
        public virtual Amigo Amigo { get; set; }


        [Required(ErrorMessage = "O campo Jogo é obrigatório")]
        [Display(Name = "Jogo")]
        public int JogoID { get; set; }
        [ForeignKey("JogoID")]
        public virtual Jogo Jogo { get; set; }

        [Required(ErrorMessage = "O campo Usuário é obrigatório")]
        [Display(Name = "Usuário")]
        public int UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; }
    }
}
