CREATE PROCEDURE [dbo].[spEntry_Insert]
	@value int,
	@activityId int,
	@dateAdded datetime
AS
	insert into [dbo].[Entry] (Value, ActivityId, DateAdded)
	values (@value, @activityId, @dateAdded)
RETURN 1