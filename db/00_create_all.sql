-- create all tables
-- creates the tables and adds constraints
-- peter saunders
-- april 3 2022

-- start with db
USE [master]
GO

DROP DATABASE IF EXISTS [rrr-db]
GO

CREATE DATABASE [rrr-db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'rrr-db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\rrr-db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'rrr-db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\rrr-db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [rrr-db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [rrr-db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [rrr-db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [rrr-db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [rrr-db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [rrr-db] SET ARITHABORT OFF 
GO
ALTER DATABASE [rrr-db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [rrr-db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [rrr-db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [rrr-db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [rrr-db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [rrr-db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [rrr-db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [rrr-db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [rrr-db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [rrr-db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [rrr-db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [rrr-db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [rrr-db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [rrr-db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [rrr-db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [rrr-db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [rrr-db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [rrr-db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [rrr-db] SET  MULTI_USER 
GO
ALTER DATABASE [rrr-db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [rrr-db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [rrr-db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [rrr-db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [rrr-db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [rrr-db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [rrr-db] SET QUERY_STORE = OFF
GO
ALTER DATABASE [rrr-db] SET  READ_WRITE 
GO

--- now deal with tables
USE [rrr-db]
GO

-- unit
ALTER TABLE [dbo].[Unit] DROP CONSTRAINT [DF_Unit_PhysicalMeasure]
GO

ALTER TABLE [dbo].[Unit] DROP CONSTRAINT [DF_Unit_Weight]
GO

ALTER TABLE [dbo].[Unit] DROP CONSTRAINT [DF_Unit_Liquid]
GO

ALTER TABLE [dbo].[Unit] DROP CONSTRAINT [DF_Unit_Name]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Unit]') AND type in (N'U'))
DROP TABLE [dbo].[Unit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Liquid] [bit] NULL,
	[Weight] [bit] NULL,
	[PhysicalMeasure] [bit] NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_Name]  DEFAULT (N'd') FOR [Name]
GO

ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_Liquid]  DEFAULT ((0)) FOR [Liquid]
GO

ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_Weight]  DEFAULT ((0)) FOR [Weight]
GO

ALTER TABLE [dbo].[Unit] ADD  CONSTRAINT [DF_Unit_PhysicalMeasure]  DEFAULT ((0)) FOR [PhysicalMeasure]
GO

-- account type
ALTER TABLE [dbo].[AccountType] DROP CONSTRAINT [DF_AccountType_Type]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountType]') AND type in (N'U'))
DROP TABLE [dbo].[AccountType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AccountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](255) NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_Type]  DEFAULT ('d') FOR [Type]
GO

-- course
ALTER TABLE [dbo].[Course] DROP CONSTRAINT [DF_Course_Name]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
DROP TABLE [dbo].[Course]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [DF_Course_Name]  DEFAULT (N'd') FOR [Name]
GO

-- cuisine
ALTER TABLE [dbo].[Cuisine] DROP CONSTRAINT [DF_Cuisine_Country]
GO

ALTER TABLE [dbo].[Cuisine] DROP CONSTRAINT [DF_Cuisine_Region]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuisine]') AND type in (N'U'))
DROP TABLE [dbo].[Cuisine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cuisine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Region] [nvarchar](255) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Cuisine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cuisine] ADD  CONSTRAINT [DF_Cuisine_Region]  DEFAULT (N'd') FOR [Region]
GO

ALTER TABLE [dbo].[Cuisine] ADD  CONSTRAINT [DF_Cuisine_Country]  DEFAULT (N'd') FOR [Country]
GO

-- person
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_AccountType]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_FailedLoginCount]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_Password]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_Username]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_EmailNewsletter]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_EmailUpdates]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_Email]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_FirstName]
GO

ALTER TABLE [dbo].[Person] DROP CONSTRAINT [DF_Person_AccountTypeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE [dbo].[Person]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[FirstName] [varchar](255) NOT NULL,
	[LastName] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[EmailUpdates] [bit] NULL,
	[EmailNewsletter] [bit] NULL,
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[FailedLoginCount] [int] NOT NULL,
	[About] [varchar](3750) NOT NULL,
	[Image] [varchar](255) NOT NULL,
	[File] [varbinary] NULL
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_AccountTypeId]  DEFAULT ((0)) FOR [AccountTypeId]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_FirstName]  DEFAULT ('d') FOR [FirstName]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Email]  DEFAULT ('d') FOR [Email]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_EmailUpdates]  DEFAULT ((0)) FOR [EmailUpdates]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_EmailNewsletter]  DEFAULT ((0)) FOR [EmailNewsletter]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Username]  DEFAULT ('d') FOR [Username]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Password]  DEFAULT ('d') FOR [Password]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_FailedLoginCount]  DEFAULT ((0)) FOR [FailedLoginCount]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_About]  DEFAULT ('d') FOR [About]
GO

ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Image]  DEFAULT ('no_image.png') FOR [Image]
GO

ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AccountType] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountType] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AccountType]
GO

-- recipe
ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [FK_Recipe_Cuisine]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_Difficulty]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_Story]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_ServingCount]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_CreationDate]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_Name]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_CuisineId]
GO

