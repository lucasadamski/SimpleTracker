using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IEntrySqlDal
    {
        public NewEntryResult CreateNewEntry(Entry repetitions);
    }
}
