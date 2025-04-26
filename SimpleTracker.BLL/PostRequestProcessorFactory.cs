using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL
{
    public class PostRequestProcessorFactory
    {
        public IPostRequestProcessor ReturnPostRequestProcessor(List<string> data)
        {
            if (data.ElementAt(1).ToLower() == "activity")
            {
                return new ActivityPostRequestProcessor();
            }
            else if (data.ElementAt(1).ToLower() == "entry")
            {
                return new EntryPostRequestProcessor();
            }
            else
            {
                return new UnknownPostRequestProcessor();
            }
        }
    }
}
