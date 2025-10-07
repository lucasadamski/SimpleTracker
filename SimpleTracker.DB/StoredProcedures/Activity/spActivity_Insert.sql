CREATE PROCEDURE [dbo].[spActivity_Insert]
	@name nvarchar(30),
	@unitId int = 1,
	@userId varchar(100)
AS
	insert into [dbo].[Activity] ([Name], [UnitId], [UserId])
	values (@name, @unitId, @userId)
RETURN 0
