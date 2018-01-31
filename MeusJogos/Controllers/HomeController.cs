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
            if (Request.IsAuthenticated)
            {
                //Consulta se o usuário logado existe.. 
                Usuario usuarioLogado = null;
                using (MeusJogosContext db = new MeusJogosContext())
                {
                    usuarioLogado = db.Usuario.Where(p => p.Login == User.Identity.Name).FirstOrDefault();
                }

                //Se não encontrou faz logoff
                if (usuarioLogado == null || usuarioLogado.UsuarioID <= 0)
                {
                    LogOff();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //Login retornando Json
        [AllowAnonymous]
        [HttpPost]
        public JsonResult ValidarUsuario(string login, string senha)
        {
            string mensagem = string.Empty;

            if (string.IsNullOrWhiteSpace(login) == false && string.IsNullOrWhiteSpace(senha) == false)
            {
                //Utilizando dapper no login
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    //comando sql
                    string query = string.Format("select top 1 UsuarioID, Login, Senha from tblUsuario where Login = '{0}' and Senha = '{1}'", login, senha);
                    var usuarioLogin = sqlConnection.Query<Usuario>(query).FirstOrDefault();

                    if (usuarioLogin != null)
                    {
                        //Session["usuarioLogado"] = usuarioExiste.Login;

                        FormsAuthentication.SetAuthCookie(login, false);
                        //return RedirectToAction("Index", "Home");                        
                        return Json(new { Mensagem = "Sucess" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { Mensagem = "Usuário ou senha inválidos." }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new { Mensagem = "Informe o usuário e senha." }, JsonRequestBehavior.AllowGet);
            }
        }

        //Login utilizando ActionResult
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