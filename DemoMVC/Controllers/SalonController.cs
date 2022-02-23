using DemoMVC.Context;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

/*
 Procesos CRUD utilizando SqlClient para la ejecucion de Procedimientos Almacenados
Sin utilizar clases para asignacion de tipos de dato a los parametros
 */
namespace DemoMVC.Controllers
{
    public class SalonController : Controller
    {
        private readonly SchoolDatabaseContext _context;

        public SalonController(SchoolDatabaseContext context)
        {
            _context = context;
        }

        // GET: SalonController
        public async Task<IActionResult> Index()
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
            opc.Value = 1;

            return View(await _context.SalonT.FromSqlRaw("exec Salones_SP @Opcion",
                            opc).ToListAsync());
        }

        // GET: SalonController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
            var spId = new SqlParameter("@Id", SqlDbType.Int);
            spOpc.Value = 2;
            spId.Value = id;

            Salon datos = (await _context.Salones.FromSqlRaw("exec Salones_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // GET: SalonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grado,Grupo,Total")] Salon salon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
                    var spGra = new SqlParameter("@Grado", SqlDbType.VarChar);
                    var spGru = new SqlParameter("@Grupo", SqlDbType.VarChar);
                    var spTot = new SqlParameter("@Total", SqlDbType.Int);
                    spOpc.Value = 3;
                    spGra.Value = salon.Grado;
                    spGru.Value = salon.Grupo;
                    spTot.Value = salon.Total;

                    var datos = await _context.SalonR.FromSqlInterpolated($"exec Salones_SP @Opcion={spOpc}, @Grado={spGra}, @Grupo={spGru}, @Total={spTot}").ToListAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: SalonController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
            var spId = new SqlParameter("@Id", SqlDbType.Int);
            spOpc.Value = 2;
            spId.Value = id;

            Salon datos = (await _context.Salones.FromSqlRaw("exec Salones_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();

            if (datos == null)
            {
                return NotFound();
            }
            return View(datos);
        }

        // POST: SalonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Grado,Grupo,Total")] Salon salon)
        {
            try
            {
                if (id != salon.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
                        var spId = new SqlParameter("@Id", SqlDbType.Int);
                        var spGra = new SqlParameter("@Grado", SqlDbType.VarChar);
                        var spGru = new SqlParameter("@Grupo", SqlDbType.VarChar);
                        var spTot = new SqlParameter("@Total", SqlDbType.Int);
                        spOpc.Value = 4;
                        spId.Value = id;
                        spGra.Value = salon.Grado;
                        spGru.Value = salon.Grupo;
                        spTot.Value = salon.Total;

                        //Se guarda el registro y se retorna el total de guardados (count=1)
                        var datos = await _context.SalonR.FromSqlInterpolated($"exec Salones_SP @Opcion={spOpc}, @Id={spId}, @Grado={spGra}, @Grupo={spGru}, @Total={spTot}").ToListAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(salon.Id))
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
                return View(salon);
            }
            catch
            {
                return View();
            }
        }

        // GET: SalonController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
            var spId = new SqlParameter("@Id", SqlDbType.Int);
            spOpc.Value = 5;
            spId.Value = id;

            SalonTabla datos = (await _context.SalonT.FromSqlRaw("exec Salones_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // POST: SalonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var spOpc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
                var spId = new SqlParameter("@Id", SqlDbType.Int);
                spOpc.Value = 6;
                spId.Value = id;

                var datos = await _context.SalonR.FromSqlRaw("exec Salones_SP @Opcion, @Id",
                                spOpc, spId).ToListAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Maestros.Any(e => e.Id == id);
        }
    }
}
