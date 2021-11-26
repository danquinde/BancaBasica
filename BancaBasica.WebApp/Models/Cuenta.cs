using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BancaBasica.WebApp.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El número es obligatorio")]
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        public bool Eliminada { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
