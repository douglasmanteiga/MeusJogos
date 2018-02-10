using MeusJogos.Application;
using MeusJogos.Application.App;
using MeusJogos.Application.Interface;
using MeusJogos.Domain.Interfaces.Repositories;
using MeusJogos.Domain.Interfaces.Services;
using MeusJogos.Domain.Interfaces.Services.Base;
using MeusJogos.Domain.Services;
using MeusJogos.Infra.Data.Context;
using MeusJogos.Infra.Data.Repositories;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static void Register(Container container)
        {
            //Lifestyle.Scoped = Uma instância por request
            container.Register<MeusJogosContext>(Lifestyle.Scoped);

            container.Register<IAmigoAppService, AmigoAppService>(Lifestyle.Scoped);
            container.Register<IAmigoService, AmigoService>(Lifestyle.Scoped);
            container.Register<IAmigoRepository, AmigoRepository>(Lifestyle.Scoped);

            container.Register<IEmprestimoAppService, EmprestimoAppService>(Lifestyle.Scoped);
            container.Register<IEmprestimoService, EmprestimoService>(Lifestyle.Scoped);
            container.Register<IEmprestimoRepository, EmprestimoRepository>(Lifestyle.Scoped);

            container.Register<IEmprestimoSituacaoAppService, EmprestimoSituacaoAppService>(Lifestyle.Scoped);
            container.Register<IEmprestimoSituacaoService, EmprestimoSituacaoService>(Lifestyle.Scoped);
            container.Register<IEmprestimoSituacaoRepository, EmprestimoSituacaoRepository>(Lifestyle.Scoped);

            container.Register<IJogoAppService, JogoAppService>(Lifestyle.Scoped);
            container.Register<IJogoService, JogoService>(Lifestyle.Scoped);
            container.Register<IJogoRepository, JogoRepository>(Lifestyle.Scoped);

            container.Register<IUsuarioAppService, UsuarioAppService>(Lifestyle.Scoped);
            container.Register<IUsuarioService, UsuarioService>(Lifestyle.Scoped);
            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);

        }
    }
}
