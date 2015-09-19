USE [master]
GO
/****** Object:  Database [TestcaseManagerDemo]    Script Date: 8/30/2015 9:57:52 PM ******/
CREATE DATABASE [TestcaseManagerDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestcaseManagerDemo', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TestcaseManagerDemo.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TestcaseManagerDemo_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TestcaseManagerDemo_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 8/30/2015 9:57:52 PM ******/
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
 CONSTRAINT [PK_ApplicationUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ApplicationUsers] ON 

INSERT [dbo].[ApplicationUsers] ([UserId], [Username], [Password], [IsAdmin], [IsReadOnly]) VALUES (4, N'Prdophian', N'1', 0, 0)
SET IDENTITY_INSERT [dbo].[ApplicationUsers] OFF
USE [master]
GO
ALTER DATABASE [TestcaseManagerDemo] SET  READ_WRITE 
GO
