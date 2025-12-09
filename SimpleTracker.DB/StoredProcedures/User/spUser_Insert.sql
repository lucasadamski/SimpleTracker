CREATE PROCEDURE [dbo].[spUser_Insert]
	@login VARCHAR(100),
	@password VARCHAR(100),
	@token VARCHAR(100),
	@refreshToken VARCHAR(100),
	@refreshTokenExpiryDate DATETIME2
AS
	INSERT INTO [dbo].[User] 
		([Login]
		,[Password]
		,[Token]
		,[RefreshToken]
		,[RefreshTokenExpiryDate])
	VALUES 
		(@login
		,@password
		,@token
		,@refreshToken
		,@refreshTokenExpiryDate)
RETURN 0
