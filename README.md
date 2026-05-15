# Sistema de Gestión de Estudiantes

## Descripción
Aplicación de Windows Forms en C# para gestionar estudiantes mediante operaciones CRUD (Crear, Leer, Actualizar, Eliminar) con base de datos SQLite.

## Interfaz Gráfica

La aplicación cuenta con una interfaz gráfica moderna y amigable que incluye:

- **Panel principal** con tabla de datos interactiva
- **Barra lateral** con botones de acción
- **Búsqueda en tiempo real** por nombre o apellido
- **Panel de estadísticas** con información resumida
- **Formularios modales** para agregar/editar estudiantes

### Capturas de Pantalla

```
┌─────────────────────────────────────────────────────────────────┐
│         SISTEMA DE GESTIÓN DE ESTUDIANTES                       │
├──────────────┬──────────────────────────────────────────────────┤
│              │  Buscar: [_________________] [Buscar]            │
│ [+ Agregar]  ├──────────────────────────────────────────────────┤
│              │  ID │ Nombre   │ Apellido  │ Edad │ Email        │
│ [✎ Editar]   │ ───┼──────────┼───────────┼──────┼────────────── │
│              │  1 │ Juan     │ Pérez     │  20  │ juan@...      │
│ [✗ Eliminar] │  2 │ María    │ García    │  21  │ maria@...     │
│              │  3 │ Carlos   │ López     │  22  │ carlos@...    │
│ [↻ Refrescar]│  4 │ Ana      │ Rodríguez │  19  │ ana@...       │
│              │                                                   │
│ ┌──────────┐ │                                                   │
│ │Estadíst. │ │                                                   │
│ │Total: 5  │ │                                                   │
│ │Prom: 21  │ │                                                   │
│ └──────────┘ │                                                   │
├──────────────┴──────────────────────────────────────────────────┤
│  Total: 5 estudiante(s) registrado(s)                           │
└─────────────────────────────────────────────────────────────────┘
```

## Características Implementadas

### Operaciones CRUD Completas
- **Agregar estudiante** - Formulario con validación de datos
- **Listar estudiantes** - Tabla interactiva con ordenamiento
- **Buscar estudiantes** - Búsqueda por nombre o apellido
- **Actualizar datos** - Doble clic en fila o botón editar
- **Eliminar estudiante** - Con confirmación de seguridad

### Validaciones
- Nombre y apellido requeridos
- Edad entre 1 y 120 años
- Validación de formato de email
- Manejo de excepciones con mensajes amigables

### Funcionalidades de la Interfaz
- Diseño moderno con colores profesionales
- Tabla con filas alternadas para mejor lectura
- Estadísticas en tiempo real
- Búsqueda con Enter o botón
- Doble clic para editar estudiante

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
│   ├── Menu.cs                 # Interfaz de consola (legacy)
│   ├── FormPrincipal.cs        # Ventana principal
│   └── FormEstudiante.cs       # Formulario agregar/editar
├── Resources/                  # Recursos (iconos, imágenes)
├── Scripts/
│   └── CrearBaseDatos.sql      # Script SQL (referencia)
├── Program.cs                  # Punto de entrada
└── GestionEstudiantes.csproj   # Proyecto C#
```

## Requisitos Previos

- .NET SDK 8.0 o superior
- Windows 10/11 (para Windows Forms)
- Visual Studio Code o Visual Studio

## Instalación y Ejecución

### Paso 1: Clonar o Descargar el Proyecto

```bash
git clone <url-del-repositorio>
cd GestionEstudiantes
```

### Paso 2: Restaurar Dependencias

```bash
dotnet restore
```

### Paso 3: Compilar y Ejecutar

```bash
# Compila el proyecto
dotnet build

# Ejecuta la aplicación
dotnet run
```

La base de datos SQLite se crea automáticamente en:
- `%APPDATA%/GestionEstudiantes/estudiantes.db`

## Modelo de Datos

### Tabla: Estudiantes

| Campo | Tipo | Restricciones |
|-------|------|---------------|
| Id | INT | PK, Autoincrement |
| Nombre | NVARCHAR(100) | NOT NULL |
| Apellido | NVARCHAR(100) | NOT NULL |
| Edad | INT | NOT NULL, > 0 |
| Email | NVARCHAR(100) | NULL |
| Telefono | NVARCHAR(20) | NULL |
| FechaRegistro | DATETIME | DEFAULT datetime('now') |

## Uso de la Aplicación

### Agregar Estudiante
1. Clic en el botón verde "Agregar Estudiante"
2. Completa el formulario con los datos
3. Clic en "Guardar"

### Editar Estudiante
1. Selecciona un estudiante en la tabla
2. Clic en "Editar Estudiante" o doble clic en la fila
3. Modifica los datos
4. Clic en "Guardar"

### Eliminar Estudiante
1. Selecciona un estudiante en la tabla
2. Clic en "Eliminar Estudiante"
3. Confirma la eliminación

### Buscar Estudiante
1. Escribe el nombre o apellido en el campo de búsqueda
2. Presiona Enter o clic en "Buscar"
3. Para ver todos, limpia el campo y presiona "Refrescar Lista"

## Tecnologías Utilizadas

- **Lenguaje**: C# 12
- **Framework**: .NET 8.0
- **UI**: Windows Forms
- **ORM**: Entity Framework Core 8.0
- **Base de Datos**: SQLite
- **Patrón**: Repository Pattern

## Paleta de Colores

- **Azul principal**: #3B82F6 (encabezados, botones primarios)
- **Verde éxito**: #22C55E (botón agregar)
- **Rojo error**: #EF4444 (botón eliminar)
- **Gris neutro**: #6B7280 (botón refrescar)
- **Fondo claro**: #F5F7FA (fondo principal)
- **Blanco**: #FFFFFF (paneles y tarjetas)

## Mejoras Futuras

- [ ] Exportar datos a Excel/CSV
- [ ] Tema oscuro
- [ ] Gráficos de estadísticas
- [ ] Historial de cambios
- [ ] Backup automático
- [ ] Múltiples usuarios

## Autor
Desarrollado como trabajo de curso - Universidad de Cundinamarca
