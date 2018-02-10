using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        //Alem do CRUD implementado pelo repositório base (IRepositoryBase), uma pesquisa especial

        Usuario Login(string usuario, string senha);
        Usuario UsuarioExistenteNoSistema(string usuario);


    }
}
