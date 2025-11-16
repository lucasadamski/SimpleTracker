using SimpleTracker.DTO;
using System.Runtime.InteropServices;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IEntryDal
    {
        bool CreateNewEntry(Entry entry);
        bool DeleteEntry(int id);
        Entries ReadEntries(string userId, [Optional] DateTime? from, [Optional] DateTime? to);
        Entry ReadEntry(int id, string userId);
        bool UpdateEntry(int id, int value);
    }
}