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

Select * from Users;

/* Procedure Stocke */
Create Procedure Authentification
(
	@UserName varchar(255), 
	@Password varchar(255),
	@msg int Out
)
As
Begin 
	If exists (Select * from Users where Name = @UserName)
		If Exists (Select * from Users where Name = @UserName And Password = @Password)
			Begin Set @msg = 200
			Return @msg End
		Else
			Begin Set @msg = 202
			Return @msg End
	Else
		Set @msg = 201
		Return @msg
End

Declare @status int
Execute Authentification 'Hamza Semmak4', 'AA102374', @status Output
Select @status




