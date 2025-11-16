create procedure [dbo].[spEntry_Update] 
    @id int,
    @value int
as
    update [dbo].[Entry]
    set [Value] = @value
    where [Id] = @id
return 1