ALTER TABLE [dbo].[Recipe] DROP CONSTRAINT [DF_Recipe_PersonId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recipe]') AND type in (N'U'))
DROP TABLE [dbo].[Recipe]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[CuisineId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[ServingCount] [int] NOT NULL,
	[Story] [nvarchar](4000) NOT NULL,
	[Difficulty] [int] NOT NULL,
	[Image] [varchar](255) NOT NULL,
	[File] [varbinary] NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_PersonId]  DEFAULT ((0)) FOR [PersonId]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_CuisineId]  DEFAULT ((0)) FOR [CuisineId]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_Name]  DEFAULT (N'd') FOR [Name]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_ServingCount]  DEFAULT ((0)) FOR [ServingCount]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_Story]  DEFAULT (N'd') FOR [Story]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_Difficulty]  DEFAULT ((0)) FOR [Difficulty]
GO

ALTER TABLE [dbo].[Recipe] ADD  CONSTRAINT [DF_Recipe_Image]  DEFAULT (('no_image.png')) FOR [Image]
GO

ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [FK_Recipe_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [FK_Recipe_Cuisine] FOREIGN KEY([CuisineId])
REFERENCES [dbo].[Cuisine] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [FK_Recipe_Cuisine]
GO

-- diet
ALTER TABLE [dbo].[Diet] DROP CONSTRAINT [DF_Diet_Name]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diet]') AND type in (N'U'))
DROP TABLE [dbo].[Diet]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Diet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Diet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Diet] ADD  CONSTRAINT [DF_Diet_Name]  DEFAULT (N'd') FOR [Name]
GO

ALTER TABLE [dbo].[DietList] CHECK CONSTRAINT [FK_DietList_Recipe]
GO

-- image recipe
/*
ALTER TABLE [dbo].[ImageRecipe] DROP CONSTRAINT [FK_ImageRecipe_Recipe]
GO

ALTER TABLE [dbo].[ImageRecipe] DROP CONSTRAINT [DF_ImageRecipe_Location]
GO

ALTER TABLE [dbo].[ImageRecipe] DROP CONSTRAINT [DF_ImageRecipe_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageRecipe]') AND type in (N'U'))
DROP TABLE [dbo].[ImageRecipe]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImageRecipe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[Location] [varchar](255) NOT NULL,
 CONSTRAINT [PK_ImageRecipe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ImageRecipe] ADD  CONSTRAINT [DF_ImageRecipe_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[ImageRecipe] ADD  CONSTRAINT [DF_ImageRecipe_Location]  DEFAULT ('d') FOR [Location]
GO

ALTER TABLE [dbo].[ImageRecipe]  WITH CHECK ADD  CONSTRAINT [FK_ImageRecipe_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ImageRecipe] CHECK CONSTRAINT [FK_ImageRecipe_Recipe]
GO
*/
-- ingredient list
ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [FK_IngredientList_Unit]
GO

ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [FK_IngredientList_Recipe]
GO

ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [DF_IngredientList_Quantity]
GO

ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [DF_IngredientList_Name]
GO

ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [DF_IngredientList_UnitId]
GO

