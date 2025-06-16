-- Crear base de datos si no existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Student_Liliana')
BEGIN
    CREATE DATABASE Student_Liliana;
END
GO

USE Student_Liliana;
GO
-- Crear tabla Estudiantes
CREATE TABLE Estudiantes (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Documento NVARCHAR(20) NOT NULL,
    PRIMARY KEY (Id)
);
GO

-- Crear tabla Materias
CREATE TABLE Materias (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL DEFAULT 3,
    PRIMARY KEY (Id)
);
GO


-- Crear tabla EstudianteMateria
CREATE TABLE EstudianteMateria (
    Id INT IDENTITY(1,1) NOT NULL,
    EstudianteId INT NOT NULL,
    MateriaId INT NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_Estudiante FOREIGN KEY (EstudianteId) REFERENCES Estudiantes(Id),
    CONSTRAINT fk_Materia FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);
GO



-- Crear tabla Profesores
CREATE TABLE Profesores (
    Id INT IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    PRIMARY KEY (Id)
);
GO

-- Crear tabla ProfesorMateria
CREATE TABLE ProfesorMateria (
    Id INT IDENTITY(1,1) NOT NULL,
    ProfesorId INT NOT NULL,
    MateriaId INT NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_Profesor FOREIGN KEY (ProfesorId) REFERENCES Profesores(Id),
    CONSTRAINT fk_MateriaProfesor FOREIGN KEY (MateriaId) REFERENCES Materias(Id)
);
GO

-- Insertar datos en Materias
INSERT INTO Materias (Nombre, Creditos) 
VALUES ('Matemáticas', 3), 
       ('Física', 3), 
       ('Química', 3), 
       ('Biología', 3), 
       ('Historia', 3), 
       ('Literatura', 3), 
       ('Filosofía', 3), 
       ('Geografía', 3), 
       ('Informática', 3), 
       ('Inglés', 3);
GO

-- Insertar datos en Profesores
INSERT INTO Profesores (Nombre) 
VALUES ('Prof. Juan Pérez'),
       ('Prof. Ana Gómez'),
       ('Prof. Luis Ramírez'),
       ('Prof. Marta Torres'),
       ('Prof. Carlos Díaz');
GO

-- Insertar datos en ProfesorMateria
INSERT INTO ProfesorMateria (ProfesorId, MateriaId) 
VALUES (1, 1), (1, 2), (2, 3), (2, 4), (3, 5), (3, 6), 
       (4, 7), (4, 8), (5, 9), (5, 10);
GO

-- Crear procedimientos almacenados

-- Procedimiento para agregar materia a estudiante
CREATE PROCEDURE sp_AddMateriaToEstudiante
    @EstudianteId INT,
    @MateriaId INT
AS
BEGIN
    INSERT INTO EstudianteMateria (EstudianteId, MateriaId)
    VALUES (@EstudianteId, @MateriaId);
END;
GO

-- Procedimiento para crear estudiante
CREATE PROCEDURE sp_CreateEstudiante
    @Nombre NVARCHAR(100),
    @Documento NVARCHAR(20)
AS
BEGIN
    INSERT INTO Estudiantes (Nombre, Documento)
    VALUES (@Nombre, @Documento);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO

-- Procedimiento para eliminar estudiante
CREATE PROCEDURE sp_DeleteEstudiante
    @Id INT
AS
BEGIN
    DELETE FROM EstudianteMateria WHERE EstudianteId = @Id;
    DELETE FROM Estudiantes WHERE Id = @Id;
END;
GO

-- Procedimiento para obtener todos los estudiantes
CREATE PROCEDURE sp_GetAllEstudiantes
AS
BEGIN
    SELECT Id, Nombre, Documento FROM Estudiantes;
END;
GO

-- Procedimiento para obtener todas las materias
CREATE PROCEDURE sp_GetAllMaterias
AS
BEGIN
    SELECT m.Id, m.Nombre, m.Creditos, p.Id AS ProfesorId, p.Nombre AS ProfesorNombre
    FROM Materias m
    INNER JOIN ProfesorMateria pm ON m.Id = pm.MateriaId
    INNER JOIN Profesores p ON p.Id = pm.ProfesorId;
END;
GO

-- Procedimiento para obtener compañeros por materia
CREATE PROCEDURE sp_GetCompanerosPorMateria
    @EstudianteId INT
AS
BEGIN
    SELECT m.Nombre AS MateriaNombre, e2.Nombre AS CompaneroNombre
    FROM EstudianteMateria em1
    INNER JOIN Materias m ON em1.MateriaId = m.Id
    INNER JOIN EstudianteMateria em2 ON em1.MateriaId = em2.MateriaId AND em2.EstudianteId != @EstudianteId
    INNER JOIN Estudiantes e2 ON em2.EstudianteId = e2.Id
    WHERE em1.EstudianteId = @EstudianteId;
END;
GO

-- Procedimiento para obtener un estudiante por ID
CREATE PROCEDURE sp_GetEstudianteById
    @Id INT
AS
BEGIN
    SELECT e.Id, e.Nombre, e.Documento FROM Estudiantes e WHERE e.Id = @Id;
    SELECT m.Id AS MateriaId, m.Nombre AS NombreMateria, m.Creditos, p.Id AS ProfesorId, p.Nombre AS ProfesorNombre
    FROM Materias m
    INNER JOIN EstudianteMateria em ON m.Id = em.MateriaId
    INNER JOIN ProfesorMateria pm ON em.MateriaId = pm.MateriaId
    INNER JOIN Profesores p ON p.Id = pm.ProfesorId
    WHERE em.EstudianteId = @Id;
END;
GO

-- Procedimiento para obtener materias por ID
CREATE PROCEDURE sp_GetMateriasByIds
    @Ids NVARCHAR(MAX)
AS
BEGIN
    SELECT m.Id, m.Nombre, m.Creditos, pm.ProfesorId
    FROM Materias m
    INNER JOIN ProfesorMateria pm ON m.Id = pm.MateriaId
    WHERE m.Id IN (SELECT value FROM STRING_SPLIT(@Ids, ','));
END;
GO

-- Procedimiento para obtener un profesor por ID
CREATE PROCEDURE sp_GetProfesorById
    @Id INT
AS
BEGIN
    SELECT Id, Nombre FROM Profesores WHERE Id = @Id;
END;
GO

-- Procedimiento para actualizar estudiante
CREATE PROCEDURE sp_UpdateEstudiante
    @Id INT,
    @Nombre NVARCHAR(100),
    @Documento NVARCHAR(50),
    @MateriaIds NVARCHAR(MAX)
AS
BEGIN
    UPDATE Estudiantes
    SET Nombre = @Nombre, Documento = @Documento
    WHERE Id = @Id;

    DELETE FROM EstudianteMateria WHERE EstudianteId = @Id;

    INSERT INTO EstudianteMateria (EstudianteId, MateriaId)
    SELECT @Id, value
    FROM STRING_SPLIT(@MateriaIds, ',')
    WHERE TRY_CAST(value AS INT) IS NOT NULL;
END;
GO
