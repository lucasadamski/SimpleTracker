using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.DAL
{
    public class UnitSqlDal : SqlDalBase, IUnitSqlDal
    {
        public UnitSqlDal(ISQLDataAccess db, ILogger logger) : base(db, logger) { }

        public int? GetUnitId(string name)
        {
            int? result = 0;

            try
            {
                result = _db.LoadData<int, dynamic>(storedProcedure: "[dbo].[spUnit_GetId]", new { name }).First();
                _logger.LogDebug("[dbo].[spUnit_GetId] returned {UnitId} ", result);
            }
            catch (Exception e)
            {
                _logger.LogError("[dbo].[spUnit_GetId] returned exception {Exception}", e);
                result = null;
            }

            return result;
        }
    }
}
