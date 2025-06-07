using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.RequestProcessor.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor.Post
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

            try
            {
                activity.Name = data.ElementAt(2).ToLower().Trim();
                activity.UnitId = GetUnitId(data);

                _activityDal.CreateNewActivity(activity);

                result.Add($"Activity {activity.Name} created successfully");
                _logger.LogDebug("ActivityPostRequestProcessor.Process Activity {ActivityName} created successfully", activity.Name);
            }
            catch (Exception e)
            {
                _logger.LogError("ActivityPostRequestProcessor.Process exception: {ExceptionMessage}", e.Message);
            }

            return result;
        }

        private int GetUnitId(List<string> data)
        {
            var unitName = data.ElementAt(3).ToLower().Trim();
            int? unitId = _unitSqlDal.GetUnitId(unitName);
            if (unitId == null)
            {
                throw new Exception(string.Format("{0} is not a valid unit name", unitName));
            }
            return (int)unitId;
        }
    }
}
