using DemoMVC.Context;
using DemoMVC.Models;
using DemoMVC.ParametroSp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

/*
 Procesos CRUD utilizando SqlClient para la ejecucion de Procedimientos Almacenados
Se utiliza la clase MaestroParamSP para definir los parametros requeridos por el SP
 */

namespace DemoMVC.Controllers
{
    public class MaestroController : Controller
    {
        private readonly SchoolDatabaseContext _context;

        public MaestroController(SchoolDatabaseContext context)
        {
            _context = context;
        }

        // GET: MaestroController
        public async Task<IActionResult> Index()
        {
            MaestroParamSP input = new MaestroParamSP();
            var opc = new SqlParameter("@Opcion", input.Opcion);
            opc.Value = 1;

            return View(await _context.MaestrosT.FromSqlRaw("exec Maestros_SP @Opcion",
                            opc).ToListAsync());
        }

        // GET: MaestroController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaestroParamSP input = new MaestroParamSP();
            var spOpc = new SqlParameter("@Opcion", input.Opcion);
            var spId = new SqlParameter("@Id", input.Id);
            spOpc.Value = 2;
            spId.Value = id;

            Maestro datos = (await _context.Maestros.FromSqlRaw("exec Maestros_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // GET: MaestroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaestroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Edad,Telefono,Domicilio")] Maestro maestro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MaestroParamSP input = new MaestroParamSP();
                    var spOpc = new SqlParameter("@Opcion", input.Opcion);
                    var spNo = new SqlParameter("@Nombre", input.Nombre);
                    var spEd = new SqlParameter("@Edad", input.Edad);
                    var spTe = new SqlParameter("@Telefono", input.Telefono);
                    var spDo = new SqlParameter("@Domicilio", input.Domicilio);
                    spOpc.Value = 3;
                    spNo.Value = maestro.Nombre;
                    spEd.Value = maestro.Edad;
                    spTe.Value = maestro.Telefono;
                    spDo.Value = maestro.Domicilio;

                    //Se guarda el registro y se retorna el total de guardados (count=1)
                    var datos = await _context.MaestrosR.FromSqlInterpolated($"exec Maestros_SP @Opcion={spOpc}, @Nombre={spNo}, @Edad={spEd}, @Telefono={spTe}, @Domicilio={spDo}").ToListAsync();
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: MaestroController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaestroParamSP input = new MaestroParamSP();
            var spOpc = new SqlParameter("@Opcion", input.Opcion);
            var spId = new SqlParameter("@Id", input.Id);
            spOpc.Value = 2;
            spId.Value = id;

            Maestro datos = (await _context.Maestros.FromSqlRaw("exec Maestros_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();

            if (datos == null)
            {
                return NotFound();
            }
            return View(datos);
        }

        // POST: MaestroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Edad,Telefono,Domicilio")] Maestro maestro)
        {
            try
            {
                if (id != maestro.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        MaestroParamSP input = new MaestroParamSP();
                        var spOpc = new SqlParameter("@Opcion", input.Opcion);
                        var spId = new SqlParameter("@Id", input.Id);
                        var spNo = new SqlParameter("@Nombre", input.Nombre);
                        var spEd = new SqlParameter("@Edad", input.Edad);
                        var spTe = new SqlParameter("@Telefono", input.Telefono);
                        var spDo = new SqlParameter("@Domicilio", input.Domicilio);
                        spOpc.Value = 4;
                        spId.Value = id;
                        spNo.Value = maestro.Nombre;
                        spEd.Value = maestro.Edad;
                        spTe.Value = maestro.Telefono;
                        spDo.Value = maestro.Domicilio;

                        //Se guarda el registro y se retorna el total de guardados (count=1)
                        var datos = await _context.MaestrosR.FromSqlInterpolated($"exec Maestros_SP @Opcion={spOpc}, @Id={spId}, @Nombre={spNo}, @Edad={spEd}, @Telefono={spTe}, @Domicilio={spDo}").ToListAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(maestro.Id))
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
                return View(maestro);
            }
            catch
            {
                return View();
            }
        }

        // GET: MaestroController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaestroParamSP input = new MaestroParamSP();
            var spOpc = new SqlParameter("@Opcion", input.Opcion);
            var spId = new SqlParameter("@Id", input.Id);
            spOpc.Value = 5;
            spId.Value = id;

            MaestroTabla datos = (await _context.MaestrosT.FromSqlRaw("exec Maestros_SP @Opcion, @Id",
                            spOpc, spId).ToListAsync()).FirstOrDefault();
            if (datos == null)
            {
                return NotFound();
            }

            return View(datos);
        }

        // POST: MaestroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                MaestroParamSP input = new MaestroParamSP();
                var spOpc = new SqlParameter("@Opcion", input.Opcion);
                var spId = new SqlParameter("@Id", input.Id);
                spOpc.Value = 6;
                spId.Value = id;

                var datos = await _context.MaestrosR.FromSqlRaw("exec Maestros_SP @Opcion, @Id",
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
