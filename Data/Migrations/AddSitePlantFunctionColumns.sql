-- Migration script to add SiteCode, PlantCode, and Function columns to TaskMaster and TaskTransaction tables

IF COL_LENGTH('TaskMaster', 'SiteCode') IS NULL
BEGIN
    ALTER TABLE TaskMaster ADD SiteCode NVARCHAR(255) NOT NULL DEFAULT '';
    ALTER TABLE TaskMaster ADD PlantCode NVARCHAR(255) NOT NULL DEFAULT '';
    ALTER TABLE TaskMaster ADD Function NVARCHAR(255) NOT NULL DEFAULT '';
END
GO

IF COL_LENGTH('TaskTransaction', 'SiteCode') IS NULL
BEGIN
    ALTER TABLE TaskTransaction ADD SiteCode NVARCHAR(255) NOT NULL DEFAULT '';
    ALTER TABLE TaskTransaction ADD PlantCode NVARCHAR(255) NOT NULL DEFAULT '';
    ALTER TABLE TaskTransaction ADD Function NVARCHAR(255) NOT NULL DEFAULT '';
END
GO
