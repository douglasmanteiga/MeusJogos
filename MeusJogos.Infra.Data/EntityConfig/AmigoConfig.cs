using MeusJogos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.EntityConfig
{
    public class AmigoConfig : EntityTypeConfiguration<Amigo>
    {
        public AmigoConfig()
        {
            //-> Modelando a tabela do banco de dados com Entity Framework - Fluent API

            //AmigoID é chave
            HasKey(a => a.AmigoID);

            //O campo nome é obrigatório e seu tamanho máximo é 150
            Property(c => c.Nome).IsRequired().HasMaxLength(150);

            //O campo e-mail é obrigatório porém seu tamhno é 100, isso foi definido no OnModelCreating (DbContext)
            Property(c => c.Email).IsRequired();
        }
    }
}
