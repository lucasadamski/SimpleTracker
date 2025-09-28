CREATE PROCEDURE [dbo].[spPurgeDb]
AS
	begin 
		truncate table [dbo].[Activity]
		truncate table [dbo].[Entry]
		truncate table [dbo].[Unit]
	end
RETURN 0
