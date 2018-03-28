
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2018 12:13:38
-- Generated from EDMX file: D:\DiplomaNBU\TestCaseManager.DB\TestCaseManagerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TestcaseManagerDemo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Areas_Projects]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Areas] DROP CONSTRAINT [FK_Areas_Projects];
GO
IF OBJECT_ID(N'[dbo].[FK_StepDefinitions_TestCases]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StepDefinitions] DROP CONSTRAINT [FK_StepDefinitions_TestCases];
GO
IF OBJECT_ID(N'[dbo].[FK_TestCases_Areas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestCases] DROP CONSTRAINT [FK_TestCases_Areas];
GO
IF OBJECT_ID(N'[dbo].[FK_TestComposite_TestCases]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestComposite] DROP CONSTRAINT [FK_TestComposite_TestCases];
GO
IF OBJECT_ID(N'[dbo].[FK_TestComposite_TestRuns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestComposite] DROP CONSTRAINT [FK_TestComposite_TestRuns];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ApplicationUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationUsers];
GO
IF OBJECT_ID(N'[dbo].[Areas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Areas];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[StepDefinitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StepDefinitions];
GO
IF OBJECT_ID(N'[dbo].[TestCases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestCases];
GO
IF OBJECT_ID(N'[dbo].[TestComposite]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestComposite];
GO
IF OBJECT_ID(N'[dbo].[TestRuns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestRuns];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ApplicationUsers'
CREATE TABLE [dbo].[ApplicationUsers] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [IsReadOnly] bit  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [UpdatedBy] nvarchar(max)  NULL,
    [CreatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Areas'
CREATE TABLE [dbo].[Areas] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [UpdatedBy] nvarchar(max)  NULL,
    [ProjectId] int  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [UpdatedBy] nvarchar(max)  NULL
);
GO

-- Creating table 'StepDefinitions'
CREATE TABLE [dbo].[StepDefinitions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Step] nvarchar(max)  NOT NULL,
    [ExpectedResult] nvarchar(max)  NULL,
    [TestCaseID] int  NOT NULL
);
GO

-- Creating table 'TestCases'
CREATE TABLE [dbo].[TestCases] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Priority] nvarchar(100)  NOT NULL,
    [Severity] nvarchar(100)  NOT NULL,
    [IsAutomated] bit  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL,
    [UpdatedBy] nvarchar(max)  NULL,
    [AreaID] int  NOT NULL
);
GO

-- Creating table 'TestComposites'
CREATE TABLE [dbo].[TestComposites] (
    [TestRunID] int  NOT NULL,
    [TestCaseID] int  NOT NULL,
    [TestCaseStatus] nvarchar(100)  NULL
);
GO

-- Creating table 'TestRuns'
CREATE TABLE [dbo].[TestRuns] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'ApplicationUsers'
ALTER TABLE [dbo].[ApplicationUsers]
ADD CONSTRAINT [PK_ApplicationUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ID] in table 'Areas'
ALTER TABLE [dbo].[Areas]
ADD CONSTRAINT [PK_Areas]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'StepDefinitions'
ALTER TABLE [dbo].[StepDefinitions]
ADD CONSTRAINT [PK_StepDefinitions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TestCases'
ALTER TABLE [dbo].[TestCases]
ADD CONSTRAINT [PK_TestCases]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [TestRunID], [TestCaseID] in table 'TestComposites'
ALTER TABLE [dbo].[TestComposites]
ADD CONSTRAINT [PK_TestComposites]
    PRIMARY KEY CLUSTERED ([TestRunID], [TestCaseID] ASC);
GO

-- Creating primary key on [ID] in table 'TestRuns'
ALTER TABLE [dbo].[TestRuns]
ADD CONSTRAINT [PK_TestRuns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'Areas'
ALTER TABLE [dbo].[Areas]
ADD CONSTRAINT [FK_Areas_Projects]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Areas_Projects'
CREATE INDEX [IX_FK_Areas_Projects]
ON [dbo].[Areas]
    ([ProjectId]);
GO

-- Creating foreign key on [AreaID] in table 'TestCases'
ALTER TABLE [dbo].[TestCases]
ADD CONSTRAINT [FK_TestCases_Areas]
    FOREIGN KEY ([AreaID])
    REFERENCES [dbo].[Areas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestCases_Areas'
CREATE INDEX [IX_FK_TestCases_Areas]
ON [dbo].[TestCases]
    ([AreaID]);
GO

-- Creating foreign key on [TestCaseID] in table 'StepDefinitions'
ALTER TABLE [dbo].[StepDefinitions]
ADD CONSTRAINT [FK_StepDefinitions_TestCases]
    FOREIGN KEY ([TestCaseID])
    REFERENCES [dbo].[TestCases]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StepDefinitions_TestCases'
CREATE INDEX [IX_FK_StepDefinitions_TestCases]
ON [dbo].[StepDefinitions]
    ([TestCaseID]);
GO

-- Creating foreign key on [TestCaseID] in table 'TestComposites'
ALTER TABLE [dbo].[TestComposites]
ADD CONSTRAINT [FK_TestComposite_TestCases]
    FOREIGN KEY ([TestCaseID])
    REFERENCES [dbo].[TestCases]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestComposite_TestCases'
CREATE INDEX [IX_FK_TestComposite_TestCases]
ON [dbo].[TestComposites]
    ([TestCaseID]);
GO

-- Creating foreign key on [TestRunID] in table 'TestComposites'
ALTER TABLE [dbo].[TestComposites]
ADD CONSTRAINT [FK_TestComposite_TestRuns]
    FOREIGN KEY ([TestRunID])
    REFERENCES [dbo].[TestRuns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------