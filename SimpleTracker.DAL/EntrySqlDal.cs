using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class EntrySqlDal : SqlDalBase, IEntrySqlDal
    {
        public EntrySqlDal(ISQLDataAccess db, ILogger logger) : base(db, logger)
        { }

        public Result CreateNewEntry(Entry entry)
        {
            var result = _db.SaveData(storedProcedure: "dbo.spEntry_Insert", new { entry.Value, entry.ActivityId });
            _logger.LogDebug("dbo.spEntry_Insert {Value} returned {Result} {Message}", entry.Value, result.Success, result.Message);
            return result;
        }
              
        public IEnumerable<Entry> GetAllEntries()
        {
            var result = _db.LoadData<Entry, dynamic>(storedProcedure: "dbo.Entry_GetAll", new { });
            _logger.LogDebug("dbo.Entry_GetAll returned {ResultCount} items", result.Count());
            return result;
        }
    }
}
