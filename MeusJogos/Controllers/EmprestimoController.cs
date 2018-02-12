using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeusJogos.Models;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using PagedList;
using MeusJogos.Application.Interface;
using AutoMapper;
using MeusJogos.Domain.Entities;

namespace MeusJogos.Controllers
{
    public class EmprestimoController : Controller
    {        
        private readonly IEmprestimoAppService _emprestimoAppService;
        private readonly IEmprestimoSituacaoAppService _emprestimoSituacaoAppService;        
        private readonly IAmigoAppService _amigoAppService;
        private readonly IJogoAppService _jogoAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public EmprestimoController(IEmprestimoAppService emprestimoAppService,
            IEmprestimoSituacaoAppService emprestimoSituacaoAppService,
            IAmigoAppService amigoAppService,            
            IJogoAppService jogoAppService,
            IUsuarioAppService usuarioAppService
            )
        {
            _emprestimoAppService = emprestimoAppService;
            _emprestimoSituacaoAppService = emprestimoSituacaoAppService;
            _amigoAppService = amigoAppService;
            _jogoAppService = jogoAppService;
            _usuarioAppService = usuarioAppService;
        }

        private void DadosComboBox()
        {

            var situacao = Mapper.Map<IEnumerable<EmprestimoSituacao>, IEnumerable<EmprestimoSituacaoViewModel>>(_emprestimoSituacaoAppService.GetAll());
            var amigo = Mapper.Map<IEnumerable<Amigo>, IEnumerable<AmigoViewModel>>(_amigoAppService.GetAll());
            var jogo = Mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoViewModel>>(_jogoAppService.GetAll());

            ViewBag.Situacao = new SelectList(situacao, "EmprestimoSituacaoID", "Descricao");
            ViewBag.Amigo = new SelectList(amigo, "AmigoID", "Nome");
            ViewBag.Jogo = new SelectList(jogo, "JogoID", "Nome");
        }

        // GET: Emprestimo
        [Authorize]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //NomeAsc
            //NomeDesc
            //DataAsc
            //DataDesc

            if (sortOrder == "Nome")
            {
                if (Session["ordenacao"] != null && Session["ordenacao"].Equals("NomeAsc"))
                    Session["ordenacao"] = "NomeDesc";
                else
                    Session["ordenacao"] = "NomeAsc";
            }

            else if (sortOrder == "Data")
            {
                if (Session["ordenacao"] != null && Session["ordenacao"].Equals("DataAsc"))
                    Session["ordenacao"] = "DataDesc";
                else
                    Session["ordenacao"] = "DataAsc";
            }

            if (Session["ordenacao"] != null)
                sortOrder = Session["ordenacao"].ToString();

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var emprestimos = from s in _emprestimoAppService.GetAll() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                emprestimos = emprestimos.Where(s => s.Amigo.Nome.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                //NomeAsc
                //NomeDesc
                //DataAsc
                //DataDesc

                case "NomeAsc":
                    emprestimos = emprestimos.OrderBy(s => s.Amigo.Nome);
                    break;
                case "NomeDesc":
                    emprestimos = emprestimos.OrderByDescending(s => s.Amigo.Nome);
                    break;
                case "DataAsc":
                    emprestimos = emprestimos.OrderBy(s => s.DataHora);
                    break;
                case "DataDesc":
                    emprestimos = emprestimos.OrderByDescending(s => s.DataHora);
                    break;
                default:
                    emprestimos = emprestimos.OrderByDescending(s => s.DataHora);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var viewModel = Mapper.Map<IEnumerable<Emprestimo>, IEnumerable<EmprestimoViewModel>>(emprestimos);

            return View(viewModel.ToPagedList(pageNumber, pageSize));
        }

        // GET: Emprestimo/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emprestimo = _emprestimoAppService.GetById(id.Value);

            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Emprestimo, EmprestimoViewModel>(emprestimo);

            return View(viewModel);
        }



        // GET: Emprestimo/Create
        [Authorize]
        public ActionResult Create()
        {
            DadosComboBox();

            return View();
        }

        // POST: Emprestimo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmprestimoID,DataHora,DataEmprestimo,DataProgramadaDevolucao,EmprestimoSituacaoID,AmigoID,JogoID")] EmprestimoViewModel emprestimo)
        {
            DadosComboBox();

            var usuarioLogado = _usuarioAppService.UsuarioExistenteNoSistema(User.Identity.Name);

            if (usuarioLogado == null || usuarioLogado.UsuarioID <= 0)
            {
                ModelState.AddModelError("", "O usuário logado não foi encontrado, cadastre um novo usuário e realize o login para prosseguir com a operação!");
            }
            //Seta o usuário logado para salvar no banco o log
            emprestimo.Usuario = Mapper.Map<Usuario, UsuarioViewModel>(usuarioLogado);
            emprestimo.DataHora = DateTime.Now;

            if (emprestimo.EmprestimoSituacaoID <= 0)
                ModelState.AddModelError("", "É necessário informar a situação!");

            if (emprestimo.AmigoID <= 0)
                ModelState.AddModelError("", "É necessário informar o amigo!");

            if (emprestimo.JogoID <= 0)
                ModelState.AddModelError("", "É necessário informar o jogo!");

            if (ModelState.IsValid)
            {
                var viewModel = Mapper.Map<EmprestimoViewModel, Emprestimo>(emprestimo);
                _emprestimoAppService.Add(viewModel);
                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        // GET: Emprestimo/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            DadosComboBox();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emprestimo = _emprestimoAppService.GetById(id.Value);

            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Emprestimo, EmprestimoViewModel>(emprestimo);

            return View(viewModel);
        }

        // POST: Emprestimo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmprestimoID,DataHora,DataEmprestimo,DataProgramadaDevolucao,EmprestimoSituacaoID,AmigoID,JogoID")] EmprestimoViewModel emprestimo)
        {
            DadosComboBox();

            if (ModelState.IsValid)
            {
                var usuarioLogado = _usuarioAppService.UsuarioExistenteNoSistema(User.Identity.Name);
                emprestimo.UsuarioID = usuarioLogado.UsuarioID;
                emprestimo.Usuario = Mapper.Map<Usuario, UsuarioViewModel>(usuarioLogado);

                var viewModel = Mapper.Map<EmprestimoViewModel, Emprestimo>(emprestimo);
                

                _emprestimoAppService.Update(viewModel);

                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var emprestimo = _emprestimoAppService.GetById(id.Value);

            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Emprestimo, EmprestimoViewModel>(emprestimo);

            return View(viewModel);
        }

        // POST: Emprestimo/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var emprestimo = _emprestimoAppService.GetById(id);

            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            _emprestimoAppService.Remove(emprestimo);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
