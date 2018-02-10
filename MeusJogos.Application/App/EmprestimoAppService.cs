using MeusJogos.Application.Interface;
using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Services;

namespace MeusJogos.Application.App
{
    public class EmprestimoAppService : AppServiceBase<Emprestimo>, IEmprestimoAppService
    {
        private readonly IEmprestimoService _emprestimoService;
        //Injeção de dependência de AppServiceBase
        public EmprestimoAppService(IEmprestimoService emprestimoService) : base(emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }
    }
}
