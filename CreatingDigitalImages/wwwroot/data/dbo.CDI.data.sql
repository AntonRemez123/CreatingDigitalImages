SET IDENTITY_INSERT [dbo].[CDI] ON
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (1, N'Creating Digital Images', N'')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (2, N'Resources', N'Creating Digital Images')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (3, N'Evidence', N'Creating Digital Images')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (4, N'Graphic Products', N'Creating Digital Images')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (5, N'Primary Sources', N'Resources')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (6, N'Secondary Sources', N'Resources')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (7, N'Process', N'Graphic Products')
INSERT INTO [dbo].[CDI] ([Id], [Name], [Parent]) VALUES (8, N'Final Product', N'Graphic Products')
SET IDENTITY_INSERT [dbo].[CDI] OFF
