using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Interface;
using SimpleTracker.BLL.RequestProcessor.Get;
using SimpleTracker.DAL;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.Factory
{
    public class GetRequestProcessorFactory : IGetRequestProcessorFactory
    {
        private readonly ILogger _logger;

        public GetRequestProcessorFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IGetRequestProcessor ReturnGetRequestProcessor(Response response)
        {
            IGetRequestProcessor result;

            if (response == null)
            {
                result = new UnknownGetRequestProcessor(_logger);
                _logger.LogWarning("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);
            }
            else if (response.Type == typeof(Activity))
            {
                result = new ActivityGetRequestProcessor(_logger);
            }
            else if (response.Type == typeof(Entry))
            {
                result = new EntryGetRequestProcessor(_logger);
            }
            else if(response.Type == typeof(Summary))
            {
                result = new SummaryGetRequestProcessor(_logger);
            }
            else
            {
                result = new UnknownGetRequestProcessor(_logger);
                _logger.LogWarning("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);
            }

            _logger.LogDebug("GetRequestProcessorFactory.ReturnGetRequestProcessor returned {Result}", result.GetType().Name);

            return result;
        }
    }
}
