using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using GestionEstudiantes.Repositories;

namespace GestionEstudiantes.UI
{
    public class Menu
    {
        private readonly EstudianteRepository _repository;

        public Menu()
        {
            var context = new EstudianteDbContext();
            _repository = new EstudianteRepository(context);
        }

        public void MostrarMenuPrincipal()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║   SISTEMA DE GESTIÓN DE ESTUDIANTES    ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Agregar estudiante");
                Console.WriteLine("2. Listar todos los estudiantes");
                Console.WriteLine("3. Buscar estudiante por ID");
                Console.WriteLine("4. Buscar estudiante por nombre");
                Console.WriteLine("5. Actualizar estudiante");
                Console.WriteLine("6. Eliminar estudiante");
                Console.WriteLine("7. Ver estadísticas");
                Console.WriteLine("8. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción (1-8): ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarEstudiante();
                        break;
                    case "2":
                        ListarEstudiantes();
                        break;
                    case "3":
                        BuscarPorId();
                        break;
                    case "4":
                        BuscarPorNombre();
                        break;
                    case "5":
                        ActualizarEstudiante();
                        break;
                    case "6":
                        EliminarEstudiante();
                        break;
                    case "7":
                        MostrarEstadisticas();
                        break;
                    case "8":
                        salir = true;
                        Console.WriteLine("\n¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\nOpción inválida. Presione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AgregarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       AGREGAR NUEVO ESTUDIANTE         ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            try
            {
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();

                Console.Write("Edad: ");
                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("\nError: La edad debe ser un número entero.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                var estudiante = new Estudiante
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Edad = edad,
                    Email = email,
                    Telefono = telefono
                };

                if (_repository.AgregarEstudiante(estudiante))
                {
                    Console.WriteLine("\n✓ Estudiante agregado exitosamente.");
                }
                else
                {
                    Console.WriteLine("\n✗ Error al agregar el estudiante.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void ListarEstudiantes()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      LISTADO DE TODOS LOS ESTUDIANTES  ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            var estudiantes = _repository.ObtenerTodos();

            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
            }
            else
            {
                Console.WriteLine($"{"ID",-5} {"Nombre",-15} {"Apellido",-15} {"Edad",-5} {"Email",-20}");
                Console.WriteLine(new string('-', 65));

                foreach (var estudiante in estudiantes)
                {
                    Console.WriteLine($"{estudiante.Id,-5} {estudiante.Nombre,-15} {estudiante.Apellido,-15} {estudiante.Edad,-5} {estudiante.Email,-20}");
                }
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void BuscarPorId()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     BUSCAR ESTUDIANTE POR ID           ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Ingrese el ID del estudiante: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nError: El ID debe ser un número entero.");
                Console.ReadLine();
                return;
            }

            var estudiante = _repository.ObtenerPorId(id);

            if (estudiante != null)
            {
                Console.WriteLine("\n┌─────────────────────────────────────┐");
                Console.WriteLine($"│ ID: {estudiante.Id}");
                Console.WriteLine($"│ Nombre: {estudiante.Nombre}");
                Console.WriteLine($"│ Apellido: {estudiante.Apellido}");
                Console.WriteLine($"│ Edad: {estudiante.Edad}");
                Console.WriteLine($"│ Email: {estudiante.Email}");
                Console.WriteLine($"│ Teléfono: {estudiante.Telefono}");
                Console.WriteLine($"│ Fecha Registro: {estudiante.FechaRegistro:dd/MM/yyyy}");
                Console.WriteLine("└─────────────────────────────────────┘");
            }
            else
            {
                Console.WriteLine($"\n✗ No se encontró estudiante con ID {id}.");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void BuscarPorNombre()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    BUSCAR ESTUDIANTE POR NOMBRE        ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Ingrese nombre o apellido a buscar: ");
            string termino = Console.ReadLine();

            var estudiantes = _repository.BuscarPorNombre(termino);

            if (estudiantes.Count == 0)
            {
                Console.WriteLine($"\n✗ No se encontraron estudiantes con '{termino}'.");
            }
            else
            {
                Console.WriteLine($"\nResultados encontrados: {estudiantes.Count}\n");
                Console.WriteLine($"{"ID",-5} {"Nombre",-15} {"Apellido",-15} {"Edad",-5} {"Email",-20}");
                Console.WriteLine(new string('-', 65));

                foreach (var estudiante in estudiantes)
                {
                    Console.WriteLine($"{estudiante.Id,-5} {estudiante.Nombre,-15} {estudiante.Apellido,-15} {estudiante.Edad,-5} {estudiante.Email,-20}");
                }
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void ActualizarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      ACTUALIZAR DATOS DE ESTUDIANTE    ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Ingrese el ID del estudiante a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nError: El ID debe ser un número entero.");
                Console.ReadLine();
                return;
            }

            var estudiante = _repository.ObtenerPorId(id);

            if (estudiante == null)
            {
                Console.WriteLine($"\n✗ No se encontró estudiante con ID {id}.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nEstudiante actual: {estudiante.Nombre} {estudiante.Apellido}\n");

            Console.Write($"Nuevo nombre [{estudiante.Nombre}]: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
                nombre = estudiante.Nombre;

            Console.Write($"Nuevo apellido [{estudiante.Apellido}]: ");
            string apellido = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(apellido))
                apellido = estudiante.Apellido;

            Console.Write($"Nueva edad [{estudiante.Edad}]: ");
            int edad = estudiante.Edad;
            if (int.TryParse(Console.ReadLine(), out int edadInput))
                edad = edadInput;

            Console.Write($"Nuevo email [{estudiante.Email}]: ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
                email = estudiante.Email;

            Console.Write($"Nuevo teléfono [{estudiante.Telefono}]: ");
            string telefono = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(telefono))
                telefono = estudiante.Telefono;

            var estudianteActualizado = new Estudiante
            {
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Email = email,
                Telefono = telefono
            };

            if (_repository.ActualizarEstudiante(id, estudianteActualizado))
            {
                Console.WriteLine("\n✓ Estudiante actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("\n✗ Error al actualizar el estudiante.");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void EliminarEstudiante()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      ELIMINAR ESTUDIANTE               ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Ingrese el ID del estudiante a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nError: El ID debe ser un número entero.");
                Console.ReadLine();
                return;
            }

            var estudiante = _repository.ObtenerPorId(id);

            if (estudiante == null)
            {
                Console.WriteLine($"\n✗ No se encontró estudiante con ID {id}.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\n¿Desea eliminar a {estudiante.Nombre} {estudiante.Apellido}? (S/N): ");
            string confirmacion = Console.ReadLine().ToUpper();

            if (confirmacion == "S")
            {
                if (_repository.EliminarEstudiante(id))
                {
                    Console.WriteLine("\n✓ Estudiante eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("\n✗ Error al eliminar el estudiante.");
                }
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void MostrarEstadisticas()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         ESTADÍSTICAS DEL SISTEMA       ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            int total = _repository.ObtenerCantidadTotal();
            var estudiantes = _repository.ObtenerTodos();

            Console.WriteLine($"Total de estudiantes registrados: {total}");
            
            if (total > 0)
            {
                double edadPromedio = estudiantes.Average(e => e.Edad);
                int edadMaxima = estudiantes.Max(e => e.Edad);
                int edadMinima = estudiantes.Min(e => e.Edad);

                Console.WriteLine($"Edad promedio: {edadPromedio:F2}");
                Console.WriteLine($"Edad máxima: {edadMaxima}");
                Console.WriteLine($"Edad mínima: {edadMinima}");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }
}
