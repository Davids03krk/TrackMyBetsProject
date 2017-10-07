CREATE TABLE [dbo].[User] (
    [Id_User]       INT             IDENTITY (1, 1) NOT NULL,
    [Nick]          NVARCHAR (100)  NOT NULL,
    [Name]          NVARCHAR (100)  NULL,
    [SurnameFirst]  NVARCHAR (100)  NULL,
    [SurnameSecond] NVARCHAR (100)  NULL,
    [Phone]         NCHAR (9)       NULL,
    [Address]       NVARCHAR (250)  NULL,
    [PasswordHash]  VARBINARY (100) NULL,
    [PasswordSalt]  VARBINARY (100) NULL,
    [Email]         NVARCHAR (150)  NULL,
    CONSTRAINT [PK_User_IdUser] PRIMARY KEY CLUSTERED ([Id_User] ASC)
);







