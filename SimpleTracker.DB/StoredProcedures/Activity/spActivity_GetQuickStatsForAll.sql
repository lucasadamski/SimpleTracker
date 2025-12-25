CREATE PROCEDURE [dbo].[spActivity_GetQuickStatsForAllActivities]
	@userId int
AS
	declare @today date = cast(getdate() as date)
	declare @weekStart date = DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0)
	declare @monthStart date = DATEADD(DAY, 1, EOMONTH(GETDATE(), -1)) 

	select 
		 a.Name as [ActivityName]
		,u.Name as [UnitName]
		,sum(
			case when cast(e.[DateAdded] as date) = @today then 
				e.[Value] 
			else 
				0 
			end
		) as [TodayValue]
		,sum(
			case when cast(e.DateAdded as date) >= @weekStart then 
				e.[Value]
			else 
				0
			end
		) as [ThisWeekValue]
		,sum(
			case when cast(e.[DateAdded] as date) >= @monthStart then 
				e.[Value]
			else 
				0
			end
		) as [ThisMonthValue]
		,sum(e.Value) as [AllTimeValue]
	from 
		[dbo].[Activity] a
			join [dbo].[Unit] u on a.[UnitId] = u.[Id]
			join [dbo].[Entry] e on a.[Id] = e.[ActivityId]
	where 
		a.[UserId] = @userId
	group by 
		a.[Name],
		u.[Name]