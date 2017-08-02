CREATE TABLE [dbo].[TypePickTotalPoints] (
    [Id_Pick]          INT  NOT NULL,
    [IsOver]           BIT  DEFAULT ((0)) NULL,
    [IsUnder]          BIT  DEFAULT ((0)) NULL,
    [ValueTotalPoints] REAL NOT NULL,
    CONSTRAINT [PK_TypePickTotalPoints_IdPick] PRIMARY KEY CLUSTERED ([Id_Pick] ASC)
);

