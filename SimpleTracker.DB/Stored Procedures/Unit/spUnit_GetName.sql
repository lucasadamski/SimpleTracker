CREATE PROCEDURE [dbo].[spUnit_GetName]
	@id int = 0

AS
	select 
		top 1 [Name]
	from 
		[dbo].[Unit]
	where 
		[Id] = @id

