using Serilog;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using System.Runtime.InteropServices;

namespace SimpleTracker.DAL
{
    public class EntryDal : DalBase, IEntryDal
    {
        public EntryDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {

        }
        // implement EntryDal 
        // 

        public bool CreateNewEntry(Entry entry)
        {
            var result = false;
            try
            {
                if (entry.Value < 1) throw new Exception("Invalid entry properties");
                result = _db.SaveData(storedProcedure: "[dbo].[spEntry_Insert]", new { value = entry.Value, activityId = entry.ActivityId, dateAdded = entry.DateAdded }); // sql signature return: bit
                _logger.Debug("[dbo].[spEntry_Insert] Value: {Value} ActivityId: {ActivityId} returned {Result}", entry.Value, entry.ActivityId, result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            
            return result;
        }
        public Entry ReadEntry(int id, int userId)
        {
            var result = new Entry();
            try
            {
                result = _db.LoadData<Entry, dynamic>(storedProcedure: "[dbo].[spEntry_Read]", new { id, userId }).FirstOrDefault(); // sql signature return: Entry
                _logger.Debug("[dbo].[spEntry_Read] Value: {Value} ActivityId: {ActivityId} returned {Result}", result.Value, result.ActivityId, result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                result = new EntryEmpty();
            }
            return result;
        }

        public IEnumerable<EntryDto> ReadEntriesDto(int userId, [Optional] DateTime? from, [Optional] DateTime? to)
        {
            IEnumerable<EntryDto> result;
            try
            {
                result = _db.LoadData<EntryDto, dynamic>(storedProcedure: "[dbo].[spEntriesDto_Read]", new { userId, from, to }); // sql signature return: collection of Entry
                _logger.Debug("[dbo].[spEntriesDto_Read] Count: {Count}", result.Count());
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                result = new List<EntryDto>();
            }
            return result;
        }

        public Entries ReadEntries(int userId, [Optional] DateTime? from, [Optional] DateTime? to)
        {
            var result = new Entries();
            try
            {
                result.Data = _db.LoadData<Entry, dynamic>(storedProcedure: "[dbo].[spEntries_Read]", new { userId, from, to }); // sql signature return: collection of Entry
                _logger.Debug("[dbo].[spEntries_Read] Count: {Count} ActivityId: {ActivityId} returned {Result}", result.Data.Count());
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return result;
        }
        public bool UpdateEntry(int id, int value)
        {
            var result = false;
            try
            {
                result = _db.SaveData(storedProcedure: "[dbo].[spEntry_Update]", new { id, value }); // sql signature return: bit
                _logger.Debug("[dbo].[spEntry_Update] returned {Result}", result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return result;
        }
        public bool DeleteEntry(int id)
        {
            var result = false;
            try
            {
                result = _db.SaveData(storedProcedure: "[dbo].[spEntry_Delete]", new { id }); // sql signature return: bit
                _logger.Debug("[dbo].[spEntry_Delete] returned {Result}", result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
            return result;
        }

    }
}
