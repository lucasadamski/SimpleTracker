using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IEntrySqlDal
    {
        public Result CreateNewEntry(Entry repetitions);

        public IEnumerable<Entry> GetAllEntries();
    }
}
