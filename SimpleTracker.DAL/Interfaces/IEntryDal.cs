using SimpleTracker.DTO;
using System.Runtime.InteropServices;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IEntryDal
    {
        bool CreateNewEntry(Entry entry);
        bool DeleteEntry(int id);
        Entries ReadEntries(int userId, [Optional] DateTime? from, [Optional] DateTime? to);
        IEnumerable<EntryDto> ReadEntriesDto(int userId, [Optional] DateTime? from, [Optional] DateTime? to);
        Entry ReadEntry(int id, int userId);
        bool UpdateEntry(int id, int value);
    }
}