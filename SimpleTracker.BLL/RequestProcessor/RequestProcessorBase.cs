using Microsoft.Extensions.Logging;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.Utility;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class RequestProcessorBase
    {
        protected IActivitySqlDal _activityDal;
        protected readonly ILogger _logger;

        public RequestProcessorBase(ILogger logger)
        {
            _activityDal = new ActivitySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString), logger);
        }
    }
}
