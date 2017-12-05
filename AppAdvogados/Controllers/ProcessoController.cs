using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAdvogados.Models;
using AppAdvogados.ViewModels;

namespace AppAdvogados.Controllers
{
    [Authorize]
    public class ProcessoController : Controller
    {

        private ApplicationDbContext _context;

        public ProcessoController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var processo = _context.Processo.ToList();
            if (User.IsInRole("CanManageAdemir"))
                return View(processo);

            return View("ReadOnlyIndex", processo);
        }

        public ActionResult Details(int id)
        {
            var processo = _context.Processo.SingleOrDefault(c => c.Id == id);

            if (processo == null)
                return HttpNotFound();

            return View(processo);
        }

        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult New()
        {

            var viewModel = new ProcessoFormViewModel {
                Processo = new Processo()
            };

            return View("ProcessoForm", viewModel);
        }

        [HttpPost] // só será acessada com POST
        public ActionResult Save(Processo processo) // recebemos um cliente
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProcessoFormViewModel
                {
                    Processo = processo,

                };

                return View("ProcessoForm", viewModel);
            }

            if (processo.Id == 0)
            {
                // armazena o cliente em memória
                _context.Processo.Add(processo);
            }
            else
            {
                var processoInDb = _context.Processo.Single(c => c.Id == processo.Id);

                processoInDb.Nome = processo.Nome;
                processoInDb.Data = processo.Data;
                processoInDb.Causa = processo.Causa;
            }

            // faz a persistência
            _context.SaveChanges();
            // Voltamos para a lista de clientes
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult Edit(int id)
        {
            var processo = _context.Processo.SingleOrDefault(c => c.Id == id);

            if (processo == null)
                return HttpNotFound();

            var viewModel = new ProcessoFormViewModel
            {
                Processo = processo,
            };

            return View("ProcessoForm", viewModel);
        }

        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult Delete(int id)
        {
            var processo = _context.Processo.SingleOrDefault(c => c.Id == id);

            if (processo == null)
                return HttpNotFound();

            _context.Processo.Remove(processo);
            _context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

    }
}