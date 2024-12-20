USE [master]
GO
/****** Object:  Database [Stock]    Script Date: 12/3/2024 9:14:28 PM ******/
CREATE DATABASE [Stock]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Stock_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Stock.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Stock_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Stock.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Stock] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [Stock] SET QUERY_STORE = ON
GO
ALTER DATABASE [Stock] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Stock]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/3/2024 9:14:28 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CategoriesHirarichy]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CategoriesHirarichy] AS 
WITH CategoryHierarchy AS (
    -- Anchor member: selects root categories (categories with no parent)
    SELECT
        Cat_ID,
        ParentCategoryId,
        Cat_NameAr,
        Cat_NameEn,
        Cat_DesAr,
        Cat_DesEn,
        0 AS Level
    FROM Category
    WHERE ParentCategoryId IS NULL
    
    UNION ALL
    
    -- Recursive member: selects children categories and increments the level
    SELECT
        c.Cat_ID,
        c.ParentCategoryId,
        c.Cat_NameAr,
        c.Cat_NameEn,
        c.Cat_DesAr,
        c.Cat_DesEn,
        ch.Level + 1 AS Level
    FROM Category c
    INNER JOIN CategoryHierarchy ch ON c.ParentCategoryId = ch.Cat_ID
)
-- Final SELECT without ORDER BY in the CTE
SELECT
    Cat_ID,
    ParentCategoryId,
    Cat_NameAr,
    Cat_NameEn,
    Cat_DesAr,
    Cat_DesEn,
    Level
FROM CategoryHierarchy
GO
/****** Object:  Table [dbo].[Items]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_Code] [nvarchar](50) NOT NULL,
	[Item_NameEn] [nvarchar](50) NOT NULL,
	[Item_NameAr] [nvarchar](50) NOT NULL,
	[Cat_FK] [int] NOT NULL,
	[Unite_FK] [int] NULL,
	[Item_Experationdate] [datetime] NULL,
	[Item_Createdat] [datetime] NULL,
	[Item_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainWearhouse]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainWearhouse](
	[Main_ID] [int] IDENTITY(1,1) NOT NULL,
	[Main_NameEn] [nvarchar](50) NOT NULL,
	[Main_NameAr] [nvarchar](50) NOT NULL,
	[Main_DescriptionEn] [nvarchar](max) NULL,
	[Main_DescriptionAr] [nvarchar](max) NULL,
	[Main_Adderess] [nvarchar](max) NULL,
	[Main_Createdat] [datetime] NULL,
	[Main_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_MainWearhouse] PRIMARY KEY CLUSTERED 
(
	[Main_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubItem]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Item_FK] [int] NOT NULL,
	[Sub_FK] [int] NULL,
	[Quantity] [float] NULL,
 CONSTRAINT [PK_SubItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubWearhouse]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubWearhouse](
	[Sub_ID] [int] IDENTITY(1,1) NOT NULL,
	[Main_FK] [int] NOT NULL,
	[ParentSubWearhouseId] [int] NULL,
	[Sub_NameEn] [nvarchar](50) NOT NULL,
	[Sub_NameAr] [nvarchar](50) NOT NULL,
	[Sub_DescriptionEn] [nvarchar](max) NULL,
	[Sub_DescriptionAr] [nvarchar](max) NULL,
	[Sub_AddressEn] [nvarchar](max) NULL,
	[Sub_AddressAr] [nvarchar](max) NULL,
	[Sub_Createdat] [datetime] NULL,
	[Sub_Updatedat] [datetime] NULL,
	[Delet] [bit] NULL,
 CONSTRAINT [PK_SubWearhouse] PRIMARY KEY CLUSTERED 
(
	[Sub_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_MainWearhouseWithSubWearhouseHierarchy]    Script Date: 12/3/2024 9:14:28 PM ******/
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
        ParentSubWearhouseId,
        Sub_NameEn,
        Sub_NameAr,
        Sub_AddressEn,
        Sub_AddressAr,
        Sub_DescriptionEn,
        Sub_DescriptionAr,
        Sub_Createdat,
        Sub_Updatedat,
        1 AS Level,
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
        sw.ParentSubWearhouseId,
        sw.Sub_NameEn,
        sw.Sub_NameAr,
        sw.Sub_AddressEn,
        sw.Sub_AddressAr,
        sw.Sub_DescriptionEn, -- Include this column
        sw.Sub_DescriptionAr, -- Include this column
        sw.Sub_Createdat,
        sw.Sub_Updatedat,
        sh.Level + 1 AS Level,
        sw.Delet
    FROM 
        dbo.SubWearhouse sw
    INNER JOIN 
        SubWarehouseHierarchy sh ON sw.ParentSubWearhouseId = sh.Sub_ID
    WHERE 
        sw.Delet = 0
)

