using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario Login(string usuario, string senha)
        {
            return db.Usuario.Where(u => u.Login == usuario && u.Senha == senha).FirstOrDefault();
        }

        public Usuario UsuarioExistenteNoSistema(string usuario)
        {
            return db.Usuario.ToList().Where(u => u.Login == usuario).FirstOrDefault();
        }
    }
}
