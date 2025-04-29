using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.BLL.RequestProcessor;

namespace SimpleTracker.BLL.Factory
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
                result = new ActivityGetRequestProcessor(_logger);
            }
            else if (data.ElementAt(1).ToLower() == "entry")
            {
                result = new EntryGetRequestProcessor(_logger);
            }
            else
            {
                result = new UnknownGetRequestProcessor(_logger);
            }

            _logger.LogDebug("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
