CREATE PROCEDURE [dbo].[spItem_Insert]
	@name char(30)
AS
	insert into [dbo].[Item] (Name)
	values (@name)
RETURN 0
