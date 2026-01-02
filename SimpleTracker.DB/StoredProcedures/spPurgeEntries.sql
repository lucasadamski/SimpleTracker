CREATE PROCEDURE [dbo].[spPurgeEntries]
AS
	TRUNCATE TABLE [dbo].[Entry]
RETURN 0
