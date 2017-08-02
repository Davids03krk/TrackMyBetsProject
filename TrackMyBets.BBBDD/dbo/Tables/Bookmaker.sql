CREATE TABLE [dbo].[Bookmaker] (
    [Id_Bookmaker]   INT            IDENTITY (1, 1) NOT NULL,
    [Desc_Bookmaker] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_Bookmaker_IdBookmaker] PRIMARY KEY CLUSTERED ([Id_Bookmaker] ASC)
);

