
using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor
{
    public class EntryPostRequestProcessor : RequestProcessorBase, IPostRequestProcessor
    {
        public EntryPostRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            var result = new List<string>();
            var dalResult = new Result();

            var entry = new Entry();
            var newActivityResult = new NewActivityResult();

            try
            {
                entry.Value = int.Parse(data.ElementAt(2));
                entry.ActivityId = int.Parse(data.ElementAt(3).ToLower().Trim());

                dalResult = _entryDal.CreateNewEntry(entry);
                _logger.LogDebug("ActivityPostRequestProcessor.Process success");
            }
            catch (Exception e)
            {
                _logger.LogDebug("ActivityPostRequestProcessor.Process exception: {ExceptionMessage}", e.Message);
            }

            result.Add("Entry created with " + dalResult.Success.ToString());

            return result;
        }
    }
}
