using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DbIntegrationTest;

public class DbIntegration
{
    private ILogger logger;
    private SqlDataAccess sqlDataAccess;
    private TestDal testDal;

    public DbIntegration()
    {
        logger = A.Fake<ILogger<SqlDataAccess>>();
        sqlDataAccess = new SqlDataAccess(@"Data Source=DESKTOP-1KHRBVS\SQLEXPRESS;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False", logger);
        testDal = new TestDal(sqlDataAccess, logger);
    }

    private void PurgeDatabase() => testDal.PurgeDatabase();
    
    private void PopulateDatabase() => testDal.PopulateDatabase();
}
