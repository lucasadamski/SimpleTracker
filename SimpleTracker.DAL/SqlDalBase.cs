using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL
{
    public class SqlDalBase
    {
        protected readonly ISQLDataAccess _db;
        protected readonly ILogger _logger;

        public SqlDalBase(ISQLDataAccess db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }
    }
}