using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL;

public class ActivityDal : DalBase, IActivityDal
{
    public ActivityDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
    {
    }

    public void GetActivity(int id)
    {

    }

    public bool CreateNewActivity(Activity activity)
    {
        var result = _db.SaveData(storedProcedure: "[dbo].[spActivity_Insert]", new { activity.Name, activity.UnitId, activity.UserId });
        _logger.LogDebug("[dbo].[spActivity_Insert] {Name} {UnitId} returned {Result}", activity.Name, activity.UnitId, result); //todo move loggers to layer down
        return result;
    }

    public bool DeleteActivity(int id)
    {
        var result = _db.SaveData("[dbo].[spActivity_Delete]", new { id });
        return result;
    }

    public IEnumerable<Activity> GetAllActivities(string userId)
    {
        var result = _db.LoadData<Activity, dynamic>(storedProcedure: "[dbo].[spActivity_GetAll]", parameters: new { userId });
        _logger.LogDebug("[dbo].[spActivity_GetAll] returned {ResultCount} items", result.Count());
        return result;
    }
}
