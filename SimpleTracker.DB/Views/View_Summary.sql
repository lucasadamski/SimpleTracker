CREATE VIEW [dbo].[View_Summary]
	AS 
	SELECT a.Name as [Activity], sum(e.Value) as [Value], 'Reps' as [Unit] -- to fix
	FROM Entry e
	join Activity a on a.Id = e.ActivityId
	join Unit u on u.Id = a.UnitId
	group by a.Name
