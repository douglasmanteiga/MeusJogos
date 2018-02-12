using MeusJogos.Domain.Entities;
using MeusJogos.Infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.Context
{
    public class MeusJogosContext : DbContext
    {
        public MeusJogosContext() : base("ConexaoMeusJogos")//Encontra a conexão ConexaoMeusJogos no arquivo (Web.config)
        {
        }

        public DbSet<Amigo> Amigo { get; set; }
        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EmprestimoSituacao> EmprestimoSituacao { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Database.SetInitializer<MeusJogosContext>(new DropCreateDatabaseAlways<MeusJogosContext>());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Remove a criação de tabelas no plural
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); //Não deleta em cascata qnd estiver uma relação de um para N
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //Não deleta em cascada qnd estiver uma relação de N para N

            //Qnd a propriedade tiver o final com id -> Será a chave primária da tabela
            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "id")
                .Configure(p => p.IsKey());

            //Qnd a propriedade for string -> Utilize varchar no banco de dados
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            //Qnd a propriedade for string -> Utilize no banco varchar(100)
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            //Aplicando as configurações das tabelas
            modelBuilder.Configurations.Add(new AmigoConfig());
            modelBuilder.Configurations.Add(new JogoConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new EmprestimoSituacaoConfig());
            modelBuilder.Configurations.Add(new EmprestimoConfig());


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataHora") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataHora").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataHora").IsModified = false;
                }
            }

            //foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UsuarioID") != null))
            //{
            //    if (entry.State == EntityState.Modified)
            //    {
            //        entry.Property("UsuarioID").IsModified = false;
            //    }
            //}

            return base.SaveChanges();
        }
    }
}
