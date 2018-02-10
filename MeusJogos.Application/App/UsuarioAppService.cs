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
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioAppService(IUsuarioService usuarioService) : base(usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario Login(string usuario, string senha)
        {
            return _usuarioService.Login(usuario, senha);
        }

        public Usuario UsuarioExistenteNoSistema(string usuario)
        {
            return _usuarioService.UsuarioExistenteNoSistema(usuario);
        }
    }
}
