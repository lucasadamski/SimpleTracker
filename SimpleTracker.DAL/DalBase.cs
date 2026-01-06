using Serilog;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL;

public class DalBase
{
    protected readonly ISqlDataAccess _db;
    protected readonly ILogger _logger;

    public DalBase(ISqlDataAccess db, ILogger logger)
    {
        _db = db;
        _logger = logger;
    }
} 
