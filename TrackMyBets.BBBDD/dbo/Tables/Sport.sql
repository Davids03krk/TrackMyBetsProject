CREATE TABLE [dbo].[Sport] (
    [Id_Sport]             INT           IDENTITY (1, 1) NOT NULL,
    [Desc_Sport]           NVARCHAR (75) NOT NULL,
    [DurationMatchInHours] REAL          DEFAULT ((2.5)) NULL,
    CONSTRAINT [PK_Sport_IdSport] PRIMARY KEY CLUSTERED ([Id_Sport] ASC)
);

