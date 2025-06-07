using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.BLL.RequestProcessor.Get
{
    public class EntryGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public EntryGetRequestProcessor(ILogger logger) : base(logger)
        {
        }
        public EntryGetRequestProcessor(ILogger logger, IEntrySqlDal entrySqlDal) : base(logger)
        {
            _entryDal = entrySqlDal;
        }

        // what type I have to return here? 
        public List<string> Process(List<string> data)
        {
            var result = new List<string>();
            IEnumerable<Entry> dalResult = new List<Entry>();

            MakeDalQuery(data, ref result, ref dalResult);
            result = MapDalResultToMethodResult(result, dalResult);

            return result;
        }

        private List<string> MapDalResultToMethodResult(List<string> result, IEnumerable<Entry> dalResult)
        {
            if (!dalResult.IsNullOrEmpty())
            {
                result = dalResult.Select(x => x.ActivityId.ToString() + " " + x.Value.ToString()).ToList();
                _logger.LogDebug("EntryGetRequestProcessor.Process success");
            }
            else
            {
                _logger.LogError("EntryGetRequestProcessor.Process DAL returned null or empty");
            }

            return result;
        }

        private void MakeDalQuery(List<string> data, ref List<string> result, ref IEnumerable<Entry> dalResult)
        {
            if (!data.IsNullOrEmpty() && data.Count > 1)
            {
                switch (data.ElementAt(1))
                {
                    case "entry":
                        dalResult = _entryDal.GetAllEntries();
                        break;
                    default:
                        result = new List<string>();
                        break;
                }
            }
            else
            {
                _logger.LogError("EntryGetRequestProcessor.Process argument is null or empty, or count is lower than 2");
            }
        }
    }
}
