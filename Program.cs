using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using GestionEstudiantes.UI;

// Crear base de datos y cargar datos de prueba
using (var context = new EstudianteDbContext())
{
    context.Database.EnsureCreated();
    
    // Agregar datos de prueba si la BD está vacía
    if (!context.Estudiantes.Any())
    {
        context.Estudiantes.AddRange(
            new Estudiante { Nombre = "Juan", Apellido = "Pérez", Edad = 20, Email = "juan.perez@email.com", Telefono = "3001234567", FechaRegistro = DateTime.Now },
            new Estudiante { Nombre = "María", Apellido = "García", Edad = 21, Email = "maria.garcia@email.com", Telefono = "3007654321", FechaRegistro = DateTime.Now },
            new Estudiante { Nombre = "Carlos", Apellido = "López", Edad = 22, Email = "carlos.lopez@email.com", Telefono = "3005555555", FechaRegistro = DateTime.Now },
            new Estudiante { Nombre = "Ana", Apellido = "Rodríguez", Edad = 19, Email = "ana.rodriguez@email.com", Telefono = "3009999999", FechaRegistro = DateTime.Now },
            new Estudiante { Nombre = "Pedro", Apellido = "Martínez", Edad = 23, Email = "pedro.martinez@email.com", Telefono = "3002222222", FechaRegistro = DateTime.Now }
        );
        context.SaveChanges();
    }
}

var menu = new Menu();
menu.MostrarMenuPrincipal();
