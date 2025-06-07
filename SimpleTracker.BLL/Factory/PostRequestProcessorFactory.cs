using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.BLL.RequestProcessor.Interface;
using SimpleTracker.BLL.RequestProcessor.Post;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.Factory
{
    public class PostRequestProcessorFactory : IPostRequestProcessorFactory
    {
        private readonly ILogger _logger;

        public PostRequestProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }
        public IPostRequestProcessor ReturnPostRequestProcessor(Response response)
        {
            IPostRequestProcessor result;

            if (response == null)
            {
                result = new UnknownPostRequestProcessor(_logger);
                _logger.LogWarning("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);
            }
            else if (response.Type == typeof(Activity))
            {
                result = new ActivityPostRequestProcessor(_logger);
            }
            else if (response.Type == typeof(Entry))
            {
                result = new EntryPostRequestProcessor(_logger);
            }
            else
            {
                result = new UnknownPostRequestProcessor(_logger);
                _logger.LogWarning("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);
            }

            _logger.LogDebug("PostRequestProcessorFactory.ReturnPostRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
