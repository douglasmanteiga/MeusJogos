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
    public class AmigoController : Controller
    {
        private readonly IAmigoAppService _amigoAppService;

        //O controller de Amigo espera no construtor um objeto IAmigoAppService
        //Precisamos injetar esse parâmetro no construor através de um container de injeção de dependência (existem vários no caso utilizei o Simple)
        //Para instalar utilize o Packege Manger Consolor selecionando o Projeto MVC. Digite o seguinte comando: Install-Package Ninject.MVC5
        //Injeção de dependência

        public AmigoController(IAmigoAppService amigoAppService)
        {
            _amigoAppService = amigoAppService;
        }

        // GET: Amigo
        [Authorize]
        public ActionResult Index()
        {
            var viewModel = Mapper.Map<IEnumerable<Amigo>, IEnumerable<AmigoViewModel>>(_amigoAppService.GetAll());

            return View(viewModel);
        }

        // GET: Amigo/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var amigo = _amigoAppService.GetById(id.Value);            

            if (amigo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Amigo, AmigoViewModel>(amigo);

            return View(viewModel);
        }

        // GET: Amigo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create([Bind(Include = "AmigoID,Nome,Email")] AmigoViewModel amigo)
        {
            if (ModelState.IsValid)
            {
                var viewModel = Mapper.Map<AmigoViewModel, Amigo>(amigo);
                _amigoAppService.Add(viewModel);

                return RedirectToAction("Index");
            }

            return View(amigo);
        }

        // GET: Amigo/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var amigo = _amigoAppService.GetById(id.Value);

            if (amigo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Amigo, AmigoViewModel>(amigo);

            return View(viewModel);
        }

        // POST: Amigo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AmigoID,Nome,Email")] AmigoViewModel amigo)
        {
            if (ModelState.IsValid)
            {

                var viewModel = Mapper.Map<AmigoViewModel, Amigo>(amigo);

                _amigoAppService.Update(viewModel);

                return RedirectToAction("Index");
            }

            return View(amigo);
        }

        // GET: Amigo/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var amigo = _amigoAppService.GetById(id.Value);

            if (amigo == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Amigo, AmigoViewModel>(amigo);

            return View(viewModel);
        }

        // POST: Amigo/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var amigo = _amigoAppService.GetById(id);

            if (amigo == null)
            {
                return HttpNotFound();
            }

            _amigoAppService.Remove(amigo);
            
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
