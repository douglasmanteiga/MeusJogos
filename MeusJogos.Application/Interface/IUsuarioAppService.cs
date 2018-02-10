using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Application.Interface
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {
        Usuario Login(string usuario, string senha);
        Usuario UsuarioExistenteNoSistema(string usuario);
    }
}