-- Now incorporate this CTE into the main query
SELECT 
    mw.Main_ID, 
    mw.Main_NameEn, 
    mw.Main_NameAr,
    mw.Main_DescriptionEn, 
    mw.Main_DescriptionAr, 
    mw.Main_Adderess, 
    mw.Main_Createdat, 
    mw.Main_Updatedat, 
    sh.Sub_ID, 
    sh.Sub_NameEn, 
    sh.Sub_NameAr, 
    sh.Sub_AddressEn, 
    sh.Sub_AddressAr, 
    sh.Sub_Createdat, 
    sh.Sub_Updatedat, 
    sh.Sub_DescriptionEn,
    sh.Sub_DescriptionAr,
    sh.ParentSubWearhouseId,
    sh.Level,  -- Include Level here
    si.Item_FK AS Item_ID, 
    i.Item_NameEn, 
    i.Item_NameAr, 
    i.Item_Experationdate, 
    mw.Delet AS MD, 
    sh.Delet AS SD, 
    i.Delet AS ID
FROM 
    dbo.MainWearhouse mw
LEFT OUTER JOIN 
    SubWarehouseHierarchy sh ON mw.Main_ID = sh.Main_FK
LEFT OUTER JOIN 
    dbo.SubItem si ON sh.Sub_ID = si.Sub_FK
LEFT OUTER JOIN 
    dbo.Items i ON si.Item_FK = i.Item_ID AND i.Delet = 0
WHERE 
    mw.Delet = 0 
    AND (i.Delet IS NULL OR i.Delet = 0);
