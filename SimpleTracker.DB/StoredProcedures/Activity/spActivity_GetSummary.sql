create procedure [dbo].[spActivity_GetSummary] 
	@userId int
	,@from datetime = null
	,@to datetime = null
as
	select 
		a.Name as [Activity]
		,sum(isnull(e.Value, 0)) as [Value]
		,u.Name as [Unit]
	from 
		Entry e
		right outer join 
			Activity a on a.Id = e.ActivityId
		join 
			Unit u on u.Id = a.UnitId
	where 
		a.UserId = @userId
		and (@from is null or e.DateAdded >= @from)
		and (@to is null or e.DateAdded <= @to)
	group by 
		a.Name
		,u.Name
	option(recompile)

