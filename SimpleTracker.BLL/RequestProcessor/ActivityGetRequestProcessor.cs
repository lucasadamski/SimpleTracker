using SimpleTracker.BLL.Interface;
using Microsoft.Extensions.Logging;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class ActivityGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public ActivityGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            IEnumerable<Activity> result;

            result = _activityDal.GetAllActivities();
            _logger.LogDebug("ActivityGetRequestProcessor.Process success");

            return result.Select(x => x.Name + " " + x.UnitId.ToString()).ToList();
        }
    }
}
