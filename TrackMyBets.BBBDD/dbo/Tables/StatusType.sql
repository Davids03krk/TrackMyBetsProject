CREATE TABLE [dbo].[StatusType] (
    [Id_StatusType]   INT           IDENTITY (1, 1) NOT NULL,
    [Desc_StatusType] NVARCHAR (25) NOT NULL,
    CONSTRAINT [PK_StatusType_IdStatusType] PRIMARY KEY CLUSTERED ([Id_StatusType] ASC)
);

