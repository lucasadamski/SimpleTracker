CREATE PROCEDURE [dbo].[spEntry_Insert]
	@value int,
	@activityId int
AS
	insert into [dbo].[Entry] (Value, ActivityId, DateAdded)
	values (@value, @activityId, GETDATE())
RETURN 0
 