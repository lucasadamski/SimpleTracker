using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class ActivitySqlDal : IActivitySqlDal
    {
        public NewActivityResult CreateNewActivity(Activity activity)
        {
            return new NewActivityResult();
        }
    }
}
