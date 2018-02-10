using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeusJogos.Domain.Interfaces.Repositories;
using MeusJogos.Domain.Interfaces.Services;

namespace MeusJogos.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario Login(string usuario, string senha)
        {
            return _usuarioRepository.Login(usuario, senha);
        }

        public Usuario UsuarioExistenteNoSistema(string usuario)
        {
            return _usuarioRepository.UsuarioExistenteNoSistema(usuario);
        }
    }
}
