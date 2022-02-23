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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PadreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PadreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PadreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PadreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
