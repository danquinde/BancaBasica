using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancaBasica.WebApp.Data;
using BancaBasica.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BancaBasica.WebApp.Controllers
{
    public class CuentaController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CuentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CuentaController
        public ActionResult Index()
        {
            IEnumerable<Cuenta> data = _context.Cuenta.
                                        Include("Cliente").
                                        Where(x => !x.Eliminada);

            return View(data);
        }

        // GET: CuentaController/Create
        public ActionResult Create()
        {
            var clienteId = new SelectList(_context.Cliente.Where(x => !x.Eliminado).ToDictionary(x => x.Id, x => x.Nombre), "Key", "Value");

            ViewBag.ClienteId = clienteId;

            return View();
        }

        // POST: CuentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cuenta cuenta)
        {
            try
            {
                _context.Cuenta.Add(cuenta);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentaController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _context.Cuenta.Include("Cliente").Where(x => x.Id == id).FirstOrDefault();

            return View(data);
        }

        // POST: CuentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cuenta cuenta)
        {
            try
            {
                _context.Cuenta.Update(cuenta);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CuentaController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _context.Cuenta.Include("Cliente").Where(x => x.Id == id).FirstOrDefault();

            return View(data);
        }

        // POST: CuentaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cuenta cuenta)
        {
            try
            {
                var data = _context.Cuenta.Find(id);
                data.Eliminada = true;

                _context.Cuenta.Update(data);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
