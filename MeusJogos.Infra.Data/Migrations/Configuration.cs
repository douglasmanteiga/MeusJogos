namespace MeusJogos.Infra.Data.Migrations
{
    using Context;
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MeusJogosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MeusJogosContext context)
        {
            //Insere ou Atualiza as situações
            context.EmprestimoSituacao.AddOrUpdate
                (e => e.EmprestimoSituacaoID,
                new EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Emprestado, Descricao = EmprestimoSituacaoEnum.Emprestado.ToString() },
                new EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Devolvido, Descricao = EmprestimoSituacaoEnum.Devolvido.ToString() },
                new EmprestimoSituacao { EmprestimoSituacaoID = (int)EmprestimoSituacaoEnum.Extraviado, Descricao = EmprestimoSituacaoEnum.Extraviado.ToString() }
                );
        }
    }
}
