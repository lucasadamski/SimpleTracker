 CREATE PROCEDURE [dbo].[spEntry_GetAll]
AS
	SELECT 
		[Id]
		,[Value]
		,[ActivityId]
		,[DateAdded]
	FROM
		[dbo].[Entry]
