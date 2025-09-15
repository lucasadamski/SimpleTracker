create procedure [dbo].[spEntry_Update] 
    @id int,
    @value char(50)
as
    update [dbo].[Entry]
    set [Value] = @value
    where [Id] = @id
return 0