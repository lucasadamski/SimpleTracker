create procedure [dbo].[spActivity_GetId]
	@name nvarchar(30),
	@userId int
as
	select top 1 [Id] 
	from [Activity] 
	where [Name] = @name
		and [UserId] = @userId
