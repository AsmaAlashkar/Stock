USE [master]
GO
/****** Object:  Database [Stock]    Script Date: 9/29/2024 12:12:46 PM ******/
CREATE DATABASE [Stock]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Stock', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Stock.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Stock_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Stock_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Stock] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Stock].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Stock] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Stock] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Stock] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Stock] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Stock] SET ARITHABORT OFF 
GO
ALTER DATABASE [Stock] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Stock] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Stock] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Stock] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Stock] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Stock] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Stock] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Stock] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Stock] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Stock] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Stock] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Stock] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Stock] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Stock] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Stock] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Stock] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Stock] SET  MULTI_USER 
GO
ALTER DATABASE [Stock] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Stock] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Stock] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Stock] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Stock] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Stock] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Stock] SET QUERY_STORE = ON
GO
ALTER DATABASE [Stock] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Stock]
GO
/****** Object:  Table [dbo].[MainWearhouse]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainWearhouse](
	[Main_ID] [int] IDENTITY(1,1) NOT NULL,
	[Main_Name] [nvarchar](50) NOT NULL,
	[Main_Description] [nvarchar](50) NULL,
	[Main_Adderess] [nvarchar](max) NULL,
	[Main_Createdat] [datetime] NULL,
	[Main_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_MainWearhouse] PRIMARY KEY CLUSTERED 
(
	[Main_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_Name] [nvarchar](50) NOT NULL,
	[Cat_FK] [int] NOT NULL,
	[Unite_FK] [int] NULL,
	[Sub_FK] [int] NULL,
	[Item_Experationdate] [datetime] NULL,
	[Item_Createdat] [datetime] NULL,
	[Item_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubWearhouse]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubWearhouse](
	[Sub_ID] [int] IDENTITY(1,1) NOT NULL,
	[Main_FK] [int] NOT NULL,
	[ParentSubWearhouseId] [int] NULL,
	[Sub_Name] [nvarchar](50) NOT NULL,
	[Sub_Description] [nvarchar](max) NULL,
	[Sub_Address] [nvarchar](max) NULL,
	[Sub_Createdat] [datetime] NULL,
	[Sub_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_SubWearhouse] PRIMARY KEY CLUSTERED 
(
	[Sub_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_WearhouseItem]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_WearhouseItem] AS
select Main_ID,Main_Name,Main_Description,Main_Adderess,Main_Createdat,Main_Updatedat,
Sub_ID,Sub_Name,Sub_Address,Sub_Createdat,Sub_Updatedat,
Item_ID,Item_Name,Item_Experationdate,MainWearhouse.Delet as MD,SubWearhouse.Delet as SD, Items.Delet as ID
from MainWearhouse
left join SubWearhouse
on Main_ID = Main_FK
And SubWearhouse.Delet = 0
left join Items
on Sub_ID = Sub_FK
And SubWearhouse.Delet = 0
where   MainWearhouse.Delet = 0
AND Items.Delet IS NULL or Items.Delet = 0





GO
/****** Object:  View [dbo].[View_MainWearhouseWithSubWearhouseHierarchy]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_MainWearhouseWithSubWearhouseHierarchy] AS
WITH SubWarehouseHierarchy AS (
    -- Base level (Sub-warehouses directly under the Main warehouse)
    SELECT 
        Sub_ID,
        Main_FK,
        ParentSubWearhouseId,  -- Added ParentSubWearhouseId
        Sub_Name,
        Sub_Address,
        Sub_Description,       -- Added Sub_Description
        Sub_Createdat,
        Sub_Updatedat,
        1 AS [Level],    -- Level 1 for base level
        Delet
    FROM 
        dbo.SubWearhouse
    WHERE 
        ParentSubWearhouseId IS NULL 
        AND Delet = 0
    
    UNION ALL
    
    -- Recursively find sub-warehouses under other sub-warehouses
    SELECT 
        sw.Sub_ID,
        sw.Main_FK,
        sw.ParentSubWearhouseId,  -- Added ParentSubWearhouseId
        sw.Sub_Name,
        sw.Sub_Address,
        sw.Sub_Description,       -- Added Sub_Description
        sw.Sub_Createdat,
        sw.Sub_Updatedat,
        sh.[Level] + 1 AS [Level],   -- Increment Level
        sw.Delet
    FROM 
        dbo.SubWearhouse sw
    INNER JOIN 
        SubWarehouseHierarchy sh ON sw.ParentSubWearhouseId = sh.Sub_ID
    WHERE 
        sw.Delet = 0
)

-- Main query to incorporate the CTE
SELECT 
    mw.Main_ID, 
    mw.Main_Name, 
    mw.Main_Description, 
    mw.Main_Adderess, 
    mw.Main_Createdat, 
    mw.Main_Updatedat, 
    sh.Sub_ID, 
    sh.Sub_Name, 
    sh.Sub_Address, 
    sh.Sub_Description,       -- Added Sub_Description
    sh.Sub_Createdat, 
    sh.Sub_Updatedat, 
    sh.ParentSubWearhouseId,  -- Added ParentSubWearhouseId
    sh.[Level],  
    i.Item_ID, 
    i.Item_Name, 
    i.Item_Experationdate, 
    mw.Delet AS MD, 
    sh.Delet AS SD, 
    i.Delet AS ID
FROM 
    dbo.MainWearhouse mw
LEFT OUTER JOIN 
    SubWarehouseHierarchy sh ON mw.Main_ID = sh.Main_FK
LEFT OUTER JOIN 
    dbo.Items i ON sh.Sub_ID = i.Sub_FK AND i.Delet = 0
WHERE 
    mw.Delet = 0 
    AND (i.Delet IS NULL OR i.Delet = 0)
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Cat_ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentCategoryId] [int] NULL,
	[Cat_NameAr] [nvarchar](50) NOT NULL,
	[Cat_NameEn] [nvarchar](50) NULL,
	[Cat_DesAr] [nvarchar](max) NULL,
	[Cat_DesEn] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Cat_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSuppliers]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemSuppliers](
	[ItemSuppliers_ID] [int] IDENTITY(1,1) NOT NULL,
	[Items_FK] [int] NULL,
	[Suppliers_Fk] [int] NULL,
 CONSTRAINT [PK_ItemSuppliers] PRIMARY KEY CLUSTERED 
(
	[ItemSuppliers_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[Stock_ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_FK] [int] NOT NULL,
	[CurrentStockLevel] [float] NULL,
	[Stock_Updatedat] [datetime] NULL,
	[Stock_Createdat] [datetime] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[Stock_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Supplier_ID] [int] IDENTITY(1,1) NOT NULL,
	[Suppliers_Name] [nvarchar](50) NOT NULL,
	[ContactPeraon] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Suppliar_Addreaa] [nvarchar](max) NULL,
	[Delet] [bit] NULL,
	[Sup_Createdat] [datetime] NULL,
	[Sup_Updatedat] [datetime] NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Supplier_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 9/29/2024 12:12:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Unit_ID] [int] IDENTITY(1,1) NOT NULL,
	[Unit_Name] [nvarchar](50) NULL,
	[Unit_Desc] [nvarchar](max) NULL,
	[Unit_CreatedAt] [datetime] NULL,
	[Unit_UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Unite] PRIMARY KEY CLUSTERED 
(
	[Unit_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (1, NULL, N'أدوية', N'Medications', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (2, NULL, N'معدات الوقاية الشخصية', N'PPE', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (3, NULL, N'المعدات الطبية', N'Medical Equip', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (4, NULL, N'المواد الاستهلاكية', N'Consumables', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (5, NULL, N'اللقاحات', N'Vaccines', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (7, NULL, N'رعاية المرضى', N'Patient Care', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[MainWearhouse] ON 

INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_Name], [Main_Description], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (1, N'testupdate', N'testupdate', N'testupdate', CAST(N'2024-02-04T00:00:00.000' AS DateTime), CAST(N'2024-09-11T16:02:50.763' AS DateTime), 0)
INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_Name], [Main_Description], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (2, N'testupdate5', N'Test update 2', N'مدينة نصر', CAST(N'2024-02-04T00:00:00.000' AS DateTime), CAST(N'2024-09-03T14:24:32.440' AS DateTime), 0)
INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_Name], [Main_Description], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (3, N'El-Mostakbal ware house', N'Main ware house for El Mostakbal company', N'El-Manhal', CAST(N'2024-09-01T14:22:20.430' AS DateTime), CAST(N'2024-09-23T10:43:02.453' AS DateTime), 0)
INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_Name], [Main_Description], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (31, N'اختبار', N'اختبار', N'القاهره', CAST(N'2024-09-17T14:37:35.353' AS DateTime), CAST(N'2024-09-23T13:04:01.040' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[MainWearhouse] OFF
GO
SET IDENTITY_INSERT [dbo].[SubWearhouse] ON 

INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (1, 2, NULL, N'test3', N'test', N'test', CAST(N'2024-09-03T15:30:24.320' AS DateTime), CAST(N'2024-09-03T15:34:25.037' AS DateTime), 1)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (3, 3, NULL, N'2اختبار', N'2اختبار', N'2اختبار', CAST(N'2024-09-03T15:31:46.700' AS DateTime), CAST(N'2024-09-03T15:33:24.760' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (7, 3, 3, N'El mostakbal warehouse', N'the main ware house', N'el manhal', CAST(N'2024-09-13T00:00:00.000' AS DateTime), CAST(N'2024-09-23T09:33:51.783' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (8, 2, NULL, N'kjhsdfjsk', N'jkshfj', N'skjl', CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (9, 3, 7, N'test3', N'sdfsdfsd', N'dfgdfgdfgdfgdfg', CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (10, 3, 9, N'iiiii', N'iiiiii', N'iiiiiii', CAST(N'2024-09-16T00:00:00.000' AS DateTime), CAST(N'2024-09-18T13:18:18.943' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (11, 2, 8, N'kjhsdfjsk', N'jkshfj', N'skjl', CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (12, 3, NULL, N'iiii', N'pppp', N'll', CAST(N'2024-09-03T15:31:46.700' AS DateTime), CAST(N'2024-09-18T13:18:03.303' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (13, 3, 12, N'Mostafa', N'jkshfj', N'skjl', CAST(N'2024-09-16T00:00:00.000' AS DateTime), CAST(N'2024-09-18T13:07:35.967' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (14, 3, 10, N'swagger', N'ghfdghdg', N'fdghdfghdfh', CAST(N'2024-09-18T14:33:33.913' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (15, 3, 7, N'swagger', N'ghfdghdg', N'fdghdfghdfh', CAST(N'2024-09-18T14:35:10.553' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (16, 3, 7, N'fdgg', N'ghfdghdg', N'fdghdfghdfh', CAST(N'2024-09-18T14:37:22.617' AS DateTime), CAST(N'2024-09-18T14:37:42.570' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_Name], [Sub_Description], [Sub_Address], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (30, 3, 14, N'2222222222', N'222222222', N'22222222', CAST(N'2024-09-23T13:19:19.063' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[SubWearhouse] OFF
GO
SET IDENTITY_INSERT [dbo].[Unit] ON 

INSERT [dbo].[Unit] ([Unit_ID], [Unit_Name], [Unit_Desc], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (1, N'Tabs', NULL, NULL, NULL)
INSERT [dbo].[Unit] ([Unit_ID], [Unit_Name], [Unit_Desc], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (2, N'miligram', NULL, NULL, NULL)
INSERT [dbo].[Unit] ([Unit_ID], [Unit_Name], [Unit_Desc], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (3, N'test', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Unit] OFF
GO
/****** Object:  Index [idx_SubWearhouse_ParentDelet]    Script Date: 9/29/2024 12:12:46 PM ******/
CREATE NONCLUSTERED INDEX [idx_SubWearhouse_ParentDelet] ON [dbo].[SubWearhouse]
(
	[ParentSubWearhouseId] ASC,
	[Delet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Category] ([Cat_ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Category] FOREIGN KEY([Cat_FK])
REFERENCES [dbo].[Category] ([Cat_ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Category]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_SubWearhouse] FOREIGN KEY([Sub_FK])
REFERENCES [dbo].[SubWearhouse] ([Sub_ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_SubWearhouse]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Unite] FOREIGN KEY([Unite_FK])
REFERENCES [dbo].[Unit] ([Unit_ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Unite]
GO
ALTER TABLE [dbo].[ItemSuppliers]  WITH CHECK ADD  CONSTRAINT [FK_ItemSuppliers_Items] FOREIGN KEY([Items_FK])
REFERENCES [dbo].[Items] ([Item_ID])
GO
ALTER TABLE [dbo].[ItemSuppliers] CHECK CONSTRAINT [FK_ItemSuppliers_Items]
GO
ALTER TABLE [dbo].[ItemSuppliers]  WITH CHECK ADD  CONSTRAINT [FK_ItemSuppliers_Suppliers] FOREIGN KEY([Suppliers_Fk])
REFERENCES [dbo].[Suppliers] ([Supplier_ID])
GO
ALTER TABLE [dbo].[ItemSuppliers] CHECK CONSTRAINT [FK_ItemSuppliers_Suppliers]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Items] FOREIGN KEY([Item_FK])
REFERENCES [dbo].[Items] ([Item_ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Items]
GO
ALTER TABLE [dbo].[SubWearhouse]  WITH CHECK ADD  CONSTRAINT [FK_SubWearhouse_MainWearhouse] FOREIGN KEY([Main_FK])
REFERENCES [dbo].[MainWearhouse] ([Main_ID])
GO
ALTER TABLE [dbo].[SubWearhouse] CHECK CONSTRAINT [FK_SubWearhouse_MainWearhouse]
GO
ALTER TABLE [dbo].[SubWearhouse]  WITH CHECK ADD  CONSTRAINT [FK_SubWearhouse_SubWearhouse] FOREIGN KEY([ParentSubWearhouseId])
REFERENCES [dbo].[SubWearhouse] ([Sub_ID])
GO
ALTER TABLE [dbo].[SubWearhouse] CHECK CONSTRAINT [FK_SubWearhouse_SubWearhouse]
GO
USE [master]
GO
ALTER DATABASE [Stock] SET  READ_WRITE 
GO
