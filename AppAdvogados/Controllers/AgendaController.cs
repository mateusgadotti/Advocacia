using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppAdvogados.Models;
using AppAdvogados.ViewModels;
using System.Data.Entity;


namespace AppAdvogados.Controllers
{
    public class AgendaController : Controller
    {

        private ApplicationDbContext _context;

        public AgendaController()
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
            var agenda = _context.Agenda.Include(c => c.Advogado).ToList();
            if (User.IsInRole("CanManageAdemir"))
                return View(agenda);

            return View("ReadOnlyIndex", agenda);
        }

        public ActionResult Details(int id)
        {
            var agenda = _context.Agenda.Include(c => c.Advogado).ToList();


            if (agenda == null)
                return HttpNotFound();

            return View(agenda);
        }
        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult New()
        {
            var advogado = _context.Advogado.ToList();
            var viewModel = new AgendaFormViewModel
            {
                Agenda = new Agenda(),
                Advogado = advogado
            };

            return View("AgendaForm", viewModel);

        }

        [HttpPost] // só será acessada com POST
        public ActionResult Save(Agenda agenda) // recebemos um cliente
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new AgendaFormViewModel
                {
                    Agenda = agenda,
                    Advogado = _context.Advogado.ToList()
                };

                return View("AgendaForm", viewModel);
            }

            if (agenda.Id == 0)
            {
                // armazena o cliente em memória
                _context.Agenda.Add(agenda);
            }
            else
            {
                var agendaInDb = _context.Agenda.SingleOrDefault(c => c.Id == agenda.Id);

                agendaInDb.Tarefa = agenda.Tarefa;

                agendaInDb.Data = agenda.Data;

                agendaInDb.AdvogadoId = agenda.AdvogadoId;
            }

            // faz a persistência
            _context.SaveChanges();
            // Voltamos para a lista de clientes
            return RedirectToAction("Index");


        }
        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult Edit(int id)
        {
            var agenda = _context.Agenda.Include(c => c.Advogado).SingleOrDefault(c => c.Id == id);

            if (agenda == null)
                return HttpNotFound();

            var viewModel = new AgendaFormViewModel
            {
                Agenda = agenda,
                Advogado = _context.Advogado.ToList()
            };

            return View("AgendaForm", viewModel);
        }
        [Authorize(Roles = "CanManageAdemir")]
        public ActionResult Delete(int id)
        {
            var agenda = _context.Agenda.SingleOrDefault(c => c.Id == id);

            if (agenda == null)
                return HttpNotFound();

            _context.Agenda.Remove(agenda);
            _context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }
    }
}