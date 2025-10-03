namespace SimpleTracker.DAL.Interfaces
{
    public interface ITestDal
    {
        bool PopulateDatabase();
        bool PurgeDatabase();
    }
}