CREATE TABLE [dbo].[Activity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nchar(30) not null,
	[UnitId] int not null, 
    [UserId] NCHAR(100) NOT NULL
)
