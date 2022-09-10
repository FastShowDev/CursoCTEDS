USE db_5

--Chave mestre:
CREATE MASTER KEY 
ENCRYPTION BY PASSWORD = 'ChaveMestra'
GO

--Certificado:
CREATE CERTIFICATE MeuCertificado
ENCRYPTION BY PASSWORD = 'Certificado'
WITH SUBJECT = 'Meu Certificado'
GO

--Criação de chave simétrica usando algoritmo AES 256 e por meio de um certificado:
CREATE SYMMETRIC KEY MinhaChave
WITH ALGORITHM = AES_256
ENCRYPTION BY CERTIFICATE MeuCertificado --
GO

--Tabela:
CREATE TABLE Users(
	UserID VARCHAR(255) NOT NULL UNIQUE,
	[Name] VARCHAR(255) NOT NULL,
	Job VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL UNIQUE,
	[Password] VARCHAR(255) NOT NULL

);
GO

--Criptografia da senha: 
OPEN SYMMETRIC KEY MinhaChave
DECRYPTION BY CERTIFICATE MeuCertificado WITH PASSWORD = 'Certificado'

DECLARE @GUID UNIQUEIDENTIFIER = (SELECT KEY_GUID('MinhaChave'))
INSERT INTO Users (UserID, [Name], Job, Email, [Password])
--VALUES ('da910cf1-4b58-421a-b1bb-88b390ea3556', 'Gabriel Camargo', 'ADMIN', 'gabriel@email.com', ENCRYPTBYKEY(@GUID, 'gabriel123'))
VALUES  ('5ed79876-17e7-4b6b-8ee4-86f305d19666','Administrator','ADMIN','admin@email.com', ENCRYPTBYKEY(@GUID, 'admin123'))
CLOSE SYMMETRIC KEY MinhaChave


--Descriptografia da senha:
OPEN SYMMETRIC KEY MinhaCHave
DECRYPTION BY CERTIFICATE MeuCertificado WITH PASSWORD = 'Certificado'

SELECT *
, SenhaDescriptografada = CAST (DECRYPTBYKEY([Password]) AS VARCHAR(255)) 
FROM Users WHERE Email = 'admin@email.com'

CLOSE SYMMETRIC KEY MinhaChave

--Teste inicial, sem criptografia
INSERT INTO Users (UserID, [Name], Job, Email, [Password])
VALUES  ('5ed79876-17e7-4b6b-8ee4-86f305d19666','Administrator','ADMIN','admin@email.com', 'admin123')
GO

SELECT * FROM Users
GO

DELETE FROM Users
WHERE Email = 'admin@email.com'

--Visualizar as keys e certificados existentes:
SELECT * FROM SYS.symmetric_keys
SELECT * FROM SYS.certificates

SELECT UserId, [Name], Email, [Password] FROM Users
WHERE Email = 'admin@email.com'
GO