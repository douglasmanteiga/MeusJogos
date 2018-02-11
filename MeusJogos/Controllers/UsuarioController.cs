using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeusJogos.Models;
using MeusJogos.Application.Interface;
using AutoMapper;
using MeusJogos.Domain.Entities;

namespace MeusJogos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }


        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            var viewModel = Mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioViewModel>>(_usuarioAppService.GetAll());

            return View(viewModel);
        }

        // GET: Usuario/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = _usuarioAppService.GetById(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(viewModel);
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
        public ActionResult Create([Bind(Include = "UsuarioID,Login,Senha")] UsuarioViewModel usuario)
        {
            if (usuario != null && string.IsNullOrEmpty(usuario.Login) == false)
            {
                if (_usuarioAppService.UsuarioExistenteNoSistema(usuario.Login) != null)
                {
                    ModelState.AddModelError("", "O nome de usuário informado já existe no sistema.");
                }
            }

            if (ModelState.IsValid)
            {

                var viewModel = Mapper.Map<UsuarioViewModel, Usuario>(usuario);
                _usuarioAppService.Add(viewModel);

                return RedirectToAction("Index");
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

            else if (_usuarioAppService.UsuarioExistenteNoSistema(usuario) != null)
            {
                retorno = "O nome de usuário informado já existe no sistema.";
            }
            else
            {
                UsuarioViewModel novoUsuario = new UsuarioViewModel();
                novoUsuario.Login = usuario;
                novoUsuario.Senha = senha;

                var viewModel = Mapper.Map<UsuarioViewModel, Usuario>(novoUsuario);
                _usuarioAppService.Add(viewModel);

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

            var usuario = _usuarioAppService.GetById(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(viewModel);
        }

        // POST: Usuario/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioID,Login,Senha")] UsuarioViewModel usuario)
        {
            //var usuarioExistente =_usuarioAppService.UsuarioExistenteNoSistema(usuario.Login);

            //if(usuarioExistente != null && usuarioExistente.UsuarioID != usuario.UsuarioID)
            //{
            //    ModelState.AddModelError("", "O nome de usuário informado já existe no sistema.");
            //}

            if (ModelState.IsValid)
            {

                var viewModel = Mapper.Map<UsuarioViewModel, Usuario>(usuario);

                _usuarioAppService.Update(viewModel);

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

            var usuario = _usuarioAppService.GetById(id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

            return View(viewModel);
        }

        // POST: Usuario/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = _usuarioAppService.GetById(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            _usuarioAppService.Remove(usuario);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult VoltarLogin(UsuarioViewModel usuario)
        {
            return RedirectToAction("Login", "Home");
        }

    }
}
