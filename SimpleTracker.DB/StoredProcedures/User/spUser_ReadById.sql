CREATE PROCEDURE [dbo].[spUser_ReadById]
	@id INT
AS
	SELECT * 
	FROM [dbo].[User]
	WHERE [Id] = @id

