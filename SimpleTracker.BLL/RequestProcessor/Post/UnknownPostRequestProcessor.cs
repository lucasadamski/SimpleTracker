
using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.RequestProcessor.Interface;

namespace SimpleTracker.BLL.RequestProcessor.Post
{
    public class UnknownPostRequestProcessor : RequestProcessorBase, IPostRequestProcessor
    {
        public UnknownPostRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            return new List<string>() { "Item type unknown. Must be activity or entry" };
        }
    }
}
