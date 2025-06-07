using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.Api.Interface;
using SimpleTracker.BLL.DTO;
using SimpleTracker.BLL.Factory;
using SimpleTracker.BLL.RequestProcessor.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.Api
{
    public class Api : IApi
    {
        private IPostRequestProcessorFactory _postRequestProcessorFactory { get; set; }
        private IGetRequestProcessorFactory _getRequestProcessorFactory { get; set; }
        private readonly ILogger _logger;

        public Api(ILogger logger)
        {
            _postRequestProcessorFactory = new PostRequestProcessorFactory(logger);
            _getRequestProcessorFactory = new GetRequestProcessorFactory(logger);
            _logger = logger;
        } 

        public Api(ILogger logger, IPostRequestProcessorFactory postRequestProcessorFactory, IGetRequestProcessorFactory getRequestProcessorFactory)
        {
            _postRequestProcessorFactory = postRequestProcessorFactory;
            _getRequestProcessorFactory = getRequestProcessorFactory;
            _logger = logger;
        }

        public IEnumerable<string> Request(IEnumerable<string> arguments)
        {
            // result as a property
            _logger.LogInformation("Api.Request: Request received");

            var request = new Request() { Arguments = arguments };

            request = SanitizeData(request);
            if (!request.Success) return null;

            request = CheckTypeOfRequest(request);
            request = CheckTypeOfRequestedObject(request);
            request = TryAssignValue(request);

            // Shift to response from request

            var response = MapRequestToResponse(request);

            response = ProcessGetRequest(response);
            response = ProcessPostRequest(response);

            return response.Output;
        }

        private Request TryAssignValue(Request request)
        {
            var result = request;

            foreach (var argument in result.Arguments)
            {
                if (argument.All(char.IsDigit))
                {
                    result.Value = int.Parse(argument);
                }
            }

            return result;
        }

        private Request CheckTypeOfRequestedObject(Request request)
        {
            var result = request;

            if (result.Success == false)
                return result;

            try
            {
                result.Type = result.Arguments.ElementAt(1).ToLower().Trim() switch
                {
                    "activity" => typeof(Activity),
                    "entry" => typeof(Entry),
                    "summary" => typeof(Summary),
                    _ => null
                };
            }
            catch (Exception e)
            {
                result.Type = null;
            }

            return result;
        }

        private Response MapRequestToResponse(Request request)
        {
            var result = new Response();

            if (request.Success == false)
                return result;

            result.RequestVerb = request.RequestVerb;
            result.Success = request.Success;
            result.Arguments = request.Arguments;
            result.Type = request.Type;
            result.Value = request.Value;

            return result;
        }

        private Response ProcessPostRequest(Response response)
        {
            var result = response;
            if (result.Success == false || result.RequestVerb != RequestVerb.Post)
                return result;

            _logger.LogDebug("Api.ProcessPostRequest");

            IPostRequestProcessor postRequestProcessor = _postRequestProcessorFactory.ReturnPostRequestProcessor(response);
            result.Output = postRequestProcessor.Process(result.Arguments.ToList());

            return result;
        }

        private Response ProcessGetRequest(Response response)
        {
            var result = response;
            if (result.Success == false || result.RequestVerb != RequestVerb.Get)
                return result;

            _logger.LogDebug("Api.ProcessGetRequest");

            IGetRequestProcessor getRequestProcessor = _getRequestProcessorFactory.ReturnGetRequestProcessor(response);
            result.Output = getRequestProcessor.Process(result.Arguments.ToList());

            return result;
        }

        private Request SanitizeData(Request request)
        {
            var result = request;

            if (result.Success == false)
                return result;

            if (request.Arguments.IsNullOrEmpty() || request.Arguments.Count() == 0)
            {
                _logger.LogError("Api.SanitizeData: No data received");
                result.Arguments = new List<string>() { "empty" };
                request.Success = false;
            }
            if (request.Arguments.Contains(string.Empty) || request.Arguments.Contains(null))
            {
                _logger.LogError("Api.SanitizeData: One argument is empty or null");
                request.Success = false;
            }
            else
            {
                _logger.LogDebug("Api.SanitizeData: Data sanitized successfully");
            }

            return result;
        }

        private Request CheckTypeOfRequest(Request request)
        {
            var result = request;
            if (result.Success == false) 
                return result;
            
            if (result.Arguments.ElementAt(0).ToLower().Trim() == "get")
            { 
                result.RequestVerb = RequestVerb.Get;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is GET");
            }
            else if (result.Arguments.ElementAt(0).ToLower() == "post")
            {
                result.RequestVerb = RequestVerb.Post;
                _logger.LogDebug("Api.CheckTypeOfRequest: Request type is POST");
            }
            else
            {
                result.RequestVerb = RequestVerb.NotDefined;
                _logger.LogError("Api.CheckTypeOfRequest: Can't determine request type");
                result.Success = false;
            }

            return result;
        }

    }
}
