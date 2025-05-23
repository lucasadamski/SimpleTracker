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

        // what type I have to return here? 
        public List<string> Process(List<string> data)
        {
            IEnumerable<string> result;

            switch(data.ElementAt(1))
            {
                case "entry":
                    result = _entryDal.GetAllEntries().Select(x => x.ActivityId.ToString() + " " + x.Value.ToString()).ToList();
                    break;
                case "summaryAllTime":
                    result = _entryDal.GetSummaryAllTime();
                    break;
                default:
                    result = new List<string>();
                    break;
            }
            
            return result.ToList();
        }
    }
}
