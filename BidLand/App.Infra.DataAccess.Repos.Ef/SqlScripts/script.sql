USE [MarketPlaceDb]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Province] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Street] [nvarchar](max) NULL,
	[No] [int] NULL,
	[Phone] [int] NULL,
	[PostalCode] [int] NULL,
	[BuyerId] [int] NULL,
	[SellerId] [int] NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteCommissionIncome] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
	[CurrentHighestPrice] [int] NULL,
	[MinimumFinalPrice] [int] NOT NULL,
	[StockId] [int] NOT NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Auctions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bids]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bids](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [int] NULL,
	[BidDate] [datetime2](7) NULL,
	[HasWon] [bit] NULL,
	[AuctionId] [int] NULL,
	[BuyerId] [int] NULL,
 CONSTRAINT [PK_Bids] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booths]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booths](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[SellerId] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Booth__E2D0E1DD5CEB9CEA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buyers]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buyers](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Credit] [int] NULL,
	[TotalPurchaseAmount] [int] NULL,
	[IsBan] [bit] NULL,
	[IsDelete] [bit] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Buyer__4B81C1CA60F39982] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [int] NULL,
	[BuyerId] [int] NULL,
	[PurchaseDate] [datetime2](7) NULL,
	[PurchaseCompeleted] [bit] NOT NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Shopping__7A789A84E74B74AC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Category__19093A2B7D631E80] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[IsPositive] [bit] NULL,
	[Advantages] [nvarchar](max) NULL,
	[DisAdvantages] [nvarchar](max) NULL,
	[IsConfirm] [bit] NULL,
	[Description] [nvarchar](max) NULL,
	[ConfirmDate] [datetime2](7) NULL,
	[StockId] [int] NULL,
	[BuyerId] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Url] [nvarchar](max) NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medals]    Script Date: 11/7/2023 4:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LevelType] [int] NULL,
	[SellerId] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Medals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/7/2023 4:46:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[BasePrice] [int] NULL,
	[IsConfirm] [bit] NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[InsertionDate] [datetime2](7) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK__Product__B40CC6EDE2FD57A1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sellers]    Script Date: 11/7/2023 4:46:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sellers](
	[Id] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsBan] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CommissionPercentage] [float] NULL,
	[CommissionsAmount] [int] NULL,
	[SalesAmount] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK__Seller__7FE3DBA13EC0B8EB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 11/7/2023 4:46:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[BoothId] [int] NULL,
	[Price] [int] NOT NULL,
	[AdditionalDescription] [nvarchar](max) NULL,
	[AvailableNumber] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsAuction] [bit] NOT NULL,
	[InsertionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StocksCarts]    Script Date: 11/7/2023 4:46:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocksCarts](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[StockId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[InsertionDate] [datetime2](7) NULL,
	[CartId1] [int] NOT NULL,
 CONSTRAINT [PK_StocksCarts] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'762e98f6-0fe7-4550-8376-6de394de0144', N'Seller', N'SELLER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f2a19542-a468-4354-aca1-f1fe2a90b24c', N'Admin', N'ADMIN', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ef3044-36f9-47f8-b3ea-bbb512db3c00', N'762e98f6-0fe7-4550-8376-6de394de0144')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3a6aae27-4799-491f-b1bc-be333e2dba0c', N'f2a19542-a468-4354-aca1-f1fe2a90b24c')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3a6aae27-4799-491f-b1bc-be333e2dba0c', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEHDBCKXeja1fNHUIhBsLgjWQ8PzJqX/Uj+5q++442RtJkgN8SXw2Tj0K7qsKwAX3Yw==', N'MW7UBTES6ZYW73ELHLQBCSEVPHN6XCUY', N'f73bb6f6-a4a3-4ade-af75-546166c52d11', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'88ef3044-36f9-47f8-b3ea-bbb512db3c00', N'seller@gmail.com', N'SELLER@GMAIL.COM', N'seller@gmail.com', N'SELLER@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEF5IB1MInBOwKqNYaWArjTLWOorpzqmjYHhbFabZvDjl1DO4MbRaFJRmuGNvFu3yWA==', N'J7LAWJFAAEV5FMAAF3LWHQDOYD2ZR3A2', N'7a0ef5f9-9eda-46db-84d2-d560fb0197de', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'efe22fac-9cf9-4bfc-b1ee-da8c016d3ed5', N'ali@alizadeh.com', N'ALI@ALIZADEH.COM', N'ali@alizadeh.com', N'ALI@ALIZADEH.COM', 1, N'AQAAAAIAAYagAAAAEAtw6TdWlLygKWC6PvIJvwoXxqSWGgRYNiQBkRaW3AEjdzWXhYjI0P4n6XP6TnNTNg==', N'SS5Q5QRC7AGEISKB5Q4CY3PO2YQLPRSH', N'83fd6f1e-1ff6-4723-a2b6-26f14636ace6', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Booths] ON 

