CREATE PROCEDURE [dbo].[spActivity_GetAll]
AS
	SELECT 
		[Id], 
		[Name], 
		[UnitId] ,
		[UserId]
	FROM 
		[dbo].[Activity]
