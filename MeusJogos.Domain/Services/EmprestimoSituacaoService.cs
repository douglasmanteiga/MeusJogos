using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Repositories;
using MeusJogos.Domain.Interfaces.Services;
using MeusJogos.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Services
{
    public class EmprestimoSituacaoService : ServiceBase<EmprestimoSituacao>, IEmprestimoSituacaoService
    {
        private readonly IEmprestimoSituacaoRepository _emprestimoSituacaoRepository;

        public EmprestimoSituacaoService(IEmprestimoSituacaoRepository emprestimoSituacaoRepository) : base(emprestimoSituacaoRepository)
        {
            _emprestimoSituacaoRepository = emprestimoSituacaoRepository;
        }
    }
}
