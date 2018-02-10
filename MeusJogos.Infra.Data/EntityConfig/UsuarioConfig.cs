using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.UsuarioID);

            Property(u => u.Login).IsRequired();
            Property(u => u.Senha).IsRequired();
        }
    }
}
