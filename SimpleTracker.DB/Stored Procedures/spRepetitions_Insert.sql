CREATE PROCEDURE [dbo].[spRepetitions_Insert]
	@reps int,
	@itemId int
AS
	insert into [dbo].[Repetitions] (Repetitions, ItemId, DateAdded)
	values (@reps, @itemId, GETDATE())
RETURN 0
