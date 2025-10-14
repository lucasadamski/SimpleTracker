 create procedure [dbo].[spEntry_GetAll]
 @userId varchar(100)
as
	select 
		e.[Id]
		,[Value]
		,[ActivityId]
		,[DateAdded]
	from
		[dbo].[Entry] e
	join [dbo].[Activity] a on a.[Id] = e.[ActivityId]
	where a.[UserId] = @userId
