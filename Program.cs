using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using GestionEstudiantes.UI;

namespace GestionEstudiantes
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            PrepararBaseDatos();

            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }

        private static void PrepararBaseDatos()
        {
            using var context = new EstudianteDbContext();
            context.Database.EnsureCreated();

            CorregirDatosDePrueba(context);

            if (context.Estudiantes.Any())
            {
                return;
            }

            context.Estudiantes.AddRange(
                new Estudiante { Nombre = "Juan", Apellido = "Perez", Edad = 20, Email = "juan.perez@email.com", Telefono = "3001234567", FechaRegistro = DateTime.Now },
                new Estudiante { Nombre = "Maria", Apellido = "Garcia", Edad = 21, Email = "maria.garcia@email.com", Telefono = "3007654321", FechaRegistro = DateTime.Now },
                new Estudiante { Nombre = "Carlos", Apellido = "Lopez", Edad = 22, Email = "carlos.lopez@email.com", Telefono = "3005555555", FechaRegistro = DateTime.Now },
                new Estudiante { Nombre = "Ana", Apellido = "Rodriguez", Edad = 19, Email = "ana.rodriguez@email.com", Telefono = "3009999999", FechaRegistro = DateTime.Now },
                new Estudiante { Nombre = "Pedro", Apellido = "Martinez", Edad = 23, Email = "pedro.martinez@email.com", Telefono = "3002222222", FechaRegistro = DateTime.Now }
            );
            context.SaveChanges();
        }

        private static void CorregirDatosDePrueba(EstudianteDbContext context)
        {
            bool hayCambios = false;

            foreach (var estudiante in context.Estudiantes)
            {
                if (estudiante.Nombre == "Mar\u00C3\u00ADa")
                {
                    estudiante.Nombre = "Maria";
                    hayCambios = true;
                }

                if (estudiante.Apellido == "P\u00C3\u00A9rez")
                {
                    estudiante.Apellido = "Perez";
                    hayCambios = true;
                }

                if (estudiante.Apellido == "Garc\u00C3\u00ADa")
                {
                    estudiante.Apellido = "Garcia";
                    hayCambios = true;
                }

                if (estudiante.Apellido == "L\u00C3\u00B3pez")
                {
                    estudiante.Apellido = "Lopez";
                    hayCambios = true;
                }

                if (estudiante.Apellido == "Rodr\u00C3\u00ADguez")
                {
                    estudiante.Apellido = "Rodriguez";
                    hayCambios = true;
                }

                if (estudiante.Apellido == "Mart\u00C3\u00ADnez")
                {
                    estudiante.Apellido = "Martinez";
                    hayCambios = true;
                }
            }

            if (hayCambios)
            {
                context.SaveChanges();
            }
        }

    }
}
