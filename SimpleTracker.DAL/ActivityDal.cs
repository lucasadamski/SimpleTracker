using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL;

public class ActivityDal : DalBase
{
    public ActivityDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
    {
    }

    public void GetActivity(int id)
    {
        
    }

    public bool CreateNewActivity(Activity activity)
    {
        var result = _db.SaveData(storedProcedure: "[SimpleTrackerDev].[dbo].[s]pActivity_Insert]", new { activity.Name, activity.UnitId });
        _logger.LogDebug("dbo.spActivity_Insert {Name} {UnitId} returned {Result}", activity.Name, activity.UnitId, result);
        return result;
    }

    public IEnumerable<Activity> GetAllActivities()
    {
        var result = _db.LoadData<Activity, dynamic>(storedProcedure: "[SimpleTrackerDev].[dbo].[spActivity_GetAll]", new { });
        _logger.LogDebug("dbo.Activity_GetAll returned {ResultCount} items", result.Count());
        return result;
    }
}
