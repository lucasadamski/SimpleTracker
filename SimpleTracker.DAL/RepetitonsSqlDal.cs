using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;

namespace SimpleTracker.DAL
{
    public class RepetitonsSqlDal : IRepetitonsSqlDal
    {
        public CreateNewRepetitionsResult CreateNewRepetitions(Repetitions repetitions)
        {
            return new CreateNewRepetitionsResult();
        }
    }
}
