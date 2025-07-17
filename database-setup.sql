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
    SiteCode NVARCHAR(100),       -- New column
    PlantCode NVARCHAR(100),      -- New column
    Function NVARCHAR(100)        -- New column
);
GO

-- Create TaskTransaction table with additional columns
CREATE TABLE TaskTransaction (
    TaskTransactionId INT IDENTITY(1,1) PRIMARY KEY,
    TaskId INT NOT NULL,
    Status NVARCHAR(255) NOT NULL,
    StatusUpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    SiteCode NVARCHAR(100),       -- New column
    PlantCode NVARCHAR(100),      -- New column
    Function NVARCHAR(100),       -- New column
    CONSTRAINT FK_TaskTransaction_TaskMaster FOREIGN KEY (TaskId) REFERENCES TaskMaster(TaskId)
);
GO
