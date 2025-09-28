CREATE PROCEDURE [dbo].[spPopulateWithTestData]
AS
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

    if not exists (select 1 from [dbo].[Entry])
    begin
        insert into [dbo].[Entry] (Value, ActivityId) -- datetime?
        values (1 , 1),
            (1, 2),
            (1, 3)
    end
RETURN 0
