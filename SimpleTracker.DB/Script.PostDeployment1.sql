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


if not exists (select 1 from [dbo].[Activity])
begin
    insert into [dbo].[Activity] (Name, UnitId, UserId)
    values ('push-ups', 1, 'testUser'), 
        ('running', 1, 'testUser'),
        ('reading', 1, 'testUser')
end
GO


GO
