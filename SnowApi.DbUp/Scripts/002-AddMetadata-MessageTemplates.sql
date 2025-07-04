USE [SnowDatabase]
GO

INSERT [dbo].[MessageTemplates] ([Name], [Subject], [Body], [SetAsDeleted]) VALUES (N'Email confirmation', N'Please confirm your email', N'Dear {CustomerName}, please confirm your your email by following this link: ''link_to_confirm_email''.', 0)
GO
INSERT [dbo].[MessageTemplates] ([Name], [Subject], [Body], [SetAsDeleted]) VALUES (N'Unique Id reminder', N'Details reminder', N'Dear {CustomerName}, your unique id is {UniqueId} and your email is {EmailAddress}. Please keep this information safe.', 0)
GO
