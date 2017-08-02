CREATE TABLE [dbo].[User] (
    [Id_User]       INT            IDENTITY (1, 1) NOT NULL,
    [Nick]          NVARCHAR (100) NOT NULL,
    [Password]      NVARCHAR (100) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [SurnameFirst]  NVARCHAR (100) NULL,
    [SurnameSecond] NVARCHAR (100) NULL,
    [Email]         NVARCHAR (150) NOT NULL,
    [Telefono]      NCHAR (9)      NULL,
    [Direccion]     NVARCHAR (250) NULL,
    CONSTRAINT [PK_User_IdUser] PRIMARY KEY CLUSTERED ([Id_User] ASC)
);

