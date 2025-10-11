create procedure [dbo].[spActivity_Update]
    @id int,
    @name nvarchar(30),
    @unitId int,
    @userId varchar(100)
as
    update [dbo].[Activity]
    set [Name] = @name,
        [UnitId] = @unitId
    where [Id] = @id 
        and [UserId] = @userId

return 0