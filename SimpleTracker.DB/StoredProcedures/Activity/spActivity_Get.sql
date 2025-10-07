create procedure [dbo].[spActivity_Get]
	@id int,
	@userId varchar(100)
as
	select [Id],
	[Name], 
	[UnitId],
	[UserId]
	from [Activity] 
	where [Id] = @id
		and [UserId] = @userId
