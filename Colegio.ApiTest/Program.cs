using Newtonsoft.Json;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Http;
using Colegio.Models; // Usamos el namespace correcto para tus modelos

namespace Colegio.ApiTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Esperando 5 segundos para que la API se inicie...");
            Thread.Sleep(5000); // Espera para que el proyecto API esté listo
            Console.WriteLine("Iniciando pruebas de CRUD en Colegio.API (usando atributos ingresados por teclado)...");

            var httpClient = new HttpClient();
            // ⚠️ AJUSTAR ESTA DIRECCIÓN si tu API corre en otro puerto
            httpClient.BaseAddress = new Uri("https://localhost:7001"); // ¡Asegúrate que coincida con tu API!

            // Rutas de tus controladores generados
            string rutaEstudiantes = "api/Estudiantes";
            string rutaCursos = "api/Cursos";

            // Corremos las pruebas de forma secuencial
            TestCRUD_Estudiante(httpClient, rutaEstudiantes);
            TestCRUD_Curso(httpClient, rutaCursos);

            Console.WriteLine("\n*** Todos los tests básicos completados. ***");
            Console.ReadLine();
        }

        // ------------------------------------------------------------------
        // --- MÉTODOS AUXILIARES PARA LECTURA DE CONSOLA ---
        // ------------------------------------------------------------------

        static string ReadStringInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static int ReadIntInput(string prompt)
        {
            int result;
            string input;
            bool success;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                // Intenta convertir el texto a número
                success = int.TryParse(input, out result);
                if (!success)
                {
                    Console.WriteLine("Entrada no válida. Por favor, ingrese un número entero.");
                }
            } while (!success);
            return result;
        }

        // ------------------------------------------------------------------
        // --- MÉTODO DE PRUEBA CRUD PARA ESTUDIANTE ---
        // ------------------------------------------------------------------
        static void TestCRUD_Estudiante(HttpClient httpClient, string ruta)
        {
            Console.WriteLine("\n=== TEST CRUD: ESTUDIANTE ===");
            int estudianteId = 0;

            try
            {
                // 1. CREAR (POST) - Usando datos ingresados por el usuario
                Console.WriteLine("\n--- Creación de Estudiante ---");
                string nombreEstudiante = ReadStringInput("Ingrese Nombre Completo del Estudiante: ");
                int edadEstudiante = ReadIntInput("Ingrese Edad del Estudiante (15-99): ");

                var nuevoEstudiante = new Estudiante
                {
                    NombreCompleto = nombreEstudiante,
                    Edad = edadEstudiante,
                    FechaIngreso = DateTime.UtcNow.AddDays(-30) // Valor estático para la prueba
                };

                var jsonPayload = JsonConvert.SerializeObject(nuevoEstudiante);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(ruta, content).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var estudianteCreado = JsonConvert.DeserializeObject<Estudiante>(json);
                    estudianteId = estudianteCreado.Id;
                    Console.WriteLine($"1. ✅ Creado: Estudiante '{estudianteCreado.NombreCompleto}' (ID: {estudianteId}).");
                }
                else
                {
                    Console.WriteLine($"1. ❌ ERROR al crear Estudiante. Código: {response.StatusCode}. Respuesta: {json}");
                    return;
                }

                // 2. LEER (GET por ID)
                response = httpClient.GetAsync($"{ruta}/{estudianteId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("2. ✅ Leído: Estudiante encontrado con éxito.");
                }
                else
                {
                    Console.WriteLine($"2. ❌ ERROR al leer Estudiante. Código: {response.StatusCode}.");
                }

                // 3. ACTUALIZAR (PUT)
                Console.WriteLine("\n--- Actualización de Estudiante ---");
                string nuevoNombre = ReadStringInput("Ingrese NUEVO Nombre Completo para actualizar: ");
                int nuevaEdad = ReadIntInput("Ingrese NUEVA Edad para actualizar: ");

                var estudianteActualizado = new Estudiante
                {
                    Id = estudianteId,
                    NombreCompleto = nuevoNombre,
                    Edad = nuevaEdad,
                    FechaIngreso = nuevoEstudiante.FechaIngreso
                };
                jsonPayload = JsonConvert.SerializeObject(estudianteActualizado);
                content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                response = httpClient.PutAsync($"{ruta}/{estudianteId}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("3. ✅ Actualizado: Nombre modificado correctamente.");
                }
                else
                {
                    Console.WriteLine($"3. ❌ ERROR al actualizar. Código: {response.StatusCode}.");
                }

                // 4. ELIMINAR (DELETE)
                response = httpClient.DeleteAsync($"{ruta}/{estudianteId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("4. ✅ Eliminado: Registro borrado con éxito.");
                }
                else
                {
                    Console.WriteLine($"4. ❌ ERROR al eliminar. Código: {response.StatusCode}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR EXCEPCIÓN EN TEST DE ESTUDIANTE: {ex.Message}");
            }
        }

        // ------------------------------------------------------------------
        // --- MÉTODO DE PRUEBA CRUD PARA CURSO ---
        // ------------------------------------------------------------------
        static void TestCRUD_Curso(HttpClient httpClient, string ruta)
        {
            Console.WriteLine("\n=== TEST CRUD: CURSO ===");
            int cursoId = 0;

            try
            {
                // 1. CREAR (POST) - Usando datos ingresados por el usuario
                Console.WriteLine("\n--- Creación de Curso ---");
                string nombreCurso = ReadStringInput("Ingrese Nombre del Curso: ");
                string codigoCurso = ReadStringInput("Ingrese Código del Curso (ej: MAT101): ");
                int creditosCurso = ReadIntInput("Ingrese Créditos del Curso (1-10): ");

                var nuevoCurso = new Curso
                {
                    Nombre = nombreCurso,
                    Codigo = codigoCurso,
                    Creditos = creditosCurso
                };

                var jsonPayload = JsonConvert.SerializeObject(nuevoCurso);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(ruta, content).Result;
                var json = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var cursoCreado = JsonConvert.DeserializeObject<Curso>(json);
                    cursoId = cursoCreado.Id;
                    Console.WriteLine($"1. ✅ Creado: Curso '{cursoCreado.Nombre}' (ID: {cursoId}).");
                }
                else
                {
                    Console.WriteLine($"1. ❌ ERROR al crear Curso. Código: {response.StatusCode}. Respuesta: {json}");
                    return;
                }

                // 2. LEER (GET por ID)
                response = httpClient.GetAsync($"{ruta}/{cursoId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("2. ✅ Leído: Curso encontrado con éxito.");
                }
                else
                {
                    Console.WriteLine($"2. ❌ ERROR al leer Curso. Código: {response.StatusCode}.");
                }

                // 3. ELIMINAR (DELETE)
                response = httpClient.DeleteAsync($"{ruta}/{cursoId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("3. ✅ Eliminado: Registro borrado con éxito.");
                }
                else
                {
                    Console.WriteLine($"3. ❌ ERROR al eliminar. Código: {response.StatusCode}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR EXCEPCIÓN EN TEST DE CURSO: {ex.Message}");
            }
        }
    }
}