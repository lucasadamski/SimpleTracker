CREATE PROCEDURE [dbo].[spActivity_GetAll]
@userId nchar(100)
AS
	SELECT 
		[Id],
		[Name],
		[UnitId],
		[UserId]
	FROM 
		[dbo].[Activity]
	WHERE
		[UserId] = @userId