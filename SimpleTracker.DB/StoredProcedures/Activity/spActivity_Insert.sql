CREATE PROCEDURE [dbo].[spActivity_Insert]
	@name nvarchar(30),
	@unitId int = 1,
	@userId int
AS
	insert into [dbo].[Activity] ([Name], [UnitId], [UserId])
	values (@name, @unitId, @userId)
RETURN 0
