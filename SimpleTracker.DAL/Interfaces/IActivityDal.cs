using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivityDal
    {
        bool CreateNewActivity(Activity activity);
        void GetActivity(int id);
        IEnumerable<Activity> GetAllActivities(string userId);
    }
}