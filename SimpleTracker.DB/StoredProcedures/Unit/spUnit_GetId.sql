CREATE PROCEDURE [dbo].[spUnit_GetId]
	@name nchar(30)
AS
	select top 1 Id 
	from Unit
	where Name = @name