INSERT [dbo].[Booths] ([Id], [Name], [Description], [IsDelete], [SellerId], [InsertionDate]) VALUES (1, N'غرفه مشدی حسن', NULL, 0, 1, CAST(N'2023-11-07T11:09:04.6100000' AS DateTime2))
INSERT [dbo].[Booths] ([Id], [Name], [Description], [IsDelete], [SellerId], [InsertionDate]) VALUES (2, N'غرفه گوشی فروشی', NULL, 0, 2, CAST(N'2023-11-07T11:09:04.6100000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Booths] OFF
INSERT [dbo].[Buyers] ([Id], [FullName], [Credit], [TotalPurchaseAmount], [IsBan], [IsDelete], [InsertionDate]) VALUES (1, N'حسن خانی', 10000000, NULL, 0, NULL, CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Buyers] ([Id], [FullName], [Credit], [TotalPurchaseAmount], [IsBan], [IsDelete], [InsertionDate]) VALUES (2, N'فرهاد هاشمی', 5000000, 30000000, 0, NULL, CAST(N'2022-10-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Buyers] ([Id], [FullName], [Credit], [TotalPurchaseAmount], [IsBan], [IsDelete], [InsertionDate]) VALUES (3, N'نوشین عالیان', 1000000, NULL, 0, NULL, CAST(N'2023-06-27T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [Description], [ParentId], [InsertionDate]) VALUES (1, N'لوازم الکترونیک و دیجیتال', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [ParentId], [InsertionDate]) VALUES (2, N'موبایل و تبلت', NULL, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [ParentId], [InsertionDate]) VALUES (3, N'لپتاپ', NULL, 1, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [ParentId], [InsertionDate]) VALUES (4, N'تجهیزات شبکه', NULL, NULL, NULL)
INSERT [dbo].[Categories] ([Id], [Name], [Description], [ParentId], [InsertionDate]) VALUES (5, N'سرور HP', NULL, 4, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (2, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 1, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (3, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 0, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (4, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 0, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (5, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 0, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (6, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 1, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (7, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 0, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (8, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 0, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (9, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 1, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (10, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', 1, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (11, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (12, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (13, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (14, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (15, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (16, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (17, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (18, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (19, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (20, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (21, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (22, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (23, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (24, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (25, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (26, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (27, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (28, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (29, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (30, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (31, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (32, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
INSERT [dbo].[Comments] ([Id], [Title], [IsPositive], [Advantages], [DisAdvantages], [IsConfirm], [Description], [ConfirmDate], [StockId], [BuyerId]) VALUES (33, N'کالا اصل و با قیمت مناسب', 1, N'تحویل سریع و قیمت مناسب', N'بسته بندی شل بود', NULL, N'همکارم یدونه ازین خریده بود و راضی بود منم دوتا برای خودم و خواهرم گرفتم و تا الان که راضیم', CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2), 4, 1)
SET IDENTITY_INSERT [dbo].[Comments] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [BasePrice], [IsConfirm], [Description], [IsActive], [IsDelete], [InsertionDate], [CategoryId]) VALUES (1, N'iphone 11', 20000, 1, NULL, 1, 0, NULL, 2)
INSERT [dbo].[Products] ([Id], [Name], [BasePrice], [IsConfirm], [Description], [IsActive], [IsDelete], [InsertionDate], [CategoryId]) VALUES (2, N'گوشی سامسونگ A30', 10000, 0, NULL, 1, 0, NULL, 2)
INSERT [dbo].[Products] ([Id], [Name], [BasePrice], [IsConfirm], [Description], [IsActive], [IsDelete], [InsertionDate], [CategoryId]) VALUES (3, N'سامسونگ A31', 22000, 0, NULL, 1, 0, CAST(N'2023-11-07T09:38:13.9333333' AS DateTime2), 2)
INSERT [dbo].[Products] ([Id], [Name], [BasePrice], [IsConfirm], [Description], [IsActive], [IsDelete], [InsertionDate], [CategoryId]) VALUES (4, N' نام کالا 1', 15000100, 0, N'شرح کالای 1', 0, 0, NULL, 2)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[Sellers] ([Id], [FullName], [IsActive], [IsBan], [IsDelete], [CommissionPercentage], [CommissionsAmount], [SalesAmount], [InsertionDate]) VALUES (1, N'امیر احمدی', 1, 0, 0, 5, 20000, 10000000, CAST(N'2023-11-07T11:06:59.7700000' AS DateTime2))
INSERT [dbo].[Sellers] ([Id], [FullName], [IsActive], [IsBan], [IsDelete], [CommissionPercentage], [CommissionsAmount], [SalesAmount], [InsertionDate]) VALUES (2, N'حسین رضایی', 1, 0, 0, 4, 40000, 13200000, CAST(N'2023-11-07T11:06:59.7700000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Stocks] ON 

INSERT [dbo].[Stocks] ([Id], [ProductId], [BoothId], [Price], [AdditionalDescription], [AvailableNumber], [IsActive], [IsDelete], [IsAuction], [InsertionDate]) VALUES (4, 1, 1, 50000000, NULL, 5, 1, 0, 0, CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Stocks] ([Id], [ProductId], [BoothId], [Price], [AdditionalDescription], [AvailableNumber], [IsActive], [IsDelete], [IsAuction], [InsertionDate]) VALUES (5, 2, 2, 15000000, NULL, 10, 1, 0, 1, CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Stocks] ([Id], [ProductId], [BoothId], [Price], [AdditionalDescription], [AvailableNumber], [IsActive], [IsDelete], [IsAuction], [InsertionDate]) VALUES (6, 3, 1, 12000000, NULL, 4, 1, 0, 0, CAST(N'2023-10-27T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Stocks] OFF
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Customer_CustomerId] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[Buyers] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Sellers_SellerId] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Sellers] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Sellers_SellerId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Auctions]  WITH CHECK ADD  CONSTRAINT [FK_Auctions_Stocks_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stocks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Auctions] CHECK CONSTRAINT [FK_Auctions_Stocks_StockId]
GO
ALTER TABLE [dbo].[Bids]  WITH CHECK ADD  CONSTRAINT [FK_Bids_Auctions_AuctionId] FOREIGN KEY([AuctionId])
REFERENCES [dbo].[Auctions] ([Id])
GO
ALTER TABLE [dbo].[Bids] CHECK CONSTRAINT [FK_Bids_Auctions_AuctionId]
GO
ALTER TABLE [dbo].[Bids]  WITH CHECK ADD  CONSTRAINT [FK_Bids_Customer_CustomerId] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[Buyers] ([Id])
GO
ALTER TABLE [dbo].[Bids] CHECK CONSTRAINT [FK_Bids_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Booths]  WITH CHECK ADD  CONSTRAINT [FK_Booths_Sellers_SellerId] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Sellers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Booths] CHECK CONSTRAINT [FK_Booths_Sellers_SellerId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[Buyers] ([Id])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Cart_Customer]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Categories] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Categories]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Customer_CustomerId] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[Buyers] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Stocks] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stocks] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Stocks]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Image_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Image_Product]
GO
ALTER TABLE [dbo].[Medals]  WITH CHECK ADD  CONSTRAINT [FK_Medals_Sellers_SellerId] FOREIGN KEY([SellerId])
REFERENCES [dbo].[Sellers] ([Id])
GO
ALTER TABLE [dbo].[Medals] CHECK CONSTRAINT [FK_Medals_Sellers_SellerId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Product_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Product_Categories]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Booths] FOREIGN KEY([BoothId])
REFERENCES [dbo].[Booths] ([Id])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Booths]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Product]
GO
ALTER TABLE [dbo].[StocksCarts]  WITH CHECK ADD  CONSTRAINT [FK_ProductsInCart_ShoppingCart] FOREIGN KEY([CartId1])
REFERENCES [dbo].[Carts] ([Id])
GO
ALTER TABLE [dbo].[StocksCarts] CHECK CONSTRAINT [FK_ProductsInCart_ShoppingCart]
GO
ALTER TABLE [dbo].[StocksCarts]  WITH CHECK ADD  CONSTRAINT [FK_StocksCarts_Stocks] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stocks] ([Id])
GO
ALTER TABLE [dbo].[StocksCarts] CHECK CONSTRAINT [FK_StocksCarts_Stocks]
GO
