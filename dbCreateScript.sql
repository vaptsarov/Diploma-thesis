USE [master]
GO
/****** Object:  Database [TestcaseManagerDemo]    Script Date: 1/2/2016 8:01:11 PM ******/
CREATE DATABASE [TestcaseManagerDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestcaseManagerDemo', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TestcaseManagerDemo.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TestcaseManagerDemo_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TestcaseManagerDemo_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TestcaseManagerDemo] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestcaseManagerDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestcaseManagerDemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestcaseManagerDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestcaseManagerDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestcaseManagerDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET RECOVERY FULL 
GO
ALTER DATABASE [TestcaseManagerDemo] SET  MULTI_USER 
GO
ALTER DATABASE [TestcaseManagerDemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestcaseManagerDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestcaseManagerDemo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestcaseManagerDemo', N'ON'
GO
USE [TestcaseManagerDemo]
GO
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 1/2/2016 8:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsReadOnly] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ApplicationUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Areas]    Script Date: 1/2/2016 8:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 1/2/2016 8:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StepDefinitions]    Script Date: 1/2/2016 8:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StepDefinitions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Step] [nvarchar](max) NOT NULL,
	[ExpectedResult] [nvarchar](max) NULL,
	[TestCaseID] [int] NOT NULL,
 CONSTRAINT [PK_StepDefinitions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestCases]    Script Date: 1/2/2016 8:01:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Priority] [nvarchar](100) NOT NULL,
	[Severity] [nvarchar](100) NOT NULL,
	[IsAutomated] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[AreaID] [int] NOT NULL,
 CONSTRAINT [PK_TestCases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([ID], [Title], [CreatedBy], [UpdatedBy], [ProjectId]) VALUES (3, N'Area 1', N'Borislav Vaptsarov', NULL, 1)
INSERT [dbo].[Areas] ([ID], [Title], [CreatedBy], [UpdatedBy], [ProjectId]) VALUES (5, N'Area 2', N'Borislav Vaptsarov', NULL, 1)
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ID], [Title], [CreatedBy], [UpdatedBy]) VALUES (1, N'Project 1', N'Borislav Vaptsarov', NULL)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[StepDefinitions] ON 

INSERT [dbo].[StepDefinitions] ([ID], [Step], [ExpectedResult], [TestCaseID]) VALUES (1, N'Login into the system with the valid credentials', N'User is authenticated', 1)
INSERT [dbo].[StepDefinitions] ([ID], [Step], [ExpectedResult], [TestCaseID]) VALUES (2, N'Logout from the system', NULL, 1)
SET IDENTITY_INSERT [dbo].[StepDefinitions] OFF
SET IDENTITY_INSERT [dbo].[TestCases] ON 

INSERT [dbo].[TestCases] ([ID], [Title], [Priority], [Severity], [IsAutomated], [CreatedBy], [UpdatedBy], [AreaID]) VALUES (1, N'Test case 1', N'Critical', N'Critical', 1, N'Borislav Vaptsarov', NULL, 3)
INSERT [dbo].[TestCases] ([ID], [Title], [Priority], [Severity], [IsAutomated], [CreatedBy], [UpdatedBy], [AreaID]) VALUES (2, N'Test case 2', N'High', N'High', 0, N'Borislav Vaptsarov', NULL, 5)
SET IDENTITY_INSERT [dbo].[TestCases] OFF
ALTER TABLE [dbo].[Areas]  WITH CHECK ADD  CONSTRAINT [FK_Areas_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
ALTER TABLE [dbo].[Areas] CHECK CONSTRAINT [FK_Areas_Projects]
GO
ALTER TABLE [dbo].[StepDefinitions]  WITH CHECK ADD  CONSTRAINT [FK_StepDefinitions_TestCases] FOREIGN KEY([TestCaseID])
REFERENCES [dbo].[TestCases] ([ID])
GO
ALTER TABLE [dbo].[StepDefinitions] CHECK CONSTRAINT [FK_StepDefinitions_TestCases]
GO
ALTER TABLE [dbo].[TestCases]  WITH CHECK ADD  CONSTRAINT [FK_TestCases_Areas] FOREIGN KEY([AreaID])
REFERENCES [dbo].[Areas] ([ID])
GO
ALTER TABLE [dbo].[TestCases] CHECK CONSTRAINT [FK_TestCases_Areas]
GO
USE [master]
GO
ALTER DATABASE [TestcaseManagerDemo] SET  READ_WRITE 
GO
