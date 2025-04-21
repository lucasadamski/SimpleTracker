using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.DTO;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL
{
    public class Api : IApi
    {
        private RequestResult result { get; set; }
        private PostRequestProcessorFactory postRequestProcessorFactory { get; set; }

        public Api()
        {
            result = new RequestResult();
            postRequestProcessorFactory = new PostRequestProcessorFactory();
        }

        public List<string> Request(List<string> data)
        {
            // result as a property

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

            IPostRequestProcessor postRequestProcessor =  postRequestProcessorFactory.ReturnPostRequestProcessor(data);
            result.Messages = postRequestProcessor.Process(data);
        }

        private void ProcessGetRequest(List<string> data)
        {
            if (result.IsGet == false)
                return;
        }

        private void SanitizeData(List<string> data)
        {
            if (data.IsNullOrEmpty() || data.Count == 0)
            {
                data = new List<string>() { "empty" };

                result.Messages.Add("No data received");
                result.Success = false;
            }
            else
            {
                result.Messages.Add("Data received");
            }
        }

        private void CheckTypeOfRequest(List<string> data)
        {
            if (result.Success == false) 
                return;
            
            if (data.ElementAt(0).ToLower() == "get")
            {
                result.IsGet = true;
                result.Messages.Add("Request type is GET");
            }
            else if (data.ElementAt(0).ToLower() == "post")
            {
                result.IsPost = true;
                result.Messages.Add("Request type is POST");
            }
            else
            {
                result.Success = false;
                result.Messages.Add("Can't determine request type");
            }
        }

    }
}
