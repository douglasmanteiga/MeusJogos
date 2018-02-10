using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Entities
{
    public class Emprestimo
    {
        public int EmprestimoID { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataProgramadaDevolucao { get; set; }
        public int EmprestimoSituacaoID { get; set; }
        public virtual EmprestimoSituacao EmprestimoSituacao { get; set; }
        public int AmigoID { get; set; }
        public virtual Amigo Amigo { get; set; }
        public int JogoID { get; set; }
        public virtual Jogo Jogo { get; set; }
        public int UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
