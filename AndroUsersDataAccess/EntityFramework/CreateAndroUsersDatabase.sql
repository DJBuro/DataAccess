/****** Object:  Table [dbo].[Permission]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SecurityGroup]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_SecurityGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SecurityGroupPermission]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityGroupPermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SecurityGroupId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SecurityGroupUser]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityGroupUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SecurityGroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_SecurityGroupUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_AndroUser]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_AndroUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](10) NOT NULL,
	[SurName] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_tbl_AndroUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AndroUserPermission]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AndroUserPermission](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AndroUser] [int] NOT NULL,
	[Project] [int] NOT NULL,
 CONSTRAINT [PK_tbl_AndroUserPermission] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Project]    Script Date: 07/02/2013 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Project](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Url] [varchar](200) NOT NULL,
 CONSTRAINT [PK_tbl_Project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbl_AndroUser] ADD  CONSTRAINT [DF_tbl_AndroUser_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[SecurityGroupPermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroupPermission_Permission] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([Id])
GO
ALTER TABLE [dbo].[SecurityGroupPermission] CHECK CONSTRAINT [FK_SecurityGroupPermission_Permission]
GO
ALTER TABLE [dbo].[SecurityGroupPermission]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroupPermission_SecurityGroup] FOREIGN KEY([SecurityGroupId])
REFERENCES [dbo].[SecurityGroup] ([Id])
GO
ALTER TABLE [dbo].[SecurityGroupPermission] CHECK CONSTRAINT [FK_SecurityGroupPermission_SecurityGroup]
GO
ALTER TABLE [dbo].[SecurityGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroupUser_SecurityGroup] FOREIGN KEY([SecurityGroupId])
REFERENCES [dbo].[SecurityGroup] ([Id])
GO
ALTER TABLE [dbo].[SecurityGroupUser] CHECK CONSTRAINT [FK_SecurityGroupUser_SecurityGroup]
GO
ALTER TABLE [dbo].[SecurityGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_SecurityGroupUser_tbl_AndroUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tbl_AndroUser] ([id])
GO
ALTER TABLE [dbo].[SecurityGroupUser] CHECK CONSTRAINT [FK_SecurityGroupUser_tbl_AndroUser]
GO
ALTER TABLE [dbo].[tbl_AndroUserPermission]  WITH CHECK ADD  CONSTRAINT [FK_tbl_AndroUserPermission_tbl_AndroUser] FOREIGN KEY([AndroUser])
REFERENCES [dbo].[tbl_AndroUser] ([id])
GO
ALTER TABLE [dbo].[tbl_AndroUserPermission] CHECK CONSTRAINT [FK_tbl_AndroUserPermission_tbl_AndroUser]
GO
ALTER TABLE [dbo].[tbl_AndroUserPermission]  WITH CHECK ADD  CONSTRAINT [FK_tbl_AndroUserPermission_tbl_Project] FOREIGN KEY([Project])
REFERENCES [dbo].[tbl_Project] ([id])
GO
ALTER TABLE [dbo].[tbl_AndroUserPermission] CHECK CONSTRAINT [FK_tbl_AndroUserPermission_tbl_Project]
GO

GO
SET IDENTITY_INSERT [dbo].[Permission] ON 

GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (2, N'ViewStores', N'View store details')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (3, N'AddStore', N'Add a new store')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (4, N'EditStore', N'Edit an existing store')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (5, N'ViewPaymentProviders', N'View payment providers')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (6, N'AddPaymentProvider', N'Add a new payment provider')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (7, N'ViewAMSStores', N'View AMS stores')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (8, N'EditAMSStore', N'Edit AMS stores')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (9, N'ViewAMSServers', N'View AMS servers')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (10, N'AddAMSServer', N'Add a new AMS server')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (11, N'EditAMSServer', N'Edit an existing AMS server')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (12, N'ViewFTPSites', N'View FTP sites')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (13, N'AddFTPSite', N'Add a new FTP site')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (14, N'EditFTPSite', N'Edit an existing FTP site')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (15, N'ViewACSPartners', N'View ACS partners')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (16, N'AddACSPartner', N'Add a new ACS partner')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (17, N'EditACSPartner', N'Edit an existing ACS partner')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (18, N'ViewCloudServers', N'View cloud servers')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (19, N'ViewAndroAdminLinks', N'View the links to the old Andro Admin websites')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (20, N'ViewUsers', N'View users')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (21, N'AddUser', N'Add a new user')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (22, N'EditUser', N'Edit an existing user')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (23, N'ViewSecurityGroups', N'View security groups')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (24, N'AddSecurityGroup', N'Add a new security group')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (25, N'EditSecurityGroup', N'Edit an existing security group')
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO