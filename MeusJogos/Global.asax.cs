using MeusJogos.App_Start;
using MeusJogos.AutoMapper;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeusJogos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Inicializa a injeção de dependência
            SimpleInjectorInitializer.Initialize();

            // Configurando o AutoMapper para registrar os profiles
            // de mapeamento durante a inicialização da aplicação.
            AutoMapperConfig.RegisterMappings();
        }

    }
}
