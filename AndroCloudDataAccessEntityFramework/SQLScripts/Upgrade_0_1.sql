USE [ACS]
GO
/****** Object:  Table [dbo].[ACSLog]    Script Date: 15/10/2012 15:34:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Thread] [nvarchar](255) NULL,
	[Level] [nvarchar](50) NULL,
	[Logger] [nvarchar](255) NULL,
	[Message] [nvarchar](4000) NULL,
	[Exception] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ACSLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACSQueue]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSQueue](
	[ID] [uniqueidentifier] NOT NULL,
	[TimeStamp] [datetime] NULL,
	[SrcSiteID] [int] NULL,
	[SrcSiteGuid] [uniqueidentifier] NULL,
	[DestSiteID] [int] NULL,
	[DestSiteGuid] [uniqueidentifier] NULL,
	[Timeout] [int] NULL,
	[Result] [nvarchar](64) NULL,
	[Replicated] [bit] NULL,
	[dataType] [nvarchar](8) NULL,
	[Packet] [text] NULL,
 CONSTRAINT [PK_Queue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[ID] [uniqueidentifier] NOT NULL,
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
	[Country] [nvarchar](64) NULL,
	[Lat] [float] NULL,
	[Long] [float] NULL,
	[Location] [geography] NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Audit]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[ID] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NULL,
	[SrcID] [nvarchar](64) NULL,
	[HardwareID] [nvarchar](64) NULL,
	[IP_Port] [nvarchar](32) NULL,
	[Action] [nvarchar](64) NULL,
	[ResponseTime] [int] NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Day]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Day](
	[ID] [uniqueidentifier] NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Day] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [uniqueidentifier] NOT NULL,
	[SiteID] [uniqueidentifier] NULL,
	[Firstname] [nvarchar](64) NULL,
	[Surname] [nvarchar](64) NULL,
	[Role] [nvarchar](32) NULL,
	[Phone] [nvarchar](32) NULL,
	[Notes] [nvarchar](256) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [uniqueidentifier] NOT NULL,
	[PartnerID] [uniqueidentifier] NULL,
	[GroupName] [nvarchar](64) NULL,
	[LastUpdated] [datetime] NULL,
	[ExternalId] [nvarchar](64) NULL,
 CONSTRAINT [PK_Chains] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Hosts]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hosts](
	[ID] [uniqueidentifier] NOT NULL,
	[HostName] [nvarchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[Port] [int] NOT NULL,
	[PrivateHostName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Hosts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MyAndromedaUser]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyAndromedaUser](
	[ID] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[GroupID] [uniqueidentifier] NOT NULL,
	[EmployeeID] [uniqueidentifier] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_MyAndromedaUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OpeningHours]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpeningHours](
	[ID] [uniqueidentifier] NOT NULL,
	[SiteID] [uniqueidentifier] NOT NULL,
	[TimeStart] [time](7) NOT NULL,
	[TimeEnd] [time](7) NOT NULL,
	[OpenAllDay] [bit] NOT NULL,
	[DayID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_OpeningHours] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [uniqueidentifier] NOT NULL,
	[SiteID] [uniqueidentifier] NULL,
	[ACSQueueID] [uniqueidentifier] NULL,
	[StatusId] [uniqueidentifier] NULL,
	[InternetOrderNumber] [int] IDENTITY(1,1) NOT NULL,
	[ExternalID] [nvarchar](50) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[ID] [uniqueidentifier] NOT NULL,
	[RamesesStatusId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Partners]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partners](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[ExternalId] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Partners] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SiteMenus]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteMenus](
	[ID] [uniqueidentifier] NOT NULL,
	[SiteID] [uniqueidentifier] NULL,
	[Version] [int] NULL,
	[MenuType] [nvarchar](8) NULL,
	[menuData] [text] NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_SiteMenus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sites]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[ID] [uniqueidentifier] NOT NULL,
	[SignalRConnectionID] [uniqueidentifier] NULL,
	[SessionID] [uniqueidentifier] NULL,
	[AndroSiteName] [nvarchar](64) NOT NULL,
	[ClientSiteName] [nvarchar](64) NULL,
	[ExternalSiteName] [nvarchar](64) NULL,
	[AndroID] [int] NOT NULL,
	[AddressID] [uniqueidentifier] NULL,
	[StoreConnected] [bit] NULL,
	[LastUpdated] [datetime] NULL,
	[ExternalId] [nvarchar](64) NULL,
	[EstimatedDeliveryTime] [int] NULL,
	[TimeZone] [nvarchar](16) NULL,
	[Telephone] [nvarchar](32) NULL,
	[LicenceKey] [nvarchar](64) NULL,
	[StorePaymentProviderID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SitesGroups]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SitesGroups](
	[ID] [uniqueidentifier] NOT NULL,
	[SiteID] [uniqueidentifier] NOT NULL,
	[GroupID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SitesGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StorePaymentProvider]    Script Date: 15/10/2012 15:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorePaymentProvider](
	[ID] [uniqueidentifier] NOT NULL,
	[ProviderName] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[ClientPassword] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PaymentProvider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ACSQueue] ADD  CONSTRAINT [DF_Queue_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [DF_Addresses_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Day] ADD  CONSTRAINT [DF_Day_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Table_1_Id]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Chains_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Hosts] ADD  CONSTRAINT [DF_Hosts_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[MyAndromedaUser] ADD  CONSTRAINT [DF_MyAndromedaUser_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[OpeningHours] ADD  CONSTRAINT [DF_OpeningHours_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[OpeningHours] ADD  CONSTRAINT [DF_OpeningHours_OpenAllDay]  DEFAULT ((0)) FOR [OpenAllDay]
GO
ALTER TABLE [dbo].[OrderStatus] ADD  CONSTRAINT [DF_OrderStatus_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Partners] ADD  CONSTRAINT [DF_Partners_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[SiteMenus] ADD  CONSTRAINT [DF_SiteMenus_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[SiteMenus] ADD  CONSTRAINT [DF_SiteMenus_Version]  DEFAULT ((0)) FOR [Version]
GO
ALTER TABLE [dbo].[SiteMenus] ADD  CONSTRAINT [DF_SiteMenus_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Sites] ADD  CONSTRAINT [DF_Sites_StoreConnected]  DEFAULT ((0)) FOR [StoreConnected]
GO
ALTER TABLE [dbo].[Sites] ADD  CONSTRAINT [DF_Sites_LastUpdated]  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[SitesGroups] ADD  CONSTRAINT [DF_SitesGroups_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[StorePaymentProvider] ADD  CONSTRAINT [DF_StorePaymentProvider_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Sites] FOREIGN KEY([SiteID])
REFERENCES [dbo].[Sites] ([ID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Sites]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Chains_Partners] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partners] ([ID])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Chains_Partners]
GO
ALTER TABLE [dbo].[MyAndromedaUser]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUser_Employees] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([ID])
GO
ALTER TABLE [dbo].[MyAndromedaUser] CHECK CONSTRAINT [FK_MyAndromedaUser_Employees]
GO
ALTER TABLE [dbo].[MyAndromedaUser]  WITH CHECK ADD  CONSTRAINT [FK_MyAndromedaUser_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[MyAndromedaUser] CHECK CONSTRAINT [FK_MyAndromedaUser_Groups]
GO
ALTER TABLE [dbo].[OpeningHours]  WITH CHECK ADD  CONSTRAINT [FK_OpeningHours_Day] FOREIGN KEY([DayID])
REFERENCES [dbo].[Day] ([ID])
GO
ALTER TABLE [dbo].[OpeningHours] CHECK CONSTRAINT [FK_OpeningHours_Day]
GO
ALTER TABLE [dbo].[OpeningHours]  WITH CHECK ADD  CONSTRAINT [FK_OpeningHours_Sites] FOREIGN KEY([SiteID])
REFERENCES [dbo].[Sites] ([ID])
GO
ALTER TABLE [dbo].[OpeningHours] CHECK CONSTRAINT [FK_OpeningHours_Sites]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_ACSQueue] FOREIGN KEY([ACSQueueID])
REFERENCES [dbo].[ACSQueue] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_ACSQueue]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[OrderStatus] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_OrderStatus]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Sites] FOREIGN KEY([SiteID])
REFERENCES [dbo].[Sites] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Sites]
GO
ALTER TABLE [dbo].[SiteMenus]  WITH CHECK ADD  CONSTRAINT [FK_SiteMenus_Sites] FOREIGN KEY([SiteID])
REFERENCES [dbo].[Sites] ([ID])
GO
ALTER TABLE [dbo].[SiteMenus] CHECK CONSTRAINT [FK_SiteMenus_Sites]
GO
ALTER TABLE [dbo].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_Sites_Addresses] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Addresses] ([ID])
GO
ALTER TABLE [dbo].[Sites] CHECK CONSTRAINT [FK_Sites_Addresses]
GO
ALTER TABLE [dbo].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_Sites_PaymentProvider] FOREIGN KEY([StorePaymentProviderID])
REFERENCES [dbo].[StorePaymentProvider] ([ID])
GO
ALTER TABLE [dbo].[Sites] CHECK CONSTRAINT [FK_Sites_PaymentProvider]
GO
ALTER TABLE [dbo].[SitesGroups]  WITH CHECK ADD  CONSTRAINT [FK_SitesGroups_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([ID])
GO
ALTER TABLE [dbo].[SitesGroups] CHECK CONSTRAINT [FK_SitesGroups_Groups]
GO
ALTER TABLE [dbo].[SitesGroups]  WITH CHECK ADD  CONSTRAINT [FK_SitesGroups_Sites] FOREIGN KEY([SiteID])
REFERENCES [dbo].[Sites] ([ID])
GO
ALTER TABLE [dbo].[SitesGroups] CHECK CONSTRAINT [FK_SitesGroups_Sites]
GO
