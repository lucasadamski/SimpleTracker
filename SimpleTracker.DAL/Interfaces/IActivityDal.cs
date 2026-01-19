using SimpleTracker.DTO;
using SimpleTracker.DTO.Summary;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivityDal
    {
        bool CreateNewActivity(Activity activity);
        Activity ReadActivity(int id, int userId);
        bool UpdateActivity(Activity activity);
        bool DeleteActivity(int id);
        IEnumerable<Activity> GetAllActivities(int userId);
        IEnumerable<ActivityQuickStats> GetAllActivitiesQuickStats(int userId);
        IEnumerable<ActivityQuickStatsCompareWithPreviousDays> GetQuickStatsCompareWithPreviousDaysForAll(int userId);

    }
}