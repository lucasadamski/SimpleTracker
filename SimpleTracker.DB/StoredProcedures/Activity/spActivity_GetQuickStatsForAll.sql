CREATE PROCEDURE [dbo].[spActivity_GetQuickStatsForAllActivities]
	@userId int
AS
	declare @today date = cast(GETDATE() as date)
	declare @weekStart date = DATEADD(DAY, -((DATEPART(WEEKDAY, @today) + @@DATEFIRST - 2) % 7), CAST(@today AS date))
	declare @monthStart date = DATEADD(DAY, 1, EOMONTH(@today, -1))

	declare @yesterday date = dateadd(day, -1, @today)
	declare @previousWeek date = dateadd(week, -1, @weekStart)
	declare @previousMonth date = dateadd(month, -1, @monthStart)

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
			case when cast(e.DateAdded as date) = @yesterday then 
				e.[Value]
			else 
				0
			end
		) as [YesterdayValue]
		,sum(
			case when cast(e.DateAdded as date) >= @weekStart then 
				e.[Value]
			else 
				0
			end
		) as [ThisWeekValue]
		,sum(
			case when cast(e.DateAdded as date) >= @previousWeek and cast(e.DateAdded as date) < @weekStart then 
				e.[Value]
			else 
				0
			end
		) as [LastWeekValue]
		,sum(
			case when cast(e.[DateAdded] as date) >= @monthStart then 
				e.[Value]
			else 
				0
			end
		) as [ThisMonthValue]
		,sum(
			case when cast(e.DateAdded as date) >= @previousMonth and cast(e.DateAdded as date) < @monthStart then 
				e.[Value]
			else 
				0
			end
		) as [LastMonthValue]
		,coalesce(sum(e.Value), 0) as [AllTimeValue]
	from 
		[dbo].[Activity] a
			join [dbo].[Unit] u on a.[UnitId] = u.[Id]
			left join [dbo].[Entry] e on a.[Id] = e.[ActivityId]
	where 
		a.[UserId] = @userId
	group by 
		a.[Name],
		u.[Name]