CREATE TABLE [dbo].[RelUserBookmaker] (
    [Id_RelUserBookmaker] INT   IDENTITY (1, 1) NOT NULL,
    [Bankroll]            MONEY DEFAULT ((0)) NOT NULL,
    [Id_User]             INT   NOT NULL,
    [Id_Bookmaker]        INT   NOT NULL,
    CONSTRAINT [PK_RelUserBookmaker_IdRelUserBookmaker] PRIMARY KEY CLUSTERED ([Id_RelUserBookmaker] ASC),
    CONSTRAINT [FK_RelUserBookmaker_Bookmaker] FOREIGN KEY ([Id_Bookmaker]) REFERENCES [dbo].[Bookmaker] ([Id_Bookmaker]),
    CONSTRAINT [FK_RelUserBookmaker_User] FOREIGN KEY ([Id_User]) REFERENCES [dbo].[User] ([Id_User])
);

