CREATE TABLE [dbo].[Withdrawal] (
    [Id_Withdrawal]       INT            IDENTITY (1, 1) NOT NULL,
    [Amount]              MONEY          NOT NULL,
    [Comment]             NVARCHAR (350) NULL,
    [Date_Withdrawal]     DATETIME       NOT NULL,
    [Id_RelUserBookmaker] INT            NOT NULL,
    CONSTRAINT [PK_Withdrawal_IdWithdrawal] PRIMARY KEY CLUSTERED ([Id_Withdrawal] ASC),
    CONSTRAINT [FK_Withdrawal_RelUserBookmaker] FOREIGN KEY ([Id_RelUserBookmaker]) REFERENCES [dbo].[RelUserBookmaker] ([Id_RelUserBookmaker])
);

