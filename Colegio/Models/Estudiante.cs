using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colegio.Models; // Asegúrate de que la clase Inscripcion esté definida en este espacio de nombres o ajusta el using según corresponda.

namespace Colegio.Models
{
    public class Estudiante
    {
        private ICollection<Inscripcion> inscripciones = new List<Inscripcion>();

        // Clave Primaria (PK)
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [Range(15, 99, ErrorMessage = "La edad debe estar entre 15 y 99 años.")]
        public int Edad { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        // Propiedad de navegación para la relación Muchos a Muchos:
        // Contiene todas las inscripciones (registros) de este estudiante.
        public ICollection<Inscripcion> Inscripciones { get => inscripciones; set => inscripciones = value; }
    }
}