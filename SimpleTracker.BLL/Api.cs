using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL
{
    public class Api : IApi
    {
        private RequestInput _input;
        private RequestResult result { get; set; }
        private PostRequestProcessorFactory postRequestProcessorFactory { get; set; }
        private readonly ILogger _logger;


        public Api(ILogger logger)
        {
            result = new RequestResult();
            postRequestProcessorFactory = new PostRequestProcessorFactory();
            _input = new RequestInput();
            _logger = logger;
        }

        public List<string> Request(List<string> data)
        {
            // result as a property
            _logger.LogInformation("Api.Request: Request received");

            SanitizeData(data);
            CheckTypeOfRequest(data);

            ProcessGetRequest(data);
            ProcessPostRequest(data);

            return result.Messages;
        }

        private void ProcessPostRequest(List<string> data)
        {
            if (result.IsPost == false)
                return;

            _logger.LogDebug("Api.ProcessPostRequest");
            IPostRequestProcessor postRequestProcessor =  postRequestProcessorFactory.ReturnPostRequestProcessor(data);
            result.Messages = postRequestProcessor.Process(data);
        }

        private void ProcessGetRequest(List<string> data)
        {
            return;
            throw new NotImplementedException();
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
            if (result.Success == false) 
                return;
            
            if (data.ElementAt(0).ToLower() == "get")
            {
                result.IsGet = true;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is GET");
            }
            else if (data.ElementAt(0).ToLower() == "post")
            {
                result.IsPost = true;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is POST");
            }
            else
            {
                _logger.LogError("Api.CheckTypeOfRequest: Can't determine request type");
                result.Success = false;
            }
        }

    }
}
