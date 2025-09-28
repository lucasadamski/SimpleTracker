using SimpleTracker.DAL;

namespace SimpleTracker.DbIntegrationTest;

public class DbIntegration
{
    [Fact]
    public void Test1()
    {
        ISqlDataAccess sqlDataAccess;
        sqlDataAccess = new SqlDataAccess(@"Data Source=DESKTOP-1KHRBVS\SQLEXPRESS;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        // purge data base

        // populate with test data 

        // perform test on separate test

    }
}
