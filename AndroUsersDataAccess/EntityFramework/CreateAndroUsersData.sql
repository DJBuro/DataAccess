SET IDENTITY_INSERT [dbo].[Permission] ON 

GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (2, N'ViewStores', N'View store details', 1)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (3, N'AddStore', N'Add a new store', 2)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (4, N'EditStore', N'Edit an existing store', 3)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (5, N'ViewPaymentProviders', N'View payment providers', 4)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (6, N'AddPaymentProvider', N'Add a new payment provider', 5)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (7, N'ViewAMSStores', N'View AMS stores', 6)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (8, N'EditAMSStore', N'Edit AMS stores', 7)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (9, N'ViewAMSServers', N'View AMS servers', 8)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (10, N'AddAMSServer', N'Add a new AMS server', 9)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (11, N'EditAMSServer', N'Edit an existing AMS server', 10)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (12, N'ViewFTPSites', N'View FTP sites', 11)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (13, N'AddFTPSite', N'Add a new FTP site', 12)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (14, N'EditFTPSite', N'Edit an existing FTP site', 13)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (15, N'ViewACSPartners', N'View ACS partners', 14)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (16, N'AddACSPartner', N'Add a new ACS partner', 15)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (17, N'EditACSPartner', N'Edit an existing ACS partner', 16)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (18, N'ViewCloudServers', N'View cloud servers', 17)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (19, N'ViewAndroAdminLinks', N'View the links to the old Andro Admin websites', 18)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (20, N'ViewUsers', N'View users', 19)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (21, N'AddUser', N'Add a new user', 20)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (22, N'EditUser', N'Edit an existing user', 21)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (23, N'ViewSecurityGroups', N'View security groups', 22)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (24, N'AddSecurityGroup', N'Add a new security group', 23)
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description], [DisplayOrder]) VALUES (25, N'EditSecurityGroup', N'Edit an existing security group', 24)
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
