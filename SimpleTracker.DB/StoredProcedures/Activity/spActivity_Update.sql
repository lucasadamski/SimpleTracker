create procedure [dbo].[spActivity_Update]
    @id int,
    @name nvarchar(30)
as
    update [dbo].[Activity]
    set [Name] = @name
    where [Id] = @id

return 0