# Sistema de Gestión de Estudiantes

## Descripción
Aplicación de consola en C# para gestionar estudiantes mediante operaciones CRUD (Crear, Leer, Actualizar, Eliminar) conectada a SQL Server.

## Características Implementadas

### Operaciones CRUD Completas
- ✅ **Agregar estudiante** - Crear nuevos registros con validación de datos
- ✅ **Listar estudiantes** - Ver todos los estudiantes registrados
- ✅ **Buscar por ID** - Consultar estudiante específico
- ✅ **Buscar por nombre** - Búsqueda por nombre o apellido
- ✅ **Actualizar datos** - Modificar información de estudiantes
- ✅ **Eliminar estudiante** - Remover registros con confirmación

### Validaciones
- ✅ Nombre y apellido requeridos (no vacíos)
- ✅ Edad debe ser mayor a 0
- ✅ Validación de entrada de datos
- ✅ Manejo de excepciones

### Funcionalidades Adicionales
- ✅ Interfaz de menú amigable
- ✅ Estadísticas de estudiantes
- ✅ Procedimientos almacenados en SQL Server
- ✅ Índices para optimizar búsquedas

## Estructura del Proyecto

```
GestionEstudiantes/
├── Models/
│   └── Estudiante.cs           # Modelo de datos
├── Data/
│   └── EstudianteDbContext.cs  # Contexto de Entity Framework
├── Repositories/
│   └── EstudianteRepository.cs # Lógica de acceso a datos (CRUD)
├── UI/
│   └── Menu.cs                 # Interfaz de usuario
├── Scripts/
│   └── CrearBaseDatos.sql      # Script SQL para crear BD
├── Program.cs                  # Punto de entrada
└── GestionEstudiantes.csproj   # Proyecto C#
```

## Requisitos Previos

- .NET SDK 10.0 o superior
- SQL Server (Express o superior)
- Visual Studio Code o Visual Studio

## Configuración Inicial

### Paso 1: Crear la Base de Datos

1. Abre SQL Server Management Studio (SSMS)
2. Conectate a tu servidor SQL Server (generalmente `.\SQLEXPRESS`)
3. Abre el script: `Scripts/CrearBaseDatos.sql`
4. Ejecuta el script completo (F5 o Ctrl+E)

El script realizará:
- Crear la base de datos `GestionEstudiantes`
- Crear la tabla `Estudiantes`
- Crear índices para optimizar búsquedas
- Crear procedimientos almacenados
- Insertar datos de prueba

### Paso 2: Configurar la Cadena de Conexión (si es necesario)

Si tu SQL Server tiene una configuración diferente, edita `Data/EstudianteDbContext.cs`:

```csharp
string connectionString = @"Server=TU_SERVIDOR;Database=GestionEstudiantes;Trusted_Connection=true;";
```

Reemplaza `TU_SERVIDOR` con:
- `.\SQLEXPRESS` - SQL Server Express (por defecto)
- `localhost` - SQL Server local
- `(local)` - Otra forma de local

### Paso 3: Compilar y Ejecutar

```bash
# Navega al directorio del proyecto
cd GestionEstudiantes

# Compila el proyecto
dotnet build

# Ejecuta la aplicación
dotnet run
```

## Modelo de Datos

### Tabla: Estudiantes

| Campo | Tipo | Restricciones |
|-------|------|---------------|
| Id | INT | PK, Identity(1,1) |
| Nombre | NVARCHAR(100) | NOT NULL |
| Apellido | NVARCHAR(100) | NOT NULL |
| Edad | INT | NOT NULL, > 0 |
| Email | NVARCHAR(100) | NULL |
| Telefono | NVARCHAR(20) | NULL |
| FechaRegistro | DATETIME | DEFAULT GETDATE() |

## Procedimientos Almacenados

1. **sp_AgregarEstudiante** - Insertar nuevo estudiante con validaciones
2. **sp_ObtenerTodosEstudiantes** - Obtener lista completa
3. **sp_ObtenerEstudiantePorId** - Buscar por ID
4. **sp_BuscarEstudiantePorNombre** - Buscar por nombre/apellido
5. **sp_ActualizarEstudiante** - Actualizar registros
6. **sp_EliminarEstudiante** - Eliminar registros

## Uso de la Aplicación

### Menú Principal

```
╔════════════════════════════════════════╗
║   SISTEMA DE GESTIÓN DE ESTUDIANTES    ║
╚════════════════════════════════════════╝

1. Agregar estudiante
2. Listar todos los estudiantes
3. Buscar estudiante por ID
4. Buscar estudiante por nombre
5. Actualizar estudiante
6. Eliminar estudiante
7. Ver estadísticas
8. Salir
```

### Ejemplos de Uso

#### 1. Agregar Estudiante
- Selecciona opción 1
- Ingresa nombre, apellido, edad, email y teléfono
- El sistema valida y confirma

#### 2. Listar Estudiantes
- Selecciona opción 2
- Verás tabla con todos los estudiantes

#### 3. Buscar por Nombre
- Selecciona opción 4
- Ingresa término de búsqueda
- Muestra resultados coincidentes

#### 4. Ver Estadísticas
- Selecciona opción 7
- Muestra:
  - Total de estudiantes
  - Edad promedio
  - Edad máxima
  - Edad mínima

## Tecnologías Utilizadas

- **Lenguaje**: C# 11+
- **Framework**: .NET 10.0
- **ORM**: Entity Framework Core 9.0
- **Base de Datos**: SQL Server
- **Patrón**: Repository Pattern

## Mejoras Posibles

- [ ] Interfaz gráfica (WPF o Windows Forms)
- [ ] Autenticación de usuarios
- [ ] Exportar datos a Excel/PDF
- [ ] Copias de seguridad automáticas
- [ ] Historial de cambios
- [ ] API REST
- [ ] Aplicación web (ASP.NET Core)

## Troubleshooting

### Error: "No .NET SDKs were found"
```bash
# Descarga e instala .NET SDK desde https://dotnet.microsoft.com/download
```

### Error: "Database connection failed"
- Verifica que SQL Server esté corriendo
- Revisa la cadena de conexión en `EstudianteDbContext.cs`
- Asegúrate de que la base de datos fue creada con el script SQL

### Error: "Connection timeout"
- SQL Server puede estar en otra máquina
- Modifica la cadena de conexión con la dirección correcta

## Autor
Desarrollado como trabajo de curso - Universidad de Cundinamarca
