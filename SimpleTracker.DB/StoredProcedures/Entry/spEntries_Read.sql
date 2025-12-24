 create procedure [dbo].[spEntries_Read]
	 @userId int,
	 @from datetime = null,
	 @to datetime = null
as
	select 
		e.[Id]
		,[Value]
		,[ActivityId]
		,[DateAdded]
	from
		[dbo].[Entry] e
	join [dbo].[Activity] a on a.[Id] = e.[ActivityId]
	where 
		a.UserId = @userId
		and (@from is null or e.DateAdded >= @from)
		and (@to is null or e.DateAdded <= @to)
	option(recompile)



--where 
--		a.UserId = @userId
--		and (@from is null or e.DateAdded >= @from)
--		and (@to is null or e.DateAdded <= @to)