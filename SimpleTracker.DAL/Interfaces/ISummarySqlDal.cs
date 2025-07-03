namespace SimpleTracker.DAL.Interfaces
{
    public interface ISummarySqlDal
    {
        IEnumerable<string> GetSummary(string userId);
    }
}