using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Entities
{
    public class Jogo
    {
        public int JogoID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
