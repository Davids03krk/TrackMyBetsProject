CREATE TABLE [dbo].[TypePickWinner] (
    [Id_Pick]       INT  NOT NULL,
    [HasHandicap]   BIT  DEFAULT ((0)) NULL,
    [ValueHandicap] REAL DEFAULT ((0)) NULL,
    [Id_WinnerTeam] INT  NOT NULL,
    CONSTRAINT [PK_TypePickWinner_IdPick] PRIMARY KEY CLUSTERED ([Id_Pick] ASC)
);

