CREATE PROCEDURE [dbo].[spUser_ReadByCredentials]
	@login varchar(100),
	@password varchar(100)
AS
	SELECT * 
	FROM [dbo].[User]
	WHERE [Login] = @login
		AND [Password] = @password
