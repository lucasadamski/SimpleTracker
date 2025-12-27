using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IUnitDal
    {
        IEnumerable<Unit> GetAll();
    }
}