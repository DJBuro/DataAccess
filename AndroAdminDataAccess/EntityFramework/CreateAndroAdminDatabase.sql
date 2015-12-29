
/****** Object:  Table [dbo].[ACSApplication]    Script Date: 06/02/2013 14:28:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSApplication](
	[Id] [int] IDENTITY(1000000105,1) NOT NULL,
	[ExternalApplicationId] [nvarchar](40) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ExternalDisplayName] [nvarchar](50) NOT NULL,
	[PartnerId] [int] NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_ACSApplication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACSApplicationSite]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 06/02/2013 14:28:54 ******/
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
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AMSServer]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Country]    Script Date: 06/02/2013 14:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] NOT NULL,
	[CountryName] [nvarchar](64) NOT NULL,
	[ISO3166_1_alpha_2] [varchar](2) NOT NULL,
	[ISO3166_1_numeric] [int] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Day]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[FTPSite]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[FTPSiteType]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Group]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Host]    Script Date: 06/02/2013 14:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Host](
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Log]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[MyAndromedaUser]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[MyAndromedaUserGroup]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[MyAndromedaUserStore]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[OpeningHour]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Partner]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Settings]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[Store]    Script Date: 06/02/2013 14:28:54 ******/
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
	[AddressId] [int] NOT NULL,
	[StorePaymentProviderID] [int] NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Store] UNIQUE NONCLUSTERED 
(
	[AndromedaSiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreAMSServer]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[StoreAMSServerFtpSite]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[StoreGroup]    Script Date: 06/02/2013 14:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreGroup](
	[Id] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_StoreGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreMenu]    Script Date: 06/02/2013 14:28:54 ******/
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
/****** Object:  Table [dbo].[StorePaymentProvider]    Script Date: 06/02/2013 14:28:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorePaymentProvider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProviderName] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[ClientPassword] [nvarchar](50) NOT NULL,
	[DisplayText] [nvarchar](50) NOT NULL,
	[DataVersion] [int] NOT NULL,
 CONSTRAINT [PK_PaymentProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreStatus]    Script Date: 06/02/2013 14:28:54 ******/
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
ALTER TABLE [dbo].[StoreGroup]  WITH CHECK ADD  CONSTRAINT [FK_StoreGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[StoreGroup] CHECK CONSTRAINT [FK_StoreGroup_Group]
GO
ALTER TABLE [dbo].[StoreGroup]  WITH CHECK ADD  CONSTRAINT [FK_StoreGroup_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[StoreGroup] CHECK CONSTRAINT [FK_StoreGroup_Store]
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
