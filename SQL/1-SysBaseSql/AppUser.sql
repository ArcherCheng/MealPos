--Drop table AppUser
go
create table AppUser
(
	Id      int identity(1,1)   not null,
	UserName    nvarchar(30)   not null, --我的暱稱
	Phone       nvarchar(15)   not null, --行動電話
	Email       nvarchar(30)   not null, --電子郵件
	MainPhotoUrl  nvarchar(250)   null,--封面相片網址

	--這部份內容由系統自動產生
	IsInWork      bit            not null,--是否關閉檔案,即不做網路配對
	LoginDate     datetime       null,--上次登入日期
	UserRole      nvarchar(15)   null,--角色歸屬 users,operators,managers,admins
	PasswordHash  varbinary(2000)   null,
	PasswordSalt  varbinary(2000)   null,

    WriteTime     Datetime Null,
	WriteUser     Nvarchar(30)   Null, 
	WriteIp       Nvarchar(30)   Null, 
	constraint AppUser_Pkey primary key (Id) 
);
Go
Alter Table AppUser Add Constraint AppUser_IsInWork_Default Default 1 For IsInWork
Go

Create unique index AppUser_Idx1 on AppUser (Email asc);
create unique index AppUser_Idx2 on AppUser (Phone asc);
create unique index AppUser_Idx3 on AppUser (UserName asc);
go

create trigger AppUser_trigger1 on AppUser after insert,update,delete not for replication as
begin
	Declare @TableName Nvarchar(30);
	Set @TableName='AppUser';

	Declare @WriteType Tinyint;
	Set @WriteType=0;
 
	If Exists(Select 1 From Inserted) And Not Exists(Select 1 From Deleted)
		Set @WriteType = 1;    --Insert
	Else If Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @WriteType = 2;    --Update
	Else If Not Exists(Select 1 From Inserted) And Exists(Select 1 From Deleted)
		Set @WriteType = 3;    --Delete
	
	Declare @InsertData Nvarchar(max);
	Declare @DeleteData Nvarchar(max);

	Set @InsertData=(Select * From Inserted For Json Auto);
	Set @DeleteData=(Select * From Deleted For Json Auto);

	Insert Into AppDataLog(TableName,InsertData,DeleteData,WriteType) Values(@TableName,@InsertData,@DeleteData,@WriteType);
end
go
