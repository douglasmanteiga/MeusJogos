using Dapper;
using MeusJogos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MeusJogos.Controllers
{
    public class HomeController : Controller
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoMeusJogos"].ToString();

        //private MeusJogosContext db = new MeusJogosContext();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            //Se tiver autenticado joga para index..
            if(Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                //comando sql
                string query = string.Format("select top 1 UsuarioID, Login, Senha from tblUsuario where Login = '{0}' and Senha = '{1}'", usuario.Login, usuario.Senha);

                //Utilizando dapper no login
                using (var sqlConnection = new SqlConnection(ConnectionString)) 
                {
                    var usuarioLogin = sqlConnection.Query<Usuario>(query).FirstOrDefault();

                    if (usuarioLogin != null)
                    {
                        //Session["usuarioLogado"] = usuarioExiste.Login;
                        FormsAuthentication.SetAuthCookie(usuarioLogin.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login ou Senha incorreta!");
                    }

                }

                //Login utilizando Entity
                //using (MeusJogosContext db = new MeusJogosContext())
                //{
                //    var usuarioExiste = db.Usuario.Where(p => p.Login == usuario.Login && p.Senha == usuario.Senha).FirstOrDefault();

                //    if (usuarioExiste != null)
                //    {
                //        //Session["usuarioLogado"] = usuarioExiste.Login;
                //        FormsAuthentication.SetAuthCookie(usuarioExiste.Login, false);
                //        return RedirectToAction("Index", "Home");
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("", "Login ou Senha incorreta!");
                //    }
                //}

            }

            return View();
        }

        [Authorize]        
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

    }
}