using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WSTerceros.Models
{
    public class Persona:Cliente
    {
        public string PrimerApellido { get; set; }
        [StringLength(30)]
        public string SegundoApellido { get; set; }
        [StringLength(30)]
        public string PrimerNombre { get; set; }
        [StringLength(30)]
        public string SegundoNombre { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
    }
}
