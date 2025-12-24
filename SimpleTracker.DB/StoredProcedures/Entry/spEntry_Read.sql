 create procedure [dbo].[spEntry_Read]
	 @id int,
	 @userId int
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
		and e.[Id] = @id
