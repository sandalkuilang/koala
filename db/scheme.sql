USE [master]
GO
/****** Object:  Database [DigitPrint]    Script Date: 13/07/2016 18.30.06 ******/
CREATE DATABASE [DigitPrint]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DigitPrint', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DigitPrint.mdf' , SIZE = 28672KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DigitPrint_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DigitPrint_log.ldf' , SIZE = 24384KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DigitPrint] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DigitPrint].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DigitPrint] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DigitPrint] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DigitPrint] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DigitPrint] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DigitPrint] SET ARITHABORT OFF 
GO
ALTER DATABASE [DigitPrint] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DigitPrint] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DigitPrint] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DigitPrint] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DigitPrint] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DigitPrint] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DigitPrint] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DigitPrint] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DigitPrint] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DigitPrint] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DigitPrint] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DigitPrint] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DigitPrint] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DigitPrint] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DigitPrint] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DigitPrint] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DigitPrint] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DigitPrint] SET RECOVERY FULL 
GO
ALTER DATABASE [DigitPrint] SET  MULTI_USER 
GO
ALTER DATABASE [DigitPrint] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DigitPrint] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DigitPrint] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DigitPrint] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DigitPrint] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DigitPrint', N'ON'
GO
USE [DigitPrint]
GO
/****** Object:  Table [dbo].[Finishing]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Finishing](
	[Id] [varchar](5) NOT NULL,
	[Description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Finishing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MaterialType]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaterialType](
	[Id] [varchar](5) NOT NULL,
	[QualityId] [varchar](5) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_ItemType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[QualityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [varchar](20) NOT NULL,
	[CustomerName] [varchar](50) NULL,
	[CustomerPhone] [varchar](15) NULL,
	[Status] [varchar](1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[Total] [decimal](18, 0) NOT NULL,
	[Installment] [decimal](18, 0) NULL,
	[Remaining] [decimal](18, 0) NULL,
	[Disc] [decimal](5, 2) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [varchar](20) NOT NULL,
	[SeqNbr] [int] NOT NULL,
	[MaterialTypeId] [varchar](5) NOT NULL,
	[QualityId] [varchar](5) NOT NULL,
	[FinishingId] [varchar](5) NOT NULL,
	[SizeId] [varchar](5) NULL,
	[Title] [varchar](200) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[Filename] [varchar](150) NULL,
	[Image] [varbinary](max) NOT NULL,
	[Queue] [int] NOT NULL,
	[Deadline] [date] NULL,
	[Description] [varchar](max) NULL,
	[Total] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[SeqNbr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quality]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Quality](
	[Id] [varchar](5) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Quality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Size]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Size](
	[Id] [varchar](5) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[Id] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Telp] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Active] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TransactionStock]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TransactionStock](
	[TransId] [int] IDENTITY(1,1) NOT NULL,
	[MaterialId] [varchar](5) NOT NULL,
	[QualityId] [varchar](5) NOT NULL,
	[SupplierId] [varchar](5) NOT NULL,
	[Qty] [int] NOT NULL,
	[Status] [char](3) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](10) NOT NULL,
 CONSTRAINT [PK_TransactionStock_1] PRIMARY KEY CLUSTERED 
(
	[TransId] ASC,
	[MaterialId] ASC,
	[QualityId] ASC,
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserName] [varchar](20) NOT NULL,
	[Name] [varchar](100) NULL,
	[Password] [varchar](50) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 13/07/2016 18.30.06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Finishing] ([Id], [Description]) VALUES (N'9F6C3', N'4 Sisi')
INSERT [dbo].[Finishing] ([Id], [Description]) VALUES (N'CA624', N'Cakar Ayam')
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'6ABF6', N'30E8F', N'KORCIN', CAST(12500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'6ABF6', N'4E97A', N'KORCIN', CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'6ABF6', N'B68D6', N'KORCIN', CAST(15600 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'B45CA', N'4E97A', N'Cilukba_250_gr', CAST(85000 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'EAF2A', N'30E8F', N'Japan', CAST(17500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'EAF2A', N'4E97A', N'Japan', CAST(24500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'EAF2A', N'B68D6', N'Japan', CAST(31500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'ED500', N'30E8F', N'Oblong', CAST(14500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'ED500', N'4E97A', N'Oblong', CAST(21500 AS Decimal(18, 0)))
INSERT [dbo].[MaterialType] ([Id], [QualityId], [Description], [Price]) VALUES (N'ED500', N'B68D6', N'Oblong', CAST(17500 AS Decimal(18, 0)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'1A11237F-45-9', N'asdasd', N'0987654', N'F', CAST(N'2016-04-17 17:52:21.803' AS DateTime), CAST(N'2016-04-17 17:53:51.570' AS DateTime), CAST(215000 AS Decimal(18, 0)), CAST(215000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'210516-001', N'wisnu', N'0812131', N'Q', CAST(N'2016-05-21 13:08:02.477' AS DateTime), CAST(N'2016-06-26 00:50:16.510' AS DateTime), CAST(680000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-680000 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'250616-001', N'Test', N'9876545678', N'Q', CAST(N'2016-06-25 21:48:35.573' AS DateTime), CAST(N'2016-06-26 00:49:48.100' AS DateTime), CAST(344250 AS Decimal(18, 0)), CAST(344250 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'260616-001', N'ASG', N'0812312311', N'Q', CAST(N'2016-06-26 00:53:52.737' AS DateTime), CAST(N'2016-06-26 00:53:59.330' AS DateTime), CAST(85000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-85000 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'260616-002', N'Budi Jaya', N'089786756781', N'Q', CAST(N'2016-06-26 00:55:36.753' AS DateTime), CAST(N'2016-06-26 22:08:03.580' AS DateTime), CAST(5880 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-5880 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'260616-003', N'Podomampir', N'29081283713', N'Q', CAST(N'2016-06-26 00:57:22.790' AS DateTime), CAST(N'2016-06-26 00:57:24.767' AS DateTime), CAST(38250 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-38250 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'260616-004', N'Toko Jaya Indah', N'09876545678', N'Q', CAST(N'2016-06-26 00:58:23.390' AS DateTime), CAST(N'2016-06-26 00:58:25.213' AS DateTime), CAST(20400 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-20400 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'260616-005', N'Budi Jaya', N'08971231', N'I', CAST(N'2016-06-26 16:32:48.387' AS DateTime), NULL, CAST(3000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(-3000 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'280416-002', N'hallo', N'08123123', N'F', CAST(N'2016-04-28 12:11:10.970' AS DateTime), CAST(N'2016-04-28 12:11:24.483' AS DateTime), CAST(23000000 AS Decimal(18, 0)), CAST(23000000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'62B3F2F0-52-1', N'Podomampir', N'081261231', N'Q', CAST(N'2016-04-15 19:16:25.880' AS DateTime), CAST(N'2016-06-26 00:53:28.433' AS DateTime), CAST(1003000 AS Decimal(18, 0)), CAST(500000 AS Decimal(18, 0)), CAST(-503000 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'7F2057FD-B9-A', N'sarung', N'098123123', N'F', CAST(N'2016-05-02 20:29:41.543' AS DateTime), CAST(N'2016-04-17 20:32:54.343' AS DateTime), CAST(1958000 AS Decimal(18, 0)), CAST(1958000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'86EDAB47-C5-2', N'Agung Sedayu', N'08123333177', N'F', CAST(N'2016-04-15 19:15:42.010' AS DateTime), CAST(N'2016-04-15 22:10:59.490' AS DateTime), CAST(5185000 AS Decimal(18, 0)), CAST(5185000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'9250F487-B4-C', N'Mbok Darmi', N'0812312311', N'F', CAST(N'2016-04-15 19:17:07.613' AS DateTime), CAST(N'2016-04-17 21:40:55.613' AS DateTime), CAST(170000 AS Decimal(18, 0)), CAST(170000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'A63C236E-D8-5', N'Budi Jaya', N'081231238712', N'Q', CAST(N'2016-04-16 02:45:43.493' AS DateTime), CAST(N'2016-06-26 00:53:17.810' AS DateTime), CAST(636500 AS Decimal(18, 0)), CAST(100000 AS Decimal(18, 0)), CAST(-536500 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'AA536496-75-F', N'Lazada', N'08123123777', N'F', CAST(N'2016-04-15 21:58:00.657' AS DateTime), CAST(N'2016-04-17 21:40:55.607' AS DateTime), CAST(323000 AS Decimal(18, 0)), CAST(323000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'B7BFC215-C7-6', N'Warung Mbok Jami', N'0123712735', N'F', CAST(N'2016-04-15 22:26:10.417' AS DateTime), CAST(N'2016-04-15 22:26:15.867' AS DateTime), CAST(2450000 AS Decimal(18, 0)), CAST(2450000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[Order] ([OrderId], [CustomerName], [CustomerPhone], [Status], [CreatedDate], [UpdateDate], [Total], [Installment], [Remaining], [Disc]) VALUES (N'E4BDD7D3-3D-D', N'Bakrie Land', N'08127612271', N'F', CAST(N'2016-04-15 19:17:48.697' AS DateTime), CAST(N'2016-04-15 22:10:57.540' AS DateTime), CAST(1700000 AS Decimal(18, 0)), CAST(1700000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'1A11237F-45-9', 1, N'ED500', N'4E97A', N'9F6C3', NULL, N'banner', 340, 440, 10, NULL, 0x, 2, CAST(N'2016-04-05' AS Date), N'', CAST(215000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'210516-001', 1, N'B45CA', N'4E97A', N'9F6C3', NULL, N'spanduk', 200, 200, 1, NULL, 0x, 1, CAST(N'2016-05-21' AS Date), NULL, CAST(340000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'210516-001', 2, N'B45CA', N'4E97A', N'9F6C3', NULL, N'spandauk', 200, 200, 1, NULL, 0x, 1, CAST(N'2016-05-21' AS Date), NULL, CAST(340000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'250616-001', 1, N'B45CA', N'4E97A', N'CA624', NULL, N'jj', 90, 90, 5, NULL, 0x, 1, CAST(N'2016-06-25' AS Date), NULL, CAST(344250 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'260616-001', 1, N'B45CA', N'4E97A', N'9F6C3', NULL, N'Spanduk', 20, 40, 1, NULL, 0x, 1, CAST(N'2016-06-26' AS Date), NULL, CAST(85000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'260616-002', 1, N'EAF2A', N'4E97A', N'9F6C3', NULL, N'Spanduk', 30, 40, 2, NULL, 0x, 1, CAST(N'2016-06-26' AS Date), NULL, CAST(5880 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'260616-003', 1, N'B45CA', N'4E97A', N'9F6C3', NULL, N'Spanduk', 30, 50, 3, NULL, 0x, 1, CAST(N'2016-06-26' AS Date), NULL, CAST(38250 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'260616-004', 1, N'B45CA', N'4E97A', N'CA624', NULL, N'Spanduk', 30, 40, 2, NULL, 0x, 1, CAST(N'2016-06-26' AS Date), NULL, CAST(20400 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'260616-005', 1, N'6ABF6', N'30E8F', N'CA624', NULL, N'Spanduk', 30, 40, 2, NULL, 0x, 0, CAST(N'2016-06-26' AS Date), NULL, CAST(3000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'280416-002', 1, N'B45CA', N'4E97A', N'9F6C3', NULL, N'spanduk', 40, 50, 1000, NULL, 0x, 2, CAST(N'2016-04-30' AS Date), N'laminasi', CAST(23000000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'62B3F2F0-52-1', 1, N'6ABF6', N'4E97A', N'CA624', NULL, N'Blok Pondok Indah ', 200, 60, 2, NULL, 0x, 1, CAST(N'2016-06-30' AS Date), N'', CAST(40800 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'62B3F2F0-52-1', 2, N'6ABF6', N'4E97A', N'CA624', NULL, N'Cilandak', 60, 90, 55, NULL, 0x, 1, CAST(N'2016-04-15' AS Date), N'', CAST(504900 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'7F2057FD-B9-A', 1, N'6ABF6', N'4E97A', N'CA624', NULL, N'spanduk', 30, 50, 100, NULL, 0x, 2, CAST(N'2016-05-02' AS Date), N'', CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'7F2057FD-B9-A', 2, N'ED500', N'4E97A', N'9F6C3', NULL, N'banner', 40, 50, 12, NULL, 0x, 2, CAST(N'2016-05-02' AS Date), N'', CAST(258000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'86EDAB47-C5-2', 1, N'6ABF6', N'4E97A', N'9F6C3', NULL, N'Blok A', 90, 230, 100, NULL, 0x, 2, CAST(N'2016-04-29' AS Date), N'', CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'86EDAB47-C5-2', 2, N'6ABF6', N'4E97A', N'9F6C3', NULL, N'Blok B', 50, 90, 100, NULL, 0x, 2, CAST(N'2016-04-27' AS Date), N'', CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'86EDAB47-C5-2', 3, N'6ABF6', N'4E97A', N'9F6C3', NULL, N'Blok 41', 50, 90, 5, NULL, 0x, 2, CAST(N'2016-05-07' AS Date), N'', CAST(85000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'86EDAB47-C5-2', 4, N'6ABF6', N'4E97A', N'9F6C3', NULL, N'Pantai Marunda', 120, 400, 100, NULL, 0x, 2, CAST(N'2016-05-31' AS Date), N'', CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'9250F487-B4-C', 1, N'6ABF6', N'4E97A', N'CA624', NULL, N'Tenda', 2000, 90, 10, NULL, 0x, 2, CAST(N'2016-05-06' AS Date), N'', CAST(170000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 1, N'EAF2A', N'4E97A', N'CA624', NULL, N'spanduk samping', 100, 30, 10, N'I', 0x, 1, CAST(N'2016-04-16' AS Date), N'', CAST(73500 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 2, N'6ABF6', N'4E97A', N'CA624', NULL, N'banner', 20, 30, 1, N'I', 0x, 1, CAST(N'2016-04-16' AS Date), N'', CAST(17000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 3, N'B45CA', N'4E97A', N'9F6C3', NULL, N'halo', 200, 200, 3, N'I', 0x, 1, CAST(N'2016-04-28' AS Date), N'', CAST(420000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 4, N'B45CA', N'4E97A', N'CA624', NULL, N'asdasd', 30, 40, 1, NULL, 0x, 1, CAST(N'2016-04-28' AS Date), N'', CAST(35000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 5, N'B45CA', N'4E97A', N'9F6C3', NULL, N'fasdf', 200, 40, 2, NULL, 0x, 1, CAST(N'2016-04-28' AS Date), N'', CAST(56000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'A63C236E-D8-5', 6, N'B45CA', N'4E97A', N'CA624', NULL, N'rerrrr', 30, 40, 1, NULL, 0x, 1, CAST(N'2016-04-28' AS Date), N'', CAST(35000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'AA536496-75-F', 1, N'6ABF6', N'4E97A', N'9F6C3', NULL, N'spanduk', 30, 30, 19, NULL, 0x, 2, CAST(N'2016-04-15' AS Date), N'', CAST(323000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'B7BFC215-C7-6', 1, N'EAF2A', N'4E97A', N'9F6C3', NULL, N'spanduk', 30, 40, 100, NULL, 0x, 2, CAST(N'2016-04-15' AS Date), N'', CAST(2450000 AS Decimal(18, 0)))
INSERT [dbo].[OrderDetail] ([OrderId], [SeqNbr], [MaterialTypeId], [QualityId], [FinishingId], [SizeId], [Title], [Width], [Height], [Qty], [Filename], [Image], [Queue], [Deadline], [Description], [Total]) VALUES (N'E4BDD7D3-3D-D', 1, N'6ABF6', N'4E97A', N'CA624', NULL, N'Flag', 90, 120, 100, NULL, 0x, 2, CAST(N'2016-05-31' AS Date), N'', CAST(1700000 AS Decimal(18, 0)))
INSERT [dbo].[Quality] ([Id], [Description]) VALUES (N'30E8F', N'Standart')
INSERT [dbo].[Quality] ([Id], [Description]) VALUES (N'4E97A', N'HiRez')
INSERT [dbo].[Quality] ([Id], [Description]) VALUES (N'B68D6', N'Medium')
INSERT [dbo].[Supplier] ([Id], [Name], [Telp], [Address], [Active], [CreatedDate]) VALUES (N'SP312', N'AHASS', N'081234543', N'Bekasi', 1, CAST(N'2016-05-03 11:46:42.370' AS DateTime))
INSERT [dbo].[Supplier] ([Id], [Name], [Telp], [Address], [Active], [CreatedDate]) VALUES (N'SP5B2', N'TMMIN', N'08123765412', N'Kungingan, Jakarta Selatan', 1, CAST(N'2016-05-03 11:46:29.447' AS DateTime))
SET IDENTITY_INSERT [dbo].[TransactionStock] ON 

INSERT [dbo].[TransactionStock] ([TransId], [MaterialId], [QualityId], [SupplierId], [Qty], [Status], [CreatedDate], [CreatedBy]) VALUES (1, N'6ABF6', N'30E8F', N'SP312', 2, N'IN ', CAST(N'2016-06-25 00:00:00.000' AS DateTime), N'Admin')
INSERT [dbo].[TransactionStock] ([TransId], [MaterialId], [QualityId], [SupplierId], [Qty], [Status], [CreatedDate], [CreatedBy]) VALUES (2, N'6ABF6', N'30E8F', N'SP312', 6, N'IN ', CAST(N'2016-06-25 00:00:00.000' AS DateTime), N'Admin')
INSERT [dbo].[TransactionStock] ([TransId], [MaterialId], [QualityId], [SupplierId], [Qty], [Status], [CreatedDate], [CreatedBy]) VALUES (3, N'6ABF6', N'30E8F', N'SP312', -1, N'OUT', CAST(N'2016-06-25 00:00:00.000' AS DateTime), N'Admin')
SET IDENTITY_INSERT [dbo].[TransactionStock] OFF
INSERT [dbo].[User] ([UserName], [Name], [Password], [Type]) VALUES (N'admin', N'Admin', N'admin', 900)
INSERT [dbo].[User] ([UserName], [Name], [Password], [Type]) VALUES (N'test', N'Hendra Yudha P', N'test', 100)
INSERT [dbo].[UserType] ([Id], [Description]) VALUES (100, N'Cashier')
INSERT [dbo].[UserType] ([Id], [Description]) VALUES (900, N'Admin')
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
USE [master]
GO
ALTER DATABASE [DigitPrint] SET  READ_WRITE 
GO
