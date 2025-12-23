CREATE PROCEDURE [dbo].[spUser_ReadUserIdFromToken]
	@token varchar(100)
AS
	SELECT u.Id
	FROM [dbo].[User] u
	WHERE [Token] = @token