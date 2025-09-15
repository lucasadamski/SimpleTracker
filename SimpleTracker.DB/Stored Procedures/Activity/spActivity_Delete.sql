CREATE PROCEDURE [dbo].[spActivity_Delete]
@id int
AS 
    delete from [dbo].[Activity]
    where [Id] = @id
return 0