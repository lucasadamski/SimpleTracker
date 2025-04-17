using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class EntrySqlDal : IEntrySqlDal
    {
        public NewEntryResult CreateNewEntry(Entry entry)
        {
            return new NewEntryResult();
        }
    }
}
