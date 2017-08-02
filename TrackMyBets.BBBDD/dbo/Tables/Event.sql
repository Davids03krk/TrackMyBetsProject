CREATE TABLE [dbo].[Event] (
    [Id_Event]     INT            IDENTITY (1, 1) NOT NULL,
    [Comment]      NVARCHAR (350) NULL,
    [Date_Event]   DATETIME       NULL,
    [Id_LocalTeam] INT            NOT NULL,
    [Id_VisitTeam] INT            NOT NULL,
    [Id_Sport]     INT            NOT NULL,
    CONSTRAINT [PK_Event_IdEvent] PRIMARY KEY CLUSTERED ([Id_Event] ASC),
    CONSTRAINT [FK_Event_Sport] FOREIGN KEY ([Id_Sport]) REFERENCES [dbo].[Sport] ([Id_Sport]),
    CONSTRAINT [FK_Event_TeamLocal] FOREIGN KEY ([Id_LocalTeam]) REFERENCES [dbo].[Team] ([Id_Team]),
    CONSTRAINT [FK_Events_TeamVisit] FOREIGN KEY ([Id_VisitTeam]) REFERENCES [dbo].[Team] ([Id_Team])
);