GO
/****** Object:  Table [dbo].[CodeFormat]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodeFormat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Format] [nvarchar](6) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CodeFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemPermission]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPermission](
	[ItemPer_ID] [int] IDENTITY(1,1) NOT NULL,
	[Perm_FK] [int] NOT NULL,
	[Item_FK] [int] NOT NULL,
	[Quantity] [float] NOT NULL,
 CONSTRAINT [PK_SubItemPermission] PRIMARY KEY CLUSTERED 
(
	[ItemPer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemSuppliers]    Script Date: 12/3/2024 9:14:28 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[Perm_ID] [int] IDENTITY(1,1) NOT NULL,
	[PermCode] [nvarchar](10) NOT NULL,
	[PermType_FK] [int] NULL,
	[Perm_Createdat] [datetime] NULL,
	[Sub_FK] [int] NOT NULL,
	[DestinationSubFk] [int] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Perm_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionType]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionType](
	[Per_ID] [int] IDENTITY(1,1) NOT NULL,
	[PerTypeValueAr] [nvarchar](50) NOT NULL,
	[PerTypeValueEn] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PermissionType] PRIMARY KEY CLUSTERED 
(
	[Per_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quantity]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quantity](
	[Quantity_ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_FK] [int] NOT NULL,
	[CurrentQuantity] [float] NULL,
	[Quantity_Updatedat] [datetime] NULL,
	[Quantity_Createdat] [datetime] NULL,
 CONSTRAINT [PK_Quantity_1] PRIMARY KEY CLUSTERED 
(
	[Quantity_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 12/3/2024 9:14:28 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 12/3/2024 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Unit_ID] [int] IDENTITY(1,1) NOT NULL,
	[Unit_NameEn] [nvarchar](50) NOT NULL,
	[Unit_NameAr] [nvarchar](50) NOT NULL,
	[Unit_DescEn] [nvarchar](max) NULL,
	[Unit_DescAr] [nvarchar](max) NULL,
	[Unit_CreatedAt] [datetime] NULL,
	[Unit_UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Unite] PRIMARY KEY CLUSTERED 
(
	[Unit_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (1, NULL, N'-mjfgmgأدوية', N'Medications', N'gfdg', N'sdfgsdfg')
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (2, 1, N'معدات الوقاية الشخصية', N'PPE', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (3, 2, N'المعدات الطبية', N'Medical Equip', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (4, 1, N'المواد الاستهلاكية', N'Consumables', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (5, 3, N'اللقاحات', N'Vaccines', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (7, NULL, N'رعاية المرضى', N'Patient Care', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (8, 1, N'مصطفى ', N'Mostafa', NULL, NULL)
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (10, NULL, N'5trt', N'ertert', N'ertre', N'ert')
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (13, NULL, N'tttt', N'tttt', N'tttt', N'tttt')
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (22, 2, N'pppp', N'pppppp', N'ppp', N'pppppp')
INSERT [dbo].[Category] ([Cat_ID], [ParentCategoryId], [Cat_NameAr], [Cat_NameEn], [Cat_DesAr], [Cat_DesEn]) VALUES (23, 7, N'iiiii', N'iiiiiiii', N'iiiiii', N'iiiiii')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[CodeFormat] ON 

INSERT [dbo].[CodeFormat] ([Id], [Format], [CreatedDate], [IsActive]) VALUES (1, N'string', CAST(N'2024-11-04T13:03:33.970' AS DateTime), 1)
INSERT [dbo].[CodeFormat] ([Id], [Format], [CreatedDate], [IsActive]) VALUES (2, N'FRN', CAST(N'2024-11-04T13:03:33.970' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[CodeFormat] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemPermission] ON 

INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (18, 66, 1, 250)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (19, 67, 1, 150)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (20, 69, 1, 100)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (21, 71, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (22, 72, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (23, 73, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (24, 74, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (25, 75, 1, 200)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (26, 76, 1, 200)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (27, 78, 1, 200)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (28, 79, 1, 200)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (29, 84, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (30, 85, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (31, 88, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (32, 89, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (33, 90, 18, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (34, 91, 18, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (35, 92, 18, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (36, 93, 18, 20)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (37, 94, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (38, 94, 2, 20)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (39, 94, 3, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (40, 98, 1, 50)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (41, 100, 1, 50)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (42, 101, 1, 20)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (43, 102, 1, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (44, 103, 1, 100)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (45, 106, 1, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (46, 107, 1, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (47, 108, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (48, 109, 1, 70)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (49, 112, 1, 70)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (50, 112, 2, 20)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (51, 112, 18, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (52, 115, 12, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (53, 116, 12, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (54, 119, 1, 40)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (55, 120, 1, 5)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (56, 122, 1, 12)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (57, 124, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (58, 125, 1, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (59, 126, 1, 20)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (60, 126, 3, 10)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (61, 126, 20, 30)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (62, 126, 21, 50)
INSERT [dbo].[ItemPermission] ([ItemPer_ID], [Perm_FK], [Item_FK], [Quantity]) VALUES (63, 127, 2, 30)
SET IDENTITY_INSERT [dbo].[ItemPermission] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (1, N'123', N'mostafa', N'مصطفى', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (2, N'3sq', N'Asmaa', N'اسماء', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (3, N'qqq', N'Mostafa2', N'مصطفى2', 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (12, N'40', N'medo', N'ميدو', 2, 3, CAST(N'2024-10-13T13:57:11.997' AS DateTime), CAST(N'2024-10-13T13:57:11.997' AS DateTime), CAST(N'2024-10-13T13:57:11.997' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (13, N'40', N'medo', N'ميدو', 2, 3, CAST(N'2024-10-13T13:57:11.997' AS DateTime), CAST(N'2024-10-13T13:57:11.997' AS DateTime), CAST(N'2024-10-13T13:57:11.997' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (14, N'10', N'string', N'نص', 1, 1, NULL, CAST(N'2024-10-14T11:18:08.487' AS DateTime), NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (15, N'60', N'Manal', N'منال', 1, 1, NULL, CAST(N'2024-10-14T11:24:52.573' AS DateTime), NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (16, N'10', N'mostafa2', N'مصطفى2', 1, 1, NULL, CAST(N'2024-10-14T11:48:57.690' AS DateTime), NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (17, N'100', N'mona', N'منى', 1, 1, NULL, CAST(N'2024-10-14T11:49:44.047' AS DateTime), NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (18, N'string', N'string', N'نص', 8, 3, CAST(N'2025-01-16T10:01:38.780' AS DateTime), CAST(N'2024-10-16T10:01:38.780' AS DateTime), CAST(N'2024-10-16T10:01:38.780' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (19, N'new', N'new', N'جديد', 8, 3, CAST(N'2025-01-16T10:01:38.780' AS DateTime), CAST(N'2024-09-16T10:01:38.780' AS DateTime), CAST(N'2024-10-16T10:01:38.780' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (20, N'SKU', N'Mostafa', N'مصطفى', 1, 1, CAST(N'2024-10-20T08:25:40.893' AS DateTime), CAST(N'2024-10-17T13:44:54.760' AS DateTime), CAST(N'2024-10-17T13:44:54.760' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (21, N'KKK', N'Mostafa', N'مصطفى', 7, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (22, N'RRR', N'Manal', N'منال', 2, 1, CAST(N'2026-10-21T00:00:00.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (24, N'y', N'Manal222', N'منال222', 23, 4, CAST(N'2028-10-11T00:00:00.000' AS DateTime), CAST(N'2024-10-21T12:19:10.690' AS DateTime), CAST(N'2024-10-21T12:19:10.690' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (25, N'22', N'Manal', N'منال', 5, 2, CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(N'2024-10-21T12:26:31.540' AS DateTime), CAST(N'2024-10-21T12:26:31.540' AS DateTime), NULL)
INSERT [dbo].[Items] ([Item_ID], [Item_Code], [Item_NameEn], [Item_NameAr], [Cat_FK], [Unite_FK], [Item_Experationdate], [Item_Createdat], [Item_Updatedat], [Delet]) VALUES (26, N'RRR', N'Manal', N'منال', 2, 3, CAST(N'2026-06-21T00:00:00.000' AS DateTime), CAST(N'2024-10-21T12:39:23.290' AS DateTime), CAST(N'2024-10-21T12:39:23.290' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[MainWearhouse] ON 

INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_NameEn], [Main_NameAr], [Main_DescriptionEn], [Main_DescriptionAr], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (1, N'test update', N'تحديث الاختبار', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.', N'لوريم إيبسوم هو ببساطة نص وهمي من صناعة الطباعة والتنضيد. لقد كان لوريم إيبسوم هو النص الوهمي القياسي في هذه الصناعة منذ القرن السادس عشر، عندما أخذت طابعة غير معروفة لوح الكتابة وخلطته لصنع نموذج كتاب. لقد صمدت ليس فقط لخمسة قرون، بل قفزت أيضًا إلى التنضيد الإلكتروني، وبقيت دون تغيير بشكل أساسي. انتشر بشكل كبير في ستينيات القرن الماضي مع إصدار أوراق Letraset التي تحتوي على مقاطع لوريم إيبسوم، ومؤخراً مع ظهور برامج النشر المكتبي مثل Aldus PageMaker والتي تضمنت إصدارات لوريم إيبسوم.', N'testupdate', CAST(N'2024-02-04T00:00:00.000' AS DateTime), CAST(N'2024-09-11T16:02:50.763' AS DateTime), 0)
INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_NameEn], [Main_NameAr], [Main_DescriptionEn], [Main_DescriptionAr], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (2, N'test update5', N'تحديث الاختبار5', N'The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from "de Finibus Bonorum et Malorum" by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.

', N'الجزء القياسي من نص لوريم إيبسوم المستخدم منذ القرن السادس عشر مكرر أدناه للمهتمين. تم أيضًا إعادة إنتاج الأقسام 1.10.32 و1.10.33 من كتاب "de Finibus Bonorum et Malorum" لشيشرون في شكلها الأصلي تمامًا، مصحوبة بنسخ إنجليزية من ترجمة عام 1914 بواسطة H. Rackham.', N'مدينة نصر', CAST(N'2024-02-04T00:00:00.000' AS DateTime), CAST(N'2024-09-03T14:24:32.440' AS DateTime), 0)
INSERT [dbo].[MainWearhouse] ([Main_ID], [Main_NameEn], [Main_NameAr], [Main_DescriptionEn], [Main_DescriptionAr], [Main_Adderess], [Main_Createdat], [Main_Updatedat], [Delet]) VALUES (3, N'El-Mostakbal ware house', N'المستقبل للتوريدات', N'Main ware house for El Mostakbal company', N'المستقبل للتوريدات', N'El-Manhal', CAST(N'2024-09-01T14:22:20.430' AS DateTime), CAST(N'2024-09-23T10:43:02.453' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[MainWearhouse] OFF
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 

INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (66, N'1', 2, CAST(N'2024-10-17T13:58:55.317' AS DateTime), 1, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (67, N'2', 3, CAST(N'2024-10-17T14:00:03.447' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (69, N'3', 4, CAST(N'2024-10-17T14:00:32.803' AS DateTime), 7, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (71, N'4', 2, CAST(N'2024-10-17T15:07:49.623' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (72, N'5', 2, CAST(N'2024-10-17T15:08:15.013' AS DateTime), 9, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (73, N'6', 3, CAST(N'2024-10-17T15:08:42.600' AS DateTime), 1, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (74, N'7', 5, CAST(N'2024-10-17T15:09:33.987' AS DateTime), 3, 7)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (75, N'8', 2, CAST(N'2024-10-17T16:40:51.890' AS DateTime), 7, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (76, N'9', 2, CAST(N'2024-10-17T16:42:25.377' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (78, N'10', 2, CAST(N'2024-10-17T16:43:09.730' AS DateTime), 9, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (79, N'11', 4, CAST(N'2024-10-17T16:43:29.137' AS DateTime), 1, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (84, N'12', 2, CAST(N'2024-10-23T14:13:15.223' AS DateTime), 1, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (85, N'13', 5, CAST(N'2024-10-23T14:14:52.243' AS DateTime), 1, 3)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (88, N'14', 3, CAST(N'2024-10-23T14:16:32.020' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (89, N'15', 4, CAST(N'2024-10-23T14:16:47.123' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (90, N'16', 2, CAST(N'2024-10-29T09:16:22.873' AS DateTime), 35, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (91, N'17', 2, CAST(N'2024-10-29T09:19:33.503' AS DateTime), 35, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (92, N'18', 3, CAST(N'2024-10-29T09:20:25.643' AS DateTime), 35, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (93, N'19', 2, CAST(N'2024-10-29T09:42:53.817' AS DateTime), 35, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (94, N'20', 2, CAST(N'2024-10-31T10:39:39.213' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (98, N'21', 2, CAST(N'2024-11-03T16:19:50.767' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (100, N'22', 2, CAST(N'2024-11-03T16:20:39.610' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (101, N'23', 3, CAST(N'2024-11-03T16:21:15.207' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (102, N'24', 5, CAST(N'2024-11-04T16:09:10.093' AS DateTime), 7, 1)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (103, N'FRN', 3, CAST(N'2024-11-04T16:50:15.820' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (106, N'FRN001', 4, CAST(N'2024-11-04T17:11:48.170' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (107, N'FRN002', 4, CAST(N'2024-11-04T17:16:13.107' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (108, N'FRN010', 4, CAST(N'2024-11-04T17:17:58.140' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (109, N'string001', 5, CAST(N'2024-11-05T10:04:10.887' AS DateTime), 7, 31)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (112, N'string002', 5, CAST(N'2024-11-05T10:06:36.837' AS DateTime), 7, 31)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (115, N'string-123', 2, CAST(N'2024-11-05T10:23:16.620' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (116, N'string-003', 2, CAST(N'2024-11-05T10:23:40.750' AS DateTime), 8, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (119, N'string003', 3, CAST(N'2024-11-05T15:22:48.883' AS DateTime), 31, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (120, N'string333', 2, CAST(N'2024-11-05T15:32:21.660' AS DateTime), 7, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (122, N'string334', 5, CAST(N'2024-11-05T15:33:47.770' AS DateTime), 1, 7)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (124, N'strin335', 3, CAST(N'2024-11-05T15:35:16.367' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (125, N'string335', 3, CAST(N'2024-11-05T15:42:44.793' AS DateTime), 1, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (126, N'mostafa', 2, CAST(N'2024-11-05T16:19:24.633' AS DateTime), 3, NULL)
INSERT [dbo].[Permission] ([Perm_ID], [PermCode], [PermType_FK], [Perm_Createdat], [Sub_FK], [DestinationSubFk]) VALUES (127, N'string336', 4, CAST(N'2024-11-05T16:29:05.087' AS DateTime), 3, NULL)
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
SET IDENTITY_INSERT [dbo].[PermissionType] ON 

INSERT [dbo].[PermissionType] ([Per_ID], [PerTypeValueAr], [PerTypeValueEn]) VALUES (2, N'اذن اضافة', N'Addition permission')
INSERT [dbo].[PermissionType] ([Per_ID], [PerTypeValueAr], [PerTypeValueEn]) VALUES (3, N'اذن صرف', N'Withdraw permission')
INSERT [dbo].[PermissionType] ([Per_ID], [PerTypeValueAr], [PerTypeValueEn]) VALUES (4, N'اذن تالف', N'Damaged permission')
INSERT [dbo].[PermissionType] ([Per_ID], [PerTypeValueAr], [PerTypeValueEn]) VALUES (5, N'اذن نقل', N'Transfer permission')
SET IDENTITY_INSERT [dbo].[PermissionType] OFF
GO
SET IDENTITY_INSERT [dbo].[Quantity] ON 

INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (1, 1, 700, CAST(N'2024-11-05T16:19:24.643' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (2, 2, 30, CAST(N'2024-11-05T16:29:05.103' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (3, 14, 30, CAST(N'2024-10-16T11:28:10.087' AS DateTime), CAST(N'2024-10-16T11:23:06.577' AS DateTime))
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (4, 13, 25, CAST(N'2024-10-16T13:16:38.780' AS DateTime), CAST(N'2024-10-16T11:26:58.470' AS DateTime))
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (5, 15, 120, CAST(N'2024-10-16T12:08:24.450' AS DateTime), CAST(N'2024-10-16T09:00:02.820' AS DateTime))
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (6, 12, 80, CAST(N'2024-11-05T10:23:40.760' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (7, 3, 55, CAST(N'2024-11-05T16:19:24.650' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (8, 16, 20, NULL, NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (9, 17, 30, NULL, NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (10, 18, 70, CAST(N'2024-10-29T09:42:53.827' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (11, 19, NULL, NULL, NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (12, 20, NULL, CAST(N'2024-11-05T16:19:24.657' AS DateTime), NULL)
INSERT [dbo].[Quantity] ([Quantity_ID], [Item_FK], [CurrentQuantity], [Quantity_Updatedat], [Quantity_Createdat]) VALUES (13, 21, 50, CAST(N'2024-11-05T16:19:24.667' AS DateTime), CAST(N'2024-11-05T16:19:24.667' AS DateTime))
SET IDENTITY_INSERT [dbo].[Quantity] OFF
GO
SET IDENTITY_INSERT [dbo].[SubItem] ON 

INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (3, 1, 3, 50)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (4, 1, 1, 8)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (5, 1, 7, 47)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (6, 2, 7, 12)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (8, 2, 3, 10)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (9, 3, 3, 50)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (10, 18, 7, 20)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (11, 1, 8, 80)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (12, 1, 31, 100)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (13, 2, 31, 20)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (14, 18, 31, 30)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (15, 12, 8, 60)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (16, 20, 3, 30)
INSERT [dbo].[SubItem] ([Id], [Item_FK], [Sub_FK], [Quantity]) VALUES (17, 21, 3, 50)
SET IDENTITY_INSERT [dbo].[SubItem] OFF
GO
SET IDENTITY_INSERT [dbo].[SubWearhouse] ON 

INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (1, 2, NULL, N'test3', N'اختبار3', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'test', NULL, CAST(N'2024-09-03T15:30:24.320' AS DateTime), CAST(N'2024-09-03T15:34:25.037' AS DateTime), 1)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (3, 3, NULL, N'test2', N'اختبار2', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'test2', NULL, CAST(N'2024-09-03T15:31:46.700' AS DateTime), CAST(N'2024-10-08T13:01:17.683' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (7, 3, 3, N'El mostakbal warehouse', N'المستقبل للتوريدات', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'el manhal', NULL, CAST(N'2024-09-13T00:00:00.000' AS DateTime), CAST(N'2024-09-23T09:33:51.783' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (8, 2, NULL, N'kjhsdfjsk', N'سسس', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'skjl', NULL, CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (9, 3, 7, N'test3', N'اختبار3', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'dfgdfgdfgdfgdfg', NULL, CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (10, 3, 9, N'iiiii', N'ييي', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'iiiiiii', NULL, CAST(N'2024-09-16T00:00:00.000' AS DateTime), CAST(N'2024-09-18T13:18:18.943' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (11, 2, 8, N'kjhsdfjsk', N'بلبثلر', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'skjl', NULL, CAST(N'2024-09-16T00:00:00.000' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (12, 3, NULL, N'iiii', N'ييي', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'll', NULL, CAST(N'2024-09-03T15:31:46.700' AS DateTime), CAST(N'2024-09-18T13:18:03.303' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (13, 3, 12, N'Mostafa', N'مصطفى', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'skjl', NULL, CAST(N'2024-09-16T00:00:00.000' AS DateTime), CAST(N'2024-09-18T13:07:35.967' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (14, 3, 10, N'swagger', N'سواجر', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'fdghdfghdfh', NULL, CAST(N'2024-09-18T14:33:33.913' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (15, 3, 7, N'swagger', N'سواجر', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'fdghdfghdfh', NULL, CAST(N'2024-09-18T14:35:10.553' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (16, 3, 7, N'fdgg', N'في', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'fdghdfghdfh', NULL, CAST(N'2024-09-18T14:37:22.617' AS DateTime), CAST(N'2024-09-18T14:37:42.570' AS DateTime), 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (30, 3, 14, N'2222222222', N'2222222', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'22222222', NULL, CAST(N'2024-09-23T13:19:19.063' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (31, 3, 10, N'khater', N'خاطر', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'sdfgdsfgsd', NULL, CAST(N'2024-10-08T12:51:46.077' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (32, 3, NULL, N'asdasd', N'اسداسد', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'asdasd', NULL, CAST(N'2024-10-08T13:03:14.640' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (33, 1, NULL, N'mmom', N'مومم', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'aSD', NULL, CAST(N'2024-10-20T10:46:40.223' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (34, 1, NULL, N'MOMMM2', N'مومم2', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'SADAS', NULL, CAST(N'2024-10-20T10:46:54.153' AS DateTime), NULL, 0)
INSERT [dbo].[SubWearhouse] ([Sub_ID], [Main_FK], [ParentSubWearhouseId], [Sub_NameEn], [Sub_NameAr], [Sub_DescriptionEn], [Sub_DescriptionAr], [Sub_AddressEn], [Sub_AddressAr], [Sub_Createdat], [Sub_Updatedat], [Delet]) VALUES (35, 1, 33, N'MOMSUB', N'مومساب', N'A .NET class library (and associated sample application) that grabs "Lorem Ipsum" filler text from lipsum.com

', N'مكتبة فئة .NET (ونموذج التطبيق المرتبط بها) التي تلتقط نص حشو "Lorem Ipsum" من موقع Lipsum.com', N'SDF', NULL, CAST(N'2024-10-20T10:47:06.893' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[SubWearhouse] OFF
GO
SET IDENTITY_INSERT [dbo].[Unit] ON 

INSERT [dbo].[Unit] ([Unit_ID], [Unit_NameEn], [Unit_NameAr], [Unit_DescEn], [Unit_DescAr], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (1, N'Tabs', N'شريط', NULL, NULL, NULL, NULL)
INSERT [dbo].[Unit] ([Unit_ID], [Unit_NameEn], [Unit_NameAr], [Unit_DescEn], [Unit_DescAr], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (2, N'miligram', N'ملليجرام', NULL, NULL, NULL, NULL)
INSERT [dbo].[Unit] ([Unit_ID], [Unit_NameEn], [Unit_NameAr], [Unit_DescEn], [Unit_DescAr], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (3, N'test', N'اختبار', NULL, NULL, NULL, NULL)
INSERT [dbo].[Unit] ([Unit_ID], [Unit_NameEn], [Unit_NameAr], [Unit_DescEn], [Unit_DescAr], [Unit_CreatedAt], [Unit_UpdatedAt]) VALUES (4, N'unit', N'وحده', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Unit] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_CodeFormats_Format]    Script Date: 12/3/2024 9:14:28 PM ******/
ALTER TABLE [dbo].[CodeFormat] ADD  CONSTRAINT [UQ_CodeFormats_Format] UNIQUE NONCLUSTERED 
(
	[Format] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_PermCode]    Script Date: 12/3/2024 9:14:28 PM ******/
