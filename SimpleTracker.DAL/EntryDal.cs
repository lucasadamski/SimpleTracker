using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL
{
    public class EntryDal : DalBase
    {
        public EntryDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {
            // todo write EntryDal body, so that test will pass
        }
    }
}