ALTER TABLE [dbo].[IngredientList] DROP CONSTRAINT [DF_IngredientList_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IngredientList]') AND type in (N'U'))
DROP TABLE [dbo].[IngredientList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IngredientList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[UnitId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_IngredientList2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[IngredientList] ADD  CONSTRAINT [DF_IngredientList_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[IngredientList] ADD  CONSTRAINT [DF_IngredientList_UnitId]  DEFAULT ((0)) FOR [UnitId]
GO

ALTER TABLE [dbo].[IngredientList] ADD  CONSTRAINT [DF_IngredientList_Name]  DEFAULT (N'd') FOR [Name]
GO

ALTER TABLE [dbo].[IngredientList] ADD  CONSTRAINT [DF_IngredientList_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO

ALTER TABLE [dbo].[IngredientList]  WITH CHECK ADD  CONSTRAINT [FK_IngredientList_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[IngredientList] CHECK CONSTRAINT [FK_IngredientList_Recipe]
GO

ALTER TABLE [dbo].[IngredientList]  WITH CHECK ADD  CONSTRAINT [FK_IngredientList_Unit] FOREIGN KEY([UnitId])
REFERENCES [dbo].[Unit] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[IngredientList] CHECK CONSTRAINT [FK_IngredientList_Unit]
GO

-- review
ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_IHaveAQuestion]
GO

ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_IMadeThis]
GO

ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_Votes]
GO

ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_PublishDate]
GO

ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_Comment]
GO

ALTER TABLE [dbo].[Review] DROP CONSTRAINT [DF_Review_RatingValue]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Review]') AND type in (N'U'))
DROP TABLE [dbo].[Review]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Review](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RatingValue] [int] NOT NULL,
	[Comment] [nvarchar](255) NULL,
	[PublishDate] [date] NOT NULL,
	[Votes] [int] NOT NULL,
	[IMadeThis] [bit] NULL,
	[IHaveAQuestion] [bit] NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_RatingValue]  DEFAULT ((0)) FOR [RatingValue]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_Comment]  DEFAULT (N'd') FOR [Comment]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_PublishDate]  DEFAULT (getdate()) FOR [PublishDate]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_Votes]  DEFAULT ((0)) FOR [Votes]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_IMadeThis]  DEFAULT ((0)) FOR [IMadeThis]
GO

ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_IHaveAQuestion]  DEFAULT ((0)) FOR [IHaveAQuestion]
GO

-- person review
ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [FK_PersonReview_Review]
GO

ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [FK_PersonReview_Recipe]
GO

ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [FK_PersonReview_Person]
GO

ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [DF_PersonReview_ReviewId]
GO

ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [DF_PersonReview_PersonId]
GO

ALTER TABLE [dbo].[PersonReview] DROP CONSTRAINT [DF_PersonReview_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonReview]') AND type in (N'U'))
DROP TABLE [dbo].[PersonReview]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PersonReview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[ReviewId] [int] NOT NULL,
 CONSTRAINT [PK_PersonReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PersonReview] ADD  CONSTRAINT [DF_PersonReview_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[PersonReview] ADD  CONSTRAINT [DF_PersonReview_PersonId]  DEFAULT ((0)) FOR [PersonId]
GO

ALTER TABLE [dbo].[PersonReview] ADD  CONSTRAINT [DF_PersonReview_ReviewId]  DEFAULT ((0)) FOR [ReviewId]
GO

ALTER TABLE [dbo].[PersonReview]  WITH CHECK ADD  CONSTRAINT [FK_PersonReview_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PersonReview] CHECK CONSTRAINT [FK_PersonReview_Person]
GO

ALTER TABLE [dbo].[PersonReview]  WITH CHECK ADD  CONSTRAINT [FK_PersonReview_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE NO ACTION
ON DELETE NO ACTION
GO

ALTER TABLE [dbo].[PersonReview] CHECK CONSTRAINT [FK_PersonReview_Recipe]
GO

ALTER TABLE [dbo].[PersonReview]  WITH CHECK ADD  CONSTRAINT [FK_PersonReview_Review] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Review] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PersonReview] CHECK CONSTRAINT [FK_PersonReview_Review]
GO

