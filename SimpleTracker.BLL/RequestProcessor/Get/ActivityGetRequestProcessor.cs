using SimpleTracker.BLL.Interface;
using Microsoft.Extensions.Logging;
using SimpleTracker.DTO;
using SimpleTracker.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace SimpleTracker.BLL.RequestProcessor.Get
{
    public class ActivityGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public ActivityGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public ActivityGetRequestProcessor(ILogger logger, IActivitySqlDal activitySqlDal) : base(logger)
        {
            _activityDal = activitySqlDal;
        }

        public List<string> Process(List<string> data)
        {
            var result = new List<string>();

            if(!data.IsNullOrEmpty())
            {
                IEnumerable<Activity> dalResult;

                dalResult = _activityDal.GetAllActivities();
                if (!dalResult.IsNullOrEmpty())
                {
                    _logger.LogDebug("ActivityGetRequestProcessor.Process success");
                    result = dalResult.Select(x => x.Name + " " + x.UnitId.ToString()).ToList();
                }
                else
                {
                    _logger.LogError("ActivityGetRequestProcessor.Process Dal returned null or empty");
                }
            }
            else
            {
                _logger.LogError("ActivityGetRequestProcessor.Process argument is null or empty");
            }

            return result;
        }
    }
}