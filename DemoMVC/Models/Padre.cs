using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Models
{
    public class Padre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
        public byte Hijos { get; set; }
    }

    public class PadreTabla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class PadreReturn
    {
        public int Id { get; set; }
    }
}
