using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class EntrySqlDal : IEntrySqlDal
    {
        public NewEntryResult CreateNewRepetitions(Entry repetitions)
        {
            return new NewEntryResult();
        }
    }
}