-- diet list
ALTER TABLE [dbo].[DietList] DROP CONSTRAINT [FK_DietList_Recipe]
GO

ALTER TABLE [dbo].[DietList] DROP CONSTRAINT [FK_DietList_Diet]
GO

ALTER TABLE [dbo].[DietList] DROP CONSTRAINT [DF_DietList_DietId]
GO

ALTER TABLE [dbo].[DietList] DROP CONSTRAINT [DF_DietList_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DietList]') AND type in (N'U'))
DROP TABLE [dbo].[DietList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DietList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[DietId] [int] NOT NULL,
 CONSTRAINT [PK_DietList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DietList] ADD  CONSTRAINT [DF_DietList_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[DietList] ADD  CONSTRAINT [DF_DietList_DietId]  DEFAULT ((0)) FOR [DietId]
GO

ALTER TABLE [dbo].[DietList]  WITH CHECK ADD  CONSTRAINT [FK_DietList_Diet] FOREIGN KEY([DietId])
REFERENCES [dbo].[Diet] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[DietList] CHECK CONSTRAINT [FK_DietList_Diet]
GO

ALTER TABLE [dbo].[DietList]  WITH CHECK ADD  CONSTRAINT [FK_DietList_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

-- image person
/*
ALTER TABLE [dbo].[ImagePerson] DROP CONSTRAINT [FK_ImagePerson_Person]
GO

ALTER TABLE [dbo].[ImagePerson] DROP CONSTRAINT [DF_File_Location]
GO

ALTER TABLE [dbo].[ImagePerson] DROP CONSTRAINT [DF_File_ParentId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImagePerson]') AND type in (N'U'))
DROP TABLE [dbo].[ImagePerson]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ImagePerson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Location] [varchar](255) NOT NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ImagePerson] ADD  CONSTRAINT [DF_File_ParentId]  DEFAULT ((0)) FOR [PersonId]
GO

ALTER TABLE [dbo].[ImagePerson] ADD  CONSTRAINT [DF_File_Location]  DEFAULT ('d') FOR [Location]
GO

ALTER TABLE [dbo].[ImagePerson]  WITH CHECK ADD  CONSTRAINT [FK_ImagePerson_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ImagePerson] CHECK CONSTRAINT [FK_ImagePerson_Person]
GO
*/
-- season
ALTER TABLE [dbo].[Season] DROP CONSTRAINT [DF_Season_SeasonName]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Season]') AND type in (N'U'))
DROP TABLE [dbo].[Season]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Season](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SeasonName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Season] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Season] ADD  CONSTRAINT [DF_Season_SeasonName]  DEFAULT (N'd') FOR [SeasonName]
GO

-- season list
ALTER TABLE [dbo].[SeasonList] DROP CONSTRAINT [FK_SeasonList_Season]
GO

ALTER TABLE [dbo].[SeasonList] DROP CONSTRAINT [FK_SeasonList_Recipe]
GO

ALTER TABLE [dbo].[SeasonList] DROP CONSTRAINT [DF_SeasonList_SeasonId]
GO

