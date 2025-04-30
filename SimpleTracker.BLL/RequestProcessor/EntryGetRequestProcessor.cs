using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class EntryGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public EntryGetRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            IEnumerable<Entry> result;
            
            result = _entryDal.GetAllEntries();
            _logger.LogDebug("EntryGetRequestProcessor.Process success");

            return result.Select(x => x.ActivityId.ToString() + " " + x.Value.ToString()).ToList();
        }
    }
}
