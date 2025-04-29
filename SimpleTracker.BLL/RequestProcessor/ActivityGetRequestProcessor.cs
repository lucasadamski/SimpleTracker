using SimpleTracker.BLL.Interface;
using Microsoft.Extensions.Logging;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class ActivityGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public ActivityGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            throw new NotImplementedException();
        }
    }
}
