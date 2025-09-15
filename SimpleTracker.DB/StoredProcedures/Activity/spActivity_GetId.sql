CREATE PROCEDURE [dbo].[spActivity_GetId]
	@name nchar(30),
	@userId nchar(100)
AS
	SELECT top 1 Id 
	from Activity 
	where Name = @name
		and UserId = @userId
