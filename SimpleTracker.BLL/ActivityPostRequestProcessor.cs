
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.Utility;

namespace SimpleTracker.BLL
{
    public class ActivityPostRequestProcessor : IPostRequestProcessor
    {
        IActivitySqlDal _activityDal;

        public ActivityPostRequestProcessor()
        {
            _activityDal = new ActivitySqlDal(new SQLDataAccess(DBConnectionString.ConnectionString));
        }

        public List<string> Process(List<string> data)
        {
            var result = new List<string>();

            var activity = new Activity();

            var newActivityResult = new NewActivityResult();

            try
            {
                activity.Name = data.ElementAt(2).ToLower().Trim();
                activity.UnitId = int.Parse(data.ElementAt(3).ToLower().Trim());

                result.Add(_activityDal.CreateNewActivity(activity).Message);
            }
            catch (Exception e)
            {
                
                
            }

            return result;
        }
    }
}
