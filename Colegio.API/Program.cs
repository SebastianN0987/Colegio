using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Colegio.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración de la conexión a la base de datos
            builder.Services.AddDbContext<ColegioDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ColegioDbContext") 
                ?? throw new InvalidOperationException("Connection string 'ColegioDbContext' not found.")));

            // Añadir servicios al contenedor
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuración para que la aplicación escuche en todas las direcciones (0.0.0.0) y el puerto 80
            app.Urls.Add("http://0.0.0.0:80");

            // Configuración de middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
