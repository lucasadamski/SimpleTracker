using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IEntrySqlDal
    {
        public bool CreateNewEntry(Entry repetitions);
        public IEnumerable<Entry> GetAllEntries();
    }
}