ALTER TABLE [dbo].[Permission] ADD  CONSTRAINT [UQ_PermCode] UNIQUE NONCLUSTERED 
(
	[PermCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_SubWearhouse_ParentDelet]    Script Date: 12/3/2024 9:14:28 PM ******/
CREATE NONCLUSTERED INDEX [idx_SubWearhouse_ParentDelet] ON [dbo].[SubWearhouse]
(
	[ParentSubWearhouseId] ASC,
	[Delet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Category] ([Cat_ID])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[ItemPermission]  WITH CHECK ADD  CONSTRAINT [FK_ItemPermission_Items] FOREIGN KEY([Item_FK])
REFERENCES [dbo].[Items] ([Item_ID])
GO
ALTER TABLE [dbo].[ItemPermission] CHECK CONSTRAINT [FK_ItemPermission_Items]
GO
ALTER TABLE [dbo].[ItemPermission]  WITH CHECK ADD  CONSTRAINT [FK_ItemPermission_Permission] FOREIGN KEY([Perm_FK])
REFERENCES [dbo].[Permission] ([Perm_ID])
GO
ALTER TABLE [dbo].[ItemPermission] CHECK CONSTRAINT [FK_ItemPermission_Permission]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Category] FOREIGN KEY([Cat_FK])
REFERENCES [dbo].[Category] ([Cat_ID])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Category]
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
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_PermissionType] FOREIGN KEY([PermType_FK])
REFERENCES [dbo].[PermissionType] ([Per_ID])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_PermissionType]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_SubWearhouse] FOREIGN KEY([DestinationSubFk])
REFERENCES [dbo].[SubWearhouse] ([Sub_ID])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_SubWearhouse]
GO
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_SubWearhouse1] FOREIGN KEY([Sub_FK])
REFERENCES [dbo].[SubWearhouse] ([Sub_ID])
GO
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_SubWearhouse1]
GO
ALTER TABLE [dbo].[Quantity]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Items] FOREIGN KEY([Item_FK])
REFERENCES [dbo].[Items] ([Item_ID])
GO
ALTER TABLE [dbo].[Quantity] CHECK CONSTRAINT [FK_Stock_Items]
GO
ALTER TABLE [dbo].[SubItem]  WITH CHECK ADD  CONSTRAINT [FK_SubItem_Items] FOREIGN KEY([Item_FK])
REFERENCES [dbo].[Items] ([Item_ID])
GO
ALTER TABLE [dbo].[SubItem] CHECK CONSTRAINT [FK_SubItem_Items]
GO
ALTER TABLE [dbo].[SubItem]  WITH CHECK ADD  CONSTRAINT [FK_SubItem_SubWearhouse] FOREIGN KEY([Sub_FK])
REFERENCES [dbo].[SubWearhouse] ([Sub_ID])
GO
ALTER TABLE [dbo].[SubItem] CHECK CONSTRAINT [FK_SubItem_SubWearhouse]
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
