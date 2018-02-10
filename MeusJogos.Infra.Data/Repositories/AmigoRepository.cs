using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.Repositories
{
    public class AmigoRepository : RepositoryBase<Amigo>, IAmigoRepository
    {
        //O CRUD da Classe IRepositoryBase já está implementado pela classe RepositoryBase<Amigo>
        //Por este motivo essa classe não está dando erro para implementar o CRUD
    }
}
