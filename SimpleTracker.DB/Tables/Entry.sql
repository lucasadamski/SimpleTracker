CREATE TABLE [dbo].[Entry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Value] int not null,
	[ActivityId] int not null,
	[DateAdded] datetime2 not null
)
