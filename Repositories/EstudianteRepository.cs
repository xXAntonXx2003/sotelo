using GestionEstudiantes.Data;
using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

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

        // READ: Buscar estudiante con filtro
        public List<Estudiante> Buscar(string termino, string filtro)
        {
            try
            {
                string criterio = NormalizarTexto(termino);
                string filtroNormalizado = NormalizarTexto(filtro);

                if (string.IsNullOrWhiteSpace(criterio))
                {
                    return ObtenerTodos();
                }

                return _context.Estudiantes
                    .AsNoTracking()
                    .AsEnumerable()
                    .Where(e => CoincideConFiltro(e, criterio, filtroNormalizado))
                    .OrderBy(e => e.Nombre)
                    .ThenBy(e => e.Apellido)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar estudiantes: {ex.Message}");
                return new List<Estudiante>();
            }
        }

        // READ: Buscar estudiante por nombre
        public List<Estudiante> BuscarPorNombre(string nombre)
        {
            return Buscar(nombre, "Nombre");
        }

        private static bool CoincideConFiltro(Estudiante estudiante, string criterio, string filtro)
        {
            return filtro switch
            {
                "id" => estudiante.Id.ToString().Contains(criterio),
                "nombre" => Contiene(estudiante.Nombre, criterio) || Contiene($"{estudiante.Nombre} {estudiante.Apellido}", criterio),
                "apellido" => Contiene(estudiante.Apellido, criterio),
                "email" => Contiene(estudiante.Email, criterio),
                "telefono" => Contiene(estudiante.Telefono, criterio),
                "edad" => estudiante.Edad.ToString().Contains(criterio),
                "todos" => estudiante.Id.ToString().Contains(criterio) ||
                           estudiante.Edad.ToString().Contains(criterio) ||
                           Contiene(estudiante.Nombre, criterio) ||
                           Contiene(estudiante.Apellido, criterio) ||
                           Contiene($"{estudiante.Nombre} {estudiante.Apellido}", criterio) ||
                           Contiene(estudiante.Email, criterio) ||
                           Contiene(estudiante.Telefono, criterio),
                _ => Contiene(estudiante.Nombre, criterio) || Contiene($"{estudiante.Nombre} {estudiante.Apellido}", criterio)
            };
        }

        private static bool Contiene(string texto, string criterio)
        {
            return NormalizarTexto(texto).Contains(criterio);
        }

        private static string NormalizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            string textoNormalizado = RepararTextoCodificado(texto.Trim().ToLowerInvariant())
                .Normalize(NormalizationForm.FormD);
            var resultado = new StringBuilder(textoNormalizado.Length);

            foreach (char caracter in textoNormalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                {
                    resultado.Append(caracter);
                }
            }

            return resultado.ToString().Normalize(NormalizationForm.FormC);
        }

        private static string RepararTextoCodificado(string texto)
        {
            return texto
                .Replace("\u00e3\u00a1", "a")
                .Replace("\u00e3\u00a9", "e")
                .Replace("\u00e3\u00ad", "i")
                .Replace("\u00e3\u00b3", "o")
                .Replace("\u00e3\u00ba", "u")
                .Replace("\u00e3\u00b1", "n");
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
