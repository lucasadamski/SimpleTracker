CREATE PROCEDURE [dbo].[spPopulateWithTestData]
AS
	--if not exists (select 1 from [dbo].[Unit])
    begin
        insert into [dbo].[Unit] (Name)
        values ('times'), 
            ('minutes'),
            ('hours'),
            ('days'),
            ('weeks'),
            ('years')
    end

    --if not exists (select 1 from [dbo].[Activity])
    begin
        insert into [dbo].[Activity] (Name, UnitId, UserId)
        values ('push-ups', 1, 'testUser'), 
            ('running', 2, 'testUser'),
            ('reading', 3, 'testUser')
    end

    --if not exists (select 1 from [dbo].[Entry])
    begin
        insert into [dbo].[Entry] (Value, ActivityId, DateAdded)
        values (1 , 1, cast('2025-05-08 13:00:00' as datetime2)),
            (5, 2, cast('2025-05-09 14:00:00' as datetime2)),
            (10, 3, cast('2025-05-10 15:00:00' as datetime2))
    end
RETURN 0
