CREATE PROCEDURE [dbo].[spActivity_GetId]
	@name nvarchar(30),
	@userId varchar(100)
AS
	SELECT top 1 Id 
	from Activity 
	where Name = @name
		and UserId = @userId
