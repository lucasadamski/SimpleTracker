using FakeItEasy;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DbIntegrationTest;

public class DbIntegration
{
    ILogger logger = A.Fake<ILogger<SqlDataAccess>>();
    [Fact]
    public void Test1()
    {
        ISqlDataAccess sqlDataAccess;
        sqlDataAccess = new SqlDataAccess(@"Data Source=DESKTOP-1KHRBVS\SQLEXPRESS;Initial Catalog=SimpleTrackerTest;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False", logger);
        // purge data base

        // populate with test data 

        // perform test on separate test

    }
}
