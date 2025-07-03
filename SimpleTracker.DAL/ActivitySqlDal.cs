using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using Activity = SimpleTracker.DTO.Activity;

namespace SimpleTracker.DAL
{
    public class ActivitySqlDal : SqlDalBase, IActivitySqlDal
    {
        public ActivitySqlDal(ISQLDataAccess db, ILogger logger) : base(db, logger)  
        { }

        public bool CreateNewActivity(Activity activity)
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spActivity_Insert]", new { activity.Name, activity.UnitId });
            _logger.LogDebug("dbo.spActivity_Insert {Name} {UnitId} returned {Result}", activity.Name, activity.UnitId, result);
            return result;
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            var result = _db.LoadData<Activity, dynamic>(storedProcedure: "[dbo].[spActivity_GetAll]", new { });
            _logger.LogDebug("dbo.Activity_GetAll returned {ResultCount} items", result.Count());
            return result;
        }

        public int? GetActivityId(string name, string userId)
        {
            int? result = 0;
            try
            {
                 result = _db.LoadData<int, dynamic>(storedProcedure: "[dbo].[spActivity_GetId]", new { name, userId }).First();
                _logger.LogDebug("[dbo].[spActivity_GetId] returned Id {ActivityId}", result);
            }
            catch (Exception e)
            {
                _logger.LogError("[dbo].[spActivity_GetId] can't find activity name {ActivityName}", name);
                result = null;
            }
            return result;
        }
    }
}