 create procedure [dbo].[spEntriesDto_Read]
	 @userId varchar(100),
	 @from datetime = null,
	 @to datetime = null
as
	select 
		e.[Value]
		,u.[Name] as [Unit]
		,a.[Name] as [Activity]
		,e.[DateAdded]
	from
		[dbo].[Entry] e
	join [dbo].[Activity] a on a.[Id] = e.[ActivityId]
	join [dbo].[Unit] u on a.UnitId = u.Id
	where 
		a.UserId = @userId
		and (@from is null or e.DateAdded >= @from)
		and (@to is null or e.DateAdded <= @to)
	option(recompile)