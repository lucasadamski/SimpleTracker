using Dapper;
using System.Diagnostics;

namespace SimpleTracker.DAL;

public class ActivityDal : DalBase
{
    private ISqlDataAccess SqlDataAccess { get; set; }

    public ActivityDal(ISqlDataAccess sqlDataAccess)
    {
        SqlDataAccess = sqlDataAccess;
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
