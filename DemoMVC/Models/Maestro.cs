using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Models
{
    public class Maestro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
    }

    public class MaestroTabla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class MaestroReturn
    {
        public int Id { get; set; }
    }
}
