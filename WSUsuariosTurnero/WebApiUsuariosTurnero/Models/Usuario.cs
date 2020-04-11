using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WSUsuariosTurnero.Models
{
    public class Usuario    
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string PrimerApellido { get; set; }
        [StringLength(30)]
        public string SegundoApellido { get; set; }
        [StringLength(30)]
        public string PrimerNombre { get; set; }
        [StringLength(30)]
        public string SegundoNombre { get; set; }
        public string TelefonoCelular { get; set; }
        public string TelefonoVivienda { get; set; }
        public string TelefonoContacto { get; set; }
        public string TelefonoAlterno { get; set; }
        public int Identificacion { get; set; }
        [StringLength(60)]
        public string Direccion { get; set; }
        public int Pais { get; set; }
        public int Departamento { get; set; }
        public int Ciudad { get; set; }
        public int Rol { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public int VIP { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        public int AtiendeVIP { get; set; }
        public int CodigoAtencion { get; set; }
        public string Taquilla { get; set; }
        public int Interaccion { get; set; }
        public DateTime FechaLogueo { get; set; }

    }
}
