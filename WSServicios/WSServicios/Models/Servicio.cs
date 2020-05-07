using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSServicios.Models
{
    public class Servicio
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }

    }
}
