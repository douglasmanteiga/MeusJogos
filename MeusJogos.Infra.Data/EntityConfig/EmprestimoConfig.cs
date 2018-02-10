using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.EntityConfig
{
    public class EmprestimoConfig : EntityTypeConfiguration<Emprestimo>
    {
        public EmprestimoConfig()
        {
            HasKey(e => e.EmprestimoID);

            Property(e => e.DataHora).IsRequired();
            Property(e => e.DataEmprestimo).IsRequired();
            Property(e => e.DataProgramadaDevolucao).IsRequired();

            Property(e => e.EmprestimoSituacaoID).IsRequired();
            Property(e => e.AmigoID).IsRequired();
            Property(e => e.JogoID).IsRequired();
            Property(e => e.UsuarioID).IsRequired();

            //Adicionando a Foreign key
            HasRequired(e => e.EmprestimoSituacao).WithMany().HasForeignKey(e => e.EmprestimoSituacaoID);
            HasRequired(e => e.Amigo).WithMany().HasForeignKey(e => e.AmigoID);
            HasRequired(e => e.Jogo).WithMany().HasForeignKey(e => e.JogoID);
            HasRequired(e => e.Usuario).WithMany().HasForeignKey(e => e.UsuarioID);
        }

    }
}
