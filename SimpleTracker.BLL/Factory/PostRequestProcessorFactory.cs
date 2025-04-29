using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.BLL.RequestProcessor;

namespace SimpleTracker.BLL.Factory
{
    public class PostRequestProcessorFactory
    {
        private readonly ILogger _logger;

        public PostRequestProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }
        public IPostRequestProcessor ReturnPostRequestProcessor(List<string> data)
        {
            IPostRequestProcessor result;

            if (data.ElementAt(1).ToLower() == "activity")
            {
                result = new ActivityPostRequestProcessor(_logger);
            }
            else if (data.ElementAt(1).ToLower() == "entry")
            {
                result = new EntryPostRequestProcessor(_logger);
            }
            else
            {
                result = new UnknownPostRequestProcessor(_logger);
            }

            _logger.LogDebug("PostRequestProcessorFactory.ReturnPostRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
