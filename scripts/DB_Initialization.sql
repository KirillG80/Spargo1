USE [SP_Test];

INSERT INTO Goods (Name, Price)
Values('Аспирин', 50.00)
DECLARE @PROD_1 AS INT = SCOPE_IDENTITY()

INSERT INTO Goods (Name, Price)
Values('Корвалол', 13.45)
DECLARE @PROD_2 AS INT = SCOPE_IDENTITY()

INSERT INTO Goods (Name, Price)
Values('Глюкоза', 5.50)
DECLARE @PROD_3 AS INT = SCOPE_IDENTITY()

INSERT INTO Goods (Name, Price)
Values('Активированный уголь', 10.80)
DECLARE @PROD_4 AS INT = SCOPE_IDENTITY()

INSERT INTO Goods (Name, Price)
Values('Мирамистин', 240.15)
DECLARE @PROD_5 AS INT = SCOPE_IDENTITY()

INSERT INTO Pharmacy(Name, Address, Phone)
Values('Аптека №1', 'Москва, Пушкина, 10', '8-910-900-23-09')
DECLARE @PHARM_1 AS INT = SCOPE_IDENTITY()

INSERT INTO Pharmacy(Name, Address, Phone)
Values('Радуга', 'Пермь, Пугачёва, 10/2', '8-920-900-23-09')
DECLARE @PHARM_2 AS INT = SCOPE_IDENTITY()

INSERT INTO Storage(Pharm_Id, Name)
Values(@PHARM_1, 'Склад 1')
DECLARE @STOR_1 AS INT = SCOPE_IDENTITY()
INSERT INTO Storage(Pharm_Id, Name)
Values(@PHARM_1, 'Склад 2')
DECLARE @STOR_2 AS INT = SCOPE_IDENTITY()

INSERT INTO Storage(Pharm_Id, Name)
Values(@PHARM_2, 'Первый Склад')
DECLARE @STOR_3 AS INT = SCOPE_IDENTITY()
INSERT INTO Storage(Pharm_Id, Name)
Values(@PHARM_2, 'Третий склад')
DECLARE @STOR_4 AS INT = SCOPE_IDENTITY()

INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_1, @STOR_1, 1200)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_1, @STOR_2, 300)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_1, @STOR_3, 566)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_1, @STOR_4, 3)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_2, @STOR_1, 2)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_2, @STOR_2, 540)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_3, @STOR_3, 10000)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_3, @STOR_4, 13000)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_4, @STOR_1, 4598)
INSERT INTO Party(Goods_Id, Stor_Id, Quantity)
Values(@PROD_5, @STOR_3, 102)

GO