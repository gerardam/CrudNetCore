using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVC.Context;
using DemoMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class PadreController : Controller
    {
        private readonly SchoolDatabaseContext _context;

        public PadreController(SchoolDatabaseContext context)
        {
            _context = context;
        }

        // GET: PadreController
        public IActionResult Index()
        {
            List<PadreTabla> datos = new PadreCrud(_context).ObtenerIndex();
            return View(datos);
        }

        // GET: PadreController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Padre datos = new PadreCrud(_context).ObtenerDetalle(id);
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // GET: PadreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PadreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,Edad,Telefono,Domicilio,Hijos")] Padre padre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    new PadreCrud(_context).Insertar(padre);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PadreController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Padre datos = new PadreCrud(_context).ObtenerDetalle(id);
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // POST: PadreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Edad,Telefono,Domicilio,Hijos")] Padre padre)
        {
            try
            {
                if (id != padre.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        new PadreCrud(_context).Actualizar(padre);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ExisteRegistro(padre.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(padre);
            }
            catch
            {
                return View();
            }
        }

        // GET: PadreController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PadreTabla datos = new PadreCrud(_context).ObtenerEliminar(id);
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // POST: PadreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                int datos = new PadreCrud(_context).Eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool ExisteRegistro(int id)
        {
            return _context.Padres.Any(e => e.Id == id);
        }
    }
}
