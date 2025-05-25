CREATE PROCEDURE [dbo].[spActivity_Insert]
	@name char(30),
	@unitId int = 1
AS
	insert into [dbo].[Activity] (Name, UnitId)
	values (@name, @unitId)
RETURN 0
