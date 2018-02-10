using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Repositories;

namespace MeusJogos.Domain.Services
{
    public class EmprestimoService : ServiceBase<Emprestimo>, IDisposable
    {
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository) : base(emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }
    }
}
