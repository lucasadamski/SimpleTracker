using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class SummarySqlDal : SqlDalBase, ISummarySqlDal
    {
        public SummarySqlDal(ISQLDataAccess db, ILogger logger) : base(db, logger) { }

        public IEnumerable<string> GetSummary()
        {
            IEnumerable<string> result;

            try
            {
                var spResult = _db.LoadData<Summary, dynamic>(storedProcedure: "[dbo].[spSummary_Get]", new { });
                result = spResult.Select(n => n.Activity + " " + n.Value.ToString() + " " + n.Unit).ToList();

                _logger.LogDebug("[dbo].[spEntry_GetSummaryAllTime] returned {ResultCount} items", result.Count());
            }
            catch (Exception e)
            {
                _logger.LogError("[dbo].[spEntry_GetSummaryAllTime] returned exception {Exception}", e);
                result = new List<string>();
            }

            return result;
        }
    }
}
