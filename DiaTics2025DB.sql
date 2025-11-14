--USE MASTER
--DROP DATABASE WebAppDB

USE MASTER

CREATE DATABASE WebAppDB
COLLATE Modern_Spanish_CI_AI; 
GO
USE WebAppDB

-- #################################################### --
-- #################################################### --
-- ASISTENCIAS
-- #################################################### --
-- #################################################### --

BEGIN

    -- ====================================================
	-- Taller
	-- ====================================================
	BEGIN

		-- DROP TABLE Taller
		CREATE TABLE [dbo].[Taller]
		(
			[Id]			        TINYINT 		NOT NULL,
			[Nombre]				VARCHAR(100)		NOT NULL,
			CONSTRAINT [PK_Taller] PRIMARY KEY ([Id]),
		)

	    INSERT INTO Taller VALUES(1, 'Desarrollo Web con IA')
	    INSERT INTO Taller VALUES(2, 'Hacking ético y buenas prácticas de seguridad informática')
	    INSERT INTO Taller VALUES(3, 'Electrónica')
	    INSERT INTO Taller VALUES(4, 'Desarrollo de Apps móviles')

	END


	-- ====================================================
	-- Alumno										
	-- ====================================================
	BEGIN

		-- DROP TABLE Alumno
		CREATE TABLE [dbo].[Alumno]
		(
			[NumeroControl]			VARCHAR(10)		NOT NULL, 
			[Nombre]				VARCHAR(50)		NOT NULL,
			[ApellidoPaterno]		VARCHAR(50)		NOT NULL,
			[ApellidoMaterno]		VARCHAR(50)		NOT NULL,
			[Carrera]				VARCHAR(50)		NOT NULL,
			[RegistradoParaEvento]	BIT			    NOT NULL,
			[FechaRegistro]			DATETIME	    NULL,
			CONSTRAINT [PK_Alumno] PRIMARY KEY ([NumeroControl]),
		)
		
	END

	
	-- ====================================================
	-- Invitado										
	-- ====================================================
	BEGIN

		-- DROP TABLE Invitado
		CREATE TABLE [dbo].[Invitado]
		(
			[Id]					INT				NOT NULL IDENTITY, 
			[Nombre]				VARCHAR(100)	NOT NULL,
			[ApellidoPaterno]		VARCHAR(50)		NOT NULL,
			[ApellidoMaterno]		VARCHAR(50)		NOT NULL,
			[Escuela]				VARCHAR(100)	NOT NULL,
			[CorreoElectronico]		VARCHAR(100)	NOT NULL,
			[FechaRegistro]			DATETIME		NOT NULL,
            [IdTallerRegistrado]    TINYINT         NOT NULL,
			CONSTRAINT [PK_Invitado] PRIMARY KEY ([Id]),
			CONSTRAINT [UK_Invitado] UNIQUE ([CorreoElectronico]),
            CONSTRAINT [FK_Invitado_Taller]
				FOREIGN KEY ([IdTallerRegistrado]) REFERENCES Taller([Id])
		)

	END


END