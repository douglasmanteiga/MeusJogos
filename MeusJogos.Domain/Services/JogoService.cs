using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Repositories;
using MeusJogos.Domain.Interfaces.Services;

namespace MeusJogos.Domain.Services
{
    public class JogoService : ServiceBase<Jogo>, IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository) : base(jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }
    }
}
