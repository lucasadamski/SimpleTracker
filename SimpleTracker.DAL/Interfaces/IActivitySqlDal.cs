using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivitySqlDal
    {
        public bool CreateNewActivity(Activity item);
        public IEnumerable<Activity> GetAllActivities();
        public int? GetActivityId(string name, string userId);
    }
}
