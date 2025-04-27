using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL
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
                result = new ActivityPostRequestProcessor();
            }
            else if (data.ElementAt(1).ToLower() == "entry")
            {
                result = new EntryPostRequestProcessor();
            }
            else
            {
                result = new UnknownPostRequestProcessor();
            }

            _logger.LogDebug("PostRequestProcessorFactory.ReturnPostRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
