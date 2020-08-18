--Creating database only if database is not created yet
IF DB_ID('Zadatak_49') IS NULL
CREATE DATABASE Zadatak_49
GO
USE Zadatak_49;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblManagers')
drop table tblManagers;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmploye')
drop table tblEmploye;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblAll')
drop table tblAll;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblDegree')
drop table tblDegree;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEngagment')
drop table tblEngagment;

create table tblAll (
All_ID int identity(1,1) primary key,
FirstName nvarchar (50) not null ,
Surname nvarchar (50) not null,
DateOfBirth date ,
Email nvarchar(40) not null,
Username nvarchar(50) not null,
Pasword nvarchar(50) not null,
)

create table tblManagers(
ManagerID int identity(1,1) primary key,
AllIDman int,
ManagerFlor int,
Experience int,
SSS nvarchar (3)
)

create table tblDegree(
degreIS int identity (1,1) primary key,
name nvarchar (5)
)

create table tblEmploye(
EmployeID int identity (1,1) primary key,
AllIDemp int,
EmployeFlor int,
Gender nvarchar(1),
Salary int,
Citizenship nvarchar(50),
Engagment nvarchar(20)
)
create table tblEngagment(
engID int identity (1,1) primary key,
engName nvarchar (15)
)

Alter Table tblManagers
Add foreign key (AllIDman) references tblAll(All_ID);

Alter Table tblEmploye
Add foreign key (AllIDemp) references tblAll(All_ID);

Insert into tblDegree values ('I'),('II'),('III'),('IV'),('V'),('VI'),('VII');
Insert into tblEngagment values ('Cleaning'),('Cooking'),('Supervising'),('Reporting');






