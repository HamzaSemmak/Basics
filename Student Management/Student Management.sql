CREATE DATABASE Student_Management;

USE Student_Management;

/* 
*	Table : 
*/

CREATE TABLE Users (
	ID INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(255),
	Password VARCHAR(255),
);

CREATE TABLE SuspendAdmins (
	ID INT PRIMARY KEY IDENTITY(1,1),
	ID_Admins INT FOREIGN KEY REFERENCES Users(ID),
	Status VARCHAR(25) DEFAULT 'InLocked',
	CONSTRAINT CK_Admins_Status CHECK (Status in ('Locked', 'InLocked')),
	Checks INT DEFAULT 1,
	CONSTRAINT CK_Cheks CHECK (Checks < 4),
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
INSERT INTO Users VALUES('Hamza Semmak', 'AA102374');
INSERT INTO Users VALUES('Karim Aissa', 'AA102374');
/* 
*	Procédure Stockée : 
*/

CREATE PROCEDURE Authentification
	@UserName VARCHAR(255), 
	@Password VARCHAR(255)
AS
BEGIN
	DECLARE @Response INT
	IF EXISTS (SELECT * FROM Users WHERE Name LIKE @UserName)
		IF EXISTS (SELECT * FROM Users WHERE Name LIKE @UserName AND Password LIKE @Password)
			SET @Response = 200
		ELSE
			SET @Response = 202
	ELSE
		SET @Response = 201

	RETURN @Response;
END;
DECLARE @Status INT;
EXECUTE @Status = Authentification 'Hamza Semmak', 'AA102374';
SELECT @Status;
/* 200 => Access || 201 => UserName is Incorrect || 202 => Password is Incorect */
SELECT * FROM Users;

