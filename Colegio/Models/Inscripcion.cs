using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Usado para [Key] o [ForeignKey] si fuera necesario, aunque no estrictamente requerido aquí.

namespace Colegio.Models
{
    /// <summary>
    /// Representa un registro de inscripción, la tabla de unión entre Estudiante y Curso.
    /// </summary>
    public class Inscripcion
    {
        // Clave Primaria (PK)
        // Usamos una clave simple 'Id' para facilitar la implementación CRUD.
        public int Id { get; set; }

        // --- Claves Foráneas (Foreign Keys) ---

        [Display(Name = "Estudiante ID")]
        public int EstudianteId { get; set; } // FK para el Estudiante

        [Display(Name = "Curso ID")]
        public int CursoId { get; set; } // FK para el Curso


        // --- Propiedades de Navegación ---

        /// <summary>
        /// Enlace al Estudiante inscrito (Relación 1:M con la tabla Estudiante).
        /// </summary>
        public Estudiante Estudiante { get; set; }

        /// <summary>
        /// Enlace al Curso en el que está inscrito (Relación 1:M con la tabla Curso).
        /// </summary>
        public Curso Curso { get; set; }


        // --- Datos Adicionales de la Inscripción ---

        /// <summary>
        /// Nota obtenida en el curso. Es opcional (nullable: int?).
        /// </summary>
        [Display(Name = "Nota Final")]
        [Range(0, 100, ErrorMessage = "La nota debe estar entre 0 y 100.")]
        public int? Nota { get; set; }

        /// <summary>
        /// Fecha en que se realizó la inscripción. Se inicializa a la fecha actual.
        /// </summary>
        [Display(Name = "Fecha de Inscripción")]
        [DataType(DataType.Date)]
        public DateTime FechaInscripcion { get; set; } = DateTime.Now;
    }
}