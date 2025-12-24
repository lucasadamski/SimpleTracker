create procedure [dbo].[spActivity_Get]
	@id int,
	@userId int
as
	select [Id],
	[Name], 
	[UnitId],
	[UserId]
	from [Activity] 
	where [Id] = @id
		and [UserId] = @userId
