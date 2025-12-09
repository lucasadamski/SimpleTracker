CREATE PROCEDURE [dbo].[spUser_ReadByLogin]
	@login VARCHAR(100)
AS
	SELECT * 
	FROM [dbo].[User]
	WHERE [Login] = @login

