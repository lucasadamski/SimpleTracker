create procedure as [dbo].[spActivity_Update]
    @id int
    ,@name char(30)
as
    update [dbo].[Activity] (Name)
    set [Name] = @name
    where [Id] = @id

return 0