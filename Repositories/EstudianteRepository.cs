using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.Repositories
{
    public class EstudianteRepository
    {
        private readonly EstudianteDbContext _context;

        public EstudianteRepository(EstudianteDbContext context)
        {
            _context = context;
        }

        // CREATE: Agregar un estudiante
        public bool AgregarEstudiante(Estudiante estudiante)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(estudiante.Nombre) || 
                    string.IsNullOrWhiteSpace(estudiante.Apellido))
                {
                    throw new ArgumentException("Nombre y Apellido son requeridos.");
                }

                if (estudiante.Edad <= 0)
                {
                    throw new ArgumentException("La edad debe ser mayor a 0.");
                }

                estudiante.FechaRegistro = DateTime.Now;
                _context.Estudiantes.Add(estudiante);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar estudiante: {ex.Message}");
                return false;
            }
        }

        // READ: Listar todos los estudiantes
        public List<Estudiante> ObtenerTodos()
        {
            try
            {
                return _context.Estudiantes.OrderBy(e => e.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener estudiantes: {ex.Message}");
                return new List<Estudiante>();
            }
        }

        // READ: Buscar estudiante por ID
        public Estudiante? ObtenerPorId(int id)
        {
            try
            {
                return _context.Estudiantes.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener estudiante: {ex.Message}");
                return null;
            }
        }

        // READ: Buscar estudiante por nombre
        public List<Estudiante> BuscarPorNombre(string nombre)
        {
            try
            {
                return _context.Estudiantes
                    .Where(e => e.Nombre.Contains(nombre) || e.Apellido.Contains(nombre))
                    .OrderBy(e => e.Nombre)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar estudiantes: {ex.Message}");
                return new List<Estudiante>();
            }
        }

        // UPDATE: Actualizar estudiante
        public bool ActualizarEstudiante(int id, Estudiante estudianteActualizado)
        {
            try
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Id == id);
                
                if (estudiante == null)
                {
                    throw new ArgumentException($"No se encontró estudiante con ID {id}.");
                }

                if (string.IsNullOrWhiteSpace(estudianteActualizado.Nombre) || 
                    string.IsNullOrWhiteSpace(estudianteActualizado.Apellido))
                {
                    throw new ArgumentException("Nombre y Apellido son requeridos.");
                }

                if (estudianteActualizado.Edad <= 0)
                {
                    throw new ArgumentException("La edad debe ser mayor a 0.");
                }

                estudiante.Nombre = estudianteActualizado.Nombre;
                estudiante.Apellido = estudianteActualizado.Apellido;
                estudiante.Edad = estudianteActualizado.Edad;
                estudiante.Email = estudianteActualizado.Email;
                estudiante.Telefono = estudianteActualizado.Telefono;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar estudiante: {ex.Message}");
                return false;
            }
        }

        // DELETE: Eliminar estudiante
        public bool EliminarEstudiante(int id)
        {
            try
            {
                var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Id == id);
                
                if (estudiante == null)
                {
                    throw new ArgumentException($"No se encontró estudiante con ID {id}.");
                }

                _context.Estudiantes.Remove(estudiante);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar estudiante: {ex.Message}");
                return false;
            }
        }

        // Obtener cantidad total de estudiantes
        public int ObtenerCantidadTotal()
        {
            try
            {
                return _context.Estudiantes.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cantidad: {ex.Message}");
                return 0;
            }
        }
    }
}
