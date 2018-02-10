using MeusJogos.Application.Interface;
using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Services;

namespace MeusJogos.Application.App
{
    public class JogoAppService : AppServiceBase<Jogo>, IJogoAppService
    {
        private readonly IJogoService _jogoService;
        //Injeção de dependência de AppServiceBase
        public JogoAppService(IJogoService jogoService) : base(jogoService)
        {
            _jogoService = jogoService;            
        }
    }
}
