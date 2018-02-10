using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Repositories;
using MeusJogos.Domain.Services.Base;
using System;

namespace MeusJogos.Domain.Services
{
    public class AmigoService : ServiceBase<Amigo>, IDisposable
    {
        private readonly IAmigoRepository _amigoRepository;

        public AmigoService(IAmigoRepository amigoRepository) : base(amigoRepository)
        {
            _amigoRepository = amigoRepository;
        }
    }
}
