using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.DTO.Summary;

namespace SimpleTracker.DAL;

public class ActivityDal : DalBase, IActivityDal
{
    public ActivityDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
    {
    }

    public Activity ReadActivity(int id, int userId)
    {
        Activity result = new Activity();
        if (id > 0)
        {
            result = _db.LoadData<Activity, dynamic>(storedProcedure: "[dbo].[spActivity_Get]", parameters: new { id, userId }).FirstOrDefault();
        }
        return result ?? new Activity();
    }

    public bool CreateNewActivity(Activity activity)
    {
        var result = false;
        if(activity != null && activity?.Name != null && activity?.UserId != null && activity.UnitId > 0)
        {
            result = _db.SaveData(storedProcedure: "[dbo].[spActivity_Insert]", new { activity.Name, activity.UnitId, activity.UserId });
            _logger.LogDebug("[dbo].[spActivity_Insert] {Name} {UnitId} returned {Result}", activity.Name, activity.UnitId, result);
        }
        return result;
    }

    public bool DeleteActivity(int id)
    {
        var result = _db.SaveData("[dbo].[spActivity_Delete]", new { id });
        return result;
    }

    public bool UpdateActivity(Activity activity)
    {
        var result = false;
        if (activity != null && activity?.Name != null && activity?.UserId != null && activity.UnitId > 0)
        {
            result = _db.SaveData("[dbo].[spActivity_Update]", new { id = activity.Id, name = activity.Name, unitId = activity.UnitId, userId = activity.UserId });
        }
        return result;
    }

    public IEnumerable<Activity> GetAllActivities(int userId)
    {
        IEnumerable<Activity> result;
        if (userId > 0)
        {
            result = _db.LoadData<Activity, dynamic>(storedProcedure: "[dbo].[spActivity_GetAll]", parameters: new { userId });
            _logger.LogDebug("[dbo].[spActivity_GetAll] returned {ResultCount} items", result.Count());
        }
        else
        {
            result = new List<Activity>();
        }
        return result;
    }

    public IEnumerable<ActivityQuickStats> GetAllActivitiesQuickStats(int userId)
    {
        IEnumerable<ActivityQuickStats> result;
        if (userId > 0)
        {
            result = _db.LoadData<ActivityQuickStats, dynamic>(storedProcedure: "[dbo].[spActivity_GetQuickStatsForAllActivities]", parameters: new { userId });
            _logger.LogDebug("[dbo].[spActivity_GetQuickStatsForAllActivities] returned {ResultCount} items", result.Count());
        }
        else
        {
            result = new List<ActivityQuickStats>();
        }
        return result;
    }
}
