using Microsoft.Extensions.Logging;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.BLL.RequestProcessor.Get
{
    public class SummaryGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public SummaryGetRequestProcessor(ILogger logger) : base(logger) { }

        public List<string> Process(List<string> data)
        {
            var result = new List<string>();

            if(data.Contains("all-time"))
            {
                result = _summarySqlDal.GetSummary().ToList();
            }

            return result;
        }
    }
}
