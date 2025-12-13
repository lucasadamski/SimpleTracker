/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from [dbo].[Unit])
begin
    insert into [dbo].[Unit] (Name)
    values ('times'), 
        ('minutes'),
        ('hours'),
        ('days'),
        ('weeks'),
        ('years')
end
go

if not exists (select 1 from [dbo].[Activity])
begin
    insert into [dbo].[Activity] (Name, UnitId, UserId)
    values ('push-ups', 1, 'testUser'), 
        ('running', 2, 'testUser'),
        ('reading', 3, 'testUser')
end
go

if not exists (select 1 from [dbo].[Entry])
insert into [dbo].[Entry] (Value, ActivityId, DateAdded)
        values (1 , 1, cast('2025-05-08 13:00:00' as datetime2)),
            (5, 2, cast('2025-05-09 14:00:00' as datetime2)),
            (10, 3, cast('2025-05-10 15:00:00' as datetime2))
go

if not exists (select 1 from [dbo].[User])
insert into [dbo].[User] (Login, Password, Token, RefreshToken, RefreshTokenExpiryDate)
        values ('test' , 'test', '', '', cast('2025-12-30 13:00:00' as datetime2))
go


