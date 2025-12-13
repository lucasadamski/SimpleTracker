CREATE PROCEDURE [dbo].[spUser_ReadByToken]
	@token varchar(100)
AS
	SELECT * 
	FROM [dbo].[User]
	WHERE [Token] = @token
