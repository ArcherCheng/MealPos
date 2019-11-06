
CREATE TABLE AppKeyValue(
	Id int Identity(1,1) NOT NULL,
	KeyGroup nvarchar(30) NOT NULL,
	KeyValue nvarchar(30) NOT NULL,
 	--以下每檔資料表都會有這些欄位
    WriteTime Datetime Null,
	WriteUser nvarchar(30) Null,
	WriteIp   nvarchar(30) NULL,
	
	CONSTRAINT AppKeyValue_Pkey PRIMARY KEY CLUSTERED (Id));
go

insert into AppKeyValue (KeyGroup,KeyValue) values('OrderType','內用');
insert into AppKeyValue (KeyGroup,KeyValue) values('OrderType','外帶');
insert into AppKeyValue (KeyGroup,KeyValue) values('OrderType','自取');

insert into AppKeyValue (KeyGroup,KeyValue) values('MealType','飯類');
insert into AppKeyValue (KeyGroup,KeyValue) values('MealType','麵類');
insert into AppKeyValue (KeyGroup,KeyValue) values('MealType','單點');
insert into AppKeyValue (KeyGroup,KeyValue) values('MealType','湯類');
insert into AppKeyValue (KeyGroup,KeyValue) values('MealType','飲品');

insert into AppKeyValue (KeyGroup,KeyValue) values('Unit','份');
insert into AppKeyValue (KeyGroup,KeyValue) values('Unit','套');
insert into AppKeyValue (KeyGroup,KeyValue) values('Unit','碗');
insert into AppKeyValue (KeyGroup,KeyValue) values('Unit','杯');

 
