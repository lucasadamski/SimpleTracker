using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivitySqlDal
    {
        public Result CreateNewActivity(Activity item);
    }
}
