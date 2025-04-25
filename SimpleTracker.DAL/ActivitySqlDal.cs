using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class ActivitySqlDal : IActivitySqlDal
    {
        private readonly ISQLDataAccess _db;
        public ActivitySqlDal(ISQLDataAccess db)
        {
            _db = db;
        }

        public Result CreateNewActivity(Activity activity)
        {
            Utility.Logger.Log.LogDebug("dbo.spActivity_Insert {Name} {UnitId}", activity.Name, activity.UnitId);
            return _db.SaveData(storedProcedure: "dbo.spActivity_Insert", new { activity.Name, activity.UnitId });
        }

        public IEnumerable<Activity> GetAllActivities() =>
            _db.LoadData<Activity, dynamic>(storedProcedure: "dbo.Activity_GetAll", new { });
    }
}
