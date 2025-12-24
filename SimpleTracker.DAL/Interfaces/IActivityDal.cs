using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivityDal
    {
        bool CreateNewActivity(Activity activity);
        Activity ReadActivity(int id, int userId);
        bool UpdateActivity(Activity activity);
        bool DeleteActivity(int id);
        IEnumerable<Activity> GetAllActivities(int userId);
    }
}