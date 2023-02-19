CREATE DATABASE Student_Management;

use Student_Management;

/* 
*	Table : 
*/

Create Table Users (
	ID int primary key identity(1,1),
	Name varchar(255),
	Password varchar(255),
	Email varchar(255),
	Phone varchar(10),
	Status varchar(25) default 'InLocked',
	Constraint CK_User_Status check (Status in ('Locked', 'InLocked')),
	Checks int default 1,
	Constraint CK_Checks check (Checks < 4),
);

