using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {
        Usuario Login(string usuario, string senha);
        Usuario UsuarioExistenteNoSistema(string usuario);
    }
}
