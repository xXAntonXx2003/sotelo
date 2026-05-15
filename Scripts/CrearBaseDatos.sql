-- Script para crear la base de datos GestionEstudiantes

-- 1. Crear la base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GestionEstudiantes')
BEGIN
    CREATE DATABASE GestionEstudiantes;
END
GO

USE GestionEstudiantes;
GO

-- 2. Crear la tabla Estudiantes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Estudiantes')
BEGIN
    CREATE TABLE Estudiantes
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        Edad INT NOT NULL,
        Email NVARCHAR(100),
        Telefono NVARCHAR(20),
        FechaRegistro DATETIME DEFAULT GETDATE()
    );
END
GO

-- 3. Crear índices
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Estudiantes_Nombre')
BEGIN
    CREATE INDEX IX_Estudiantes_Nombre ON Estudiantes(Nombre);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Estudiantes_Apellido')
BEGIN
    CREATE INDEX IX_Estudiantes_Apellido ON Estudiantes(Apellido);
END
GO

-- 4. Procedimiento almacenado para insertar estudiante
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_AgregarEstudiante' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_AgregarEstudiante;
END
GO

CREATE PROCEDURE sp_AgregarEstudiante
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Edad INT,
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20)
AS
BEGIN
    IF @Nombre IS NULL OR @Nombre = '' OR @Apellido IS NULL OR @Apellido = ''
    BEGIN
        RAISERROR ('Nombre y Apellido son requeridos', 16, 1);
        RETURN;
    END

    IF @Edad <= 0
    BEGIN
        RAISERROR ('La edad debe ser mayor a 0', 16, 1);
        RETURN;
    END

    INSERT INTO Estudiantes (Nombre, Apellido, Edad, Email, Telefono, FechaRegistro)
    VALUES (@Nombre, @Apellido, @Edad, @Email, @Telefono, GETDATE());
END
GO

-- 5. Procedimiento almacenado para obtener todos los estudiantes
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_ObtenerTodosEstudiantes' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_ObtenerTodosEstudiantes;
END
GO

CREATE PROCEDURE sp_ObtenerTodosEstudiantes
AS
BEGIN
    SELECT Id, Nombre, Apellido, Edad, Email, Telefono, FechaRegistro
    FROM Estudiantes
    ORDER BY Id;
END
GO

-- 6. Procedimiento almacenado para obtener estudiante por ID
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_ObtenerEstudiantePorId' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_ObtenerEstudiantePorId;
END
GO

CREATE PROCEDURE sp_ObtenerEstudiantePorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, Edad, Email, Telefono, FechaRegistro
    FROM Estudiantes
    WHERE Id = @Id;
END
GO

-- 7. Procedimiento almacenado para buscar por nombre
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_BuscarEstudiantePorNombre' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_BuscarEstudiantePorNombre;
END
GO

CREATE PROCEDURE sp_BuscarEstudiantePorNombre
    @Termino NVARCHAR(100)
AS
BEGIN
    SELECT Id, Nombre, Apellido, Edad, Email, Telefono, FechaRegistro
    FROM Estudiantes
    WHERE Nombre LIKE '%' + @Termino + '%' OR Apellido LIKE '%' + @Termino + '%'
    ORDER BY Nombre;
END
GO

-- 8. Procedimiento almacenado para actualizar estudiante
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_ActualizarEstudiante' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_ActualizarEstudiante;
END
GO

CREATE PROCEDURE sp_ActualizarEstudiante
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Edad INT,
    @Email NVARCHAR(100),
    @Telefono NVARCHAR(20)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Estudiantes WHERE Id = @Id)
    BEGIN
        RAISERROR ('Estudiante no encontrado', 16, 1);
        RETURN;
    END

    UPDATE Estudiantes
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Edad = @Edad,
        Email = @Email,
        Telefono = @Telefono
    WHERE Id = @Id;
END
GO

-- 9. Procedimiento almacenado para eliminar estudiante
IF EXISTS (SELECT * FROM sys.objects WHERE name = 'sp_EliminarEstudiante' AND type = 'P')
BEGIN
    DROP PROCEDURE sp_EliminarEstudiante;
END
GO

CREATE PROCEDURE sp_EliminarEstudiante
    @Id INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Estudiantes WHERE Id = @Id)
    BEGIN
        RAISERROR ('Estudiante no encontrado', 16, 1);
        RETURN;
    END

    DELETE FROM Estudiantes
    WHERE Id = @Id;
END
GO

-- 10. Datos de prueba
INSERT INTO Estudiantes (Nombre, Apellido, Edad, Email, Telefono, FechaRegistro)
VALUES
    ('Juan', 'Pérez', 20, 'juan.perez@email.com', '3001234567', GETDATE()),
    ('María', 'García', 21, 'maria.garcia@email.com', '3007654321', GETDATE()),
    ('Carlos', 'López', 22, 'carlos.lopez@email.com', '3005555555', GETDATE()),
    ('Ana', 'Rodríguez', 19, 'ana.rodriguez@email.com', '3009999999', GETDATE()),
    ('Pedro', 'Martínez', 23, 'pedro.martinez@email.com', '3002222222', GETDATE());
GO

-- Verificar que los datos fueron insertados
SELECT * FROM Estudiantes;
GO
