
Create Table AppDataLog(
   Id Bigint Identity(1,1) Not Null,
   TableName Nvarchar(30) not Null,
   InsertData Nvarchar(max) Null,
   DeleteData Nvarchar(max) Null,
   WriteType Tinyint not Null, 
   WriteTime Datetime Null,
   Constraint AppDataLog_Pkey Primary Key (Id Asc)
);
Go

Alter Table AppDataLog Add Constraint AppDataLog_WriteTime_Default Default (Getdate()) For WriteTime
Go


