using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class EntrySqlDal : SqlDalBase, IEntrySqlDal
    {
        public EntrySqlDal(ISQLDataAccess db, ILogger logger) : base(db, logger)
        { }

        public bool CreateNewEntry(Entry entry)
        {
            var result = _db.SaveData(storedProcedure: "[dbo].[spEntry_Insert]", new { entry.Value, entry.ActivityId });
            _logger.LogDebug("dbo.spEntry_Insert {Value} returned {Result}", entry.Value, result);
            return result;
        }
              
        public IEnumerable<Entry> GetAllEntries()
        {
            var result = _db.LoadData<Entry, dynamic>(storedProcedure: "[dbo].[spEntry_GetAll]", new { });
            _logger.LogDebug("dbo.Entry_GetAll returned {ResultCount} items", result.Count());
            return result;
        }

        public IEnumerable<string> GetSummaryAllTime()
        {
            IEnumerable<string> result;

            try
            {
                var spResult = _db.LoadData<EntrySummaryAllTime, dynamic>(storedProcedure: "[dbo].[spSummary_Get]", new { });
                result = spResult.Select(n => n.Name + " " + n.Value.ToString() + " " + n.Reps).ToList();

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
