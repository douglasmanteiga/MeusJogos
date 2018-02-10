using MeusJogos.Application.Interface;
using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Services;
using MeusJogos.Domain.Interfaces.Services.Base;

namespace MeusJogos.Application
{
    public class AmigoAppService : AppServiceBase<Amigo>, IAmigoAppService
    {
        private readonly IAmigoService _amigoService;
        //Injeção de dependência de AppServiceBase
        public AmigoAppService(IAmigoService amigoService) : base(amigoService)
        {
            _amigoService = amigoService;
        }
    }
}
