CREATE DATABASE Student_Management;

USE Student_Management;

/* 
*	Table : 
*/

CREATE TABLE Admins (
	ID INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(255),
	Password VARCHAR(255),
);

CREATE TABLE Formers (
	ID INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(255),
	Date_Of_Birth date,
	Age INT,
	Adress VARCHAR(255),
	City varchar(100),
	Phone INT
);

CREATE TABLE Subjects (
	ID INT PRIMARY KEY IDENTITY(1,1),
	Subject Varchar(255),
	CONSTRAINT Ck_Subject CHECK (Subject in ('Math', 'Science','English','France','Arabe')),
	Nomber_Of_Hours INT,
);

CREATE TABLE Students (
	ID INT PRIMARY KEY IDENTITY(1,1),
	Registration_Number VARCHAR(25) UNIQUE,
	Name VARCHAR(255),
	Date_Of_Birth date,
	Age INT,
	Adress VARCHAR(255),
	City varchar(100),
	Phone INT
);

CREATE TABLE Study (
	ID INT PRIMARY KEY IDENTITY(1,1),
	ID_Subject INT FOREIGN KEY REFERENCES Subjects(ID),
	ID_Student INT FOREIGN KEY REFERENCES Students(ID),
);

CREATE TABLE Teach (
	ID INT PRIMARY KEY IDENTITY(1,1),
	ID_Subject INT FOREIGN KEY REFERENCES Subjects(ID),
	ID_Student INT FOREIGN KEY REFERENCES Students(ID),
);

/* 
*	Insertion Data : 
*/
INSERT INTO Admins VALUES('Hamza Semmak', 'AA102374');
INSERT INTO Admins VALUES('Karim Aissa', 'AA102374');

/* 
*	Procédure Stockée : 
*/

CREATE PROCEDURE Authentification
	@UserName VARCHAR(255), 
	@Password VARCHAR(255)
AS
BEGIN	
	DECLARE @Bool BIT
	IF EXISTS (SELECT * FROM Admins WHERE Name LIKE @UserName)
		IF EXISTS (SELECT * FROM Admins WHERE Name LIKE @UserName AND Password LIKE @Password)
			BEGIN
				SET @Bool = 1
			END
		ELSE
			BEGIN
				SET @Bool = 0
			END
	ELSE
		SET @Bool = 0

	RETURN @Bool;
END;
DECLARE @Status BIT;
EXECUTE @Status = Authentification 'Hamza Semmak', 'AA102374';
SELECT @Status;