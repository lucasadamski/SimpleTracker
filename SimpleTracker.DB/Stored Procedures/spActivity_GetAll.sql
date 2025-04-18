CREATE PROCEDURE [dbo].[spActivity_GetAll]
AS
	SELECT 
		[Id], 
		[Name], 
		[UnitId] 
	FROM 
		[dbo].[Activity]
