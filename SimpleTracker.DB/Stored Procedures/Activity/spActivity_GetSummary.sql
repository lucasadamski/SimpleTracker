create procedure [dbo].[spActivity_GetSummary] 
	@userId nchar(100)
	,@from datetime = null
	,@to datetime = null
as	
	
if (@from is null and @to is null) 
	begin
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
		group by 
			a.Name
			,u.Name
	end
else if(@from is not null and @to is null)
	begin
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
			and e.DateAdded <= @from 
		group by 
			a.Name
			,u.Name
	end
else if(@from is null and @to is not null)
	begin
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
			and e.DateAdded >= @to
		group by 
			a.Name
			,u.Name
	end
else
	begin
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
			and e.DateAdded <= @from 
			and e.DateAdded >= @to
		group by 
			a.Name
			,u.Name
	end