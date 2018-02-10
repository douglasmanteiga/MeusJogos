using MeusJogos.Application.Interface;
using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Application.App
{
    public class EmprestimoSituacaoAppService : AppServiceBase<EmprestimoSituacao>, IEmprestimoSituacaoAppService
    {
        private readonly IEmprestimoSituacaoService _emprestimoSituacaoService;
        //Injeção de dependência de AppServiceBase
        public EmprestimoSituacaoAppService(IEmprestimoSituacaoService emprestimoSituacaoService) : base(emprestimoSituacaoService)
        {
            _emprestimoSituacaoService = emprestimoSituacaoService;
        }
    }
}
