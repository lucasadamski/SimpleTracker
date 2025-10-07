using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivityDal
    {
        bool CreateNewActivity(Activity activity);
        Activity ReadActivity(int id, string userId);
        bool DeleteActivity(int id);
        IEnumerable<Activity> GetAllActivities(string userId);
    }
}