create procedure [dbo].[spActivity_GetId]
	@name nvarchar(30),
	@userId varchar(100)
as
	select top 1 [Id] 
	from [Activity] 
	where [Name] = @name
		and [UserId] = @userId
