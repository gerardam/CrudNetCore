using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Models
{
    public class Salon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Grado { get; set; }
        public string Grupo { get; set; }
        public int Total { get; set; }
    }

    public class SalonTabla
    {
        public int Id { get; set; }
        public string Grado { get; set; }
        public string Grupo { get; set; }
    }

    public class SalonReturn
    {
        public int Id { get; set; }
    }
}
