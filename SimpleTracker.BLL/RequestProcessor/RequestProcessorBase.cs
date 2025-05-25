using Microsoft.Extensions.Logging;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.Utility;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class RequestProcessorBase
    {
        protected IActivitySqlDal _activityDal;
        protected IEntrySqlDal _entryDal;
        protected ISummarySqlDal _summarySqlDal;
        protected IUnitSqlDal _unitSqlDal;
        protected readonly ILogger _logger;

        public RequestProcessorBase(ILogger logger)
        {
            _activityDal = new ActivitySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString, logger), logger);
            _entryDal = new EntrySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString, logger), logger);
            _summarySqlDal = new SummarySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString, logger), logger);
            _unitSqlDal = new UnitSqlDal(new SQLDataAccess(DBConnectionString.ConnectionString, logger), logger);
            _logger = logger;
        }
    }
}
