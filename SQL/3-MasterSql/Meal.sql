CREATE TABLE Meal(
	Id int Identity(1,1) NOT NULL,
	MealName     nvarchar(60) NOT NULL,
	MealType     nvarchar(30) NOT NULL,  --由 sysCode 檔案帶入 
	Unit         nvarchar(15) NOT NULL,  --由 sysCode 檔案帶入 
	BarCode      nvarchar(30) NULL,

	Cost         int not null,
	BrandPrice   int not null,
	SalePrice    int not null,

	CookMinutes  int not null, --烹調時間
	Notes        nvarchar(500) NULL,

 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime     Null,
	WriteUser     nvarchar(30) Null,
	WriteIp       nvarchar(30) NULL,
CONSTRAINT Meal_Pkey PRIMARY KEY CLUSTERED (Id));
go
Create unique index Meal_Idx1 on Meal (MealName asc) WHERE MealName is not null ;


Create Trigger Meal_Trigger1 On Meal After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='Meal';

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

CREATE TABLE MealAddOn(
	Id int       NOT NULL,
	AddOnName    nvarchar(15) NOT NULL,  
	AddPrice     int not null,
 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime     Null,
	WriteUser     nvarchar(30)   Null,
	WriteIp       nvarchar(30)   NULL,
CONSTRAINT MealAddOn_Pkey PRIMARY KEY CLUSTERED (Id));
go


Create Trigger MealAddOn_Trigger1 On MealAddOn After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='MealAddOn';

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


CREATE TABLE MealAddOnRela(
	MealId  int  NOT NULL,
	AddOnId int  NOT NULL,
 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime     Null,
	WriteUser     nvarchar(30)   Null,
	WriteIp       nvarchar(30)   NULL,
CONSTRAINT MealAddOnRela_Pkey PRIMARY KEY CLUSTERED (MealId,AddOnId));
go

ALTER TABLE  MealAddOnRela ADD CONSTRAINT MealAddOnRela_MealId
      FOREIGN KEY (MealId)
      REFERENCES Meal (Id)
GO
ALTER TABLE  MealAddOnRela ADD CONSTRAINT MealAddOnRela_AddOnId
      FOREIGN KEY (AddOnId)
      REFERENCES MealAddOn (Id)
GO


Create Trigger MealAddOnRela_Trigger1 On MealAddOnRela After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='MealAddOnRela';

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

/*

insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('牛肉麵','麵類','碗',100,150,140,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('牛肉湯麵','麵類','碗',60,40,50,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('豬腳麵線','麵類','碗',60,100,90,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('豬腳飯','飯類','碗',70,110,100,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('焢肉飯','飯類','碗',60,100,90,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('四神湯','湯類','碗',40,60,50,10)
insert into meal(mealname,mealtype,unit,cost,brandprice,saleprice,cookminutes)
values('蛋花湯','湯類','碗',20,40,30,10)


insert into mealaddon(id,AddOnName,addprice)
values(1,'加麵',5) 
insert into mealaddon(id,AddOnName,addprice)
values(2,'加湯',0) 
insert into mealaddon(id,AddOnName,addprice)
values(3,'加飯',5) 
insert into mealaddon(id,AddOnName,addprice)
values(4,'加菜',5) 
insert into mealaddon(id,AddOnName,addprice)
values(5,'加肉',10) 
insert into mealaddon(id,AddOnName,addprice)
values(6,'減麵',-5) 
insert into mealaddon(id,AddOnName,addprice)
values(7,'減湯',0) 
insert into mealaddon(id,AddOnName,addprice)
values(8,'減飯',5) 
insert into mealaddon(id,AddOnName,addprice)
values(9,'減菜',-5) 
insert into mealaddon(id,AddOnName,addprice)
values(10,'減肉',-10) 

insert into mealaddonRela(mealId,AddOnid)
values(1,1)
insert into mealaddonRela(mealId,AddOnid)
values(1,2)
insert into mealaddonRela(mealId,AddOnid)
values(2,1)
insert into mealaddonRela(mealId,AddOnid)
values(2,2)


select * from meal
select * from mealaddon
select * from mealaddonrela

insert into mealaddonRela(mealId,AddOnid)
values(3,1)
insert into mealaddonRela(mealId,AddOnid)
values(3,2)


*/