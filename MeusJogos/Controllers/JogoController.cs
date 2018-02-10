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
using MeusJogos.Domain.Entities;
using AutoMapper;

namespace MeusJogos.Controllers
{
    public class JogoController : Controller
    {
        private readonly IJogoAppService _jogoAppService;

        public JogoController(IJogoAppService jogoAppService)
        {
            _jogoAppService = jogoAppService;
        }

        // GET: Jogo
        [Authorize]
        public ActionResult Index()
        {
            var viewModel = Mapper.Map<IEnumerable<Jogo>, IEnumerable<JogoViewModel>>(_jogoAppService.GetAll());

            return View(viewModel);
        }

        // GET: Jogo/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var jogo = _jogoAppService.GetById(id.Value);

            if (jogo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Jogo, JogoViewModel>(jogo);

            return View(viewModel);
        }

        // GET: Jogo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jogo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JogoID,Nome,Ativo")] JogoViewModel jogo)
        {
            if (ModelState.IsValid)
            {
                var viewModel = Mapper.Map<JogoViewModel, Jogo>(jogo);
                _jogoAppService.Add(viewModel);

                return RedirectToAction("Index");
            }

            return View(jogo);
        }

        // GET: Jogo/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var jogo = _jogoAppService.GetById(id.Value);

            if (jogo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Jogo, JogoViewModel>(jogo);

            return View(viewModel);
        }

        // POST: Jogo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JogoID,Nome,Ativo")] JogoViewModel jogo)
        {
            if (ModelState.IsValid)
            {

                var viewModel = Mapper.Map<JogoViewModel, Jogo>(jogo);

                _jogoAppService.Update(viewModel);

                return RedirectToAction("Index");
            }

            return View(jogo);
        }
        [Authorize]
        // GET: Jogo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var jogo = _jogoAppService.GetById(id.Value);

            if (jogo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Jogo, JogoViewModel>(jogo);

            return View(viewModel);
        }

        [Authorize]
        // POST: Jogo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jogo = _jogoAppService.GetById(id);

            if (jogo == null)
            {
                return HttpNotFound();
            }

            _jogoAppService.Remove(jogo);

            return RedirectToAction("Index");
        }

    }
}
