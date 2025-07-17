-- SQL script to create the TasksManagerDb database and required tables

-- Create database (run this in master or any existing database context)
CREATE DATABASE TasksManagerDb;
GO

USE TasksManagerDb;
GO

-- Create TaskMaster table with additional columns
CREATE TABLE TaskMaster (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeDomainId NVARCHAR(255) NOT NULL,
    TaskDescription NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    SiteCode NVARCHAR(100),       
    PlantCode NVARCHAR(100),      
    Function NVARCHAR(100)        
);
GO

-- Create Tasklogs table with additional columns
CREATE TABLE Tasklogs (
    TaskId INT NOT NULL,
    Status NVARCHAR(255) NOT NULL,
    PrevoiusStatus NVARCHAR(255) NOT NULL,
    Remarks NVARCHAR(255),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    StatusUpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TaskTransaction_TaskMaster FOREIGN KEY (TaskId) REFERENCES TaskMaster(TaskId)
);
GO
