using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class ActivityPostRequestProcessor : RequestProcessorBase, IPostRequestProcessor
    {
        public ActivityPostRequestProcessor(ILogger logger) : base(logger)
        {
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
