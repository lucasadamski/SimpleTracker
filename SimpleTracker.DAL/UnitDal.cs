using Microsoft.Extensions.Logging;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
namespace SimpleTracker.DAL
{
    public class UnitDal : DalBase, IUnitDal
    {
        public UnitDal(ISqlDataAccess db, ILogger logger) : base(db, logger)
        {

        }

        public IEnumerable<Unit> GetAll()
        {
            IEnumerable<Unit> result;
            try
            {
                result = _db.LoadData<Unit, dynamic>(storedProcedure: "[dbo].[spUnit_GetAll]", new { });
                _logger.LogDebug("[dbo].[spUnit_GetAll]");
            }
            catch (Exception e)
            {
                result = new List<Unit>();
                _logger.LogError(e.Message);
            }

            return result;
        }

    }
}
