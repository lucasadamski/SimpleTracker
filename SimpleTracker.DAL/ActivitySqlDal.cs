using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class ActivitySqlDal : IActivitySqlDal
    {
        public NewActivityResult CreateNewItem(Activity item)
        {
            return new NewActivityResult();
        }
    }
}
