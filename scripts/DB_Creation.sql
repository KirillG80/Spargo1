
IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'SP_Test')
	CREATE DATABASE SP_Test
	COLLATE Cyrillic_General_CI_AS
GO
 
USE [SP_Test];

IF NOT EXISTS(SELECT name FROM sys.table_types WHERE name = 'Goods')
	CREATE TABLE Goods
	(
		Id INT PRIMARY KEY IDENTITY,
		Name NVARCHAR(20) NOT NULL,
		Price DECIMAL(10,2) NOT NULL
	)

IF NOT EXISTS(SELECT name FROM sys.table_types WHERE name = 'Pharmacy')
	CREATE TABLE Pharmacy
	(
		Id INT PRIMARY KEY IDENTITY,
		Name NVARCHAR(20) NOT NULL,
		Address VARCHAR(30) UNIQUE,
		Phone VARCHAR(20) UNIQUE
	)

IF NOT EXISTS(SELECT name FROM sys.table_types WHERE name = 'Storage')
	CREATE TABLE Storage
	(
		Id INT PRIMARY KEY IDENTITY,
		Pharm_Id INT,
		Name NVARCHAR(20) NOT NULL,
		FOREIGN KEY (Pharm_Id) REFERENCES Pharmacy (Id) ON DELETE CASCADE
	)

IF NOT EXISTS(SELECT name FROM sys.table_types WHERE name = 'Party')
	CREATE TABLE Party
	(
		Id INT PRIMARY KEY IDENTITY,
		Goods_Id INT,
		Stor_Id INT,
		Quantity INT,
		FOREIGN KEY (Goods_Id) REFERENCES Goods (Id) ON DELETE CASCADE,
		FOREIGN KEY (Stor_Id) REFERENCES Storage (Id) ON DELETE CASCADE
	)

GO