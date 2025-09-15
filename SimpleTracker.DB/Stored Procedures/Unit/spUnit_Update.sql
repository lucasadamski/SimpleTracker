create procedure [dbo].[spUnit_Update] 
    @id int,
    @name char(50)
as
    update [dbo].[Unit]
    set [Name] = @name
    where [Id] = @id
return 0