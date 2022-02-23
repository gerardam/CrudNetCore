using DemoMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Context
{
    public class PadreCrud
    {
        private readonly SchoolDatabaseContext _context;

        public PadreCrud()
        {
        }

        public PadreCrud(SchoolDatabaseContext context)
        {
            _context = context;
        }

        public List<PadreTabla> ObtenerIndex()
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 1 };

            return _context.PadreT.FromSqlRaw("exec Padres_SP @Opcion",
                            opc).ToList();
        }

        public Padre ObtenerDetalle(int? id)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 2 };
            var spId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            return (_context.Padres.FromSqlRaw("exec Padres_SP @Opcion, @Id",
                            opc, spId).ToList()).FirstOrDefault();
        }

        public void Insertar(Padre padre)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 3 };
            var spNo = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = padre.Nombre };
            var spEd = new SqlParameter("@Edad", SqlDbType.Int) { Value = padre.Edad };
            var spTe = new SqlParameter("@Telefono", SqlDbType.VarChar) { Value = padre.Telefono };
            var spDo = new SqlParameter("@Domicilio", SqlDbType.VarChar) { Value = padre.Domicilio };
            var spHi = new SqlParameter("@Hijos", SqlDbType.TinyInt) { Value = padre.Hijos };

            _context.PadreR.FromSqlInterpolated($"exec Padres_SP @Opcion={opc}, @Nombre={spNo}, @Edad={spEd}, @Telefono={spTe}, @Domicilio={spDo}, @Hijos={spHi}").ToListAsync();
        }

        public void Actualizar(Padre padre)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 4 };
            var spId = new SqlParameter("@Id", SqlDbType.Int) { Value = padre.Id };
            var spNo = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = padre.Nombre };
            var spEd = new SqlParameter("@Edad", SqlDbType.Int) { Value = padre.Edad };
            var spTe = new SqlParameter("@Telefono", SqlDbType.VarChar) { Value = padre.Telefono };
            var spDo = new SqlParameter("@Domicilio", SqlDbType.VarChar) { Value = padre.Domicilio };
            var spHi = new SqlParameter("@Hijos", SqlDbType.TinyInt) { Value = padre.Hijos };

            _context.PadreR.FromSqlInterpolated($"exec Padres_SP @Opcion={opc}, @Id={spId}, @Nombre={spNo}, @Edad={spEd}, @Telefono={spTe}, @Domicilio={spDo}, @Hijos={spHi}").ToListAsync();
        }

        public PadreTabla ObtenerEliminar(int? id)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 5 };
            var spId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            return (_context.PadreT.FromSqlRaw("exec Padres_SP @Opcion, @Id",
                            opc, spId).ToList()).FirstOrDefault();
        }

        public int Eliminar(int id)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt) { Value = 6 };
            var spId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            return _context.PadreT.FromSqlRaw("exec Padres_SP @Opcion, @Id",
                            opc, spId).ToListAsync().Id;
        }
    }
}
