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
    public class ClienteController : Controller
    {

        private ApplicationDbContext _context;

        public ClienteController()
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
            var cliente = _context.Cliente.ToList();
            if (User.IsInRole(RoleName.CanManageCliente))
                return View(cliente);

            return View("ReadOnlyIndex", cliente);
        }

        [Authorize(Roles = RoleName.CanManageCliente)]
        public ActionResult New()
        {
           var cliente = _context.Cliente.ToList();
            var viewModel = new ClienteFormViewModel
            {
                Cliente = new Cliente()
            };

            return View("ClienteForm", viewModel);
        }


        public ActionResult Details(int id)
        {
            var cliente = _context.Cliente.SingleOrDefault(c => c.Id == id);

            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }
        [HttpPost] // só será acessada com POST
        public ActionResult Save(Cliente cliente) // recebemos um cliente
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ClienteFormViewModel
                {
                    Cliente = cliente,

                };

                return View("ClienteForm", viewModel);
            }

            if (cliente.Id == 0)
            {
                // armazena o cliente em memória
                _context.Cliente.Add(cliente);
            }
            else
            {
                var clienteInDb = _context.Cliente.Single(c => c.Id == cliente.Id);

                clienteInDb.Nome = cliente.Nome;
                clienteInDb.Idade = cliente.Idade;
                clienteInDb.CPF = cliente.CPF;
            }

            // faz a persistência
            _context.SaveChanges();
            // Voltamos para a lista de clientes
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cliente = _context.Cliente.SingleOrDefault(c => c.Id == id);

            if (cliente == null)
                return HttpNotFound();

            var viewModel = new ClienteFormViewModel
            {
                Cliente = cliente,
            };

            return View("ClienteForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var cliente = _context.Cliente.SingleOrDefault(c => c.Id == id);

            if (cliente == null)
                return HttpNotFound();

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            return new HttpStatusCodeResult(200);
        }

    }
}