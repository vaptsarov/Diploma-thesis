USE [master]
GO
/****** Object:  Database [TestcaseManagerDemo]    Script Date: 28.3.2018 г. 13:15:22 ******/
CREATE DATABASE [TestcaseManagerDemo]
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
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestcaseManagerDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [TestcaseManagerDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestcaseManagerDemo] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TestcaseManagerDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET  MULTI_USER 
GO
ALTER DATABASE [TestcaseManagerDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestcaseManagerDemo] SET ENCRYPTION ON
GO
ALTER DATABASE [TestcaseManagerDemo] SET QUERY_STORE = ON
GO
ALTER DATABASE [TestcaseManagerDemo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [TestcaseManagerDemo]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [TestcaseManagerDemo]
GO
/****** Object:  Table [dbo].[ApplicationUsers]    Script Date: 28.3.2018 г. 13:15:24 ******/
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
/****** Object:  Table [dbo].[Areas]    Script Date: 28.3.2018 г. 13:15:24 ******/
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
/****** Object:  Table [dbo].[Projects]    Script Date: 28.3.2018 г. 13:15:24 ******/
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
/****** Object:  Table [dbo].[StepDefinitions]    Script Date: 28.3.2018 г. 13:15:24 ******/
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
/****** Object:  Table [dbo].[TestCases]    Script Date: 28.3.2018 г. 13:15:24 ******/
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
/****** Object:  Table [dbo].[TestComposites]    Script Date: 28.3.2018 г. 13:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestComposites](
	[TestRunID] [int] NOT NULL,
	[TestCaseID] [int] NOT NULL,
	[TestCaseStatus] [nvarchar](100) NULL,
 CONSTRAINT [PK_TestComposites] PRIMARY KEY CLUSTERED 
(
	[TestRunID] ASC,
	[TestCaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestRuns]    Script Date: 28.3.2018 г. 13:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestRuns](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TestRuns] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ApplicationUsers] ON 

INSERT [dbo].[ApplicationUsers] ([UserId], [Username], [Password], [IsAdmin], [IsReadOnly], [CreatedBy], [UpdatedBy], [CreatedOn]) VALUES (1, N'Borislav Vaptsarov', N'CeldllnR4q5RSTyAyjSmhE1RKh7hd10Piu44HuoVOCfJ/RzsnZELta6Oz7uktEeyxq9wtnRHupBofpcrRcNXlZKggjk4we08wqs2OjMs/iJgiPU0afuy4vwt5axdrsCn72NnbxkV3otaAz6VhNTg/hN03/Ba1CuTb+Zlh/gjAVpn+p8F7904cK32qVnmI0Y9/MVrVSjQVf5lAlHUK+d0n/Orhmx9WWVjGz0m1MrTpPSk1865dtzlbi6Re88KHvVgyxfbizJiQMY5GQ5qfDCBQ5TQgRfNI87nQ1+RmN8uRotG/JVLTOwJ44y6xlAmlAp/BwnImJQM/M55YEPRdH7X2g==', 1, 0, N'Admin', N'Admin', CAST(N'2017-04-30T00:00:00.000' AS DateTime))
INSERT [dbo].[ApplicationUsers] ([UserId], [Username], [Password], [IsAdmin], [IsReadOnly], [CreatedBy], [UpdatedBy], [CreatedOn]) VALUES (2, N'admin', N'IDJ6mGj3IzH8ldgdOXjHbMqbSpA9Q8PzVGC59gEGipdEuLEYvaxfkurmcSkq2PJZCb50DwlWdAsWkQUtN/rlKSj+MwBYT57uKTs2Sw6h1NIhBWvJeMtj5jCw4jOBEGYFIK7L0MmsBbUQn6tb6YhppjEXrl5MPeEgAXiDydNpNhrQpCDnskG/BOUUuvcEkGrg4ptmPf5lrYtUrFuxuK8DZNq0fC2m3QioWrDcdslq/sAX8ytq3tKZ8rTpLAOYWdxg/rpCPVxXvGcc9WczkKmFkSpzatGlVnaq/2e97hr9D++PPPfiWkQBthWOjLB/85oNw9O989ITRDwU9h2/Hp0DEw==', 1, 0, N'Borislav Vaptsarov', NULL, CAST(N'2018-03-28T09:57:37.023' AS DateTime))
SET IDENTITY_INSERT [dbo].[ApplicationUsers] OFF
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([ID], [Title], [CreatedBy], [UpdatedBy], [ProjectId]) VALUES (3010, N'UI ', N'Borislav Vaptsarov', NULL, 2009)
INSERT [dbo].[Areas] ([ID], [Title], [CreatedBy], [UpdatedBy], [ProjectId]) VALUES (3011, N'BackEnd', N'Borislav Vaptsarov', NULL, 2009)
SET IDENTITY_INSERT [dbo].[Areas] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ID], [Title], [CreatedBy], [UpdatedBy]) VALUES (2009, N'TC Manager Test Cases', N'Borislav Vaptsarov', NULL)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[StepDefinitions] ON 

INSERT [dbo].[StepDefinitions] ([ID], [Step], [ExpectedResult], [TestCaseID]) VALUES (2009, N'Verify that log in screen is present.', N' The log in screen should be present when the UI loads up.', 2010)
SET IDENTITY_INSERT [dbo].[StepDefinitions] OFF
SET IDENTITY_INSERT [dbo].[TestCases] ON 

INSERT [dbo].[TestCases] ([ID], [Title], [Priority], [Severity], [IsAutomated], [CreatedBy], [UpdatedBy], [AreaID]) VALUES (2010, N'Verify visibility of elements', N'Critical', N'Blocking', 1, N'Borislav Vaptsarov', N'admin', 3010)
SET IDENTITY_INSERT [dbo].[TestCases] OFF
INSERT [dbo].[TestComposites] ([TestRunID], [TestCaseID], [TestCaseStatus]) VALUES (1, 2010, N'Passed')
SET IDENTITY_INSERT [dbo].[TestRuns] ON 

INSERT [dbo].[TestRuns] ([ID], [Name], [CreatedOn], [CreatedBy]) VALUES (1, N'Test Run for TC Manager', CAST(N'2018-03-28T09:53:38.813' AS DateTime), N'Borislav Vaptsarov')
SET IDENTITY_INSERT [dbo].[TestRuns] OFF
/****** Object:  Index [IX_FK_Areas_Projects]    Script Date: 28.3.2018 г. 13:15:26 ******/
CREATE NONCLUSTERED INDEX [IX_FK_Areas_Projects] ON [dbo].[Areas]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_StepDefinitions_TestCases]    Script Date: 28.3.2018 г. 13:15:26 ******/
CREATE NONCLUSTERED INDEX [IX_FK_StepDefinitions_TestCases] ON [dbo].[StepDefinitions]
(
	[TestCaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TestCases_Areas]    Script Date: 28.3.2018 г. 13:15:26 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TestCases_Areas] ON [dbo].[TestCases]
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_TestComposite_TestCases]    Script Date: 28.3.2018 г. 13:15:26 ******/
CREATE NONCLUSTERED INDEX [IX_FK_TestComposite_TestCases] ON [dbo].[TestComposites]
(
	[TestCaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
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
ALTER TABLE [dbo].[TestComposites]  WITH CHECK ADD  CONSTRAINT [FK_TestComposite_TestCases] FOREIGN KEY([TestCaseID])
REFERENCES [dbo].[TestCases] ([ID])
GO
ALTER TABLE [dbo].[TestComposites] CHECK CONSTRAINT [FK_TestComposite_TestCases]
GO
ALTER TABLE [dbo].[TestComposites]  WITH CHECK ADD  CONSTRAINT [FK_TestComposite_TestRuns] FOREIGN KEY([TestRunID])
REFERENCES [dbo].[TestRuns] ([ID])
GO
ALTER TABLE [dbo].[TestComposites] CHECK CONSTRAINT [FK_TestComposite_TestRuns]
GO
USE [master]
GO
ALTER DATABASE [TestcaseManagerDemo] SET  READ_WRITE 
GO
