create procedure [dbo].[spEntry_Delete] 
    @id int
as
    delete from [dbo].[Entry]
    where [Id] = @id
return 0