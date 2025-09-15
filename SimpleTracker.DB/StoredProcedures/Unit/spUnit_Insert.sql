CREATE PROCEDURE [dbo].[spUnit_Insert]
	@name int = 0
AS
	insert into [dbo].[Unit] (Name)
	values (@name)
RETURN 0
