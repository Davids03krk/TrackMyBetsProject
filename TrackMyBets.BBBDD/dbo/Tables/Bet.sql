CREATE TABLE [dbo].[Bet] (
    [Id_Bet]              INT      IDENTITY (1, 1) NOT NULL,
    [Id_RelUserBookmaker] INT      NOT NULL,
    [Date_Bet]            DATETIME NOT NULL,
    [IsLiveBet]           BIT      DEFAULT ((0)) NULL,
    [IsCombinedBet]       BIT      DEFAULT ((0)) NULL,
    [Stake]               MONEY    NOT NULL,
    [Quota]               REAL     NOT NULL,
    [Profits]             MONEY    DEFAULT ((0)) NULL,
    [Benefits]            MONEY    DEFAULT ((0)) NULL,
    [Id_StatusType]       INT      DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Bet_IdBet] PRIMARY KEY CLUSTERED ([Id_Bet] ASC),
    CONSTRAINT [FK_Bet_RelUserBookmaker] FOREIGN KEY ([Id_RelUserBookmaker]) REFERENCES [dbo].[RelUserBookmaker] ([Id_RelUserBookmaker]),
    CONSTRAINT [FK_Bet_StatusType] FOREIGN KEY ([Id_StatusType]) REFERENCES [dbo].[StatusType] ([Id_StatusType])
);

