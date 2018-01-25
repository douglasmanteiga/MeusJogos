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

namespace MeusJogos.Controllers
{
    public class EmprestimoController : Controller
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoMeusJogos"].ToString();
        private MeusJogosContext db = new MeusJogosContext();

        private void DadosComboBox()
        {
            ViewBag.Situacao = new SelectList(db.EmprestimoSituacao.ToList(), "EmprestimoSituacaoID", "Descricao");
            ViewBag.Amigo = new SelectList(db.Amigo.ToList(), "AmigoID", "Nome");
            ViewBag.Jogo = new SelectList(db.Jogo.ToList(), "JogoID", "Nome");
        }

        // GET: Emprestimo
        [Authorize]
        public ActionResult Index()
        {

            return View(db.Emprestimo.ToList());
        }

        // GET: Emprestimo/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
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
        public ActionResult Create([Bind(Include = "EmprestimoID,DataHora,DataEmprestimo,DataProgramadaDevolucao,EmprestimoSituacaoID,AmigoID,JogoID")] Emprestimo emprestimo)
        {
            DadosComboBox();

            //Seta o usuário logado para salvar no banco o log
            emprestimo.Usuario = db.Usuario.Where(p => p.Login == User.Identity.Name).FirstOrDefault();
            emprestimo.DataHora = DateTime.Now;

            if (emprestimo.EmprestimoSituacaoID <= 0)
                ModelState.AddModelError("", "É necessário informar a situação!");

            if (emprestimo.AmigoID <= 0)
                ModelState.AddModelError("", "É necessário informar o amigo!");

            if (emprestimo.JogoID <= 0)
                ModelState.AddModelError("", "É necessário informar o jogo");


            if (ModelState.IsValid)
            {
                //var amigo = db.Amigo.Find(emprestimo.AmigoID);

                db.Emprestimo.Add(emprestimo);
                db.SaveChanges();
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
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmprestimoID,DataHora,DataEmprestimo,DataProgramadaDevolucao,EmprestimoSituacaoID,AmigoID,JogoID")] Emprestimo emprestimo)
        {
            DadosComboBox();

            var objetoNoBanco = db.Emprestimo.Find(emprestimo.EmprestimoID);
            //emprestimo = objetoNoBanco;

            if (objetoNoBanco != null)
            {
                //Mantem o que foi cadastrado inicialmente
                emprestimo.DataHora = objetoNoBanco.DataHora;
                emprestimo.Usuario = objetoNoBanco.Usuario;
            }

            //emprestimo.EmprestimoSituacao = db.EmprestimoSituacao.Find(emprestimo.EmprestimoSituacaoID);
            //emprestimo.Amigo = db.Amigo.Find(emprestimo.AmigoID);
            //emprestimo.Jogo = db.Jogo.Find(emprestimo.JogoID);

            if (ModelState.IsValid)
            {
                //Comando SQL
                string query = string.Format("update tblEmprestimo set EmprestimoSituacaoID = {0}, AmigoID = {1}, JogoID = {2}, DataEmprestimo = '{4}', DataProgramadaDevolucao = '{5}' where EmprestimoID = {3}", emprestimo.EmprestimoSituacaoID, emprestimo.AmigoID, emprestimo.JogoID, emprestimo.EmprestimoID, emprestimo.DataEmprestimo.ToString("MM/dd/yyyy HH:mm:ss"), emprestimo.DataProgramadaDevolucao.ToString("MM/dd/yyyy HH:mm:ss"));

                //Utilizando dapper
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    int rowsAffected = sqlConnection.Execute(query);

                    return RedirectToAction("Index");
                }

                
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
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            db.Emprestimo.Remove(emprestimo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private Usuario UsuarioLogado(string login)
        {
            using (MeusJogosContext db = new MeusJogosContext())
            {
                return db.Usuario.Where(p => p.Login == login).FirstOrDefault();
            }
        }
    }
}
