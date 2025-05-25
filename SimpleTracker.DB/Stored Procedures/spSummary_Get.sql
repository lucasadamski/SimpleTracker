CREATE PROCEDURE [dbo].[spSummary_Get]
@From DateTime2,
@To DateTime2
AS
	IF (@From is null AND @To is null) 
	Begin 
		SELECT a.Name as [Activity], sum(e.Value) as [Value], 'Reps' as [Unit] -- to fix
		FROM Entry e
		join Activity a on a.Id = e.ActivityId
		join Unit u on u.Id = a.UnitId
		group by a.Name
	End
	ELSE
	BEGIN
		SELECT a.Name as [Activity], sum(e.Value) as [Value], 'Reps' as [Unit] -- to fix
		FROM Entry e
		join Activity a on a.Id = e.ActivityId
		join Unit u on u.Id = a.UnitId
		where e.DateAdded >= @From AND e.DateAdded <= @To
		group by a.Name
		
	END