
CREATE TABLE Customer(
	Id        Int Identity(1,1) NOT NULL,
	Name      nvarchar(60) NOT NULL,
	EngName   nvarchar(60) NULL,

	TaxId     nvarchar(30) NULL,
	TelNo     nvarchar(30) NULL,
	MobileNo  nvarchar(30) NULL,
	FaxNo     nvarchar(30) NULL,

	PostNo    nvarchar(6) NULL,
	Address   nvarchar(60) NULL,

	Contactor nvarchar(30) NULL,
	Notes     nvarchar(500) NULL,

 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime       Null,
	WriteUser     nvarchar(30)   Null,
	WriteIp       nvarchar(30)   NULL,
CONSTRAINT Customer_Pkey PRIMARY KEY CLUSTERED (Id));
go
--Create unique index Customer_Idx1 on Customer (Name asc) WHERE eSSN is not null ;
--create unique index Customer_Idx2 on Customer (TelNo asc) WHERE eSSN is not null ;
--create unique index Customer_Idx3 on Customer (MobileNo asc) WHERE eSSN is not null ;
--create unique index Customer_Idx4 on Customer (TaxNo asc) WHERE eSSN is not null ;
Create index Customer_Idx1 on Customer (Name asc);
create index Customer_Idx2 on Customer (TelNo asc);
create index Customer_Idx3 on Customer (MobileNo asc);
create index Customer_Idx4 on Customer (TaxId asc);
go


Create Trigger Customer_Trigger1 On Customer After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='Customer';

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
End
Go

insert into customer(name,engname)
values('來店客','Guset')
go

insert into customer(name,engname)
values('VIP客戶','VIP')
go

insert into customer(name,engname)
values('優惠券客戶','Coupon')
go
