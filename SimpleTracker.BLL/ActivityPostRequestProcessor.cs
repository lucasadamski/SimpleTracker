
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.Utility;

namespace SimpleTracker.BLL
{
    public class ActivityPostRequestProcessor : IPostRequestProcessor
    {
        IActivitySqlDal _activityDal;

        public ActivityPostRequestProcessor()
        {
            _activityDal = new ActivitySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString));
        }

        public List<string> Process(List<string> data)
        {
            throw new NotImplementedException();
        }
    }
}
