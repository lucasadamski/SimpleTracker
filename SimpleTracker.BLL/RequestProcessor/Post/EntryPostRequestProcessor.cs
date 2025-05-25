using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor.Post
{
    public class EntryPostRequestProcessor : RequestProcessorBase, IPostRequestProcessor
    {
        public EntryPostRequestProcessor(ILogger logger) : base(logger)
        {
        }

        public List<string> Process(List<string> data)
        {
            var result = new List<string>();

            var entry = new Entry();


            try
            {
                entry.Value = TryAssignValue(data);
                if (entry.Value == -1)
                {
                    throw new Exception("Can't find value in post entry arguments");
                }


                int? activityId = _activityDal.GetActivityId(data.ElementAt(2).ToLower().Trim());
                if (activityId == null)
                {
                    throw new Exception("Can't process agruments");
                }

                entry.ActivityId = (int)activityId;

                _entryDal.CreateNewEntry(entry);
                _logger.LogDebug("ActivityPostRequestProcessor.Process success");
            }
            catch (Exception e)
            {
                _logger.LogError("ActivityPostRequestProcessor.Process exception: {ExceptionMessage}", e.Message);
            }

            result.Add("Entry created with success");

            return result;
        }

        private int TryAssignValue(IEnumerable<string> data)
        {
            var result = -1;

            foreach (var argument in data)
            {
                if (argument.All(char.IsDigit))
                {
                    result = int.Parse(argument);
                }
            }

            return result;
        }
    }
}
