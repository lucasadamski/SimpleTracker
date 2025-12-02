CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Login] nvarchar(30) not null,
	[Password] nvarchar(100) not null,
	[Token] nvarchar(100) null,
	[RefreshToken] nvarchar(100) null,
	[RefreshTokenExpiryDate] datetime2 null
)