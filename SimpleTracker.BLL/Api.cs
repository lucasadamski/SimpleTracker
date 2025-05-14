using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Factory;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL
{
    public class Api : IApi
    {
        private Response _response { get; set; } = new Response();
        private PostRequestProcessorFactory _postRequestProcessorFactory { get; set; }
        private GetRequestProcessorFactory _getRequestProcessorFactory { get; set; }
        private readonly ILogger _logger;

        public Api(ILogger logger)
        {
            _postRequestProcessorFactory = new PostRequestProcessorFactory(logger);
            _getRequestProcessorFactory = new GetRequestProcessorFactory(logger);
            _logger = logger;
        }

        public List<string> Request(List<string> data)
        {
            // result as a property
            _logger.LogInformation("Api.Request: Request received");

            SanitizeData(data);
            CheckTypeOfRequest(data);

            _response.Messages.AddRange(ProcessGetRequest(data));
            ProcessPostRequest(data);

            return _response.Messages;
        }

        private void ProcessPostRequest(List<string> data)
        {
            if (_response.IsPost == false)
                return;

            _logger.LogDebug("Api.ProcessPostRequest");
            IPostRequestProcessor postRequestProcessor = _postRequestProcessorFactory.ReturnPostRequestProcessor(data);
            _response.Messages = postRequestProcessor.Process(data); // should be return data not messages
        }
        private List<string> ProcessGetRequest(List<string> data)
        {
            string result;

            IGetRequestProcessor getRequestProcessor = _getRequestProcessorFactory.ReturnGetRequestProcessor(data);

            return getRequestProcessor.Process(data);
        }

        private void SanitizeData(List<string> data)
        {
            if (data.IsNullOrEmpty() || data.Count == 0)
            {
                _logger.LogError("Api.SanitizeData: No data received");
                data = new List<string>() { "empty" };
            }
            else
            {
                _logger.LogDebug("Api.SanitizeData: Data sanitized successfully");
            }
        }

        private void CheckTypeOfRequest(List<string> data)
        {
            if (_response.Success == false) 
                return;
            
            if (data.ElementAt(0).ToLower() == "get")
            {
                _response.IsGet = true;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is GET");
            }
            else if (data.ElementAt(0).ToLower() == "post")
            {
                _response.IsPost = true;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is POST");
            }
            else
            {
                _logger.LogError("Api.CheckTypeOfRequest: Can't determine request type");
                _response.Success = false;
            }
        }

    }
}
