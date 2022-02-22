﻿using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Context
{
    public class SchoolDatabaseContext : DbContext
    {
        public SchoolDatabaseContext(DbContextOptions<SchoolDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<MaestroTabla> MaestrosT { get; set; }
        public DbSet<MaestroReturn> MaestrosR { get; set; }

        public DbSet<Salon> Salones { get; set; }
        public DbSet<SalonTabla> SalonT { get; set; }
        public DbSet<SalonReturn> SalonR { get; set; }
    }
}
