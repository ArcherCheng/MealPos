--Drop Table AppUserLog;
go
Create Table AppUserLog(
 	Id bigint identity(1,1) not null,
	UserId nvarchar(30) not null,
	Refer nvarchar(255) null,
	Destination nvarchar(255) null,
	QueryString nvarchar(255) null,
	Method nvarchar(30) null,
	IpAddress nvarchar(30) null,
	RequestTime datetime null,
   Constraint AppUserLog_Pkey Primary Key (Id Asc)
);
Go

Alter Table AppUserLog Add Constraint AppUserLog_RequestTime_Default Default (Getdate()) For RequestTime
Go
