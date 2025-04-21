using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IActivitySqlDal
    {
        public NewActivityResult CreateNewActivity(Activity item);
    }
}
