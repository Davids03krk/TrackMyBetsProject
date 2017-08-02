CREATE TABLE [dbo].[PickType] (
    [Id_PickType]   INT           IDENTITY (1, 1) NOT NULL,
    [Desc_PickType] NVARCHAR (75) NOT NULL,
    CONSTRAINT [PK_PickType_IdPickType] PRIMARY KEY CLUSTERED ([Id_PickType] ASC)
);

