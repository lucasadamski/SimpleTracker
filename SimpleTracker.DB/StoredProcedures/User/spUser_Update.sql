CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int, 
	@Login nvarchar(50), 
	@Password nvarchar(100), 
	@Token nvarchar(100),
	@RefreshToken nvarchar(100),
	@RefreshTokenExpiryDate datetime2
AS
	UPDATE [dbo].[User] 
	SET 
		 [Login] = @Login
		,[Password] = @Password
		,[Token] = @Token
		,[RefreshToken] = @RefreshToken 
		,[RefreshTokenExpiryDate] = @RefreshTokenExpiryDate
	WHERE [Id] = @Id
RETURN 1
