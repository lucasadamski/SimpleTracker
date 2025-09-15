CREATE PROCEDURE [dbo].[spActivity_Insert]
	@name char(30),
	@unitId int = 1,
	@userId nchar(100)
AS
	insert into [dbo].[Activity] (Name, UnitId, UserId)
	values (@name, @unitId, @userId)
RETURN 0
