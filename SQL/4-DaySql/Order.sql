/*
報價單 - QUOTATION
訂購單 - REQEUST ORDER
採購單 - PURCHASE ORDER
進貨單 - BILL OF PURCHASE
銷貨單 - BILL OF SALE

Drop TABLE OrderDetail
Drop TABLE OrderMaster
*/

CREATE TABLE OrderMaster(
	Id            int Identity(1,1) NOT NULL,
	CustomerId    int NULL,
	SeatNo        nvarchar(15) null,   --由select max(OrderNo) from ordermaster where orderdate=today and OrderType='內用'
	OrderType     nvarchar(15) not null,  --由sysCode帶入,內用,外帶,自取,
	OrderDate     Date not null,
	OrderNo       int not null,   --取餐編號, OrderType + OrderDate  產生

	OrderAmt      int not null,
	TaxType       int not null,  --0=免稅,1=應稅
	TaxRate       int not null,
	TaxAmt        int not null,
	TotalAmt      int not null,

	InvoiceNo     nvarchar(30), 
	TaxId         nvarchar(30), 
	Notes         nvarchar(500) NULL,
	--烹調狀態
	CookStatus    int not null, --0=準備中,1=烹調中,2=烹調完成

 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime Null,
	WriteUser     nvarchar(30)   Null,
	WriteIp       nvarchar(30)   NULL,
CONSTRAINT OrderMaster_Pkey PRIMARY KEY CLUSTERED (Id));
go

ALTER TABLE  OrderMaster ADD CONSTRAINT OrderMaster_CustomerId
      FOREIGN KEY (CustomerId)
      REFERENCES Customer (Id)
GO


Create Trigger OrderMaster_Trigger1 On OrderMaster After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='OrderMaster';

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

--Drop TABLE OrderDetail;
go
CREATE TABLE OrderDetail(
	Id           int Identity(1,1) NOT NULL,
	MasterId     int NOT NULL,

	MealId       int NOT NULL,
	MealAddOnId  int Not NULL,
	Qty          int not null,
	Price        int not null,
	AddPrice     int not null,
	--TotalAmt     int not null,  --金額由提交時再計算,client 端不計算
	--Notes        nvarchar(500) NULL,

 	--以下每檔資料表都會有這些欄位
    WriteTime     Datetime Null,
	WriteUser     nvarchar(30)   Null,
	WriteIp       nvarchar(30)   NULL,
CONSTRAINT OrderDetail_Pkey PRIMARY KEY CLUSTERED (Id));
go

ALTER TABLE  OrderDetail ADD CONSTRAINT OrderDetail_MasterId
      FOREIGN KEY (MasterId)
      REFERENCES OrderMaster (Id)
GO

ALTER TABLE  OrderDetail ADD CONSTRAINT OrderDetail_MealId
      FOREIGN KEY (MealId)
      REFERENCES Meal (Id)
GO



Create Trigger OrderDetail_Trigger1 On OrderDetail After Insert,Update,Delete Not For Replication As
Begin
	Declare @TableName Nvarchar(30);
	Set @TableName='OrderDetail';

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
select * from ordermaster
select * from orderdetail
go

insert into ordermaster(customerId,ordertype,orderdate,orderno,seatno,orderamt,taxtype,taxrate,taxamt,totalamt,cookStatus)
values(1,1,'2019-11-3',1,'a-1',100,2,5,5,210,0)

insert into ordermaster(customerId,ordertype,orderdate,orderno,seatno,orderamt,taxtype,taxrate,taxamt,totalamt,cookStatus)
values(1,1,'2019-11-3',2,'a-2',200,2,5,10,210,0)

insert into ordermaster(customerId,ordertype,orderdate,orderno,seatno,orderamt,taxtype,taxrate,taxamt,totalamt,cookStatus)
values(1,1,'2019-11-3',3,'a-3',300,2,5,15,315,0)
go


insert into orderdetail(masterid,mealid,mealaddonid,qty,price,addprice,totalamt)
values(4,1,1,1,100,5,105)

insert into orderdetail(masterid,mealid,mealaddonid,qty,price,addprice,totalamt)
values(5,1,1,2,100,5,210)
insert into orderdetail(masterid,mealid,mealaddonid,qty,price,addprice,totalamt)
values(5,2,1,3,100,5,315)

insert into orderdetail(masterid,mealid,mealaddonid,qty,price,addprice,totalamt)
values(6,1,1,2,100,5,210)
insert into orderdetail(masterid,mealid,mealaddonid,qty,price,addprice,totalamt)
values(6,1,1,1,100,5,105)


*/