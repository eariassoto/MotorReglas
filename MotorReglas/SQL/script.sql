DROP TABLE IF EXISTS [Hechos]

GO
DROP TABLE IF EXISTS [PropiedadDispositivo]

GO
DROP TABLE IF EXISTS [Dispositivos]

GO
DROP TABLE IF EXISTS [Reglas]

GO
CREATE TABLE [Hechos] (
	Id integer IDENTITY(1,1) NOT NULL,
	Nombre varchar(128) NOT NULL,
	Valor varchar(128) NOT NULL,
  CONSTRAINT [PK_HECHOS] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Dispositivos] (
	Id integer IDENTITY(1,1) NOT NULL,
	Nombre varchar(128) NOT NULL
	CONSTRAINT [PK_DISPOSITIVOS] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Reglas] (
	Id integer IDENTITY(1,1) NOT NULL,
	TipoEvaluacion  varchar(128) NOT NULL,
	PropiedadEvaluacion varchar(128) NOT NULL,
	ValorPropiedadEvaluacion varchar(128) NOT NULL,
	PropiedadDispositivo varchar(128) NOT NULL,
	ValorPropiedadDispositivo varchar(128) NOT NULL,
	Certeza integer NOT NULL,
  CONSTRAINT [PK_REGLAS] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [PropiedadDispositivo] (
	Id integer IDENTITY(1,1) NOT NULL,
	IdDisp integer NOT NULL,
	Nombre varchar(128) NOT NULL,
	Valor varchar(128) NOT NULL,
  CONSTRAINT [PK_PROPIEDADDISPOSITIVO] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO

/*
DELETE FROM PropiedadDispositivo;
DELETE FROM Dispositivos;
DELETE FROM Hechos;
DELETE FROM Reglas;
DBCC CHECKIDENT ('[Dispositivos]', RESEED, 0);
GO
DBCC CHECKIDENT ('[PropiedadDispositivo]', RESEED, 0);
GO
DBCC CHECKIDENT ('[Hechos]', RESEED, 0);
GO
DBCC CHECKIDENT ('[Reglas]', RESEED, 0);
GO
*/
/*
SELECT * FROM Dispositivos;
SELECT * FROM PropiedadDispositivo;
SELECT * FROM Hechos;
SELECT * FROM Reglas;
*/

INSERT INTO [Dispositivos] VALUES('Smartphone');
INSERT INTO [Dispositivos] VALUES('SmartTV');
INSERT INTO [Dispositivos] VALUES('Smart Fridge');
INSERT INTO [Dispositivos] VALUES('Smartwatch');
INSERT INTO [Dispositivos] VALUES('Tablet');
INSERT INTO [Dispositivos] VALUES('Work PC');
INSERT INTO [Dispositivos] VALUES('Amazon Echo');
INSERT INTO [Dispositivos] VALUES('Car''s Screen');

-- SELECT * FROM [Dispositivos];
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(1, 'location', 'user');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(1, 'personal', 'yes');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(1, 'sweetness', 'medium');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(1, 'sweetness', 'sweet');

INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(2, 'location', 'tv-room');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(2, 'location', 'house');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(2, 'personal', 'no');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(2, 'sweetness', 'dry');

INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(3, 'location', 'kitchen');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(3, 'location', 'house');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(3, 'personal', 'no');
INSERT INTO [PropiedadDispositivo] (IdDisp, Nombre, Valor) VALUES(3, 'sweetness', 'dry');

INSERT INTO [Hechos] VALUES ('preferred-location', 'tv-room');
INSERT INTO [Hechos] VALUES ('sense-of-urgency', 'warning');

INSERT INTO [Reglas] VALUES ('fact', 'preferred-location', 'tv-room', 'location', 'house', 50);
INSERT INTO [Reglas] VALUES ('fact', 'sense-of-urgency', 'warning', 'sweetness', 'medium', 100);
INSERT INTO [Reglas] VALUES ('fact', 'sense-of-urgency', 'warning', 'sweetness', 'medium', 100);
INSERT INTO [Reglas] VALUES ('message', 'message-type', 'text', 'personal', 'yes', 100);

select * from reglas;