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
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt)
            {
                Value = 1
            };

            return _context.PadreT.FromSqlRaw("exec Padres_SP @Opcion",
                            opc).ToList();
        }

        public Padre ObtenerDetalle(int? id)
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt)
            {
                Value = 2
            };
            var spId = new SqlParameter("@Id", SqlDbType.Int)
            {
                Value = id
            };

            return (_context.Padres.FromSqlRaw("exec Padres_SP @Opcion, @Id",
                            opc, spId).ToList()).FirstOrDefault();
        }
    }
}
