using MeusJogos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Entities;
using MeusJogos.Infra.Data.Context;

namespace MeusJogos.Infra.Data.Repositories
{
    public class EmprestimoSituacaoRepository : RepositoryBase<EmprestimoSituacao>, IEmprestimoSituacaoRepository
    {
    }
}
