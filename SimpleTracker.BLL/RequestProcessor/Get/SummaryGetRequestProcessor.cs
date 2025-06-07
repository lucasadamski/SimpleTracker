using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.BLL.Interface;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.BLL.RequestProcessor.Get
{
    public class SummaryGetRequestProcessor : RequestProcessorBase, IGetRequestProcessor
    {
        public SummaryGetRequestProcessor(ILogger logger) : base(logger) { }

        public SummaryGetRequestProcessor(ILogger logger, ISummarySqlDal summarySqlDal) : base(logger)
        {
            _summarySqlDal = summarySqlDal;
        }
        public List<string> Process(List<string> data)
        {
            var result = new List<string>();
            IEnumerable<string> dalResult = new List<string>();

            if (!data.IsNullOrEmpty())
            {
                if (data.Contains("all-time"))
                {
                    dalResult = _summarySqlDal.GetSummary();
                }
            }
            else
            {
                _logger.LogError("SummaryGetRequestProcessor.Process argument is null or empty");
            }



            if (!dalResult.IsNullOrEmpty())
            {
                result = dalResult.ToList();
            }
            else
            {
                _logger.LogError("SummaryGetRequestProcessor.Process dal returned null or empty");
            }

            return result;
        }
    }
}
