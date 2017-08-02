CREATE TABLE [dbo].[Pick] (
    [Id_Pick]     INT IDENTITY (1, 1) NOT NULL,
    [Id_Bet]      INT NOT NULL,
    [Id_Event]    INT NOT NULL,
    [Id_PickType] INT NOT NULL,
    CONSTRAINT [PK_Pick_IdPick] PRIMARY KEY CLUSTERED ([Id_Pick] ASC),
    CONSTRAINT [FK_Pick_Bet] FOREIGN KEY ([Id_Bet]) REFERENCES [dbo].[Bet] ([Id_Bet]),
    CONSTRAINT [FK_Pick_Event] FOREIGN KEY ([Id_Event]) REFERENCES [dbo].[Event] ([Id_Event]),
    CONSTRAINT [FK_Pick_PickType] FOREIGN KEY ([Id_PickType]) REFERENCES [dbo].[PickType] ([Id_PickType])
);

