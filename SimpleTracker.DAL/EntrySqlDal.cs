using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace SimpleTracker.DAL
{
    public class EntrySqlDal : IEntrySqlDal
    {
        private readonly ISQLDataAccess _db;

        public EntrySqlDal(ISQLDataAccess db)
        {
            _db = db;
        }

        public NewEntryResult CreateNewEntry(Entry entry)
        {
            //Utility.Logger.Log.LogDebug("dbo.spEntry_Insert {Value}", entry.Value);
            return (NewEntryResult)_db.SaveData(storedProcedure: "dbo.spEntry_Insert", new { entry.Value, entry.ActivityId });
        }
              
      
        public IEnumerable<Entry> GetAllEntries() => 
            _db.LoadData<Entry, dynamic>(storedProcedure: "dbo.Entry_GetAll", new { });


    }
}
