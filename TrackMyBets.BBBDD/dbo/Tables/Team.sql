CREATE TABLE [dbo].[Team] (
    [Id_Team]      INT            IDENTITY (1, 1) NOT NULL,
    [Desc_Team]    NVARCHAR (125) NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [City]         NVARCHAR (100) NOT NULL,
    [Stadium]      NVARCHAR (125) NULL,
    [Abbreviation] NCHAR (3)      NOT NULL,
    [Id_Sport]     INT            NOT NULL,
    CONSTRAINT [PK_Team_IdTeam] PRIMARY KEY CLUSTERED ([Id_Team] ASC),
    CONSTRAINT [FK_Team_Sport] FOREIGN KEY ([Id_Sport]) REFERENCES [dbo].[Sport] ([Id_Sport])
);

