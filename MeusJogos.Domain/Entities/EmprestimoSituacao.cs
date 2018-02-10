using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Entities
{
    public enum EmprestimoSituacaoEnum
    {
        Emprestado = 1,
        Devolvido = 2,
        Extraviado = 3
    }
    public class EmprestimoSituacao
    {
        public int EmprestimoSituacaoID { get; set; }
        public string Descricao { get; set; }
    }
}
