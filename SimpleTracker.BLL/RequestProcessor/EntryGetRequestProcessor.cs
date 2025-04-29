using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class EntryGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public EntryGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            throw new NotImplementedException();
        }
    }
}
