--USE MASTER
--DROP DATABASE DiaIngInformatico2025DB

USE MASTER

CREATE DATABASE DiaIngInformatico2025DB
COLLATE Modern_Spanish_CI_AI; 
GO
USE DiaIngInformatico2025DB

-- #################################################### --
-- #################################################### --
-- ASISTENCIAS
-- #################################################### --
-- #################################################### --

BEGIN

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
			CONSTRAINT [PK_Invitado] PRIMARY KEY ([Id]),
			CONSTRAINT [UK_Invitado] UNIQUE ([CorreoElectronico])
		)

	END


END