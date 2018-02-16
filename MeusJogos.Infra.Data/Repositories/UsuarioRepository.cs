using Dapper;
using MeusJogos.Domain.Entities;
using MeusJogos.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusJogos.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConexaoMeusJogos"].ToString();

        //Login Utilizando Dapper
        public Usuario Login(string usuario, string senha)
        {
            Usuario usuarioEncontrado = null;

            var query = "SELECT UsuarioID, Login FROM Usuario WHERE Login = @Usuario AND Senha = @Senha";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                usuarioEncontrado = sqlConnection.Query<Usuario>(query, new { Usuario = usuario, Senha = senha }).FirstOrDefault();
            }

            //return db.Usuario.Where(u => u.Login == usuario && u.Senha == senha).FirstOrDefault();

            return usuarioEncontrado;
        }

        //Consulta Utilizando Dapper
        public Usuario UsuarioExistenteNoSistema(string usuario)
        {
            Usuario usuarioEncontrado = null;

            var query = "SELECT UsuarioID, Login FROM Usuario WHERE Login = @Usuario";

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                usuarioEncontrado = sqlConnection.Query<Usuario>(query, new { Usuario = usuario }).FirstOrDefault();
            }

            //return db.Usuario.ToList().Where(u => u.Login == usuario).FirstOrDefault();
            return usuarioEncontrado;


        }

        /*
            <<Exemplos CRUD com DAPPER>>
         
            public void Save(Supplier model) 
            {
               using (var sqlConnection = new SqlConnection(ConnectionString)) 
               {
	               sqlConnection.Execute("INSERT INTO SUPPLIER(CompanyName,ContactName, City, Country) VALUES (@CompanyName, @ContactName, @City, @Country)", model);
               }
            }

            public void Update(Supplier model) 
            {
               using (var sqlConnection = new SqlConnection(ConnectionString)) 
               {
	               sqlConnection.Execute(@"UPDATE SUPPLIER
							               SET CompanyName = @CompanyName, 
								               ContactName = @ContactName,
								               City = @City,
								               Country = @Country
							               WHERE ID = @Id", model);
               }
            }

            public void Delete(Supplier model) 
            {
               using (var sqlConnection = new SqlConnection(ConnectionString)) 
               {
	               sqlConnection.Execute("DELETE FROM SUPPLIER WHERE ID = @Id", model);
               }
            }                  

	        public List<Product> GetProducts()
	        {
		        List<Product> products = new List<Product>();

		        using (var sqlConnection = new SqlConnection(ConnectionString)) 
		        {
			        var result = sqlConnection.Query<Product>("Select * from Product");

			        foreach (Product product in result) 
				        products.Add(product);
		        }

		        return products;
	        }

	        public List<Product> GetProductBySupplier(int supplierId) 
	        {

		        var query = @" Select * from Product
					           join Supplier on Product.SupplierId = Supplier.Id
					           where SupplierId = @SupplierId; ";

		        using (var sqlConnection = new SqlConnection(ConnectionString))
		        {
			        var products = sqlConnection.Query<Product>(query, new { SupplierId = supplierId });

			        return products.ToList();
		        }
	        }

	        public Product GetProductById(int productId) 
	        {
		        using (var sqlConnection = new SqlConnection(ConnectionString))
		        {
			        return sqlConnection.Query&lt;Product&gt;("Select * from Product where Id = @Id", new { Id = productId }).SingleOrDefault();
		        }

	        }
         
         */
    }
}
