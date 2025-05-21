CREATE VIEW [dbo].[EntrySummaryAllTime]
	AS 
	SELECT a.Name, sum(e.Value) as Value, 'Reps' as Reps FROM Entry e
	join Activity a on a.Id = e.ActivityId
	group by a.Name
