using System;
using System.Collections.Generic;
using System.Linq;
using BancaBasica.WebApp.Data;
using BancaBasica.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BancaBasica.WebApp.Controllers
{
    public class MovimientoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MovimientoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovimientoController
        public ActionResult Index()
        {
            var data = _context.Movimiento.
                        Include("Cuenta").
                        Where(x => !x.Eliminado).
                        OrderByDescending(x => x.Id).
                        ToList();

            foreach (var mov in data)
            {
                mov.Tipo = mov.Tipo.Equals("DEB") ? "Débito" : "Crédito";
            }

            return View(data);
        }

       
        // GET: MovimientoController/Create
        public ActionResult Create()
        {            
            var cuentaId = new SelectList(_context.Cuenta.Where(x => !x.Eliminada).ToDictionary(x => x.Id, x => $"{x.Numero} - {x.Saldo}"), "Key", "Value");            
            ViewBag.CuentaId = cuentaId;

            IEnumerable<SelectListItem> GetTipoMovimSelectItems()
            {
                yield return new SelectListItem { Text = "Crédito", Value = "CRE" };
                yield return new SelectListItem { Text = "Débito", Value = "DEB" };
            }

            var tipoCod = new SelectList(GetTipoMovimSelectItems().ToDictionary(x => x.Value, x => x.Text), "Key", "Value");
            ViewBag.TipoCod = tipoCod;

            return View();
        }

        // POST: MovimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movimiento movimiento)
        {
            try
            {
                var cuenta = SaldoCuentaAlInsertar(movimiento);

                if (cuenta.Saldo < 0)
                {
                    TempData["mensajeError"] = $"Error, el Saldo de la cuenta {cuenta.Numero} no puede quedar en negativo";                    
                    return RedirectToAction(nameof(Create));
                }

                _context.Cuenta.Update(cuenta);
                
                _context.Movimiento.Add(movimiento);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Cuenta SaldoCuentaAlInsertar(Movimiento movimiento)
        {
            decimal nuevoSaldo = 0;

            var cuenta = _context.Cuenta.Find(movimiento.CuentaId);

            if (movimiento.Tipo.Equals("CRE"))
            {
                nuevoSaldo = cuenta.Saldo - movimiento.Valor;
            }
            else if (movimiento.Tipo.Equals("DEB"))
            {
                nuevoSaldo = cuenta.Saldo + movimiento.Valor;
            }

            cuenta.Saldo = nuevoSaldo;

            return cuenta;
        }

        // GET: MovimientoController/Edit/5
        public ActionResult Edit(int id)
        {
            var cuentaId = new SelectList(_context.Cuenta.Where(x => !x.Eliminada).ToDictionary(x => x.Id, x => $"{x.Numero} - {x.Saldo}"), "Key", "Value");
            ViewBag.CuentaId = cuentaId;

            var data = _context.Movimiento.Include("Cuenta").Where(x => x.Id == id).FirstOrDefault();
            data.Tipo = data.Tipo.Equals("DEB") ? "Débito" : "Crédito";

            return View(data);
        }

        // POST: MovimientoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Movimiento movimiento)
        {
            try
            {
                movimiento.Tipo = movimiento.Tipo.Equals("Débito") ? "DEB" : "CRE";

                var movActualizar = _context.Movimiento.Find(id);
                
                var cuenta = SaldoCuentaAlActualizar(movimiento, movActualizar.Valor);

                movActualizar.Fecha = movimiento.Fecha;
                movActualizar.Valor = movimiento.Valor;

                if (cuenta.Saldo < 0)
                {
                    TempData["mensajeError"] = $"Error, el Saldo de la cuenta {cuenta.Numero} no puede quedar en negativo";
                    return RedirectToAction(nameof(Edit);
                }

                _context.Cuenta.Update(cuenta);

                _context.Movimiento.Update(movActualizar);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Cuenta SaldoCuentaAlActualizar(Movimiento movimiento, decimal valorAnterior)
        {
            decimal nuevoSaldo = 0;

            var cuenta = _context.Cuenta.Find(movimiento.CuentaId);

            if (movimiento.Tipo.Equals("CRE"))
            {
                nuevoSaldo = (cuenta.Saldo - valorAnterior) + movimiento.Valor;
            }
            else if (movimiento.Tipo.Equals("DEB"))
            {
                nuevoSaldo = (cuenta.Saldo + valorAnterior) - movimiento.Valor;
            }

            cuenta.Saldo = nuevoSaldo;

            return cuenta;
        }


        // GET: MovimientoController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _context.Movimiento.Include("Cuenta").Where(x => x.Id == id).FirstOrDefault();
            data.Tipo = data.Tipo.Equals("DEB") ? "Débito" : "Crédito";

            return View(data);
        }

        // POST: MovimientoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movimiento movimiento)
        {
            try
            {                
                var data = _context.Movimiento.Find(id);
                data.Eliminado = true;

                var cuenta = SaldoCuentaAlEliminar(data);

                if (cuenta.Saldo < 0)
                {
                    TempData["mensajeError"] = $"Error, el Saldo de la cuenta {cuenta.Numero} no puede quedar en negativo";
                    return RedirectToAction(nameof(Delete);
                }
                
                _context.Cuenta.Update(cuenta);

                _context.Movimiento.Update(data);

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Cuenta SaldoCuentaAlEliminar(Movimiento movimiento)
        {
            decimal nuevoSaldo = 0;

            var cuenta = _context.Cuenta.Find(movimiento.CuentaId);

            if (movimiento.Tipo.Equals("CRE"))
            {
                nuevoSaldo = cuenta.Saldo + movimiento.Valor;
            }
            else if (movimiento.Tipo.Equals("DEB"))
            {
                nuevoSaldo = cuenta.Saldo - movimiento.Valor;
            }

            cuenta.Saldo = nuevoSaldo;

            return cuenta;
        }
    }
}
