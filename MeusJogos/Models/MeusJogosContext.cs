using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeusJogos.Models
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
            //Cria a base e tabelas se não existir
            Database.SetInitializer(new CreateDatabaseIfNotExists<MeusJogosContext>());

            //Remove a criação das tabelas no plural / não precisa pq já declarei nos objetos o atributo [Table("")]
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}