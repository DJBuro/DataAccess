/****** Object:  Table [dbo].[ACSApplication]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSApplication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalApplicationId] [nvarchar](40) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PartnerId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_ACSApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACSApplicationSite]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSApplicationSite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[ACSApplicationId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_ACSApplicationSite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Address]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Org1] [nvarchar](64) NULL,
	[Org2] [nvarchar](64) NULL,
	[Org3] [nvarchar](64) NULL,
	[Prem1] [nvarchar](64) NULL,
	[Prem2] [nvarchar](64) NULL,
	[Prem3] [nvarchar](64) NULL,
	[Prem4] [nvarchar](64) NULL,
	[Prem5] [nvarchar](64) NULL,
	[Prem6] [nvarchar](64) NULL,
	[RoadNum] [nvarchar](16) NULL,
	[RoadName] [nvarchar](64) NULL,
	[Locality] [nvarchar](64) NULL,
	[Town] [nvarchar](64) NULL,
	[County] [nvarchar](64) NULL,
	[State] [nvarchar](64) NULL,
	[PostCode] [nvarchar](16) NULL,
	[DPS] [nvarchar](4) NULL,
	[Lat] [nvarchar](15) NULL,
	[Long] [nvarchar](15) NULL,
	[Location] [geography] NULL,
	[DataVersion] [int] NOT NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AMSServer]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AMSServer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](3500) NOT NULL,
 CONSTRAINT [PK_AMSServer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](64) NOT NULL,
	[ISO3166_1_alpha_2] [varchar](2) NOT NULL,
	[ISO3166_1_numeric] [int] NOT NULL,
 CONSTRAINT [PK_CountryCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CountryCode] UNIQUE NONCLUSTERED 
(
	[ISO3166_1_alpha_2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CountryCode_1] UNIQUE NONCLUSTERED 
(
	[ISO3166_1_numeric] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CountryCode_2] UNIQUE NONCLUSTERED 
(
	[CountryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Day]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Day](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Day] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NULL,
	[Firstname] [nvarchar](64) NULL,
	[Surname] [nvarchar](64) NULL,
	[Role] [nvarchar](32) NULL,
	[Phone] [nvarchar](32) NULL,
	[Notes] [nvarchar](256) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FTPSite]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FTPSite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[Port] [int] NOT NULL,
	[ServerType] [nvarchar](50) NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[FTPSiteType_Id] [int] NOT NULL,
 CONSTRAINT [PK_FTPSite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FTPSiteType]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FTPSiteType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FTPServerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Group]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartnerId] [int] NULL,
	[GroupName] [nvarchar](64) NULL,
	[LastUpdated] [datetime] NULL,
	[ExternalId] [nvarchar](64) NULL,
 CONSTRAINT [PK_Groups_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Host]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Host](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HostName] [nvarchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[PrivateHostName] [nvarchar](255) NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_Host] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [nvarchar](50) NULL,
	[Severity] [nvarchar](50) NULL,
	[Message] [nvarchar](3500) NULL,
	[Method] [nvarchar](200) NULL,
	[Source] [nvarchar](50) NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MyAndromedaUser]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyAndromedaUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AndromedaUser_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MyAndromedaUserGroup]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyAndromedaUserGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MyAndromedaUserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_MyAndromedaUserGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MyAndromedaUserStore]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyAndromedaUserStore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MyAndromedaUserId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_MyAndromedaUserStore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OpeningHour]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningHour](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[TimeStart] [time](7) NOT NULL,
	[TimeEnd] [time](7) NOT NULL,
	[OpenAllDay] [bit] NOT NULL,
	[DayId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_OpeningHour] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partner]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[ExternalId] [nvarchar](64) NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Store]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[AndromedaSiteId] [int] NOT NULL,
	[CustomerSiteId] [nvarchar](50) NULL,
	[LastFTPUploadDateTime] [datetime] NULL,
	[StoreStatusId] [int] NOT NULL,
	[ClientSiteName] [nvarchar](64) NOT NULL,
	[ExternalSiteName] [nvarchar](64) NOT NULL,
	[ExternalId] [nvarchar](64) NOT NULL,
	[EstimatedDeliveryTime] [int] NULL,
	[TimeZone] [nvarchar](16) NULL,
	[Telephone] [nvarchar](32) NULL,
	[LicenseKey] [nvarchar](64) NULL,
	[AddressId] [int] NULL,
	[StorePaymentProviderID] [int] NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAMSServer]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAMSServer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[AMSServerId] [int] NOT NULL,
 CONSTRAINT [PK_StoreAMSServer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAMSServerFtpSite]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAMSServerFtpSite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreAMSServerId] [int] NOT NULL,
	[FTPSiteId] [int] NOT NULL,
 CONSTRAINT [PK_StoreAMSServerFtpSite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAMSServerFTPSitePair]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAMSServerFTPSitePair](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[AMSServerId] [int] NOT NULL,
	[PrimaryFTPSiteId] [int] NULL,
	[SecondaryFTPSiteId] [int] NULL,
 CONSTRAINT [PK_AMSServerFTPServerPair] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreMenu]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [int] NULL,
	[Version] [int] NULL,
	[MenuType] [nvarchar](8) NULL,
	[MenuData] [text] NULL,
	[LastUpdated] [datetime] NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_StoreMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StorePaymentProvider]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorePaymentProvider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[ClientPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PaymentProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreStatus]    Script Date: 13/11/2012 10:36:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](25) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_StoreStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_Address_DataVersion]  DEFAULT ((0)) FOR [DataVersion]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Groups_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Log] ADD  CONSTRAINT [DF_Log_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[OpeningHour] ADD  CONSTRAINT [DF_OpeningHour_OpenAllDay]  DEFAULT ((0)) FOR [OpenAllDay]
GO
ALTER TABLE [dbo].[StoreMenu] ADD  CONSTRAINT [DF_StoreMenu_Version]  DEFAULT ((0)) FOR [Version]
GO
ALTER TABLE [dbo].[StoreMenu] ADD  CONSTRAINT [DF_StoreMenu_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[ACSApplication]  WITH CHECK ADD  CONSTRAINT [FK_ACSApplication_Partner] FOREIGN KEY([PartnerId])
REFERENCES [dbo].[Partner] ([Id])
GO
ALTER TABLE [dbo].[ACSApplication] CHECK CONSTRAINT [FK_ACSApplication_Partner]
GO
ALTER TABLE [dbo].[ACSApplicationSite]  WITH CHECK ADD  CONSTRAINT [FK_ACSApplicationSite_ACSApplication] FOREIGN KEY([ACSApplicationId])
REFERENCES [dbo].[ACSApplication] ([Id])
GO
ALTER TABLE [dbo].[ACSApplicationSite] CHECK CONSTRAINT [FK_ACSApplicationSite_ACSApplication]
GO
ALTER TABLE [dbo].[ACSApplicationSite]  WITH CHECK ADD  CONSTRAINT [FK_ACSApplicationSite_Store] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[ACSApplicationSite] CHECK CONSTRAINT [FK_ACSApplicationSite_Store]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Address] FOREIGN KEY([Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Address]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Country]
GO
ALTER TABLE [dbo].[FTPSite]  WITH CHECK ADD  CONSTRAINT [FK_FTPSite_FTPSiteType] FOREIGN KEY([FTPSiteType_Id])
REFERENCES [dbo].[FTPSiteType] ([Id])
GO
ALTER TABLE [dbo].[FTPSite] CHECK CONSTRAINT [FK_FTPSite_FTPSiteType]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Partner] FOREIGN KEY([PartnerId])
REFERENCES [dbo].[Partner] ([Id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Partner]
GO
ALTER TABLE [dbo].[MyAndromedaUserGroup]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUserGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[MyAndromedaUserGroup] CHECK CONSTRAINT [FK_MyAndromedaUserGroup_Group]
GO
ALTER TABLE [dbo].[MyAndromedaUserGroup]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUserGroup_MyAndromedaUser] FOREIGN KEY([MyAndromedaUserId])
REFERENCES [dbo].[MyAndromedaUser] ([Id])
GO
ALTER TABLE [dbo].[MyAndromedaUserGroup] CHECK CONSTRAINT [FK_MyAndromedaUserGroup_MyAndromedaUser]
GO
ALTER TABLE [dbo].[MyAndromedaUserStore]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUserStore_MyAndromedaUser] FOREIGN KEY([MyAndromedaUserId])
REFERENCES [dbo].[MyAndromedaUser] ([Id])
GO
ALTER TABLE [dbo].[MyAndromedaUserStore] CHECK CONSTRAINT [FK_MyAndromedaUserStore_MyAndromedaUser]
GO
ALTER TABLE [dbo].[MyAndromedaUserStore]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUserStore_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[MyAndromedaUserStore] CHECK CONSTRAINT [FK_MyAndromedaUserStore_Store]
GO
ALTER TABLE [dbo].[OpeningHour]  WITH CHECK ADD  CONSTRAINT [FK_OpeningHour_Day] FOREIGN KEY([DayId])
REFERENCES [dbo].[Day] ([Id])
GO
ALTER TABLE [dbo].[OpeningHour] CHECK CONSTRAINT [FK_OpeningHour_Day]
GO
ALTER TABLE [dbo].[OpeningHour]  WITH CHECK ADD  CONSTRAINT [FK_OpeningHour_Store] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[OpeningHour] CHECK CONSTRAINT [FK_OpeningHour_Store]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_Address]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_StorePaymentProvider] FOREIGN KEY([StorePaymentProviderID])
REFERENCES [dbo].[StorePaymentProvider] ([Id])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_StorePaymentProvider]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_StoreStatus] FOREIGN KEY([StoreStatusId])
REFERENCES [dbo].[StoreStatus] ([Id])
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [FK_Store_StoreStatus]
GO
ALTER TABLE [dbo].[StoreAMSServer]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServer_AMSServer] FOREIGN KEY([AMSServerId])
REFERENCES [dbo].[AMSServer] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServer] CHECK CONSTRAINT [FK_StoreAMSServer_AMSServer]
GO
ALTER TABLE [dbo].[StoreAMSServer]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServer_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServer] CHECK CONSTRAINT [FK_StoreAMSServer_Store]
GO
ALTER TABLE [dbo].[StoreAMSServerFtpSite]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFtpSite_FTPSite] FOREIGN KEY([FTPSiteId])
REFERENCES [dbo].[FTPSite] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFtpSite] CHECK CONSTRAINT [FK_StoreAMSServerFtpSite_FTPSite]
GO
ALTER TABLE [dbo].[StoreAMSServerFtpSite]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFtpSite_StoreAMSServer] FOREIGN KEY([StoreAMSServerId])
REFERENCES [dbo].[StoreAMSServer] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFtpSite] CHECK CONSTRAINT [FK_StoreAMSServerFtpSite_StoreAMSServer]
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFTPServerPair_AMSServer] FOREIGN KEY([AMSServerId])
REFERENCES [dbo].[AMSServer] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair] CHECK CONSTRAINT [FK_StoreAMSServerFTPServerPair_AMSServer]
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFTPServerPair_FTPSite] FOREIGN KEY([PrimaryFTPSiteId])
REFERENCES [dbo].[FTPSite] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair] CHECK CONSTRAINT [FK_StoreAMSServerFTPServerPair_FTPSite]
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFTPServerPair_FTPSite1] FOREIGN KEY([SecondaryFTPSiteId])
REFERENCES [dbo].[FTPSite] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair] CHECK CONSTRAINT [FK_StoreAMSServerFTPServerPair_FTPSite1]
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair]  WITH CHECK ADD  CONSTRAINT [FK_StoreAMSServerFTPServerPair_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[StoreAMSServerFTPSitePair] CHECK CONSTRAINT [FK_StoreAMSServerFTPServerPair_Store]
GO
ALTER TABLE [dbo].[StoreMenu]  WITH CHECK ADD  CONSTRAINT [FK_StoreMenu_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[StoreMenu] CHECK CONSTRAINT [FK_StoreMenu_Store]
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD  CONSTRAINT [CK_Store] CHECK  ((datalength([ExternalId])>(0)))
GO
ALTER TABLE [dbo].[Store] CHECK CONSTRAINT [CK_Store]
GO


USE [AndroAdmin]
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (1, N'Afghanistan', N'AF', 4)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (2, N'Åland Islands', N'AX', 248)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (3, N'Albania', N'AL', 8)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (4, N'Algeria', N'DZ', 12)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (5, N'American Samoa', N'AS', 16)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (6, N'Andorra', N'AO', 20)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (7, N'Angola', N'AI', 24)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (8, N'Anguilla', N'AQ', 660)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (9, N'Antarctica', N'AG', 10)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (10, N'Antigua and Barbuda', N'AR', 28)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (11, N'Argentina', N'AM', 32)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (12, N'Armenia', N'AW', 51)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (13, N'Australia', N'AU', 36)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (14, N'Austria', N'AT', 40)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (15, N'Azerbaijan', N'AZ', 31)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (16, N'Bahamas', N'BS', 44)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (17, N'Bahrain', N'BH', 48)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (18, N'Bangladesh', N'BD', 50)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (19, N'Barbados', N'BB', 52)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (20, N'Belarus', N'BY', 112)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (21, N'Belgium', N'BE', 56)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (22, N'Belize', N'BZ', 84)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (23, N'Benin', N'BJ', 204)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (24, N'Bermuda', N'BM', 60)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (25, N'Bhutan', N'BT', 64)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (26, N'Bolivia, Plurinational State of', N'BO', 68)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (27, N'Bonaire, Sint Eustatius and Saba', N'BQ', 535)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (28, N'Bosnia and Herzegovina', N'BA', 70)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (29, N'Botswana', N'BW', 72)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (30, N'Bouvet Island', N'BV', 74)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (31, N'Brazil', N'BR', 76)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (32, N'British Indian Ocean Territory', N'IO', 86)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (33, N'Brunei Darussalam', N'BN', 96)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (34, N'Bulgaria', N'BG', 100)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (35, N'Burkina Faso', N'BF', 854)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (36, N'Burundi', N'BI', 108)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (37, N'Cambodia', N'KH', 116)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (38, N'Cameroon', N'CM', 120)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (39, N'Canada', N'CA', 124)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (40, N'Cape Verde', N'CV', 132)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (41, N'Cayman Islands', N'KY', 136)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (42, N'Central African Republic', N'CF', 140)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (43, N'Chad', N'TD', 148)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (44, N'Chile', N'CL', 152)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (45, N'China', N'CN', 156)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (46, N'Christmas Island', N'CX', 162)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (47, N'Cocos (Keeling) Islands', N'CC', 166)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (48, N'Colombia', N'CO', 170)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (49, N'Comoros', N'KM', 174)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (50, N'Congo', N'CG', 178)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (51, N'Congo, the Democratic Republic of the', N'CD', 180)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (52, N'Cook Islands', N'CK', 184)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (53, N'Costa Rica', N'CR', 188)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (54, N'Côte d''Ivoire', N'CI', 384)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (55, N'Croatia', N'HR', 191)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (56, N'Cuba', N'CU', 192)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (57, N'Curaçao', N'CW', 531)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (58, N'Cyprus', N'CY', 196)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (59, N'Czech Republic', N'CZ', 203)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (60, N'Denmark', N'DK', 208)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (61, N'Djibouti', N'DJ', 262)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (62, N'Dominica', N'DM', 212)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (63, N'Dominican Republic', N'DO', 214)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (64, N'Ecuador', N'EC', 218)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (65, N'Egypt', N'EG', 818)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (66, N'El Salvador', N'SV', 222)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (67, N'Equatorial Guinea', N'GQ', 226)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (68, N'Eritrea', N'ER', 232)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (69, N'Estonia', N'EE', 233)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (70, N'Ethiopia', N'ET', 231)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (71, N'Falkland Islands (Malvinas)', N'FK', 238)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (72, N'Faroe Islands', N'FO', 234)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (73, N'Fiji', N'FJ', 242)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (74, N'Finland', N'FI', 246)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (75, N'France', N'FR', 250)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (76, N'French Guiana', N'GF', 254)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (77, N'French Polynesia', N'PF', 258)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (78, N'French Southern Territories', N'TF', 260)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (79, N'Gabon', N'GA', 266)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (80, N'Gambia', N'GM', 270)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (81, N'Georgia', N'GE', 268)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (82, N'Germany', N'DE', 276)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (83, N'Ghana', N'GH', 288)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (84, N'Gibraltar', N'GI', 292)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (85, N'Greece', N'GR', 300)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (86, N'Greenland', N'GL', 304)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (87, N'Grenada', N'GD', 308)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (88, N'Guadeloupe', N'GP', 312)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (89, N'Guam', N'GU', 316)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (90, N'Guatemala', N'GT', 320)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (91, N'Guernsey', N'GG', 831)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (92, N'Guinea', N'GN', 324)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (93, N'Guinea-Bissau', N'GW', 624)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (94, N'Guyana', N'GY', 328)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (95, N'Haiti', N'HT', 332)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (96, N'Heard Island and McDonald Islands', N'HM', 334)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (97, N'Holy See (Vatican City State)', N'VA', 336)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (98, N'Honduras', N'HN', 340)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (99, N'Hong Kong', N'HK', 344)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (100, N'Hungary', N'HU', 348)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (101, N'Iceland', N'IS', 352)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (102, N'India', N'IN', 356)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (103, N'Indonesia', N'ID', 360)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (104, N'Iran, Islamic Republic of', N'IR', 364)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (105, N'Iraq', N'IQ', 368)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (106, N'Ireland', N'IE', 372)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (107, N'Isle of Man', N'IM', 833)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (108, N'Israel', N'IL', 376)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (109, N'Italy', N'IT', 380)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (110, N'Jamaica', N'JM', 388)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (111, N'Japan', N'JP', 392)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (112, N'Jersey', N'JE', 832)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (113, N'Jordan', N'JO', 400)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (114, N'Kazakhstan', N'KZ', 398)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (115, N'Kenya', N'KE', 404)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (116, N'Kiribati', N'KI', 296)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (117, N'Korea, Democratic People''s Republic of', N'KP', 408)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (118, N'Korea, Republic of', N'KR', 410)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (119, N'Kuwait', N'KW', 414)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (120, N'Kyrgyzstan', N'KG', 417)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (121, N'Lao People''s Democratic Republic', N'LA', 418)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (122, N'Latvia', N'LV', 428)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (123, N'Lebanon', N'LB', 422)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (124, N'Lesotho', N'LS', 426)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (125, N'Liberia', N'LR', 430)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (126, N'Libya', N'LY', 434)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (127, N'Liechtenstein', N'LI', 438)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (128, N'Lithuania', N'LT', 440)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (129, N'Luxembourg', N'LU', 442)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (130, N'Macao', N'MO', 446)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (131, N'Macedonia, the former Yugoslav Republic of', N'MK', 807)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (132, N'Madagascar', N'MG', 450)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (133, N'Malawi', N'MW', 454)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (134, N'Malaysia', N'MY', 458)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (135, N'Maldives', N'MV', 462)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (136, N'Mali', N'ML', 466)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (137, N'Malta', N'MT', 470)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (138, N'Marshall Islands', N'MH', 584)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (139, N'Martinique', N'MQ', 474)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (140, N'Mauritania', N'MR', 478)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (141, N'Mauritius', N'MU', 480)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (142, N'Mayotte', N'YT', 175)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (143, N'Mexico', N'MX', 484)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (144, N'Micronesia, Federated States of', N'FM', 583)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (145, N'Moldova, Republic of', N'MD', 498)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (146, N'Monaco', N'MC', 492)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (147, N'Mongolia', N'MN', 496)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (148, N'Montenegro', N'ME', 499)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (149, N'Montserrat', N'MS', 500)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (150, N'Morocco', N'MA', 504)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (151, N'Mozambique', N'MZ', 508)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (152, N'Myanmar', N'MM', 104)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (153, N'Namibia', N'NA', 516)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (154, N'Nauru', N'NR', 520)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (155, N'Nepal', N'NP', 524)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (156, N'Netherlands', N'NL', 528)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (157, N'New Caledonia', N'NC', 540)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (158, N'New Zealand', N'NZ', 554)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (159, N'Nicaragua', N'NI', 558)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (160, N'Niger', N'NE', 562)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (161, N'Nigeria', N'NG', 566)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (162, N'Niue', N'NU', 570)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (163, N'Norfolk Island', N'NF', 574)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (164, N'Northern Mariana Islands', N'MP', 580)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (165, N'Norway', N'NO', 578)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (166, N'Oman', N'OM', 512)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (167, N'Pakistan', N'PK', 586)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (168, N'Palau', N'PW', 585)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (169, N'Palestinian Territory, Occupied', N'PS', 275)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (170, N'Panama', N'PA', 591)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (171, N'Papua New Guinea', N'PG', 598)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (172, N'Paraguay', N'PY', 600)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (173, N'Peru', N'PE', 604)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (174, N'Philippines', N'PH', 608)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (175, N'Pitcairn', N'PN', 612)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (176, N'Poland', N'PL', 616)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (177, N'Portugal', N'PT', 620)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (178, N'Puerto Rico', N'PR', 630)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (179, N'Qatar', N'QA', 634)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (180, N'Réunion', N'RE', 638)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (181, N'Romania', N'RO', 642)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (182, N'Russian Federation', N'RU', 643)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (183, N'Rwanda', N'RW', 646)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (184, N'Saint Barthélemy', N'BL', 652)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (185, N'Saint Helena, Ascension and Tristan da Cunha', N'SH', 654)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (186, N'Saint Kitts and Nevis', N'KN', 659)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (187, N'Saint Lucia', N'LC', 662)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (188, N'Saint Martin (French part)', N'MF', 663)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (189, N'Saint Pierre and Miquelon', N'PM', 666)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (190, N'Saint Vincent and the Grenadines', N'VC', 670)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (191, N'Samoa', N'WS', 882)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (192, N'San Marino', N'SM', 674)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (193, N'Sao Tome and Principe', N'ST', 678)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (194, N'Saudi Arabia', N'SA', 682)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (195, N'Senegal', N'SN', 686)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (196, N'Serbia', N'RS', 688)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (197, N'Seychelles', N'SC', 690)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (198, N'Sierra Leone', N'SL', 694)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (199, N'Singapore', N'SG', 702)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (200, N'Sint Maarten (Dutch part)', N'SX', 534)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (201, N'Slovakia', N'SK', 703)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (202, N'Slovenia', N'SI', 705)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (203, N'Solomon Islands', N'SB', 90)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (204, N'Somalia', N'SO', 706)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (205, N'South Africa', N'ZA', 710)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (206, N'South Georgia and the South Sandwich Islands', N'GS', 239)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (207, N'South Sudan', N'SS', 728)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (208, N'Spain', N'ES', 724)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (209, N'Sri Lanka', N'LK', 144)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (210, N'Sudan', N'SD', 729)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (211, N'Suriname', N'SR', 740)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (212, N'Svalbard and Jan Mayen', N'SJ', 744)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (213, N'Swaziland', N'SZ', 748)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (214, N'Sweden', N'SE', 752)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (215, N'Switzerland', N'CH', 756)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (216, N'Syrian Arab Republic', N'SY', 760)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (217, N'Taiwan, Province of China', N'TW', 158)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (218, N'Tajikistan', N'TJ', 762)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (219, N'Tanzania, United Republic of', N'TZ', 834)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (220, N'Thailand', N'TH', 764)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (221, N'Timor-Leste', N'TL', 626)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (222, N'Togo', N'TG', 768)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (223, N'Tokelau', N'TK', 772)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (224, N'Tonga', N'TO', 776)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (225, N'Trinidad and Tobago', N'TT', 780)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (226, N'Tunisia', N'TN', 788)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (227, N'Turkey', N'TR', 792)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (228, N'Turkmenistan', N'TM', 795)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (229, N'Turks and Caicos Islands', N'TC', 796)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (230, N'Tuvalu', N'TV', 798)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (231, N'Uganda', N'UG', 800)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (232, N'Ukraine', N'UA', 804)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (233, N'United Arab Emirates', N'AE', 784)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (234, N'United Kingdom', N'GB', 826)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (235, N'United States', N'US', 840)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (236, N'United States Minor Outlying Islands', N'UM', 581)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (237, N'Uruguay', N'UY', 858)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (238, N'Uzbekistan', N'UZ', 860)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (239, N'Vanuatu', N'VU', 548)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (240, N'Venezuela, Bolivarian Republic of', N'VE', 862)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (241, N'Viet Nam', N'VN', 704)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (242, N'Virgin Islands, British', N'VG', 92)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (243, N'Virgin Islands, U.S.', N'VI', 850)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (244, N'Wallis and Futuna', N'WF', 876)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (245, N'Western Sahara', N'EH', 732)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (246, N'Yemen', N'YE', 887)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (247, N'Zambia', N'ZM', 894)
GO
INSERT [dbo].[Country] ([Id], [CountryName], [ISO3166_1_alpha_2], [ISO3166_1_numeric]) VALUES (248, N'Zimbabwe', N'ZW', 716)
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Day] ON 

GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (1, N'Monday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (2, N'Tuesday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (3, N'Wednesday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (4, N'Thursday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (5, N'Friday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (6, N'Saturday')
GO
INSERT [dbo].[Day] ([Id], [Description]) VALUES (7, N'Sunday')
GO
SET IDENTITY_INSERT [dbo].[Day] OFF
GO
SET IDENTITY_INSERT [dbo].[StoreStatus] ON 

GO
INSERT [dbo].[StoreStatus] ([Id], [Status], [Description]) VALUES (1, N'Disabled', N'Store is disabled either temporarily or permanently')
GO
INSERT [dbo].[StoreStatus] ([Id], [Status], [Description]) VALUES (2, N'Future', N'Store has not opened yet')
GO
INSERT [dbo].[StoreStatus] ([Id], [Status], [Description]) VALUES (3, N'Live', N'Store is trading')
GO
SET IDENTITY_INSERT [dbo].[StoreStatus] OFF
GO

INSERT [dbo].[Settings] ([Name], [Value]) VALUES (N'DataVersion', 0)
GO
