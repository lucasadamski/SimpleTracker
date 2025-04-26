using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using Activity = SimpleTracker.DTO.Activity;

namespace SimpleTracker.DAL
{
    public class ActivitySqlDal : IActivitySqlDal
    {
        private readonly ISQLDataAccess _db;
        private readonly ILogger _logger;
        public ActivitySqlDal(ISQLDataAccess db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public Result CreateNewActivity(Activity activity)
        {
            var result = _db.SaveData(storedProcedure: "dbo.spActivity_Insert", new { activity.Name, activity.UnitId });
            _logger.LogDebug("dbo.spActivity_Insert {Name} {UnitId} returned {Result} {ResultMessage}", activity.Name, activity.UnitId, result.Success, result.Message);
            return result;
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            var result = _db.LoadData<Activity, dynamic>(storedProcedure: "dbo.Activity_GetAll", new { });
            _logger.LogDebug("dbo.Activity_GetAll returned {ResultCount} items", result.Count());
            return result;
        }
    }
}