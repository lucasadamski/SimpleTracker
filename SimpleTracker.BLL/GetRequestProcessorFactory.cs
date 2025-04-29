using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL
{
    public class GetRequestProcessorFactory 
    {
        private readonly ILogger _logger;

        public GetRequestProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IGetRequestProcessor ReturnGetRequestProcessor(List<string> data)
        {
            IGetRequestProcessor result;

            if (data.ElementAt(1).ToLower() == "activity")
            {
                result = new ActivityGetRequestProcessor();
            }
            else if (data.ElementAt(1).ToLower() == "entry")
            {
                result = new EntryGetRequestProcessor();
            }
            else
            {
                result = new UnknownGetRequestProcessor();
            }

            _logger.LogDebug("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
