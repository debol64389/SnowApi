USE [SnowDatabase]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessagesSent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageTemplateId] [int] NOT NULL,
	[CustomerUniqueId] [nvarchar](10) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[DateTimeSent] [datetime] NULL,
 CONSTRAINT [PK_MessagesSent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MessagesSent]  WITH CHECK ADD  CONSTRAINT [FK_MessageTemplate] FOREIGN KEY([MessageTemplateId])
REFERENCES [dbo].[MessageTemplates] ([Id])
GO

ALTER TABLE [dbo].[MessagesSent] CHECK CONSTRAINT [FK_MessageTemplate]
GO

ALTER TABLE [dbo].[MessagesSent]  WITH CHECK ADD  CONSTRAINT [FK_CustomerUnique] FOREIGN KEY([CustomerUniqueId])
REFERENCES [dbo].[Customers] ([UniqueId])
GO

ALTER TABLE [dbo].[MessagesSent] CHECK CONSTRAINT [FK_CustomerUnique]
GO
