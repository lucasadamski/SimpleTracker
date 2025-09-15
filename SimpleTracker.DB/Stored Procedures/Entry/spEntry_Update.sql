create procedure [dbo].[spEntry_Update] 
    @id int,
    @name char(50)
as
    update [dbo].[Entry]
    set [Name] = @name
    where [Id] = @id
return 0