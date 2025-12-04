using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colegio.Models; // Asegúrate de que la clase Inscripcion esté definida en este espacio de nombres o ajusta el using según corresponda.

namespace Colegio.Models
{
    public class Curso
    {
        // Clave Primaria (PK)
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100)]
        [Display(Name = "Nombre del Curso")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Créditos")]
        [Range(1, 10, ErrorMessage = "Los créditos deben ser entre 1 y 10.")]
        public int Creditos { get; set; }

        // Propiedad de navegación para la relación Muchos a Muchos:
        // Contiene todas las inscripciones (registros) en este curso.
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}