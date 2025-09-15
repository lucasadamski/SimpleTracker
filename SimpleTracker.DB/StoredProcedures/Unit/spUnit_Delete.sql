create procedure [dbo].[spUnit_Delete] 
    @id int
as 
    delete from [dbo].[Unit]
    where [Id] = @id
return 0