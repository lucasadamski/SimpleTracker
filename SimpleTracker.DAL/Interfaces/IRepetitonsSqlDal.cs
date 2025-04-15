using SimpleTracker.DTO;

namespace SimpleTracker.DAL.Interfaces
{
    public interface IRepetitonsSqlDal
    {
        public CreateNewRepetitionsResult CreateNewRepetitions(Repetitions repetitions);
    }
}
