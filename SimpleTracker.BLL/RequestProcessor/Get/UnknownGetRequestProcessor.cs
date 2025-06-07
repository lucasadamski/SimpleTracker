using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL.RequestProcessor.Get
{
    public class UnknownGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public UnknownGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            _logger.LogError("UnknownGetRequestProcessor.Process called");
            return new List<string>() { "Item type unknown. Must be activity or entry" };
        }
    }
}
