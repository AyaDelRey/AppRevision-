﻿/*
Deployment script for Database

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Database"
:setvar DefaultFilePrefix "Database"
:setvar DefaultDataPath "C:\Users\m.chanu\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\m.chanu\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating Table [dbo].[Comment]...';


GO
CREATE TABLE [dbo].[Comment] (
    [Comment_Id] UNIQUEIDENTIFIER NOT NULL,
    [Title]      NVARCHAR (64)    NOT NULL,
    [Content]    NVARCHAR (512)   NOT NULL,
    [Concern]    UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]  DATETIME2 (7)    NOT NULL,
    [CreatedBy]  UNIQUEIDENTIFIER NULL,
    [Note]       TINYINT          NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Comment_Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Recettes]...';


GO
CREATE TABLE [dbo].[Recettes] (
    [Recette_Id]   UNIQUEIDENTIFIER NOT NULL,
    [Titre]        NVARCHAR (64)    NOT NULL,
    [Description]  NVARCHAR (512)   NULL,
    [Instructions] NVARCHAR (MAX)   NOT NULL,
    [CreatedAt]    DATETIME2 (7)    NOT NULL,
    [CreatedBy]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Recette] PRIMARY KEY CLUSTERED ([Recette_Id] ASC)
);


GO
PRINT N'Creating Table [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [User_Id]    UNIQUEIDENTIFIER NOT NULL,
    [First_Name] NVARCHAR (64)    NOT NULL,
    [Last_Name]  NVARCHAR (64)    NOT NULL,
    [Email]      NVARCHAR (320)   NOT NULL,
    [Password]   VARBINARY (64)   NOT NULL,
    [Salt]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]  DATETIME2 (7)    NOT NULL,
    [DisabledAt] DATETIME2 (7)    NULL,
    [Role]       NVARCHAR (8)     NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([User_Id] ASC),
    CONSTRAINT [UK_User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Comment]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD DEFAULT NEWID() FOR [Comment_Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Comment]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Recettes]...';


GO
ALTER TABLE [dbo].[Recettes]
    ADD DEFAULT NEWID() FOR [Recette_Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Recettes]...';


GO
ALTER TABLE [dbo].[Recettes]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT NEWID() FOR [User_Id];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT 'User' FOR [Role];


GO
PRINT N'Creating Foreign Key [dbo].[FK_Comment_User]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [FK_Comment_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([User_Id]) ON DELETE SET NULL;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Comment_Recette]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [FK_Comment_Recette] FOREIGN KEY ([Concern]) REFERENCES [dbo].[Recettes] ([Recette_Id]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [dbo].[FK_Recette_User]...';


GO
ALTER TABLE [dbo].[Recettes]
    ADD CONSTRAINT [FK_Recette_User] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([User_Id]) ON DELETE SET NULL;


GO
PRINT N'Creating Check Constraint [dbo].[CK_Comment_Note]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [CK_Comment_Note] CHECK ([Note] IS NULL OR [Note] BETWEEN 0 AND 5);


GO
PRINT N'Creating Function [dbo].[SF_SaltAndHash]...';


GO
CREATE FUNCTION [dbo].[SF_SaltAndHash]
(
	@password NVARCHAR(32),
	@salt UNIQUEIDENTIFIER
)
RETURNS VARBINARY(64)
AS
BEGIN
	DECLARE @saltedPassword NVARCHAR(68)
	SET @saltedPassword = CONCAT(@password,@salt)
	RETURN HASHBYTES('SHA2_512',@saltedPassword)
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_Delete]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_Delete]
	@recette_id UNIQUEIDENTIFIER
AS
BEGIN
	DELETE 
		FROM [Recettes]
		WHERE [Recette_Id] = @recette_id
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_GetAll]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_GetAll]
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_GetById]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_GetById]
	@recette_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
	WHERE [Recette_Id] = @recette_id
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_GetByUserId]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_GetByUserId]
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[Recette_Id],
			[Titre], 
			[Description],
			[Instructions],
			[CreatedBy],
			[CreatedAt]
		FROM [Recettes]
	WHERE [CreatedBy] = @user_id
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_Insert]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_Insert]
	@titre NVARCHAR(64),
	@description NVARCHAR(512) NULL,-- NULL manquant pour la description
	@instructions NVARCHAR(MAX),	-- NULL n'est pas valable pour les instructions
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	INSERT INTO [Recettes] ([Titre],[Description],[Instructions],[CreatedBy])
		OUTPUT [inserted].[Recette_Id]
		VALUES
			(@titre, @description, @instructions, @user_id);
END
GO
PRINT N'Creating Procedure [dbo].[SP_Recette_Update]...';


GO
CREATE PROCEDURE [dbo].[SP_Recette_Update]
	@recette_id UNIQUEIDENTIFIER,
	@titre NVARCHAR(64),
	@description NVARCHAR(512) NULL,
	@instructions NVARCHAR(MAX)
AS
BEGIN
	UPDATE [Recettes]
		SET [titre] = @titre,
			[Description] = @description,
			[Instructions] = @instructions
		WHERE [Recette_Id] = @recette_id
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_CheckPassword]...';


GO
CREATE PROCEDURE [dbo].[SP_User_CheckPassword]
	@email NVARCHAR(320),
	@password NVARCHAR(32)
AS
BEGIN
	SELECT [User_Id]
		FROM [User]
		WHERE	[Email] = @email
			AND [Password] = [dbo].[SF_SaltAndHash](@password,[Salt])
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_Delete]...';


GO
CREATE PROCEDURE [dbo].[SP_User_Delete]
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE [Recettes]
		SET [CreatedBy] = NULL
		WHERE [CreatedBy] = @user_id;
	UPDATE [Comment]
		SET [CreatedBy] = NULL
		WHERE [CreatedBy] = @user_id;
	UPDATE [User]
		SET [DisabledAt] = GETDATE()
		WHERE [User_Id] = @user_id;
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_GetAll]...';


GO
CREATE PROCEDURE [dbo].[SP_User_GetAll]
AS
BEGIN
	SELECT [User_Id], [First_Name], [Last_Name], [Email], [CreatedAt], [DisabledAt], [Role] FROM [User]
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_GetById]...';


GO
CREATE PROCEDURE [dbo].[SP_User_GetById]
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	SELECT	[User_Id],
			[First_Name],
			[Last_Name],
			[Email], 
			[CreatedAt], 
			[DisabledAt],
			[Role]
		FROM [User]
		WHERE [User_Id] = @user_id
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_Insert]...';


GO
CREATE PROCEDURE [dbo].[SP_User_Insert]
	@first_name NVARCHAR(64),
	@last_name NVARCHAR(64),
	@email NVARCHAR(320),
	@password NVARCHAR(32)
AS
BEGIN
	DECLARE @salt UNIQUEIDENTIFIER
	SET @salt = NEWID()
	INSERT INTO [User] ([First_Name],[Last_Name],[Email],[Password],[Salt])
	OUTPUT [inserted].[User_Id]
	VALUES (@first_name, @last_name, @email, [dbo].[SF_SaltAndHash](@password, @salt), @salt)
END
GO
PRINT N'Creating Procedure [dbo].[SP_User_Update]...';


GO
CREATE PROCEDURE [dbo].[SP_User_Update]
	@first_name NVARCHAR(64),
	@last_name NVARCHAR(64),
	@email NVARCHAR(320),
	@user_id UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE [User]
		SET	[First_Name] = @first_name,
			[Last_Name] = @last_name,
			[Email] = @email
		WHERE [User_Id] = @user_id
END
GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
