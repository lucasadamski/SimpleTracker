CREATE TABLE [dbo].[Activity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] nvarchar(30) not null,
	[UnitId] int not null, 
    [UserId] varchar(100) NOT NULL
)
