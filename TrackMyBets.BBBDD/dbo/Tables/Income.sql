CREATE TABLE [dbo].[Income] (
    [Id_Income]           INT            IDENTITY (1, 1) NOT NULL,
    [Amount]              MONEY          NOT NULL,
    [Comment]             NVARCHAR (350) NULL,
    [Date_Income]         DATETIME       NOT NULL,
    [IsFreeBonus]         BIT            DEFAULT ((0)) NULL,
    [Id_RelUserBookmaker] INT            NOT NULL,
    CONSTRAINT [PK_Income_IdIncome] PRIMARY KEY CLUSTERED ([Id_Income] ASC),
    CONSTRAINT [FK_Income_RelUserBookmaker] FOREIGN KEY ([Id_RelUserBookmaker]) REFERENCES [dbo].[RelUserBookmaker] ([Id_RelUserBookmaker])
);

