create procedure [dbo].[spSummary_Get]
@from datetime = null,
@to datetime = null
AS	
	
if (@from is null and @to is null) 
begin
	select a.Name as [Activity], sum(isnull(e.Value, 0)) as [Value], u.Name as [Unit]
	from Entry e
	right outer join Activity a on a.Id = e.ActivityId
	join Unit u on u.Id = a.UnitId
	group by a.Name, u.Name
end
else 
begin
	select a.Name as [Activity], sum(isnull(e.Value, 0)) as [Value], u.Name as [Unit]
	from Entry e
	right outer join Activity a on a.Id = e.ActivityId
	join Unit u on u.Id = a.UnitId
	where e.DateAdded <= @from AND e.DateAdded >= @to
	group by a.Name, u.Name
end