using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.EntityConfig
{
    public class JogoConfig : EntityTypeConfiguration<Jogo>
    {
        public JogoConfig()
        {
            HasKey(j => j.JogoID);

            Property(j => j.Nome).IsRequired();
            Property(j => j.Ativo).IsRequired();
        }
    }
}
