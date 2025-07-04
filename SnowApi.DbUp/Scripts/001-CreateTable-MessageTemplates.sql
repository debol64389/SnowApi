﻿IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SnowDatabase')
BEGIN
CREATE DATABASE [SnowDatabase]
END
GO

USE [SnowDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Subject] [nvarchar](100) NULL,
	[Body] [nvarchar](MAX) NOT NULL,
	[SetAsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_MessageTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
