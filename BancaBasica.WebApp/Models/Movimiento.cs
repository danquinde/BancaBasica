using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BancaBasica.WebApp.Models
{
    public partial class Movimiento
    {
        [Display(Name = "ID Movim")]
        public int Id { get; set; }
        public int CuentaId { get; set; }

        [Display(Name = "Tipo de Movim")]
        [Required(ErrorMessage = "El Tipo de cuenta es obligatoria")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El Valor es obligatorio")]
        public decimal Valor { get; set; }
        public bool Eliminado { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }
}