ALTER TABLE [dbo].[SeasonList] DROP CONSTRAINT [DF_SeasonList_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeasonList]') AND type in (N'U'))
DROP TABLE [dbo].[SeasonList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SeasonList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[SeasonId] [int] NOT NULL,
 CONSTRAINT [PK_SeasonList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SeasonList] ADD  CONSTRAINT [DF_SeasonList_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[SeasonList] ADD  CONSTRAINT [DF_SeasonList_SeasonId]  DEFAULT ((0)) FOR [SeasonId]
GO

ALTER TABLE [dbo].[SeasonList]  WITH CHECK ADD  CONSTRAINT [FK_SeasonList_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SeasonList] CHECK CONSTRAINT [FK_SeasonList_Recipe]
GO

ALTER TABLE [dbo].[SeasonList]  WITH CHECK ADD  CONSTRAINT [FK_SeasonList_Season] FOREIGN KEY([SeasonId])
REFERENCES [dbo].[Season] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[SeasonList] CHECK CONSTRAINT [FK_SeasonList_Season]
GO

-- timing
ALTER TABLE [dbo].[Timing] DROP CONSTRAINT [FK_Timing_Recipe]
GO

ALTER TABLE [dbo].[Timing] DROP CONSTRAINT [DF_Timing_Cooking]
GO

ALTER TABLE [dbo].[Timing] DROP CONSTRAINT [DF_Timing_Preparation]
GO

ALTER TABLE [dbo].[Timing] DROP CONSTRAINT [DF_Timing_RecipeId]
GO

ALTER TABLE [dbo].[Timing] DROP CONSTRAINT [DF_Timing_Id]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Timing]') AND type in (N'U'))
DROP TABLE [dbo].[Timing]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Timing](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[Preparation] [time](7) NOT NULL,
	[Cooking] [time](7) NOT NULL,
 CONSTRAINT [PK_Timing] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Timing] ADD  CONSTRAINT [DF_Timing_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[Timing] ADD  CONSTRAINT [DF_Timing_Preparation]  DEFAULT (CONVERT([time],'0:0:0')) FOR [Preparation]
GO

ALTER TABLE [dbo].[Timing] ADD  CONSTRAINT [DF_Timing_Cooking]  DEFAULT (CONVERT([time],'0:0:0')) FOR [Cooking]
GO

ALTER TABLE [dbo].[Timing]  WITH CHECK ADD  CONSTRAINT [FK_Timing_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Timing] CHECK CONSTRAINT [FK_Timing_Recipe]
GO

-- publish state
ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [FK_PublishState_Review]
GO

ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [FK_PublishState_Recipe]
GO

ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [DF_PublishState_State]
GO

ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [DF_PublishState_ReviewId]
GO

ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [DF_PublishState_RecipeId]
GO

ALTER TABLE [dbo].[PublishState] DROP CONSTRAINT [DF_PublishState_Id]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublishState]') AND type in (N'U'))
DROP TABLE [dbo].[PublishState]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PublishState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[ReviewId] [int] NOT NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_PublishState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PublishState] ADD  CONSTRAINT [DF_PublishState_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[PublishState] ADD  CONSTRAINT [DF_PublishState_ReviewId]  DEFAULT ((0)) FOR [ReviewId]
GO

ALTER TABLE [dbo].[PublishState] ADD  CONSTRAINT [DF_PublishState_State]  DEFAULT ((0)) FOR [State]
GO

ALTER TABLE [dbo].[PublishState]  WITH CHECK ADD  CONSTRAINT [FK_PublishState_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PublishState] CHECK CONSTRAINT [FK_PublishState_Recipe]
GO

ALTER TABLE [dbo].[PublishState]  WITH CHECK ADD  CONSTRAINT [FK_PublishState_Review] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Review] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[PublishState] CHECK CONSTRAINT [FK_PublishState_Review]
GO

-- course list
ALTER TABLE [dbo].[CourseList] DROP CONSTRAINT [FK_CourseList_Recipe]
GO

ALTER TABLE [dbo].[CourseList] DROP CONSTRAINT [FK_CourseList_Course]
GO

ALTER TABLE [dbo].[CourseList] DROP CONSTRAINT [DF_CourseList_RecipeId]
GO

