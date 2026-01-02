namespace SimpleTracker.DAL.Interfaces
{
    public interface ITestDal
    {
        bool PurgeAndPopulateDatabase();
        bool PurgeEntries();
    }
}