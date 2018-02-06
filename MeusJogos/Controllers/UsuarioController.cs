using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeusJogos.Models;

namespace MeusJogos.Controllers
{
    public class UsuarioController : Controller
    {
        private MeusJogosContext db = new MeusJogosContext();

        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuario/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioID,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (ValidarUsuario(usuario.Login) == false)
                {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "O nome de usuário informado já existe no sistema!");
                }
            }

            return View(usuario);
        }

        // GET: Usuario/CreateUsuarioNaoLogado
        [AllowAnonymous]
        public ActionResult CreateUsuarioNaoLogado()
        {
            return View();
        }

        // POST: Usuario/Create
        //[Authorize] AQUI Não tem pq pode ser primeiro cadastro
        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult CreateUsuarioNaoLogado(string usuario, string senha, string confirmarSenha)
        {
            string retorno = string.Empty;

            if(string.IsNullOrEmpty(usuario) == true)
            {
                retorno = "É necessário informar o nome do usuário.";
            }
            else if (string.IsNullOrEmpty(senha) == true)
            {
                retorno = "É necessário informar a senha.";
            }
            else if (string.IsNullOrEmpty(confirmarSenha) == true)
            {
                retorno = "É necessário confirmar a senha.";
            }
            else if(senha != confirmarSenha)
            {
                retorno = "As senhas informadas não conferem.";
            }
            else if (ValidarUsuario(usuario) == true)
            {
                retorno = "O nome de usuário informado já existe no sistema.";
            }
            else
            {
                Usuario novoUsuario = new Usuario();
                novoUsuario.Login = usuario;
                novoUsuario.Senha = senha;

                db.Usuario.Add(novoUsuario);
                db.SaveChanges();

                retorno = "Sucess";
            }


            return Json(new { Mensagem = retorno }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult VoltarLogin(Usuario usuario)
        {
            return RedirectToAction("Login", "Home");
        }



        private bool ValidarUsuario(string login)
        {
            var loginExiste = db.Usuario.ToList().Where(p => p.Login == login).FirstOrDefault();

            if (loginExiste != null)
                return true;
            else
                return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
