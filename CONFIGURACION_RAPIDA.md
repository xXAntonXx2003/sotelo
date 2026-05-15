# GUÍA RÁPIDA - CONFIGURACIÓN Y EJECUCIÓN

## Paso 1: Crear la Base de Datos en SQL Server

### Opción A: Usando SQL Server Management Studio (SSMS)

1. Abre **SQL Server Management Studio**
2. Conectate a tu servidor (usualmente `.\SQLEXPRESS`)
3. Haz clic derecho en "Databases" → "New Query"
4. Copia TODO el contenido del archivo: `Scripts/CrearBaseDatos.sql`
5. Pega en la ventana de consulta
6. Presiona **F5** o haz clic en "Execute"
7. Verás el mensaje "Comandos completados correctamente"

### Opción B: Usando línea de comandos

```bash
# Windows
sqlcmd -S .\SQLEXPRESS -i "Scripts\CrearBaseDatos.sql"

# Linux/Mac
sqlcmd -S localhost -i Scripts/CrearBaseDatos.sql
```

## Paso 2: Verificar la Conexión (opcional)

En SSMS, ejecuta:
```sql
USE GestionEstudiantes;
SELECT * FROM Estudiantes;
```

Deberías ver 5 estudiantes de prueba.

## Paso 3: Compilar y Ejecutar la Aplicación

```bash
# Abre una terminal en la carpeta del proyecto
cd c:\Users\xXAnt\Downloads\GestionEstudiantes

# Compila el proyecto
dotnet build

# Ejecuta la aplicación
dotnet run
```

## Paso 4: Usar la Aplicación

1. Verás el menú principal
2. Selecciona una opción (1-8)
3. Sigue las instrucciones
4. Opción 8 para salir

## ¿PROBLEMAS COMUNES?

### Error: "Cannot open database 'GestionEstudiantes'"
**Solución**: Ejecuta el script SQL nuevamente desde SSMS

### Error: "Connection failed"
**Solución**: Cambia la cadena de conexión en:
- Archivo: `Data/EstudianteDbContext.cs`
- Línea: `string connectionString = @"..."`

Ejemplos de servidores:
- `.\SQLEXPRESS` ← Por defecto en Windows
- `localhost` ← SQL Server local
- `(local)` ← Otra forma
- `SERVIDOR_NOMBRE` ← Servidor en red

### Error de compilación con advertencias
No es un problema. Las advertencias de nullability son normales.

### La aplicación no inicia
1. Verifica que SQL Server esté corriendo
2. Asegúrate de tener .NET SDK instalado: `dotnet --version`
3. Intenta `dotnet restore` y luego `dotnet build`

## PRÓXIMOS PASOS

1. ✅ Base de datos creada
2. ✅ Proyecto compilado
3. ✅ Aplicación lista para usar
4. Prueba cada opción del menú
5. Ingresa datos de prueba

## DATOS DE PRUEBA YA CARGADOS

La aplicación viene con 5 estudiantes de ejemplo:
- Juan Pérez
- María García
- Carlos López
- Ana Rodríguez
- Pedro Martínez

Puedes:
- Listar para verlos (opción 2)
- Buscar por nombre (opción 4)
- Actualizar sus datos (opción 5)
- Agregar más estudiantes (opción 1)
- Eliminar si deseas (opción 6)

¡La aplicación está lista para usar!
