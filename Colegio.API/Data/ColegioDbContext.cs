using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Colegio.Models;

    public class ColegioDbContext : DbContext
    {
        public ColegioDbContext (DbContextOptions<ColegioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Colegio.Models.Estudiante> Estudiante { get; set; } = default!;

public DbSet<Colegio.Models.Inscripcion> Inscripcion { get; set; } = default!;

public DbSet<Colegio.Models.Curso> Curso { get; set; } = default!;
    public object Estudiantes { get; set; }
}
