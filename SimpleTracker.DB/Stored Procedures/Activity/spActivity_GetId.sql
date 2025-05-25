CREATE PROCEDURE [dbo].[spActivity_GetId]
	@name nchar(30)
AS
	SELECT top 1 Id 
	from Activity 
	where Name = @name
