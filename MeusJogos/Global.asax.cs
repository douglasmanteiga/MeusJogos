using MeusJogos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeusJogos
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private MeusJogosContext db = new MeusJogosContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Insere as situações a primeira vez que iniciar o sistema caso não exista
            if (db.EmprestimoSituacao != null && db.EmprestimoSituacao.ToList().Count <= 0)
            {
                db.EmprestimoSituacao.Add(new Models.EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Emprestado, Descricao = EmprestimoSituacaoEnum.Emprestado.ToString() });
                db.EmprestimoSituacao.Add(new Models.EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Devolvido, Descricao = EmprestimoSituacaoEnum.Devolvido.ToString() });
                db.EmprestimoSituacao.Add(new Models.EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Extraviado, Descricao = EmprestimoSituacaoEnum.Extraviado.ToString() });
                db.SaveChanges();
            }
        }

    }
}