ALTER TABLE [dbo].[CourseList] DROP CONSTRAINT [DF_CourseList_CourseId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CourseList]') AND type in (N'U'))
DROP TABLE [dbo].[CourseList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CourseList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CourseList] ADD  CONSTRAINT [DF_CourseList_CourseId]  DEFAULT ((0)) FOR [CourseId]
GO

ALTER TABLE [dbo].[CourseList] ADD  CONSTRAINT [DF_CourseList_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[CourseList]  WITH CHECK ADD  CONSTRAINT [FK_CourseList_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO

ALTER TABLE [dbo].[CourseList] CHECK CONSTRAINT [FK_CourseList_Course]
GO

ALTER TABLE [dbo].[CourseList]  WITH CHECK ADD  CONSTRAINT [FK_CourseList_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CourseList] CHECK CONSTRAINT [FK_CourseList_Recipe]
GO

-- account manager
ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [FK_AccountManager_Person]
GO

ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [DF_AccountManager_LastActivity]
GO

ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [DF_AccountManager_LastLogout]
GO

ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [DF_AccountManager_LastLogin]
GO

ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [DF_AccountManager_IsLoggedIn]
GO

ALTER TABLE [dbo].[AccountManager] DROP CONSTRAINT [DF_AccountManager_PersonId]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountManager]') AND type in (N'U'))
DROP TABLE [dbo].[AccountManager]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AccountManager](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[IsLoggedIn] [bit] NOT NULL,
	[LastLogin] [date] NOT NULL,
	[LastLogout] [date] NOT NULL,
	[LastActivity] [time](7) NOT NULL,
 CONSTRAINT [PK_AccountManager_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccountManager] ADD  CONSTRAINT [DF_AccountManager_PersonId]  DEFAULT ((0)) FOR [PersonId]
GO

ALTER TABLE [dbo].[AccountManager] ADD  CONSTRAINT [DF_AccountManager_IsLoggedIn]  DEFAULT ((0)) FOR [IsLoggedIn]
GO

ALTER TABLE [dbo].[AccountManager] ADD  CONSTRAINT [DF_AccountManager_LastLogin]  DEFAULT (CONVERT([date],'0001-01-01')) FOR [LastLogin]
GO

ALTER TABLE [dbo].[AccountManager] ADD  CONSTRAINT [DF_AccountManager_LastLogout]  DEFAULT (CONVERT([date],'0001-01-01')) FOR [LastLogout]
GO

ALTER TABLE [dbo].[AccountManager] ADD  CONSTRAINT [DF_AccountManager_LastActivity]  DEFAULT (CONVERT([time],'0001-01-01')) FOR [LastActivity]
GO

ALTER TABLE [dbo].[AccountManager]  WITH CHECK ADD  CONSTRAINT [FK_AccountManager_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AccountManager] CHECK CONSTRAINT [FK_AccountManager_Person]
GO

-- instruction
ALTER TABLE [dbo].[Instruction] DROP CONSTRAINT [DF_Instruction_StepWithDoneness]
GO

ALTER TABLE [dbo].[Instruction] DROP CONSTRAINT [DF_Instruction_StepNumber]
GO

ALTER TABLE [dbo].[Instruction] DROP CONSTRAINT [DF_Instruction_RecipeId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instruction]') AND type in (N'U'))
DROP TABLE [dbo].[Instruction]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Instruction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[StepNumber] [int] NOT NULL,
	[StepWithDoneness] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Instruction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Instruction] ADD  CONSTRAINT [DF_Instruction_RecipeId]  DEFAULT ((0)) FOR [RecipeId]
GO

ALTER TABLE [dbo].[Instruction] ADD  CONSTRAINT [DF_Instruction_StepNumber]  DEFAULT ((0)) FOR [StepNumber]
GO

ALTER TABLE [dbo].[Instruction] ADD  CONSTRAINT [DF_Instruction_StepWithDoneness]  DEFAULT ('d') FOR [StepWithDoneness]
GO

ALTER TABLE [dbo].[Instruction]  WITH CHECK ADD CONSTRAINT [FK_Recipe_Id] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO